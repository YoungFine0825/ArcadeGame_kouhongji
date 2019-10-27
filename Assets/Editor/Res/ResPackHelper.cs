using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using JW.Res;
using System.Text;

namespace JW.Editor.Res
{
    [System.Flags]
    public enum EPackHandlerParam
    {
        None = 0,
        Preprocess = 1,
        UnCompress = 1 << 1,
        LZMA=1<<2,
        LZ4=1<<3,
        CompleteAssets=1<<4,
    }

    /// <summary>
    /// 新打包辅助
    /// </summary>
    public static class ResPackHelper
    {
        public static BuildTarget BuildTarget = BuildTarget.StandaloneWindows;

        /// 场景打包
        static public bool BuildScene(List<string> levels, string dstPath)
        {
            /*
            string error = BuildPipeline.BuildPlayer(levels.ToArray(), dstPath, BuildTarget, BuildOptions.BuildAdditionalStreamedScenes);
            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception(string.Format("Build level [{0}] failed, error:{1}", dstPath, error));
            }*/
            return true;
        }

        //只是设置资源AssetBundle 名称
        static public bool JustSetBundleName(string abName, List<string> filePaths)
        {
            for (int i = 0; i < filePaths.Count; i++)
            {
                AssetImporter im = AssetImporter.GetAtPath(filePaths[i]);
                if (im != null)
                {
                    im.assetBundleName = abName;
                    im.SaveAndReimport();
                }
                else
                {
                    Console.WriteLine("JustSetBundleName No Asset " + filePaths[i]);
                    return false;
                }
            }
            return true;
        }

        //第一步
        static public bool Build(string path, List<string> filePaths, EPackHandlerParam packHandlerParam, bool customPath = false,
            List<string> assetNames = null, List<UnityEngine.Object> assetList = null)
        {
            List<IResPacker> packers = new List<IResPacker>();
            for (int i = 0; i < filePaths.Count; i++)
            {
                IResPacker resPacker = CreateResPacker(filePaths[i], packHandlerParam);
                packers.Add(resPacker);
            }
            return Build(path, packers, packHandlerParam, customPath, assetNames, assetList);
        }

        //第2步
        static bool Build(string path, List<IResPacker> packers, EPackHandlerParam packHandlerParam, bool customPath = false,
         List<string> assetNames = null, List<UnityEngine.Object> assetList = null)
        {
            IResPacker packer = null;
            Console.WriteLine("ResPackHelper Build " + path);
            //目前无预处理
            if ((packHandlerParam & EPackHandlerParam.Preprocess) != 0)
            {
                for (int i = 0; i < packers.Count; i++)
                {
                    packer = packers[i];
                    packer.Preprocess();
                }
                Console.WriteLine("ResPackHelper Build Preprocess Finish " + path);
            }

            // 主预制件对象
            if (null == assetList)
            {
                assetList = new List<UnityEngine.Object>();
            }
            if (null == assetNames)
            {
                assetNames = new List<string>();
            }

            for (int i = 0; i < packers.Count; i++)
            {
                packer = packers[i];
                if (assetList.Count < packers.Count)
                {
                    assetList.Add(packer.GetAimAssetObj());
                }
                if (assetNames.Count < packers.Count)
                {
                    assetNames.Add(GetFileFullPathWithoutExtension(packer.GetPath().Substring(7)));
                }
            }

            if (assetList.Count == 0)
            {
                for (int i = 0; i < packers.Count; i++)
                {
                    packer = packers[i];
                    packer.Clear();
                }
                return false;
            }
            //
            BuildAssetBundleOptions buildOpts = BuildAssetBundleOptions.DeterministicAssetBundle;
            if (0 != (packHandlerParam & EPackHandlerParam.UnCompress))
            {
                buildOpts |= BuildAssetBundleOptions.UncompressedAssetBundle;
            }else if(0 != (packHandlerParam & EPackHandlerParam.LZ4))
            {
                buildOpts |= BuildAssetBundleOptions.ChunkBasedCompression;
            }else if (0 != (packHandlerParam & EPackHandlerParam.LZMA))
            {
                //默认就是
            }
            //优化
            buildOpts |= BuildAssetBundleOptions.DisableWriteTypeTree;
            // build other
            bool bSucess = BuildAssetBundle(assetList.ToArray(), assetNames.ToArray(), path, buildOpts, customPath);
            // clear tmp asset
            for (int i = 0; i < packers.Count; i++)
            {
                packer = packers[i];
                packer.Clear();
            }
            return bSucess;
        }

