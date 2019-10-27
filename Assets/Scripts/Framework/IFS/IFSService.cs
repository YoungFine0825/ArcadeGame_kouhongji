using System.Collections;
using System.IO;
using UnityEngine;
using JW.Common;
using JW.Res;
using JW.IFS;
using UnityEngine.Networking;
using JW.Framework.Http;
using FileUtil = JW.Res.FileUtil;
using System.Collections.Generic;

namespace JW.Framework.IFS
{
    /// <summary>
    /// IFS 文件系统状态
    /// </summary>
    public enum IFSState
    {
        //开始
        Start,
        //首次zip包移动状态
        FirstMoveInit,
        FirstMoving,
        FirstMoveSuccess,
        FirstMoveFailed,
        //首次zip包网络下载状态
        FirstDownloadInit,
        FirstDownloading,
        FirstDownloadSuccess,
        FirstDownloadFailed,
        //首次解压
        FirstUnZip,
        //本地文件列表初始化
        LocalFileListInit,
        //本地文件完整性检查
        LocalFileListCheck,
        //网络文件列表下载
        NetFileListDownload,
        //Diff文件列表
        LocalDiffNetFileList,
        //下载差异开始
        DownloadDiffFileListBegin,
        //Diff下载中
        DownloadingDiff,
        //Diff下载完成
        DownloadDiffSuccess,
        //生成最新文件列表
        GenerateLastFileList,
        //结束
        Over,
    }

    /// <summary>
    /// 网络文件服务 
    /// </summary>
    public class IFSService : MonoSingleton<IFSService>
    {

        private  JWArrayList<IFSUpdateChecker> _updateCheckers = new JWArrayList<IFSUpdateChecker>(3);
        private  JWArrayList<IFSSessionProcessor> _sessionProcessors = new JWArrayList<IFSSessionProcessor>(2);
        private  JWArrayList<IFSLocalChecker> _localCheckers = new JWArrayList<IFSLocalChecker>(3);

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public override bool Initialize()
        {
            return true;
        }

        /// <summary>
        /// 反初始化
        /// </summary>
        public override void Uninitialize()
        {

            if (_updateCheckers != null)
            {
                for(int i = 0; i < _updateCheckers.Count; i++)
                {
                    IFSUpdateChecker.Destory(_updateCheckers[i]);
                }
                _updateCheckers.Clear();
                _updateCheckers = null;
            }

            if (_sessionProcessors != null)
            {
                for (int i = 0; i < _sessionProcessors.Count; i++)
                {
                    IFSSessionProcessor.Destory(_sessionProcessors[i]);
                }
                _sessionProcessors.Clear();
                _sessionProcessors = null;
            }

            if (_localCheckers != null)
            {
                for (int i = 0; i < _localCheckers.Count; i++)
                {
                    IFSLocalChecker.Destory(_localCheckers[i]);
                }
                _localCheckers.Clear();
                _localCheckers = null;
            }
        }

        /// <summary>
        /// To Lua
        /// </summary>
        /// <param name="sessionName"></param>
        /// <param name="firstZipName"></param>
        /// <param name="fileListFileName"></param>
        /// <param name="firstZipUrl"></param>
        /// <param name="fileListFileUrl"></param>
        /// <param name="netFileRootUrl"></param>
        /// <param name="handler"></param>
        public void BeginSessionLua(string sessionName, string firstZipName, string fileListFileName, string firstZipUrl, string fileListFileUrl, string netFileRootUrl, IFSSessionDelegate handler)
        {
            if (string.IsNullOrEmpty(sessionName))
            {
                JW.Common.Log.LogE("IFS Service BeginSessionLua Error SessionName");
                return;
            }
            IFSSession ss = new IFSSession();
            ss.Name = sessionName;
            ss.FirstZipName = firstZipName;
            ss.FileListFileName = fileListFileName;
            ss.SessionHandler = handler;
            //
            ss.FirstZipURL = firstZipUrl;
            ss.FileListFileUrl = fileListFileUrl;
            ss.NetFileRootUrl = netFileRootUrl;
            //
            for (int i = _sessionProcessors.Count - 1; i >= 0; --i)
            {
                if (_sessionProcessors[i].Name.Equals(sessionName))
                {
                    JW.Common.Log.LogE("Repeat Session:" + sessionName);
                    return;
                }
            }

            IFSSessionProcessor pp = IFSSessionProcessor.Create(ss);
            if (pp != null)
            {
                pp.gameObject.transform.parent = this.transform;
                _sessionProcessors.Add(pp);
                pp.BeginSession();
            }

        }

        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="ss"></param>
        public void BeginSession(IFSSession ss)
        {
            if (ss==null)
            {
                JW.Common.Log.LogE("IFS Service BeginSession nil Session");
                return;
            }
            //
            for (int i = _sessionProcessors.Count - 1; i >= 0; --i)
            {
                if (_sessionProcessors[i].Name.Equals(ss.Name))
                {
                    JW.Common.Log.LogE("Repeat Session:" + ss.Name);
                    return;
                }
            }
            //
            IFSSessionProcessor pp = IFSSessionProcessor.Create(ss);
            if (pp != null)
            {
                pp.gameObject.transform.parent = this.transform;
                _sessionProcessors.Add(pp);
                pp.BeginSession();
            }
        }


