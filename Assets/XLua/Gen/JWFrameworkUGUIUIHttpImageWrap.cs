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
    public class JWFrameworkUGUIUIHttpImageWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(JW.Framework.UGUI.UIHttpImage);
			Utils.BeginObjectRegister(type, L, translator, 0, 4, 8, 8);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Initialize", _m_Initialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetImageUrl", _m_SetImageUrl);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSprite", _m_SetSprite);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetNativeSize", _m_SetNativeSize);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "ImageUrl", _g_get_ImageUrl);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsSetNativeSize", _g_get_IsSetNativeSize);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsCacheTexture", _g_get_IsCacheTexture);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CachedTextureValidDays", _g_get_CachedTextureValidDays);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsForceSetImageUrl", _g_get_IsForceSetImageUrl);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LoadingCover", _g_get_LoadingCover);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CachedNetAssetWidth", _g_get_CachedNetAssetWidth);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CachedNetAssetHeight", _g_get_CachedNetAssetHeight);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "ImageUrl", _s_set_ImageUrl);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "IsSetNativeSize", _s_set_IsSetNativeSize);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "IsCacheTexture", _s_set_IsCacheTexture);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "CachedTextureValidDays", _s_set_CachedTextureValidDays);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "IsForceSetImageUrl", _s_set_IsForceSetImageUrl);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LoadingCover", _s_set_LoadingCover);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "CachedNetAssetWidth", _s_set_CachedNetAssetWidth);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "CachedNetAssetHeight", _s_set_CachedNetAssetHeight);
            
			
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
					
					JW.Framework.UGUI.UIHttpImage __cl_gen_ret = new JW.Framework.UGUI.UIHttpImage();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.UGUI.UIHttpImage constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Initialize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIHttpImage __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIHttpImage)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    JW.Framework.UGUI.UIForm formScript = (JW.Framework.UGUI.UIForm)translator.GetObject(L, 2, typeof(JW.Framework.UGUI.UIForm));
                    
                    __cl_gen_to_be_invoked.Initialize( formScript );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetImageUrl(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIHttpImage __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIHttpImage)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string url = LuaAPI.lua_tostring(L, 2);
                    
                    __cl_gen_to_be_invoked.SetImageUrl( url );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSprite(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIHttpImage __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIHttpImage)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Sprite ss = (UnityEngine.Sprite)translator.GetObject(L, 2, typeof(UnityEngine.Sprite));
                    
                    __cl_gen_to_be_invoked.SetSprite( ss );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetNativeSize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIHttpImage __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIHttpImage)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.SetNativeSize(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ImageUrl(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIHttpImage __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIHttpImage)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.ImageUrl);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsSetNativeSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIHttpImage __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIHttpImage)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsSetNativeSize);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsCacheTexture(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIHttpImage __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIHttpImage)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsCacheTexture);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CachedTextureValidDays(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIHttpImage __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIHttpImage)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.CachedTextureValidDays);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsForceSetImageUrl(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIHttpImage __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIHttpImage)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsForceSetImageUrl);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LoadingCover(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIHttpImage __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIHttpImage)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.LoadingCover);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CachedNetAssetWidth(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIHttpImage __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIHttpImage)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.CachedNetAssetWidth);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CachedNetAssetHeight(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIHttpImage __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIHttpImage)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.CachedNetAssetHeight);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ImageUrl(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIHttpImage __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIHttpImage)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.ImageUrl = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IsSetNativeSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIHttpImage __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIHttpImage)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.IsSetNativeSize = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IsCacheTexture(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIHttpImage __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIHttpImage)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.IsCacheTexture = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CachedTextureValidDays(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIHttpImage __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIHttpImage)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.CachedTextureValidDays = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IsForceSetImageUrl(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIHttpImage __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIHttpImage)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.IsForceSetImageUrl = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LoadingCover(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIHttpImage __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIHttpImage)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.LoadingCover = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CachedNetAssetWidth(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIHttpImage __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIHttpImage)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.CachedNetAssetWidth = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CachedNetAssetHeight(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIHttpImage __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIHttpImage)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.CachedNetAssetHeight = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
