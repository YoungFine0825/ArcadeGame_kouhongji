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
    public class JWFrameworkAudioAudioServiceWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(JW.Framework.Audio.AudioService);
			Utils.BeginObjectRegister(type, L, translator, 0, 15, 10, 9);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Initialize", _m_Initialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Uninitialize", _m_Uninitialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Play", _m_Play);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Replay", _m_Replay);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Stop", _m_Stop);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StopAll", _m_StopAll);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CloseAll", _m_CloseAll);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OpenAll", _m_OpenAll);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetVolumeByChannel", _m_GetVolumeByChannel);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetBGMVolume", _m_GetBGMVolume);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetChannelVolume", _m_SetChannelVolume);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ChangeVolume", _m_ChangeVolume);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FadeIn", _m_FadeIn);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FadeOut", _m_FadeOut);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FadeToTargetVolume", _m_FadeToTargetVolume);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "RootTf", _g_get_RootTf);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_fCurBGMVolume", _g_get_m_fCurBGMVolume);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_fCurUIVolume", _g_get_m_fCurUIVolume);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_fCurEFVolume", _g_get_m_fCurEFVolume);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_fCurVoiceVolume", _g_get_m_fCurVoiceVolume);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_kCurBGMName", _g_get_m_kCurBGMName);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_fLastBGMVolume", _g_get_m_fLastBGMVolume);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_fLastUIVolume", _g_get_m_fLastUIVolume);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_fLastEFVolume", _g_get_m_fLastEFVolume);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_fLastVoiceVolume", _g_get_m_fLastVoiceVolume);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_fCurBGMVolume", _s_set_m_fCurBGMVolume);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_fCurUIVolume", _s_set_m_fCurUIVolume);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_fCurEFVolume", _s_set_m_fCurEFVolume);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_fCurVoiceVolume", _s_set_m_fCurVoiceVolume);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_kCurBGMName", _s_set_m_kCurBGMName);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_fLastBGMVolume", _s_set_m_fLastBGMVolume);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_fLastUIVolume", _s_set_m_fLastUIVolume);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_fLastEFVolume", _s_set_m_fLastEFVolume);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_fLastVoiceVolume", _s_set_m_fLastVoiceVolume);
            
			
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
					
					JW.Framework.Audio.AudioService __cl_gen_ret = new JW.Framework.Audio.AudioService();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.Audio.AudioService constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Initialize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Uninitialize(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Play(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)) 
                {
                    uint eChannelType = LuaAPI.xlua_touint(L, 2);
                    string resName = LuaAPI.lua_tostring(L, 3);
                    bool loop = LuaAPI.lua_toboolean(L, 4);
                    bool isNetAsset = LuaAPI.lua_toboolean(L, 5);
                    
                    __cl_gen_to_be_invoked.Play( eChannelType, resName, loop, isNetAsset );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    uint eChannelType = LuaAPI.xlua_touint(L, 2);
                    string resName = LuaAPI.lua_tostring(L, 3);
                    bool loop = LuaAPI.lua_toboolean(L, 4);
                    
                    __cl_gen_to_be_invoked.Play( eChannelType, resName, loop );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)) 
                {
                    uint eChannelType = LuaAPI.xlua_touint(L, 2);
                    string resName = LuaAPI.lua_tostring(L, 3);
                    float volumeScale = (float)LuaAPI.lua_tonumber(L, 4);
                    bool loop = LuaAPI.lua_toboolean(L, 5);
                    
                    __cl_gen_to_be_invoked.Play( eChannelType, resName, volumeScale, loop );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    uint eChannelType = LuaAPI.xlua_touint(L, 2);
                    string resName = LuaAPI.lua_tostring(L, 3);
                    float volumeScale = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    __cl_gen_to_be_invoked.Play( eChannelType, resName, volumeScale );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.Audio.AudioService.Play!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Replay(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    uint eChannelType = LuaAPI.xlua_touint(L, 2);
                    string resName = LuaAPI.lua_tostring(L, 3);
                    float volume = (float)LuaAPI.lua_tonumber(L, 4);
                    bool loop = LuaAPI.lua_toboolean(L, 5);
                    
                    __cl_gen_to_be_invoked.Replay( eChannelType, resName, volume, loop );
                    
                    
                    
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
            
            
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    uint eChannelType = LuaAPI.xlua_touint(L, 2);
                    
                    __cl_gen_to_be_invoked.Stop( eChannelType );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StopAll(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.StopAll(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CloseAll(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.CloseAll(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenAll(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.OpenAll(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetVolumeByChannel(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    uint eChannelType = LuaAPI.xlua_touint(L, 2);
                    
                        float __cl_gen_ret = __cl_gen_to_be_invoked.GetVolumeByChannel( eChannelType );
                        LuaAPI.lua_pushnumber(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetBGMVolume(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        float __cl_gen_ret = __cl_gen_to_be_invoked.GetBGMVolume(  );
                        LuaAPI.lua_pushnumber(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetChannelVolume(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    uint eChannelType = LuaAPI.xlua_touint(L, 2);
                    float in_value = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    __cl_gen_to_be_invoked.SetChannelVolume( eChannelType, in_value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ChangeVolume(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    uint eChannelType = LuaAPI.xlua_touint(L, 2);
                    float in_value = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    __cl_gen_to_be_invoked.ChangeVolume( eChannelType, in_value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FadeIn(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    uint eChannelType = LuaAPI.xlua_touint(L, 2);
                    
                    __cl_gen_to_be_invoked.FadeIn( eChannelType );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    uint eChannelType = LuaAPI.xlua_touint(L, 2);
                    string audioResName = LuaAPI.lua_tostring(L, 3);
                    
                    __cl_gen_to_be_invoked.FadeIn( eChannelType, audioResName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.Audio.AudioService.FadeIn!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FadeOut(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    uint eChannelType = LuaAPI.xlua_touint(L, 2);
                    
                    __cl_gen_to_be_invoked.FadeOut( eChannelType );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    uint eChannelType = LuaAPI.xlua_touint(L, 2);
                    string audioResName = LuaAPI.lua_tostring(L, 3);
                    
                    __cl_gen_to_be_invoked.FadeOut( eChannelType, audioResName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.Audio.AudioService.FadeOut!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FadeToTargetVolume(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    uint eChannelType = LuaAPI.xlua_touint(L, 2);
                    float myTargetVolume = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    __cl_gen_to_be_invoked.FadeToTargetVolume( eChannelType, myTargetVolume );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RootTf(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.RootTf);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_fCurBGMVolume(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.m_fCurBGMVolume);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_fCurUIVolume(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.m_fCurUIVolume);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_fCurEFVolume(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.m_fCurEFVolume);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_fCurVoiceVolume(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.m_fCurVoiceVolume);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_kCurBGMName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.m_kCurBGMName);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_fLastBGMVolume(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.m_fLastBGMVolume);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_fLastUIVolume(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.m_fLastUIVolume);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_fLastEFVolume(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.m_fLastEFVolume);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_fLastVoiceVolume(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.m_fLastVoiceVolume);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_fCurBGMVolume(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_fCurBGMVolume = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_fCurUIVolume(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_fCurUIVolume = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_fCurEFVolume(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_fCurEFVolume = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_fCurVoiceVolume(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_fCurVoiceVolume = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_kCurBGMName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_kCurBGMName = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_fLastBGMVolume(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_fLastBGMVolume = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_fLastUIVolume(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_fLastUIVolume = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_fLastEFVolume(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_fLastEFVolume = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_fLastVoiceVolume(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Audio.AudioService __cl_gen_to_be_invoked = (JW.Framework.Audio.AudioService)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_fLastVoiceVolume = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
