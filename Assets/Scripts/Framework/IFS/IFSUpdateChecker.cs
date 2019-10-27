/********************************************************************
	created:	2018-8-20
	author:		jordenwu
	
	purpose:	IFS 文件系统检查器 负责检查本地文件列表和 网络文件列表 用来判断更新信息
*********************************************************************/
using JW.Common;
using JW.IFS;
using System.Collections;
using UnityEngine;

namespace JW.Framework.IFS
{
    public class IFSUpdateCheckerResult
    {
        //检查ID
        public string CheckName = string.Empty;
        //是否检查成功
        public bool IsSuccess = false;
        //是否有更新
        public bool IsHaveUpdate = false;
        //更新大小
        public int UpdateSize = 0;

    }

    public delegate void IFSUpdateCheckerDelegate(string checkName, IFSUpdateCheckerResult result);

    public class IFSUpdateChecker:MonoBehaviour
    {
        //事务名称
        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
        }

        //本地文件列表文件名称 必须
        private string _fileListFileName;
        //网络的文件列表地址 必须
        private string _netFileListFileUrl;
        //回调处理
        private IFSUpdateCheckerDelegate _handler;

        //本地文件列表信息
        private UFileInfoList _localFileList;
        //网络文件列表信息
        private UFileInfoList _netFileList;
        //Diff文件列表信息
        private UFileInfoList _diffFileList;

        public bool IsDone = false;

        //
        private bool _isLocalOk = true;
        private bool _isNetOk = true;
   

        public static IFSUpdateChecker Create(string sName, string localFileList, string netFileList, IFSUpdateCheckerDelegate handler)
        {
            IFSUpdateChecker ret = null;
            GameObject go = new GameObject("IFSUpdateChecker_"+sName); 
            ret = go.ExtAddComponent<IFSUpdateChecker>(true);
            ret.Init(sName, localFileList, netFileList, handler);
            return ret;
        }

        public static void Destory(IFSUpdateChecker cc)
        {
            if (cc != null)
            {
                cc.UnInit();
                cc.gameObject.ExtDestroy();
            }
        }
            

        private void Init(string sName,string localFileList,string netFileList, IFSUpdateCheckerDelegate handler)
        {
            _name = sName;
            _fileListFileName = localFileList;
            _netFileListFileUrl = netFileList;
            _handler = handler;
            IsDone = false;
            _isLocalOk = true;
            _isNetOk = true;
        }

        private void UnInit()
        {
            StopAllCoroutines();
            _handler = null;
            _localFileList = null;
            _netFileList = null;
            _diffFileList = null;
            IsDone = false;
        }

        public void StartCheck()
        {
            StartCoroutine(CheckUpdate());
        }
            
        public void StopCheck()
        {
            StopAllCoroutines();
            IsDone = true;
        }

        IEnumerator CheckUpdate()
        {
            yield return StartCoroutine(DoLocalFileListCheck());
            yield return StartCoroutine(DoDownloadNetFileList());
            yield return StartCoroutine(DoDiffNetFileList());

            if (_diffFileList != null)
            {
                if (_handler != null)
                {
                    IFSUpdateCheckerResult ret = new IFSUpdateCheckerResult();
                    ret.CheckName = this._name;
                    ret.UpdateSize = _diffFileList.GetAllFileSize();
                    ret.IsHaveUpdate = ret.UpdateSize > 0 ? true:false ;
                    //
                    ret.IsSuccess = _isNetOk && _isLocalOk;
                    _handler(this._name, ret);
                }
            }
            else
            {
                //失败
                if (_handler != null)
                {
                    IFSUpdateCheckerResult ret = new IFSUpdateCheckerResult();
                    ret.CheckName = this._name;
                    ret.UpdateSize = 0;
                    ret.IsHaveUpdate = false;
                    ret.IsSuccess = false;
                    _handler(this._name, ret);
                }
            }
            yield return null;
            IsDone = true;
        }

        //本地文件列表检查
        IEnumerator DoLocalFileListCheck()
        {
            string ifsDir = JW.Res.FileUtil.GetIFSExtractPath();
            //ToDO 二进制读取列表方式
            string flpath = JW.Res.FileUtil.CombinePath(ifsDir,this._fileListFileName);
            if (JW.Res.FileUtil.IsFileExist(flpath))
            {
                string localFlStr = System.Text.UTF8Encoding.UTF8.GetString(JW.Res.FileUtil.ReadFile(flpath));
                _localFileList = UnityEngine.JsonUtility.FromJson<UFileInfoList>(localFlStr);
                localFlStr = null;
                if (_localFileList == null)
                {
                    JW.Common.Log.LogE("IFSUpdateChecker Local File List File Error");
                    _isLocalOk = false;
                }
            }
            else
            {
                _localFileList = null;
                _isLocalOk = false;
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
                    if (!fileMd5.Equals(uf.MD5Num, System.StringComparison.CurrentCultureIgnoreCase))
                    {
                        JW.Common.Log.LogE("IFSUpdateChecker Local Res File Hacked:" + uf.FileName);
                        //设置00000 重新下载
                        uf.MD5Num = "000000";
                    }
                    if (i % 2 == 0)
                    {
                        yield return null;
                    }
                }
            }
            yield return null;
        }

        //下载网络文件列表
        IEnumerator DoDownloadNetFileList()
        {
            string netUrl =this._netFileListFileUrl;
            JW.Common.Log.LogD("IFSUpdateChecker DoDownloadNetFileList:" + netUrl);
            WWW www = new WWW(netUrl);
            while (www.isDone == false)
            {
                yield return null;
            }
            if (string.IsNullOrEmpty(www.error))
            {
                string netss = www.text;
                if (!string.IsNullOrEmpty(netss))
                {
                    JW.Common.Log.LogD("IFSUpdateChecker DoDownloadNetFileList Done:" + netss);
                    _netFileList = JsonUtility.FromJson<UFileInfoList>(netss);
                    if (_netFileList == null)
                    {
                        JW.Common.Log.LogE("IFSUpdateChecker Net File List File Error");
                        _isNetOk = false;
                    }
                    www.Dispose();
                    www = null;
                }
                else
                {
                    JW.Common.Log.LogE("IFSUpdateChecker DoDownloadNetFileList Error:" + www.error);
                    www.Dispose();
                    www = null;
                    _netFileList = null;
                    _isNetOk = false;
                }
            }
            else
            {
                JW.Common.Log.LogE("IFSUpdateChecker DoDownloadNetFileList Error:" + netUrl + ":" + www.error);
                www.Dispose();
                www = null;
                _netFileList = null;
                _isNetOk = false;
            }
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
                JW.Common.Log.LogD("<color=yellow>IFSUpdateChecker 需要更新文件列表</color>:" + UnityEngine.JsonUtility.ToJson(_diffFileList));
            }
            yield return null;
        }
    }
}

