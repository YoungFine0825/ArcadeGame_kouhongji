﻿#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    
    public class UnityEnginePlayModeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.PlayMode), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.PlayMode), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.PlayMode), L, null, 3, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "StopSameLayer", UnityEngine.PlayMode.StopSameLayer);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "StopAll", UnityEngine.PlayMode.StopAll);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.PlayMode), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEnginePlayMode(L, (UnityEngine.PlayMode)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "StopSameLayer"))
                {
                    translator.PushUnityEnginePlayMode(L, UnityEngine.PlayMode.StopSameLayer);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "StopAll"))
                {
                    translator.PushUnityEnginePlayMode(L, UnityEngine.PlayMode.StopAll);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.PlayMode!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.PlayMode! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineScreenOrientationWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.ScreenOrientation), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.ScreenOrientation), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.ScreenOrientation), L, null, 8, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Unknown", UnityEngine.ScreenOrientation.Unknown);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Portrait", UnityEngine.ScreenOrientation.Portrait);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PortraitUpsideDown", UnityEngine.ScreenOrientation.PortraitUpsideDown);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LandscapeLeft", UnityEngine.ScreenOrientation.LandscapeLeft);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LandscapeRight", UnityEngine.ScreenOrientation.LandscapeRight);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "AutoRotation", UnityEngine.ScreenOrientation.AutoRotation);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Landscape", UnityEngine.ScreenOrientation.Landscape);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.ScreenOrientation), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineScreenOrientation(L, (UnityEngine.ScreenOrientation)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Unknown"))
                {
                    translator.PushUnityEngineScreenOrientation(L, UnityEngine.ScreenOrientation.Unknown);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Portrait"))
                {
                    translator.PushUnityEngineScreenOrientation(L, UnityEngine.ScreenOrientation.Portrait);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "PortraitUpsideDown"))
                {
                    translator.PushUnityEngineScreenOrientation(L, UnityEngine.ScreenOrientation.PortraitUpsideDown);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LandscapeLeft"))
                {
                    translator.PushUnityEngineScreenOrientation(L, UnityEngine.ScreenOrientation.LandscapeLeft);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LandscapeRight"))
                {
                    translator.PushUnityEngineScreenOrientation(L, UnityEngine.ScreenOrientation.LandscapeRight);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "AutoRotation"))
                {
                    translator.PushUnityEngineScreenOrientation(L, UnityEngine.ScreenOrientation.AutoRotation);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Landscape"))
                {
                    translator.PushUnityEngineScreenOrientation(L, UnityEngine.ScreenOrientation.Landscape);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.ScreenOrientation!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.ScreenOrientation! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineTouchPhaseWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.TouchPhase), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.TouchPhase), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.TouchPhase), L, null, 6, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Began", UnityEngine.TouchPhase.Began);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Moved", UnityEngine.TouchPhase.Moved);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Stationary", UnityEngine.TouchPhase.Stationary);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Ended", UnityEngine.TouchPhase.Ended);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Canceled", UnityEngine.TouchPhase.Canceled);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.TouchPhase), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineTouchPhase(L, (UnityEngine.TouchPhase)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Began"))
                {
                    translator.PushUnityEngineTouchPhase(L, UnityEngine.TouchPhase.Began);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Moved"))
                {
                    translator.PushUnityEngineTouchPhase(L, UnityEngine.TouchPhase.Moved);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Stationary"))
                {
                    translator.PushUnityEngineTouchPhase(L, UnityEngine.TouchPhase.Stationary);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Ended"))
                {
                    translator.PushUnityEngineTouchPhase(L, UnityEngine.TouchPhase.Ended);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Canceled"))
                {
                    translator.PushUnityEngineTouchPhase(L, UnityEngine.TouchPhase.Canceled);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.TouchPhase!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.TouchPhase! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class DGTweeningAutoPlayWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(DG.Tweening.AutoPlay), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(DG.Tweening.AutoPlay), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(DG.Tweening.AutoPlay), L, null, 5, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "None", DG.Tweening.AutoPlay.None);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "AutoPlaySequences", DG.Tweening.AutoPlay.AutoPlaySequences);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "AutoPlayTweeners", DG.Tweening.AutoPlay.AutoPlayTweeners);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "All", DG.Tweening.AutoPlay.All);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(DG.Tweening.AutoPlay), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushDGTweeningAutoPlay(L, (DG.Tweening.AutoPlay)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "None"))
                {
                    translator.PushDGTweeningAutoPlay(L, DG.Tweening.AutoPlay.None);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "AutoPlaySequences"))
                {
                    translator.PushDGTweeningAutoPlay(L, DG.Tweening.AutoPlay.AutoPlaySequences);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "AutoPlayTweeners"))
                {
                    translator.PushDGTweeningAutoPlay(L, DG.Tweening.AutoPlay.AutoPlayTweeners);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "All"))
                {
                    translator.PushDGTweeningAutoPlay(L, DG.Tweening.AutoPlay.All);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for DG.Tweening.AutoPlay!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for DG.Tweening.AutoPlay! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class DGTweeningAxisConstraintWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(DG.Tweening.AxisConstraint), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(DG.Tweening.AxisConstraint), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(DG.Tweening.AxisConstraint), L, null, 6, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "None", DG.Tweening.AxisConstraint.None);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "X", DG.Tweening.AxisConstraint.X);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Y", DG.Tweening.AxisConstraint.Y);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Z", DG.Tweening.AxisConstraint.Z);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "W", DG.Tweening.AxisConstraint.W);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(DG.Tweening.AxisConstraint), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushDGTweeningAxisConstraint(L, (DG.Tweening.AxisConstraint)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "None"))
                {
                    translator.PushDGTweeningAxisConstraint(L, DG.Tweening.AxisConstraint.None);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "X"))
                {
                    translator.PushDGTweeningAxisConstraint(L, DG.Tweening.AxisConstraint.X);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Y"))
                {
                    translator.PushDGTweeningAxisConstraint(L, DG.Tweening.AxisConstraint.Y);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Z"))
                {
                    translator.PushDGTweeningAxisConstraint(L, DG.Tweening.AxisConstraint.Z);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "W"))
                {
                    translator.PushDGTweeningAxisConstraint(L, DG.Tweening.AxisConstraint.W);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for DG.Tweening.AxisConstraint!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for DG.Tweening.AxisConstraint! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class DGTweeningEaseWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(DG.Tweening.Ease), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(DG.Tweening.Ease), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(DG.Tweening.Ease), L, null, 39, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Unset", DG.Tweening.Ease.Unset);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Linear", DG.Tweening.Ease.Linear);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InSine", DG.Tweening.Ease.InSine);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OutSine", DG.Tweening.Ease.OutSine);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InOutSine", DG.Tweening.Ease.InOutSine);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InQuad", DG.Tweening.Ease.InQuad);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OutQuad", DG.Tweening.Ease.OutQuad);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InOutQuad", DG.Tweening.Ease.InOutQuad);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InCubic", DG.Tweening.Ease.InCubic);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OutCubic", DG.Tweening.Ease.OutCubic);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InOutCubic", DG.Tweening.Ease.InOutCubic);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InQuart", DG.Tweening.Ease.InQuart);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OutQuart", DG.Tweening.Ease.OutQuart);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InOutQuart", DG.Tweening.Ease.InOutQuart);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InQuint", DG.Tweening.Ease.InQuint);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OutQuint", DG.Tweening.Ease.OutQuint);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InOutQuint", DG.Tweening.Ease.InOutQuint);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InExpo", DG.Tweening.Ease.InExpo);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OutExpo", DG.Tweening.Ease.OutExpo);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InOutExpo", DG.Tweening.Ease.InOutExpo);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InCirc", DG.Tweening.Ease.InCirc);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OutCirc", DG.Tweening.Ease.OutCirc);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InOutCirc", DG.Tweening.Ease.InOutCirc);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InElastic", DG.Tweening.Ease.InElastic);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OutElastic", DG.Tweening.Ease.OutElastic);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InOutElastic", DG.Tweening.Ease.InOutElastic);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InBack", DG.Tweening.Ease.InBack);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OutBack", DG.Tweening.Ease.OutBack);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InOutBack", DG.Tweening.Ease.InOutBack);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InBounce", DG.Tweening.Ease.InBounce);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OutBounce", DG.Tweening.Ease.OutBounce);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InOutBounce", DG.Tweening.Ease.InOutBounce);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Flash", DG.Tweening.Ease.Flash);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InFlash", DG.Tweening.Ease.InFlash);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OutFlash", DG.Tweening.Ease.OutFlash);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InOutFlash", DG.Tweening.Ease.InOutFlash);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "INTERNAL_Zero", DG.Tweening.Ease.INTERNAL_Zero);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "INTERNAL_Custom", DG.Tweening.Ease.INTERNAL_Custom);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(DG.Tweening.Ease), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushDGTweeningEase(L, (DG.Tweening.Ease)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Unset"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.Unset);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Linear"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.Linear);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InSine"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InSine);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OutSine"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.OutSine);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InOutSine"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InOutSine);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InQuad"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InQuad);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OutQuad"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.OutQuad);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InOutQuad"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InOutQuad);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InCubic"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InCubic);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OutCubic"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.OutCubic);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InOutCubic"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InOutCubic);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InQuart"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InQuart);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OutQuart"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.OutQuart);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InOutQuart"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InOutQuart);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InQuint"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InQuint);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OutQuint"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.OutQuint);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InOutQuint"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InOutQuint);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InExpo"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InExpo);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OutExpo"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.OutExpo);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InOutExpo"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InOutExpo);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InCirc"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InCirc);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OutCirc"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.OutCirc);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InOutCirc"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InOutCirc);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InElastic"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InElastic);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OutElastic"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.OutElastic);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InOutElastic"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InOutElastic);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InBack"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InBack);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OutBack"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.OutBack);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InOutBack"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InOutBack);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InBounce"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InBounce);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OutBounce"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.OutBounce);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InOutBounce"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InOutBounce);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Flash"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.Flash);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InFlash"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InFlash);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OutFlash"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.OutFlash);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InOutFlash"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.InOutFlash);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "INTERNAL_Zero"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.INTERNAL_Zero);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "INTERNAL_Custom"))
                {
                    translator.PushDGTweeningEase(L, DG.Tweening.Ease.INTERNAL_Custom);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for DG.Tweening.Ease!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for DG.Tweening.Ease! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class DGTweeningLogBehaviourWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(DG.Tweening.LogBehaviour), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(DG.Tweening.LogBehaviour), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(DG.Tweening.LogBehaviour), L, null, 4, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Default", DG.Tweening.LogBehaviour.Default);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Verbose", DG.Tweening.LogBehaviour.Verbose);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ErrorsOnly", DG.Tweening.LogBehaviour.ErrorsOnly);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(DG.Tweening.LogBehaviour), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushDGTweeningLogBehaviour(L, (DG.Tweening.LogBehaviour)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Default"))
                {
                    translator.PushDGTweeningLogBehaviour(L, DG.Tweening.LogBehaviour.Default);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Verbose"))
                {
                    translator.PushDGTweeningLogBehaviour(L, DG.Tweening.LogBehaviour.Verbose);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ErrorsOnly"))
                {
                    translator.PushDGTweeningLogBehaviour(L, DG.Tweening.LogBehaviour.ErrorsOnly);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for DG.Tweening.LogBehaviour!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for DG.Tweening.LogBehaviour! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class DGTweeningLoopTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(DG.Tweening.LoopType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(DG.Tweening.LoopType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(DG.Tweening.LoopType), L, null, 4, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Restart", DG.Tweening.LoopType.Restart);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Yoyo", DG.Tweening.LoopType.Yoyo);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Incremental", DG.Tweening.LoopType.Incremental);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(DG.Tweening.LoopType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushDGTweeningLoopType(L, (DG.Tweening.LoopType)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Restart"))
                {
                    translator.PushDGTweeningLoopType(L, DG.Tweening.LoopType.Restart);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Yoyo"))
                {
                    translator.PushDGTweeningLoopType(L, DG.Tweening.LoopType.Yoyo);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Incremental"))
                {
                    translator.PushDGTweeningLoopType(L, DG.Tweening.LoopType.Incremental);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for DG.Tweening.LoopType!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for DG.Tweening.LoopType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class DGTweeningPathModeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(DG.Tweening.PathMode), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(DG.Tweening.PathMode), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(DG.Tweening.PathMode), L, null, 5, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Ignore", DG.Tweening.PathMode.Ignore);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Full3D", DG.Tweening.PathMode.Full3D);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TopDown2D", DG.Tweening.PathMode.TopDown2D);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Sidescroller2D", DG.Tweening.PathMode.Sidescroller2D);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(DG.Tweening.PathMode), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushDGTweeningPathMode(L, (DG.Tweening.PathMode)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Ignore"))
                {
                    translator.PushDGTweeningPathMode(L, DG.Tweening.PathMode.Ignore);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Full3D"))
                {
                    translator.PushDGTweeningPathMode(L, DG.Tweening.PathMode.Full3D);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "TopDown2D"))
                {
                    translator.PushDGTweeningPathMode(L, DG.Tweening.PathMode.TopDown2D);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Sidescroller2D"))
                {
                    translator.PushDGTweeningPathMode(L, DG.Tweening.PathMode.Sidescroller2D);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for DG.Tweening.PathMode!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for DG.Tweening.PathMode! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class DGTweeningPathTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(DG.Tweening.PathType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(DG.Tweening.PathType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(DG.Tweening.PathType), L, null, 3, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Linear", DG.Tweening.PathType.Linear);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "CatmullRom", DG.Tweening.PathType.CatmullRom);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(DG.Tweening.PathType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushDGTweeningPathType(L, (DG.Tweening.PathType)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Linear"))
                {
                    translator.PushDGTweeningPathType(L, DG.Tweening.PathType.Linear);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "CatmullRom"))
                {
                    translator.PushDGTweeningPathType(L, DG.Tweening.PathType.CatmullRom);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for DG.Tweening.PathType!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for DG.Tweening.PathType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class DGTweeningRotateModeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(DG.Tweening.RotateMode), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(DG.Tweening.RotateMode), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(DG.Tweening.RotateMode), L, null, 5, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Fast", DG.Tweening.RotateMode.Fast);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "FastBeyond360", DG.Tweening.RotateMode.FastBeyond360);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "WorldAxisAdd", DG.Tweening.RotateMode.WorldAxisAdd);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LocalAxisAdd", DG.Tweening.RotateMode.LocalAxisAdd);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(DG.Tweening.RotateMode), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushDGTweeningRotateMode(L, (DG.Tweening.RotateMode)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Fast"))
                {
                    translator.PushDGTweeningRotateMode(L, DG.Tweening.RotateMode.Fast);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "FastBeyond360"))
                {
                    translator.PushDGTweeningRotateMode(L, DG.Tweening.RotateMode.FastBeyond360);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "WorldAxisAdd"))
                {
                    translator.PushDGTweeningRotateMode(L, DG.Tweening.RotateMode.WorldAxisAdd);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LocalAxisAdd"))
                {
                    translator.PushDGTweeningRotateMode(L, DG.Tweening.RotateMode.LocalAxisAdd);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for DG.Tweening.RotateMode!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for DG.Tweening.RotateMode! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class DGTweeningScrambleModeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(DG.Tweening.ScrambleMode), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(DG.Tweening.ScrambleMode), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(DG.Tweening.ScrambleMode), L, null, 7, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "None", DG.Tweening.ScrambleMode.None);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "All", DG.Tweening.ScrambleMode.All);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Uppercase", DG.Tweening.ScrambleMode.Uppercase);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Lowercase", DG.Tweening.ScrambleMode.Lowercase);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Numerals", DG.Tweening.ScrambleMode.Numerals);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Custom", DG.Tweening.ScrambleMode.Custom);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(DG.Tweening.ScrambleMode), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushDGTweeningScrambleMode(L, (DG.Tweening.ScrambleMode)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "None"))
                {
                    translator.PushDGTweeningScrambleMode(L, DG.Tweening.ScrambleMode.None);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "All"))
                {
                    translator.PushDGTweeningScrambleMode(L, DG.Tweening.ScrambleMode.All);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Uppercase"))
                {
                    translator.PushDGTweeningScrambleMode(L, DG.Tweening.ScrambleMode.Uppercase);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Lowercase"))
                {
                    translator.PushDGTweeningScrambleMode(L, DG.Tweening.ScrambleMode.Lowercase);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Numerals"))
                {
                    translator.PushDGTweeningScrambleMode(L, DG.Tweening.ScrambleMode.Numerals);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Custom"))
                {
                    translator.PushDGTweeningScrambleMode(L, DG.Tweening.ScrambleMode.Custom);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for DG.Tweening.ScrambleMode!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for DG.Tweening.ScrambleMode! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class DGTweeningTweenTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(DG.Tweening.TweenType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(DG.Tweening.TweenType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(DG.Tweening.TweenType), L, null, 4, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Tweener", DG.Tweening.TweenType.Tweener);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Sequence", DG.Tweening.TweenType.Sequence);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Callback", DG.Tweening.TweenType.Callback);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(DG.Tweening.TweenType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushDGTweeningTweenType(L, (DG.Tweening.TweenType)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Tweener"))
                {
                    translator.PushDGTweeningTweenType(L, DG.Tweening.TweenType.Tweener);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Sequence"))
                {
                    translator.PushDGTweeningTweenType(L, DG.Tweening.TweenType.Sequence);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Callback"))
                {
                    translator.PushDGTweeningTweenType(L, DG.Tweening.TweenType.Callback);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for DG.Tweening.TweenType!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for DG.Tweening.TweenType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class DGTweeningUpdateTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(DG.Tweening.UpdateType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(DG.Tweening.UpdateType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(DG.Tweening.UpdateType), L, null, 5, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Normal", DG.Tweening.UpdateType.Normal);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Late", DG.Tweening.UpdateType.Late);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Fixed", DG.Tweening.UpdateType.Fixed);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Manual", DG.Tweening.UpdateType.Manual);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(DG.Tweening.UpdateType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushDGTweeningUpdateType(L, (DG.Tweening.UpdateType)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Normal"))
                {
                    translator.PushDGTweeningUpdateType(L, DG.Tweening.UpdateType.Normal);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Late"))
                {
                    translator.PushDGTweeningUpdateType(L, DG.Tweening.UpdateType.Late);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Fixed"))
                {
                    translator.PushDGTweeningUpdateType(L, DG.Tweening.UpdateType.Fixed);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Manual"))
                {
                    translator.PushDGTweeningUpdateType(L, DG.Tweening.UpdateType.Manual);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for DG.Tweening.UpdateType!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for DG.Tweening.UpdateType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineEventTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.EventType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.EventType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.EventType), L, null, 33, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "MouseDown", UnityEngine.EventType.MouseDown);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "MouseUp", UnityEngine.EventType.MouseUp);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "MouseMove", UnityEngine.EventType.MouseMove);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "MouseDrag", UnityEngine.EventType.MouseDrag);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "KeyDown", UnityEngine.EventType.KeyDown);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "KeyUp", UnityEngine.EventType.KeyUp);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ScrollWheel", UnityEngine.EventType.ScrollWheel);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Repaint", UnityEngine.EventType.Repaint);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Layout", UnityEngine.EventType.Layout);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "DragUpdated", UnityEngine.EventType.DragUpdated);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "DragPerform", UnityEngine.EventType.DragPerform);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "DragExited", UnityEngine.EventType.DragExited);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Ignore", UnityEngine.EventType.Ignore);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Used", UnityEngine.EventType.Used);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ValidateCommand", UnityEngine.EventType.ValidateCommand);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ExecuteCommand", UnityEngine.EventType.ExecuteCommand);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ContextClick", UnityEngine.EventType.ContextClick);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "MouseEnterWindow", UnityEngine.EventType.MouseEnterWindow);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "MouseLeaveWindow", UnityEngine.EventType.MouseLeaveWindow);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.EventType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineEventType(L, (UnityEngine.EventType)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "MouseDown"))
                {
                    translator.PushUnityEngineEventType(L, UnityEngine.EventType.MouseDown);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "MouseUp"))
                {
                    translator.PushUnityEngineEventType(L, UnityEngine.EventType.MouseUp);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "MouseMove"))
                {
                    translator.PushUnityEngineEventType(L, UnityEngine.EventType.MouseMove);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "MouseDrag"))
                {
                    translator.PushUnityEngineEventType(L, UnityEngine.EventType.MouseDrag);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "KeyDown"))
                {
                    translator.PushUnityEngineEventType(L, UnityEngine.EventType.KeyDown);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "KeyUp"))
                {
                    translator.PushUnityEngineEventType(L, UnityEngine.EventType.KeyUp);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ScrollWheel"))
                {
                    translator.PushUnityEngineEventType(L, UnityEngine.EventType.ScrollWheel);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Repaint"))
                {
                    translator.PushUnityEngineEventType(L, UnityEngine.EventType.Repaint);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Layout"))
                {
                    translator.PushUnityEngineEventType(L, UnityEngine.EventType.Layout);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "DragUpdated"))
                {
                    translator.PushUnityEngineEventType(L, UnityEngine.EventType.DragUpdated);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "DragPerform"))
                {
                    translator.PushUnityEngineEventType(L, UnityEngine.EventType.DragPerform);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "DragExited"))
                {
                    translator.PushUnityEngineEventType(L, UnityEngine.EventType.DragExited);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Ignore"))
                {
                    translator.PushUnityEngineEventType(L, UnityEngine.EventType.Ignore);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Used"))
                {
                    translator.PushUnityEngineEventType(L, UnityEngine.EventType.Used);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ValidateCommand"))
                {
                    translator.PushUnityEngineEventType(L, UnityEngine.EventType.ValidateCommand);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ExecuteCommand"))
                {
                    translator.PushUnityEngineEventType(L, UnityEngine.EventType.ExecuteCommand);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ContextClick"))
                {
                    translator.PushUnityEngineEventType(L, UnityEngine.EventType.ContextClick);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "MouseEnterWindow"))
                {
                    translator.PushUnityEngineEventType(L, UnityEngine.EventType.MouseEnterWindow);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "MouseLeaveWindow"))
                {
                    translator.PushUnityEngineEventType(L, UnityEngine.EventType.MouseLeaveWindow);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.EventType!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.EventType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class JWFrameworkIFSIFSStateWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(JW.Framework.IFS.IFSState), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(JW.Framework.IFS.IFSState), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(JW.Framework.IFS.IFSState), L, null, 20, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Start", JW.Framework.IFS.IFSState.Start);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "FirstMoveInit", JW.Framework.IFS.IFSState.FirstMoveInit);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "FirstMoving", JW.Framework.IFS.IFSState.FirstMoving);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "FirstMoveSuccess", JW.Framework.IFS.IFSState.FirstMoveSuccess);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "FirstMoveFailed", JW.Framework.IFS.IFSState.FirstMoveFailed);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "FirstDownloadInit", JW.Framework.IFS.IFSState.FirstDownloadInit);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "FirstDownloading", JW.Framework.IFS.IFSState.FirstDownloading);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "FirstDownloadSuccess", JW.Framework.IFS.IFSState.FirstDownloadSuccess);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "FirstDownloadFailed", JW.Framework.IFS.IFSState.FirstDownloadFailed);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "FirstUnZip", JW.Framework.IFS.IFSState.FirstUnZip);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LocalFileListInit", JW.Framework.IFS.IFSState.LocalFileListInit);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LocalFileListCheck", JW.Framework.IFS.IFSState.LocalFileListCheck);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NetFileListDownload", JW.Framework.IFS.IFSState.NetFileListDownload);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LocalDiffNetFileList", JW.Framework.IFS.IFSState.LocalDiffNetFileList);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "DownloadDiffFileListBegin", JW.Framework.IFS.IFSState.DownloadDiffFileListBegin);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "DownloadingDiff", JW.Framework.IFS.IFSState.DownloadingDiff);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "DownloadDiffSuccess", JW.Framework.IFS.IFSState.DownloadDiffSuccess);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "GenerateLastFileList", JW.Framework.IFS.IFSState.GenerateLastFileList);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Over", JW.Framework.IFS.IFSState.Over);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(JW.Framework.IFS.IFSState), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushJWFrameworkIFSIFSState(L, (JW.Framework.IFS.IFSState)LuaAPI.xlua_tointeger(L, 1));
            }
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Start"))
                {
                    translator.PushJWFrameworkIFSIFSState(L, JW.Framework.IFS.IFSState.Start);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "FirstMoveInit"))
                {
                    translator.PushJWFrameworkIFSIFSState(L, JW.Framework.IFS.IFSState.FirstMoveInit);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "FirstMoving"))
                {
                    translator.PushJWFrameworkIFSIFSState(L, JW.Framework.IFS.IFSState.FirstMoving);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "FirstMoveSuccess"))
                {
                    translator.PushJWFrameworkIFSIFSState(L, JW.Framework.IFS.IFSState.FirstMoveSuccess);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "FirstMoveFailed"))
                {
                    translator.PushJWFrameworkIFSIFSState(L, JW.Framework.IFS.IFSState.FirstMoveFailed);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "FirstDownloadInit"))
                {
                    translator.PushJWFrameworkIFSIFSState(L, JW.Framework.IFS.IFSState.FirstDownloadInit);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "FirstDownloading"))
                {
                    translator.PushJWFrameworkIFSIFSState(L, JW.Framework.IFS.IFSState.FirstDownloading);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "FirstDownloadSuccess"))
                {
                    translator.PushJWFrameworkIFSIFSState(L, JW.Framework.IFS.IFSState.FirstDownloadSuccess);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "FirstDownloadFailed"))
                {
                    translator.PushJWFrameworkIFSIFSState(L, JW.Framework.IFS.IFSState.FirstDownloadFailed);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "FirstUnZip"))
                {
                    translator.PushJWFrameworkIFSIFSState(L, JW.Framework.IFS.IFSState.FirstUnZip);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LocalFileListInit"))
                {
                    translator.PushJWFrameworkIFSIFSState(L, JW.Framework.IFS.IFSState.LocalFileListInit);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LocalFileListCheck"))
                {
                    translator.PushJWFrameworkIFSIFSState(L, JW.Framework.IFS.IFSState.LocalFileListCheck);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "NetFileListDownload"))
                {
                    translator.PushJWFrameworkIFSIFSState(L, JW.Framework.IFS.IFSState.NetFileListDownload);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LocalDiffNetFileList"))
                {
                    translator.PushJWFrameworkIFSIFSState(L, JW.Framework.IFS.IFSState.LocalDiffNetFileList);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "DownloadDiffFileListBegin"))
                {
                    translator.PushJWFrameworkIFSIFSState(L, JW.Framework.IFS.IFSState.DownloadDiffFileListBegin);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "DownloadingDiff"))
                {
                    translator.PushJWFrameworkIFSIFSState(L, JW.Framework.IFS.IFSState.DownloadingDiff);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "DownloadDiffSuccess"))
                {
                    translator.PushJWFrameworkIFSIFSState(L, JW.Framework.IFS.IFSState.DownloadDiffSuccess);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "GenerateLastFileList"))
                {
                    translator.PushJWFrameworkIFSIFSState(L, JW.Framework.IFS.IFSState.GenerateLastFileList);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Over"))
                {
                    translator.PushJWFrameworkIFSIFSState(L, JW.Framework.IFS.IFSState.Over);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for JW.Framework.IFS.IFSState!");
                }
            }
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for JW.Framework.IFS.IFSState! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
}