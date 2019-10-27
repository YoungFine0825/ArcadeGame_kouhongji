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
    public class JWFrameworkNetAssetNetAssetServiceWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(JW.Framework.NetAsset.NetAssetService);
			Utils.BeginObjectRegister(type, L, translator, 0, 10, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Initialize", _m_Initialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Uninitialize", _m_Uninitialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetCachedNetImage", _m_GetCachedNetImage);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddCachedNetImage", _m_AddCachedNetImage);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddCachedNetAssetInfoDirect", _m_AddCachedNetAssetInfoDirect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetCachedNetAssetInfoByKey", _m_GetCachedNetAssetInfoByKey);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetCachedNetAssetInfoByUrl", _m_GetCachedNetAssetInfoByUrl);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PrepareNetAssets", _m_PrepareNetAssets);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UnPrepareNetAssets", _m_UnPrepareNetAssets);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CleanSession", _m_CleanSession);
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 1, 1);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "S_CachedNetAssetDirectory", _g_get_S_CachedNetAssetDirectory);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "S_CachedNetAssetDirectory", _s_set_S_CachedNetAssetDirectory);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					JW.Framework.NetAsset.NetAssetService __cl_gen_ret = new JW.Framework.NetAsset.NetAssetService();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.NetAsset.NetAssetService constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Initialize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.NetAsset.NetAssetService __cl_gen_to_be_invoked = (JW.Framework.NetAsset.NetAssetService)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                JW.Framework.NetAsset.NetAssetService __cl_gen_to_be_invoked = (JW.Framework.NetAsset.NetAssetService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Uninitialize(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCachedNetImage(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.NetAsset.NetAssetService __cl_gen_to_be_invoked = (JW.Framework.NetAsset.NetAssetService)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 5&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    string url = LuaAPI.lua_tostring(L, 2);
                    float validDays = (float)LuaAPI.lua_tonumber(L, 3);
                    int width = LuaAPI.xlua_tointeger(L, 4);
                    int height = LuaAPI.xlua_tointeger(L, 5);
                    
                        UnityEngine.Texture2D __cl_gen_ret = __cl_gen_to_be_invoked.GetCachedNetImage( url, validDays, width, height );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    string url = LuaAPI.lua_tostring(L, 2);
                    float validDays = (float)LuaAPI.lua_tonumber(L, 3);
                    int width = LuaAPI.xlua_tointeger(L, 4);
                    
                        UnityEngine.Texture2D __cl_gen_ret = __cl_gen_to_be_invoked.GetCachedNetImage( url, validDays, width );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    string url = LuaAPI.lua_tostring(L, 2);
                    float validDays = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        UnityEngine.Texture2D __cl_gen_ret = __cl_gen_to_be_invoked.GetCachedNetImage( url, validDays );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.NetAsset.NetAssetService.GetCachedNetImage!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddCachedNetImage(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.NetAsset.NetAssetService __cl_gen_to_be_invoked = (JW.Framework.NetAsset.NetAssetService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string url = LuaAPI.lua_tostring(L, 2);
                    int width = LuaAPI.xlua_tointeger(L, 3);
                    int height = LuaAPI.xlua_tointeger(L, 4);
                    byte[] data = LuaAPI.lua_tobytes(L, 5);
                    
                    __cl_gen_to_be_invoked.AddCachedNetImage( url, width, height, data );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddCachedNetAssetInfoDirect(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.NetAsset.NetAssetService __cl_gen_to_be_invoked = (JW.Framework.NetAsset.NetAssetService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string key = LuaAPI.lua_tostring(L, 2);
                    
                    __cl_gen_to_be_invoked.AddCachedNetAssetInfoDirect( key );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCachedNetAssetInfoByKey(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.NetAsset.NetAssetService __cl_gen_to_be_invoked = (JW.Framework.NetAsset.NetAssetService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string key = LuaAPI.lua_tostring(L, 2);
                    
                        JW.Framework.NetAsset.NetAssetInfo __cl_gen_ret = __cl_gen_to_be_invoked.GetCachedNetAssetInfoByKey( key );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCachedNetAssetInfoByUrl(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.NetAsset.NetAssetService __cl_gen_to_be_invoked = (JW.Framework.NetAsset.NetAssetService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string url = LuaAPI.lua_tostring(L, 2);
                    
                        JW.Framework.NetAsset.NetAssetInfo __cl_gen_ret = __cl_gen_to_be_invoked.GetCachedNetAssetInfoByUrl( url );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PrepareNetAssets(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.NetAsset.NetAssetService __cl_gen_to_be_invoked = (JW.Framework.NetAsset.NetAssetService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    XLua.LuaTable prepareUrls = (XLua.LuaTable)translator.GetObject(L, 2, typeof(XLua.LuaTable));
                    System.Action<float> progresHandler = translator.GetDelegate<System.Action<float>>(L, 3);
                    
                        uint __cl_gen_ret = __cl_gen_to_be_invoked.PrepareNetAssets( prepareUrls, progresHandler );
                        LuaAPI.xlua_pushuint(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UnPrepareNetAssets(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.NetAsset.NetAssetService __cl_gen_to_be_invoked = (JW.Framework.NetAsset.NetAssetService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    uint sid = LuaAPI.xlua_touint(L, 2);
                    
                    __cl_gen_to_be_invoked.UnPrepareNetAssets( sid );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CleanSession(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.NetAsset.NetAssetService __cl_gen_to_be_invoked = (JW.Framework.NetAsset.NetAssetService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.CleanSession(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_S_CachedNetAssetDirectory(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, JW.Framework.NetAsset.NetAssetService.S_CachedNetAssetDirectory);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_S_CachedNetAssetDirectory(RealStatePtr L)
        {
		    try {
                
			    JW.Framework.NetAsset.NetAssetService.S_CachedNetAssetDirectory = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
