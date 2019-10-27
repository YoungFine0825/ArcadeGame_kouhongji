/********************************************************************
	created:	18:5:2018   
	filename: 	FileUtil
	author:		jordenwu
	
	purpose:	文件工具
*********************************************************************/
using UnityEngine;
using System.IO;
using System.Security.Cryptography;
using System;
using System.Text;
using JW.Common;

namespace JW.Res
{
    //文件操作
    public enum EFileOperation
    {
        ReadFile,
        WriteFile,
        DeleteFile,
        CreateDirectory,
        DeleteDirectory,
    };

    /// <summary>
    /// 文件管理器
    /// </summary>
    public static class FileUtil
    {
        //文件操作失败事件 Todo Log 收集
        public delegate void OnOperateFileFailDelegate(string fullPath, EFileOperation fileOperation);
        public static OnOperateFileFailDelegate OnOperateFileFail = delegate { };

        //数据缓存路径
        private static string _cachePath = null;
        private static string _exeRootPath = null;

        //IFS解压路径 
        public static string IFSExtractFolder = "Resources";
        private static string _ifsExtractPath = null;
        private static string _ifsExtractPathWithHeader = null;

        //md5计算器
        private static readonly MD5CryptoServiceProvider _md5Provider = new MD5CryptoServiceProvider();

        #region 判断相关
        //文件是否存在
        public static bool IsFileExist(string filePath)
        {
            return System.IO.File.Exists(filePath);
        }

        //目录是否存在
        public static bool IsDirectoryExist(string directory)
        {
            return System.IO.Directory.Exists(directory);
        }

        //在IFS 外部目录中是否存在 主要用于资源检查
        public static bool IsExistInIFSExtraFolder(string path)
        {
            string fullPath = CombinePath(GetIFSExtractPath(), path);
            return IsFileExist(fullPath);
        }

        /// <summary>
        /// 文件是否存在于StreamingAssets目录下
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsFileExistInStreamingAssets(string fileName)
        {
            return FileUtil.IsFileExist(FileUtil.CombinePath(Application.streamingAssetsPath, fileName));
        }
        #endregion

        #region 操作相关
        //创建目录
        public static bool CreateDirectory(string directory)
        {
            if (IsDirectoryExist(directory))
            {
                return true;
            }

            int tryCount = 0;

            while (true)
            {
                try
                {
                    System.IO.Directory.CreateDirectory(directory);
                    return true;
                }
                catch (Exception ex)
                {
                    tryCount++;

                    if (tryCount >= 3)
                    {
                        JW.Common.Log.LogE("Create Directory " + directory + " Error! Exception = " + ex.ToString());
                        //派发事件
                        OnOperateFileFail(directory, EFileOperation.CreateDirectory);

                        return false;
                    }
                }
            }
        }

        //删除目录
        public static bool DeleteDirectory(string directory)
        {
            if (!IsDirectoryExist(directory))
            {
                return true;
            }

            int tryCount = 0;

            while (true)
            {
                try
                {
                    System.IO.Directory.Delete(directory, true);
                    return true;
                }
                catch (System.Exception ex)
                {
                    tryCount++;

                    if (tryCount >= 3)
                    {
                        JW.Common.Log.LogE("Delete Directory " + directory + " Error! Exception = " + ex.ToString());
                        //派发事件
                        OnOperateFileFail(directory, EFileOperation.DeleteDirectory);
                        return false;
                    }
                }
            }
        }

