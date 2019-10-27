/********************************************************************
	created:	2018-06-05   
	filename: 	UpdateState
	author:		jordenwu
	
	purpose:	更新状态
                1、第一次游戏准备
                2、数据校验和更新
*********************************************************************/
using JW.Common;
using JW.Framework.State;
using JW.Framework.MVC;
using JW.Framework.Event;
using JW.Framework.IFS;
using JW.Framework.Schedule;
using JW.Framework.Http;
using System.Collections.Generic;
using UnityEngine;
using JW.Res;
using System.Text;
using JW.Framework.Asset;
using JW.Lua;
using JW.Framework.Native;
using System;
using JW.Framework.UGUI;
using ICSharpCode.SharpZipLib;

public class UpdateState : IState, IScheduleHandler
{   
    //参数
    private EventDeclare.UpdateStateChangeEventArg _stateArg;
    private uint _netCheckSId = 0;
    private bool _isNetCheck = false;
    private int _netCheckCnt = 10;
    //版本检查
    private bool _isForceCheck = false;
    private uint _versionCheckSId = 0;
    private int _versionCheckCnt = 10;
    //资源检查
    private int _localCheckFailCnt = 0;
    private int _updateFailedCnt = 0;
    //
    private UnZipperMono _unzipper = null;

    public void InitializeState()
    {
        _stateArg= new EventDeclare.UpdateStateChangeEventArg();
    }

    public void UninitializeState()
    {

    }

    public string Name()
    {
        return "Update";
    }

    public void OnStateOverride()
    {
    }

    public void OnStateResume()
    {

    }

    public void OnStateEnter(object usrData = null)
    {
        EventService.GetInstance().AddEventHandler((uint)EventId.ApplicationPause, this, "OnApplicationPause");
        //界面
        UIStateService.GetInstance().ChangeState("UIUpdate");
        _isForceCheck = false;
        _isNetCheck = false;
        _netCheckCnt = 10;
        _versionCheckCnt = 10;
#if UNITY_EDITOR
        DoNetworkCheck();
#else
        //检查用于强制更新的 程序
        DoUpdaterCheck();
#endif
    }

    public void OnStateLeave()
    {
        JW.Common.Log.LogD("Leave Update GameState");
        EventService.GetInstance().RemoveEventHandler(this);
    }

    /// <summary>
    /// 更新程序EXE 检查
    /// </summary>
    private void DoUpdaterCheck()
    {
        _stateArg.Progress = 0f;
        _stateArg.StateInfo = "...检查更新程序...";
        EventService.GetInstance().SendEvent((uint)EventId.UpdateStateChange, _stateArg);
        //建立更新程序文件夹
        string updaterDir = FileUtil.CombinePath(FileUtil.GetExeRootPath(), "ArcadeUpdater");
        if (!FileUtil.IsDirectoryExist(updaterDir))
        {
            FileUtil.CreateDirectory(updaterDir);
        }
        //验证更新器是否存在
        string updaterExePath = FileUtil.CombinePath(updaterDir, "ArcadeUpdater.exe");
        string updaterVersionPath = FileUtil.CombinePath(updaterDir, "ArcadeUpdaterVersion.bytes");
        //
        string updaterCurVersion = "000";
        //不存在一个 就清理目录
        if (!FileUtil.IsFileExist(updaterExePath) || !FileUtil.IsFileExist(updaterVersionPath))
        {
            FileUtil.ClearDirectory(updaterDir);
            //
            updaterCurVersion = "000";
        }
        else
        {
            string rr = System.Text.Encoding.UTF8.GetString(FileUtil.ReadFile(updaterVersionPath));
            if (string.IsNullOrEmpty(rr))
            {
                rr = "000";
            }
            updaterCurVersion= rr.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
        }
        //
        string newVerisonPath = FileUtil.GetStreamingAssetsPath("ArcadeUpdaterVersion.bytes");
        string newVersion= System.Text.Encoding.UTF8.GetString(FileUtil.ReadFile(newVerisonPath));
        if (string.IsNullOrEmpty(newVersion))
        {
            newVersion = "fuck";
        }
        newVersion = newVersion.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
        //
        if (string.Equals(newVersion, updaterCurVersion))
        {
            //不需要更新更新程序
            DoNetworkCheck();
        }
        else
        {
            //需要更新更新程序
            string newArcadeZip = FileUtil.GetStreamingAssetsPath("ArcadeUpdater.zip");
            if (!FileUtil.IsFileExist(newArcadeZip))
            {
                Log.LogE("Fuck No ArcadeUpdater.zip");
                //
                DoNetworkCheck();
                return;
            }
            //
            _stateArg.Progress = 0f;
            _stateArg.StateInfo = "...解压更新程序...";
            EventService.GetInstance().SendEvent((uint)EventId.UpdateStateChange, _stateArg);
            //
            _unzipper = UnZipperMono.GetUnZipper(newArcadeZip, updaterDir, this.OnUpdaterUnZip);
            _unzipper.Begin();
        }
    }

