/********************************************************************
	created:	2:11:2017   10:16
	author:		jordenwu
	purpose:	SVN辅助工具
*********************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text;

public static class EditorSvnHelper
{
    //--------------------------------------------------------------
    /// 抛出异常
    /// @log
    //--------------------------------------------------------------
    private static void Terminate(string log)
    {
        throw (new System.Exception(log));
    }

    /// <summary>
    /// 是否需要做diff
    /// </summary>
    public static bool IsDiffNecessary(string lastRivision, string currentRivision)
    {
        lastRivision = "0";
        currentRivision = "0";
        //
        int lr = int.Parse(lastRivision);
        int cr = int.Parse(currentRivision);
        return lr < cr;
    }

    /// <summary>
    /// 获取差异
    /// </summary>
    /// <param name="dirPath"></param>
    /// <returns></returns>
    public static List<string> DoSvnDiffFileList(string dirPath, string lastRivision, string currentRevision)
    {
        List<string> files = new List<string>();

        System.Diagnostics.Process exep = new System.Diagnostics.Process();
        exep.StartInfo.FileName = "svn";
        exep.StartInfo.Arguments = string.Format("diff -r {0}:{1} --summarize \"{2}\"", lastRivision, currentRevision, dirPath);

        JW.Common.Log.LogD("CMD:" + exep.StartInfo.FileName + " " + exep.StartInfo.Arguments);
        exep.StartInfo.CreateNoWindow = true;
        exep.StartInfo.UseShellExecute = false;
        exep.StartInfo.RedirectStandardOutput = true;
        exep.StartInfo.RedirectStandardError = true;
        exep.Start();

        List<string> output = new List<string>();
        while (exep.StandardOutput.Peek() > -1)
        {
            output.Add(exep.StandardOutput.ReadLine());
        }

        List<string> error = new List<string>();
        while (exep.StandardError.Peek() > -1)
        {
            error.Add(exep.StandardError.ReadLine());
        }

        exep.WaitForExit();

        if (error != null && error.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0, imax = error.Count; i < imax; i++)
            {
                sb.AppendLine(error[i]);
            }

            Terminate(sb.ToString());
            return null;
        }

        foreach (string line in output)
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            string[] splits = System.Text.RegularExpressions.Regex.Split(line, @"\s{2,}");
            if (splits.Length != 2)
            {
                JW.Common.Log.LogE("svn diff process format error:" + line);
                continue;
            }

            string path = splits[1];
            path = path.Replace(Path.DirectorySeparatorChar, '/');
            path = "Assets" + path.Substring(path.IndexOf(Application.dataPath) + Application.dataPath.Length);

            files.Add(path);
        }

        return files;
    }

    /// <summary>
    /// 获取目录或者 读的本地.svn记录 切记先Svn Up
    /// </summary>
    /// <param name="fileOrDirPath"></param>
    /// <returns></returns>
    public static string DoSvnInfoRivision(string fileOrDirPath)
    {
        if (string.IsNullOrEmpty(fileOrDirPath))
        {
            JW.Common.Log.LogE("Arg Error");
        }
        System.Diagnostics.Process exep = new System.Diagnostics.Process
        {
            StartInfo =
                        {
                            FileName = "svn",
                            Arguments = string.Format("info \"{0}\"", fileOrDirPath),
                        }
        };
        JW.Common.Log.LogD("CMD:" + exep.StartInfo.FileName + " " + exep.StartInfo.Arguments);
        exep.StartInfo.CreateNoWindow = true;
        exep.StartInfo.UseShellExecute = false;
        exep.StartInfo.RedirectStandardOutput = true;
        exep.StartInfo.RedirectStandardError = true;

        exep.Start();
        string output = exep.StandardOutput.ReadToEnd();
        string error = exep.StandardError.ReadToEnd();
        exep.WaitForExit();

        if (!string.IsNullOrEmpty(error) && error.Trim().Length > 0)
        {
            JW.Common.Log.LogE(error);
            throw (new Exception(error));
        }

        //
        string[] lines = output.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        foreach (string line in lines)
        {
            if (line.StartsWith("Revision:"))
            {
                return line.Substring(line.IndexOf(":") + 1).Trim();
            }
        }
        return "";

    }

    /// <summary>
    /// Revert目录
    /// </summary>
    /// <param name="dirPath"></param>
    /// <returns></returns>
    public static bool DoSvnRevertDir(string dirPath)
    {
        if (string.IsNullOrEmpty(dirPath))
        {
            JW.Common.Log.LogE("Arg Error");
        }
        if (!JW.Res.FileUtil.IsDirectoryExist(dirPath))
        {
            JW.Common.Log.LogE("Svn Revert Dir Is Not Exist:" + dirPath);
        }

        System.Diagnostics.Process exep = new System.Diagnostics.Process
        {
            StartInfo =
                        {
                            FileName = "svn",
                            Arguments = string.Format("revert \"{0}\" -R ", dirPath),
                        }
        };
        JW.Common.Log.LogD("CMD:" + exep.StartInfo.FileName + " " + exep.StartInfo.Arguments);
        exep.StartInfo.CreateNoWindow = true;
        exep.StartInfo.UseShellExecute = false;
        exep.StartInfo.RedirectStandardOutput = true;
        exep.StartInfo.RedirectStandardError = true;

        exep.Start();
        string output = exep.StandardOutput.ReadToEnd();
        string error = exep.StandardError.ReadToEnd();
        exep.WaitForExit();

        if (!string.IsNullOrEmpty(error) && error.Trim().Length > 0)
        {
            JW.Common.Log.LogE(error);
            throw (new Exception(error));
        }
        JW.Common.Log.LogD("Svn Revert Success!");
        return true;
    }

    /// <summary>
    /// SVN 清理目录 删除未ADD 到库的
    /// </summary>
    /// <param name="dirPath"></param>
    /// <returns></returns>
    public static bool DoSvnCleanUpDir(string dirPath)
    {
        if (string.IsNullOrEmpty(dirPath))
        {
            JW.Common.Log.LogE("Arg Error");
        }
        if (!JW.Res.FileUtil.IsDirectoryExist(dirPath))
        {
            JW.Common.Log.LogE("Svn Revert Dir Is Not Exist:" + dirPath);
        }

        System.Diagnostics.Process exep = new System.Diagnostics.Process
        {
            StartInfo =
                        {
                            FileName = "svn",
                            Arguments = string.Format("cleanup \"{0}\" --remove-unversioned --remove-ignored ", dirPath),
                        }
        };
        JW.Common.Log.LogD("CMD:" + exep.StartInfo.FileName + " " + exep.StartInfo.Arguments);
        exep.StartInfo.CreateNoWindow = true;
        exep.StartInfo.UseShellExecute = false;
        exep.StartInfo.RedirectStandardOutput = true;
        exep.StartInfo.RedirectStandardError = true;

        exep.Start();
        string output = exep.StandardOutput.ReadToEnd();
        string error = exep.StandardError.ReadToEnd();
        exep.WaitForExit();

        if (!string.IsNullOrEmpty(error) && error.Trim().Length > 0)
        {
            JW.Common.Log.LogE(error);
            throw (new Exception(error));
        }
        JW.Common.Log.LogD("Svn CleanUp Success!");
        return true;
    }

    /// <summary>
    /// SVN 清理目录 删除未ADD 到库的
    /// </summary>
    /// <param name="dirPath"></param>
    /// <returns></returns>
    public static bool DoSvnCommitDir(string dirPath, string message)
    {
        if (string.IsNullOrEmpty(dirPath))
        {
            JW.Common.Log.LogE("Arg Error");
        }
        if (string.IsNullOrEmpty(message))
        {
            JW.Common.Log.LogE("Must Get Ci Message");
        }

        if (!JW.Res.FileUtil.IsDirectoryExist(dirPath))
        {
            JW.Common.Log.LogE("Dir Is Not Exist:" + dirPath);
        }

        System.Diagnostics.Process exep = new System.Diagnostics.Process
        {
            StartInfo =
                        {
                            FileName = "svn",
                            Arguments = string.Format("commit \"{0}\" -m \"{1}\"  ", dirPath,message),
                        }
        };
        JW.Common.Log.LogD("CMD:" + exep.StartInfo.FileName + " " + exep.StartInfo.Arguments);
        exep.StartInfo.CreateNoWindow = true;
        exep.StartInfo.UseShellExecute = false;
        exep.StartInfo.RedirectStandardOutput = true;
        exep.StartInfo.RedirectStandardError = true;

        exep.Start();
        string output = exep.StandardOutput.ReadToEnd();
        string error = exep.StandardError.ReadToEnd();
        exep.WaitForExit();

        if (!string.IsNullOrEmpty(error) && error.Trim().Length > 0)
        {
            JW.Common.Log.LogE(error);
            throw (new Exception(error));
        }
        JW.Common.Log.LogD("Svn Commit Success!");
        return true;
    }


}

