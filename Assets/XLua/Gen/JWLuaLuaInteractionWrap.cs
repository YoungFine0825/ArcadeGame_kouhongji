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
    public class JWLuaLuaInteractionWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(JW.Lua.LuaInteraction);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 26, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "Log", _m_Log_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetUIStateService", _m_GetUIStateService_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetUGUIRoot", _m_GetUGUIRoot_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetAssetService", _m_GetAssetService_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetNetworkService", _m_GetNetworkService_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetNativeService", _m_GetNativeService_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetUICommonService", _m_GetUICommonService_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetAudioService", _m_GetAudioService_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetSceneService", _m_GetSceneService_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetResService", _m_GetResService_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetIFSService", _m_GetIFSService_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetHttpService", _m_GetHttpService_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetArcadeInputService", _m_GetArcadeInputService_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetQualityService", _m_GetQualityService_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetNetAssetService", _m_GetNetAssetService_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ExitApplication", _m_ExitApplication_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LoadAsynAsset", _m_LoadAsynAsset_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "AddUIEventHandle", _m_AddUIEventHandle_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RemoveUIEventHandle", _m_RemoveUIEventHandle_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "AddUIEventListenerHandle", _m_AddUIEventListenerHandle_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RemoveUIEventListenerHandle", _m_RemoveUIEventListenerHandle_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetResourceBytes", _m_GetResourceBytes_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetTextFromFile", _m_GetTextFromFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LoadLuaZip", _m_LoadLuaZip_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "UnLoadLuaZip", _m_UnLoadLuaZip_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "JW.Lua.LuaInteraction does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Log_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    int type = LuaAPI.xlua_tointeger(L, 1);
                    string content = LuaAPI.lua_tostring(L, 2);
                    
                    JW.Lua.LuaInteraction.Log( type, content );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetUIStateService_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        JW.Framework.MVC.UIStateService __cl_gen_ret = JW.Lua.LuaInteraction.GetUIStateService(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetUGUIRoot_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        JW.Framework.UGUI.UGUIRoot __cl_gen_ret = JW.Lua.LuaInteraction.GetUGUIRoot(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetAssetService_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        JW.Framework.Asset.AssetService __cl_gen_ret = JW.Lua.LuaInteraction.GetAssetService(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetNetworkService_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        JW.Framework.Network.NetworkService __cl_gen_ret = JW.Lua.LuaInteraction.GetNetworkService(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetNativeService_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        JW.Framework.Native.NativeService __cl_gen_ret = JW.Lua.LuaInteraction.GetNativeService(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetUICommonService_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        JW.Framework.UGUI.UICommonService __cl_gen_ret = JW.Lua.LuaInteraction.GetUICommonService(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetAudioService_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        JW.Framework.Audio.AudioService __cl_gen_ret = JW.Lua.LuaInteraction.GetAudioService(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetSceneService_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        JW.Framework.Scene.SceneService __cl_gen_ret = JW.Lua.LuaInteraction.GetSceneService(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetResService_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        JW.Res.ResService __cl_gen_ret = JW.Lua.LuaInteraction.GetResService(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetIFSService_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        JW.Framework.IFS.IFSService __cl_gen_ret = JW.Lua.LuaInteraction.GetIFSService(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetHttpService_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        JW.Framework.Http.HttpService __cl_gen_ret = JW.Lua.LuaInteraction.GetHttpService(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetArcadeInputService_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        JW.Framework.ArcadeInput.ArcadeInputService __cl_gen_ret = JW.Lua.LuaInteraction.GetArcadeInputService(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetQualityService_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        JW.Framework.Quality.QualityService __cl_gen_ret = JW.Lua.LuaInteraction.GetQualityService(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetNetAssetService_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        JW.Framework.NetAsset.NetAssetService __cl_gen_ret = JW.Lua.LuaInteraction.GetNetAssetService(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ExitApplication_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    JW.Lua.LuaInteraction.ExitApplication(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadAsynAsset_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    int loadPriority = LuaAPI.xlua_tointeger(L, 1);
                    int type = LuaAPI.xlua_tointeger(L, 2);
                    string filename = LuaAPI.lua_tostring(L, 3);
                    int life = LuaAPI.xlua_tointeger(L, 4);
                    
                    JW.Lua.LuaInteraction.LoadAsynAsset( loadPriority, type, filename, life );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddUIEventHandle_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Events.UnityEvent list = (UnityEngine.Events.UnityEvent)translator.GetObject(L, 1, typeof(UnityEngine.Events.UnityEvent));
                    int id = LuaAPI.xlua_tointeger(L, 2);
                    bool oneShot = LuaAPI.lua_toboolean(L, 3);
                    
                    JW.Lua.LuaInteraction.AddUIEventHandle( list, id, oneShot );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveUIEventHandle_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Events.UnityEvent list = (UnityEngine.Events.UnityEvent)translator.GetObject(L, 1, typeof(UnityEngine.Events.UnityEvent));
                    int id = LuaAPI.xlua_tointeger(L, 2);
                    
                    JW.Lua.LuaInteraction.RemoveUIEventHandle( list, id );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddUIEventListenerHandle_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    JW.Framework.UGUI.UIListenerEvent list = (JW.Framework.UGUI.UIListenerEvent)translator.GetObject(L, 1, typeof(JW.Framework.UGUI.UIListenerEvent));
                    int id = LuaAPI.xlua_tointeger(L, 2);
                    bool oneShot = LuaAPI.lua_toboolean(L, 3);
                    
                    JW.Lua.LuaInteraction.AddUIEventListenerHandle( list, id, oneShot );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveUIEventListenerHandle_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    JW.Framework.UGUI.UIListenerEvent list = (JW.Framework.UGUI.UIListenerEvent)translator.GetObject(L, 1, typeof(JW.Framework.UGUI.UIListenerEvent));
                    int id = LuaAPI.xlua_tointeger(L, 2);
                    
                    JW.Lua.LuaInteraction.RemoveUIEventListenerHandle( list, id );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetResourceBytes_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string resPath = LuaAPI.lua_tostring(L, 1);
                    
                        byte[] __cl_gen_ret = JW.Lua.LuaInteraction.GetResourceBytes( resPath );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTextFromFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string filename = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = JW.Lua.LuaInteraction.GetTextFromFile( filename );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadLuaZip_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string fileName = LuaAPI.lua_tostring(L, 1);
                    
                        bool __cl_gen_ret = JW.Lua.LuaInteraction.LoadLuaZip( fileName );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UnLoadLuaZip_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string fileName = LuaAPI.lua_tostring(L, 1);
                    
                    JW.Lua.LuaInteraction.UnLoadLuaZip( fileName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
