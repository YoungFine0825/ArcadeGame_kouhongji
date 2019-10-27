﻿// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2014/09/30 11:59
// 
// License Copyright (c) Daniele Giardini.
// This work is subject to the terms at http://dotween.demigiant.com/license.php

using System;
using System.IO;
using System.Reflection;
using DG.DOTweenEditor.Core;
using UnityEditor;
using UnityEngine;

namespace DG.DOTweenEditor
{
    /// <summary>
    /// Not used as menu item anymore, but as a utiity function
    /// </summary>
    class DOTweenSetupMenuItem
    {
        const string _Title = "DOTween Setup";
        static Assembly _proEditorAssembly;

        /// <summary>
        /// Setups DOTween
        /// </summary>
        /// <param name="partiallySilent">If TRUE, no warning window appears in case there is no need for setup</param>
        public static void Setup(bool partiallySilent = false)
        {
            bool setupRequired = EditorUtils.DOTweenSetupRequired();
            if (setupRequired) {
                string msg = "Based on your Unity version (" + Application.unityVersion + ") and eventual plugins, DOTween will now activate additional tween elements, if available.";
                if (!EditorUtility.DisplayDialog(_Title, msg, "Ok", "Cancel")) return;
            } else {
                if (!partiallySilent) {
                    string msg = "This project has already been setup for your version of DOTween.\nReimport DOTween if you added new compatible external assets or upgraded your Unity version.";
                    if (!EditorUtility.DisplayDialog(_Title, msg, "Force Setup", "Cancel")) return;
                } else return;
            }

            string addonsDir = EditorUtils.dotweenDir;
            string proAddonsDir = EditorUtils.dotweenProDir;

            EditorUtility.DisplayProgressBar(_Title, "Please wait...", 0.25f);

            int totImported = 0;
            // Unity version-based files
            string[] vs = Application.unityVersion.Split("."[0]);
            int majorVersion = Convert.ToInt32(vs[0]);
            int minorVersion = Convert.ToInt32(vs[1]);
            if (majorVersion < 4) {
                SetupComplete(addonsDir, proAddonsDir, totImported);
                return;
            }
            if (majorVersion == 4) {
                if (minorVersion < 3) {
                    SetupComplete(addonsDir, proAddonsDir, totImported);
                    return;
                }
                totImported += ImportAddons("43", addonsDir);
                if (minorVersion >= 6) totImported += ImportAddons("46", addonsDir);
            } else {
                // 5.x
                totImported += ImportAddons("43", addonsDir);
                totImported += ImportAddons("46", addonsDir);
                totImported += ImportAddons("50", addonsDir);
            }
            // Additional plugin files
            // Pro plugins
            if (EditorUtils.hasPro) {
                // PRO > 2DToolkit shortcuts
                if (Has2DToolkit()) {
                    totImported += ImportAddons("Tk2d", proAddonsDir);
                    ProEditor_AddGlobalDefine("DOTWEEN_TK2D");
                } else ProEditor_RemoveGlobalDefine("DOTWEEN_TK2D");
                // PRO > TextMeshPro shortcuts
                if (HasTextMeshPro()) {
                    totImported += ImportAddons("TextMeshPro", proAddonsDir);
                    ProEditor_AddGlobalDefine("DOTWEEN_TMP");
                } else ProEditor_RemoveGlobalDefine("DOTWEEN_TMP");
            }

            SetupComplete(addonsDir, proAddonsDir, totImported);
        }

        // Uses reflection to call DOTweenPro's AddGlobalDefine method (because older version of Unity didn't have it)
        public static void ProEditor_AddGlobalDefine(string id)
        {
            if (!EditorUtils.hasPro || ProEditorAssembly() == null) return;

            Type type = _proEditorAssembly.GetType("DG.DOTweenEditor.Core.ProEditorUtils");
            type.GetMethod("AddGlobalDefine", BindingFlags.Public | BindingFlags.Static).Invoke(null, new object[] { id });
        }
        // Uses reflection to call DOTweenPro's RemoveGlobalDefine method (because older version of Unity didn't have it)
        public static void ProEditor_RemoveGlobalDefine(string id)
        {
            if (!EditorUtils.hasPro || ProEditorAssembly() == null) return;

            Type type = _proEditorAssembly.GetType("DG.DOTweenEditor.Core.ProEditorUtils");
            type.GetMethod("RemoveGlobalDefine", BindingFlags.Public | BindingFlags.Static).Invoke(null, new object[] { id });
        }

