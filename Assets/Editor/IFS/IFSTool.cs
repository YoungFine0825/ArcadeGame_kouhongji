/********************************************************************
	created:	2018-06-07   
	filename: 	IFSTool
	author:		jordenwu
	
	purpose:	IFS 工具
*********************************************************************/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using JW.Common;
using JW.Res;
using JW.IFS;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using JW.Editor.Res;
using System.Diagnostics;
using FileUtil = JW.Res.FileUtil;

namespace JW.Editor.IFS
{
    public static class IFSTool
    {
        /// <summary>
        /// 生成当前目录的文件列表 json
        /// </summary>
        /// <param name="workDir">工作目录</param>
        public static void GenerateFileList(string ifsBuildDir, string outName,string buildSvnResNum="0")
        {
            //
            if (!JW.Res.FileUtil.IsDirectoryExist(ifsBuildDir))
            {
                JW.Common.Log.LogE("UpdateBuilder-->Not Exist WorkDir:" + ifsBuildDir);
            }
            UFileInfoList fileList = new UFileInfoList();
            fileList.ResVersion = buildSvnResNum;
            fileList.ListType = UFileInfoListType.MainGame;
            //
            string[] files = Directory.GetFiles(ifsBuildDir, "*", SearchOption.AllDirectories);

            for(int i = 0; i < files.Length; i++)
            {
                string path = files[i];
                string ext = JW.Res.FileUtil.GetExtension(path);
                if (ext.StartsWith(".svn")||ext.StartsWith(".manifest"))
                {
                    continue;
                }
                string relativePath = JW.Res.FileUtil.GetRelativePath(path, ifsBuildDir);
                UFileInfo uf = new UFileInfo();
                uf.FileName = relativePath;
                uf.MD5Num = JW.Res.FileUtil.GetFileMd5(path.Replace('\\', '/'));
                uf.SvnVerNum = buildSvnResNum;
                uf.FileSize = JW.Res.FileUtil.GetFileLength(path.Replace('\\', '/'));
                //
                fileList.AddUFile(ref uf);
            }
            //生成json 
            string jss = UnityEngine.JsonUtility.ToJson(fileList);
            UTF8Encoding en = new UTF8Encoding();
            JW.Res.FileUtil.WriteFile(JW.Res.FileUtil.CombinePath(ifsBuildDir, outName),en.GetBytes(jss));
            JW.Common.Log.LogD("------>GenerateFileList Done<-----");
        }

        /// <summary>
        /// 生成IFS 的首次Zip包
        /// </summary>
        /// <param name="ifsBuildDir"></param>
        /// <param name="firstName"></param>
        public static void GenerateFirstIFSFile(string firstNamePath,string ifsBuildDir,IFSCompressType tt=IFSCompressType.None)
        {
            if (!JW.Res.FileUtil.IsDirectoryExist(ifsBuildDir))
            {
                JW.Common.Log.LogE("Dir Not Exist:" + ifsBuildDir);
            }
            IFSArchiver.ArchiveDir(ifsBuildDir,firstNamePath,tt);
            
        }

        /// <summary>
        /// 移动首次zip到StreamingAsset
        /// </summary>
        /// <param name="firstNamePath"></param>
        public static void MoveFirstIFSFileToStreamingAssets(string firstNamePath)
        {
            if (!JW.Res.FileUtil.IsFileExist(firstNamePath))
            {
                JW.Common.Log.LogE("File Is Not Exist:" + firstNamePath);
            }
            string fileName = JW.Res.FileUtil.GetFullName(firstNamePath);
            string dst = JW.Res.FileUtil.GetStreamingAssetsPath(fileName);
            JW.Common.Log.LogD(dst);
            JW.Res.FileUtil.CopyFile(firstNamePath, dst);
        }
    }
}