        //第3步
        private static bool BuildAssetBundle(UnityEngine.Object[] toInclude, string[] assetNames, string path, BuildAssetBundleOptions buildAssetBundleOptions, bool customPath = false)
        {
            Debug.Log("---------------Build Asset Bundle--------------->");
            // get path
            string resPath = customPath ? path : GetFileFullPathWithoutExtension(path.Substring(7));
            string spath = Application.streamingAssetsPath;
            string savePath = customPath ? path : string.Format("{0}/{1}.ab", spath, resPath.Replace('/', '-'));
            string saveDir = Path.GetDirectoryName(savePath);
            //
            string abName = JW.Res.FileUtil.GetFullName(savePath);
            abName = abName.ToLower();
            savePath = JW.Res.FileUtil.CombinePath(saveDir, abName);
            //
            AssetBundleBuild ab = new AssetBundleBuild();
            ab.assetBundleName = abName;
            List<string> abAssetNames = new List<string>();
            //导入设置 修改bundle 的名称 u5
            for (int i = 0; i < toInclude.Length; i++)
            {
                string pp = AssetDatabase.GetAssetPath(toInclude[i]);
                //AssetImporter im = AssetImporter.GetAtPath(pp);
               // if (im != null)
               // {
                //    im.assetBundleName = abName;
                //    im.SaveAndReimport();
               // }
                abAssetNames.Add(pp);
            }
            ab.assetNames = abAssetNames.ToArray();
            //
            if (!Directory.Exists(saveDir))
            {
                Directory.CreateDirectory(saveDir);
            }
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
                File.Delete(savePath + ".manifest");
            }
            //
            AssetBundleManifest mf = BuildPipeline.BuildAssetBundles(saveDir, new AssetBundleBuild[] { ab }, buildAssetBundleOptions, BuildTarget); ;
            if ((mf == null) || !File.Exists(savePath))
            {
                throw new Exception(savePath + " Build Failed!");
            }
            return true;
        }


        static IResPacker CreateResPacker(string filePath, EPackHandlerParam packHandlerParam)
        {
            filePath = filePath.Replace("\\", "/");
            IResPacker resPacker = null;
            resPacker = GetResPacker(filePath);
            resPacker.SetPath(filePath);
            resPacker.SetHandlerParam(packHandlerParam);
            return resPacker;
        }
        
        static private IResPacker GetResPacker(string path)
        {
            IResPacker ret = null;
            if (path.EndsWith(".prefab"))
            {
                ret = new PrefabResPacker();
            }
            else
            {
                ret = new ComResPacker();
            }
            return ret;
        }

        public static string GetRefAssetPath(UnityEngine.Object obj)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            if (path == "") return "*";
            return path.Remove(0, 7);
        }

        public static string GetFileFullPathWithoutExtension(string file)
        {
            return (file == null || file == "") ? "" : Path.GetDirectoryName(file) + "/" + Path.GetFileNameWithoutExtension(file);
        }


        /// <summary>
        /// 获取对应资源的依赖资源路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static public string[] GetDependencies(string path)
        {
            string[] array = AssetDatabase.GetDependencies(new string[] { path });
            List<String> list = new List<string>();
            string metaPath;
            for (int i = 0; i < array.Length; i++)
            {
                list.Add(array[i]);
                metaPath = array[i] + ".meta";
                if (File.Exists(metaPath))
                {
                    list.Add(metaPath);
                }
            }
            HashSet<string> hashSet = new HashSet<string>(list);
            hashSet.Remove(path.ToLower());
            if (hashSet.Count == 0)
                return null;

            string[] ret = new string[hashSet.Count];
            hashSet.CopyTo(ret);
            return ret;
        }
    }

}
