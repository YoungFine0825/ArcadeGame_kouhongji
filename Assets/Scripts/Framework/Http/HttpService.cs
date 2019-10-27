/********************************************************************
	created:	16:10:2017   15:15
	author:		jordenwu
	purpose:	封装Http 操作 主要针对 异步获取http 文本 或者imgae 或者下载文件
*********************************************************************/
using System;
using System.Net;
using System.IO;
using JW.Common;
using JW.Framework.Schedule;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;

namespace JW.Framework.Http
{
    /// <summary>
    /// 文件下载事务委托定义
    /// </summary>
    /// <param name="session"></param>
    public delegate void HttpFileSessionDelegate(bool isError, uint sessionId, bool isEnd, float progress);

    /// <summary>
    /// 一个Http异步下载文件 事务 方法 GET
    /// </summary>
    public class HttpFileSession
    {
        //事务Id
        public uint Id;
        //对应的Url
        public string Url;
        //下载的本地文件路径
        public string DownloadFilePath;
        //
        public HttpFileSessionDelegate Handler;

        //超时
        private static readonly int TimeOut = 5000;
        private static readonly int BuffLen = 1024 * 400;

        private HttpWebRequest _request = null;
        private HttpWebResponse _response = null;
        private Stream _responseStream = null;

        private byte[] _buff = null;
        private FileStream _fileStream;
        private long _totalFileLength = 0;
        private long _curDownloadLength = 0;
        //
        private bool _isOver = false;
        private bool _isError = false;
        //
        private string _tempDownLoadFilePath = "";

        //管理用
        public bool IsOver = false;
      

        public bool IsBegined = false;
        private int _waitedAsyn = 0;

        //开始
        public void Begin()
        {
            IsBegined = true;
            if (string.IsNullOrEmpty(Url))
            {
                JW.Common.Log.LogE("HttpFileSession Url is Error");
                _isError = true;
                _isOver = true;
                return;
            }
            if (string.IsNullOrEmpty(DownloadFilePath))
            {
                JW.Common.Log.LogE("HttpFileSession DownloadFilePath is Error");
                _isError = true;
                _isOver = true;
                return;
            }
            _tempDownLoadFilePath = JW.Res.FileUtil.CombinePath(HttpService.TempDir, Id.ToString() + ".temp");
            //
            _request = WebRequest.Create(new Uri(Url)) as HttpWebRequest;
            _request.Method = "GET";
            _request.Timeout = TimeOut;
            _request.ReadWriteTimeout = TimeOut;

            //https
            if (Url.Contains("https://"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                _request.ProtocolVersion = HttpVersion.Version10;
            }
            _request.AllowAutoRedirect = true;
            _isOver = false;
            _isError = false;
            IsOver = false;
            _waitedAsyn = 0;
            _request.BeginGetResponse(this.OnAsyncRespond, _request);
            
        }

        private void OnAsyncRespond(IAsyncResult ar)
        {
            bool isException = false;
            try
            {
                HttpWebRequest request = (HttpWebRequest)ar.AsyncState;
                if (request == null)
                {
                    _isError = true;
                }
                else
                {
                    HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(ar);
                    if (response != null)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            _response = response;
                            //
                            _totalFileLength = _response.ContentLength;
                            _curDownloadLength = 0;
                            _buff = new byte[BuffLen];
                            _responseStream = _response.GetResponseStream();
                            //
                            if (JW.Res.FileUtil.IsFileExist(_tempDownLoadFilePath))
                            {
                                JW.Res.FileUtil.DeleteFile(_tempDownLoadFilePath);
                            }
                            //
                            _fileStream = new FileStream(_tempDownLoadFilePath, FileMode.Create);

                        }
                        else
                        {
                            //
                            string sc = response.StatusCode.ToString();
                            if (sc.StartsWith("4") || sc.StartsWith("5"))
                            {
                                JW.Common.Log.LogE("HttpFileSession--->Error:" + sc);
                                _isError = true;
                            }
                        }
                    }
                    else
                    {
                        _isError = true;
                    }
                }
               
            }
            catch (System.Exception e)
            {
                isException = true;
                _isError = true;
            }
            finally
            {
                if (isException||_isError)
                {
                    JW.Common.Log.LogE("HttpFileSession Exception Or Error Finaly:" + Url);
                    _isError = true;
                }
            }
        }

