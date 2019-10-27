/********************************************************************
	created:	17:5:2018   
	filename: 	Log
	author:		jordenwu
	
	purpose:	日志输出
*********************************************************************/
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;

namespace JW.Common
{
    /// <summary>
    /// Log单例定义
    /// </summary>
    public class Log : Singleton<Log>
    {
        /// <summary>
        /// 日志类型定义
        /// </summary>
        public enum Type
        {
            Debug,
            Warning,
            Error,
            None,
        }

        private StringBuilder _stringBuilder;
        private StringBuilder _stringHelper;
        private StreamWriter _writer;

        //日志等级 默认错误才输出 Release
        public static Type LogLevel = Type.Error;
        string logPath;

        public override bool Initialize()
        {
            //
            logPath = string.Format("{0}/../ArcadeCache/Log", Application.dataPath);
            //
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            //删除非今天的日志文件Release
#if JW_RELEASE
            DateTime now = DateTime.Now;
            string logFileName = string.Format("Log_{0}_{1}_{2}.log",now.Year, now.Month, now.Day);
            string[] files = System.IO.Directory.GetFiles(logPath);
            for (int i = 0; i < files.Length; i++)
            {
                if (!files[i].Contains(logFileName))
                {
                    File.Delete(files[i]);
                    break;
                }
            }
#endif
            //日志等级初始化
#if UNITY_EDITOR || JW_DEBUG
            LogLevel = Type.Debug;
#endif
            //本地文件记录
            _stringBuilder = new StringBuilder(128);
            _stringHelper = new StringBuilder(128);
            //挂接
            Application.logMessageReceived += Application_LogMessageReceived;
            return true;
        }

        //挂接异常输出
        private void Application_LogMessageReceived(string condition, string stackTrace, LogType type)
        {
            if (type == LogType.Exception)
            {
                if (condition.StartsWith("LuaException"))
                {
                    GetInstance().OutputLog(Type.Error, "LuaException:" + condition + "\n" + stackTrace);
                }
                else
                {
                    GetInstance().OutputLog(Type.Error, "Crash:" + condition + "\n" + stackTrace);
                }
            }
        }

        public override void Uninitialize()
        {
            if (_writer != null)
            {
                try
                {
                    _writer.Flush();
                    _writer.BaseStream.Flush();
                    _writer.Close();
                    _writer = null;
                }
                catch
                {
                    // ignored
                }
            }

            _stringBuilder = null;
            _stringHelper = null;
            Application.logMessageReceived -= Application_LogMessageReceived;

        }

        //注意 条件是 或的关系
        [Conditional("UNITY_EDITOR"), Conditional("JW_DEBUG")]
        public static void LogD(string content)
        {
            GetInstance().OutputLog(Type.Debug, content);
        }

        //注意 条件是 或的关系
        [Conditional("UNITY_EDITOR"), Conditional("JW_DEBUG")]
        public static void LogD(string format, params object[] args)
        {
            GetInstance().OutputLog(Type.Debug, format, args);
        }

        //注意 条件是 或的关系
        [Conditional("UNITY_EDITOR"), Conditional("JW_DEBUG")]
        public static void LogW(string content)
        {
            GetInstance().OutputLog(Type.Warning, content);
        }

        //注意 条件是 或的关系
        [Conditional("UNITY_EDITOR"), Conditional("JW_DEBUG")]
        public static void LogW(string format, params object[] args)
        {
            GetInstance().OutputLog(Type.Warning, format, args);
        }

        //注意 条件是 或的关系
        [Conditional("UNITY_EDITOR"), Conditional("JW_DEBUG"), Conditional("JW_RELEASE")]
        public static void LogE(string content)
        {
            GetInstance().OutputLog(Type.Error, content);
        }

        //注意 条件是 或的关系
        [Conditional("UNITY_EDITOR"), Conditional("JW_DEBUG"), Conditional("JW_RELEASE")]
        public static void LogE(string format, params object[] args)
        {
            GetInstance().OutputLog(Type.Error, format, args);
        }


        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="type"></param>
        /// <param name="content"></param>
        private void OutputLog(Type type, string content)
        {
            if (type < LogLevel)
            {
                return;
            }

            switch (type)
            {
                case Type.Debug:
                    UnityEngine.Debug.Log(content);
                    break;

                case Type.Warning:
                    UnityEngine.Debug.LogWarning(content);
                    break;

                case Type.Error:
                    UnityEngine.Debug.LogError(content);
                    break;
            }

            if (_writer == null)
            {
                OpenFile();
            }

            if (_writer != null)
            {
                BuildLog(type);
                _stringBuilder.Append(content);
                try
                {
                    _writer.WriteLine(_stringBuilder.ToString());
                    _writer.Flush();
                    _writer.BaseStream.Flush();
                }
                catch
                {
                    // ignored
                }
            }
        }

