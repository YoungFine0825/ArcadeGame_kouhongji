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
    public class XHPinLSKRotateComponentWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(XH.Pin.LSKRotateComponent);
			Utils.BeginObjectRegister(type, L, translator, 0, 7, 1, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StartRotate", _m_StartRotate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetRadius", _m_GetRadius);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StopRotate", _m_StopRotate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InitData", _m_InitData);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReCaculateSpeed", _m_ReCaculateSpeed);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetSpeed", _m_GetSpeed);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Trick", _m_Trick);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnTrickOver", _g_get_OnTrickOver);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnTrickOver", _s_set_OnTrickOver);
            
			
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
					
					XH.Pin.LSKRotateComponent __cl_gen_ret = new XH.Pin.LSKRotateComponent();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to XH.Pin.LSKRotateComponent constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StartRotate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XH.Pin.LSKRotateComponent __cl_gen_to_be_invoked = (XH.Pin.LSKRotateComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.StartRotate(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetRadius(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XH.Pin.LSKRotateComponent __cl_gen_to_be_invoked = (XH.Pin.LSKRotateComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        float __cl_gen_ret = __cl_gen_to_be_invoked.GetRadius(  );
                        LuaAPI.lua_pushnumber(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StopRotate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XH.Pin.LSKRotateComponent __cl_gen_to_be_invoked = (XH.Pin.LSKRotateComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.StopRotate(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InitData(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XH.Pin.LSKRotateComponent __cl_gen_to_be_invoked = (XH.Pin.LSKRotateComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    XLua.LuaTable table = (XLua.LuaTable)translator.GetObject(L, 2, typeof(XLua.LuaTable));
                    
                    __cl_gen_to_be_invoked.InitData( table );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReCaculateSpeed(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XH.Pin.LSKRotateComponent __cl_gen_to_be_invoked = (XH.Pin.LSKRotateComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.ReCaculateSpeed(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetSpeed(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XH.Pin.LSKRotateComponent __cl_gen_to_be_invoked = (XH.Pin.LSKRotateComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        float __cl_gen_ret = __cl_gen_to_be_invoked.GetSpeed(  );
                        LuaAPI.lua_pushnumber(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Trick(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XH.Pin.LSKRotateComponent __cl_gen_to_be_invoked = (XH.Pin.LSKRotateComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int trickMode = LuaAPI.xlua_tointeger(L, 2);
                    float minAngle = (float)LuaAPI.lua_tonumber(L, 3);
                    float maxAngle = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    __cl_gen_to_be_invoked.Trick( trickMode, minAngle, maxAngle );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnTrickOver(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XH.Pin.LSKRotateComponent __cl_gen_to_be_invoked = (XH.Pin.LSKRotateComponent)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.OnTrickOver);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnTrickOver(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XH.Pin.LSKRotateComponent __cl_gen_to_be_invoked = (XH.Pin.LSKRotateComponent)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.OnTrickOver = translator.GetDelegate<System.Action>(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