        //获取文件长度
        public static int GetFileLength(string filePath)
        {
            if (!IsFileExist(filePath))
            {
                return 0;
            }
            int tryCount = 0;
            while (true)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    return (int)fileInfo.Length;
                }
                catch (Exception ex)
                {
                    tryCount++;
                    if (tryCount >= 3)
                    {
                        JW.Common.Log.LogE("Get FileLength of " + filePath + " Error! Exception = " + ex.ToString());
                        return 0;
                    }
                }
            }
        }

        //读取文件
        public static byte[] ReadFile(string filePath)
        {
            if (!IsFileExist(filePath))
            {
                JW.Common.Log.LogE("Read File " + filePath + "Is Not Exist Step1");
                if (!IsFileExist(filePath.Trim()))
                {
                    JW.Common.Log.LogE("Read File " + filePath.Trim() + "Is Not Exist Step2");
                    return null;
                }
            }

            byte[] data = null;
            int tryCount = 0;

            while (true)
            {
                try
                {
                    data = System.IO.File.ReadAllBytes(filePath);
                }
                catch (System.Exception ex)
                {
                    JW.Common.Log.LogE("Read File " + filePath + " Error! Exception = " + ex.ToString() + ", TryCount = " + tryCount);
                    data = null;
                }

                if (data == null || data.Length <= 0)
                {
                    tryCount++;

                    if (tryCount >= 3)
                    {
                        JW.Common.Log.LogE("Read File " + filePath + " Fail!, TryCount = " + tryCount);
                        //派发事件
                        OnOperateFileFail(filePath, EFileOperation.ReadFile);
                        return null;
                    }
                }
                else
                {
                    return data;
                }
            }
        }

        /// StraamingAssert 目录读取文件字节
        public static byte[] ReadFileInStreamingAssets(string fileName)
        {
            if (!IsFileExistInStreamingAssets(fileName))
            {
                return null;
            }
            fileName = GetStreamingAssetsPath(fileName);
            byte[] data = File.ReadAllBytes(fileName);
            return data;
        }

        /// 写入文件
        public static bool WriteFile(string filePath, byte[] data, bool createDirectory = false)
        {
            int tryCount = 0;

            if (createDirectory)
            {
                string directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
            }

            while (true)
            {
                try
                {
                    System.IO.File.WriteAllBytes(filePath, data);
                    return true;
                }
                catch (System.Exception ex)
                {
                    tryCount++;

                    if (tryCount >= 3)
                    {
                        JW.Common.Log.LogE("Write File " + filePath + " Error! Exception = " + ex.ToString());
                        //删除文件以防止数据错误
                        DeleteFile(filePath);
                        //派发事件
                        OnOperateFileFail(filePath, EFileOperation.WriteFile);
                        return false;
                    }
                }
            }
        }

        /// 写入文件
        /// @filePath
        /// @data
        /// @offset
        /// @length
        public static bool WriteFile(string filePath, byte[] data, int offset, int length)
        {
            FileStream fileStream = null;
            int tryCount = 0;
            while (true)
            {
                try
                {
                    fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                    fileStream.Write(data, offset, length);
                    fileStream.Close();
                    return true;
                }
                catch (System.Exception ex)
                {
                    if (fileStream != null)
                    {
                        fileStream.Close();
                    }
                    tryCount++;
                    if (tryCount >= 3)
                    {
                        JW.Common.Log.LogE("Write File " + filePath + " Error! Exception = " + ex.ToString());
                        //删除文件以防止数据错误
                        DeleteFile(filePath);
                        //派发事件
                        OnOperateFileFail(filePath, EFileOperation.WriteFile);
                        return false;
                    }
                }
            }
        }

        /// 删除文件
        public static bool DeleteFile(string filePath)
        {
            if (!IsFileExist(filePath))
            {
                return true;
            }

            int tryCount = 0;

            while (true)
            {
                try
                {
                    System.IO.File.Delete(filePath);
                    return true;
                }
                catch (System.Exception ex)
                {
                    tryCount++;

                    if (tryCount >= 3)
                    {
                        JW.Common.Log.LogE("Delete File " + filePath + " Error! Exception = " + ex.ToString());

                        //派发事件
                        OnOperateFileFail(filePath, EFileOperation.DeleteFile);

                        return false;
                    }
                }
            }
        }

        /// 拷贝文件
        /// @srcFile
        /// @dstFile
        /// @createDirectory true：当目录不存在时创建
        public static void CopyFile(string srcFile, string dstFile, bool createDirectory = false)
        {
            if (createDirectory)
            {
                string destDirectory = Path.GetDirectoryName(dstFile);
                if (!Directory.Exists(destDirectory))
                {
                    Directory.CreateDirectory(destDirectory);
                }
            }

            System.IO.File.Copy(srcFile, dstFile, true);
        }

        /// 移动文件
        /// @srcFile
        /// @dstFile
        /// @createDirectory true：当目录不存在时创建
        public static void MoveFile(string srcFile, string dstFile, bool createDirectory = false)
        {
            if (createDirectory)
            {
                string destDirectory = Path.GetDirectoryName(dstFile);
                if (!Directory.Exists(destDirectory))
                {
                    Directory.CreateDirectory(destDirectory);
                }
            }
            System.IO.File.Move(srcFile, dstFile);
        }


        /// <summary>
        /// 拷贝目录
        /// </summary>
        /// <param name="sourceDirName"></param>
        /// <param name="destDirName"></param>
        /// <param name="copySubDirs"></param>
        public static void CopyDirectory(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    CopyDirectory(subdir.FullName, temppath, copySubDirs);
                }
            }
        }


        /// 返回文件md5
        public static string GetFileMd5(string filePath)
        {
            if (!IsFileExist(filePath))
            {
                return string.Empty;
            }
            return System.BitConverter.ToString(_md5Provider.ComputeHash(ReadFile(filePath))).Replace("-", "");
        }

        public static string GetFileMd5ByFileStream(string filePath)
        {
            if (!IsFileExist(filePath))
            {
                return string.Empty;
            }
            try
            {
                FileStream sf = new FileStream(filePath, FileMode.Open);
                string ret = System.BitConverter.ToString(_md5Provider.ComputeHash(sf)).Replace("-", "");
                sf.Close();
                return ret;
            }catch(Exception e)
            {
                JW.Common.Log.LogE("GetFileMd5 Exception"+e.ToString());
                return "";
            }
        }

        /// 返回字节流md5
        public static string GetMd5(byte[] data)
        {
            return System.BitConverter.ToString(_md5Provider.ComputeHash(data)).Replace("-", "");
        }

        /// 返回字符串md5
        public static string GetMd5(string str)
        {
            return System.BitConverter.ToString(_md5Provider.ComputeHash(Encoding.UTF8.GetBytes(str))).Replace("-", "");
        }

        //----------------------------------------------
        /// 清除目录下所有文件及文件夹，并保留目录
        /// @fullPath
        //----------------------------------------------
        public static bool ClearDirectory(string fullPath)
        {
            try
            {
                //删除文件
                string[] files = System.IO.Directory.GetFiles(fullPath);
                for (int i = 0; i < files.Length; i++)
                {
                    System.IO.File.Delete(files[i]);
                }

                //删除文件夹
                string[] dirs = System.IO.Directory.GetDirectories(fullPath);
                for (int i = 0; i < dirs.Length; i++)
                {
                    System.IO.Directory.Delete(dirs[i], true);
                }

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        //----------------------------------------------
        /// 清除目录下指定文件及文件夹，并保留目录
        /// @fullPath
        //----------------------------------------------
        public static bool ClearDirectory(string fullPath, string[] fileExtensionFilter, string[] folderFilter)
        {
            try
            {
                //删除文件
                if (fileExtensionFilter != null)
                {
                    string[] files = System.IO.Directory.GetFiles(fullPath);
                    for (int i = 0; i < files.Length; i++)
                    {
                        if (fileExtensionFilter != null && fileExtensionFilter.Length > 0)
                        {
                            for (int j = 0; j < fileExtensionFilter.Length; j++)
                            {
                                if (files[i].Contains(fileExtensionFilter[j]))
                                {
                                    DeleteFile(files[i]);
                                    break;
                                }
                            }
                        }
                    }
                }

                //删除文件夹
                if (folderFilter != null)
                {
                    string[] dirs = System.IO.Directory.GetDirectories(fullPath);
                    for (int i = 0; i < dirs.Length; i++)
                    {
                        if (folderFilter != null && folderFilter.Length > 0)
                        {
                            for (int j = 0; j < folderFilter.Length; j++)
                            {
                                if (dirs[i].Contains(folderFilter[j]))
                                {
                                    DeleteDirectory(dirs[i]);
                                    break;
                                }
                            }
                        }
                    }
                }

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        #endregion

        #region 路径相关
        /// 合并路径
        public static string CombinePath(string path1, string path2)
        {
            if (path1.LastIndexOf('/') != path1.Length - 1)
            {
                path1 += "/";
            }

            if (path2.IndexOf('/') == 0)
            {
                path2 = path2.Substring(1);
            }
            return path1 + path2;
        }

        /// 合并路径
        public static string CombinePaths(params string[] values)
        {
            if (values.Length <= 0)
            {
                return string.Empty;
            }
            else if (values.Length == 1)
            {
                return CombinePath(values[0], string.Empty);
            }
            else if (values.Length > 1)
            {
                string path = CombinePath(values[0], values[1]);
                for (int i = 2; i < values.Length; i++)
                {
                    path = CombinePath(path, values[i]);
                }
                return path;
            }
            return string.Empty;
        }

        //获取资源对应AssetStream目录下的全路径
        public static string GetStreamingAssetsPath(string fileName = "")
        {
            return CombinePath(Application.streamingAssetsPath, fileName);
        }


        /// 返回StreamingAssets路径
        /// @返回值为带上file:///的可用于www方式加载的路径
        public static string GetStreamingAssetsPathWithHeader(string fileName)
        {
            return GetLocalPathHeader() + CombinePath(Application.streamingAssetsPath, fileName);
        }

        /// 返回Cache文件存储路径 程序外部文件 可读写目录
        public static string GetCachePath()
        {
            if (_cachePath == null)
            {
#if UNITY_EDITOR
                _cachePath = Application.dataPath + "/../ArcadeCache/";
#endif
                //Windows方便查找
#if UNITY_STANDALONE && !UNITY_EDITOR
                _cachePath = Application.streamingAssetsPath + "/../../ArcadeCache/";
#endif
            }
            return _cachePath;
        }

        //EXE 所在目录
        public static string GetExeRootPath()
        {
            if (_exeRootPath == null)
            {
#if UNITY_EDITOR
                _exeRootPath = Application.dataPath + "/../";
#endif
             //Windows
#if UNITY_STANDALONE && !UNITY_EDITOR
                _exeRootPath = Application.streamingAssetsPath + "/../../";
#endif
            }
            return _exeRootPath;
        }


        /// 返回Cache文件存储路径
        /// @返回值为标准路径
        public static string GetCachePath(string fileName)
        {
            return CombinePath(GetCachePath(), fileName);
        }


        /// 返回Cache文件存储路径
        /// @返回值为带上file:///的可用于www方式加载的路径
        public static string GetCachePathWithHeader(string fileName)
        {
            return string.Format("{0}{1}", GetLocalPathHeader(), GetCachePath(fileName));
        }

        /// 返回IFS解压路径
        public static string GetIFSExtractPath()
        {
            if (_ifsExtractPath == null)
            {
                _ifsExtractPath = CombinePath(GetCachePath(), IFSExtractFolder);
            }
            return _ifsExtractPath;
        }

        /// <summary>
        /// 带文件头的IFS路径
        /// </summary>
        /// <returns></returns>
        public static string GetIFSExtractPathWithHeader()
        {
            if (_ifsExtractPathWithHeader == null)
            {
              _ifsExtractPathWithHeader = GetCachePathWithHeader(IFSExtractFolder);
            }
            return _ifsExtractPathWithHeader;
        }

        /// 返回带扩展名的文件全名
        /// @fullPath : 带扩展名的完整路径
        public static string GetFullName(string fullPath)
        {
            if (fullPath == null)
            {
                return null;
            }

            int index = fullPath.LastIndexOf("/");

            if (index > 0)
            {
                return fullPath.Substring(index + 1, fullPath.Length - index - 1);
            }
            else
            {
                return fullPath;
            }
        }

        /// <summary>
        /// 删除路径中第一个目录
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static string EraseFirstDirectory(string fullPath)
        {
            if (fullPath == null)
            {
                return null;
            }

            int index = fullPath.IndexOf("/");

            if (index > 0)
            {
                return fullPath.Substring(index + 1, fullPath.Length - index - 1);
            }
            else
            {
                return fullPath;
            }
        }

        /// 移除扩展名
        public static string EraseExtension(string fullName)
        {
            if (fullName == null)
            {
                return null;
            }

            int index = fullName.LastIndexOf('.');

            if (index > 0)
            {
                return fullName.Substring(0, index);
            }
            else
            {
                return fullName;
            }
        }

        /// 返回扩展名
        /// @返回值包括"."
        public static string GetExtension(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                return null;
            }

            int index = fullName.LastIndexOf('.');

            if (index > 0 && index + 1 < fullName.Length)
            {
                return fullName.Substring(index);
            }
            else
            {
                return string.Empty;
            }
        }

        /// 返回扩展名
        /// @返回值包括"."
        public static string GetExtensionWithoutPoint(string fullName)
        {
            int index = fullName.LastIndexOf('.');

            if (index > 0 && index + 1 < fullName.Length)
            {
                return fullName.Substring(index + 1);
            }
            else
            {
                return string.Empty;
            }
        }

        /// 返回完整目录
        /// @注意:"a/b/c"会返回"a/c"
        /// @"a/b/c/"才是我们想要的效果
        /// @fullPath
        public static string GetFullDirectory(string fullPath)
        {
            return System.IO.Path.GetDirectoryName(fullPath);
        }

        //--------------------------------------------------
        /// 获取本地路径前缀(file:///)
        //--------------------------------------------------
        private static string GetLocalPathHeader()
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            return "file:///";
#elif UNITY_ANDROID
        return "file://";
#elif UNITY_IPHONE
        return "file://";
#else
        return "file:///";
#endif
        }

        /// <summary>
        /// 获取相对路径
        /// </summary>
        /// <param name="fullPath">完整路径</param>
        /// <param name="parentFolder">相对路径的父目录</param>
        /// <returns></returns>
        public static string GetRelativePath(string fullPath, string parentDir)
        {
            if (string.IsNullOrEmpty(fullPath) || string.IsNullOrEmpty(parentDir))
            {
                return null;
            }

            fullPath = fullPath.Replace('\\', '/');

            parentDir = parentDir.Replace('\\', '/');

            if (fullPath.Length < parentDir.Length + 1)
            {
                return null;
            }

            int index = fullPath.IndexOf(parentDir);
            if (-1 == index)
            {
                return null;
            }
            return fullPath.Substring(index + parentDir.Length + 1);
        }
        #endregion
    };
}