        private void OutputLog(Type type, string format, params object[] args)
        {
            if(!string.IsNullOrEmpty(format)&& args!=null && args.Length > 0)
            {
                _stringHelper.Length = 0;
                _stringHelper.AppendFormat(format, args);
                OutputLog(type, _stringHelper.ToString());
            }
        }

        private void OpenFile()
        {
            DateTime now = DateTime.Now;

            string logFile = string.Format("{0}/Log_{1}_{2}_{3}.log", logPath, now.Year, now.Month, now.Day);
            try
            {
                if (!File.Exists(logFile))
                {
                    FileStream fs = new FileStream(logFile, FileMode.OpenOrCreate);
                    _writer = new StreamWriter(fs);
                }
                else
                {
                    var fs = new FileStream(logFile, FileMode.Append);//改为追加
                    _writer = new StreamWriter(fs);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void BuildLog(Type type)
        {
            _stringBuilder.Length = 0;

            DateTime now = DateTime.Now;
            _stringBuilder.Append('[');

            //StringBuilderOperator.AppendInt(_stringBuilder, now.Day, 2);
            //_stringBuilder.Append(':');

            StringBuilderOperator.AppendInt(_stringBuilder, now.Hour, 2);
            _stringBuilder.Append(':');

            StringBuilderOperator.AppendInt(_stringBuilder, now.Minute, 2);
            _stringBuilder.Append(':');

            StringBuilderOperator.AppendInt(_stringBuilder, now.Second, 2);

            switch (type)
            {
                case Type.Debug:
                    _stringBuilder.Append("]D:");
                    break;

                case Type.Warning:
                    _stringBuilder.Append("]W:");
                    break;

                case Type.Error:
                    _stringBuilder.Append("]E:");
                    break;
            }
        }

        [Conditional("UNITY_EDITOR"), Conditional("JW_DEBUG")]
        static public void EditorAssert(bool InCondition, string InFormat = null, params object[] InParameters)
        {
            Assert(InCondition, InFormat, InParameters);
        }

        static public void Assert(bool InCondition)
        {
            Assert(InCondition, null, null);
        }

        static public void Assert(bool InCondition, string InFormat)
        {
            Assert(InCondition, InFormat, null);
        }

        static public void Assert(bool InCondition, string InFormat, params object[] InParameters)
        {
            if (!InCondition)
            {
                try
                {
                    string failedMessage = null;

                    if (!string.IsNullOrEmpty(InFormat))
                    {
                        try
                        {
                            if (InParameters != null)
                            {
                                failedMessage = string.Format(InFormat, InParameters);
                            }
                            else
                            {
                                failedMessage = InFormat;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
#if UNITY_ANDROID || UNITY_IPHONE
                else
                {
                    failedMessage = string.Format(" no assert detail, stacktrace is :{0}", Environment.StackTrace);
                }
#endif

#if UNITY_EDITOR || UNITY_STANDALONE
                    if (failedMessage != null)
                    {
                        LogE("Assert failed! " + failedMessage);
                    }
                    else
                    {
                        LogE("Assert failed!");
                    }

                    string msg = "Assert failed! ";
                    if (!string.IsNullOrEmpty(failedMessage))
                    {
                        msg += failedMessage;
                    }

                    var trace = new System.Diagnostics.StackTrace();
                    var frames = trace.GetFrames();
                    for (int i = 0; i < frames.Length; ++i)
                    {
                        msg += frames[i].ToString();
                    }

                    try
                    {
                        LogE(msg);
                    }
                    catch (Exception)
                    {
                    }
#else
                if (failedMessage != null)
                {
                    var str = "Assert failed! " + failedMessage;
                    LogW(str);
                }
                else
                {
                    LogW("Assert failed!");
                }
#endif
                }
                catch (Exception)
                {

                }
            }
        }

    }
}