    private void OnUpdaterUnZip(bool isDone,float progress)
    {
        if (isDone)
        {
            _stateArg.StateInfo = "...解压更新程序完成...";
            EventService.GetInstance().SendEvent((uint)EventId.UpdateStateChange, _stateArg);
            _unzipper.Stop();
            _unzipper = null;
            //清理掉老的AutoUpdate.exe
            string oldp = FileUtil.CombinePath(FileUtil.GetExeRootPath(), "AutoUpdate.exe");
            FileUtil.DeleteFile(oldp);
            //
            string oldp2 = FileUtil.CombinePath(FileUtil.GetExeRootPath(), "AutoUpdate.exe.manifest");
            FileUtil.DeleteFile(oldp2);
            //---开始网络检查
            DoNetworkCheck();
        }
        else
        {
            _stateArg.Progress = progress;
            _stateArg.StateInfo = "...正在解压更新程序...";
            EventService.GetInstance().SendEvent((uint)EventId.UpdateStateChange, _stateArg);
        }
    }


    protected void OnApplicationPause(EventArg arg)
    {
        EventDeclare.ApplicationPauseArg aa = (EventDeclare.ApplicationPauseArg)arg;
        if (aa.IsPause)
        {

        }
        else
        {
            //回来
            if (_isNetCheck)
            {
                DoNetworkCheck();
            }
            else
            {
                if (_isForceCheck)
                {
                    DoForceVersionCheck();
                }
            }
        }
    }

    //网络检查
    private void DoNetworkCheck()
    {
        if (NativeService.GetInstance().GetIsNetEnable())
        {
            JW.Common.Log.LogD("Net OK");
            _isNetCheck = false;
            ScheduleService.GetInstance().RemoveFrame(this);
            //版本检查
#if UNITY_EDITOR
            DoMainResCheck();
#else
            DoForceVersionCheck();
#endif
        }
        else
        {
            JW.Common.Log.LogD("Net Not Ok");
            if (_isNetCheck==false)
            {
                _isNetCheck = true;
                _stateArg.Progress = 0f;
                _stateArg.StateInfo = "...等待检查网络...";
                JW.Framework.UGUI.UICommonService.GetInstance().ShowBubble("当前无网络等待!");
                EventService.GetInstance().SendEvent((uint)EventId.UpdateStateChange, _stateArg);
            }
            _netCheckCnt--;
            if (_netCheckCnt <= 0)
            {
                _stateArg.Progress = 0f;
                _stateArg.StateInfo = "...网络检查失败即将重启设备...";
                EventService.GetInstance().SendEvent((uint)EventId.UpdateStateChange, _stateArg);
                ScheduleService.GetInstance().RemoveFrame(this);
                ScheduleService.GetInstance().AddTimer(2000,false,this);
            }
            else
            {
                ScheduleService.GetInstance().RemoveFrame(this);
                _netCheckSId = ScheduleService.GetInstance().AddFrame(60, false, this);
            }
        }
    }

