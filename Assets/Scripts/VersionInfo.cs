using System;
using UnityEngine;

public static class VersionInfo
{
    //Version文本在Resources目录下的路径
    private const string VersionFileName = "Version";
    private static string _codeNoInApp = null;

    /// <summary>
    /// 返回App版本号中的代码号
    /// </summary>
    /// <returns></returns>
    public static string GetCodeVersion()
    {
        if (_codeNoInApp == null)
        {
            Initialize();
        }

        if (string.IsNullOrEmpty(_codeNoInApp))
        {
            return "000";
        }
        else
        {
            return _codeNoInApp;
        }
    }

   
    //读取Version 文件
    private static void Initialize()
    {
        _codeNoInApp = string.Empty;

        TextAsset textAsset = Resources.Load(VersionFileName, typeof(TextAsset)) as TextAsset;
        if (textAsset != null)
        {
            string content = textAsset.text;
            if (string.IsNullOrEmpty(content))
            {
                _codeNoInApp = "000";
            }
            else
            {
                _codeNoInApp = content.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
            }
        }
    }
};
