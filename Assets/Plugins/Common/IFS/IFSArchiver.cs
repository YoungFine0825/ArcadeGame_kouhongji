/********************************************************************
	created:	24:5:2018   
	filename: 	IFSArchiver
	author:		jordenwu
	
	purpose:	菜单根 禁止分散项目扩展菜单
*********************************************************************/
using JW.Res;
using System.Collections.Generic;
using JW.Common;
using System.IO;
using System;
using System.Diagnostics;

namespace JW.IFS
{
    public sealed class IFSArchiver
    {
        /// <summary>
        /// 归档一个目录成一个ifs
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        public static bool ArchiveDir(string indir, string outPath, IFSCompressType compress = IFSCompressType.None)
        {
            if (!FileUtil.IsDirectoryExist(indir))
            {
                JW.Common.Log.LogE("IFSArchiver ArchiveDir-->Not Exist WorkDir:" + indir);
                return false;
            }
            //不压缩直接归档
            if (compress == IFSCompressType.None)
            {
                //获取所有文件 过滤svn
                string[] files = Directory.GetFiles(indir, "*", SearchOption.AllDirectories);
                if (files == null || files.Length == 0)
                {
                    JW.Common.Log.LogE("IFSArchiver ArchiveDir-->No File In WorkDir:" + indir);
                    return false;
                }
                List<string> needfiles = new List<string>();
                for (int i = 0; i < files.Length; i++)
                {
                    string path = files[i];
                    string ext = FileUtil.GetExtension(path);
                    if (ext.StartsWith(".svn")||ext.StartsWith(".meta"))
                    {
                        continue;
                    }
                    needfiles.Add(path);
                }
                return DoArchiveDir(needfiles.ToArray(), indir, outPath, IFSCompressType.None);
            }

            //需要压缩处理
            if (compress == IFSCompressType.LZMA)
            {
                //获取所有文件 过滤
                string[] files = Directory.GetFiles(indir, "*", SearchOption.AllDirectories);
                if (files == null || files.Length == 0)
                {
                    JW.Common.Log.LogE("IFSArchiver ArchiveDir-->No File In WorkDir:" + indir);
                    return false;
                }
                string tempDir = FileUtil.GetFullDirectory(outPath);
                tempDir = FileUtil.CombinePaths(tempDir, "IFSLZMATemp");
                if (FileUtil.IsDirectoryExist(tempDir))
                {
                    FileUtil.ClearDirectory(tempDir);
                }
                else
                {
                    FileUtil.CreateDirectory(tempDir);
                }

                //压缩所有归档文件到临时目录
                try
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        string path = files[i];
                        string ext = FileUtil.GetExtension(path);
                        if (ext.StartsWith(".svn"))
                        {
                            continue;
                        }
                        //压缩
                        string relativePath = FileUtil.GetRelativePath(files[i], indir);
                        string tempPath = FileUtil.CombinePaths(tempDir, relativePath);
                        //原本是相对路径 创建对应目录
                        string directory = Path.GetDirectoryName(tempPath);
                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }
                        JW.Common.Log.LogD("---LZMA--->" + relativePath);
                        CompressFileLZMAToFile(files[i], tempPath);
                    }
                }
                catch (Exception exp)
                {
                    JW.Common.Log.LogE("Archive Exception With LZMA:" + exp.ToString());
                    return false;
                }
                //合并
                string[] realfiles = Directory.GetFiles(tempDir, "*", SearchOption.AllDirectories);
                bool isOk = DoArchiveDir(realfiles, tempDir, outPath, IFSCompressType.LZMA);
                //删除临时目录
                FileUtil.DeleteDirectory(tempDir);
                JW.Common.Log.LogD("Done!");
                return isOk;
            }
            return true;
        }

        //归档一批文件
        private static bool DoArchiveDir(string[] needfiles, string indir, string outPath, IFSCompressType compressType)
        {
            if (needfiles == null || needfiles.Length == 0)
            {
                return false;
            }
            //
            int fileCnt = needfiles.Length;
            //
            IFSFile ifsFile = new IFSFile();
            ifsFile.EntryCount = fileCnt;
            ifsFile.CompressType = compressType;
            ifsFile.Entrys = new IFSEntry[fileCnt];
            for (int i = 0; i < fileCnt; i++)
            {
                ifsFile.Entrys[i] = new IFSEntry();
            }
            //
            //初始化条目
            for (int i = 0; i < fileCnt; i++)
            {
                IFSEntry entry = ifsFile.Entrys[i];
                string relativePath = FileUtil.GetRelativePath(needfiles[i], indir);
                entry.Name = relativePath;
                entry.DataSize = FileUtil.GetFileLength(needfiles[i]);
            }
            int entryNameSegL = 0;
            //写入开始
            int begin = 0;
            //签名
            begin += 4;
            //压缩方式
            begin += 4;
            //条目个数
            begin += 4;
            //条目名字段长度
            begin += 4;
            //条目名称信息
            for (int i = 0; i < ifsFile.Entrys.Length; i++)
            {
                IFSEntry entry = ifsFile.Entrys[i];
                int l = GetStringBytesLength(entry.Name);
                begin += l;
                entryNameSegL += l;
            }
            //条目数据位置段
            for (int i = 0; i < ifsFile.Entrys.Length; i++)
            {
                begin += 4;
            }
            //设置
            for (int i = 0; i < ifsFile.Entrys.Length; i++)
            {
                IFSEntry entry = ifsFile.Entrys[i];
                if (i == 0)
                {
                    entry.DataPos = begin;
                }
                else
                {
                    IFSEntry entryPre = ifsFile.Entrys[i - 1];
                    //数据开始位置是上一个的开始位置+4+L
                    entry.DataPos = entryPre.DataPos + 4 + entryPre.DataSize;
                }
            }
            //开始写
            //写大文件
            FileStream outIfs = new FileStream(outPath, FileMode.Create);
            //先写入文件头签名
            byte[] bb = ConvertIntToBytes((int)ifsFile.Signature);
            outIfs.Write(bb, 0, bb.Length);
            //写入压缩方式
            bb = ConvertIntToBytes((int)ifsFile.CompressType);
            outIfs.Write(bb, 0, bb.Length);
            //条目个数
            bb = ConvertIntToBytes(ifsFile.Entrys.Length);
            outIfs.Write(bb, 0, bb.Length);
            //条目名称总长度
            bb = ConvertIntToBytes(entryNameSegL);
            outIfs.Write(bb, 0, bb.Length);
            //条目名字信息
            for (int i = 0; i < ifsFile.Entrys.Length; i++)
            {
                IFSEntry entry = ifsFile.Entrys[i];
                byte[] bbs = ConvertStringToBytes(entry.Name);
                //名字
                outIfs.Write(bbs, 0, bbs.Length);
            }
            //条目数据位置开始位置索引写入
            for (int i = 0; i < ifsFile.Entrys.Length; i++)
            {
                IFSEntry entry = ifsFile.Entrys[i];
                byte[] bbs = ConvertIntToBytes(entry.DataPos);
                //名字
                outIfs.Write(bbs, 0, bbs.Length);
            }
            try
            {
                //写入所有文件数据
                for (int i = 0; i < ifsFile.Entrys.Length; i++)
                {
                    IFSEntry entry = ifsFile.Entrys[i];
                    //长度
                    byte[] ddl = ConvertIntToBytes(entry.DataSize);
                    outIfs.Write(ddl, 0, ddl.Length);
                    //写入数据
                    byte[] fileData = FileUtil.ReadFile(needfiles[i]);
                    outIfs.Write(fileData, 0, entry.DataSize);
                }
            }
            catch (Exception exc)
            {
                JW.Common.Log.LogE("Archive Exception:" + exc.ToString());
                return false;
            }
            outIfs.Flush();
            outIfs.Close();
            return true;
        }


        /// <summary>
        /// 同步解档一个IFS文件到目录
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="outDir"></param>
        /// <returns></returns>
        public static bool SyncUnarchiveIFSFile(string filePath, string outDir,bool clearDir=false)
        {
            if (!FileUtil.IsFileExist(filePath))
            {
                JW.Common.Log.LogE("IFSArchiver SyncUnarchiveIFSFile Error No IFS File");
                return false;
            }
            int totalL = FileUtil.GetFileLength(filePath);
            if (totalL < 20)
            {
                JW.Common.Log.LogE("IFSArchiver SyncUnarchiveIFSFile Error IFS File Length");
                return false;
            }
            if (FileUtil.IsDirectoryExist(outDir))
            {
                if (clearDir)
                {
                    FileUtil.ClearDirectory(outDir);
                }
            }
            else
            {
                FileUtil.CreateDirectory(outDir);
            }
            //
            JW.Common.Log.LogD("Begin->");
            Stopwatch st = new Stopwatch();
            st.Start();
            //
            IFSFile ifsFile = new IFSFile();
            FileStream fileS = new FileStream(filePath, FileMode.Open);
            //获取签名
            byte[] bbs = new byte[16];
            int offset = 0;
            fileS.Read(bbs, 0, 16);
            uint sig = (uint)ConvertBytesToInt(bbs, ref offset);
            if (sig != IFSFile.IFSSignature)
            {
                JW.Common.Log.LogE("IFSArchiver SyncUnarchiveIFSFile Error Signature ");
                fileS.Close();
                return false;
            }
            ifsFile.Signature = sig;
            //获取压缩方式
            ifsFile.CompressType = (IFSCompressType)ConvertBytesToInt(bbs, ref offset);
            //条目个数
            ifsFile.EntryCount = ConvertBytesToInt(bbs, ref offset);
            //名称长度
            int entryNameL = ConvertBytesToInt(bbs, ref offset);
            if (ifsFile.EntryCount == 0)
            {
                JW.Common.Log.LogE("IFSArchiver SyncUnarchiveIFSFile Error  EntryCount");
                fileS.Close();
                return false;
            }
            //条目名称段
            ifsFile.Entrys = new IFSEntry[ifsFile.EntryCount];
            for (int i = 0; i < ifsFile.EntryCount; i++)
            {
                ifsFile.Entrys[i] = new IFSEntry();
            }
            //
            bbs = null;
            //
            byte[] names = new byte[entryNameL];
            fileS.Read(names, 0, entryNameL);
            offset = 0;
            //读取条目名称
            for (int i = 0; i < ifsFile.EntryCount; i++)
            {
                IFSEntry entry = ifsFile.Entrys[i];
                entry.Name = ConvertBytesToString(names, ref offset);
            }
            //条目数据位置
            offset = 0;
            names = null;
            byte[] poss = new byte[ifsFile.EntryCount * 4];
            fileS.Read(poss, 0, ifsFile.EntryCount * 4);
            for (int i = 0; i < ifsFile.EntryCount; i++)
            {
                IFSEntry entry = ifsFile.Entrys[i];
                int vv = ConvertBytesToInt(poss, ref offset);
                entry.DataPos = vv;
            }
            offset = 0;
            poss = null;
            #region 无压缩 直接解压
            //无压缩 直接解压
            if (ifsFile.CompressType == IFSCompressType.None)
            {
                bool isOk = false;
                try
                {
                    //公用buffer
                    byte[] buffer = new byte[1024 *10];
                    //解压数据到文件
                    for (int i = 0; i < ifsFile.EntryCount; i++)
                    {
                        IFSEntry entry = ifsFile.Entrys[i];
                        string outPath = FileUtil.CombinePaths(outDir, entry.Name);
                        string directory = Path.GetDirectoryName(outPath);
                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }
                        FileStream output = new FileStream(outPath, FileMode.Create);
                        try
                        {
                            fileS.Seek(entry.DataPos, SeekOrigin.Begin);
                            byte[] ll = new byte[4];
                            fileS.Read(ll, 0, 4);
                            offset = 0;
                            int vv = ConvertBytesToInt(ll, ref offset);
                            entry.DataSize = vv;
                            //
                            bool copying = true;
                            int processed = 0;
                            while (copying)
                            {
                                int bytesRead = fileS.Read(buffer, 0, buffer.Length);
                                if (bytesRead > 0)
                                {
                                    if (entry.DataSize > bytesRead)
                                    {
                                        processed += bytesRead;
                                        if (processed > entry.DataSize)
                                        {
                                            output.Write(buffer, 0, bytesRead-(processed - entry.DataSize));
                                            copying = false;
                                        }
                                        else
                                        {
                                            output.Write(buffer, 0, bytesRead);
                                            copying = true;
                                        }
                                    }
                                    else
                                    {
                                        //少于buffer
                                        output.Write(buffer, 0, entry.DataSize);
                                        copying = false;
                                    }
                                }
                                else
                                {
                                    copying = false;
                                }
                            }
                            //
                            output.Flush();
                        }
                        catch (Exception exc)
                        {
                            JW.Common.Log.LogE("Failed:" + entry.Name + "---" + exc.ToString());
                        }
                        finally
                        {
                            output.Close();
                            output.Dispose();
                            output = null;
                        }
                    }
                }
                catch (Exception exc)
                {
                    JW.Common.Log.LogE("IFSArchiver SyncUnarchiveIFSFile:" + exc.ToString());
                    isOk = false;
                }
                finally
                {
                    fileS.Close();
                    fileS.Dispose();
                    fileS = null;
                }
                st.Stop();//终止计时
                JW.Common.Log.LogD("Done:" + st.ElapsedMilliseconds.ToString());
                return isOk;
            }
            #endregion
            //
            if (ifsFile.CompressType == IFSCompressType.LZMA)
            {
                bool isOk = DoUnarchiveLZMAIFSFile(ifsFile, outDir, fileS);
                st.Stop();
                JW.Common.Log.LogD("Done:" + st.ElapsedMilliseconds.ToString());
                return isOk;
            }
            return true;
        }

        /// <summary>
        /// 同步解压一个IFS文件内存流
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="outDir"></param>
        /// <returns></returns>
        public static bool SyncUnarchiveIFSFile(MemoryStream fileS, string outDir)
        {
            if (FileUtil.IsDirectoryExist(outDir))
            {
                FileUtil.ClearDirectory(outDir);
            }
            else
            {
                FileUtil.CreateDirectory(outDir);
            }
            //
            JW.Common.Log.LogD("Begin->");
            Stopwatch st = new Stopwatch();
            st.Start();
            //
            IFSFile ifsFile = new IFSFile();
            //获取签名
            byte[] bbs = new byte[16];
            int offset = 0;
            fileS.Read(bbs, 0, 16);
            uint sig = (uint)ConvertBytesToInt(bbs, ref offset);
            if (sig != IFSFile.IFSSignature)
            {
                JW.Common.Log.LogE("IFSArchiver SyncUnarchiveIFSFile Error Signature ");
                fileS.Close();
                return false;
            }
            ifsFile.Signature = sig;
            //获取压缩方式
            ifsFile.CompressType = (IFSCompressType)ConvertBytesToInt(bbs, ref offset);
            //条目个数
            ifsFile.EntryCount = ConvertBytesToInt(bbs, ref offset);
            //名称长度
            int entryNameL = ConvertBytesToInt(bbs, ref offset);
            if (ifsFile.EntryCount == 0)
            {
                JW.Common.Log.LogE("IFSArchiver SyncUnarchiveIFSFile Error  EntryCount");
                fileS.Close();
                return false;
            }
            //条目名称段
            ifsFile.Entrys = new IFSEntry[ifsFile.EntryCount];
            for (int i = 0; i < ifsFile.EntryCount; i++)
            {
                ifsFile.Entrys[i] = new IFSEntry();
            }
            //
            bbs = null;
            //
            byte[] names = new byte[entryNameL];
            fileS.Read(names, 0, entryNameL);
            offset = 0;
            //读取条目名称
            for (int i = 0; i < ifsFile.EntryCount; i++)
            {
                IFSEntry entry = ifsFile.Entrys[i];
                entry.Name = ConvertBytesToString(names, ref offset);
            }
            //条目数据位置
            offset = 0;
            names = null;
            byte[] poss = new byte[ifsFile.EntryCount * 4];
            fileS.Read(poss, 0, ifsFile.EntryCount * 4);
            for (int i = 0; i < ifsFile.EntryCount; i++)
            {
                IFSEntry entry = ifsFile.Entrys[i];
                int vv = ConvertBytesToInt(poss, ref offset);
                entry.DataPos = vv;
            }
            offset = 0;
            poss = null;
            #region 无压缩 直接解压
            //无压缩 直接解压
            if (ifsFile.CompressType == IFSCompressType.None)
            {
                bool isOk = false;
                try
                {
                    //公用buffer
                    byte[] buffer = new byte[4 * 1024];
                    //解压数据到文件
                    for (int i = 0; i < ifsFile.EntryCount; i++)
                    {
                        IFSEntry entry = ifsFile.Entrys[i];
                        string outPath = FileUtil.CombinePaths(outDir, entry.Name);
                        string directory = Path.GetDirectoryName(outPath);
                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }
                        FileStream output = new FileStream(outPath, FileMode.Create);
                        try
                        {
                            fileS.Seek(entry.DataPos, SeekOrigin.Begin);
                            byte[] ll = new byte[4];
                            fileS.Read(ll, 0, 4);
                            offset = 0;
                            int vv = ConvertBytesToInt(ll, ref offset);
                            entry.DataSize = vv;
                            //
                            bool copying = true;
                            int processed = 0;
                            while (copying)
                            {
                                int bytesRead = fileS.Read(buffer, 0, buffer.Length);
                                if (bytesRead > 0)
                                {
                                    if (entry.DataSize <= bytesRead)
                                    {
                                        output.Write(buffer, 0, entry.DataSize);
                                        copying = false;
                                    }
                                    else
                                    {
                                        processed += bytesRead;
                                        if (processed > entry.DataSize)
                                        {
                                            output.Write(buffer, 0, bytesRead - (processed - entry.DataSize));
                                            copying = false;
                                        }
                                        else
                                        {
                                            output.Write(buffer, 0, bytesRead);
                                            copying = true;
                                        }
                                    }
                                }
                                else
                                {
                                    copying = false;
                                }
                            }
                            //
                            output.Flush();
                        }
                        catch (Exception exc)
                        {
                            JW.Common.Log.LogE("Failed:" + entry.Name + "---" + exc.ToString());
                        }
                        finally
                        {
                            output.Close();
                            output.Dispose();
                            output = null;
                        }
                    }
                }
                catch (Exception exc)
                {
                    JW.Common.Log.LogE("IFSArchiver SyncUnarchiveIFSFile:" + exc.ToString());
                    isOk = false;
                }
                finally
                {
                    st.Stop();//终止计时
                    JW.Common.Log.LogD("Done:" + st.ElapsedMilliseconds.ToString());
                    fileS.Close();
                    fileS.Dispose();
                    fileS = null;
                }
                st.Stop();//终止计时
                JW.Common.Log.LogD("Done:" + st.ElapsedMilliseconds.ToString());
                return isOk;
            }
            #endregion
            //
            if (ifsFile.CompressType == IFSCompressType.LZMA)
            {
                bool isOk = DoUnarchiveLZMAIFSFile(ifsFile, outDir, fileS);
                st.Stop();
                JW.Common.Log.LogD("Done:" + st.ElapsedMilliseconds.ToString());
                return isOk;
            }
            return true;
        }

        //
        private static bool DoUnarchiveLZMAIFSFile(IFSFile ifsFile, string outDir, Stream fileS)
        {
            bool isOk = true;
            try
            {
                //公用buffer
                byte[] buffer = new byte[4 * 1024];
                int offset = 0;
                //解压数据到文件
                for (int i = 0; i < ifsFile.EntryCount; i++)
                {
                    IFSEntry entry = ifsFile.Entrys[i];
                    JW.Common.Log.LogD("Un LZMA File:" + entry.Name);
                    //
                    fileS.Seek(entry.DataPos, SeekOrigin.Begin);
                    byte[] ll = new byte[4];
                    fileS.Read(ll, 0, 4);
                    offset = 0;
                    int vv = ConvertBytesToInt(ll, ref offset);
                    entry.DataSize = vv;
                    //解压
                    string outPath = FileUtil.CombinePaths(outDir, entry.Name);
                    string directory = Path.GetDirectoryName(outPath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    FileStream output = new FileStream(outPath, FileMode.Create);
                    try
                    {
                        SevenZip.Compression.LZMA.Decoder coder = new SevenZip.Compression.LZMA.Decoder();
                        byte[] properties = new byte[5];
                        fileS.Read(properties, 0, 5);
                        byte[] fileLengthBytes = new byte[8];
                        fileS.Read(fileLengthBytes, 0, 8);
                        long fileLength = BitConverter.ToInt64(fileLengthBytes, 0);
                        coder.SetDecoderProperties(properties);
                        coder.Code(fileS, output, entry.DataSize, fileLength, null);
                        output.Flush();
                    }
                    catch (Exception exc)
                    {
                        JW.Common.Log.LogE("LZMA Failed:" + entry.Name + "---" + exc.ToString());
                    }
                    finally
                    {
                        output.Close();
                        output.Dispose();
                        output = null;
                    }
                }
            }
            catch (Exception exc)
            {
                JW.Common.Log.LogE("LZMA Failed:" + exc.ToString());
                isOk = false;
            }
            finally
            {
                fileS.Close();
                fileS.Dispose();
            }
            return isOk;
        }


        #region 基础数据转换

        public static byte[] ConvertIntToBytes(int value)
        {
            byte[] bytes = new byte[4];
            bytes[0] = (byte)value;
            bytes[1] = (byte)(value >> 8);
            bytes[2] = (byte)(value >> 16);
            bytes[3] = (byte)(value >> 24);
            return bytes;
        }

        public static byte[] ConvertShortToBytes(short value)
        {
            byte[] bytes = new byte[2];
            bytes[0] = (byte)value;
            bytes[1] = (byte)(value >> 8);
            return bytes;
        }

        public static byte[] ConvertStringToBytes(string str)
        {
            byte[] strby = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] bytes = new byte[strby.Length + 2];
            int length = strby.Length;
            //写入字符串数据长度
            bytes[0] = (byte)length;
            bytes[1] = (byte)(length >> 8);
            //字符串数据
            for (int i = 2; i < strby.Length + 2; i++)
            {
                bytes[i] = strby[i - 2];
            }
            return bytes;
        }
        //
        public static int GetStringBytesLength(string str)
        {
            byte[] strby = System.Text.Encoding.UTF8.GetBytes(str);
            return strby.Length + 2;
        }

        //
        public static int ConvertBytesToShort(byte[] data, ref int offset)
        {
            int value = ((data[offset + 1] << 8) | data[offset]);
            offset += 2;
            return value;
        }
        //
        public static int ConvertBytesToInt(byte[] data, ref int offset)
        {
            int value = ((data[offset + 3] << 24) | (data[offset + 2] << 16) | (data[offset + 1] << 8) | data[offset]);
            offset += 4;
            return value;
        }

        public static string ConvertBytesToString(byte[] data, ref int offset)
        {
            //读出字符串数据长度
            int length = ConvertBytesToShort(data, ref offset);
            //读出字符串
            string str = System.Text.Encoding.UTF8.GetString(data, offset, length);
            offset += length;
            return str;
        }
        #endregion

        #region 压缩解压相关
        private static void CompressFileLZMAToFile(string inFile, string outFile)
        {
            SevenZip.Compression.LZMA.Encoder coder = new SevenZip.Compression.LZMA.Encoder();
            FileStream input = new FileStream(inFile, FileMode.Open);
            FileStream output = new FileStream(outFile, FileMode.Create);
            coder.WriteCoderProperties(output);
            output.Write(BitConverter.GetBytes(input.Length), 0, 8);
            coder.Code(input, output, input.Length, -1, null);
            output.Flush();
            input.Close();
            output.Close();
        }

        #endregion
    }
}
