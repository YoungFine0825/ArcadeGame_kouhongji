/********************************************************************
	created:	2018-05-30   
	filename: 	Boot
	author:		jordenwu
	
	purpose:	游戏启动入口
*********************************************************************/
using System;
using JW.Common;
using JW.Res;
using JW.Framework.IFS;
using JW.Framework.Http;
using JW.Framework.MVC;
using JW.Framework.Schedule;
using JW.Framework.State;
using JW.Framework.Asset;
using JW.Framework.Event;
using JW.Framework.UGUI;
using JW.Framework.Scene;
using JW.Lua;
using JW.Framework.Audio;
using JW.Framework.Network;
using JW.Framework.Native;
using JW.Framework.ArcadeInput;
using JW.Framework.NetAsset;
using JW.Framework.Quality;

public static class Boot
{
    /// <summary>
    /// 初始化公共层
    /// </summary>
    /// <param name="initialize">初始化//反初始化</param>
    public static void InitCommon(bool initialize)
    {
        if (initialize)
        {
            //统一随机数
            UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
            //开启日志
            Log.GetInstance();
            BundleService.GetInstance();
            ResService.GetInstance();
            //全局
            DG.Tweening.DOTween.Init(true, true, DG.Tweening.LogBehaviour.ErrorsOnly);
        }
        else
        {
            ResService.DestroyInstance();
            BundleService.DestroyInstance();
        }
    }

    /// <summary>
    /// 初始化框架层
    /// </summary>
    /// <param name="initialize">初始化/反初始化</param>
    public static void InitFramework(bool initialize)
    {
        if (initialize)
        {
            NativeService.GetInstance();
            IFSService.GetInstance();
            AssetService.GetInstance();
            ScheduleService.GetInstance();
            EventService.GetInstance();
            StateService.GetInstance();
            HttpService.GetInstance();
            UGUIRoot.GetInstance();
            UIStateService.GetInstance();
            SceneService.GetInstance();
            UICommonService.GetInstance();
            AudioService.GetInstance();
            QualityService.GetInstance();
            NetworkService.GetInstance();
            NetAssetService.GetInstance();

        }
        else
        {
            IFSService.DestroyInstance();
            EventService.DestroyInstance();
            StateService.DestroyInstance();
            HttpService.DestroyInstance();
            UIStateService.DestroyInstance();
            SceneService.DestroyInstance();
            UICommonService.DestroyInstance();
            UGUIRoot.DestroyInstance();
            AudioService.DestroyInstance();
            NetworkService.DestroyInstance();
            NativeService.DestroyInstance();
            NetAssetService.DestroyInstance();
            QualityService.DestroyInstance();
            //最后
            ScheduleService.DestroyInstance();
            AssetService.GetInstance().Destroy();
            AssetService.DestroyInstance();
        }
    }

    /// <summary>
    /// 初始化Logic层
    /// </summary>
    /// <param name="initialize">初始化/反初始化</param>
    public static void InitLogic(bool initialize)
    {
        if (initialize)
        {
            //街机输入初始化
            ArcadeInputService.GetInstance();
            //模块初始化
            ModuleService.GetInstance();
            //注册C# 游戏模块
            ModuleService.GetInstance().Create<LaunchModule, UILaunchMediator>();
            ModuleService.GetInstance().Create<RegisterModule, UIRegisterMediator>();
            ModuleService.GetInstance().Create<UpdateModule, UIUpdateMediator>();
        }
        else
        {
            UGUIRoot.DestroyInstance();
            ModuleService.DestroyInstance();
            LuaService.DestroyInstance();
        }
    }

    /// <summary>
    /// 开始
    /// </summary>
    public static void Run()
    {
        //注册游戏状态
        IState launch = new LaunchState();
        IState reg = new RegisterState();
        IState update = new UpdateState();
        IState luavm = new LuaVmState();
        //
        StateService.GetInstance().RegisteState("Launch", launch);
        StateService.GetInstance().RegisteState("Update", update);
        StateService.GetInstance().RegisteState("Register", reg);
        StateService.GetInstance().RegisteState("LuaVM", luavm);
        //启动
        StateService.GetInstance().ChangeState("Launch");
    }

    /// <summary>
    /// 统一驱动
    /// </summary>
    public static void Update()
    {
        //驱动输入
        ArcadeInputService.GetInstance().LogicUpdate();
        //驱动网络
        NetworkService.GetInstance().LogicUpdate();
        //Lua
        if (LuaService.IsValidate())
        {
            LuaService.GetInstance().LogicUpdate();
        }
        //UI
        UGUIRoot.GetInstance().CustomUpdate();
    }


    /// <summary>
    /// 驱动
    /// </summary>
    public static void LateUpdate()
    {
        UGUIRoot.GetInstance().CustomLateUpdate();
    }


    /// 程序切前后台切换
    public static void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            JW.Common.Log.LogD("OnApplicationPause - Application is sent to background.");

            if (LuaService.IsValidate())
            {
                if (LuaService.GetInstance().LuaFramework!=null)
                {
                    LuaService.GetInstance().LuaFramework.DealAppEnterBackground();
                }
            }

            if (AudioService.IsValidate())
            {
                AudioService.GetInstance().CloseAll();
            }
            if (EventService.IsValidate())
            {
                EventService.GetInstance().SendEvent((uint)(EventId.ApplicationPause), new EventDeclare.ApplicationPauseArg() { IsPause = true });
            }
        }
        else
        {
            JW.Common.Log.LogD("OnApplicationPause - Application is brought to foreground.");

            if (LuaService.IsValidate())
            {
                if (LuaService.GetInstance().LuaFramework != null)
                {
                    LuaService.GetInstance().LuaFramework.DealAppEnterForeground();
                }
            }

            if (AudioService.IsValidate())
            {
                AudioService.GetInstance().OpenAll();
            }
            if (EventService.IsValidate())
            {
                EventService.GetInstance().SendEvent((uint)(EventId.ApplicationPause), new EventDeclare.ApplicationPauseArg() { IsPause = false });
            }
        }
    }

    /// 程序退出
    public static void OnApplicationQuit()
    {
        ProcApplicationQuit();
    }

    private static void ProcApplicationQuit()
    {
        Boot.InitLogic(false);
        Boot.InitFramework(false);
        Boot.InitCommon(false);
        JW.Common.Log.DestroyInstance();
        JW.Common.SingletonManager.Clear();
    }

}
    
