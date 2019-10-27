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
    public class JWFrameworkAssetAssetServiceWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(JW.Framework.Asset.AssetService);
			Utils.BeginObjectRegister(type, L, translator, 0, 17, 1, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Initialize", _m_Initialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Uninitialize", _m_Uninitialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Destroy", _m_Destroy);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StartPreloadResidentBundle", _m_StartPreloadResidentBundle);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StartPreloadAfterResident", _m_StartPreloadAfterResident);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ClearUIStateCache", _m_ClearUIStateCache);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UnloadAllUsingAssets", _m_UnloadAllUsingAssets);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadAsyn", _m_LoadAsyn);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CancelAsyn", _m_CancelAsyn);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadUIAsset", _m_LoadUIAsset);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadModelAsset", _m_LoadModelAsset);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadInstantiateAsset", _m_LoadInstantiateAsset);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadAudioAsset", _m_LoadAudioAsset);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadSpriteAsset", _m_LoadSpriteAsset);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadPrimitiveAsset", _m_LoadPrimitiveAsset);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Unload", _m_Unload);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetAssetManager", _m_GetAssetManager);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "LuaGCHook", _g_get_LuaGCHook);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "LuaGCHook", _s_set_LuaGCHook);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 1, 1);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "AlwaysGc", _g_get_AlwaysGc);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "AlwaysGc", _s_set_AlwaysGc);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					JW.Framework.Asset.AssetService __cl_gen_ret = new JW.Framework.Asset.AssetService();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.Asset.AssetService constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Initialize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Asset.AssetService __cl_gen_to_be_invoked = (JW.Framework.Asset.AssetService)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                JW.Framework.Asset.AssetService __cl_gen_to_be_invoked = (JW.Framework.Asset.AssetService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Uninitialize(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Destroy(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Asset.AssetService __cl_gen_to_be_invoked = (JW.Framework.Asset.AssetService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Destroy(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StartPreloadResidentBundle(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Asset.AssetService __cl_gen_to_be_invoked = (JW.Framework.Asset.AssetService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Action callBack = translator.GetDelegate<System.Action>(L, 2);
                    
                    __cl_gen_to_be_invoked.StartPreloadResidentBundle( callBack );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StartPreloadAfterResident(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Asset.AssetService __cl_gen_to_be_invoked = (JW.Framework.Asset.AssetService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Action<bool> allCompletedCallback = translator.GetDelegate<System.Action<bool>>(L, 2);
                    
                    __cl_gen_to_be_invoked.StartPreloadAfterResident( allCompletedCallback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearUIStateCache(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Asset.AssetService __cl_gen_to_be_invoked = (JW.Framework.Asset.AssetService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    JW.Common.JWArrayList<string> uiStateHistory = (JW.Common.JWArrayList<string>)translator.GetObject(L, 2, typeof(JW.Common.JWArrayList<string>));
                    
                    __cl_gen_to_be_invoked.ClearUIStateCache( uiStateHistory );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UnloadAllUsingAssets(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Asset.AssetService __cl_gen_to_be_invoked = (JW.Framework.Asset.AssetService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.UnloadAllUsingAssets(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadAsyn(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Asset.AssetService __cl_gen_to_be_invoked = (JW.Framework.Asset.AssetService)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 7&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& (LuaAPI.lua_isnil(L, 4) || LuaAPI.lua_type(L, 4) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& translator.Assignable<JW.Framework.Asset.IAssetLoadCallback>(L, 6)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7)) 
                {
                    int type = LuaAPI.xlua_tointeger(L, 2);
                    int loadPriority = LuaAPI.xlua_tointeger(L, 3);
                    string filename = LuaAPI.lua_tostring(L, 4);
                    int life = LuaAPI.xlua_tointeger(L, 5);
                    JW.Framework.Asset.IAssetLoadCallback callback = (JW.Framework.Asset.IAssetLoadCallback)translator.GetObject(L, 6, typeof(JW.Framework.Asset.IAssetLoadCallback));
                    int cnt = LuaAPI.xlua_tointeger(L, 7);
                    
                    __cl_gen_to_be_invoked.LoadAsyn( type, loadPriority, filename, life, callback, cnt );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 6&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& (LuaAPI.lua_isnil(L, 4) || LuaAPI.lua_type(L, 4) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& translator.Assignable<JW.Framework.Asset.IAssetLoadCallback>(L, 6)) 
                {
                    int type = LuaAPI.xlua_tointeger(L, 2);
                    int loadPriority = LuaAPI.xlua_tointeger(L, 3);
                    string filename = LuaAPI.lua_tostring(L, 4);
                    int life = LuaAPI.xlua_tointeger(L, 5);
                    JW.Framework.Asset.IAssetLoadCallback callback = (JW.Framework.Asset.IAssetLoadCallback)translator.GetObject(L, 6, typeof(JW.Framework.Asset.IAssetLoadCallback));
                    
                    __cl_gen_to_be_invoked.LoadAsyn( type, loadPriority, filename, life, callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.Asset.AssetService.LoadAsyn!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CancelAsyn(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Asset.AssetService __cl_gen_to_be_invoked = (JW.Framework.Asset.AssetService)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 3&& translator.Assignable<JW.Framework.Asset.IAssetLoadCallback>(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    JW.Framework.Asset.IAssetLoadCallback callback = (JW.Framework.Asset.IAssetLoadCallback)translator.GetObject(L, 2, typeof(JW.Framework.Asset.IAssetLoadCallback));
                    string assetName = LuaAPI.lua_tostring(L, 3);
                    
                    __cl_gen_to_be_invoked.CancelAsyn( callback, assetName );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 2&& translator.Assignable<JW.Framework.Asset.IAssetLoadCallback>(L, 2)) 
                {
                    JW.Framework.Asset.IAssetLoadCallback callback = (JW.Framework.Asset.IAssetLoadCallback)translator.GetObject(L, 2, typeof(JW.Framework.Asset.IAssetLoadCallback));
                    
                    __cl_gen_to_be_invoked.CancelAsyn( callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.Asset.AssetService.CancelAsyn!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadUIAsset(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Asset.AssetService __cl_gen_to_be_invoked = (JW.Framework.Asset.AssetService)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    string filename = LuaAPI.lua_tostring(L, 2);
                    int lifeType = LuaAPI.xlua_tointeger(L, 3);
                    
                        JW.Framework.Asset.UIAsset __cl_gen_ret = __cl_gen_to_be_invoked.LoadUIAsset( filename, lifeType );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string filename = LuaAPI.lua_tostring(L, 2);
                    
                        JW.Framework.Asset.UIAsset __cl_gen_ret = __cl_gen_to_be_invoked.LoadUIAsset( filename );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.Asset.AssetService.LoadUIAsset!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadModelAsset(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Asset.AssetService __cl_gen_to_be_invoked = (JW.Framework.Asset.AssetService)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    string filename = LuaAPI.lua_tostring(L, 2);
                    int lifeType = LuaAPI.xlua_tointeger(L, 3);
                    
                        JW.Framework.Asset.ModelAsset __cl_gen_ret = __cl_gen_to_be_invoked.LoadModelAsset( filename, lifeType );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string filename = LuaAPI.lua_tostring(L, 2);
                    
                        JW.Framework.Asset.ModelAsset __cl_gen_ret = __cl_gen_to_be_invoked.LoadModelAsset( filename );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.Asset.AssetService.LoadModelAsset!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadInstantiateAsset(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Asset.AssetService __cl_gen_to_be_invoked = (JW.Framework.Asset.AssetService)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    string filename = LuaAPI.lua_tostring(L, 2);
                    int lifeType = LuaAPI.xlua_tointeger(L, 3);
                    
                        JW.Framework.Asset.BaseAsset __cl_gen_ret = __cl_gen_to_be_invoked.LoadInstantiateAsset( filename, lifeType );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string filename = LuaAPI.lua_tostring(L, 2);
                    
                        JW.Framework.Asset.BaseAsset __cl_gen_ret = __cl_gen_to_be_invoked.LoadInstantiateAsset( filename );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.Asset.AssetService.LoadInstantiateAsset!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadAudioAsset(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Asset.AssetService __cl_gen_to_be_invoked = (JW.Framework.Asset.AssetService)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& (LuaAPI.lua_isnil(L, 4) || LuaAPI.lua_type(L, 4) == LuaTypes.LUA_TSTRING)) 
                {
                    string filename = LuaAPI.lua_tostring(L, 2);
                    int lifeType = LuaAPI.xlua_tointeger(L, 3);
                    string fileExt = LuaAPI.lua_tostring(L, 4);
                    
                        JW.Framework.Asset.AudioAsset __cl_gen_ret = __cl_gen_to_be_invoked.LoadAudioAsset( filename, lifeType, fileExt );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    string filename = LuaAPI.lua_tostring(L, 2);
                    int lifeType = LuaAPI.xlua_tointeger(L, 3);
                    
                        JW.Framework.Asset.AudioAsset __cl_gen_ret = __cl_gen_to_be_invoked.LoadAudioAsset( filename, lifeType );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string filename = LuaAPI.lua_tostring(L, 2);
                    
                        JW.Framework.Asset.AudioAsset __cl_gen_ret = __cl_gen_to_be_invoked.LoadAudioAsset( filename );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.Asset.AssetService.LoadAudioAsset!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadSpriteAsset(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Asset.AssetService __cl_gen_to_be_invoked = (JW.Framework.Asset.AssetService)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    string filename = LuaAPI.lua_tostring(L, 2);
                    int lifeType = LuaAPI.xlua_tointeger(L, 3);
                    
                        JW.Framework.Asset.SpriteAsset __cl_gen_ret = __cl_gen_to_be_invoked.LoadSpriteAsset( filename, lifeType );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string filename = LuaAPI.lua_tostring(L, 2);
                    
                        JW.Framework.Asset.SpriteAsset __cl_gen_ret = __cl_gen_to_be_invoked.LoadSpriteAsset( filename );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.Asset.AssetService.LoadSpriteAsset!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadPrimitiveAsset(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Asset.AssetService __cl_gen_to_be_invoked = (JW.Framework.Asset.AssetService)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    string filename = LuaAPI.lua_tostring(L, 2);
                    int lifeType = LuaAPI.xlua_tointeger(L, 3);
                    
                        JW.Framework.Asset.BaseAsset __cl_gen_ret = __cl_gen_to_be_invoked.LoadPrimitiveAsset( filename, lifeType );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string filename = LuaAPI.lua_tostring(L, 2);
                    
                        JW.Framework.Asset.BaseAsset __cl_gen_ret = __cl_gen_to_be_invoked.LoadPrimitiveAsset( filename );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.Asset.AssetService.LoadPrimitiveAsset!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Unload(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Asset.AssetService __cl_gen_to_be_invoked = (JW.Framework.Asset.AssetService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    JW.Framework.Asset.BaseAsset ba = (JW.Framework.Asset.BaseAsset)translator.GetObject(L, 2, typeof(JW.Framework.Asset.BaseAsset));
                    
                    __cl_gen_to_be_invoked.Unload( ba );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetAssetManager(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Asset.AssetService __cl_gen_to_be_invoked = (JW.Framework.Asset.AssetService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        JW.Framework.Asset.AssetManager __cl_gen_ret = __cl_gen_to_be_invoked.GetAssetManager(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AlwaysGc(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, JW.Framework.Asset.AssetService.AlwaysGc);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LuaGCHook(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.AssetService __cl_gen_to_be_invoked = (JW.Framework.Asset.AssetService)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.LuaGCHook);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_AlwaysGc(RealStatePtr L)
        {
		    try {
                
			    JW.Framework.Asset.AssetService.AlwaysGc = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LuaGCHook(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.AssetService __cl_gen_to_be_invoked = (JW.Framework.Asset.AssetService)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.LuaGCHook = translator.GetDelegate<System.Action<bool>>(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
