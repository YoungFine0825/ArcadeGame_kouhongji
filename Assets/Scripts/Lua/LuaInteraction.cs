/********************************************************************
	created:	2018-07-02
	filename: 	LuaInteraction
	author:		jordenwu
	
	purpose:	Lua交互
*********************************************************************/
using JW.Common;
using JW.Framework.MVC;
using JW.Framework.UGUI;
using JW.Framework.Asset;
using JW.Framework.Network;
using JW.Framework.Native;
using JW.Framework.Http;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using JW.Framework.Audio;
using JW.Framework.Scene;
using JW.Res;
using JW.Framework.IFS;
using UnityEngine;
using System.Text;
using JW.Framework.ArcadeInput;
using JW.Framework.Quality;
using JW.Framework.NetAsset;

namespace JW.Lua
{
    public static class LuaInteraction
    {
        public static void Log(int type, string content)
        {
            switch (type)
            {
                case 0:
                    JW.Common.Log.LogD("<color=cyan>[Lua]</color>" + content);
                    break;
                case 1:
                    JW.Common.Log.LogW("[Lua]" + content);
                    break;
                case 2:
                    JW.Common.Log.LogE("[Lua]" + content);
                    break;
            }
        }

        //-----------------------------------------------------------------------
        public static UIStateService GetUIStateService()
        {
            return UIStateService.GetInstance();
        }


        public static UGUIRoot GetUGUIRoot()
        {
            return UGUIRoot.GetInstance();
        }


        public static AssetService GetAssetService()
        {
            return AssetService.GetInstance();
        }

        public static NetworkService GetNetworkService()
        {
            return NetworkService.GetInstance();
        }

        public static NativeService GetNativeService()
        {
            return NativeService.GetInstance();
        }

        public static UICommonService GetUICommonService()
        {
            return UICommonService.GetInstance();
        }

        public static AudioService GetAudioService()
        {
            return AudioService.GetInstance();
        }

        public static SceneService GetSceneService()
        {
            return SceneService.GetInstance();
        }

        public static ResService GetResService()
        {
            return ResService.GetInstance();
        }

        public static IFSService GetIFSService()
        {
            return IFSService.GetInstance();
        }
        public static HttpService GetHttpService()
        {
            return HttpService.GetInstance();
        }

        public static ArcadeInputService GetArcadeInputService()
        {
            return ArcadeInputService.GetInstance();
        }

        public static QualityService GetQualityService()
        {
            return QualityService.GetInstance();
        }

        public static NetAssetService GetNetAssetService()
        {
            return NetAssetService.GetInstance();
        }

        /// <summary>
        /// 退出程序
        /// </summary>
        public static void ExitApplication()
        {
            if (!Application.isEditor)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    Boot.OnApplicationQuit();
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
                catch (System.Exception e)
                {
                    Application.Quit();
                }         
#else
                Application.Quit();
#endif
            }
            else
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
        }


        //--------------------------Asset------------------------------------------------

        public static void LoadAsynAsset(int loadPriority, int type, string filename, int life)
        {
            LuaService.GetInstance().LuaFramework.LoadAsynAsset(loadPriority, type, filename, life);
        }

        //-------------------------------------------------------------
        public static void AddUIEventHandle(UnityEvent list, int id, bool oneShot)
        {
            LuaService.GetInstance().LuaFramework.AddUIEventHandle(list, id, oneShot);
        }

        public static void RemoveUIEventHandle(UnityEvent list, int id)
        {
            LuaService.GetInstance().LuaFramework.RemoveUIEventHandle(list, id);
        }

        public static void AddUIEventListenerHandle(UIListenerEvent list, int id, bool oneShot)
        {
            LuaService.GetInstance().LuaFramework.AddUIEventListenerHandler(list, id, oneShot);
        }

        public static void RemoveUIEventListenerHandle(UIListenerEvent list, int id)
        {
            LuaService.GetInstance().LuaFramework.RemoveUIEventListenerHandle(list, id);
        }
        //------------------------------------------

        /// <summary>
        /// 取资源二进制数据
        /// </summary>
        /// <param name="resPath">资源路径</param>
        /// <returns>二进制数据</returns>
        public static byte[] GetResourceBytes(string resPath)
        {
            ResObj res = ResService.GetResource(resPath);
            if (res == null)
            {
                return null;
            }

            BinaryObject binaryObject = res.Content as BinaryObject;
            if (binaryObject == null)
            {
                return null;
            }

            byte[] rawBytes = binaryObject.m_data;

            ResService.UnloadResource(res);

            return rawBytes;
        }

        /// 获取文件内容
        public static string GetTextFromFile(string filename)
        {
            string text = string.Empty;
            ResObj res = ResService.GetResource(filename);
            if (res != null)
            {
                BinaryObject binaryObject = res.Content as BinaryObject;
                byte[] rawBytes = binaryObject.m_data;
                if (rawBytes != null && rawBytes.Length > 0)
                {
                    ResService.UnloadResource(res);
                    text = Encoding.UTF8.GetString(rawBytes);
                }
            }
            return text;
        }

        public static bool LoadLuaZip(string fileName)
        {
            return LuaService.GetInstance().LoadLuaZip(fileName);
        }

        public static void UnLoadLuaZip(string fileName)
        {
            LuaService.GetInstance().UnLoadLuaZip(fileName);
        }
        //---------------------------

    }
}
