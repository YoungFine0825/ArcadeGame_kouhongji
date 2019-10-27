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
    public class JWResResServiceWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(JW.Res.ResService);
			Utils.BeginObjectRegister(type, L, translator, 0, 12, 1, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Initialize", _m_Initialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Uninitialize", _m_Uninitialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadResPackConfig", _m_LoadResPackConfig);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "MergeResPackConfig", _m_MergeResPackConfig);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetResourceAsync", _m_GetResourceAsync);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Exists", _m_Exists);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UnloadUnusedAssets", _m_UnloadUnusedAssets);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UnloadAllAssets", _m_UnloadAllAssets);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ClearResCachePath", _m_ClearResCachePath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ClearAllCachePath", _m_ClearAllCachePath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsBinaryResourceExsitInPkg", _m_IsBinaryResourceExsitInPkg);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CheckResourceExist", _m_CheckResourceExist);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "PackConfig", _g_get_PackConfig);
            
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 3, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "GetResource", _m_GetResource_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "UnloadResource", _m_UnloadResource_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					JW.Res.ResService __cl_gen_ret = new JW.Res.ResService();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Res.ResService constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Initialize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Res.ResService __cl_gen_to_be_invoked = (JW.Res.ResService)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                JW.Res.ResService __cl_gen_to_be_invoked = (JW.Res.ResService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Uninitialize(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadResPackConfig(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Res.ResService __cl_gen_to_be_invoked = (JW.Res.ResService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    byte[] filebytes = LuaAPI.lua_tobytes(L, 2);
                    
                    __cl_gen_to_be_invoked.LoadResPackConfig( filebytes );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MergeResPackConfig(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Res.ResService __cl_gen_to_be_invoked = (JW.Res.ResService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string fileName = LuaAPI.lua_tostring(L, 2);
                    
                    __cl_gen_to_be_invoked.MergeResPackConfig( fileName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetResource_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string fullPathInResources = LuaAPI.lua_tostring(L, 1);
                    
                        JW.Res.ResObj __cl_gen_ret = JW.Res.ResService.GetResource( fullPathInResources );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetResourceAsync(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Res.ResService __cl_gen_to_be_invoked = (JW.Res.ResService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 2);
                    
                        JW.Res.ResObjRequest __cl_gen_ret = __cl_gen_to_be_invoked.GetResourceAsync( path );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Exists(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Res.ResService __cl_gen_to_be_invoked = (JW.Res.ResService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string resource = LuaAPI.lua_tostring(L, 2);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.Exists( resource );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UnloadResource_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& translator.Assignable<JW.Res.ResObj>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    JW.Res.ResObj ResObj = (JW.Res.ResObj)translator.GetObject(L, 1, typeof(JW.Res.ResObj));
                    bool immediately = LuaAPI.lua_toboolean(L, 2);
                    
                    JW.Res.ResService.UnloadResource( ResObj, immediately );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 1&& translator.Assignable<JW.Res.ResObj>(L, 1)) 
                {
                    JW.Res.ResObj ResObj = (JW.Res.ResObj)translator.GetObject(L, 1, typeof(JW.Res.ResObj));
                    
                    JW.Res.ResService.UnloadResource( ResObj );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Res.ResService.UnloadResource!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UnloadUnusedAssets(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Res.ResService __cl_gen_to_be_invoked = (JW.Res.ResService)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& translator.Assignable<JW.Common.JWObjList<string>>(L, 2)) 
                {
                    JW.Common.JWObjList<string> willUseAssets = (JW.Common.JWObjList<string>)translator.GetObject(L, 2, typeof(JW.Common.JWObjList<string>));
                    
                    __cl_gen_to_be_invoked.UnloadUnusedAssets( willUseAssets );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 1) 
                {
                    
                    __cl_gen_to_be_invoked.UnloadUnusedAssets(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Res.ResService.UnloadUnusedAssets!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UnloadAllAssets(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Res.ResService __cl_gen_to_be_invoked = (JW.Res.ResService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.UnloadAllAssets(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearResCachePath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Res.ResService __cl_gen_to_be_invoked = (JW.Res.ResService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.ClearResCachePath(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearAllCachePath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Res.ResService __cl_gen_to_be_invoked = (JW.Res.ResService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        JW.Res.ClearCacheResult __cl_gen_ret = __cl_gen_to_be_invoked.ClearAllCachePath(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsBinaryResourceExsitInPkg(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Res.ResService __cl_gen_to_be_invoked = (JW.Res.ResService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string Path = LuaAPI.lua_tostring(L, 2);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.IsBinaryResourceExsitInPkg( Path );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CheckResourceExist(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Res.ResService __cl_gen_to_be_invoked = (JW.Res.ResService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string resourceKey = LuaAPI.lua_tostring(L, 2);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.CheckResourceExist( resourceKey );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PackConfig(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Res.ResService __cl_gen_to_be_invoked = (JW.Res.ResService)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.PackConfig);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
