using System.Collections;
using UnityEngine;
using JW.Common;
using JW.Res;
using JW.IFS;
using JW.Framework.Http;
using FileUtil = JW.Res.FileUtil;
using System.Collections.Generic;


namespace JW.Framework.IFS
{
    /// <summary>
    /// 定义IFS 事务处理回调代理
    /// </summary>
    /// <param name="state"></param>
    public delegate void IFSSessionDelegate(string SessionName, int state, float progress,int errorCnt);

    /// <summary>
    /// 定义一个IFS 文件事务 
    /// </summary>
    public class IFSSession
    {
        //事务名称
        public string Name = "IFSSession";
        //首包名称带扩展名
        public string FirstZipName = "";
        //文件列表定义文件名称 必须
        public string FileListFileName;
        //首包UrL地址
        public string FirstZipURL = "";
        //网络的文件列表地址 必须
        public string FileListFileUrl;
        //网络文件库地址 必须
        public string NetFileRootUrl;
        //回调处理
        public IFSSessionDelegate SessionHandler;
    }

    /// <summary>
    /// 事务处理器
    /// </summary>
    public class IFSSessionProcessor : MonoBehaviour
    {

        public bool IsDone = false;

        public string Name
        {
            get
            {
                return _curSession.Name;
            }
        }

        //事务定义
        private IFSSession _curSession;
        //本地文件列表信息
        private UFileInfoList _localFileList;
        //网络文件列表信息
        private UFileInfoList _netFileList;
        //Diff文件列表信息
        private UFileInfoList _diffFileList;
        //Diff文件下载信息
        private JWObjDictionary<uint, string> _diffDownloadInfo;
        private IFSFileDownloader _cachedDownloader;

        private int _diffTotalCnt = 0;
        private int _diffDownloadedCnt = 0;

        private int _errorCnt = 0;

        public static IFSSessionProcessor Create(IFSSession session)
        {
            IFSSessionProcessor ret = null;
            GameObject go = new GameObject("IFSSessionProcessor_" + session.Name);
            ret = go.ExtAddComponent<IFSSessionProcessor>(true);
            ret.Init(session);
            return ret;
        }

        public static void Destory(IFSSessionProcessor cc)
        {
            if (cc != null)
            {
                cc.UnInit();
                cc.gameObject.ExtDestroy();
            }
        }

        public void Init(IFSSession ss)
        {
            _curSession = ss;
            IsDone = false;
        }

        public void UnInit()
        {
            StopAllCoroutines();
            //
            _curSession = null;
            _localFileList = null;
            _netFileList = null;
            _diffFileList = null;
            //移动解压终止
            if (_cachedDownloader != null)
            {
                _cachedDownloader.Dispose();
                _cachedDownloader = null;
            }
            //终止文件列表下载
            if (_diffDownloadInfo != null)
            {
                foreach (KeyValuePair<uint, string> kvp in _diffDownloadInfo)
                {
                    HttpService.GetInstance().UnRegisteFileHttpDownload(kvp.Key);
                }
                _diffDownloadInfo.Clear();
                _diffDownloadInfo = null;
            }
            _errorCnt = 0;
            IsDone = false;
        }


        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="ss"></param>
        public void BeginSession()
        {
            if (_curSession == null)
            {
                JW.Common.Log.LogE("IFSSessionProcessor BeginSession arg null");
            }
            if (_curSession == null)
            {
                JW.Common.Log.LogE("IFSSessionProcessor BeginSession arg null");
            }
            if (string.IsNullOrEmpty(_curSession.FileListFileUrl))
            {
                JW.Common.Log.LogE("IFSSessionProcessor BeginSession Error FileListFileUrl");
                return;
            }
            if (string.IsNullOrEmpty(_curSession.NetFileRootUrl))
            {
                JW.Common.Log.LogE("IFSSessionProcessor BeginSession Error NetFileRootUrl");
                return;
            }
            if (string.IsNullOrEmpty(_curSession.FileListFileName))
            {
                JW.Common.Log.LogE("IFSSessionProcessor BeginSession Error FileListFileName");
                return;
            }

            _diffDownloadedCnt = 0;
            _diffTotalCnt = 0;
            StartCoroutine(StartSession());
        }

      
        public void StopSession()
        {
            StopCoroutine("StartSession");
            //回收 
            if (_netFileList != null)
            {
                _netFileList = null;
            }
            if (_localFileList != null)
            {
                _localFileList = null;
            }
            if (_diffFileList != null)
            {
                _diffFileList = null;
            }

            //移动解压终止
            if (_cachedDownloader != null)
            {
                _cachedDownloader.Dispose();
                _cachedDownloader = null;
            }
            //终止文件列表下载
            if (_diffDownloadInfo != null)
            {
                foreach (KeyValuePair<uint, string> kvp in _diffDownloadInfo)
                {
                    HttpService.GetInstance().UnRegisteFileHttpDownload(kvp.Key);
                }
                _diffDownloadInfo.Clear();
                _diffDownloadInfo = null;
            }

            if (_curSession == null)
            {
                JW.Common.Log.LogE("IFS Service Stop No Run");
                return;
            }
            else
            {
                _curSession.SessionHandler = null;
                //删除标记文件
                string ifsDir = JW.Res.FileUtil.GetIFSExtractPath();
                string localPath = JW.Res.FileUtil.CombinePath(ifsDir, _curSession.FileListFileName);
                JW.Res.FileUtil.DeleteFile(localPath);
                _curSession = null;
            }
            //
            IsDone = true;
        }

