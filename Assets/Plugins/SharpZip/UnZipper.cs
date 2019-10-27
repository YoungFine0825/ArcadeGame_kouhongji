using System.Collections;
using System.IO;
using UnityEngine;
using ICSharpCode.SharpZipLib.Zip;
using UnityEngine.Networking;
using System;
using ICSharpCode.SharpZipLib.Core;
using System.Threading;

namespace ICSharpCode.SharpZipLib
{
    public class UnZipper
    {
        public bool IsDone = false;
        public float CurProgress = 0.0f;
        private int _fileTotal = 0;
        private int _extractedFileCnt = 0;

        FastZipEvents _zipEvent = null;
        FastZip _fastZip = null;

        Thread _zipThread = null;

        private MemoryStream _stream;

        public void Begin(string zipFile, string extractPath, string password)
        {
            // 打开文件
            FileStream fileStream = new FileStream(zipFile, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            // 把 byte[] 转换成 Stream
            _stream = new MemoryStream(bytes);
            //解压
            _zipEvent = new FastZipEvents();
            _zipEvent.CompletedFile = CompletedFileHandler;
            _zipEvent.DirectoryFailure = DirectoryFailureHandler;
            _zipEvent.FileFailure = FileFailureHandler;

            _fastZip = new FastZip(_zipEvent);
            _fastZip.Password = password;
            _fastZip.CreateEmptyDirectories = true;
            _fileTotal = _fastZip.UnityExtractStreamZipInit(_stream, extractPath);
            //
            IsDone = false;
            CurProgress = 0.0f;
        }

        public void Start()
        {
            _zipThread = new Thread(new ThreadStart(DoUnZip));
            _zipThread.Priority = System.Threading.ThreadPriority.Highest;
            _zipThread.IsBackground = true;
            _zipThread.Start();
        }

        public void Stop()
        {
            if (_zipEvent != null)
            {
                _zipEvent.CompletedFile = null;
                _zipEvent.DirectoryFailure = null;
                _zipEvent.FileFailure = null;
                _zipEvent = null;
            }
            if (_zipThread != null)
            {
                _zipThread.Abort();
                _zipThread = null;
            }
            if (_stream != null)
            {
                _stream.Close();
                _stream = null;
            }
            _fastZip = null;
            _fileTotal = 0;
            _extractedFileCnt = 0;
            CurProgress = 0.0f;
        }

        private void DoUnZip()
        {
            if (_fastZip != null)
            {
                _fastZip.UnityExtractStreamZipBegin();
            }
        }


        public void CompletedFileHandler(object sender, ScanEventArgs e)
        {
            Debug.Log("UnZip CompletedFile:" + e.Name);
            e.ContinueRunning = true;
            //
            _extractedFileCnt++;
            CurProgress = (float)_extractedFileCnt / _fileTotal;
            if (_extractedFileCnt >= _fileTotal)
            {
                IsDone = true;
                CurProgress = 1.0f;
            }
        }

        public void DirectoryFailureHandler(object sender, ScanFailureEventArgs e)
        {
            Debug.Log("UnZip DirectoryFailure");
            e.ContinueRunning = true;
        }

        public void FileFailureHandler(object sender, ScanFailureEventArgs e)
        {
            Debug.Log("UnZip FileFailure");
            e.ContinueRunning = true;
            //失败也算
            _extractedFileCnt++;
            CurProgress = (float)_extractedFileCnt / _fileTotal;
            //
            if (_extractedFileCnt >= _fileTotal)
            {
                IsDone = true;
                CurProgress = 1.0f;
            }
        }
    }
}
