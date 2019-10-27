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
    public class JWFrameworkUGUIUIFormWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(JW.Framework.UGUI.UIForm);
			Utils.BeginObjectRegister(type, L, translator, 0, 30, 16, 16);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "MatchScreen", _m_MatchScreen);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Initialize", _m_Initialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CustomUpdate", _m_CustomUpdate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CustomLateUpdate", _m_CustomLateUpdate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetSequence", _m_GetSequence);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetGraphicRaycaster", _m_GetGraphicRaycaster);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetCamera", _m_GetCamera);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetReferenceResolution", _m_GetReferenceResolution);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetSortingOrder", _m_GetSortingOrder);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsNeedFadeIn", _m_IsNeedFadeIn);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsNeedFadeOut", _m_IsNeedFadeOut);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsNeedClose", _m_IsNeedClose);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsClosed", _m_IsClosed);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsCanvasEnabled", _m_IsCanvasEnabled);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsHided", _m_IsHided);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ChangeScreenValueToForm", _m_ChangeScreenValueToForm);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ChangeFormValueToScreen", _m_ChangeFormValueToScreen);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CompareTo", _m_CompareTo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetDisplayOrder", _m_SetDisplayOrder);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Open", _m_Open);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TurnToClosed", _m_TurnToClosed);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Close", _m_Close);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnRemove", _m_OnRemove);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RestorePriority", _m_RestorePriority);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetPriority", _m_SetPriority);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetActive", _m_SetActive);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Hide", _m_Hide);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Appear", _m_Appear);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsInFadeIn", _m_IsInFadeIn);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsInFadeOut", _m_IsInFadeOut);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "ReferenceResolution", _g_get_ReferenceResolution);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsSingleton", _g_get_IsSingleton);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsModal", _g_get_IsModal);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ShowPriority", _g_get_ShowPriority);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "GroupId", _g_get_GroupId);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsFullScreenBG", _g_get_IsFullScreenBG);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsDisableInput", _g_get_IsDisableInput);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsHideUnderForms", _g_get_IsHideUnderForms);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsAlwaysKeepVisible", _g_get_IsAlwaysKeepVisible);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "FadeInAnimationType", _g_get_FadeInAnimationType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "FadeInAnimationName", _g_get_FadeInAnimationName);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "FadeOutAnimationType", _g_get_FadeOutAnimationType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "FadeOutAnimationName", _g_get_FadeOutAnimationName);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "FormPath", _g_get_FormPath);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "EventHandler", _g_get_EventHandler);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Controller", _g_get_Controller);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "ReferenceResolution", _s_set_ReferenceResolution);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "IsSingleton", _s_set_IsSingleton);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "IsModal", _s_set_IsModal);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ShowPriority", _s_set_ShowPriority);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "GroupId", _s_set_GroupId);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "IsFullScreenBG", _s_set_IsFullScreenBG);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "IsDisableInput", _s_set_IsDisableInput);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "IsHideUnderForms", _s_set_IsHideUnderForms);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "IsAlwaysKeepVisible", _s_set_IsAlwaysKeepVisible);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "FadeInAnimationType", _s_set_FadeInAnimationType);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "FadeInAnimationName", _s_set_FadeInAnimationName);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "FadeOutAnimationType", _s_set_FadeOutAnimationType);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "FadeOutAnimationName", _s_set_FadeOutAnimationName);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "FormPath", _s_set_FormPath);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "EventHandler", _s_set_EventHandler);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "Controller", _s_set_Controller);
            
			
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
					
					JW.Framework.UGUI.UIForm __cl_gen_ret = new JW.Framework.UGUI.UIForm();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.UGUI.UIForm constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MatchScreen(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.MatchScreen(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Initialize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Initialize(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CustomUpdate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.CustomUpdate(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CustomLateUpdate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.CustomLateUpdate(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetSequence(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        int __cl_gen_ret = __cl_gen_to_be_invoked.GetSequence(  );
                        LuaAPI.xlua_pushinteger(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetGraphicRaycaster(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        UnityEngine.UI.GraphicRaycaster __cl_gen_ret = __cl_gen_to_be_invoked.GetGraphicRaycaster(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCamera(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        UnityEngine.Camera __cl_gen_ret = __cl_gen_to_be_invoked.GetCamera(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetReferenceResolution(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        UnityEngine.Vector2 __cl_gen_ret = __cl_gen_to_be_invoked.GetReferenceResolution(  );
                        translator.PushUnityEngineVector2(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetSortingOrder(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        int __cl_gen_ret = __cl_gen_to_be_invoked.GetSortingOrder(  );
                        LuaAPI.xlua_pushinteger(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsNeedFadeIn(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.IsNeedFadeIn(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsNeedFadeOut(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.IsNeedFadeOut(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsNeedClose(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.IsNeedClose(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsClosed(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.IsClosed(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsCanvasEnabled(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.IsCanvasEnabled(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsHided(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.IsHided(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ChangeScreenValueToForm(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float value = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        float __cl_gen_ret = __cl_gen_to_be_invoked.ChangeScreenValueToForm( value );
                        LuaAPI.lua_pushnumber(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ChangeFormValueToScreen(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float value = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        float __cl_gen_ret = __cl_gen_to_be_invoked.ChangeFormValueToScreen( value );
                        LuaAPI.lua_pushnumber(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CompareTo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object obj = translator.GetObject(L, 2, typeof(object));
                    
                        int __cl_gen_ret = __cl_gen_to_be_invoked.CompareTo( obj );
                        LuaAPI.xlua_pushinteger(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetDisplayOrder(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int openOrder = LuaAPI.xlua_tointeger(L, 2);
                    
                    __cl_gen_to_be_invoked.SetDisplayOrder( openOrder );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Open(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    int sequence = LuaAPI.xlua_tointeger(L, 2);
                    int openOrder = LuaAPI.xlua_tointeger(L, 3);
                    bool exist = LuaAPI.lua_toboolean(L, 4);
                    
                    __cl_gen_to_be_invoked.Open( sequence, openOrder, exist );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 5&& translator.Assignable<UnityEngine.Camera>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)) 
                {
                    UnityEngine.Camera camera = (UnityEngine.Camera)translator.GetObject(L, 2, typeof(UnityEngine.Camera));
                    int sequence = LuaAPI.xlua_tointeger(L, 3);
                    int openOrder = LuaAPI.xlua_tointeger(L, 4);
                    bool exist = LuaAPI.lua_toboolean(L, 5);
                    
                    __cl_gen_to_be_invoked.Open( camera, sequence, openOrder, exist );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.UGUI.UIForm.Open!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TurnToClosed(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool ignoreFadeOut = LuaAPI.lua_toboolean(L, 2);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.TurnToClosed( ignoreFadeOut );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Close(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Close(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnRemove(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.OnRemove(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RestorePriority(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.RestorePriority(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPriority(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    JW.Framework.UGUI.UIFormPriority priority;translator.Get(L, 2, out priority);
                    
                    __cl_gen_to_be_invoked.SetPriority( priority );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetActive(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool active = LuaAPI.lua_toboolean(L, 2);
                    
                    __cl_gen_to_be_invoked.SetActive( active );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Hide(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 3&& translator.Assignable<JW.Framework.UGUI.UIFormHideFlag>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    JW.Framework.UGUI.UIFormHideFlag hideFlag;translator.Get(L, 2, out hideFlag);
                    bool dispatchEvent = LuaAPI.lua_toboolean(L, 3);
                    
                    __cl_gen_to_be_invoked.Hide( hideFlag, dispatchEvent );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 2&& translator.Assignable<JW.Framework.UGUI.UIFormHideFlag>(L, 2)) 
                {
                    JW.Framework.UGUI.UIFormHideFlag hideFlag;translator.Get(L, 2, out hideFlag);
                    
                    __cl_gen_to_be_invoked.Hide( hideFlag );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 1) 
                {
                    
                    __cl_gen_to_be_invoked.Hide(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.UGUI.UIForm.Hide!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Appear(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 3&& translator.Assignable<JW.Framework.UGUI.UIFormHideFlag>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    JW.Framework.UGUI.UIFormHideFlag hideFlag;translator.Get(L, 2, out hideFlag);
                    bool dispatchdEvent = LuaAPI.lua_toboolean(L, 3);
                    
                    __cl_gen_to_be_invoked.Appear( hideFlag, dispatchdEvent );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 2&& translator.Assignable<JW.Framework.UGUI.UIFormHideFlag>(L, 2)) 
                {
                    JW.Framework.UGUI.UIFormHideFlag hideFlag;translator.Get(L, 2, out hideFlag);
                    
                    __cl_gen_to_be_invoked.Appear( hideFlag );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 1) 
                {
                    
                    __cl_gen_to_be_invoked.Appear(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.UGUI.UIForm.Appear!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsInFadeIn(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.IsInFadeIn(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsInFadeOut(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.IsInFadeOut(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ReferenceResolution(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector2(L, __cl_gen_to_be_invoked.ReferenceResolution);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsSingleton(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsSingleton);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsModal(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsModal);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ShowPriority(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.ShowPriority);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_GroupId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.GroupId);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsFullScreenBG(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsFullScreenBG);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsDisableInput(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsDisableInput);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsHideUnderForms(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsHideUnderForms);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsAlwaysKeepVisible(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsAlwaysKeepVisible);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_FadeInAnimationType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.FadeInAnimationType);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_FadeInAnimationName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.FadeInAnimationName);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_FadeOutAnimationType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.FadeOutAnimationType);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_FadeOutAnimationName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.FadeOutAnimationName);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_FormPath(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.FormPath);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_EventHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.EventHandler);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Controller(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.Controller);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ReferenceResolution(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector2 __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.ReferenceResolution = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IsSingleton(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.IsSingleton = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IsModal(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.IsModal = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ShowPriority(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                JW.Framework.UGUI.UIFormPriority __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.ShowPriority = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_GroupId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.GroupId = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IsFullScreenBG(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.IsFullScreenBG = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IsDisableInput(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.IsDisableInput = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IsHideUnderForms(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.IsHideUnderForms = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IsAlwaysKeepVisible(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.IsAlwaysKeepVisible = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_FadeInAnimationType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                JW.Framework.UGUI.UIFormFadeAnimationType __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.FadeInAnimationType = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_FadeInAnimationName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.FadeInAnimationName = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_FadeOutAnimationType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                JW.Framework.UGUI.UIFormFadeAnimationType __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.FadeOutAnimationType = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_FadeOutAnimationName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.FadeOutAnimationName = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_FormPath(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.FormPath = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_EventHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.EventHandler = translator.GetDelegate<JW.Framework.UGUI.UIFormEventDelegate>(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Controller(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIForm __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIForm)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.Controller = (JW.Framework.MVC.UIFormClass)translator.GetObject(L, 2, typeof(JW.Framework.MVC.UIFormClass));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