        private void CallSessionHandler(IFSState st, float progress = 0)
        {
            if (_curSession != null)
            {
                if (_curSession.SessionHandler != null)
                {
                    _curSession.SessionHandler(_curSession.Name, (int)st, progress,_errorCnt);
                }
            }
        }

        IEnumerator StartSession()
        {
            CallSessionHandler(IFSState.Start, 0);
            yield return null;
            //---------首次下载或者移动----------------
            yield return StartCoroutine(DoFirstMoveOrDownload());
            //
            yield return new WaitForEndOfFrame();
            //--------------本地文件检测--------------
            CallSessionHandler(IFSState.LocalFileListInit, 0);
            yield return StartCoroutine(DoLocalFileListCheck());
            //-----------获取网络文件列表
            CallSessionHandler(IFSState.NetFileListDownload, 0);
            yield return StartCoroutine(DoDownloadNetFileList());
            //------生成差异-------------
            CallSessionHandler(IFSState.LocalDiffNetFileList, 0);
            yield return StartCoroutine(DoDiffNetFileList());
            //下载更新文件
            CallSessionHandler(IFSState.DownloadDiffFileListBegin, 0);
            yield return StartCoroutine(DoDownloadDiffFileList());
            //
            yield return new WaitForEndOfFrame();
            //写最新的服务器文件列表到本地
            CallSessionHandler(IFSState.GenerateLastFileList, 1.0f);
            yield return StartCoroutine(DoWriteSvrFileListToLocal());
            //GC 下
            System.GC.Collect();
            UnityEngine.Resources.UnloadUnusedAssets();
            yield return null;
            //Over
            CallSessionHandler(IFSState.Over, 1.0f);
            //
            _curSession.SessionHandler = null;
            _curSession = null;
            yield return null;
            IsDone = true;
        }

