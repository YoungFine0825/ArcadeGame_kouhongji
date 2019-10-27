using UnityEngine;
using System.Collections.Generic;
using JW.Res;
using JW.Common;
using System.Xml;
using System.IO;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using UnityEditor;
using FileUtil = JW.Res.FileUtil;

namespace JW.Editor.Res
{
    public class ResBuildConfig
    {
        public bool IsGlobalBuild = false;
        public UnityEditor.BuildAssetBundleOptions GOptions = BuildAssetBundleOptions.None;

        string _outConfigName = "ResPackConfig";
        string _xmlPath = "";
        private ResPackConfig _config;

        // 所有包信息
        public List<ResPackInfo> PackInfoAll
        {
            get
            {
                return _config.PackInfo;
            }
        }

        List<string> _deleteAssets = new List<string>();
        public List<string> DeleteAssets
        {
            get
            {
                return _deleteAssets;
            }
        }

        public ResBuildConfig(string xmlPath, string outCfgName)
        {
            _xmlPath = xmlPath;
            _outConfigName = outCfgName;
            _config = new ResPackConfig();
        }
        public void Write(string buidlDir, string excludePackName = null)
        {
            List<ResPackInfo> allInfo = _config.PackInfo;
            List<ResPackInfo> baseInfo = new List<ResPackInfo>();
            if (allInfo != null)
            {
                for (int i = 0; i < allInfo.Count; ++i)
                {
                    ResPackInfo pack = allInfo[i];
                    pack.Path = JW.Res.FileUtil.CombinePaths(
                                "",
                                pack.Path);
                    //
                    if (pack.Path.Equals(excludePackName, StringComparison.OrdinalIgnoreCase))
                    {
                        JW.Common.Log.LogD("-->写入包配置信息文件 移除-->" + excludePackName);
                        continue;
                    }
                    else
                    {

                        baseInfo.Add(pack);
                    }
                }
            }
            byte[] buffer = new byte[1024 * 1024];
            int offset = 0;
            // count
            MemoryOperator.WriteShort((short)baseInfo.Count, buffer, ref offset);
            // resources
            for (int i = 0; i < baseInfo.Count; i++)
            {
                baseInfo[i].Write(buffer, ref offset);
            }
            JW.Res.FileUtil.WriteFile(JW.Res.FileUtil.CombinePaths(buidlDir, _outConfigName)
                , buffer, 0, offset);

        }

        /// 输出子配置文件 用于运行时读取
        public void WriteSubCfg(string buildDir, string[] resourcesPrefixs, string cfgFileName, bool excludeMode = false, string cleanLua = "")
        {
            List<ResPackInfo> allInfo = _config.PackInfo;

            List<ResPackInfo> baseInfo = new List<ResPackInfo>();
            if (allInfo != null)
            {
                for (int i = 0; i < allInfo.Count; ++i)
                {
                    ResPackInfo pack = allInfo[i];
                    pack.Path = JW.Res.FileUtil.CombinePaths(
                                  "",
                                  pack.Path);

                    bool isIn = false;
                    //非排他模式
                    if (excludeMode == false)
                    {
                        isIn = false;
                        for (int j = 0; j < resourcesPrefixs.Length; j++)
                        {
                            if (pack.Path.StartsWith(resourcesPrefixs[j]))
                            {
                                isIn = true;
                                break;
                            }
                        }

                    }
                    else
                    {
                        //排他模式
                        isIn = true;
                        for (int j = 0; j < resourcesPrefixs.Length; j++)
                        {
                            if (pack.Path.StartsWith(resourcesPrefixs[j]))
                            {
                                isIn = false;
                                break;
                            }
                        }
                    }
                    //Lua脚本包排除
                    if (!string.IsNullOrEmpty(cleanLua))
                    {
                        if (pack.Path.Equals(cleanLua, StringComparison.OrdinalIgnoreCase))
                        {
                            isIn = false;
                        }
                    }

                    if (isIn)
                    {
                        baseInfo.Add(pack);
                    }

                }
            }


            byte[] buffer = new byte[1024 * 1024];
            int offset = 0;
            // count
            MemoryOperator.WriteShort((short)baseInfo.Count, buffer, ref offset);
            // resources
            for (int i = 0; i < baseInfo.Count; i++)
            {
                baseInfo[i].Write(buffer, ref offset);
            }
            //保存
            JW.Res.FileUtil.WriteFile(JW.Res.FileUtil.CombinePaths(buildDir, cfgFileName)
                , buffer, 0, offset);

        }


