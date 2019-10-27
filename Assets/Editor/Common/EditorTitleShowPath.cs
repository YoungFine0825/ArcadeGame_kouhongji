/********************************************************************
	created:	2018-06-25
	filename: 	EditorTitleShowPath
	author:		jordenwu
	
	purpose:	扩展显示编辑器工程路径
*********************************************************************/
using UnityEngine;
using UnityEditor;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System;
using System.Text;

#if UNITY_EDITOR_WIN
[InitializeOnLoad]
static class EditorTitleShowPath
{
    static EditorTitleShowPath()
    {
       EditorApplication.update += DoUpdateTitleFunc;
    }

    static void DoUpdateTitleFunc()
    {
        EditorApplication.update -= DoUpdateTitleFunc;
        UpdateUnityEditorProcess.Instance.SetTitle();
    }
}

class UpdateUnityEditorProcess
{
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool EnumWindows(EnumThreadWindowsCallback callback, IntPtr extraData);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int GetWindowThreadProcessId(HandleRef handle, out int processId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
    private static extern IntPtr GetWindow(HandleRef hWnd, int uCmd);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern bool IsWindowVisible(HandleRef hWnd);

    [DllImport("user32.dll")]
    private static extern bool GetWindowText(int hWnd, StringBuilder title, int maxBufSize);

    [DllImport("user32.dll", EntryPoint = "SetWindowText", CharSet = CharSet.Auto)]
    private static extern int SetWindowText(int hwnd, string lpString);

    private delegate bool EnumThreadWindowsCallback(IntPtr hWnd, IntPtr lParam);

    private static UpdateUnityEditorProcess _instance;

    private int _processId;
    private IntPtr _mainWindowHandle = IntPtr.Zero;


    private readonly StringBuilder _sbtitle = new StringBuilder(255);
    private double _lasttime;
        
    public static UpdateUnityEditorProcess Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UpdateUnityEditorProcess();
                _instance.GetMainWindowHandle(Process.GetCurrentProcess().Id);
            }

            return _instance;
        }
    }

    public void SetTitle()
    {
        if (EditorApplication.timeSinceStartup < _lasttime || _mainWindowHandle == IntPtr.Zero)
        {
            return;
        }

        _lasttime = EditorApplication.timeSinceStartup + 2.0;

        _sbtitle.Length = 0;

        GetWindowText(_mainWindowHandle.ToInt32(), _sbtitle, 255);
        
        string strTitle = _sbtitle.ToString();
        if (strTitle.Length > 0 && !strTitle.Contains(Application.dataPath))
        {
            SetWindowText(_mainWindowHandle.ToInt32(), string.Format("{0} - {1}", Application.dataPath, strTitle));
        }
    }

    private void GetMainWindowHandle(int processId)
    {
        if (_mainWindowHandle != IntPtr.Zero)
        {
            return;
        }

        _processId = processId;
        EnumThreadWindowsCallback callback = EnumWindowsCallback;
        EnumWindows(callback, IntPtr.Zero);
        GC.KeepAlive(callback);
    }

    private bool EnumWindowsCallback(IntPtr handle, IntPtr extraParameter)
    {
        int num;
        GetWindowThreadProcessId(new HandleRef(this, handle), out num);
        if ((num == _processId) && IsMainWindow(handle))
        {
            _mainWindowHandle = handle;
            return false;
        }

        return true;
    }

    private bool IsMainWindow(IntPtr handle)
    {
        return !(GetWindow(new HandleRef(this, handle), 4) != IntPtr.Zero) && IsWindowVisible(new HandleRef(this, handle));
    }
}

#endif
