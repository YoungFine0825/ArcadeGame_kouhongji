#if USE_UNI_LUA
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
    public class DGTweeningCoreABSSequentiableWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(DG.Tweening.Core.ABSSequentiable);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 4, 4);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "tweenType", _g_get_tweenType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "sequencedPosition", _g_get_sequencedPosition);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "sequencedEndPosition", _g_get_sequencedEndPosition);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onStart", _g_get_onStart);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "tweenType", _s_set_tweenType);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "sequencedPosition", _s_set_sequencedPosition);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "sequencedEndPosition", _s_set_sequencedEndPosition);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onStart", _s_set_onStart);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "DG.Tweening.Core.ABSSequentiable does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_tweenType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Core.ABSSequentiable __cl_gen_to_be_invoked = (DG.Tweening.Core.ABSSequentiable)translator.FastGetCSObj(L, 1);
                translator.PushDGTweeningTweenType(L, __cl_gen_to_be_invoked.tweenType);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_sequencedPosition(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Core.ABSSequentiable __cl_gen_to_be_invoked = (DG.Tweening.Core.ABSSequentiable)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.sequencedPosition);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_sequencedEndPosition(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Core.ABSSequentiable __cl_gen_to_be_invoked = (DG.Tweening.Core.ABSSequentiable)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.sequencedEndPosition);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onStart(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Core.ABSSequentiable __cl_gen_to_be_invoked = (DG.Tweening.Core.ABSSequentiable)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.onStart);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_tweenType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Core.ABSSequentiable __cl_gen_to_be_invoked = (DG.Tweening.Core.ABSSequentiable)translator.FastGetCSObj(L, 1);
                DG.Tweening.TweenType __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.tweenType = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_sequencedPosition(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Core.ABSSequentiable __cl_gen_to_be_invoked = (DG.Tweening.Core.ABSSequentiable)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.sequencedPosition = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_sequencedEndPosition(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Core.ABSSequentiable __cl_gen_to_be_invoked = (DG.Tweening.Core.ABSSequentiable)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.sequencedEndPosition = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onStart(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Core.ABSSequentiable __cl_gen_to_be_invoked = (DG.Tweening.Core.ABSSequentiable)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.onStart = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
