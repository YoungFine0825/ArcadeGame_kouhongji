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
    public class JWFrameworkMVCUIStateServiceWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(JW.Framework.MVC.UIStateService);
			Utils.BeginObjectRegister(type, L, translator, 0, 10, 2, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Initialize", _m_Initialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Uninitialize", _m_Uninitialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddMediator", _m_AddMediator);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveMediator", _m_RemoveMediator);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetStateParam", _m_SetStateParam);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ChangeState", _m_ChangeState);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PushState", _m_PushState);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PushMultiState", _m_PushMultiState);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PopState", _m_PopState);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ClearState", _m_ClearState);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "CurrentStateName", _g_get_CurrentStateName);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Hook", _g_get_Hook);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "Hook", _s_set_Hook);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 4, 0, 0);
			
			
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UIStateChangeTypeChange", JW.Framework.MVC.UIStateService.UIStateChangeTypeChange);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UIStateChangeTypePush", JW.Framework.MVC.UIStateService.UIStateChangeTypePush);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UIStateChangeTypePop", JW.Framework.MVC.UIStateService.UIStateChangeTypePop);
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					JW.Framework.MVC.UIStateService __cl_gen_ret = new JW.Framework.MVC.UIStateService();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.MVC.UIStateService constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Initialize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.MVC.UIStateService __cl_gen_to_be_invoked = (JW.Framework.MVC.UIStateService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.Initialize(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Uninitialize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.MVC.UIStateService __cl_gen_to_be_invoked = (JW.Framework.MVC.UIStateService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Uninitialize(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddMediator(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.MVC.UIStateService __cl_gen_to_be_invoked = (JW.Framework.MVC.UIStateService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    JW.Framework.MVC.UIMediator mediator = (JW.Framework.MVC.UIMediator)translator.GetObject(L, 2, typeof(JW.Framework.MVC.UIMediator));
                    string[] stateName = (string[])translator.GetObject(L, 3, typeof(string[]));
                    
                    __cl_gen_to_be_invoked.AddMediator( mediator, stateName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveMediator(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.MVC.UIStateService __cl_gen_to_be_invoked = (JW.Framework.MVC.UIStateService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    JW.Framework.MVC.UIMediator mediator = (JW.Framework.MVC.UIMediator)translator.GetObject(L, 2, typeof(JW.Framework.MVC.UIMediator));
                    
                    __cl_gen_to_be_invoked.RemoveMediator( mediator );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetStateParam(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.MVC.UIStateService __cl_gen_to_be_invoked = (JW.Framework.MVC.UIStateService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string stateName = LuaAPI.lua_tostring(L, 2);
                    object stateParam = translator.GetObject(L, 3, typeof(object));
                    
                    __cl_gen_to_be_invoked.SetStateParam( stateName, stateParam );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ChangeState(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.MVC.UIStateService __cl_gen_to_be_invoked = (JW.Framework.MVC.UIStateService)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<object>(L, 3)) 
                {
                    string stateName = LuaAPI.lua_tostring(L, 2);
                    object stateParam = translator.GetObject(L, 3, typeof(object));
                    
                    __cl_gen_to_be_invoked.ChangeState( stateName, stateParam );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string stateName = LuaAPI.lua_tostring(L, 2);
                    
                    __cl_gen_to_be_invoked.ChangeState( stateName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.MVC.UIStateService.ChangeState!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PushState(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.MVC.UIStateService __cl_gen_to_be_invoked = (JW.Framework.MVC.UIStateService)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 5&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<object>(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)&& (LuaAPI.lua_isnil(L, 5) || LuaAPI.lua_type(L, 5) == LuaTypes.LUA_TSTRING)) 
                {
                    string stateName = LuaAPI.lua_tostring(L, 2);
                    object stateParam = translator.GetObject(L, 3, typeof(object));
                    bool allowLoadAsset = LuaAPI.lua_toboolean(L, 4);
                    string oldStateName = LuaAPI.lua_tostring(L, 5);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.PushState( stateName, stateParam, allowLoadAsset, oldStateName );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<object>(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    string stateName = LuaAPI.lua_tostring(L, 2);
                    object stateParam = translator.GetObject(L, 3, typeof(object));
                    bool allowLoadAsset = LuaAPI.lua_toboolean(L, 4);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.PushState( stateName, stateParam, allowLoadAsset );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<object>(L, 3)) 
                {
                    string stateName = LuaAPI.lua_tostring(L, 2);
                    object stateParam = translator.GetObject(L, 3, typeof(object));
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.PushState( stateName, stateParam );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string stateName = LuaAPI.lua_tostring(L, 2);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.PushState( stateName );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.MVC.UIStateService.PushState!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PushMultiState(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.MVC.UIStateService __cl_gen_to_be_invoked = (JW.Framework.MVC.UIStateService)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<object>(L, 3)) 
                {
                    string stateName = LuaAPI.lua_tostring(L, 2);
                    object lastStateParam = translator.GetObject(L, 3, typeof(object));
                    
                    __cl_gen_to_be_invoked.PushMultiState( stateName, lastStateParam );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string stateName = LuaAPI.lua_tostring(L, 2);
                    
                    __cl_gen_to_be_invoked.PushMultiState( stateName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.MVC.UIStateService.PushMultiState!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PopState(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.MVC.UIStateService __cl_gen_to_be_invoked = (JW.Framework.MVC.UIStateService)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 3&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)&& translator.Assignable<object>(L, 3)) 
                {
                    bool callChangeStateHandler = LuaAPI.lua_toboolean(L, 2);
                    object newPopStateParam = translator.GetObject(L, 3, typeof(object));
                    
                    __cl_gen_to_be_invoked.PopState( callChangeStateHandler, newPopStateParam );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool callChangeStateHandler = LuaAPI.lua_toboolean(L, 2);
                    
                    __cl_gen_to_be_invoked.PopState( callChangeStateHandler );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 1) 
                {
                    
                    __cl_gen_to_be_invoked.PopState(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.MVC.UIStateService.PopState!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearState(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.MVC.UIStateService __cl_gen_to_be_invoked = (JW.Framework.MVC.UIStateService)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string stateName = LuaAPI.lua_tostring(L, 2);
                    
                    __cl_gen_to_be_invoked.ClearState( stateName );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 1) 
                {
                    
                    __cl_gen_to_be_invoked.ClearState(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.MVC.UIStateService.ClearState!");
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CurrentStateName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.MVC.UIStateService __cl_gen_to_be_invoked = (JW.Framework.MVC.UIStateService)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.CurrentStateName);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Hook(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.MVC.UIStateService __cl_gen_to_be_invoked = (JW.Framework.MVC.UIStateService)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.Hook);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Hook(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.MVC.UIStateService __cl_gen_to_be_invoked = (JW.Framework.MVC.UIStateService)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.Hook = translator.GetDelegate<JW.Framework.MVC.ChangeUIStateHookDelegate>(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