        /// <summary>
        /// 检查是否有更新
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="localFileList"></param>
        /// <param name="netFileList"></param>
        /// <param name="handler"></param>
        public void BeginUpdateChecker(string sName, string localFileList, string netFileListUrl, IFSUpdateCheckerDelegate handler)
        {
            for (int i = _updateCheckers.Count - 1; i >= 0; --i)
            {
                if (_updateCheckers[i].Name.Equals(sName))
                {
                    JW.Common.Log.LogE("Repeat BeginUpdateChecker:" + sName);
                    return;
                }
            }
            IFSUpdateChecker newChecker = IFSUpdateChecker.Create(sName, localFileList, netFileListUrl, handler);
            if (newChecker != null)
            {
                newChecker.gameObject.transform.parent = this.transform;
                _updateCheckers.Add(newChecker);
                newChecker.StartCheck();
            }
            
        }

        /// <summary>
        /// 停止更新检查
        /// </summary>
        /// <param name="sName"></param>
        public void StopUpdateChecker(string sName)
        {
            IFSUpdateChecker cc = null;
            for (int i = _updateCheckers.Count - 1; i >= 0; --i)
            {
                if (_updateCheckers[i].Name.Equals(sName))
                {
                    _updateCheckers[i].StopCheck();
                    return;
                }
            }
        }

        /// <summary>
        /// 停止事务
        /// </summary>
        /// <param name="sessionName"></param>
        public void StopSession(string sessionName)
        {
            for (int i = _sessionProcessors.Count - 1; i >= 0; --i)
            {
                if (_sessionProcessors[i].Name.Equals(sessionName))
                {
                    _sessionProcessors[i].StopSession();
                    return;
                }
            }
        }


        /// <summary>
        /// 开始一个本地文件列表检查 完整性
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="localFileList"></param>
        /// <param name="handler"></param>
        public void BeginLocalChecker(string sName, string localFileList, IFSLocalCheckerDelegate handler)
        {
            for (int i = _localCheckers.Count - 1; i >= 0; --i)
            {
                if (_localCheckers[i].Name.Equals(sName))
                {
                    JW.Common.Log.LogE("Repeat BeginLocalChecker:" + sName);
                    return;
                }
            }
            IFSLocalChecker newChecker = IFSLocalChecker.Create(sName, localFileList,handler);
            if (newChecker != null)
            {
                newChecker.gameObject.transform.parent = this.transform;
                _localCheckers.Add(newChecker);
                newChecker.StartCheck();
            }
        }

        //
        public void StopLocalChecker(string sName)
        {
            for (int i = _localCheckers.Count - 1; i >= 0; --i)
            {
                if (_localCheckers[i].Name.Equals(sName))
                {
                    _localCheckers[i].StopCheck();
                    return;
                }
            }
        }

        public bool IsSessionBusy()
        {
            if (_sessionProcessors != null)
            {
                if (_sessionProcessors.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        protected void LateUpdate()
        {
            //清理
            if (_updateCheckers != null)
            {
                for (int i = _updateCheckers.Count - 1; i >= 0; --i)
                {
                    if (_updateCheckers[i].IsDone)
                    {
                        IFSUpdateChecker.Destory(_updateCheckers[i]);
                        _updateCheckers.RemoveAt(i);
                    }
                }
            }
            //
            if (_sessionProcessors != null)
            {
                for (int i = _sessionProcessors.Count - 1; i >= 0; --i)
                {
                    if (_sessionProcessors[i].IsDone)
                    {
                        IFSSessionProcessor.Destory(_sessionProcessors[i]);
                        _sessionProcessors.RemoveAt(i);
                    }
                }
            }

            if (_localCheckers != null)
            {
                for (int i = _localCheckers.Count - 1; i >= 0; --i)
                {
                    if (_localCheckers[i].IsDone)
                    {
                        IFSLocalChecker.Destory(_localCheckers[i]);
                        _localCheckers.RemoveAt(i);
                    }
                }
            }
        }
    }
}
