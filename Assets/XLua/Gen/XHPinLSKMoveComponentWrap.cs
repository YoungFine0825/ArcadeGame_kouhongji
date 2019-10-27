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
    public class XHPinLSKMoveComponentWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(XH.Pin.LSKMoveComponent);
			Utils.BeginObjectRegister(type, L, translator, 0, 12, 6, 5);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSpeed", _m_SetSpeed);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Shoot", _m_Shoot);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Stop", _m_Stop);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AttachToWheel", _m_AttachToWheel);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetColliderEnabled", _m_SetColliderEnabled);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DoParabolaMove", _m_DoParabolaMove);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetPBMGravity", _m_SetPBMGravity);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetPBMRotateRange", _m_SetPBMRotateRange);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetPBMInitSpeedRange", _m_SetPBMInitSpeedRange);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetRotation", _m_SetRotation);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ResetParabola", _m_ResetParabola);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DORotate", _m_DORotate);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "ID", _g_get_ID);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Velocity", _g_get_Velocity);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnHitRotateTarget", _g_get_OnHitRotateTarget);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnHitMoveTarget", _g_get_OnHitMoveTarget);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "TopY", _g_get_TopY);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "_centerOffsetY", _g_get__centerOffsetY);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "ID", _s_set_ID);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnHitRotateTarget", _s_set_OnHitRotateTarget);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnHitMoveTarget", _s_set_OnHitMoveTarget);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "TopY", _s_set_TopY);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "_centerOffsetY", _s_set__centerOffsetY);
            
			
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
					
					XH.Pin.LSKMoveComponent __cl_gen_ret = new XH.Pin.LSKMoveComponent();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to XH.Pin.LSKMoveComponent constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSpeed(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XH.Pin.LSKMoveComponent __cl_gen_to_be_invoked = (XH.Pin.LSKMoveComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float x = (float)LuaAPI.lua_tonumber(L, 2);
                    float y = (float)LuaAPI.lua_tonumber(L, 3);
                    float z = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    __cl_gen_to_be_invoked.SetSpeed( x, y, z );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Shoot(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XH.Pin.LSKMoveComponent __cl_gen_to_be_invoked = (XH.Pin.LSKMoveComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Shoot(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Stop(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XH.Pin.LSKMoveComponent __cl_gen_to_be_invoked = (XH.Pin.LSKMoveComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Stop(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AttachToWheel(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XH.Pin.LSKMoveComponent __cl_gen_to_be_invoked = (XH.Pin.LSKMoveComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Transform target = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    float radius = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    __cl_gen_to_be_invoked.AttachToWheel( target, radius );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetColliderEnabled(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XH.Pin.LSKMoveComponent __cl_gen_to_be_invoked = (XH.Pin.LSKMoveComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool isEnable = LuaAPI.lua_toboolean(L, 2);
                    
                    __cl_gen_to_be_invoked.SetColliderEnabled( isEnable );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DoParabolaMove(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XH.Pin.LSKMoveComponent __cl_gen_to_be_invoked = (XH.Pin.LSKMoveComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool isForward = LuaAPI.lua_toboolean(L, 2);
                    
                    __cl_gen_to_be_invoked.DoParabolaMove( isForward );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPBMGravity(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XH.Pin.LSKMoveComponent __cl_gen_to_be_invoked = (XH.Pin.LSKMoveComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float gravity = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    __cl_gen_to_be_invoked.SetPBMGravity( gravity );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPBMRotateRange(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XH.Pin.LSKMoveComponent __cl_gen_to_be_invoked = (XH.Pin.LSKMoveComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Vector3 min;translator.Get(L, 2, out min);
                    UnityEngine.Vector3 max;translator.Get(L, 3, out max);
                    
                    __cl_gen_to_be_invoked.SetPBMRotateRange( min, max );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPBMInitSpeedRange(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XH.Pin.LSKMoveComponent __cl_gen_to_be_invoked = (XH.Pin.LSKMoveComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Vector3 min;translator.Get(L, 2, out min);
                    UnityEngine.Vector3 max;translator.Get(L, 3, out max);
                    
                    __cl_gen_to_be_invoked.SetPBMInitSpeedRange( min, max );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetRotation(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XH.Pin.LSKMoveComponent __cl_gen_to_be_invoked = (XH.Pin.LSKMoveComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Quaternion rotation;translator.Get(L, 2, out rotation);
                    
                    __cl_gen_to_be_invoked.SetRotation( rotation );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ResetParabola(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XH.Pin.LSKMoveComponent __cl_gen_to_be_invoked = (XH.Pin.LSKMoveComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.ResetParabola(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DORotate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XH.Pin.LSKMoveComponent __cl_gen_to_be_invoked = (XH.Pin.LSKMoveComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.DORotate(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ID(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XH.Pin.LSKMoveComponent __cl_gen_to_be_invoked = (XH.Pin.LSKMoveComponent)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.ID);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Velocity(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XH.Pin.LSKMoveComponent __cl_gen_to_be_invoked = (XH.Pin.LSKMoveComponent)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector3(L, __cl_gen_to_be_invoked.Velocity);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnHitRotateTarget(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XH.Pin.LSKMoveComponent __cl_gen_to_be_invoked = (XH.Pin.LSKMoveComponent)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.OnHitRotateTarget);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnHitMoveTarget(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XH.Pin.LSKMoveComponent __cl_gen_to_be_invoked = (XH.Pin.LSKMoveComponent)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.OnHitMoveTarget);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_TopY(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XH.Pin.LSKMoveComponent __cl_gen_to_be_invoked = (XH.Pin.LSKMoveComponent)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.TopY);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get__centerOffsetY(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XH.Pin.LSKMoveComponent __cl_gen_to_be_invoked = (XH.Pin.LSKMoveComponent)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked._centerOffsetY);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ID(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XH.Pin.LSKMoveComponent __cl_gen_to_be_invoked = (XH.Pin.LSKMoveComponent)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.ID = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnHitRotateTarget(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XH.Pin.LSKMoveComponent __cl_gen_to_be_invoked = (XH.Pin.LSKMoveComponent)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.OnHitRotateTarget = translator.GetDelegate<System.Action<float>>(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnHitMoveTarget(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XH.Pin.LSKMoveComponent __cl_gen_to_be_invoked = (XH.Pin.LSKMoveComponent)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.OnHitMoveTarget = translator.GetDelegate<System.Action<int>>(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_TopY(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XH.Pin.LSKMoveComponent __cl_gen_to_be_invoked = (XH.Pin.LSKMoveComponent)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.TopY = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set__centerOffsetY(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XH.Pin.LSKMoveComponent __cl_gen_to_be_invoked = (XH.Pin.LSKMoveComponent)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked._centerOffsetY = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
