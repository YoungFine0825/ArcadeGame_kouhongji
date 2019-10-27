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
    public class JWFrameworkNetAssetNetAssetInfoWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(JW.Framework.NetAsset.NetAssetInfo);
			Utils.BeginObjectRegister(type, L, translator, 0, 2, 5, 5);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CompareTo", _m_CompareTo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetCachedFilePath", _m_GetCachedFilePath);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Key", _g_get_Key);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LastModifyTime", _g_get_LastModifyTime);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "AssetType", _g_get_AssetType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ImageWidth", _g_get_ImageWidth);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ImageHeight", _g_get_ImageHeight);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "Key", _s_set_Key);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LastModifyTime", _s_set_LastModifyTime);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "AssetType", _s_set_AssetType);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ImageWidth", _s_set_ImageWidth);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ImageHeight", _s_set_ImageHeight);
            
			
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
					
					JW.Framework.NetAsset.NetAssetInfo __cl_gen_ret = new JW.Framework.NetAsset.NetAssetInfo();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.NetAsset.NetAssetInfo constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CompareTo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.NetAsset.NetAssetInfo __cl_gen_to_be_invoked = (JW.Framework.NetAsset.NetAssetInfo)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object obj = translator.GetObject(L, 2, typeof(object));
                    
                        int __cl_gen_ret = __cl_gen_to_be_invoked.CompareTo( obj );
                        LuaAPI.xlua_pushinteger(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCachedFilePath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.NetAsset.NetAssetInfo __cl_gen_to_be_invoked = (JW.Framework.NetAsset.NetAssetInfo)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        string __cl_gen_ret = __cl_gen_to_be_invoked.GetCachedFilePath(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Key(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.NetAsset.NetAssetInfo __cl_gen_to_be_invoked = (JW.Framework.NetAsset.NetAssetInfo)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.Key);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LastModifyTime(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.NetAsset.NetAssetInfo __cl_gen_to_be_invoked = (JW.Framework.NetAsset.NetAssetInfo)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.LastModifyTime);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AssetType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.NetAsset.NetAssetInfo __cl_gen_to_be_invoked = (JW.Framework.NetAsset.NetAssetInfo)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.AssetType);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ImageWidth(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.NetAsset.NetAssetInfo __cl_gen_to_be_invoked = (JW.Framework.NetAsset.NetAssetInfo)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.ImageWidth);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ImageHeight(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.NetAsset.NetAssetInfo __cl_gen_to_be_invoked = (JW.Framework.NetAsset.NetAssetInfo)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.ImageHeight);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Key(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.NetAsset.NetAssetInfo __cl_gen_to_be_invoked = (JW.Framework.NetAsset.NetAssetInfo)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.Key = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LastModifyTime(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.NetAsset.NetAssetInfo __cl_gen_to_be_invoked = (JW.Framework.NetAsset.NetAssetInfo)translator.FastGetCSObj(L, 1);
                System.DateTime __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.LastModifyTime = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_AssetType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.NetAsset.NetAssetInfo __cl_gen_to_be_invoked = (JW.Framework.NetAsset.NetAssetInfo)translator.FastGetCSObj(L, 1);
                JW.Framework.NetAsset.NetAssetType __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.AssetType = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ImageWidth(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.NetAsset.NetAssetInfo __cl_gen_to_be_invoked = (JW.Framework.NetAsset.NetAssetInfo)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.ImageWidth = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ImageHeight(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.NetAsset.NetAssetInfo __cl_gen_to_be_invoked = (JW.Framework.NetAsset.NetAssetInfo)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.ImageHeight = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
