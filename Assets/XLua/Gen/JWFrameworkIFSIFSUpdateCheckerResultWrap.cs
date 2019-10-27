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
    public class JWFrameworkIFSIFSUpdateCheckerResultWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(JW.Framework.IFS.IFSUpdateCheckerResult);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 4, 4);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "CheckName", _g_get_CheckName);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsSuccess", _g_get_IsSuccess);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsHaveUpdate", _g_get_IsHaveUpdate);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "UpdateSize", _g_get_UpdateSize);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "CheckName", _s_set_CheckName);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "IsSuccess", _s_set_IsSuccess);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "IsHaveUpdate", _s_set_IsHaveUpdate);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "UpdateSize", _s_set_UpdateSize);
            
			
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
					
					JW.Framework.IFS.IFSUpdateCheckerResult __cl_gen_ret = new JW.Framework.IFS.IFSUpdateCheckerResult();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.IFS.IFSUpdateCheckerResult constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CheckName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.IFS.IFSUpdateCheckerResult __cl_gen_to_be_invoked = (JW.Framework.IFS.IFSUpdateCheckerResult)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.CheckName);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsSuccess(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.IFS.IFSUpdateCheckerResult __cl_gen_to_be_invoked = (JW.Framework.IFS.IFSUpdateCheckerResult)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsSuccess);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsHaveUpdate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.IFS.IFSUpdateCheckerResult __cl_gen_to_be_invoked = (JW.Framework.IFS.IFSUpdateCheckerResult)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsHaveUpdate);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UpdateSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.IFS.IFSUpdateCheckerResult __cl_gen_to_be_invoked = (JW.Framework.IFS.IFSUpdateCheckerResult)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.UpdateSize);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CheckName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.IFS.IFSUpdateCheckerResult __cl_gen_to_be_invoked = (JW.Framework.IFS.IFSUpdateCheckerResult)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.CheckName = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IsSuccess(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.IFS.IFSUpdateCheckerResult __cl_gen_to_be_invoked = (JW.Framework.IFS.IFSUpdateCheckerResult)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.IsSuccess = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IsHaveUpdate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.IFS.IFSUpdateCheckerResult __cl_gen_to_be_invoked = (JW.Framework.IFS.IFSUpdateCheckerResult)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.IsHaveUpdate = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UpdateSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.IFS.IFSUpdateCheckerResult __cl_gen_to_be_invoked = (JW.Framework.IFS.IFSUpdateCheckerResult)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.UpdateSize = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
