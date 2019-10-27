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
    //口红机资源打包
    public static class LSKResBuilder
    {
         static readonly string IFS_BUILD_PATH = Application.dataPath + "/../../IFS/Build/LSK";

         static readonly string IFS_RELEASE_PATH = Application.dataPath + "/../../IFS/Release/LSK";

         static readonly string RES_BUILD_CFG_XML = Application.dataPath + "/Editor/Config/LSKResBuildConfig.xml";

         static readonly string IFS_FILE_LIST_CFG = "LSKFileList.json";

         public static readonly string  RES_RUN_CFG_NAME = "LSKResCfg.bytes";

         static readonly string  IFS_FILE_ZIP_NAME = "LSKZip.bytes";

         static readonly string LUA_ZIP_NAME = "LSK_Lua.bytes";

        [MenuItem("街机/资源/打包LSK-> Win资源")]
        public static void BuildWinRes()
        {
            BuildWindowsResource();
        }

        /// <summary>
        /// 从全量出档里面 分离出自己的
        /// </summary>
        /// <param name="target"></param>
        public static void SeparateBuildFromFullBuild(string fullBuildDir, BuildTarget target)
        {
            JW.Common.Log.LogD("-->从Full分离到LSK<------");
            fullBuildDir = JW.Res.FileUtil.CombinePath(fullBuildDir, ResourceBuilder.GetPlatformStrAll(target));
            string outDir = JW.Res.FileUtil.CombinePath(IFS_BUILD_PATH, ResourceBuilder.GetPlatformStrAll(target));
            //
            if (JW.Res.FileUtil.IsDirectoryExist(outDir))
            {
                JW.Res.FileUtil.ClearDirectory(outDir);
            }
            else
            {
                JW.Res.FileUtil.CreateDirectory(outDir);
            }

            bool packLua = false;
#if USE_LUA_PACK
            packLua=true;
#endif
            string[] files = Directory.GetFiles(fullBuildDir, "*.ab", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < files.Length; i++)
            {
                string src = files[i].Replace('\\', '/');
                string fullName = JW.Res.FileUtil.GetFullName(src);
                if (fullName.StartsWith("lsk_", StringComparison.OrdinalIgnoreCase))
                {
                    string dst = JW.Res.FileUtil.CombinePath(outDir, fullName);
                    JW.Res.FileUtil.CopyFile(src, dst, true);
                }
            }
            //打包配置
            string rescfgfile = JW.Res.FileUtil.CombinePath(fullBuildDir, RES_RUN_CFG_NAME);
            string dstrescfgfile = JW.Res.FileUtil.CombinePath(outDir, RES_RUN_CFG_NAME);
            JW.Res.FileUtil.CopyFile(rescfgfile, dstrescfgfile, false);

            //归档
            ResourceBuilder bb = new ResourceBuilder();
            bb.Init(IFS_BUILD_PATH, RES_BUILD_CFG_XML, RES_RUN_CFG_NAME, IFS_FILE_LIST_CFG, IFS_FILE_ZIP_NAME);
            //二进制
            bb.InitResourcePacker(target);
            bb.BuildBinaryPacker(target);
            if (packLua)
            {
                bb.DoResIFSPackage(target, IFSCompressType.None, true, true, LUA_ZIP_NAME);
            }
            else
            {
                bb.DoResIFSPackage(target, IFSCompressType.None, true);
            }
            // //归档好的 反到Full
            string selfDir = JW.Res.FileUtil.CombinePath(IFS_BUILD_PATH, ResourceBuilder.GetPlatformStrAll(target));
            string selfZipPath = JW.Res.FileUtil.CombinePath(selfDir, IFS_FILE_ZIP_NAME);
            string fullZipPath = JW.Res.FileUtil.CombinePath(fullBuildDir, IFS_FILE_ZIP_NAME);
            string selfFileListPath = JW.Res.FileUtil.CombinePath(selfDir, IFS_FILE_LIST_CFG);
            string fullFileListPath = JW.Res.FileUtil.CombinePath(fullBuildDir, IFS_FILE_LIST_CFG);
            JW.Res.FileUtil.CopyFile(selfZipPath, fullZipPath);
            JW.Res.FileUtil.CopyFile(selfFileListPath, fullFileListPath);
            //Lua ZIP
            if (packLua)
            {
                string selfLuaZipPath = JW.Res.FileUtil.CombinePath(selfDir, LUA_ZIP_NAME);
                string fullLuaZipPath = JW.Res.FileUtil.CombinePath(fullBuildDir, LUA_ZIP_NAME);
                if (JW.Res.FileUtil.IsFileExist(selfLuaZipPath))
                {
                    JW.Res.FileUtil.CopyFile(selfLuaZipPath, fullLuaZipPath);
                }
            }
        }

        /// <summary>
        /// 构建win 资源包
        /// </summary>
        static void BuildWindowsResource()
        {
            ResourceBuilder bb = new ResourceBuilder();
            bb.Init(IFS_BUILD_PATH, RES_BUILD_CFG_XML, RES_RUN_CFG_NAME, IFS_FILE_LIST_CFG, IFS_FILE_ZIP_NAME);
            bb.InitResourcePacker(BuildTarget.StandaloneWindows);
            bb.BuildResourcePacker(BuildTarget.StandaloneWindows, false);
            bool packLua = false;
#if USE_LUA_PACK
            packLua=true;
#endif
            if (packLua)
            {
                //跟随
                bb.DoResIFSPackage(BuildTarget.StandaloneWindows, IFSCompressType.None, true, true, LUA_ZIP_NAME);
                //写配置
                bb.WriteResPackConfig(BuildTarget.StandaloneWindows, "LSK_LuaScripts");
            }
            else
            {
                //跟随
                bb.DoResIFSPackage(BuildTarget.StandaloneWindows, IFSCompressType.None, true);
                bb.WriteResPackConfig(BuildTarget.StandaloneWindows, null);
            }
        }

        /// 删除多余的Resource 目录资源
        public static void DeleteRedundantFiles(BuildTarget target)
        {
            ResourceBuilder bb = new ResourceBuilder();
            bb.Init(IFS_BUILD_PATH, RES_BUILD_CFG_XML, RES_RUN_CFG_NAME, IFS_FILE_LIST_CFG, IFS_FILE_ZIP_NAME);
            bb.DeleteRedundantFiles(target);
        }
    }
}
