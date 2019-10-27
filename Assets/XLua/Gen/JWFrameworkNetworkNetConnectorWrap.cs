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
    public class JWFrameworkNetworkNetConnectorWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(JW.Framework.Network.NetConnector);
			Utils.BeginObjectRegister(type, L, translator, 0, 9, 3, 3);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Initialize", _m_Initialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Uninitialize", _m_Uninitialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ClearReceiveQueue", _m_ClearReceiveQueue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ClearSendMsg", _m_ClearSendMsg);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OpenConnect", _m_OpenConnect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CloseConnect", _m_CloseConnect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LogicUpdate", _m_LogicUpdate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SendMsg", _m_SendMsg);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SendJsonMsg", _m_SendJsonMsg);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "RecvMsgHook", _g_get_RecvMsgHook);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ConnectHook", _g_get_ConnectHook);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CurSessionState", _g_get_CurSessionState);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "RecvMsgHook", _s_set_RecvMsgHook);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ConnectHook", _s_set_ConnectHook);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "CurSessionState", _s_set_CurSessionState);
            
			
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
					
					JW.Framework.Network.NetConnector __cl_gen_ret = new JW.Framework.Network.NetConnector();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.Network.NetConnector constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Initialize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Network.NetConnector __cl_gen_to_be_invoked = (JW.Framework.Network.NetConnector)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string gateIp = LuaAPI.lua_tostring(L, 2);
                    int gatePort = LuaAPI.xlua_tointeger(L, 3);
                    string gateRoute = LuaAPI.lua_tostring(L, 4);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.Initialize( gateIp, gatePort, gateRoute );
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
            
            
                JW.Framework.Network.NetConnector __cl_gen_to_be_invoked = (JW.Framework.Network.NetConnector)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Uninitialize(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearReceiveQueue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Network.NetConnector __cl_gen_to_be_invoked = (JW.Framework.Network.NetConnector)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.ClearReceiveQueue(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearSendMsg(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Network.NetConnector __cl_gen_to_be_invoked = (JW.Framework.Network.NetConnector)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.ClearSendMsg(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenConnect(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Network.NetConnector __cl_gen_to_be_invoked = (JW.Framework.Network.NetConnector)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.OpenConnect(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CloseConnect(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Network.NetConnector __cl_gen_to_be_invoked = (JW.Framework.Network.NetConnector)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.CloseConnect(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LogicUpdate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Network.NetConnector __cl_gen_to_be_invoked = (JW.Framework.Network.NetConnector)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float deltaTime = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    __cl_gen_to_be_invoked.LogicUpdate( deltaTime );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SendMsg(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Network.NetConnector __cl_gen_to_be_invoked = (JW.Framework.Network.NetConnector)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string route = LuaAPI.lua_tostring(L, 2);
                    string msg = LuaAPI.lua_tostring(L, 3);
                    
                    __cl_gen_to_be_invoked.SendMsg( route, msg );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SendJsonMsg(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Network.NetConnector __cl_gen_to_be_invoked = (JW.Framework.Network.NetConnector)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string route = LuaAPI.lua_tostring(L, 2);
                    SimpleJson.JsonObject data = (SimpleJson.JsonObject)translator.GetObject(L, 3, typeof(SimpleJson.JsonObject));
                    
                    __cl_gen_to_be_invoked.SendJsonMsg( route, data );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RecvMsgHook(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Network.NetConnector __cl_gen_to_be_invoked = (JW.Framework.Network.NetConnector)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.RecvMsgHook);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ConnectHook(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Network.NetConnector __cl_gen_to_be_invoked = (JW.Framework.Network.NetConnector)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.ConnectHook);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CurSessionState(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Network.NetConnector __cl_gen_to_be_invoked = (JW.Framework.Network.NetConnector)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.CurSessionState);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_RecvMsgHook(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Network.NetConnector __cl_gen_to_be_invoked = (JW.Framework.Network.NetConnector)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.RecvMsgHook = translator.GetDelegate<System.Action<string, string>>(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ConnectHook(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Network.NetConnector __cl_gen_to_be_invoked = (JW.Framework.Network.NetConnector)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.ConnectHook = translator.GetDelegate<System.Action<int>>(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CurSessionState(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Network.NetConnector __cl_gen_to_be_invoked = (JW.Framework.Network.NetConnector)translator.FastGetCSObj(L, 1);
                JW.Framework.Network.NetConnector.SessionState __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.CurSessionState = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
