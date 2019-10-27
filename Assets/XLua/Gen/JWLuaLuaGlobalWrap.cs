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
    public class JWLuaLuaGlobalWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(JW.Lua.LuaGlobal);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 2, 5, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "GetAppCodeVersion", _m_GetAppCodeVersion_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "IsDebug", _g_get_IsDebug);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "IsEditor", _g_get_IsEditor);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "IsUsePackRes", _g_get_IsUsePackRes);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "IsWin", _g_get_IsWin);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "IsUseLuaPack", _g_get_IsUseLuaPack);
            
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "JW.Lua.LuaGlobal does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetAppCodeVersion_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string __cl_gen_ret = JW.Lua.LuaGlobal.GetAppCodeVersion(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsDebug(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, JW.Lua.LuaGlobal.IsDebug);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsEditor(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, JW.Lua.LuaGlobal.IsEditor);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsUsePackRes(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, JW.Lua.LuaGlobal.IsUsePackRes);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsWin(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, JW.Lua.LuaGlobal.IsWin);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsUseLuaPack(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, JW.Lua.LuaGlobal.IsUseLuaPack);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