    /// 强更信息检查
    private void DoForceVersionCheck()
    {
        _isForceCheck = true;
        _stateArg.StateInfo = "系统版本检查...";
        _stateArg.Progress = 0f;
        EventService.GetInstance().SendEvent((uint)EventId.UpdateStateChange, _stateArg);
        //
        _versionCheckCnt--;
        if (_versionCheckCnt < 0)
        {
            _stateArg.Progress = 0f;
            _stateArg.StateInfo = "...系统版本检查失败,即将重启设备...";
            EventService.GetInstance().SendEvent((uint)EventId.UpdateStateChange, _stateArg);
            ScheduleService.GetInstance().RemoveFrame(this);
            ScheduleService.GetInstance().AddTimer(2000, false, this);
            return;
        }
        //同步获取游戏主版本是否需要更新
        string reqF = string.Empty;
#if JW_DEBUG
        reqF = "https://gamehalldownloadcdn.scbczx.com/Arcade/EXE/Debug/Version.bytes";
#else
        reqF = "https://gamehalldownloadcdn.scbczx.com/Arcade/EXE/Release/Version.bytes";
#endif
        JW.Common.Log.LogD("Force Version Check:"+reqF);
        //同步获取
        string ret = HttpService.GetInstance().SyncGetText(reqF);
        if (string.IsNullOrEmpty(ret) || ret.Equals("error"))
        {
            //获取主版本信息
            JW.Common.Log.LogE("Force version check: Error");
            _stateArg.StateInfo = "...获取系统版本信息失败等待重试...";
            _stateArg.Progress = 0f;
            EventService.GetInstance().SendEvent((uint)EventId.UpdateStateChange, _stateArg);
            ScheduleService.GetInstance().RemoveFrame(this);
            _versionCheckSId = ScheduleService.GetInstance().AddFrame(60, false, this);
        }
        else
        {
            Log.LogD("Force Version Ret:" + ret);
            DoForceVersionCheckSession(ret);
        }
    }

    private void DoForceVersionCheckSession(string netVersion)
    {
        Log.LogD("------------>DoForceVersionCheckSession<----------");
        netVersion= netVersion.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
        string codeV = VersionInfo.GetCodeVersion();
        Log.LogD("-------------->App Code Version:" + codeV);
        if (!string.Equals(codeV, netVersion))
        {
            //需要更新EXE
            _stateArg.StateInfo = "...即将开始系统升级...";
            _stateArg.Progress = 0f;
            EventService.GetInstance().SendEvent((uint)EventId.UpdateStateChange, _stateArg);
            string exeUrl = string.Empty;
#if JW_DEBUG
            exeUrl = "https://gamehalldownloadcdn.scbczx.com/Arcade/EXE/Debug/"+"Debug_Arcade_" + netVersion+".zip";
#else
            exeUrl = "https://gamehalldownloadcdn.scbczx.com/Arcade/EXE/Release/"+"Release_Arcade_" + netVersion + ".zip";
#endif
            string extractDir = FileUtil.GetExeRootPath();
            string updaterExePath = FileUtil.CombinePath(FileUtil.GetExeRootPath(), "ArcadeUpdater/ArcadeUpdater.exe");
            string args =exeUrl + " " + extractDir;
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
            process.StartInfo.FileName = updaterExePath;
            process.StartInfo.Arguments =args;
            process.Start();
#if !UNITY_EDITOR
            Application.Quit();
#endif
        }
        else
        {
            DoMainResCheck();
        }
    }

    //检查是否是首次安装
    private void CheckIsFirstInstall()
    {
        string appCodeV = VersionInfo.GetCodeVersion();
        string recordV = PlayerPrefs.GetString("APP_Version");
        if (!string.Equals(appCodeV,recordV))
        {
            //首次
            JW.Common.Log.LogD("-------------First Install App-----------");
            //清理掉IFS目录
            JW.Res.FileUtil.ClearDirectory(JW.Res.FileUtil.GetIFSExtractPath());
            //写入
            PlayerPrefs.SetString("APP_Version", appCodeV);
        }
    }

