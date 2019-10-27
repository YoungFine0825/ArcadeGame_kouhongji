/********************************************************************
	created:	2018-06-07   
	filename: 	CommandBuild
	author:		jordenwu
	
	purpose:	对接项目自动构建
*********************************************************************/
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using JW.Editor.Res;
using FileUtil = JW.Res.FileUtil;
using ICSharpCode.SharpZipLib;

public static class CommandBuild
{
    /// 获取出档的场景列表
    static EditorBuildSettingsScene[] GetScenePaths(bool isFilter)
    {
        var names = new List<EditorBuildSettingsScene>();
        foreach (EditorBuildSettingsScene e in EditorBuildSettings.scenes)
        {
            if (e == null)
                continue;

            if (!e.enabled)
                continue;
            names.Add(e);
        }
        return names.ToArray();
    }

    //构建Win64平台EXE 程序
    public static void BuildWin64Exe()
    {
        GenerateLuaWrap();
        AssetDatabase.Refresh();

#if USE_PACK_RES
        FullResBuilder.DeleteRedundantFiles(BuildTarget.StandaloneWindows);
#endif
        string version = System.Text.Encoding.UTF8.GetString(FileUtil.ReadFile(Application.dataPath + "/Resources/Version.bytes"));
        if (string.IsNullOrEmpty(version))
        {
            version = "000";
        }
        //
        version = version.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
        string exeFolderName = string.Empty;
        //
#if JW_DEBUG
        exeFolderName = "Debug_Arcade_" + version;
#else
        exeFolderName = "Release_Arcade_" + version;
#endif
        string outDir = Application.dataPath + "/../../Builds/BuildWin/" + exeFolderName;
        string outPath = outDir + "/ToyRealMachine.exe";
        //
        PlayerSettings.companyName = "BCZX";
        PlayerSettings.productName = "RealMachine";

        PlayerSettings.defaultIsFullScreen = true;
        PlayerSettings.displayResolutionDialog = ResolutionDialogSetting.Disabled;
        PlayerSettings.resizableWindow = false;
        PlayerSettings.forceSingleInstance = true;
        PlayerSettings.allowFullscreenSwitch = false;
        PlayerSettings.applicationIdentifier = "com.bczx.arcade";
        PlayerSettings.d3d11FullscreenMode = D3D11FullscreenMode.FullscreenWindow;
        PlayerSettings.visibleInBackground = false;
        PlayerSettings.defaultIsNativeResolution = true;
        PlayerSettings.runInBackground = false;
        PlayerSettings.SplashScreen.show = false;
        PlayerSettings.forceSingleInstance = true;

        string path = System.IO.Path.GetFullPath(outPath);
#if JW_DEBUG
        BuildOptions option = BuildOptions.AllowDebugging | BuildOptions.Development;
#else
        BuildOptions option = BuildOptions.None;
#endif
        string error = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, path, BuildTarget.StandaloneWindows64, option);
        if (error != null)
        {
              Debug.Log("BuildWinExe Error:" + error.ToString());
#if !UNITY_EDITOR
              EditorApplication.Exit(2);
#endif
        }
        //
        if (FileUtil.IsFileExist(outPath))
        {
            Debug.Log("BuildWinExe Zip:");
            //合并成Zip
            string outZipPath = Application.dataPath + "/../../Builds/BuildWin/" + exeFolderName + ".zip";
            //
            ZipHelper.CreateZip(outZipPath, outDir);
            //
            FileUtil.CopyFile(Application.dataPath + "/Resources/Version.bytes", Application.dataPath + "/../../Builds/BuildWin/Version.bytes");

        }

#if !UNITY_EDITOR
        EditorApplication.Exit(0);
#endif

    }

    public static void BuildWinResource()
    {

#if USE_LUA_PACK
        LuacBuilder.LuacLuaScripts();
#endif
        FullResBuilder.BuildWinRes();
    }

    public static void RefreshAssetDatabase()
    {
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 生成Lua导出文件
    /// </summary>
    static void GenerateLuaWrap()
    {
       CSObjectWrapEditor.Generator.ClearAll();
       CSObjectWrapEditor.Generator.GenAll();
    }

}