        //更新获取数据
        public void Update()
        {
            //等待销毁
            if (IsOver)
            {
                return;
            }
            if (_isError)
            {
                Stop();
                _isOver = true;
            }

            if (_isOver)
            {
                Stop();
                if (_isError)
                {
                    if (Handler != null)
                    {
                        Handler(true, Id, true, 0.0f);
                    }
                    Handler = null;
                }
                else
                {
                    if (Handler != null)
                    {
                        Handler(false, Id, true, 1.0f);
                    }
                    Handler = null;
                }
                //等待销毁
                IsOver = true;
                return;
            }
            //若干时间，HttpWebRequest始终没有调用回调（如果回调成功调用，_response等三个对象不会为空），就结束当前下载任务。
            if (_response == null|| _responseStream == null|| _fileStream == null)
            {
                _waitedAsyn += 50;
                if (_waitedAsyn >= TimeOut * 3)
                {
                    Debug.Log("等待响应超时！");
                    _isError = true;
                    _isOver = true;
                }
                return;
            }
            //
            int readSize = 0;
            bool isException = false;
            try
            {
                readSize = _responseStream.Read(_buff, 0, BuffLen);
            }
            catch (Exception ex)
            {
                Log.LogE("HttpFileSession Read Exception :" + ex.Message);
                isException = true;
            }
            finally
            {
                if (isException)
                {
                    _isError = true;
                }
                else
                {
                    if (readSize > 0)
                    {
                        _fileStream.Write(_buff, 0, readSize);
                        //
                        _curDownloadLength += readSize;
                        float progress = (float)_curDownloadLength / _totalFileLength;
                        //JW.Common.Log.LogD("FileHttpSession:Downloaded:"+_curDownloadLength+":"+_totalFileLength);
                        if (Handler != null)
                        {
                            Handler(false, Id, false, progress);
                        }
                        //下载完成
                        if (_curDownloadLength >= _totalFileLength)
                        {
                            _isOver = true;
                            if (_fileStream != null)
                            {
                                _fileStream.Close();
                                _fileStream.Dispose();
                                _fileStream = null;
                                //移动文件
                                JW.Res.FileUtil.CopyFile(_tempDownLoadFilePath, DownloadFilePath, true);
                                //删除临时文件
                                JW.Res.FileUtil.DeleteFile(_tempDownLoadFilePath);
                            }
                        }
                    }
                    else
                    {
                        JW.Common.Log.LogD("FileHttpSession:Downloaded End");
                        ////下载完成
                        if (_curDownloadLength >= _totalFileLength)
                        {
                            _isOver = true;
                            if (_fileStream != null)
                            {
                                _fileStream.Close();
                                _fileStream = null;
                                //移动文件
                                JW.Res.FileUtil.CopyFile(_tempDownLoadFilePath, DownloadFilePath, true);
                                //删除临时文件
                                JW.Res.FileUtil.DeleteFile(_tempDownLoadFilePath);
                            }
                        }
                        else
                        {
                            _isOver = true;
                            _isError = true;
                        }
                    }
                }
            }
        }