        /// <summary>
        /// 解析打包配置文件
        /// </summary>
        public bool Parse(BuildTarget target, bool allowEmpty = false)
        {
            if (!JW.Res.FileUtil.IsFileExist(_xmlPath))
            {
                JW.Common.Log.LogE("资源打包配置xml文件不存在");
                return false;
            }
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(_xmlPath);
            // root node
            XmlNode resourcesNode = xmlDocument.SelectSingleNode("//Resources");
            if (null == resourcesNode)
            {
                JW.Common.Log.LogE("资源打包配置xml文件不存在RESOURCE节点");
                return false;
            }
            else
            {
                //解析全局
                if (resourcesNode.Attributes["IsGlobalBuild"] != null && resourcesNode.Attributes["IsGlobalBuild"].Value.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    IsGlobalBuild = true;
                    if (resourcesNode.Attributes["CompressType"] != null && resourcesNode.Attributes["CompressType"].Value.Equals("LZ4", StringComparison.OrdinalIgnoreCase))
                    {
                        GOptions = BuildAssetBundleOptions.ChunkBasedCompression;
                    }
                    else if (resourcesNode.Attributes["CompressType"] != null && resourcesNode.Attributes["CompressType"].Value.Equals("UnCompress", StringComparison.OrdinalIgnoreCase))
                    {
                        GOptions = BuildAssetBundleOptions.UncompressedAssetBundle;
                    }
                    else
                    {
                        //默认LZMA
                        GOptions = BuildAssetBundleOptions.None;
                    }
                }
            }

            // parse children
            for (int i = 0; i < resourcesNode.ChildNodes.Count; ++i)
            {
                if (resourcesNode.ChildNodes[i].Name == "AssetBundles")
                {
                    for (int j = 0; j < resourcesNode.ChildNodes[i].ChildNodes.Count; ++j)
                    {
                        if (resourcesNode.ChildNodes[i].ChildNodes[j].Name == "AssetBundle")
                        {
                            bool isOK = ParseBundle(resourcesNode.ChildNodes[i].ChildNodes[j], target, allowEmpty);
                            if (isOK == false)
                            {
                                return isOK;
                            }
                        }
                        else if (resourcesNode.ChildNodes[i].ChildNodes[j].Name == "BundleGroup")
                        {
                            bool isOK = ParseBundleGroup(resourcesNode.ChildNodes[i].ChildNodes[j], target, allowEmpty);
                            if (isOK == false)
                            {
                                return isOK;
                            }
                        }
                    }
                }
                else if (resourcesNode.ChildNodes[i].Name == "Binarys")
                {
                    for (int j = 0; j < resourcesNode.ChildNodes[i].ChildNodes.Count; ++j)
                    {
                        if (resourcesNode.ChildNodes[i].ChildNodes[j].Name == "BinaryGroup")
                        {
                            bool isOK = ParseBinaryGroup(resourcesNode.ChildNodes[i].ChildNodes[j], target, allowEmpty);
                            if (isOK == false)
                            {
                                return false;
                            }
                        }
                    }
                }
                else if (resourcesNode.ChildNodes[i].Name == "Delete")
                {
                    for (int j = 0; j < resourcesNode.ChildNodes[i].ChildNodes.Count; ++j)
                    {
                        if (resourcesNode.ChildNodes[i].ChildNodes[j].Name == "Assets")
                        {
                            bool isOK = ParseDelete(resourcesNode.ChildNodes[i].ChildNodes[j], target);
                            if (isOK == false)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        /// 解析配置的BundleGroup 文件夹下面一堆子文件件 来分成一些列bundle 
        bool ParseBundleGroup(XmlNode node, BuildTarget target, bool allowEmpty)
        {
            if (node.Attributes["SrcPath"] == null || string.IsNullOrEmpty(node.Attributes["SrcPath"].Value))
            {
                JW.Common.Log.LogE("BundleGroup SrcPath is invalid.");
                return false;
            }

            // srcPath
            string srcPath = node.Attributes["SrcPath"].Value;

            // pattern
            string pattern = ".*";
            if (node.Attributes["pattern"] != null)
            {
                pattern = node.Attributes["pattern"].Value;
            }

            // get sub folders/files 子文件夹
            string absoluteDir = JW.Res.FileUtil.CombinePath(JW.Res.FileUtil.CombinePath(Application.dataPath, "Resources"), srcPath);
            if (!Directory.Exists(absoluteDir))
            {
                JW.Common.Log.LogE("Bundle group SrcPath not exists, path:" + absoluteDir);
                return false;
            }
            //
            string[] directories = Directory.GetDirectories(absoluteDir).Where(d => Regex.IsMatch(d.Replace("\\", "/"), pattern)).ToArray();
            string[] files = Directory.GetFiles(absoluteDir).Where(f => !f.EndsWith(".meta", true, null)  // 过滤meta文件
                                                                    && ((File.GetAttributes(f) & FileAttributes.Hidden) != FileAttributes.Hidden) // // 过滤隐藏文件
                                                                    && Regex.IsMatch(f.Replace("\\", "/"), pattern)).ToArray();

            if ((directories == null || directories.Length == 0)
                && (files == null || files.Length == 0))
            {
                if (allowEmpty)
                {
                    return true;
                }
                JW.Common.Log.LogE("There's no files in " + absoluteDir);
                return false;
            }

            List<string> tempList = new List<string>();
            if (directories != null && directories.Length > 0)
            {
                tempList.AddRange(directories);
            }

            if (files != null && files.Length > 0)
            {
                tempList.AddRange(files);
            }

            string[] allPathes = tempList.ToArray();

            for (int i = 0; i < node.ChildNodes.Count; ++i)
            {
                XmlNode bundleNode = node.ChildNodes[i];
                for (int j = 0; j < allPathes.Length; j++)
                {
                    string path = allPathes[j].Replace("\\", "/");
                    string name = Path.GetFileName(path);

                    if (bundleNode.Name == "BundleGroup")
                    {
                        // nested bundle group

                        if (File.Exists(path))
                        {
                            // file
                            Debug.LogError("Nested BundleGroup is only used by directory.");
                            continue;
                        }

                        // derived source from up group
                        XmlAttribute attr = bundleNode.Attributes["SrcPath"];
                        if (attr == null)
                        {
                            attr = bundleNode.OwnerDocument.CreateAttribute("SrcPath");
                            bundleNode.Attributes.Append(attr);
                        }

                        bundleNode.Attributes["SrcPath"].Value = JW.Res.FileUtil.CombinePath(srcPath, name);

                        bool isOK = ParseBundleGroup(bundleNode, target, allowEmpty);
                        if (isOK == false)
                        {
                            return false;
                        }
                    }
                    else if (bundleNode.Name == "AssetBundle")
                    {
                        if (bundleNode.Attributes["DstPath"] == null)
                        {
                            continue;
                        }

                        // make assetbundle
                        XmlNode newBundleNode = node.OwnerDocument.CreateElement("AssetBundle", bundleNode.NamespaceURI);
                        if (newBundleNode != null)
                        {
                            XmlAttribute attr = null;

                            // name prefix 前缀
                            string prefix = string.Empty;
                            if (bundleNode.Attributes["prefix"] != null
                                && !string.IsNullOrEmpty(bundleNode.Attributes["prefix"].Value))
                            {
                                prefix = bundleNode.Attributes["prefix"].Value;
                                prefix += "_";
                            }

                            // name suffix 后缀
                            string suffix = string.Empty;
                            if (bundleNode.Attributes["suffix"] != null
                                && !string.IsNullOrEmpty(bundleNode.Attributes["suffix"].Value))
                            {
                                suffix = "_" + bundleNode.Attributes["suffix"].Value;
                            }

                            // DstPath
                            string dstPath = bundleNode.Attributes["DstPath"].Value;
                            attr = bundleNode.OwnerDocument.CreateAttribute("DstPath");
                            attr.Value = JW.Res.FileUtil.CombinePath(dstPath, prefix + Path.GetFileNameWithoutExtension(name) + suffix + ".ab");
                            newBundleNode.Attributes.Append(attr);

                            // flags
                            // int flag = 0;
                            if (bundleNode.Attributes["flags"] != null)
                            {
                                string flags = bundleNode.Attributes["flags"].Value;
                                if (!string.IsNullOrEmpty(flags))
                                {
                                    attr = bundleNode.OwnerDocument.CreateAttribute("flags");
                                    attr.Value = flags;
                                    newBundleNode.Attributes.Append(attr);
                                }
                            }

                            // tag
                            if (bundleNode.Attributes["tag"] != null)
                            {
                                string tag = bundleNode.Attributes["tag"].Value;
                                if (!string.IsNullOrEmpty(tag))
                                {
                                    attr = bundleNode.OwnerDocument.CreateAttribute("tag");
                                    attr.Value = tag;
                                    newBundleNode.Attributes.Append(attr);
                                }
                            }
                            // life
                            if (bundleNode.Attributes["life"] != null)
                            {
                                string life = bundleNode.Attributes["life"].Value;
                                if (!string.IsNullOrEmpty(life))
                                {
                                    attr = bundleNode.OwnerDocument.CreateAttribute("life");
                                    attr.Value = life;
                                    newBundleNode.Attributes.Append(attr);
                                }
                            }


                            // Asset nodes
                            if (bundleNode.ChildNodes.Count == 0)
                            {
                                JW.Common.Log.LogE("There is no Asset node in AssetBundle.");
                                continue;
                            }

                            for (int k = 0; k < bundleNode.ChildNodes.Count; k++)
                            {
                                XmlNode assetNode = bundleNode.ChildNodes[k];

                                // asset nodes
                                XmlNode newAssetNode = node.OwnerDocument.CreateElement("Asset", newBundleNode.NamespaceURI);
                                if (newAssetNode != null)
                                {
                                    newBundleNode.AppendChild(newAssetNode);

                                    // SrcPath
                                    attr = newAssetNode.OwnerDocument.CreateAttribute("SrcPath");
                                    if (File.Exists(path))
                                    {
                                        attr.Value = JW.Res.FileUtil.CombinePath(srcPath, name);
                                    }
                                    else
                                    {
                                        // directory
                                        // asset srcPath
                                        string assetSrcPath = "";
                                        if (assetNode.Attributes["SrcPath"] != null && !string.IsNullOrEmpty(assetNode.Attributes["SrcPath"].Value))
                                        {
                                            assetSrcPath = assetNode.Attributes["SrcPath"].Value;
                                        }

                                        attr.Value = JW.Res.FileUtil.CombinePaths(srcPath, name, assetSrcPath);
                                    }
                                    newAssetNode.Attributes.Append(attr);

                                    // flags
                                    if (assetNode.Attributes["flags"] != null)
                                    {
                                        attr = newAssetNode.OwnerDocument.CreateAttribute("flags");
                                        attr.Value = assetNode.Attributes["flags"].Value;
                                        newAssetNode.Attributes.Append(attr);
                                    }

                                    // pattern
                                    if (assetNode.Attributes["pattern"] != null)
                                    {
                                        attr = newAssetNode.OwnerDocument.CreateAttribute("pattern");
                                        attr.Value = assetNode.Attributes["pattern"].Value;
                                        newAssetNode.Attributes.Append(attr);
                                    }

                                    // recursive
                                    if (assetNode.Attributes["recursive"] != null)
                                    {
                                        attr = newAssetNode.OwnerDocument.CreateAttribute("recursive");
                                        attr.Value = assetNode.Attributes["recursive"].Value;
                                        newAssetNode.Attributes.Append(attr);
                                    }
                                }
                            }
                            //对生成的一个 Bundle 配置解析
                            bool isOk = ParseBundle(newBundleNode, target, allowEmpty);
                            if (isOk == false)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        /// 解析AssetBundle 配置节点
        bool ParseBundle(XmlNode node, BuildTarget target, bool allowEmpty = true)
        {
            BundlePackInfo info = new BundlePackInfo();
            // 输出文件名
            info.Path = node.Attributes["DstPath"].Value;
            //
            if (node.Attributes["flags"] != null)
            {
                info.Flags = ParseResourceFlags(node.Attributes["flags"].Value);
            }
            else
            {
                //没有也需要添加如果 全局模式
                if (IsGlobalBuild)
                {
                    info.Flags = ParseResourceFlags("");
                }
            }

            // bundle生命周期
            if (node.Attributes["life"] != null)
            {
                info.Life = ParseBundleLife(node.Attributes["life"].Value);
            }

            //包里面资产配置
            for (int i = 0; i < node.ChildNodes.Count; ++i)
            {
                if (node.ChildNodes[i].Name == "Asset")
                {
                    bool isOK = ParseAsset(node.ChildNodes[i], info);
                    if (isOK == false)
                    {
                        return false;
                    }
                }
            }

            // 没有资源
            if (info.Resources.Count == 0)
            {
                if (allowEmpty || info.HasFlag(EBundleFlag.Optional))
                {
                    JW.Common.Log.LogW("No file, but ignore." + info.Path);
                    return true;
                }
                else
                {
                    JW.Common.Log.LogW(string.Format("No asset for bundle {0}.", info.Path));
                    return false;
                }
            }

            // 是否有同名bundle
            string duplicatedBundle = null;
            if (IsDuplicated(info, out duplicatedBundle))
            {
                JW.Common.Log.LogE(string.Format("Bundle {0} name duplicated with {1}.", info.Path, duplicatedBundle));
                return false;
            }

            //加入配置
            _config.AddPackInfo(info);
            return true;
        }

        /// 解析Asset配置节点
        bool ParseAsset(XmlNode node, BundlePackInfo packInfo)
        {
            // source path
            if (node.Attributes["SrcPath"] == null
                || string.IsNullOrEmpty(node.Attributes["SrcPath"].Value))
            {
                JW.Common.Log.LogE("There is no SrcPath in Asset node.");
                return false;
            }

            string srcPath = node.Attributes["SrcPath"].Value;
            string fullPath = JW.Res.FileUtil.CombinePaths(Application.dataPath, "Resources", srcPath);

            string[] files = null;
            try
            {
                if (File.Exists(fullPath))
                {
                    //单个文件
                    if (node.Attributes["pattern"] != null)
                    {
                        Regex regex = new Regex(node.Attributes["pattern"].Value, RegexOptions.IgnoreCase);
                        if (regex.IsMatch(fullPath))
                        {
                            files = new string[1];
                            files[0] = fullPath;
                        }
                    }
                    else
                    {
                        files = new string[1];
                        files[0] = fullPath;
                    }
                }
                else if (Directory.Exists(JW.Res.FileUtil.GetFullDirectory(fullPath)))
                {
                    //目录
                    SearchOption option = SearchOption.TopDirectoryOnly;

                    if (node.Attributes["recursive"] != null && node.Attributes["recursive"].Value.ToLower() == "true")
                    {
                        option = SearchOption.AllDirectories;
                    }

                    files = Directory.GetFiles(JW.Res.FileUtil.GetFullDirectory(fullPath), JW.Res.FileUtil.GetFullName(fullPath), option).Where(name => !name.EndsWith(".meta", true, null)).ToArray();
                    if (node.Attributes["pattern"] != null)
                    {
                        Regex regex = new Regex(node.Attributes["pattern"].Value, RegexOptions.IgnoreCase);
                        files = files.Where(f => !string.IsNullOrEmpty(f) && regex.IsMatch(f)).ToArray();
                    }
                }
            }
            catch (System.Exception e)
            {
                JW.Common.Log.LogE(string.Format("ParseAsset(), path:{0}, exception:{1}", packInfo.Path, e.Message));
                return false;
            }

            // 创建资源信息  resource info
            for (int j = 0; files != null && j < files.Length; j++)
            {
                string filePathInResources = GetPathInResources(files[j]);
                string duplicatedFile = null;
                if (packInfo.IsDuplicated(filePathInResources, out duplicatedFile))
                {
                    JW.Common.Log.LogE(string.Format("Duplicated resource [{0}] and [{1}] in bundle [{2}]", filePathInResources, duplicatedFile, packInfo.Path));
                    return false;
                }
                ResInfo ResInfo = new ResInfo();
                if (packInfo.HasFlag(EBundleFlag.UnityScene))
                {
                    ResInfo.Path = Path.GetFileNameWithoutExtension(filePathInResources);
                }
                else
                {
                    ResInfo.Path = JW.Res.FileUtil.EraseExtension(filePathInResources);
                }

                int artPos = filePathInResources.IndexOf("ArtWork");
                if (artPos != -1)
                {
                    ResInfo.Path = ResInfo.Path.Substring(artPos + "ArtWork/".Length);
                    ResInfo.RelativePath = JW.Res.FileUtil.CombinePath("Assets", filePathInResources.Substring(artPos));
                }
                else
                {
                    ResInfo.RelativePath = JW.Res.FileUtil.CombinePath("Assets/Resources", filePathInResources);
                }
                //扩展名
                ResInfo.Ext = JW.Res.FileUtil.GetExtension(filePathInResources);
                //是否保持
                if (node.Attributes["keep"] != null && node.Attributes["keep"].Value.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    ResInfo.Keep = true;
                }
                else
                {
                    ResInfo.Keep = false;
                }
                packInfo.Add(ref ResInfo);
            }

            return true;
        }

        /// 解析二进制 BinaryGroup 组 
        bool ParseBinaryGroup(XmlNode node, BuildTarget target, bool allowEmpty)
        {
            //检查Platform属性
            if (node.Attributes["Platform"] != null)
            {
                string platform = node.Attributes["Platform"].Value;
                //platform不匹配，不打包
                if (!string.Equals(platform, GetPlatformStr(target), StringComparison.OrdinalIgnoreCase))
                {
                    JW.Common.Log.LogE("Platform not match, ignore.");
                    return false;
                }
            }
            BinaryPackInfo info = new BinaryPackInfo();
            info.Path = node.Attributes["DstPath"].Value;

            //包含的二进制
            for (int i = 0; i < node.ChildNodes.Count; ++i)
            {
                if (node.ChildNodes[i].Name == "Binary")
                {
                    XmlNode nd = node.ChildNodes[i];

                    string srcPathInResources = nd.Attributes["SrcPath"].Value;
                    string srcFullPath = JW.Res.FileUtil.CombinePath(JW.Res.FileUtil.CombinePath(Application.dataPath, "Resources"), srcPathInResources);

                    SearchOption option = SearchOption.TopDirectoryOnly;
                    if (nd.Attributes["recursive"] != null && nd.Attributes["recursive"].Value.ToLower() == "true")
                    {
                        option = SearchOption.AllDirectories;
                    }

                    string[] files = Directory.GetFiles(JW.Res.FileUtil.GetFullDirectory(srcFullPath), JW.Res.FileUtil.GetFullName(srcFullPath), option).Where(name => !name.EndsWith(".meta", true, null)).ToArray();
                    if (files == null || files.Length <= 0)
                    {
                        continue;
                    }

                    for (int j = 0; j < files.Length; j++)
                    {
                        string filePathInResources = GetPathInResources(files[j]);

                        ResInfo ResInfo = new ResInfo();
                        ResInfo.Path = JW.Res.FileUtil.EraseExtension(filePathInResources);
                        ResInfo.Ext = JW.Res.FileUtil.GetExtension(filePathInResources);

                        if (nd.Attributes["clear"] != null && nd.Attributes["clear"].Value.Equals("true", StringComparison.OrdinalIgnoreCase))
                        {
                            ResInfo.Keep = false;
                            ResInfo.RelativePath = JW.Res.FileUtil.CombinePath("Assets/Resources", filePathInResources);
                        }
                        else
                        {
                            ResInfo.Keep = true;
                            ResInfo.RelativePath = JW.Res.FileUtil.CombinePath("Assets/Resources", filePathInResources);
                        }

                        info.Add(ref ResInfo);
                    }
                }
            }

            if (info.Resources.Count == 0)
            {
                JW.Common.Log.LogE("No binary file for " + info.Path);
                return false;
            }

            _config.AddPackInfo(info);
            return true;
        }

        /// 配置删除的资源
        bool ParseDelete(XmlNode node, BuildTarget target)
        {
            // target
            if (node.Attributes["Platform"] != null)
            {
                string platform = node.Attributes["Platform"].Value;
                if (!string.Equals(platform, GetPlatformStr(target), StringComparison.OrdinalIgnoreCase))
                {
                    JW.Common.Log.LogE("Platform not match, ignore.");
                    return false;
                }
            }

            // src path
            string srcPath = node.Attributes["SrcPath"].Value;
            if (string.IsNullOrEmpty(srcPath))
            {
                return true;
            }

            string fullPath = JW.Res.FileUtil.CombinePaths(Application.dataPath, "Resources", srcPath);
            string[] files = null;
            if (File.Exists(fullPath))
            {
                // is file
                files = new string[1];
                files[0] = fullPath;
            }
            else if (Directory.Exists(JW.Res.FileUtil.GetFullDirectory(fullPath)))
            {
                // is directory
                files = Directory.GetFiles(JW.Res.FileUtil.GetFullDirectory(fullPath), JW.Res.FileUtil.GetFullName(fullPath), SearchOption.AllDirectories).Where(name => !name.EndsWith(".meta", true, null)).ToArray();
            }

            foreach (string file in files)
            {
                string path = GetPathInProject(file);
                if (!string.IsNullOrEmpty(path))
                {
                    _deleteAssets.Add(path);
                }
            }

            return true;
        }

        /// <summary>
        /// 获取相对于Resources的路径
        /// </summary>
        /// <param name="fileFullPath"></param>
        /// <returns></returns>
        string GetPathInResources(string fileFullPath)
        {
            fileFullPath = Path.GetFullPath(fileFullPath);
            fileFullPath = fileFullPath.Replace(@"\", @"/");
            string key = "Assets/Resources/";
            int index = fileFullPath.IndexOf(key, StringComparison.OrdinalIgnoreCase);
            if (index >= 0)
            {
                return fileFullPath.Substring(index + key.Length);
            }
            return string.Empty;
        }

        /// <summary>
        /// 相对工程的路径
        /// </summary>
        /// <param name="fileFullPath"></param>
        /// <returns></returns>
        string GetPathInProject(string fileFullPath)
        {
            fileFullPath = Path.GetFullPath(fileFullPath);

            fileFullPath = fileFullPath.Replace(@"\", @"/");
            string key = "Assets/Resources/";
            int index = fileFullPath.IndexOf(key, StringComparison.OrdinalIgnoreCase);
            if (index >= 0)
            {
                return fileFullPath.Substring(index);
            }

            return string.Empty;
        }

        /// <summary>
        /// 解析资源标识位
        /// </summary>
        /// <param name="flagStr"></param>
        /// <returns></returns>
        int ParseResourceFlags(string flagStr)
        {
            System.Type enumType = typeof(EBundleFlag);
            int flags = 0;

            string[] split = flagStr.Split(new char[] { ',', ';', ' ', '|' });
            for (int i = 0; i < split.Length; ++i)
            {
                if (split[i] == null)
                {
                    continue;
                }
                if (System.String.IsNullOrEmpty(split[i].Trim()))
                {
                    continue;
                }
                //过滤掉压缩方式
                if (IsGlobalBuild)
                {
                    string ff = split[i].Trim();
                    if (ff.Equals("UnCompress") || ff.Equals("LZ4") || ff.Equals("LZMA"))
                    {
                        JW.Common.Log.LogD("全局模式，去掉自定义Flag->" + ff);
                        continue;
                    }
                }
                try
                {
                    object f = System.Enum.Parse(enumType, split[i].Trim(), true);
                    flags |= ((int)f);
                }
                catch (System.Exception e)
                {
                    UnityEngine.Debug.LogWarning(e.Message + ":" + split[i] + " is not a valid flag!");
                }
            }

            //附加全局的压缩方式
            if (IsGlobalBuild)
            {
                if (GOptions == BuildAssetBundleOptions.ChunkBasedCompression)
                {
                    JW.Common.Log.LogD("全局模式，添加Flag->" + "LZ4");
                    object f = System.Enum.Parse(enumType, "LZ4", true);
                    flags |= ((int)f);
                }
                else if (GOptions == BuildAssetBundleOptions.UncompressedAssetBundle)
                {
                    JW.Common.Log.LogD("全局模式，添加Flag->" + "UnCompress");
                    object f = System.Enum.Parse(enumType, "UnCompress", true);
                    flags |= ((int)f);

                }
                else if (GOptions == BuildAssetBundleOptions.None)
                {
                    JW.Common.Log.LogD("全局模式，添加Flag->" + "LZMA");
                    object f = System.Enum.Parse(enumType, "LZMA", true);
                    flags |= ((int)f);
                }
            }

            return flags;
        }

        /// 解析bundle生命周期属性
        EBundleLife ParseBundleLife(string lifeStr)
        {
            if (lifeStr.Equals(EBundleLife.Resident.ToString()))
            {
                return EBundleLife.Resident;
            }
            else if (lifeStr.Equals(EBundleLife.Immediate.ToString()))
            {
                return EBundleLife.Immediate;
            }

            return EBundleLife.Cache;
        }

        //
        public ResPackInfo GetPackInfoForResource(string resource)
        {
            return _config.GetPackInfoForResource(resource);
        }

        /// 获取平台字符串
        public static string GetPlatformStr(BuildTarget target)
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

        /// 检查是否有同名bundle存在
        bool IsDuplicated(BundlePackInfo pi, out string duplicatedBundle)
        {
            duplicatedBundle = null;

            string name = JW.Res.FileUtil.EraseExtension(JW.Res.FileUtil.GetFullName(pi.Path));
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            for (int i = 0; i < _config.PackInfo.Count; i++)
            {
                BundlePackInfo p = _config.PackInfo[i] as BundlePackInfo;
                if (p != null)
                {
                    string n = JW.Res.FileUtil.EraseExtension(JW.Res.FileUtil.GetFullName(p.Path));
                    if (!string.IsNullOrEmpty(n))
                    {
                        if (n.Equals(name, StringComparison.OrdinalIgnoreCase))
                        {
                            duplicatedBundle = p.Path;
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}