        //首次移动或者下载
        IEnumerator DoFirstMoveOrDownload()
        {
            bool isNeedMove = false;
            if (!JW.Res.FileUtil.IsExistInIFSExtraFolder(_curSession.FileListFileName))
            {
                isNeedMove = true;
            }
            //--------------------移动或者下载--------
            if (isNeedMove)
            {
                string filePath = "";
                bool isDownload = false;
                //存在
                if (JW.Res.FileUtil.IsFileExistInStreamingAssets(_curSession.FirstZipName))
                {
                    CallSessionHandler(IFSState.FirstMoveInit, 0);
                    filePath = JW.Res.FileUtil.GetStreamingAssetsPathWithHeader(_curSession.FirstZipName);
                }
                else
                {
                    isDownload = true;
                    filePath = _curSession.FirstZipURL;
                    CallSessionHandler(IFSState.FirstDownloadInit, 0);
                }
                JW.Common.Log.LogD("DoFirstMoveOrDownload：" + filePath);
                yield return null;
                if (!string.IsNullOrEmpty(filePath))
                {
                    string ifsDirPath = JW.Res.FileUtil.GetIFSExtractPath();
                    if (!JW.Res.FileUtil.IsDirectoryExist(ifsDirPath))
                    {
                        JW.Common.Log.LogD("Create IFS Dir " + ifsDirPath);
                        JW.Res.FileUtil.CreateDirectory(ifsDirPath);
                    }
                    //非android 直接解压
                    if (false)//isDownload==false && (Application.platform== RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.WindowsPlayer|| Application.platform == RuntimePlatform.WindowsEditor))
                    {
                        CallSessionHandler(IFSState.FirstUnZip, 0f);
                        string pp = JW.Res.FileUtil.GetStreamingAssetsPath(_curSession.FirstZipName);
                        IFSArchiver.SyncUnarchiveIFSFile(pp, ifsDirPath, false);
                        CallSessionHandler(IFSState.FirstUnZip, 1f);
                    }
                    else
                    {
                        //边下载边解档 
                        IFSFileDownloader www = new IFSFileDownloader(filePath, ifsDirPath);
                        _cachedDownloader = www;
                        //
                        CallSessionHandler(isDownload ? IFSState.FirstDownloading : IFSState.FirstMoving, 0);
                        //
                        www.Begin();
                        while (www.IsDone == false)
                        {
                            www.Update();
                            CallSessionHandler(isDownload ? IFSState.FirstDownloading : IFSState.FirstMoving, www.Progress);
                            yield return null;
                        }
                        if (www.IsError == false)
                        {
                            CallSessionHandler(isDownload ? IFSState.FirstDownloading : IFSState.FirstMoving, 1.0f);
                            yield return null;
                            CallSessionHandler(IFSState.FirstUnZip, 0f);
                            yield return null;
                            CallSessionHandler(IFSState.FirstUnZip, 1.0f);
                        }
                        else
                        {
                            JW.Common.Log.LogE("IFSService DoFirstMoveOrDownload Error");
                            _errorCnt++;
                            CallSessionHandler(isDownload ? IFSState.FirstDownloadFailed : IFSState.FirstMoveFailed, 1.0f);
                        }
                        www.Dispose();
                        www = null;
                        _cachedDownloader = null;
                    }
                }
                else
                {
                    JW.Common.Log.LogE("IFSService DoFirstMoveOrDownload Error FilePATH");
                    CallSessionHandler(isDownload ? IFSState.FirstDownloadFailed : IFSState.FirstMoveFailed, 1.0f);
                }
            }
            else
            {
                JW.Common.Log.LogD("No Need Move!");
            }
            yield return null;
        }


        //本地文件列表检查
        IEnumerator DoLocalFileListCheck()
        {
            string ifsDir = JW.Res.FileUtil.GetIFSExtractPath();
            //ToDO 二进制读取列表方式
            string flpath = JW.Res.FileUtil.CombinePath(ifsDir, _curSession.FileListFileName);
            if (JW.Res.FileUtil.IsFileExist(flpath))
            {
                string localFlStr = System.Text.UTF8Encoding.UTF8.GetString(JW.Res.FileUtil.ReadFile(flpath));
                _localFileList = UnityEngine.JsonUtility.FromJson<UFileInfoList>(localFlStr);
                localFlStr = null;
            }
            else
            {
                _errorCnt++;
                _localFileList = null;
            }
            yield return null;

            if (_localFileList != null)
            {
                int totalCnt = _localFileList.FileList.Count;
                float totalCnt2 = (float)totalCnt;
                for (int i = 0; i < totalCnt; i++)
                {
                    UFileInfo uf = _localFileList.FileList[i];
                    string filePath = JW.Res.FileUtil.CombinePath(ifsDir, uf.FileName);
                    string fileMd5 = JW.Res.FileUtil.GetFileMd5ByFileStream(filePath);
                    if (!fileMd5.Equals(uf.MD5Num))
                    {
                        JW.Common.Log.LogE("Local Res File Hacked:" + uf.FileName);
                        //设置00000 重新下载
                        uf.MD5Num = "000000";
                    }

                    if (i % 2 == 0)
                    {
                        CallSessionHandler(IFSState.LocalFileListInit, i / totalCnt2);
                        yield return null;
                    }
                }
            }
            CallSessionHandler(IFSState.LocalFileListInit, 1.0f);
            yield return null;
        }