    //主资源更新检查
    private void DoMainResCheck()
    {
        _isForceCheck = false;
        //
        CheckIsFirstInstall();

        JW.Common.Log.LogD("<color=yellow>游戏主资源检查</color>");
        string defaultMainResUrlRoot=string.Empty;
#if JW_DEBUG
        defaultMainResUrlRoot = "https://gamehalldownloadcdn.scbczx.com/Arcade/IFS/Debug/";
#else
        defaultMainResUrlRoot = "https://gamehalldownloadcdn.scbczx.com/Arcade/IFS/Release/";
#endif

#if USE_PACK_RES
        //
        IFSSession mainResSession = new IFSSession();
        mainResSession.Name = "MainResIFS"+_updateFailedCnt.ToString();
        mainResSession.FirstZipName = "MainZip.bytes";
        mainResSession.FileListFileName = "MainFileList.json";
        mainResSession.SessionHandler = OnIFSSessionCallback;
        string mainResUrlRoot = defaultMainResUrlRoot;
        //
        mainResSession.FirstZipURL = string.Format("{0}{1}", mainResUrlRoot, "MainZip.bytes");
        mainResSession.FileListFileUrl = string.Format("{0}{1}", mainResUrlRoot, "MainFileList.json");
        mainResSession.NetFileRootUrl = mainResUrlRoot;
        IFSService.GetInstance().BeginSession(mainResSession);
#else
        DoReadyEnterGame();
#endif
    }

    //主资源IFS回调
    private void OnIFSSessionCallback(string name,int st,float progress,int errorCnt)
    {
        _stateArg.StateInfo = GetIFSStateTipText((IFSState)st);
        _stateArg.Progress = progress;
        //界面显示
        EventService.GetInstance().SendEvent((uint)EventId.UpdateStateChange, _stateArg);
        //结束
        if (st == (int)IFSState.Over)
        {
            if (errorCnt > 0)
            {
                JW.Common.Log.LogE("MainRes Update Failed "+errorCnt.ToString());
                _updateFailedCnt++;
                if (_updateFailedCnt > 10)
                {
                    _stateArg.Progress = 0.0f;
                    _stateArg.StateInfo = "...资源更新重试失败!等待重启 ...";
                    EventService.GetInstance().SendEvent((uint)EventId.UpdateStateChange, _stateArg);
                    ScheduleService.GetInstance().AddTimer(2000, false, this);
                    return;
                }
                _stateArg.Progress = 0.0f;
                _stateArg.StateInfo = "...资源更新失败开始重试...";
                EventService.GetInstance().SendEvent((uint)EventId.UpdateStateChange, _stateArg);
                //
                DoMainResCheck();
                //
            }
            else
            {
                _updateFailedCnt = 0;
#if USE_PACK_RES
                DoLocalResFullCheck();
#else
                JW.Common.Log.LogD("---DoReadyEnterGame-----");
                DoReadyEnterGame();
#endif
            }
        }
    }

    //资源完整性检查
    private void DoLocalResFullCheck()
    {
        _stateArg.Progress = 0.0f;
        _stateArg.StateInfo = "...开始资源完整性检查...";
        EventService.GetInstance().SendEvent((uint)EventId.UpdateStateChange, _stateArg);

        IFSService.GetInstance().BeginLocalChecker("MainResCheck", "MainFileList.json", delegate (string checkName, IFSLocalCheckerResult result)
        {
            if (result.IsSuccess&&result.LocalIsFull)
            {
                JW.Common.Log.LogD("---DoReadyEnterGame-----");
                DoReadyEnterGame();
            }
            else
            {
                _localCheckFailCnt++;
                if (_localCheckFailCnt > 10)
                {
                    _stateArg.Progress = 0.0f;
                    _stateArg.StateInfo = "...资源完整性检查失败!等待重启 ...";
                    EventService.GetInstance().SendEvent((uint)EventId.UpdateStateChange, _stateArg);
                    ScheduleService.GetInstance().AddTimer(2000, false, this);
                    return;
                }
                //
                _stateArg.Progress = 0.0f;
                _stateArg.StateInfo = "...资源完整性检查失败!重新更新 ...";
                EventService.GetInstance().SendEvent((uint)EventId.UpdateStateChange, _stateArg);
                //
                DoMainResCheck();
            }
        });
    }