        static void SetupComplete(string addonsDir, string proAddonsDir, int totImported)
        {
            int totRemoved = 0;

            // Delete all remaining addon files
            string[] leftoverAddonFiles = Directory.GetFiles(addonsDir, "*.addon");
            if (leftoverAddonFiles.Length > 0) {
                EditorUtility.DisplayProgressBar(_Title, "Removing " + leftoverAddonFiles.Length + " unused additional files...", 0.5f);
                foreach (string leftoverAddonFile in leftoverAddonFiles) {
                    totRemoved++;
                    File.Delete(leftoverAddonFile);
                }
            }
            if (EditorUtils.hasPro) {
                leftoverAddonFiles = Directory.GetFiles(proAddonsDir, "*.addon");
                if (leftoverAddonFiles.Length > 0) {
                    EditorUtility.DisplayProgressBar(_Title, "Removing " + leftoverAddonFiles.Length + " unused additional files...", 0.5f);
                    foreach (string leftoverAddonFile in leftoverAddonFiles) {
                        totRemoved++;
                        File.Delete(leftoverAddonFile);
                    }
                }
            }
            // Delete all remaining addon meta files
            leftoverAddonFiles = Directory.GetFiles(addonsDir, "*.addon.meta");
            if (leftoverAddonFiles.Length > 0) {
                EditorUtility.DisplayProgressBar(_Title, "Removing " + leftoverAddonFiles.Length + " unused additional meta files...", 0.75f);
                foreach (string leftoverAddonFile in leftoverAddonFiles) {
                    File.Delete(leftoverAddonFile);
                }
            }
            if (EditorUtils.hasPro) {
                leftoverAddonFiles = Directory.GetFiles(proAddonsDir, "*.addon.meta");
                if (leftoverAddonFiles.Length > 0) {
                    EditorUtility.DisplayProgressBar(_Title, "Removing " + leftoverAddonFiles.Length + " unused additional meta files...", 0.75f);
                    foreach (string leftoverAddonFile in leftoverAddonFiles) {
                        File.Delete(leftoverAddonFile);
                    }
                }
            }

            EditorUtility.DisplayProgressBar(_Title, "Refreshing...", 0.9f);
            AssetDatabase.Refresh();

            EditorUtility.ClearProgressBar();
            EditorUtility.DisplayDialog(_Title, "DOTween setup is now complete." +
                (totImported == 0 ? "" : "\n" + totImported + " additional libraries were imported or updated.") +
                (totRemoved == 0 ? "" : "\n" + totRemoved + " extra files were removed."),
                "Ok"
            );
        }

        // Removes relative .addon extension thus activating files
        static int ImportAddons(string version, string addonsDir)
        {
            bool imported = false;
            string[] filenames = new[] {
                "DOTween" + version + ".dll",
                "DOTween" + version + ".xml",
                "DOTween" + version + ".dll.mdb",
                "DOTween" + version + ".cs"
            };

            foreach (string filename in filenames) {
                string addonFilepath = addonsDir + filename + ".addon";
                string finalFilepath = addonsDir + filename;
                if (File.Exists(addonFilepath)) {
                    // Delete eventual existing final file
                    if (File.Exists(finalFilepath)) File.Delete(finalFilepath);
                    // Rename addon file to final
                    File.Move(addonFilepath, finalFilepath);
                    imported = true;
                }
            }

            return imported ? 1 : 0;
        }

        static bool Has2DToolkit()
        {
            string[] rootDirs = Directory.GetDirectories(EditorUtils.projectPath, "TK2DROOT", SearchOption.AllDirectories);
            if (rootDirs.Length == 0) return false;
            foreach (string rootDir in rootDirs) {
                if (Directory.GetFiles(rootDir, "tk2dSprite.cs", SearchOption.AllDirectories).Length > 0) return true;
            }
            return false;
        }
        static bool HasTextMeshPro()
        {
            string[] rootDirs = Directory.GetDirectories(EditorUtils.projectPath, "TextMesh Pro", SearchOption.AllDirectories);
            if (rootDirs.Length == 0) return false;
            foreach (string rootDir in rootDirs) {
                if (Directory.GetFiles(rootDir, "TextMeshPro.cs", SearchOption.AllDirectories).Length > 0) return true; // Old payed version
                if (Directory.GetFiles(rootDir, "TextMeshPro*.dll", SearchOption.AllDirectories).Length > 0) return true; // New free version
            }
            return false;
        }

        static Assembly ProEditorAssembly()
        {
            if (_proEditorAssembly == null) _proEditorAssembly = Assembly.LoadFile(EditorUtils.dotweenProDir + "Editor" + EditorUtils.pathSlash + "DOTweenProEditor.dll");
            return _proEditorAssembly;
        }
    }
}