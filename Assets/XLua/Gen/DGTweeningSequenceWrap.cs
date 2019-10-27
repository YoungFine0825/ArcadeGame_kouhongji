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
using DG.Tweening;

namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class DGTweeningSequenceWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(DG.Tweening.Sequence);
			Utils.BeginObjectRegister(type, L, translator, 0, 35, 2, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Reset", _m_Reset);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Validate", _m_Validate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Startup", _m_Startup);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ApplyTween", _m_ApplyTween);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Pause", _m_Pause);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Play", _m_Play);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetAutoKill", _m_SetAutoKill);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetId", _m_SetId);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetTarget", _m_SetTarget);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetLoops", _m_SetLoops);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetEase", _m_SetEase);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetRecyclable", _m_SetRecyclable);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetUpdate", _m_SetUpdate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnStart", _m_OnStart);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnPlay", _m_OnPlay);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnPause", _m_OnPause);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnRewind", _m_OnRewind);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnUpdate", _m_OnUpdate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnStepComplete", _m_OnStepComplete);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnComplete", _m_OnComplete);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnKill", _m_OnKill);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnWaypointChange", _m_OnWaypointChange);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetAs", _m_SetAs);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Append", _m_Append);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Prepend", _m_Prepend);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Join", _m_Join);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Insert", _m_Insert);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AppendInterval", _m_AppendInterval);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PrependInterval", _m_PrependInterval);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AppendCallback", _m_AppendCallback);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PrependCallback", _m_PrependCallback);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InsertCallback", _m_InsertCallback);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetDelay", _m_SetDelay);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetRelative", _m_SetRelative);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSpeedBased", _m_SetSpeedBased);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "sequencedTweens", _g_get_sequencedTweens);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "lastTweenInsertTime", _g_get_lastTweenInsertTime);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "lastTweenInsertTime", _s_set_lastTweenInsertTime);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 9, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "DoPrepend", _m_DoPrepend_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DoInsert", _m_DoInsert_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DoAppendInterval", _m_DoAppendInterval_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DoPrependInterval", _m_DoPrependInterval_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DoInsertCallback", _m_DoInsertCallback_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Setup", _m_Setup_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DoStartup", _m_DoStartup_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DoApplyTween", _m_DoApplyTween_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					DG.Tweening.Sequence __cl_gen_ret = new DG.Tweening.Sequence();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Sequence constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DoPrepend_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    DG.Tweening.Sequence inSequence = (DG.Tweening.Sequence)translator.GetObject(L, 1, typeof(DG.Tweening.Sequence));
                    DG.Tweening.Tween t = (DG.Tweening.Tween)translator.GetObject(L, 2, typeof(DG.Tweening.Tween));
                    
                        DG.Tweening.Sequence __cl_gen_ret = DG.Tweening.Sequence.DoPrepend( inSequence, t );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DoInsert_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    DG.Tweening.Sequence inSequence = (DG.Tweening.Sequence)translator.GetObject(L, 1, typeof(DG.Tweening.Sequence));
                    DG.Tweening.Tween t = (DG.Tweening.Tween)translator.GetObject(L, 2, typeof(DG.Tweening.Tween));
                    float atPosition = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        DG.Tweening.Sequence __cl_gen_ret = DG.Tweening.Sequence.DoInsert( inSequence, t, atPosition );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DoAppendInterval_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    DG.Tweening.Sequence inSequence = (DG.Tweening.Sequence)translator.GetObject(L, 1, typeof(DG.Tweening.Sequence));
                    float interval = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        DG.Tweening.Sequence __cl_gen_ret = DG.Tweening.Sequence.DoAppendInterval( inSequence, interval );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DoPrependInterval_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    DG.Tweening.Sequence inSequence = (DG.Tweening.Sequence)translator.GetObject(L, 1, typeof(DG.Tweening.Sequence));
                    float interval = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        DG.Tweening.Sequence __cl_gen_ret = DG.Tweening.Sequence.DoPrependInterval( inSequence, interval );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DoInsertCallback_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    DG.Tweening.Sequence inSequence = (DG.Tweening.Sequence)translator.GetObject(L, 1, typeof(DG.Tweening.Sequence));
                    DG.Tweening.TweenCallback callback = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
                    float atPosition = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        DG.Tweening.Sequence __cl_gen_ret = DG.Tweening.Sequence.DoInsertCallback( inSequence, callback, atPosition );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Reset(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Reset(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Validate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.Validate(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Startup(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.Startup(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ApplyTween(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float prevPosition = (float)LuaAPI.lua_tonumber(L, 2);
                    int prevCompletedLoops = LuaAPI.xlua_tointeger(L, 3);
                    int newCompletedSteps = LuaAPI.xlua_tointeger(L, 4);
                    bool useInversePosition = LuaAPI.lua_toboolean(L, 5);
                    DG.Tweening.Core.Enums.UpdateMode updateMode;translator.Get(L, 6, out updateMode);
                    DG.Tweening.Core.Enums.UpdateNotice updateNotice;translator.Get(L, 7, out updateNotice);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.ApplyTween( prevPosition, prevCompletedLoops, newCompletedSteps, useInversePosition, updateMode, updateNotice );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Setup_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    DG.Tweening.Sequence s = (DG.Tweening.Sequence)translator.GetObject(L, 1, typeof(DG.Tweening.Sequence));
                    
                    DG.Tweening.Sequence.Setup( s );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DoStartup_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    DG.Tweening.Sequence s = (DG.Tweening.Sequence)translator.GetObject(L, 1, typeof(DG.Tweening.Sequence));
                    
                        bool __cl_gen_ret = DG.Tweening.Sequence.DoStartup( s );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DoApplyTween_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    DG.Tweening.Sequence s = (DG.Tweening.Sequence)translator.GetObject(L, 1, typeof(DG.Tweening.Sequence));
                    float prevPosition = (float)LuaAPI.lua_tonumber(L, 2);
                    int prevCompletedLoops = LuaAPI.xlua_tointeger(L, 3);
                    int newCompletedSteps = LuaAPI.xlua_tointeger(L, 4);
                    bool useInversePosition = LuaAPI.lua_toboolean(L, 5);
                    DG.Tweening.Core.Enums.UpdateMode updateMode;translator.Get(L, 6, out updateMode);
                    
                        bool __cl_gen_ret = DG.Tweening.Sequence.DoApplyTween( s, prevPosition, prevCompletedLoops, newCompletedSteps, useInversePosition, updateMode );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Pause(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.Pause(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Play(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.Play(  );
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
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1) 
                {
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.SetAutoKill(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool autoKillOnCompletion = LuaAPI.lua_toboolean(L, 2);
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.SetAutoKill( autoKillOnCompletion );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Sequence.SetAutoKill!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetId(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object id = translator.GetObject(L, 2, typeof(object));
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.SetId( id );
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
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object target = translator.GetObject(L, 2, typeof(object));
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.SetTarget( target );
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
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int loops = LuaAPI.xlua_tointeger(L, 2);
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.SetLoops( loops );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<DG.Tweening.LoopType>(L, 3)) 
                {
                    int loops = LuaAPI.xlua_tointeger(L, 2);
                    DG.Tweening.LoopType loopType;translator.Get(L, 3, out loopType);
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.SetLoops( loops, loopType );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Sequence.SetLoops!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetEase(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& translator.Assignable<DG.Tweening.Ease>(L, 2)) 
                {
                    DG.Tweening.Ease ease;translator.Get(L, 2, out ease);
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.SetEase( ease );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<UnityEngine.AnimationCurve>(L, 2)) 
                {
                    UnityEngine.AnimationCurve animCurve = (UnityEngine.AnimationCurve)translator.GetObject(L, 2, typeof(UnityEngine.AnimationCurve));
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.SetEase( animCurve );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<DG.Tweening.EaseFunction>(L, 2)) 
                {
                    DG.Tweening.EaseFunction customEase = translator.GetDelegate<DG.Tweening.EaseFunction>(L, 2);
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.SetEase( customEase );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& translator.Assignable<DG.Tweening.Ease>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    DG.Tweening.Ease ease;translator.Get(L, 2, out ease);
                    float overshoot = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.SetEase( ease, overshoot );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& translator.Assignable<DG.Tweening.Ease>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    DG.Tweening.Ease ease;translator.Get(L, 2, out ease);
                    float amplitude = (float)LuaAPI.lua_tonumber(L, 3);
                    float period = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.SetEase( ease, amplitude, period );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Sequence.SetEase!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetRecyclable(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1) 
                {
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.SetRecyclable(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool recyclable = LuaAPI.lua_toboolean(L, 2);
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.SetRecyclable( recyclable );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Sequence.SetRecyclable!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetUpdate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool isIndependentUpdate = LuaAPI.lua_toboolean(L, 2);
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.SetUpdate( isIndependentUpdate );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<DG.Tweening.UpdateType>(L, 2)) 
                {
                    DG.Tweening.UpdateType updateType;translator.Get(L, 2, out updateType);
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.SetUpdate( updateType );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& translator.Assignable<DG.Tweening.UpdateType>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    DG.Tweening.UpdateType updateType;translator.Get(L, 2, out updateType);
                    bool isIndependentUpdate = LuaAPI.lua_toboolean(L, 3);
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.SetUpdate( updateType, isIndependentUpdate );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Sequence.SetUpdate!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnStart(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.TweenCallback action = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.OnStart( action );
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
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.TweenCallback action = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.OnPlay( action );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnPause(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.TweenCallback action = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.OnPause( action );
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
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.TweenCallback action = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.OnRewind( action );
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
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.TweenCallback action = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.OnUpdate( action );
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
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.TweenCallback action = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.OnStepComplete( action );
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
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.TweenCallback action = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.OnComplete( action );
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
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.TweenCallback action = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.OnKill( action );
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
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.TweenCallback<int> action = translator.GetDelegate<DG.Tweening.TweenCallback<int>>(L, 2);
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.OnWaypointChange( action );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAs(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& translator.Assignable<DG.Tweening.Tween>(L, 2)) 
                {
                    DG.Tweening.Tween asTween = (DG.Tweening.Tween)translator.GetObject(L, 2, typeof(DG.Tweening.Tween));
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.SetAs( asTween );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<DG.Tweening.TweenParams>(L, 2)) 
                {
                    DG.Tweening.TweenParams tweenParams = (DG.Tweening.TweenParams)translator.GetObject(L, 2, typeof(DG.Tweening.TweenParams));
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.SetAs( tweenParams );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Sequence.SetAs!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Append(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.Tween t = (DG.Tweening.Tween)translator.GetObject(L, 2, typeof(DG.Tweening.Tween));
                    
                        DG.Tweening.Sequence __cl_gen_ret = __cl_gen_to_be_invoked.Append( t );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Prepend(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.Tween t = (DG.Tweening.Tween)translator.GetObject(L, 2, typeof(DG.Tweening.Tween));
                    
                        DG.Tweening.Sequence __cl_gen_ret = __cl_gen_to_be_invoked.Prepend( t );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Join(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.Tween t = (DG.Tweening.Tween)translator.GetObject(L, 2, typeof(DG.Tweening.Tween));
                    
                        DG.Tweening.Sequence __cl_gen_ret = __cl_gen_to_be_invoked.Join( t );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Insert(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float atPosition = (float)LuaAPI.lua_tonumber(L, 2);
                    DG.Tweening.Tween t = (DG.Tweening.Tween)translator.GetObject(L, 3, typeof(DG.Tweening.Tween));
                    
                        DG.Tweening.Sequence __cl_gen_ret = __cl_gen_to_be_invoked.Insert( atPosition, t );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AppendInterval(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float interval = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        DG.Tweening.Sequence __cl_gen_ret = __cl_gen_to_be_invoked.AppendInterval( interval );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PrependInterval(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float interval = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        DG.Tweening.Sequence __cl_gen_ret = __cl_gen_to_be_invoked.PrependInterval( interval );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AppendCallback(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.TweenCallback callback = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
                    
                        DG.Tweening.Sequence __cl_gen_ret = __cl_gen_to_be_invoked.AppendCallback( callback );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PrependCallback(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.TweenCallback callback = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
                    
                        DG.Tweening.Sequence __cl_gen_ret = __cl_gen_to_be_invoked.PrependCallback( callback );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InsertCallback(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float atPosition = (float)LuaAPI.lua_tonumber(L, 2);
                    DG.Tweening.TweenCallback callback = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 3);
                    
                        DG.Tweening.Sequence __cl_gen_ret = __cl_gen_to_be_invoked.InsertCallback( atPosition, callback );
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
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float delay = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.SetDelay( delay );
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
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1) 
                {
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.SetRelative(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool isRelative = LuaAPI.lua_toboolean(L, 2);
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.SetRelative( isRelative );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Sequence.SetRelative!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSpeedBased(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1) 
                {
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.SetSpeedBased(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool isSpeedBased = LuaAPI.lua_toboolean(L, 2);
                    
                        DG.Tweening.Tween __cl_gen_ret = __cl_gen_to_be_invoked.SetSpeedBased( isSpeedBased );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Sequence.SetSpeedBased!");
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_sequencedTweens(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.sequencedTweens);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_lastTweenInsertTime(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.lastTweenInsertTime);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_lastTweenInsertTime(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Sequence __cl_gen_to_be_invoked = (DG.Tweening.Sequence)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.lastTweenInsertTime = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
