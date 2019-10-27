using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using JW.Common;
using JW.IFS;
using JW.Res;
using System.IO;
using System;

namespace JW.Framework.IFS
{
    /// <summary>
    /// 负责下载IFS 文件 同时一边下载一边解档到本地
    /// </summary>
    public class IFSFileDownloader
    {

        /// <summary>
        /// 自定义UnityWebRequest下载器
        /// </summary>
        private class IFSDownloadHandler : DownloadHandlerScript
        {
            public IFSFileDownloader Downloader;


            public IFSDownloadHandler(byte[] buffer):base(buffer)
            {
                
            }

            protected override void CompleteContent()
            {
                Downloader.CompleteContent();
            }

            protected override float GetProgress()
            {
                return Downloader.Progress;
            }

            protected override void ReceiveContentLength(int contentLength)
            {
                Downloader.ReceiveContentLength(contentLength);
            }

            protected override bool ReceiveData(byte[] data, int dataLength)
            {
                return Downloader.ReceiveData(ref data, dataLength);
            }
        }

        //
        private string _fileUrl;
        private string _unarchiveDir;
        private bool _isDone;
        private float _progress;
        private bool _isError;
        private UnityWebRequest _www;
        private IFSDownloadHandler _downloadHandler;
        private byte[] _downloadBuffer;
        private IFSFile _ifsFile;
        private int _totalLength;
        private int _receivedLength;

        public IFSFileDownloader(string url,string dir)
        {
            _fileUrl = url;
            _unarchiveDir = dir;
        }

        public bool IsDone
        {
            get
            {
                return _isDone;
            }
        }

        public bool IsError
        {
            get
            {
                return _isError;
            }
        }

        public float Progress
        {
            get
            {
                return _progress;
            }
        }

        public bool Begin()
        {
            if (string.IsNullOrEmpty(_fileUrl))
            {
                JW.Common.Log.LogE("Error FileUrl");
                return false;
            }
            if (_www != null)
            {
                JW.Common.Log.LogE("Duplicate Begin Session");
                return false;
            }
            _progress = 0.0f;
            _www = new UnityWebRequest(_fileUrl);
            _downloadBuffer = new byte[1024 * 1024];
            _downloadHandler = new IFSDownloadHandler(_downloadBuffer);
            _downloadHandler.Downloader = this;
            _www.downloadHandler = _downloadHandler;
            _www.timeout = 100;
            
            _www.SendWebRequest();

            return true;
        }


        public void Update()
        {
            if (_isError || _isDone)
            {
                _isDone = true;
                Abort();
            }

            if (_www != null)
            {
                if (_www.isNetworkError||_www.isHttpError)
                {
                    JW.Common.Log.LogE("IFSFileDownloader Net Error:"+_www.error);
                    _isError = true;
                    _isDone = true;
                    Abort();
                }
            }
           
        }

        public bool Abort()
        {
            if (_www != null)
            {
                _www.Abort();
                _www.Dispose();
                _www = null;
            }
            if(_downloadHandler != null){

                _downloadHandler.Dispose();
                _downloadHandler = null;
            }
            _downloadBuffer = null;
            _waitToDealBytes = null;
            _ifsFile = null;
            return true;
        }


        public void Dispose()
        {
            if (_www != null)
            {
                _www.Dispose();
                _www = null;
            }
            if (_downloadHandler != null)
            {
                _downloadHandler.Dispose();
                _downloadHandler = null;
            }
            _downloadBuffer = null;
            _waitToDealBytes = null;
            _ifsFile = null;
        }

        //------------
        private void ReceiveContentLength(int contentLength)
        {
            JW.Common.Log.LogD("IFSDownloader ReceiveContentLength:{0:D} ", contentLength);
            //
            if (contentLength < 20)
            {
                JW.Common.Log.LogE("IFSDownloader Error ContentLength");
                _isError = true;
                Abort();
                return;
            }
            //
            _ifsFile = new IFSFile();
            _waitToDealBytes = new List<byte>(1024*1024);
            _dealState = 0;
            _dealOffset = 0;
            _entryNameLength = 0;
            _dealEntryDataIndex = 0;
            _receivedLength = 0;
            _totalLength = contentLength;
        }

