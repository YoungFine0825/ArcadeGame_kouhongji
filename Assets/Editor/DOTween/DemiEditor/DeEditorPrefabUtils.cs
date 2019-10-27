﻿// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2016/02/16 18:36
// License Copyright (c) Daniele Giardini

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DG.DemiEditor
{
    /// <summary>
    /// Prefab utilities
    /// </summary>
    public static class DeEditorPrefabUtils
    {
        /// <summary>
        /// Behaves as the Inspector's Apply button, applying any modification of this instance to the prefab parent
        /// </summary>
        /// <param name="instance"></param>
        public static void ApplyPrefabInstanceModifications(GameObject instance)
        {
            PrefabUtility.ReplacePrefab(instance, PrefabUtility.GetPrefabParent(instance), ReplacePrefabOptions.ConnectToPrefab);
        }

        /// <summary>
        /// Returns TRUe if a prefab instance has unapplied modifications, ignoring any modifications applied to the transform.<para/>
        /// NOTE: this a somehow costly operation (since it generates GC)
        /// </summary>
        public static bool InstanceHasUnappliedModifications(GameObject instance)
        {
            PropertyModification[] mods = PrefabUtility.GetPropertyModifications(instance);
            for (int i = 0; i < mods.Length; ++i) {
                string propertyPath = mods[i].propertyPath;
                int len = propertyPath.Length;
                if (len > 7 && propertyPath.Substring(0, 7) == "m_Local" || len == 11 && propertyPath == "m_RootOrder") continue;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Completely removes any prefab connection from the given prefab instances.
        /// <para>
        /// Based on RodGreen's method (http://forum.unity3d.com/threads/82883-Breaking-connection-from-gameObject-to-prefab-for-good.?p=726602&amp;viewfull=1#post726602)
        /// </para>
        /// </summary>
        public static void BreakPrefabInstances(List<GameObject> prefabInstances)
        { foreach (GameObject instance in prefabInstances) BreakPrefabInstance(instance); }
        /// <summary>
        /// Completely removes any prefab connection from the given prefab instance.
        /// <para>
        /// Based on RodGreen's method (http://forum.unity3d.com/threads/82883-Breaking-connection-from-gameObject-to-prefab-for-good.?p=726602&amp;viewfull=1#post726602)
        /// </para>
        /// </summary>
        public static void BreakPrefabInstance(GameObject prefabInstance)
        {
            string name = prefabInstance.name;
            Transform transform = prefabInstance.transform;
            Transform parent = transform.parent;
            int index = transform.GetSiblingIndex();
            // Unparent the GO so that world transforms are preserved.
            transform.SetParent(null);
            // Clone and re-assign.
            GameObject newInstance = (GameObject)Object.Instantiate(prefabInstance);
            newInstance.name = name;
            newInstance.SetActive(prefabInstance.activeSelf);
            newInstance.transform.SetParent(parent);
            newInstance.transform.SetSiblingIndex(index);
            // Remove old.
            Object.DestroyImmediate(prefabInstance, false);
        }
    }
}