        //下载网络文件列表
        IEnumerator DoDownloadNetFileList()
        {
            string netUrl = _curSession.FileListFileUrl;
            JW.Common.Log.LogD("DoDownloadNetFileList:" + netUrl);
            WWW www = new WWW(netUrl);
            while (www.isDone == false)
            {
                CallSessionHandler(IFSState.NetFileListDownload, www.progress);
                yield return null;
            }
            if (string.IsNullOrEmpty(www.error))
            {
                string netss = www.text;
                if (!string.IsNullOrEmpty(netss))
                {
                    JW.Common.Log.LogD("DoDownloadNetFileList Done:" + netss);
                    _netFileList = JsonUtility.FromJson<UFileInfoList>(netss);
                    www.Dispose();
                    www = null;
                }
                else
                {
                    _errorCnt++;
                    JW.Common.Log.LogE("DoDownloadNetFileList Error:" + www.error);
                    www.Dispose();
                    www = null;
                    _netFileList = null;
                }
            }
            else
            {
                _errorCnt++;
                JW.Common.Log.LogE("DoDownloadNetFileList Error:" + netUrl + ":" + www.error);
                www.Dispose();
                www = null;
                _netFileList = null;
            }
            CallSessionHandler(IFSState.NetFileListDownload, 1.0f);
            yield return null;
        }

        //差异生成
        IEnumerator DoDiffNetFileList()
        {
            if (_netFileList != null)
            {
                if (_localFileList != null)
                {
                    _diffFileList = _localFileList.DiffWithOther(ref _netFileList);
                }
                else
                {
                    _diffFileList = _netFileList;
                }
                JW.Common.Log.LogD("<color=yellow>需要更新文件列表</color>:" + UnityEngine.JsonUtility.ToJson(_diffFileList));
            }
            yield return null;
        }


        /// 下载更新文件 ToDO优化
        IEnumerator DoDownloadDiffFileList()
        {
            if ((_diffFileList != null) && (_diffFileList.FileList.Count > 0))
            {
                _diffTotalCnt = _diffFileList.FileList.Count;
                _diffDownloadedCnt = 0;

                string ifsDir = JW.Res.FileUtil.GetIFSExtractPath();
                for (int i = 0; i < _diffFileList.FileList.Count; i++)
                {
                    string url = _curSession.NetFileRootUrl + _diffFileList.FileList[i].FileName;
                    string localPath = JW.Res.FileUtil.CombinePath(ifsDir, _diffFileList.FileList[i].FileName);
                    uint sid = HttpService.GetInstance().RegisteFileHttpDownload(url, localPath, OnDiffFileDownloaded);
                    if (_diffDownloadInfo == null)
                    {
                        _diffDownloadInfo = new JWObjDictionary<uint, string>(_diffFileList.FileList.Count);
                    }
                    else
                    {
                        _diffDownloadInfo.Clear();
                    }
                    _diffDownloadInfo.Add(sid, _diffFileList.FileList[i].FileName);
                }
                //
                while (_diffDownloadInfo.Count > 0)
                {
                    CallSessionHandler(IFSState.DownloadingDiff, (float)_diffDownloadedCnt / _diffTotalCnt);
                    yield return null;
                }
                //
                CallSessionHandler(IFSState.DownloadDiffSuccess, 1.0f);
            }
            else
            {
                CallSessionHandler(IFSState.DownloadDiffSuccess, 1.0f);
            }
            //
            _diffDownloadInfo = null;
            yield return null;
        }

        private void OnDiffFileDownloaded(bool isError, uint sessionId, bool isEnd, float progress)
        {
            if (isError)
            {
                JW.Common.Log.LogE("IFS OnDiffFileDownloaded Error:"+sessionId.ToString());
                _errorCnt++;
                //目前都成功计数
                if (_diffDownloadInfo != null)
                {
                    _diffDownloadInfo.Remove(sessionId);
                }
                _diffDownloadedCnt++;
            }
            else
            {
                if (_diffDownloadInfo != null && isEnd)
                {
                    _diffDownloadInfo.Remove(sessionId);
                    _diffDownloadedCnt++;
                }
            }

        }

        //写入最新文件列表
        IEnumerator DoWriteSvrFileListToLocal()
        {
            if (_netFileList != null)
            {
                string ss = JsonUtility.ToJson(_netFileList);
                string ifsDir = JW.Res.FileUtil.GetIFSExtractPath();
                string localPath = JW.Res.FileUtil.CombinePath(ifsDir, _curSession.FileListFileName);
                JW.Res.FileUtil.WriteFile(localPath, System.Text.UTF8Encoding.UTF8.GetBytes(ss));
            }
            yield return null;
        }
    }
}
