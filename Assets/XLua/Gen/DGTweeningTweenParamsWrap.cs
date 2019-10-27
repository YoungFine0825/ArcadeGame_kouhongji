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
    public class DGTweeningTweenParamsWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(DG.Tweening.TweenParams);
			Utils.BeginObjectRegister(type, L, translator, 0, 19, 23, 23);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Clear", _m_Clear);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetAutoKill", _m_SetAutoKill);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetId", _m_SetId);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetTarget", _m_SetTarget);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetLoops", _m_SetLoops);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetEase", _m_SetEase);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetRecyclable", _m_SetRecyclable);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetUpdate", _m_SetUpdate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnStart", _m_OnStart);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnPlay", _m_OnPlay);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnRewind", _m_OnRewind);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnUpdate", _m_OnUpdate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnStepComplete", _m_OnStepComplete);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnComplete", _m_OnComplete);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnKill", _m_OnKill);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnWaypointChange", _m_OnWaypointChange);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetDelay", _m_SetDelay);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetRelative", _m_SetRelative);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSpeedBased", _m_SetSpeedBased);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "id", _g_get_id);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "target", _g_get_target);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "updateType", _g_get_updateType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "isIndependentUpdate", _g_get_isIndependentUpdate);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onStart", _g_get_onStart);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onPlay", _g_get_onPlay);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onRewind", _g_get_onRewind);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onUpdate", _g_get_onUpdate);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onStepComplete", _g_get_onStepComplete);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onComplete", _g_get_onComplete);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onKill", _g_get_onKill);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onWaypointChange", _g_get_onWaypointChange);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "isRecyclable", _g_get_isRecyclable);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "isSpeedBased", _g_get_isSpeedBased);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "autoKill", _g_get_autoKill);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "loops", _g_get_loops);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "loopType", _g_get_loopType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "delay", _g_get_delay);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "isRelative", _g_get_isRelative);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "easeType", _g_get_easeType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "customEase", _g_get_customEase);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "easeOvershootOrAmplitude", _g_get_easeOvershootOrAmplitude);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "easePeriod", _g_get_easePeriod);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "id", _s_set_id);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "target", _s_set_target);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "updateType", _s_set_updateType);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "isIndependentUpdate", _s_set_isIndependentUpdate);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onStart", _s_set_onStart);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onPlay", _s_set_onPlay);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onRewind", _s_set_onRewind);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onUpdate", _s_set_onUpdate);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onStepComplete", _s_set_onStepComplete);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onComplete", _s_set_onComplete);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onKill", _s_set_onKill);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onWaypointChange", _s_set_onWaypointChange);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "isRecyclable", _s_set_isRecyclable);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "isSpeedBased", _s_set_isSpeedBased);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "autoKill", _s_set_autoKill);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "loops", _s_set_loops);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "loopType", _s_set_loopType);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "delay", _s_set_delay);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "isRelative", _s_set_isRelative);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "easeType", _s_set_easeType);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "customEase", _s_set_customEase);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "easeOvershootOrAmplitude", _s_set_easeOvershootOrAmplitude);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "easePeriod", _s_set_easePeriod);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 2, 0, 0);
			
			
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Params", DG.Tweening.TweenParams.Params);
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					DG.Tweening.TweenParams __cl_gen_ret = new DG.Tweening.TweenParams();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.TweenParams constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Clear(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.Clear(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAutoKill(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool autoKillOnCompletion = LuaAPI.lua_toboolean(L, 2);
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.SetAutoKill( autoKillOnCompletion );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 1) 
                {
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.SetAutoKill(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.TweenParams.SetAutoKill!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetId(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object id = translator.GetObject(L, 2, typeof(object));
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.SetId( id );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetTarget(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object target = translator.GetObject(L, 2, typeof(object));
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.SetTarget( target );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLoops(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.Nullable<DG.Tweening.LoopType>>(L, 3)) 
                {
                    int loops = LuaAPI.xlua_tointeger(L, 2);
                    System.Nullable<DG.Tweening.LoopType> loopType;translator.Get(L, 3, out loopType);
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.SetLoops( loops, loopType );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int loops = LuaAPI.xlua_tointeger(L, 2);
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.SetLoops( loops );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.TweenParams.SetLoops!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetEase(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& translator.Assignable<UnityEngine.AnimationCurve>(L, 2)) 
                {
                    UnityEngine.AnimationCurve animCurve = (UnityEngine.AnimationCurve)translator.GetObject(L, 2, typeof(UnityEngine.AnimationCurve));
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.SetEase( animCurve );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<DG.Tweening.EaseFunction>(L, 2)) 
                {
                    DG.Tweening.EaseFunction customEase = translator.GetDelegate<DG.Tweening.EaseFunction>(L, 2);
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.SetEase( customEase );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& translator.Assignable<DG.Tweening.Ease>(L, 2)&& translator.Assignable<System.Nullable<float>>(L, 3)&& translator.Assignable<System.Nullable<float>>(L, 4)) 
                {
                    DG.Tweening.Ease ease;translator.Get(L, 2, out ease);
                    System.Nullable<float> overshootOrAmplitude;translator.Get(L, 3, out overshootOrAmplitude);
                    System.Nullable<float> period;translator.Get(L, 4, out period);
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.SetEase( ease, overshootOrAmplitude, period );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& translator.Assignable<DG.Tweening.Ease>(L, 2)&& translator.Assignable<System.Nullable<float>>(L, 3)) 
                {
                    DG.Tweening.Ease ease;translator.Get(L, 2, out ease);
                    System.Nullable<float> overshootOrAmplitude;translator.Get(L, 3, out overshootOrAmplitude);
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.SetEase( ease, overshootOrAmplitude );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<DG.Tweening.Ease>(L, 2)) 
                {
                    DG.Tweening.Ease ease;translator.Get(L, 2, out ease);
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.SetEase( ease );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.TweenParams.SetEase!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetRecyclable(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool recyclable = LuaAPI.lua_toboolean(L, 2);
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.SetRecyclable( recyclable );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 1) 
                {
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.SetRecyclable(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.TweenParams.SetRecyclable!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetUpdate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool isIndependentUpdate = LuaAPI.lua_toboolean(L, 2);
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.SetUpdate( isIndependentUpdate );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& translator.Assignable<DG.Tweening.UpdateType>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    DG.Tweening.UpdateType updateType;translator.Get(L, 2, out updateType);
                    bool isIndependentUpdate = LuaAPI.lua_toboolean(L, 3);
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.SetUpdate( updateType, isIndependentUpdate );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<DG.Tweening.UpdateType>(L, 2)) 
                {
                    DG.Tweening.UpdateType updateType;translator.Get(L, 2, out updateType);
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.SetUpdate( updateType );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.TweenParams.SetUpdate!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnStart(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.TweenCallback action = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.OnStart( action );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnPlay(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.TweenCallback action = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.OnPlay( action );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnRewind(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.TweenCallback action = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.OnRewind( action );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnUpdate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.TweenCallback action = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.OnUpdate( action );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnStepComplete(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.TweenCallback action = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.OnStepComplete( action );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnComplete(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.TweenCallback action = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.OnComplete( action );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnKill(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.TweenCallback action = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.OnKill( action );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnWaypointChange(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.TweenCallback<int> action = translator.GetDelegate<DG.Tweening.TweenCallback<int>>(L, 2);
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.OnWaypointChange( action );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetDelay(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float delay = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.SetDelay( delay );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetRelative(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool isRelative = LuaAPI.lua_toboolean(L, 2);
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.SetRelative( isRelative );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 1) 
                {
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.SetRelative(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.TweenParams.SetRelative!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSpeedBased(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool isSpeedBased = LuaAPI.lua_toboolean(L, 2);
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.SetSpeedBased( isSpeedBased );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 1) 
                {
                    
                        DG.Tweening.TweenParams __cl_gen_ret = __cl_gen_to_be_invoked.SetSpeedBased(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.TweenParams.SetSpeedBased!");
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_id(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, __cl_gen_to_be_invoked.id);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_target(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, __cl_gen_to_be_invoked.target);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_updateType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                translator.PushDGTweeningUpdateType(L, __cl_gen_to_be_invoked.updateType);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isIndependentUpdate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.isIndependentUpdate);
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
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.onStart);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onPlay(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.onPlay);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onRewind(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.onRewind);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onUpdate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.onUpdate);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onStepComplete(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.onStepComplete);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onComplete(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.onComplete);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onKill(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.onKill);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onWaypointChange(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.onWaypointChange);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isRecyclable(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.isRecyclable);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isSpeedBased(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.isSpeedBased);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_autoKill(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.autoKill);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_loops(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.loops);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_loopType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                translator.PushDGTweeningLoopType(L, __cl_gen_to_be_invoked.loopType);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_delay(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.delay);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isRelative(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.isRelative);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_easeType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                translator.PushDGTweeningEase(L, __cl_gen_to_be_invoked.easeType);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_customEase(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.customEase);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_easeOvershootOrAmplitude(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.easeOvershootOrAmplitude);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_easePeriod(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.easePeriod);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_id(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.id = translator.GetObject(L, 2, typeof(object));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_target(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.target = translator.GetObject(L, 2, typeof(object));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_updateType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                DG.Tweening.UpdateType __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.updateType = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_isIndependentUpdate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.isIndependentUpdate = LuaAPI.lua_toboolean(L, 2);
            
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
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.onStart = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onPlay(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.onPlay = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onRewind(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.onRewind = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onUpdate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.onUpdate = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onStepComplete(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.onStepComplete = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onComplete(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.onComplete = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onKill(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.onKill = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onWaypointChange(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.onWaypointChange = translator.GetDelegate<DG.Tweening.TweenCallback<int>>(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_isRecyclable(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.isRecyclable = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_isSpeedBased(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.isSpeedBased = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_autoKill(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.autoKill = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_loops(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.loops = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_loopType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                DG.Tweening.LoopType __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.loopType = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_delay(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.delay = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_isRelative(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.isRelative = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_easeType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                DG.Tweening.Ease __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.easeType = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_customEase(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.customEase = translator.GetDelegate<DG.Tweening.EaseFunction>(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_easeOvershootOrAmplitude(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.easeOvershootOrAmplitude = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_easePeriod(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.TweenParams __cl_gen_to_be_invoked = (DG.Tweening.TweenParams)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.easePeriod = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