        //等待处理的字节组
        private List<byte> _waitToDealBytes = null;
        //解析阶段 0 开始获取签名 1 获取压缩方式 2 获取条目个数 3 获取条目名称总长度  4 解析条目 5.解析条目数据位置 6.条目数据解析
        private int _dealState = 0;
        private int _dealOffset = 0;
        private int _entryNameLength = 0;
        private int _dealEntryDataIndex = 0;
        //
        private bool ReceiveData(ref byte[] data, int dataLength)
        {
            //JW.Common.Log.LogD("IFSDownloader ReceiveData Length:{0:D} ", dataLength);

            if (dataLength <= 0)
            {
                return true;
            }
            //
            _receivedLength += dataLength;
            _progress = Mathf.Clamp01(_receivedLength / (float)_totalLength);
            //
            for (int i = 0; i < dataLength; i++)
            {
                _waitToDealBytes.Add(data[i]);
            }
            //
            DealData();
            //
            return true;
        }

        //处理数据
        private void DealData()
        {
            if (_dealState == 0)
            {
                if (DealSignatureData())
                {
                    _dealState = 1;
                }
            }

            if (_dealState == 1)
            {
                if (DealCompressType())
                {
                    _dealState = 2;
                }
            }
            if (_dealState == 2)
            {
                if (DealEntryCnt())
                {
                    _dealState = 3;
                }
            }
            if (_dealState == 3)
            {
                if (DealEntryNameLength())
                {
                    _dealState = 4;
                }
            }
            if (_dealState == 4)
            {
                if (DealEntryNames())
                {
                    _dealState = 5;
                }
            }
            if (_dealState == 5)
            {
                if (DealEntryDataPos())
                {
                    _dealEntryDataIndex = 0;
                    _dealState = 6;
                }
            }
            if (_dealState == 6)
            {
                while (true)
                {
                    bool isOk=DealEntryDatas();
                    if (isOk==false)
                    {
                        break;
                    }
                }
            }
        }

        //处理签名获取数据
        private bool DealSignatureData()
        {
            if (_waitToDealBytes.Count > 4)
            {
                byte[] dd = _waitToDealBytes.GetRange(0, 4).ToArray();
                _dealOffset = 0;
                uint sig = (uint)IFSArchiver.ConvertBytesToInt(dd, ref _dealOffset);
                //
                _waitToDealBytes.RemoveRange(0, 4);
                //
                if (sig != IFSFile.IFSSignature)
                {
                    JW.Common.Log.LogE("IFSDownloader DealSignatureData Error Signature ");
                    //
                    _isError = true;
                    return false;
                }
                else
                {
                    JW.Common.Log.LogD("Get Signature:{0:X}", sig);
                    return true;
                }
            }
            return false;
        }
        //获取压缩方式
        private bool DealCompressType( )
        {
            if (_waitToDealBytes.Count > (4))
            {
                byte[] dd = _waitToDealBytes.GetRange(0, 4).ToArray();
                _dealOffset = 0;
                _ifsFile.CompressType = (IFSCompressType)IFSArchiver.ConvertBytesToInt(dd, ref _dealOffset);
                _waitToDealBytes.RemoveRange(0, 4);
                JW.Common.Log.LogD("Get CompressType:{0}", _ifsFile.CompressType.ToString());
                return true;
            }
            return false;
        }

