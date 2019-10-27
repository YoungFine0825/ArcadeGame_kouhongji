/********************************************************************
	created:	2018-07-02
	filename: 	LuaFramework
	author:		jordenwu
	
	purpose:	Lua框架层对接
*********************************************************************/
using JW.Common;
using JW.Framework.Asset;
using JW.Res;
using XLua;
using JW.Framework.MVC;
using UnityEngine.Events;
using JW.Framework.Network;
using JW.Framework.UGUI;
using JW.Framework.Native;
using JW.Framework.ArcadeInput;

namespace JW.Lua
{
    public class LuaFramework:IAssetLoadCallback
    {

        public delegate void OnLoadAssetCompletedDelegate(string name, int result, ResObj resource);
        private OnLoadAssetCompletedDelegate _onLoadAssetCompleted;

        public delegate void OnChangeUIStateDelegate(int callType, int changeType, string oldStateName, string newStateName, object stateParam);
        private OnChangeUIStateDelegate _onChangeUIState;

        private LuaUIEventBridge _uiEventBridge;

        private NativeServiceBaseDelegate _onNativeCallBack;

        //游戏回到前后台处理
        private System.Action _appEnterBackGroundCallBack;
        private System.Action _appEnterForeGroundCallBack;

        //硬件输入
        private ArcadeInputRockerDelegate _arcadeInputRockerCallBack;
        private ArcadeInputRefreshDelegate _arcadeInputRefreshCallBack;
        private ArcadeInputRotateDelegate _arcadeInputRotateCallBack;
        private ArcadeInputPressDelegate _arcadeInputOkCallBack;

        //
        private System.Action<bool> _gcCallBack;

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize(LuaEnv lua)
        {
            _onLoadAssetCompleted = lua.Global.GetInPath<OnLoadAssetCompletedDelegate>("AssetService.OnCsLoadAssetCompleted");
            _onChangeUIState = lua.Global.GetInPath<OnChangeUIStateDelegate>("MVCService.OnCsChangeUIState");

            _onNativeCallBack = lua.Global.GetInPath<NativeServiceBaseDelegate>("NativeService.OnCommonCallback");

            _appEnterBackGroundCallBack = lua.Global.GetInPath<System.Action>("AppMoniter.OnCSAppEnterBackGround");
            _appEnterForeGroundCallBack = lua.Global.GetInPath<System.Action>("AppMoniter.OnCSAppEnterForeGround");

            //
            _arcadeInputOkCallBack = lua.Global.GetInPath<ArcadeInputPressDelegate>("ArcadeInputService.OnCsOkInput");
            _arcadeInputRefreshCallBack = lua.Global.GetInPath<ArcadeInputRefreshDelegate>("ArcadeInputService.OnCsRefreshInput");
            _arcadeInputRotateCallBack= lua.Global.GetInPath<ArcadeInputRotateDelegate>("ArcadeInputService.OnCsRotateInput");
            _arcadeInputRockerCallBack= lua.Global.GetInPath<ArcadeInputRockerDelegate>("ArcadeInputService.OnCsRockerInput");
            //
            _gcCallBack = lua.Global.GetInPath<System.Action<bool>>("GCService.CollectGarbage");

            //挂接
            UIStateService.GetInstance().Hook = this.ChangeUIState;
            AssetService.GetInstance().LuaGCHook = this.OnAssetServiceGC;
            //
            NativeService.GetInstance().RegisterBaseHandler(OnNativeBaseCallBackHook);
            //硬件输入
            ArcadeInputService.GetInstance().RockerHandler = this.DealArcadeInputRockerInput;
            ArcadeInputService.GetInstance().RotateHandler = this.DealArcadeInputRotateInput;
            ArcadeInputService.GetInstance().RefreshHandler = this.DealArcadeInputRefreshInput;
            ArcadeInputService.GetInstance().PressHandler = this.DealArcadeInputOkInput;
            //
            _uiEventBridge = new LuaUIEventBridge();
            _uiEventBridge.Initialize(lua);
        }

