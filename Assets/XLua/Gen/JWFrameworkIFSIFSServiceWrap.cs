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
    public class JWFrameworkIFSIFSServiceWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(JW.Framework.IFS.IFSService);
			Utils.BeginObjectRegister(type, L, translator, 0, 10, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Initialize", _m_Initialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Uninitialize", _m_Uninitialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "BeginSessionLua", _m_BeginSessionLua);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "BeginSession", _m_BeginSession);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "BeginUpdateChecker", _m_BeginUpdateChecker);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StopUpdateChecker", _m_StopUpdateChecker);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StopSession", _m_StopSession);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "BeginLocalChecker", _m_BeginLocalChecker);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StopLocalChecker", _m_StopLocalChecker);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsSessionBusy", _m_IsSessionBusy);
			
			
			
			
			
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
					
					JW.Framework.IFS.IFSService __cl_gen_ret = new JW.Framework.IFS.IFSService();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.IFS.IFSService constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Initialize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.IFS.IFSService __cl_gen_to_be_invoked = (JW.Framework.IFS.IFSService)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                JW.Framework.IFS.IFSService __cl_gen_to_be_invoked = (JW.Framework.IFS.IFSService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Uninitialize(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BeginSessionLua(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.IFS.IFSService __cl_gen_to_be_invoked = (JW.Framework.IFS.IFSService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string sessionName = LuaAPI.lua_tostring(L, 2);
                    string firstZipName = LuaAPI.lua_tostring(L, 3);
                    string fileListFileName = LuaAPI.lua_tostring(L, 4);
                    string firstZipUrl = LuaAPI.lua_tostring(L, 5);
                    string fileListFileUrl = LuaAPI.lua_tostring(L, 6);
                    string netFileRootUrl = LuaAPI.lua_tostring(L, 7);
                    JW.Framework.IFS.IFSSessionDelegate handler = translator.GetDelegate<JW.Framework.IFS.IFSSessionDelegate>(L, 8);
                    
                    __cl_gen_to_be_invoked.BeginSessionLua( sessionName, firstZipName, fileListFileName, firstZipUrl, fileListFileUrl, netFileRootUrl, handler );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BeginSession(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.IFS.IFSService __cl_gen_to_be_invoked = (JW.Framework.IFS.IFSService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    JW.Framework.IFS.IFSSession ss = (JW.Framework.IFS.IFSSession)translator.GetObject(L, 2, typeof(JW.Framework.IFS.IFSSession));
                    
                    __cl_gen_to_be_invoked.BeginSession( ss );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BeginUpdateChecker(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.IFS.IFSService __cl_gen_to_be_invoked = (JW.Framework.IFS.IFSService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string sName = LuaAPI.lua_tostring(L, 2);
                    string localFileList = LuaAPI.lua_tostring(L, 3);
                    string netFileListUrl = LuaAPI.lua_tostring(L, 4);
                    JW.Framework.IFS.IFSUpdateCheckerDelegate handler = translator.GetDelegate<JW.Framework.IFS.IFSUpdateCheckerDelegate>(L, 5);
                    
                    __cl_gen_to_be_invoked.BeginUpdateChecker( sName, localFileList, netFileListUrl, handler );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StopUpdateChecker(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.IFS.IFSService __cl_gen_to_be_invoked = (JW.Framework.IFS.IFSService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string sName = LuaAPI.lua_tostring(L, 2);
                    
                    __cl_gen_to_be_invoked.StopUpdateChecker( sName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StopSession(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.IFS.IFSService __cl_gen_to_be_invoked = (JW.Framework.IFS.IFSService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string sessionName = LuaAPI.lua_tostring(L, 2);
                    
                    __cl_gen_to_be_invoked.StopSession( sessionName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BeginLocalChecker(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.IFS.IFSService __cl_gen_to_be_invoked = (JW.Framework.IFS.IFSService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string sName = LuaAPI.lua_tostring(L, 2);
                    string localFileList = LuaAPI.lua_tostring(L, 3);
                    JW.Framework.IFS.IFSLocalCheckerDelegate handler = translator.GetDelegate<JW.Framework.IFS.IFSLocalCheckerDelegate>(L, 4);
                    
                    __cl_gen_to_be_invoked.BeginLocalChecker( sName, localFileList, handler );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StopLocalChecker(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.IFS.IFSService __cl_gen_to_be_invoked = (JW.Framework.IFS.IFSService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string sName = LuaAPI.lua_tostring(L, 2);
                    
                    __cl_gen_to_be_invoked.StopLocalChecker( sName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsSessionBusy(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.IFS.IFSService __cl_gen_to_be_invoked = (JW.Framework.IFS.IFSService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.IsSessionBusy(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
