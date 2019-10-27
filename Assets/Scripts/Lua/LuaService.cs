/********************************************************************
	created:	2018-07-02
	filename: 	LuaService
	author:		jordenwu
	
	purpose:	Lua服务
*********************************************************************/
using XLua;
using JW.Common;
using UnityEngine;
using System;
using JW.Framework.Event;

namespace JW.Lua
{
    public class LuaService : MonoSingleton<LuaService>
    {
        // lua虚拟机
        private LuaEnv _luaEnv;
        private LuaLoader _loader;
        private Action<float> _updateFunc;

        private bool _initFramework;
        private bool _initLogic;


        public LuaEnv GetLuaEnv()
        {
            return _luaEnv;
        }


        /// <summary>
        /// Lua框架
        /// </summary>
        public LuaFramework LuaFramework
        {
            get;
            private set;
        }

        /// <summary>
        /// 模块初始化
        /// </summary>
        public override bool Initialize()
        {
            return true;
        }

        /// <summary>
        /// 反初始化
        /// </summary>
        /// <returns></returns>
        public override void Uninitialize()
        {
            InitLogic(false);
            InitFramework(false);
            CancelLoad();
        }

        /// <summary>
        /// 加载Main 包Lua
        /// </summary>
        public void LoadMainLua(System.Action<int> callBack)
        {
            JW.Common.Log.LogD("LuaService.load");
            _luaEnv = new LuaEnv();
            //
            _luaEnv.ExceptionHandler = this.DealLuaException;
            //
            _loader = new LuaLoader();
            StartCoroutine(_loader.LoadMainLua(_luaEnv,callBack));
        }

        private void DealLuaException(string errorInfo)
        {
            JW.Common.Log.LogE(errorInfo);
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPaused = true;
            return;
#endif
            _updateFunc = null;
            //
            JW.Common.Log.LogE("------------LuaException------------ReBoot----------------------");
            JW.Framework.State.StateService.GetInstance().ChangeState("Launch");
        }

        /// <summary>
        /// 取消加载
        /// </summary>
        public void CancelLoad()
        {
            StopAllCoroutines();
            if (_luaEnv != null)
            {
                _luaEnv.Dispose();
                _luaEnv = null;
            }
            if (_loader != null)
            {
                _loader.UnInit();
                _loader = null;
            }
            LuaFramework = null;
            _updateFunc = null;
        }

        public bool LoadLuaZip( string fileName)
        {
            if (_loader != null)
            {
                return _loader.LoadLuaZip(fileName);
            }
            return false;
        }

        public void UnLoadLuaZip(string fileName)
        {
            if (_loader != null)
            {
                 _loader.UnLoadLuaZip();
            }
        }

        /// <summary>
        /// 初始化Lua framework层
        /// </summary>
        /// <param name="init">true:初始化；false:反初始化</param>
        public void InitFramework(bool init = true)
        {
            if (init == _initFramework)
            {
                JW.Common.Log.LogE("LuaService.InitFramework : duplicate init/uninit - {0}", init);
                return;
            }

            if (_luaEnv == null)
            {
                JW.Common.Log.LogE("LuaService.InitFramework : lua service not initialize");
                return;
            }

            _initFramework = init;

            LuaFunction initFramework = _luaEnv.Global.Get<LuaFunction>("InitFramework");
            if (initFramework != null)
            {
                initFramework.Action<bool>(init);
            }

            if (init)
            {
                LuaFramework = new LuaFramework();
                LuaFramework.Initialize(_luaEnv);
            }
            else
            {
                if (LuaFramework != null)
                {
                    LuaFramework.UnInitialize();
                    LuaFramework = null;
                }
            }
            initFramework = null;
        }

        /// <summary>
        /// 初始化逻辑层
        /// </summary>
        /// <param name="init">true:初始化；false:反初始化</param>
        public void InitLogic(bool init = true)
        {
            if (init == _initLogic)
            {
                JW.Common.Log.LogE("LuaService.InitLogic : duplicate init/uninit - {0}", init);
                return;
            }

            if (_luaEnv == null)
            {
                JW.Common.Log.LogE("LuaService.InitLogic : lua service not initialize");
                return;
            }
            _initLogic = init;
            LuaFunction initLogic = _luaEnv.Global.Get<LuaFunction>("InitLogic");
            if (initLogic != null)
            {
                initLogic.Action<bool>(init);
            }
            initLogic = null;
        }

        /// <summary>
        /// 开始Lua 游戏
        /// </summary>
        public void StartGame()
        {
            LuaFunction ss = _luaEnv.Global.Get<LuaFunction>("StartGame");
            _updateFunc= _luaEnv.Global.Get<Action<float>>("GameUpdate");
            if (ss != null&&_updateFunc!=null)
            {
                ss.Action<int>(1);
            }
        }

        private int _bootLuaInterval = 0;

        /// <summary>
        /// Boot驱动
        /// </summary>
        public void LogicUpdate()
        {
            if (LuaFramework != null)
            {
                LuaFramework.PreUpdate();
            }
            //
            if (_updateFunc != null)
            {
                _updateFunc(Time.deltaTime);
            }
        }
    }
}