        /// <summary>
        /// 反初始化
        /// </summary>
        public void UnInitialize()
        {
            _onLoadAssetCompleted = null;
            _onChangeUIState = null;
            _uiEventBridge.UnInitialize();
            _uiEventBridge = null;

            _onNativeCallBack = null;
            _appEnterBackGroundCallBack = null;
            _appEnterForeGroundCallBack = null;

            _arcadeInputOkCallBack = null;
            _arcadeInputRefreshCallBack = null;
            _arcadeInputRockerCallBack = null;
            _arcadeInputRotateCallBack = null;
            _gcCallBack = null;
        }


        ///切换UI状态回调到Lua 调用类型（0 - stay 1 - out 2 - in）
        private void ChangeUIState(int callType, int changeType, string oldStateName, string newStateName, object stateParam)
        {
            if (_onChangeUIState != null)
            {
                _onChangeUIState(callType, changeType, oldStateName, newStateName, stateParam);
            }
        }

        public void OnNativeBaseCallBackHook(NativeResult result)
        {
            if (_onNativeCallBack != null)
            {
                _onNativeCallBack(result);
            }
        }


        //Lua 前更新
        public void PreUpdate()
        {
            if (_uiEventBridge != null)
            {
                _uiEventBridge.Update();
            }
        }

        /// 异步装载资源 lua
        public void LoadAsynAsset(int loadPriority, int type, string filename, int life)
        {
            if (AssetService.IsValidate())
            {
                AssetService.GetInstance().LoadAsyn(type, loadPriority, filename, life, this);
            }
        }

        //回调Lua
        public void OnLoadAssetCompleted(string assetName, int result, ResObj resource)
        {
            if (_onLoadAssetCompleted != null)
            {
                _onLoadAssetCompleted(assetName, result, resource);
            }
        }

        ///添加UI基本事件回调
        public  void AddUIEventHandle(UnityEvent list, int id, bool oneShot)
        {
            if (_uiEventBridge != null)
            {
                _uiEventBridge.AddUIEventHandle(list, id, oneShot);
            }
        }

        ///添加UI基本事件回调
        public void AddUIEventListenerHandler(UIListenerEvent target, int id, bool oneShort)
        {
            if (_uiEventBridge != null)
            {
                _uiEventBridge.AddUIEventListenerHandle(target, id, oneShort);
            }
        }

        public  void RemoveUIEventHandle(UnityEvent list, int id)
        {
            if (_uiEventBridge != null)
            {
                _uiEventBridge.RemoveUIEventHandle(list, id);
            }
        }

        public void RemoveUIEventListenerHandle(UIListenerEvent list, int id)
        {
            if (_uiEventBridge != null)
            {
                _uiEventBridge.RemoveUIEventListenerHandle(list, id);
            }
        }

        public void DealAppEnterBackground()
        {
            if (_appEnterBackGroundCallBack != null)
            {
                _appEnterBackGroundCallBack();
            }
        }

        public void DealAppEnterForeground()
        {
            if (_appEnterForeGroundCallBack != null)
            {
                _appEnterForeGroundCallBack();
            }
        }

        //硬件输入
        private void DealArcadeInputOkInput(int pe)
        {
            if (_arcadeInputOkCallBack != null)
            {
                _arcadeInputOkCallBack(pe);
            }
        }

        private void DealArcadeInputRefreshInput(int pe)
        {
            if (_arcadeInputRefreshCallBack != null)
            {
                _arcadeInputRefreshCallBack(pe);
            }
        }

        private void DealArcadeInputRotateInput(float pe)
        {
            if (_arcadeInputRotateCallBack != null)
            {
                _arcadeInputRotateCallBack(pe);
            }
        }

        private void DealArcadeInputRockerInput(int rst)
        {
            if (_arcadeInputRockerCallBack != null)
            {
                _arcadeInputRockerCallBack(rst);
            }
        }
        //----------------------------------------
        private void OnAssetServiceGC(bool isFull)
        {
            if (_gcCallBack != null)
            {
                _gcCallBack(isFull);
            }
        }
    }
}