        //条目个数
        private bool DealEntryCnt()
        {
            if (_waitToDealBytes.Count > 4)
            {
                byte[] dd = _waitToDealBytes.GetRange(0, 4).ToArray();
                _dealOffset = 0;
                _ifsFile.EntryCount =IFSArchiver.ConvertBytesToInt(dd, ref _dealOffset);
                _waitToDealBytes.RemoveRange(0, 4);
                JW.Common.Log.LogD("Get EntryCount:{0}", _ifsFile.EntryCount);

                if (_ifsFile.EntryCount <= 0)
                {
                    JW.Common.Log.LogE("Get Error EntryCount");
                    _isError = true;
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        //条目名称长度
        private bool DealEntryNameLength()
        {
            if (_waitToDealBytes.Count > 4)
            {
                byte[] dd = _waitToDealBytes.GetRange(0, 4).ToArray();
                _dealOffset = 0;
                _entryNameLength = IFSArchiver.ConvertBytesToInt(dd, ref _dealOffset);
                _waitToDealBytes.RemoveRange(0, 4);
                JW.Common.Log.LogD("Get EntryName Length:{0}", _entryNameLength);
                dd = null;
                if (_entryNameLength <= 0)
                {
                    JW.Common.Log.LogE("Get Error EntryName Length");
                    _isError = true;
                    return false;
                }
                else
                {
                    //条目名称段
                    _ifsFile.Entrys = new IFSEntry[_ifsFile.EntryCount];
                    for (int i = 0; i < _ifsFile.EntryCount; i++)
                    {
                        _ifsFile.Entrys[i] = new IFSEntry();
                    }
                    return true;
                }
                
            }
            return false;
        }

        //处理条目名称
        private bool DealEntryNames()
        {
            if (_waitToDealBytes.Count > _entryNameLength)
            {
                byte[] names = _waitToDealBytes.GetRange(0, _entryNameLength).ToArray(); 
                _dealOffset = 0;
                //读取条目名称
                for (int i = 0; i < _ifsFile.EntryCount; i++)
                {
                    IFSEntry entry = _ifsFile.Entrys[i];
                    entry.Name =IFSArchiver.ConvertBytesToString(names, ref _dealOffset);
                    JW.Common.Log.LogD("Get Entry Name:"+entry.Name);
                }
                //
                _waitToDealBytes.RemoveRange(0, _entryNameLength);
                //
                names = null;
                return true;
            }
            return false;
        }

        //条目数据位置信息
        private bool DealEntryDataPos()
        {
            if (_waitToDealBytes.Count > (_ifsFile.EntryCount*4))
            {
                byte[] poss = _waitToDealBytes.GetRange(0, _ifsFile.EntryCount * 4).ToArray();
                _dealOffset = 0;
                for (int i = 0; i < _ifsFile.EntryCount; i++)
                {
                    IFSEntry entry = _ifsFile.Entrys[i];
                    int vv =IFSArchiver.ConvertBytesToInt(poss, ref _dealOffset);
                    entry.DataPos = vv;
                }
                _waitToDealBytes.RemoveRange(0, _ifsFile.EntryCount * 4);
                poss = null;
                return true;
            }
            return false;
        }

        //处理条目文件数据 
        private bool DealEntryDatas()
        {
            if (_waitToDealBytes.Count > 4 &&(_dealEntryDataIndex<_ifsFile.EntryCount))
            {
                //数据长度
                _dealOffset = 0;
                IFSEntry entry = _ifsFile.Entrys[_dealEntryDataIndex];
                byte[] ll = _waitToDealBytes.GetRange(0,4).ToArray(); ;
                int vv = IFSArchiver.ConvertBytesToInt(ll, ref _dealOffset);
                entry.DataSize = vv;
                if (_waitToDealBytes.Count >= (4 + vv))
                {
                    string outPath = JW.Res.FileUtil.CombinePaths(_unarchiveDir, entry.Name);
                    string directory = Path.GetDirectoryName(outPath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    //数据
                    byte[] fileDd = _waitToDealBytes.GetRange(4, vv).ToArray();

                    if (_ifsFile.CompressType == IFSCompressType.None)
                    {
                        //
                        JW.Res.FileUtil.DeleteFile(outPath);
                        //
                        FileStream output = new FileStream(outPath, FileMode.Create);
                        try
                        {
                            output.Write(fileDd, 0, vv);
                            output.Flush();
                        }
                        finally
                        {
                            output.Close();
                            output.Dispose();
                            output = null;
                        }
                    }
                    else if (_ifsFile.CompressType == IFSCompressType.LZMA)
                    {
                        //LZMA 数据
                        UnLzmaDataToFile(outPath, fileDd,vv);
                    }
                    //
                    _dealEntryDataIndex++;
                    //移除
                    _waitToDealBytes.RemoveRange(0, vv+4);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        
        //解压数据
        private void UnLzmaDataToFile(string outPath,byte[] memoryDD,int dl)
        {
            FileStream output = new FileStream(outPath, FileMode.Create);
            MemoryStream ms = new MemoryStream(memoryDD);
            try
            {
                SevenZip.Compression.LZMA.Decoder coder = new SevenZip.Compression.LZMA.Decoder();
                byte[] properties = new byte[5];
                ms.Read(properties, 0, 5);
                byte[] fileLengthBytes = new byte[8];
                ms.Read(fileLengthBytes, 0, 8);
                long fileLength = BitConverter.ToInt64(fileLengthBytes, 0);
                coder.SetDecoderProperties(properties);
                coder.Code(ms, output, dl, fileLength, null);
                output.Flush();
            }
            catch (Exception exc)
            {
                _isError = true;
                JW.Common.Log.LogE("Un LZMA Data Failed:" + outPath + "---" + exc.ToString());
            }
            finally
            {
                output.Close();
                output.Dispose();
                output = null;
                ms.Close();
                ms.Dispose();
                ms = null;
            }
        }

        //
        private void CompleteContent()
        {
            JW.Common.Log.LogD("IFSDownloader CompleteContent");
            _isDone = true;
            _progress = 1.0f;
        }
    }
}


