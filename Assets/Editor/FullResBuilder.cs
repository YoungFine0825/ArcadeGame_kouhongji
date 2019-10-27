using System;
using System.Collections.Generic;
using System.IO;
using JW.Res;
using UnityEditor;
using UnityEngine;
using JW.Common;
using JW.Editor.IFS;
using JW.IFS;

namespace JW.Editor.Res
{
    //游戏资源全量打包
    public static class FullResBuilder
    {
        static readonly string IFS_BUILD_PATH = Application.dataPath + "/../../IFS/Build/Full";

        static readonly string IFS_RELEASE_PATH = Application.dataPath + "/../../IFS/Release/Full";

        static readonly string RES_BUILD_CFG_XML = Application.dataPath + "/Editor/Config/FullResBuildConfig.xml";

        static readonly string IFS_FILE_LIST_CFG = "FullFileList.json";

        static readonly string RES_RUN_CFG_NAME = "FullResCfg.bytes";

        static readonly string IFS_FILE_ZIP_NAME = "FullZip.bytes";


        [MenuItem("街机/资源/打包Full-->Win资源")]
        public static void BuildWinRes()
        {
            BuildWindowsResource();
        }

        /// <summary>
        /// 构建win 资源包
        /// </summary>
        static void BuildWindowsResource()
        {
            ResourceBuilder bb = new ResourceBuilder();
            bb.Init(IFS_BUILD_PATH, RES_BUILD_CFG_XML, RES_RUN_CFG_NAME, IFS_FILE_LIST_CFG, IFS_FILE_ZIP_NAME);
            bb.InitResourcePacker(BuildTarget.StandaloneWindows);
            bb.BuildResourcePacker(BuildTarget.StandaloneWindows, true, false);
            string fullBuildDir = JW.Res.FileUtil.CombinePath(IFS_BUILD_PATH, ResourceBuilder.GetPlatformStrAll(BuildTarget.StandaloneWindows));
            //分解资源打包配置
            bool packLua = false;
#if USE_LUA_PACK
            packLua=true;
#endif
            bb.WriteSubResPackConfig(fullBuildDir, new string[] { "LSK_", "WWJ_" }, MainResBuilder.RES_RUN_CFG_NAME, true, packLua?"LuaScripts":"");
            bb.WriteSubResPackConfig(fullBuildDir, new string[] { "LSK_" }, LSKResBuilder.RES_RUN_CFG_NAME, false, packLua ? "LSK_LuaScripts" : "");
            bb.WriteSubResPackConfig(fullBuildDir, new string[] { "WWJ_" }, WWJResBuilder.RES_RUN_CFG_NAME, false, packLua ? "WWJ_LuaScripts" : "");

            MainResBuilder.SeparateBuildFromFullBuild(IFS_BUILD_PATH, BuildTarget.StandaloneWindows);
            LSKResBuilder.SeparateBuildFromFullBuild(IFS_BUILD_PATH, BuildTarget.StandaloneWindows);
            WWJResBuilder.SeparateBuildFromFullBuild(IFS_BUILD_PATH, BuildTarget.StandaloneWindows);
            //删除散lua 脚本
            if (packLua)
            {
                JW.Res.FileUtil.DeleteDirectory(JW.Res.FileUtil.CombinePaths(fullBuildDir, "LuaScripts"));
            }
        }

        /// 删除多余的Resource 目录资源
        public static void DeleteRedundantFiles(BuildTarget target)
        {
            ResourceBuilder bb = new ResourceBuilder();
            bb.Init(IFS_BUILD_PATH, RES_BUILD_CFG_XML, RES_RUN_CFG_NAME, IFS_FILE_LIST_CFG, IFS_FILE_ZIP_NAME);
            bb.DeleteRedundantFiles(target);

            //MainResBuilder.DeleteRedundantFiles(target);
            //LSKResBuilder.DeleteRedundantFiles(target);
            //WWJResBuilder.DeleteRedundantFiles(target);

        }
    }
}
