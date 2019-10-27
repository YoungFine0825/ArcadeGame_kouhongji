/********************************************************************
	created:	2018-8-20
	author:		jordenwu
	
	purpose:	IFS 文件系统检查器 负责检查本地文件列表完整性
*********************************************************************/
using JW.Common;
using JW.IFS;
using System.Collections;
using UnityEngine;

namespace JW.Framework.IFS
{
    public class IFSLocalCheckerResult
    {
        //检查ID
        public string CheckName = string.Empty;
        //是否检查成功
        public bool IsSuccess = false;
        //是否本地完整
        public bool LocalIsFull = false;
    }

    public delegate void IFSLocalCheckerDelegate(string checkName, IFSLocalCheckerResult result);

    public class IFSLocalChecker : MonoBehaviour
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
        //回调处理
        private IFSLocalCheckerDelegate _handler;

        //本地文件列表信息
        private UFileInfoList _localFileList;
        public bool IsDone = false;
        //
        private bool _isLocalOk = true;
        private int _localHackedCnt = 0;

        public static IFSLocalChecker Create(string sName, string localFileList,IFSLocalCheckerDelegate handler)
        {
            IFSLocalChecker ret = null;
            GameObject go = new GameObject("IFSLocalChecker" + sName);
            ret = go.ExtAddComponent<IFSLocalChecker>(true);
            ret.Init(sName, localFileList, handler);
            return ret;
        }

        public static void Destory(IFSLocalChecker cc)
        {
            if (cc != null)
            {
                cc.UnInit();
                cc.gameObject.ExtDestroy();
            }
        }


        private void Init(string sName, string localFileList, IFSLocalCheckerDelegate handler)
        {
            _name = sName;
            _fileListFileName = localFileList;
            _handler = handler;
            IsDone = false;
            _isLocalOk = true;
            _localHackedCnt = 0;
        }

        private void UnInit()
        {
            StopAllCoroutines();
            _handler = null;
            _localFileList = null;
            IsDone = false;
        }

        public void StartCheck()
        {
            StartCoroutine(CheckLocal());
        }

        public void StopCheck()
        {
            StopAllCoroutines();
            IsDone = true;
        }

        IEnumerator CheckLocal()
        {
            yield return StartCoroutine(DoLocalFileListCheck());

            if (_isLocalOk)
            {
                if (_handler != null)
                {
                    IFSLocalCheckerResult ret = new IFSLocalCheckerResult();
                    ret.CheckName = this._name;
                    ret.IsSuccess = true;
                    ret.LocalIsFull = _localHackedCnt>0?false:true;
                    _handler(this._name, ret);
                }
            }
            else
            {
                //失败
                if (_handler != null)
                {
                    IFSLocalCheckerResult ret = new IFSLocalCheckerResult();
                    ret.CheckName = this._name;
                    ret.IsSuccess = false;
                    ret.LocalIsFull = false;
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
            string flpath = JW.Res.FileUtil.CombinePath(ifsDir, this._fileListFileName);
            if (JW.Res.FileUtil.IsFileExist(flpath))
            {
                string localFlStr = System.Text.UTF8Encoding.UTF8.GetString(JW.Res.FileUtil.ReadFile(flpath));
                _localFileList = UnityEngine.JsonUtility.FromJson<UFileInfoList>(localFlStr);
                if (_localFileList == null)
                {
                    JW.Common.Log.LogE("IFSLocalChecker Local File List Error");
                    _isLocalOk = false;
                }
                localFlStr = null;
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
                for (int i = 0; i < totalCnt; i++)
                {
                    UFileInfo uf = _localFileList.FileList[i];
                    string filePath = JW.Res.FileUtil.CombinePath(ifsDir, uf.FileName);
                    string fileMd5 = JW.Res.FileUtil.GetFileMd5ByFileStream(filePath);
                    if (!fileMd5.Equals(uf.MD5Num, System.StringComparison.CurrentCultureIgnoreCase))
                    {
                        JW.Common.Log.LogE("IFSLocalChecker Local Res File Hacked:" + uf.FileName);
                        _localHackedCnt += _localHackedCnt + 1;
                        //一个就Ok了
                        break;
                    }
                    yield return null;
                }
            }
            yield return null;
        }
    }
}