    //
    private string GetIFSStateTipText(IFSState st)
    {
        string tip = "";
        switch (st)
        {
            //开始
            case IFSState.Start:
                tip= "开始游戏资源检查!";
                break;
            case IFSState.FirstMoveInit:
            case IFSState.FirstMoving:
            case IFSState.FirstMoveSuccess:
            case IFSState.FirstMoveFailed:
                tip = "解压游戏资源,不消耗流量!";
                break;
            case IFSState.FirstDownloadInit:
            case IFSState.FirstDownloading:
            case IFSState.FirstDownloadSuccess:
            case IFSState.FirstDownloadFailed:
                tip = "下载游戏资源包!";
                break;
            case IFSState.FirstUnZip:
                tip = "解压游戏资源包!";
                break;
            case IFSState.LocalFileListInit:
            case IFSState.LocalFileListCheck:
                tip = "资源正确性校正!";
                break;
            case IFSState.NetFileListDownload:
            case IFSState.LocalDiffNetFileList:
                tip = "检查游戏资源更新列表!";
                break;
            case IFSState.DownloadDiffFileListBegin:
            case IFSState.DownloadingDiff:
            case IFSState.DownloadDiffSuccess:
                tip = "更新游戏资源!!";
                break;
            //结束
            case IFSState.Over:
                tip = "准备游戏资源即将进入游戏！";
                break;
            default:
                tip = "加载游戏资源!";
                break;
        }
        return tip;
    }


    //进入游戏准备工作
    private void DoReadyEnterGame()
    {
#if USE_PACK_RES
        //加载主资源包配置
        if (JW.Res.FileUtil.IsExistInIFSExtraFolder("MainResCfg.bytes"))
        {
            string resPackFile = JW.Res.FileUtil.CombinePath(JW.Res.FileUtil.GetIFSExtractPath(), "MainResCfg.bytes");
            byte[] bbs = JW.Res.FileUtil.ReadFile(resPackFile);
            ResService.GetInstance().LoadResPackConfig(bbs);
            bbs = null;
        }
        //加载常驻AB
        AssetService.GetInstance().StartPreloadResidentBundle(delegate ()
        {
            AssetService.GetInstance().StartPreloadAfterResident(OnAllPreloadCompelete);
        });
#else
        _stateArg.Progress = 0f;
        _stateArg.StateInfo = "准备进入游戏.....";
        EventService.GetInstance().SendEvent((uint)EventId.UpdateStateChange, _stateArg);
        //预加载基础资源
        AssetService.GetInstance().StartPreloadAfterResident(OnAllPreloadCompelete);
#endif
    }

    private void OnAllPreloadCompelete(bool isSucc)
    {
        StateService.GetInstance().ChangeState("LuaVM");
    }

    //网络检查时钟 重启时钟
    public void OnScheduleHandle(ScheduleType type, uint id)
    {
        if (type == ScheduleType.Frame)
        {
            if (id == _netCheckSId)
            {
                DoNetworkCheck();
                return;
            }
            if (id == _versionCheckSId)
            {
                DoForceVersionCheck();
                return;
            }
        }
        //重启时钟
        if (type == ScheduleType.Timer)
        {
#if !UNITY_EDITOR
            NativeService.GetInstance().RebootDevice();
#endif
            return;
        }
    }
}

