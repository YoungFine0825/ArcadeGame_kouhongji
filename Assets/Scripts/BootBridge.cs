/********************************************************************
    created:    2018-05-30   
    filename:     BootBridge
    author:        jordenwu
    
    purpose:    程序启动MonoBehaviour
*********************************************************************/
using UnityEngine;
using JW.Common;
using JW.Framework.State;
using JW.Framework.UGUI;
using UnityEngine.Rendering;
using System.Runtime.InteropServices;
using System;

/// <summary>
/// 启动桥接
/// </summary>
public class BootBridge : MonoBehaviour
{

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
    [DllImport("User32.dll", EntryPoint = "GetSystemMetrics")]
    public static extern IntPtr GetSystemMetrics(int nIndex);
    private const int SM_CXSCREEN = 0x00000000;
    private const int SM_CYSCREEN = 0x00000001;
#endif

    /// <summary>
    /// 入口
    /// </summary>
    protected void Start()
    {
        //不销毁
        ExtObject.ExtDontDestroyOnLoad(gameObject);
        JW.Common.Log.LogD("--<color=yellow>Unity Start</color> --");
        //目标帧率
        Application.targetFrameRate = 60;
        //禁止多点触摸
        Input.multiTouchEnabled = false;
        //常亮
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //Log.LogD("Screen Width:" + Screen.width.ToString());
        //Log.LogD("Screen Height:" + Screen.height.ToString());
        //Log.LogD("Screen currentResolution.width:" + Screen.currentResolution.width.ToString());
        //Log.LogD("Screen currentResolution.height:" + Screen.currentResolution.height.ToString());
        //Resolution[] resolutions = Screen.resolutions;
        //Log.LogD("--------------");
        //foreach (Resolution re in resolutions)
        //{
        //    Log.LogD(re.ToString());
        //}
        //Log.LogD("--------------");
        //if (resolutions.Length > 1)
        //{
        //    Screen.SetResolution(resolutions[resolutions.Length - 1].width, resolutions[resolutions.Length - 1].height, true);
        //}
        //else
        //{
        //    Screen.SetResolution(1080, 1920, true);
        //}

        //必须全屏
        int xscreen = (int)GetSystemMetrics(SM_CXSCREEN);
        int yscreen = (int)GetSystemMetrics(SM_CYSCREEN);
        Log.LogD("Win32 GetSystemMetrics:{0:D}:{1:D}", xscreen, yscreen);
        Screen.SetResolution(xscreen, yscreen, true);
        Screen.fullScreen = true;
        //鼠标
#if !UNITY_EDITOR && JW_RELEASE
        Cursor.visible = false;
#endif
        JW.Common.Log.LogD("-- Init Common --");
        Boot.InitCommon(true);
        JW.Common.Log.LogD("-- Init Framwwork --");
        Boot.InitFramework(true);
        JW.Common.Log.LogD("-- Init Logic --");
        Boot.InitLogic(true);
        JW.Common.Log.LogD("---<color=yellow>Run Run Run</color> ---");
        Boot.Run();
    }

    /// <summary>
    /// 统一驱动
    /// </summary>
    protected void Update()
    {
        Boot.Update();
#if UNITY_EDITOR || JW_DEBUG
        CulculateFps();
#endif
    }

    /// <summary>
    /// 统一驱动
    /// </summary>
    protected void LateUpdate()
    {
        Boot.LateUpdate();
    }


    /// <summary>
    /// 程序切前后台时调用
    /// </summary>
    /// <param name="pause"></param>
    protected void OnApplicationPause(bool pause)
    {
        Boot.OnApplicationPause(pause);
    }

    /// <summary>
    /// 程序退出
    /// </summary>
    protected void OnApplicationQuit()
    {
        Boot.OnApplicationQuit();
    }


    //------------------------------------------
    float _fps = 0;
    int _frames = 0;
    float _lastInterval = 0;
    void CulculateFps()
    {
        ++_frames;
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > _lastInterval + 0.5)
        {
            _fps = (float)(_frames / (timeNow - _lastInterval));
            _frames = 0;
            _lastInterval = timeNow;
        }
    }
    GUIStyle _style;

#if UNITY_EDITOR || JW_DEBUG
    void OnGUI()
    {
        if (_style == null)
        {
            _style = new GUIStyle();
            _style.normal.textColor = Color.yellow;
            _style.fontSize = 26;
        }
        string fps = "FPS:" + _fps;
        GUI.Label(new Rect(new Vector2(0, Screen.height - 30), new Vector2(200, 30)), new GUIContent(fps), _style);
#if USE_PACK_RES
        GUI.Label(new Rect(new Vector2(0, 0), new Vector2(300, 30)), new GUIContent("JW_DEBUG+USE_PACK_RES+Version："+VersionInfo.GetCodeVersion()), _style);
#endif
    }
#endif

}

