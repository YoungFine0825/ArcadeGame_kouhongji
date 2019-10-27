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
    public class RenderHeadsMediaAVProVideoMediaPlayerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(RenderHeads.Media.AVProVideo.MediaPlayer);
			Utils.BeginObjectRegister(type, L, translator, 0, 18, 45, 25);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OpenVideoFromFile", _m_OpenVideoFromFile);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OpenVideoFromBuffer", _m_OpenVideoFromBuffer);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OpenVideoFromNetAsset", _m_OpenVideoFromNetAsset);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "EnableSubtitles", _m_EnableSubtitles);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DisableSubtitles", _m_DisableSubtitles);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CloseVideo", _m_CloseVideo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Play", _m_Play);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Pause", _m_Pause);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Stop", _m_Stop);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Rewind", _m_Rewind);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetCurrentPlatformOptions", _m_GetCurrentPlatformOptions);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetPlatformOptions", _m_GetPlatformOptions);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreatePlatformMediaPlayer", _m_CreatePlatformMediaPlayer);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SaveFrameToPng", _m_SaveFrameToPng);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ExtractFrameAsync", _m_ExtractFrameAsync);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ExtractFrame", _m_ExtractFrame);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetGuiPositionFromVideoIndex", _m_SetGuiPositionFromVideoIndex);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetDebugGuiEnabled", _m_SetDebugGuiEnabled);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "FrameResampler", _g_get_FrameResampler);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "DisplayDebugGUI", _g_get_DisplayDebugGUI);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "DisplayDebugGUIControls", _g_get_DisplayDebugGUIControls);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Persistent", _g_get_Persistent);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Info", _g_get_Info);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Control", _g_get_Control);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Player", _g_get_Player);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "TextureProducer", _g_get_TextureProducer);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Subtitles", _g_get_Subtitles);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Events", _g_get_Events);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "VideoOpened", _g_get_VideoOpened);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "AudioHeadTransform", _g_get_AudioHeadTransform);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "AudioFocusEnabled", _g_get_AudioFocusEnabled);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "AudioFocusOffLevelDB", _g_get_AudioFocusOffLevelDB);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "AudioFocusWidthDegrees", _g_get_AudioFocusWidthDegrees);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "AudioFocusTransform", _g_get_AudioFocusTransform);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PlatformOptionsWindows", _g_get_PlatformOptionsWindows);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PlatformOptionsMacOSX", _g_get_PlatformOptionsMacOSX);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PlatformOptionsIOS", _g_get_PlatformOptionsIOS);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PlatformOptionsTVOS", _g_get_PlatformOptionsTVOS);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PlatformOptionsAndroid", _g_get_PlatformOptionsAndroid);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PlatformOptionsWindowsPhone", _g_get_PlatformOptionsWindowsPhone);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PlatformOptionsWindowsUWP", _g_get_PlatformOptionsWindowsUWP);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PlatformOptionsWebGL", _g_get_PlatformOptionsWebGL);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PlatformOptionsPS4", _g_get_PlatformOptionsPS4);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "SubtitlesEnabled", _g_get_SubtitlesEnabled);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "SubtitlePath", _g_get_SubtitlePath);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "SubtitleLocation", _g_get_SubtitleLocation);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_VideoLocation", _g_get_m_VideoLocation);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_VideoPath", _g_get_m_VideoPath);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_AutoOpen", _g_get_m_AutoOpen);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_AutoStart", _g_get_m_AutoStart);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_Loop", _g_get_m_Loop);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_Volume", _g_get_m_Volume);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_Muted", _g_get_m_Muted);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_PlaybackRate", _g_get_m_PlaybackRate);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_Resample", _g_get_m_Resample);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_ResampleMode", _g_get_m_ResampleMode);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_ResampleBufferSize", _g_get_m_ResampleBufferSize);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_StereoPacking", _g_get_m_StereoPacking);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_AlphaPacking", _g_get_m_AlphaPacking);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_DisplayDebugStereoColorTint", _g_get_m_DisplayDebugStereoColorTint);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_FilterMode", _g_get_m_FilterMode);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_WrapMode", _g_get_m_WrapMode);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_AnisoLevel", _g_get_m_AnisoLevel);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "DisplayDebugGUI", _s_set_DisplayDebugGUI);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "DisplayDebugGUIControls", _s_set_DisplayDebugGUIControls);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "Persistent", _s_set_Persistent);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "AudioHeadTransform", _s_set_AudioHeadTransform);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "AudioFocusEnabled", _s_set_AudioFocusEnabled);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "AudioFocusOffLevelDB", _s_set_AudioFocusOffLevelDB);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "AudioFocusWidthDegrees", _s_set_AudioFocusWidthDegrees);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "AudioFocusTransform", _s_set_AudioFocusTransform);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_VideoLocation", _s_set_m_VideoLocation);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_VideoPath", _s_set_m_VideoPath);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_AutoOpen", _s_set_m_AutoOpen);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_AutoStart", _s_set_m_AutoStart);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_Loop", _s_set_m_Loop);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_Volume", _s_set_m_Volume);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_Muted", _s_set_m_Muted);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_PlaybackRate", _s_set_m_PlaybackRate);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_Resample", _s_set_m_Resample);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_ResampleMode", _s_set_m_ResampleMode);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_ResampleBufferSize", _s_set_m_ResampleBufferSize);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_StereoPacking", _s_set_m_StereoPacking);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_AlphaPacking", _s_set_m_AlphaPacking);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_DisplayDebugStereoColorTint", _s_set_m_DisplayDebugStereoColorTint);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_FilterMode", _s_set_m_FilterMode);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_WrapMode", _s_set_m_WrapMode);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_AnisoLevel", _s_set_m_AnisoLevel);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 5, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "GetPlatform", _m_GetPlatform_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetPlatformOptionsVariable", _m_GetPlatformOptionsVariable_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetPath", _m_GetPath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetFilePath", _m_GetFilePath_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_ret = new RenderHeads.Media.AVProVideo.MediaPlayer();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to RenderHeads.Media.AVProVideo.MediaPlayer constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenVideoFromFile(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 4&& translator.Assignable<RenderHeads.Media.AVProVideo.MediaPlayer.FileLocation>(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    RenderHeads.Media.AVProVideo.MediaPlayer.FileLocation location;translator.Get(L, 2, out location);
                    string path = LuaAPI.lua_tostring(L, 3);
                    bool autoPlay = LuaAPI.lua_toboolean(L, 4);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.OpenVideoFromFile( location, path, autoPlay );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& translator.Assignable<RenderHeads.Media.AVProVideo.MediaPlayer.FileLocation>(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    RenderHeads.Media.AVProVideo.MediaPlayer.FileLocation location;translator.Get(L, 2, out location);
                    string path = LuaAPI.lua_tostring(L, 3);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.OpenVideoFromFile( location, path );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to RenderHeads.Media.AVProVideo.MediaPlayer.OpenVideoFromFile!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenVideoFromBuffer(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    byte[] buffer = LuaAPI.lua_tobytes(L, 2);
                    bool autoPlay = LuaAPI.lua_toboolean(L, 3);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.OpenVideoFromBuffer( buffer, autoPlay );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    byte[] buffer = LuaAPI.lua_tobytes(L, 2);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.OpenVideoFromBuffer( buffer );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to RenderHeads.Media.AVProVideo.MediaPlayer.OpenVideoFromBuffer!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenVideoFromNetAsset(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string url = LuaAPI.lua_tostring(L, 2);
                    UnityEngine.Events.UnityAction<int, int> eventHandler = translator.GetDelegate<UnityEngine.Events.UnityAction<int, int>>(L, 3);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.OpenVideoFromNetAsset( url, eventHandler );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EnableSubtitles(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    RenderHeads.Media.AVProVideo.MediaPlayer.FileLocation fileLocation;translator.Get(L, 2, out fileLocation);
                    string filePath = LuaAPI.lua_tostring(L, 3);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.EnableSubtitles( fileLocation, filePath );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DisableSubtitles(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.DisableSubtitles(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CloseVideo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.CloseVideo(  );
                    
                    
                    
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
            
            
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Play(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Pause(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Pause(  );
                    
                    
                    
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
            
            
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Stop(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Rewind(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool pause = LuaAPI.lua_toboolean(L, 2);
                    
                    __cl_gen_to_be_invoked.Rewind( pause );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetPlatform_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        RenderHeads.Media.AVProVideo.Platform __cl_gen_ret = RenderHeads.Media.AVProVideo.MediaPlayer.GetPlatform(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCurrentPlatformOptions(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        RenderHeads.Media.AVProVideo.MediaPlayer.PlatformOptions __cl_gen_ret = __cl_gen_to_be_invoked.GetCurrentPlatformOptions(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetPlatformOptions(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    RenderHeads.Media.AVProVideo.Platform platform;translator.Get(L, 2, out platform);
                    
                        RenderHeads.Media.AVProVideo.MediaPlayer.PlatformOptions __cl_gen_ret = __cl_gen_to_be_invoked.GetPlatformOptions( platform );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetPlatformOptionsVariable_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    RenderHeads.Media.AVProVideo.Platform platform;translator.Get(L, 1, out platform);
                    
                        string __cl_gen_ret = RenderHeads.Media.AVProVideo.MediaPlayer.GetPlatformOptionsVariable( platform );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetPath_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    RenderHeads.Media.AVProVideo.MediaPlayer.FileLocation location;translator.Get(L, 1, out location);
                    
                        string __cl_gen_ret = RenderHeads.Media.AVProVideo.MediaPlayer.GetPath( location );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetFilePath_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    RenderHeads.Media.AVProVideo.MediaPlayer.FileLocation location;translator.Get(L, 2, out location);
                    
                        string __cl_gen_ret = RenderHeads.Media.AVProVideo.MediaPlayer.GetFilePath( path, location );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreatePlatformMediaPlayer(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        RenderHeads.Media.AVProVideo.BaseMediaPlayer __cl_gen_ret = __cl_gen_to_be_invoked.CreatePlatformMediaPlayer(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SaveFrameToPng(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.SaveFrameToPng(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ExtractFrameAsync(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 6&& translator.Assignable<UnityEngine.Texture2D>(L, 2)&& translator.Assignable<RenderHeads.Media.AVProVideo.MediaPlayer.ProcessExtractedFrame>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)) 
                {
                    UnityEngine.Texture2D target = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
                    RenderHeads.Media.AVProVideo.MediaPlayer.ProcessExtractedFrame callback = translator.GetDelegate<RenderHeads.Media.AVProVideo.MediaPlayer.ProcessExtractedFrame>(L, 3);
                    float timeSeconds = (float)LuaAPI.lua_tonumber(L, 4);
                    bool accurateSeek = LuaAPI.lua_toboolean(L, 5);
                    int timeoutMs = LuaAPI.xlua_tointeger(L, 6);
                    
                    __cl_gen_to_be_invoked.ExtractFrameAsync( target, callback, timeSeconds, accurateSeek, timeoutMs );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 5&& translator.Assignable<UnityEngine.Texture2D>(L, 2)&& translator.Assignable<RenderHeads.Media.AVProVideo.MediaPlayer.ProcessExtractedFrame>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)) 
                {
                    UnityEngine.Texture2D target = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
                    RenderHeads.Media.AVProVideo.MediaPlayer.ProcessExtractedFrame callback = translator.GetDelegate<RenderHeads.Media.AVProVideo.MediaPlayer.ProcessExtractedFrame>(L, 3);
                    float timeSeconds = (float)LuaAPI.lua_tonumber(L, 4);
                    bool accurateSeek = LuaAPI.lua_toboolean(L, 5);
                    
                    __cl_gen_to_be_invoked.ExtractFrameAsync( target, callback, timeSeconds, accurateSeek );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 4&& translator.Assignable<UnityEngine.Texture2D>(L, 2)&& translator.Assignable<RenderHeads.Media.AVProVideo.MediaPlayer.ProcessExtractedFrame>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Texture2D target = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
                    RenderHeads.Media.AVProVideo.MediaPlayer.ProcessExtractedFrame callback = translator.GetDelegate<RenderHeads.Media.AVProVideo.MediaPlayer.ProcessExtractedFrame>(L, 3);
                    float timeSeconds = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    __cl_gen_to_be_invoked.ExtractFrameAsync( target, callback, timeSeconds );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 3&& translator.Assignable<UnityEngine.Texture2D>(L, 2)&& translator.Assignable<RenderHeads.Media.AVProVideo.MediaPlayer.ProcessExtractedFrame>(L, 3)) 
                {
                    UnityEngine.Texture2D target = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
                    RenderHeads.Media.AVProVideo.MediaPlayer.ProcessExtractedFrame callback = translator.GetDelegate<RenderHeads.Media.AVProVideo.MediaPlayer.ProcessExtractedFrame>(L, 3);
                    
                    __cl_gen_to_be_invoked.ExtractFrameAsync( target, callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to RenderHeads.Media.AVProVideo.MediaPlayer.ExtractFrameAsync!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ExtractFrame(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 5&& translator.Assignable<UnityEngine.Texture2D>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    UnityEngine.Texture2D target = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
                    float timeSeconds = (float)LuaAPI.lua_tonumber(L, 3);
                    bool accurateSeek = LuaAPI.lua_toboolean(L, 4);
                    int timeoutMs = LuaAPI.xlua_tointeger(L, 5);
                    
                        UnityEngine.Texture2D __cl_gen_ret = __cl_gen_to_be_invoked.ExtractFrame( target, timeSeconds, accurateSeek, timeoutMs );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& translator.Assignable<UnityEngine.Texture2D>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Texture2D target = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
                    float timeSeconds = (float)LuaAPI.lua_tonumber(L, 3);
                    bool accurateSeek = LuaAPI.lua_toboolean(L, 4);
                    
                        UnityEngine.Texture2D __cl_gen_ret = __cl_gen_to_be_invoked.ExtractFrame( target, timeSeconds, accurateSeek );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& translator.Assignable<UnityEngine.Texture2D>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Texture2D target = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
                    float timeSeconds = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        UnityEngine.Texture2D __cl_gen_ret = __cl_gen_to_be_invoked.ExtractFrame( target, timeSeconds );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<UnityEngine.Texture2D>(L, 2)) 
                {
                    UnityEngine.Texture2D target = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
                    
                        UnityEngine.Texture2D __cl_gen_ret = __cl_gen_to_be_invoked.ExtractFrame( target );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to RenderHeads.Media.AVProVideo.MediaPlayer.ExtractFrame!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetGuiPositionFromVideoIndex(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int index = LuaAPI.xlua_tointeger(L, 2);
                    
                    __cl_gen_to_be_invoked.SetGuiPositionFromVideoIndex( index );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetDebugGuiEnabled(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool bEnabled = LuaAPI.lua_toboolean(L, 2);
                    
                    __cl_gen_to_be_invoked.SetDebugGuiEnabled( bEnabled );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_FrameResampler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.FrameResampler);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DisplayDebugGUI(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.DisplayDebugGUI);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DisplayDebugGUIControls(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.DisplayDebugGUIControls);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Persistent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.Persistent);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Info(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.Info);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Control(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.Control);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Player(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.Player);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_TextureProducer(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.TextureProducer);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Subtitles(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.Subtitles);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Events(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.Events);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_VideoOpened(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.VideoOpened);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AudioHeadTransform(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.AudioHeadTransform);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AudioFocusEnabled(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.AudioFocusEnabled);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AudioFocusOffLevelDB(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.AudioFocusOffLevelDB);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AudioFocusWidthDegrees(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.AudioFocusWidthDegrees);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AudioFocusTransform(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.AudioFocusTransform);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PlatformOptionsWindows(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.PlatformOptionsWindows);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PlatformOptionsMacOSX(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.PlatformOptionsMacOSX);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PlatformOptionsIOS(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.PlatformOptionsIOS);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PlatformOptionsTVOS(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.PlatformOptionsTVOS);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PlatformOptionsAndroid(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.PlatformOptionsAndroid);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PlatformOptionsWindowsPhone(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.PlatformOptionsWindowsPhone);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PlatformOptionsWindowsUWP(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.PlatformOptionsWindowsUWP);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PlatformOptionsWebGL(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.PlatformOptionsWebGL);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PlatformOptionsPS4(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.PlatformOptionsPS4);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SubtitlesEnabled(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.SubtitlesEnabled);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SubtitlePath(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.SubtitlePath);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SubtitleLocation(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.SubtitleLocation);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_VideoLocation(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.m_VideoLocation);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_VideoPath(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.m_VideoPath);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_AutoOpen(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.m_AutoOpen);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_AutoStart(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.m_AutoStart);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_Loop(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.m_Loop);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_Volume(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.m_Volume);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_Muted(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.m_Muted);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_PlaybackRate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.m_PlaybackRate);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_Resample(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.m_Resample);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_ResampleMode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.m_ResampleMode);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_ResampleBufferSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.m_ResampleBufferSize);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_StereoPacking(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.m_StereoPacking);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_AlphaPacking(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.m_AlphaPacking);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_DisplayDebugStereoColorTint(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.m_DisplayDebugStereoColorTint);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_FilterMode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.m_FilterMode);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_WrapMode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.m_WrapMode);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_AnisoLevel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.m_AnisoLevel);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DisplayDebugGUI(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.DisplayDebugGUI = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DisplayDebugGUIControls(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.DisplayDebugGUIControls = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Persistent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.Persistent = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_AudioHeadTransform(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.AudioHeadTransform = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_AudioFocusEnabled(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.AudioFocusEnabled = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_AudioFocusOffLevelDB(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.AudioFocusOffLevelDB = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_AudioFocusWidthDegrees(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.AudioFocusWidthDegrees = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_AudioFocusTransform(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.AudioFocusTransform = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_VideoLocation(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                RenderHeads.Media.AVProVideo.MediaPlayer.FileLocation __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.m_VideoLocation = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_VideoPath(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_VideoPath = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_AutoOpen(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_AutoOpen = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_AutoStart(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_AutoStart = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_Loop(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_Loop = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_Volume(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_Volume = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_Muted(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_Muted = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_PlaybackRate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_PlaybackRate = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_Resample(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_Resample = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_ResampleMode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                RenderHeads.Media.AVProVideo.Resampler.ResampleMode __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.m_ResampleMode = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_ResampleBufferSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_ResampleBufferSize = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_StereoPacking(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                RenderHeads.Media.AVProVideo.StereoPacking __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.m_StereoPacking = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_AlphaPacking(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                RenderHeads.Media.AVProVideo.AlphaPacking __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.m_AlphaPacking = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_DisplayDebugStereoColorTint(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_DisplayDebugStereoColorTint = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_FilterMode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                UnityEngine.FilterMode __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.m_FilterMode = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_WrapMode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                UnityEngine.TextureWrapMode __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.m_WrapMode = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_AnisoLevel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                RenderHeads.Media.AVProVideo.MediaPlayer __cl_gen_to_be_invoked = (RenderHeads.Media.AVProVideo.MediaPlayer)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_AnisoLevel = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
