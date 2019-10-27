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
    public  class ResourceBuilder
    {
        //当前对应打包配置
        private ResBuildConfig _packConfig;
        //当前打包设置
        private string _buildCfgXML;
        private string _outCfgFileName;
        private string _ifsBuildPath;
        private string _ifsZipFileName;
        private string _ifsFileListName;
        private string _luaScriptsZipName;

        public void Init(string ifsBuildPath, string buildCfgFilePath,string outCfgName,string ifsFileListName,string ifsFileZipName)
        {
            _ifsBuildPath = ifsBuildPath;
            _buildCfgXML = buildCfgFilePath;
            _outCfgFileName = outCfgName;
            _ifsZipFileName = ifsFileZipName;
            _ifsFileListName = ifsFileListName;
        }

        /// <summary>
        /// 初始化资源打包配置
        /// </summary>
        /// <param name="target">目标平台</param>
        public void InitResourcePacker(BuildTarget target)
        {
            JW.Common.Log.LogD("------------->初始化打包配置：" + GetPlatformStr(target)+"<-------------");
            AssetDatabase.Refresh();
            //打包配置解析
            _packConfig = new ResBuildConfig(_buildCfgXML,_outCfgFileName);
            bool isOk = _packConfig.Parse(target, true);
            if (isOk==false)
            {
                Terminate("打包配置初始化错误");
                return;
            }
            JW.Common.Log.LogD("------------->Done!<-------------");
        }

        /// 构建资源打包器
        public void BuildResourcePacker(BuildTarget target,bool clearManifest,bool writeResCfg=true)
        {
            //当前配置的打包
            if (_packConfig == null)
            {
                Terminate("You must call InitResourcePacker() before BuildResourcePacker()!");
                return;
            }
            JW.Common.Log.LogD("-->开始打包： " + GetPlatformStr(target));
            AssetDatabase.SaveAssets();
            //IFS输出目录
            string ifsBuildDir = JW.Res.FileUtil.CombinePaths(
                _ifsBuildPath,
                GetPlatformStr(target));

            if (!JW.Res.FileUtil.IsDirectoryExist(ifsBuildDir))
            {
                JW.Res.FileUtil.CreateDirectory(ifsBuildDir);
            }
            else
            {
                JW.Common.Log.LogD("-->清理目录： " + ifsBuildDir);
                JW.Res.FileUtil.ClearDirectory(ifsBuildDir);
            }
            //目标target
            ResPackHelper.BuildTarget = target;
            try
            {
                for (int i = 0; i < _packConfig.PackInfoAll.Count; i++)
                {
                    ResPackInfo info = _packConfig.PackInfoAll[i];
                    BuildResource(info, ifsBuildDir, target);
                }
            }
            catch (Exception e)
            {
                Terminate(e.Message);
            }
            //
            if (_packConfig.IsGlobalBuild)
            {
                JW.Common.Log.LogD("-->全局模式打包-->");
                AssetDatabase.Refresh();
                AssetDatabase.SaveAssets();
                BuildAssetBundleOptions options = _packConfig.GOptions|BuildAssetBundleOptions.DisableWriteTypeTree;
                BuildPipeline.BuildAssetBundles(ifsBuildDir, options, target);
            }
            //
            JW.Common.Log.LogD("-->检查Manifest-->");
            CheckAndClearManifestFiles(target,clearManifest);
            //
            if (writeResCfg)
            {
                JW.Common.Log.LogD("-->写入包配置信息文件-->");
                _packConfig.Write(ifsBuildDir);
            }
            JW.Common.Log.LogD("Build Resource Done.");
        }

        /// <summary>
        /// 写打包配置
        /// </summary>
        public void WriteResPackConfig(BuildTarget target,string excludePackPath=null)
        {
            JW.Common.Log.LogD("-->写入包配置信息文件-->");
            //当前配置的打包
            if (_packConfig == null)
            {
                Terminate("You must call InitResourcePacker() before BuildResourcePacker()!");
                return;
            }
            //IFS输出目录
            string ifsBuildDir = JW.Res.FileUtil.CombinePaths(
                _ifsBuildPath,
                GetPlatformStr(target));
            //
            _packConfig.Write(ifsBuildDir,excludePackPath);
        }

        /// <summary>
        /// 输出子资源包配置文件
        /// </summary>
        /// <param name="resourcesPrefixs"></param>
        /// <param name="cfgFileName"></param>
        /// <param name="excludeMode"></param>
        public void WriteSubResPackConfig(string dir, string[] resourcesPrefixs,string cfgFileName, bool excludeMode = false,string cleanLua="")
        {
            if (_packConfig == null)
            {
                JW.Common.Log.LogE("WriteSubResPackConfig Error");
                return;
            }
            _packConfig.WriteSubCfg(dir,resourcesPrefixs, cfgFileName, excludeMode,cleanLua);
        }

        /// 只打包二进制
        public void BuildBinaryPacker(BuildTarget target)
        {
            //当前配置的打包
            if (_packConfig == null)
            {
                Terminate("You must call InitResourcePacker() before BuildResourcePacker()!");
                return;
            }
            JW.Common.Log.LogD("-->开始打包二进制： " + GetPlatformStr(target));
            //IFS输出目录
            string ifsBuildDir = JW.Res.FileUtil.CombinePaths(
                _ifsBuildPath,
                GetPlatformStr(target));
            if (!JW.Res.FileUtil.IsDirectoryExist(ifsBuildDir))
            {
                JW.Res.FileUtil.CreateDirectory(ifsBuildDir);
            }
            //目标target
            ResPackHelper.BuildTarget = target;
            try
            {
                for (int i = 0; i < _packConfig.PackInfoAll.Count; i++)
                {
                    ResPackInfo info = _packConfig.PackInfoAll[i];
                    if(info.GetPackType()== (byte)ResPackType.ResPackTypeBinary)
                    {
                        BuildResource(info, ifsBuildDir, target);
                    }
                }
            }
            catch (Exception e)
            {
                Terminate(e.Message);
            }
        }


        //根据manifest检查一遍依赖关系
        private  void CheckAndClearManifestFiles(BuildTarget target,bool isClear=false)
        {
            string ifsBuildDir = JW.Res.FileUtil.CombinePaths(
                _ifsBuildPath,
                GetPlatformStr(target));
            //
            string mainAssetBundlePath = JW.Res.FileUtil.CombinePaths(ifsBuildDir, GetPlatformStr(target));
            AssetBundle mainAssetBundle = AssetBundle.LoadFromFile(mainAssetBundlePath);
            if (mainAssetBundle == null)
            {
                Terminate("Get RootAssetBundle Failed");
                return;
            }
                
            AssetBundleManifest mainAssetManifest = (AssetBundleManifest)mainAssetBundle.LoadAsset("AssetBundleManifest");
            if (mainAssetManifest == null)
            {
                Terminate("Get RootAssetBundle Manifest Failed!");
                return;
            }
            //
            string[] allAbs = mainAssetManifest.GetAllAssetBundles();
            foreach(string ab in allAbs)
            {
                JW.Common.Log.LogD("Exist AssetBundle:"+ab);
            }
            //
            for (int i = 0; i < _packConfig.PackInfoAll.Count; i++)
            {
                ResPackInfo info = _packConfig.PackInfoAll[i];
                if (info.GetPackType() == (byte)ResPackType.ResPackTypeBundle)
                {
                    BundlePackInfo binfo = info as BundlePackInfo;
                    //查找依賴
                    string[] des = mainAssetManifest.GetAllDependencies(info.Path);
                    if (des != null && des.Length > 0)
                    {
                        binfo.DependencyNames = string.Join(",",des);
                        JW.Common.Log.LogD("Add Dependency:" + binfo.DependencyNames + "-2->"+ info.Path);
                    }
                }
            }
            //
            mainAssetBundle.Unload(true);
            mainAssetManifest = null;
            //
            if (isClear)
            {
                string[] files = Directory.GetFiles(ifsBuildDir, "*.manifest", SearchOption.AllDirectories);
                for (int i = 0; i < files.Length; i++)
                {
                    JW.Res.FileUtil.DeleteFile(files[i].Replace('\\', '/'));
                }
                //删除 Win Android IOS 
                JW.Res.FileUtil.DeleteFile(mainAssetBundlePath);
            }
        }

        #region 打包相关
        //构建资源包
        void BuildResource(ResPackInfo ResInfo, string ifsBuildDir, BuildTarget target)
        {
            if (ResInfo.GetPackType() == (byte)ResPackType.ResPackTypeBundle)
            {
                BuildBundleResource(ResInfo as BundlePackInfo, ifsBuildDir, target);
            }
            else if (ResInfo.GetPackType() == (byte)ResPackType.ResPackTypeBinary)
            {
                BuildNormalResource(ResInfo as BinaryPackInfo, ifsBuildDir);
            }
        }    
       
        /// 打包AssetBundle资源，
        void BuildBundleResource(BundlePackInfo packInfo, string ifsBuildDir, BuildTarget target)
        {
            if (packInfo == null)
            {
                return;
            }
            // 本次打包的路径和目录
            string buildBundlePath = JW.Res.FileUtil.CombinePaths(ifsBuildDir, "", packInfo.Path);
            string buildBundleDirectory = JW.Res.FileUtil.GetFullDirectory(buildBundlePath);
            if (!JW.Res.FileUtil.IsDirectoryExist(buildBundleDirectory))
            {
                JW.Res.FileUtil.CreateDirectory(buildBundleDirectory);
            }
            //========== build ==========
            List<string> filePathList = new List<string>();
            List<string> nameList = new List<string>();
            for (int j = 0; j < packInfo.Resources.Count; j++)
            {
                ResInfo res = packInfo.Resources[j];
                filePathList.Add(res.RelativePath);
                nameList.Add(res.Path);
            }

            bool succ = false;
            //场景文件
            if (packInfo.HasFlag(EBundleFlag.UnityScene))
            {
                succ = ResPackHelper.BuildScene(filePathList, buildBundlePath);
            }
            else
            {
                //全局模式只是纯粹设置bundle 名称
                if (_packConfig.IsGlobalBuild)
                {
                    succ = ResPackHelper.JustSetBundleName(packInfo.Path, filePathList);
                }
                else
                {
                    EPackHandlerParam packHandlerParam = GeneratePackParam(packInfo);
                    succ = ResPackHelper.Build(buildBundlePath, filePathList, packHandlerParam, true, nameList);
                }
            }
            if (succ)
            {
                if (_packConfig.IsGlobalBuild)
                {
                    JW.Common.Log.LogD("Set BundleName Success " + buildBundlePath);
                }
                else
                {
                    JW.Common.Log.LogD("Build Bundle Success Out" + buildBundlePath);
                }
            }
            else
            {
                if (_packConfig.IsGlobalBuild)
                {
                    Terminate("Set bundleName failed, path:" + buildBundlePath);
                }
                else
                {
                    Terminate("Build bundle failed, path:" + buildBundlePath);
                }
            }
        }

        /// 普通资源，非bundle，直接拷贝 二进制文件组
        static void BuildNormalResource(BinaryPackInfo ResInfo, string ifsBuildDir)
        {
            if (ResInfo == null)
            {
                return;
            }

            for (int j = 0; j < ResInfo.Resources.Count; j++)
            {
                ResInfo res = ResInfo.Resources[j];

                string resourceFullPathInResources = res.Path + res.Ext;
                string resourceSrcFullPath = JW.Res.FileUtil.CombinePaths(Application.dataPath, "Resources", resourceFullPathInResources);
                string resourceDstFullDirectory = JW.Res.FileUtil.CombinePaths(
                    ifsBuildDir,
                    "",
                    JW.Res.FileUtil.GetFullDirectory(resourceFullPathInResources));

                if (!JW.Res.FileUtil.IsDirectoryExist(resourceDstFullDirectory))
                {
                    JW.Res.FileUtil.CreateDirectory(resourceDstFullDirectory);
                }

                string resourceDstFullPath = JW.Res.FileUtil.CombinePath(resourceDstFullDirectory, JW.Res.FileUtil.GetFullName(resourceFullPathInResources));
                if (JW.Res.FileUtil.IsFileExist(resourceSrcFullPath))
                {
                    JW.Res.FileUtil.CopyFile(resourceSrcFullPath, resourceDstFullPath);
                }
                else
                {
                    Terminate("CopyBinaryError:"+"File " + resourceFullPathInResources + " is not exist!");
                }
            }
        }

        #endregion

        //IFS工具处理 处理
        public void DoResIFSPackage(BuildTarget target,IFSCompressType ct=IFSCompressType.None,bool firstInclude=false,bool mergeLua=false,string luaZipName="")
        {
            //SVN 版本号
            string svnVersion = EditorSvnHelper.DoSvnInfoRivision(JW.Res.FileUtil.CombinePaths(Application.dataPath, "Resources"));
            //合并Lua脚本
            if (mergeLua && (!string.IsNullOrEmpty(luaZipName)))
            {
                IFSArchiver.ArchiveDir(JW.Res.FileUtil.CombinePaths(_ifsBuildPath, GetPlatformStr(target), "LuaScripts"),
                    JW.Res.FileUtil.CombinePaths(_ifsBuildPath, GetPlatformStr(target),luaZipName), IFSCompressType.None);
                //删除散lua 脚本
                JW.Res.FileUtil.DeleteDirectory(JW.Res.FileUtil.CombinePaths(_ifsBuildPath, GetPlatformStr(target), "LuaScripts"));
            }
            //生成文件列表
            IFSTool.GenerateFileList(JW.Res.FileUtil.CombinePath(_ifsBuildPath, GetPlatformStr(target)), _ifsFileListName, svnVersion);
            string dd = JW.Res.FileUtil.CombinePath(_ifsBuildPath, GetPlatformStr(target));
            string zipPath = JW.Res.FileUtil.CombinePath(_ifsBuildPath, _ifsZipFileName);
            IFSTool.GenerateFirstIFSFile(zipPath, dd,ct);
            if (firstInclude)
            {
                //copy
                IFSTool.MoveFirstIFSFileToStreamingAssets(zipPath);
            }
            //移动Zip到原本目录
            if (JW.Res.FileUtil.IsFileExist(zipPath))
            {
                string fileName = JW.Res.FileUtil.GetFullName(zipPath);
                string dir = JW.Res.FileUtil.CombinePath(_ifsBuildPath, GetPlatformStr(target));
                string dstPath = JW.Res.FileUtil.CombinePath(dir, _ifsZipFileName);
                JW.Res.FileUtil.MoveFile(zipPath, dstPath);
            }
        }

        /// 生成打包参数
        EPackHandlerParam GeneratePackParam(BundlePackInfo ResInfo)
        {
            EPackHandlerParam packHandlerParam = EPackHandlerParam.None;
            if (ResInfo.HasFlag(EBundleFlag.UnCompress))
            {
                packHandlerParam |= EPackHandlerParam.UnCompress;
            }
            if (ResInfo.HasFlag(EBundleFlag.LZMA))
            {
                packHandlerParam |= EPackHandlerParam.LZMA;
            }
            if (ResInfo.HasFlag(EBundleFlag.LZ4))
            {
                packHandlerParam |= EPackHandlerParam.LZ4;
            }
            return packHandlerParam;
        }

        /// 返回PlatForm描述
        public  string GetPlatformStr(BuildTarget target)
        {
            switch (target)
            {
                case BuildTarget.Android:
                    return "Android";

                case BuildTarget.iOS:
                    return "IOS";

                case BuildTarget.StandaloneWindows:
                    return "Win";
            }

            return "NotSupport";
        }

        /// 返回PlatForm描述
        public static string GetPlatformStrAll(BuildTarget target)
        {
            switch (target)
            {
                case BuildTarget.Android:
                    return "Android";

                case BuildTarget.iOS:
                    return "IOS";

                case BuildTarget.StandaloneWindows:
                    return "Win";
            }

            return "NotSupport";
        }

        /// 抛出异常
        private static void Terminate(string log)
        {
            throw (new System.Exception(log));
        }

        /// 删除多余的Resource 目录资源
        public  void DeleteRedundantFiles(BuildTarget target)
        {
            if (_packConfig == null)
            {
                _packConfig = new ResBuildConfig(_buildCfgXML,_outCfgFileName);
                bool isOK = _packConfig.Parse(target, true);
                if (isOK == false)
                {
                    _packConfig = null;
                    Terminate("解析打包build 错误");
                    return;
                }
            }
            //TODO 去掉打包的场景  目前无用
            // 删除
            Action<string> Delete = (string path) =>
            {
                if (string.IsNullOrEmpty(path))
                {
                    return;
                }
                // asset
                if (File.Exists(path))
                {
                    try
                    {
                        File.Delete(path);
                    }
                    catch (Exception e)
                    {
                        Terminate("Deleting assets failed, error:" + e.Message);
                    }
                }

                // meta
                if (File.Exists(path + ".meta"))
                {
                    try
                    {
                        File.Delete(path + ".meta");
                    }
                    catch (Exception e)
                    {
                        Terminate("Deleting assets failed, error:" + e.Message);
                    }
                }
            };

            //配置定义要删除的
            foreach (string asset in _packConfig.DeleteAssets)
            {
                Delete(asset);
            }

            //删除Resource 下面打包好的资源
            foreach (ResPackInfo pi in _packConfig.PackInfoAll)
            {
                if (pi is BundlePackInfo)
                {
                    BundlePackInfo bpi = pi as BundlePackInfo;
                    if (bpi != null && bpi.IsNoBundle())
                    {
                        continue;
                    }
                }
                //
                foreach (ResInfo res in pi.Resources)
                {
                    if (res.Keep)
                    {
                        continue;
                    }
                    Delete(res.RelativePath);
                }
            }
        }
    }
}
