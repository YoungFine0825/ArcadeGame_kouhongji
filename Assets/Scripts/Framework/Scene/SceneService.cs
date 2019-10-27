/********************************************************************
	created:	2018-06-19
	filename: 	SceneService
	author:		jordenwu
	
	purpose:	场景切换管理服务
*********************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JW.Common;
using JW.Framework.Asset;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;

namespace JW.Framework.Scene
{
    public class SceneService : MonoSingleton<SceneService>
    {
        /// <summary>
        /// 场景加载统一委托定义
        /// </summary>
        /// <param name="progress"></param>
        public delegate void OnUnitySceneLoadDelegate(float progress,bool isDone);

        public override bool Initialize()
        {
            SceneManager.sceneLoaded += OnUnitySceneLoaded;
            return true;
        }

        public override void Uninitialize()
        {
            SceneManager.sceneLoaded -= OnUnitySceneLoaded;
        }

        public void RegisterSceneLoaded(UnityAction<UnityEngine.SceneManagement.Scene, LoadSceneMode> handler)
        {
            SceneManager.sceneLoaded += handler;
        }

        public void UnRegisterSceneLoaded(UnityAction<UnityEngine.SceneManagement.Scene, LoadSceneMode> handler)
        {
            SceneManager.sceneLoaded -= handler;
        }

        private void OnUnitySceneLoaded(UnityEngine.SceneManagement.Scene ss, LoadSceneMode mm)
        {
            JW.Common.Log.LogD("OnUnitySceneLoaded:" + ss.path);
        }

        /// <summary>
        /// 根据名字获取已经加载了的Unity场景
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public UnityEngine.SceneManagement.Scene GetUnitySceneByName(string name)
        {
            return SceneManager.GetSceneByName(name);
        }

        /// <summary>
        /// 同步加载Unity场景
        /// </summary>
        /// <param name="name">场景名称</param>
        public void LoadUnityScene(string name, LoadSceneMode mode)
        {
            SceneManager.LoadScene(name, mode);
        }

        /// <summary>
        /// 异步加载Unity场景
        /// </summary>
        /// <param name="name"></param>
        /// <param name="finishHander"></param>
        public void LoadUnitySceneAsync(string name, LoadSceneMode mode, OnUnitySceneLoadDelegate handler)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(name, mode);
            ao.completed += delegate (AsyncOperation asp)
            {

                if (handler != null)
                {
                    handler(asp.progress,asp.isDone);
                }
            };
        }

        /// <summary>
        /// 异步加载Unity场景
        /// </summary>
        /// <param name="name"></param>
        /// <param name="finishHander"></param>
        public void LoadUnitySceneAsyncLua(string name, int mode, OnUnitySceneLoadDelegate handler)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(name, (LoadSceneMode)mode);
            ao.completed += delegate (AsyncOperation asp)
            {

                if (handler != null)
                {
                    handler(asp.progress, asp.isDone);
                }
            };
        }

        /// <summary>
        /// 异步加载Unity场景
        /// </summary>
        /// <param name="name"></param>
        /// <param name="finishHander"></param>
        public void LoadUnitySceneAsync(string name, LoadSceneMode mode,string lastScene,bool cleanLast, OnUnitySceneLoadDelegate handler)
        {
            Action<AsyncOperation> curD = delegate (AsyncOperation asp)
             {
                 if (handler != null)
                 {
                     handler(asp.progress, asp.isDone);
                 }
            };
            
            //
            if (cleanLast)
            {
                AsyncOperation last = SceneManager.UnloadSceneAsync(lastScene);
                if (last != null)
                {
                    last.completed += delegate (AsyncOperation asp)
                    {
                        if (asp.isDone)
                        {
                            AsyncOperation ao = SceneManager.LoadSceneAsync(name, mode);
                            ao.completed += curD;
                        }
                    };
                }
                else
                {
                    AsyncOperation ao = SceneManager.LoadSceneAsync(name, mode);
                    ao.completed += curD;
                }
               
            }
            else
            {
                AsyncOperation ao = SceneManager.LoadSceneAsync(name, mode);
                ao.completed += curD;
            }
        }

    }
}