        //结束
        public void Stop()
        {
            _isOver = true;
            if (_request != null)
            {
                _request.Abort();
                _request = null;
            }
            if (_responseStream != null)
            {
                _responseStream.Close();
                _responseStream.Dispose();
                _responseStream = null;
            }
            if (_response != null)
            {
                _response.Close();
                _response = null;
            }
            if (_fileStream != null)
            {
                _fileStream.Close();
                _fileStream.Dispose();
                _fileStream = null;
                //
                JW.Res.FileUtil.DeleteFile(_tempDownLoadFilePath);
            }
            _buff = null;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }

    }

    public class HttpService : MonoSingleton<HttpService>, IScheduleHandler
    {
        //当前分配ID
        private uint _currentId = 1;

        //网络事务列表
        private readonly JWArrayList<HttpFileSession> _httpSessions = new JWArrayList<HttpFileSession>(3);

        //临时下载目录
        public static string TempDir;

        private const int MaxParallelCnt = 10;

        void Start()
        {
            TempDir = JW.Res.FileUtil.CombinePath(JW.Res.FileUtil.GetCachePath(), "HttpTemp");
            if (JW.Res.FileUtil.IsDirectoryExist(TempDir))
            {
                JW.Res.FileUtil.ClearDirectory(TempDir);
            }
            else
            {
                JW.Res.FileUtil.CreateDirectory(TempDir);
            }

        }

        private void OnDestroy()
        {
            JW.Res.FileUtil.ClearDirectory(TempDir);
        }

        //ToDo 控制同时下载文件 数量
        public override bool Initialize()
        {
            this.AddTimer(50, true);
            return true;
        }

        public override void Uninitialize()
        {
            _currentId = 1;
            _httpSessions.Clear();
            StopAllCoroutines();
            this.RemoveTimer();
        }

        /// 注册下载文件
        public uint RegisteFileHttpDownload(string url, string filePath, HttpFileSessionDelegate handler = null)
        {
            if (string.IsNullOrEmpty(url) || (filePath == null))
            {
                JW.Common.Log.LogE("RegisteFileHttpDownload:Arg Error");
                return 0;
            }
            if ((!url.Contains("http://")) && (!url.Contains("https://")))
            {
                url = "http://" + url;
            }

            HttpFileSession hf = new HttpFileSession();
            hf.Url = url;
            hf.DownloadFilePath = filePath;
            hf.Id = _currentId++;
            hf.Handler = handler;
            if (hf != null && (hf.IsOver == false))
            {
                _httpSessions.Add(hf);
            }
            return hf.Id;
        }

        //反注册
        public void UnRegisteFileHttpDownload(uint sid)
        {
            for (int i = 0; i < _httpSessions.Count; i++)
            {
                HttpFileSession session = _httpSessions[i];
                if (session.Id == sid)
                {
                    session.Stop();
                    session.IsOver = true;
                    break;
                }
                _httpSessions[i] = session;
            }
            Clean();
        }

        void IScheduleHandler.OnScheduleHandle(ScheduleType type, uint id)
        {
            if (_httpSessions == null || _httpSessions.Count <= 0)
            {
                return;
            }
            //
            int workCnt = 0;
            //
            for (int i = 0; i < _httpSessions.Count; i++)
            {
                HttpFileSession ss = _httpSessions[i];
                if (ss != null)
                {
                    if (ss.IsBegined)
                    {
                        workCnt++;
                        ss.Update();
                    }
                    if (workCnt <= MaxParallelCnt)
                    {
                        if (ss.IsBegined == false)
                        {
                            ss.IsBegined = true;
                            ss.Begin();
                        }
                    }
                    else
                    {
                        //等待
                    }
                }
                _httpSessions[i] = ss;
            }
            //
            Clean();
        }

        private void Clean()
        {
            for (int i = _httpSessions.Count - 1; i >= 0; --i)
            {
                if (_httpSessions[i].IsOver)
                {
                    _httpSessions.RemoveAt(i);
                }
            }
        }

        //tolua获取网页基本文本
        public void AsyncGetText(string url, System.Action<string> callback)
        {
            if (string.IsNullOrEmpty(url))
            {
                if (callback != null)
                {
                    callback("error");
                }
            }
            StartCoroutine(HttpHelper.WWWRequest(url, callback));
        }

        //tolua 同步获取
        public string SyncGetText(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return "error";
            }
            return HttpHelper.SyncGetHttpResponse(url);
        }

        //TOLua 
        public void AsyncGetTextByPostUrl(string url, string postData, System.Action<string> callback)
        {
            if (string.IsNullOrEmpty(url))
            {
                if (callback != null)
                {
                    callback("error");
                }
                return;
            }
            StartCoroutine(HttpHelper.GetTextByPostUrl(url, postData, callback));
        }
    }

}


