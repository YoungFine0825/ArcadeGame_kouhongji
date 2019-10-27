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
    public class JWFrameworkSceneSceneServiceWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(JW.Framework.Scene.SceneService);
			Utils.BeginObjectRegister(type, L, translator, 0, 8, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Initialize", _m_Initialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Uninitialize", _m_Uninitialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RegisterSceneLoaded", _m_RegisterSceneLoaded);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UnRegisterSceneLoaded", _m_UnRegisterSceneLoaded);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetUnitySceneByName", _m_GetUnitySceneByName);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadUnityScene", _m_LoadUnityScene);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadUnitySceneAsync", _m_LoadUnitySceneAsync);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadUnitySceneAsyncLua", _m_LoadUnitySceneAsyncLua);
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					JW.Framework.Scene.SceneService __cl_gen_ret = new JW.Framework.Scene.SceneService();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.Scene.SceneService constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Initialize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Scene.SceneService __cl_gen_to_be_invoked = (JW.Framework.Scene.SceneService)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                JW.Framework.Scene.SceneService __cl_gen_to_be_invoked = (JW.Framework.Scene.SceneService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Uninitialize(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RegisterSceneLoaded(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Scene.SceneService __cl_gen_to_be_invoked = (JW.Framework.Scene.SceneService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Events.UnityAction<UnityEngine.SceneManagement.Scene, UnityEngine.SceneManagement.LoadSceneMode> handler = translator.GetDelegate<UnityEngine.Events.UnityAction<UnityEngine.SceneManagement.Scene, UnityEngine.SceneManagement.LoadSceneMode>>(L, 2);
                    
                    __cl_gen_to_be_invoked.RegisterSceneLoaded( handler );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UnRegisterSceneLoaded(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Scene.SceneService __cl_gen_to_be_invoked = (JW.Framework.Scene.SceneService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Events.UnityAction<UnityEngine.SceneManagement.Scene, UnityEngine.SceneManagement.LoadSceneMode> handler = translator.GetDelegate<UnityEngine.Events.UnityAction<UnityEngine.SceneManagement.Scene, UnityEngine.SceneManagement.LoadSceneMode>>(L, 2);
                    
                    __cl_gen_to_be_invoked.UnRegisterSceneLoaded( handler );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetUnitySceneByName(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Scene.SceneService __cl_gen_to_be_invoked = (JW.Framework.Scene.SceneService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string name = LuaAPI.lua_tostring(L, 2);
                    
                        UnityEngine.SceneManagement.Scene __cl_gen_ret = __cl_gen_to_be_invoked.GetUnitySceneByName( name );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadUnityScene(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Scene.SceneService __cl_gen_to_be_invoked = (JW.Framework.Scene.SceneService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string name = LuaAPI.lua_tostring(L, 2);
                    UnityEngine.SceneManagement.LoadSceneMode mode;translator.Get(L, 3, out mode);
                    
                    __cl_gen_to_be_invoked.LoadUnityScene( name, mode );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadUnitySceneAsync(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Scene.SceneService __cl_gen_to_be_invoked = (JW.Framework.Scene.SceneService)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.SceneManagement.LoadSceneMode>(L, 3)&& translator.Assignable<JW.Framework.Scene.SceneService.OnUnitySceneLoadDelegate>(L, 4)) 
                {
                    string name = LuaAPI.lua_tostring(L, 2);
                    UnityEngine.SceneManagement.LoadSceneMode mode;translator.Get(L, 3, out mode);
                    JW.Framework.Scene.SceneService.OnUnitySceneLoadDelegate handler = translator.GetDelegate<JW.Framework.Scene.SceneService.OnUnitySceneLoadDelegate>(L, 4);
                    
                    __cl_gen_to_be_invoked.LoadUnitySceneAsync( name, mode, handler );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 6&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.SceneManagement.LoadSceneMode>(L, 3)&& (LuaAPI.lua_isnil(L, 4) || LuaAPI.lua_type(L, 4) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)&& translator.Assignable<JW.Framework.Scene.SceneService.OnUnitySceneLoadDelegate>(L, 6)) 
                {
                    string name = LuaAPI.lua_tostring(L, 2);
                    UnityEngine.SceneManagement.LoadSceneMode mode;translator.Get(L, 3, out mode);
                    string lastScene = LuaAPI.lua_tostring(L, 4);
                    bool cleanLast = LuaAPI.lua_toboolean(L, 5);
                    JW.Framework.Scene.SceneService.OnUnitySceneLoadDelegate handler = translator.GetDelegate<JW.Framework.Scene.SceneService.OnUnitySceneLoadDelegate>(L, 6);
                    
                    __cl_gen_to_be_invoked.LoadUnitySceneAsync( name, mode, lastScene, cleanLast, handler );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.Scene.SceneService.LoadUnitySceneAsync!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadUnitySceneAsyncLua(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Scene.SceneService __cl_gen_to_be_invoked = (JW.Framework.Scene.SceneService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string name = LuaAPI.lua_tostring(L, 2);
                    int mode = LuaAPI.xlua_tointeger(L, 3);
                    JW.Framework.Scene.SceneService.OnUnitySceneLoadDelegate handler = translator.GetDelegate<JW.Framework.Scene.SceneService.OnUnitySceneLoadDelegate>(L, 4);
                    
                    __cl_gen_to_be_invoked.LoadUnitySceneAsyncLua( name, mode, handler );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
