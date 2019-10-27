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
    public class JWFrameworkUGUIUGUIRootWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(JW.Framework.UGUI.UGUIRoot);
			Utils.BeginObjectRegister(type, L, translator, 0, 15, 2, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Initialize", _m_Initialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Uninitialize", _m_Uninitialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CustomUpdate", _m_CustomUpdate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CustomLateUpdate", _m_CustomLateUpdate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetEventSystem", _m_GetEventSystem);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DisableInput", _m_DisableInput);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "EnableInput", _m_EnableInput);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CloseGroupForm", _m_CloseGroupForm);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OpenForm", _m_OpenForm);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CloseForm", _m_CloseForm);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CloseAllForm", _m_CloseAllForm);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HasForm", _m_HasForm);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetForm", _m_GetForm);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetTopForm", _m_GetTopForm);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetForms", _m_GetForms);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "FormCamera", _g_get_FormCamera);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnFormSortedHandler", _g_get_OnFormSortedHandler);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnFormSortedHandler", _s_set_OnFormSortedHandler);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 1, 1);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "sUGUIFrameCount", _g_get_sUGUIFrameCount);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "sUGUIFrameCount", _s_set_sUGUIFrameCount);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					JW.Framework.UGUI.UGUIRoot __cl_gen_ret = new JW.Framework.UGUI.UGUIRoot();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.UGUI.UGUIRoot constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Initialize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UGUIRoot __cl_gen_to_be_invoked = (JW.Framework.UGUI.UGUIRoot)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                JW.Framework.UGUI.UGUIRoot __cl_gen_to_be_invoked = (JW.Framework.UGUI.UGUIRoot)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Uninitialize(  );
                    
                    
                    
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
            
            
                JW.Framework.UGUI.UGUIRoot __cl_gen_to_be_invoked = (JW.Framework.UGUI.UGUIRoot)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                JW.Framework.UGUI.UGUIRoot __cl_gen_to_be_invoked = (JW.Framework.UGUI.UGUIRoot)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.CustomLateUpdate(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetEventSystem(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UGUIRoot __cl_gen_to_be_invoked = (JW.Framework.UGUI.UGUIRoot)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        UnityEngine.EventSystems.EventSystem __cl_gen_ret = __cl_gen_to_be_invoked.GetEventSystem(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DisableInput(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UGUIRoot __cl_gen_to_be_invoked = (JW.Framework.UGUI.UGUIRoot)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.DisableInput(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EnableInput(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UGUIRoot __cl_gen_to_be_invoked = (JW.Framework.UGUI.UGUIRoot)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.EnableInput(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CloseGroupForm(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UGUIRoot __cl_gen_to_be_invoked = (JW.Framework.UGUI.UGUIRoot)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int group = LuaAPI.xlua_tointeger(L, 2);
                    
                    __cl_gen_to_be_invoked.CloseGroupForm( group );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenForm(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UGUIRoot __cl_gen_to_be_invoked = (JW.Framework.UGUI.UGUIRoot)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 3&& translator.Assignable<JW.Framework.UGUI.UIForm>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    JW.Framework.UGUI.UIForm formCpt = (JW.Framework.UGUI.UIForm)translator.GetObject(L, 2, typeof(JW.Framework.UGUI.UIForm));
                    bool useCamera = LuaAPI.lua_toboolean(L, 3);
                    
                        JW.Framework.UGUI.UIForm __cl_gen_ret = __cl_gen_to_be_invoked.OpenForm( formCpt, useCamera );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<JW.Framework.UGUI.UIForm>(L, 2)) 
                {
                    JW.Framework.UGUI.UIForm formCpt = (JW.Framework.UGUI.UIForm)translator.GetObject(L, 2, typeof(JW.Framework.UGUI.UIForm));
                    
                        JW.Framework.UGUI.UIForm __cl_gen_ret = __cl_gen_to_be_invoked.OpenForm( formCpt );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.UGUI.UGUIRoot.OpenForm!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CloseForm(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UGUIRoot __cl_gen_to_be_invoked = (JW.Framework.UGUI.UGUIRoot)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int formSequence = LuaAPI.xlua_tointeger(L, 2);
                    
                    __cl_gen_to_be_invoked.CloseForm( formSequence );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 2&& translator.Assignable<JW.Framework.UGUI.UIForm>(L, 2)) 
                {
                    JW.Framework.UGUI.UIForm form = (JW.Framework.UGUI.UIForm)translator.GetObject(L, 2, typeof(JW.Framework.UGUI.UIForm));
                    
                    __cl_gen_to_be_invoked.CloseForm( form );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.UGUI.UGUIRoot.CloseForm!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CloseAllForm(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UGUIRoot __cl_gen_to_be_invoked = (JW.Framework.UGUI.UGUIRoot)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 3&& translator.Assignable<string[]>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    string[] exceptFormNames = (string[])translator.GetObject(L, 2, typeof(string[]));
                    bool closeImmediately = LuaAPI.lua_toboolean(L, 3);
                    
                    __cl_gen_to_be_invoked.CloseAllForm( exceptFormNames, closeImmediately );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 2&& translator.Assignable<string[]>(L, 2)) 
                {
                    string[] exceptFormNames = (string[])translator.GetObject(L, 2, typeof(string[]));
                    
                    __cl_gen_to_be_invoked.CloseAllForm( exceptFormNames );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 1) 
                {
                    
                    __cl_gen_to_be_invoked.CloseAllForm(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.UGUI.UGUIRoot.CloseAllForm!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HasForm(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UGUIRoot __cl_gen_to_be_invoked = (JW.Framework.UGUI.UGUIRoot)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.HasForm(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetForm(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UGUIRoot __cl_gen_to_be_invoked = (JW.Framework.UGUI.UGUIRoot)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int formSequence = LuaAPI.xlua_tointeger(L, 2);
                    
                        JW.Framework.UGUI.UIForm __cl_gen_ret = __cl_gen_to_be_invoked.GetForm( formSequence );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string formPath = LuaAPI.lua_tostring(L, 2);
                    
                        JW.Framework.UGUI.UIForm __cl_gen_ret = __cl_gen_to_be_invoked.GetForm( formPath );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.UGUI.UGUIRoot.GetForm!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTopForm(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UGUIRoot __cl_gen_to_be_invoked = (JW.Framework.UGUI.UGUIRoot)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        JW.Framework.UGUI.UIForm __cl_gen_ret = __cl_gen_to_be_invoked.GetTopForm(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetForms(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UGUIRoot __cl_gen_to_be_invoked = (JW.Framework.UGUI.UGUIRoot)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        JW.Common.JWObjList<JW.Framework.UGUI.UIForm> __cl_gen_ret = __cl_gen_to_be_invoked.GetForms(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_FormCamera(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UGUIRoot __cl_gen_to_be_invoked = (JW.Framework.UGUI.UGUIRoot)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.FormCamera);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnFormSortedHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UGUIRoot __cl_gen_to_be_invoked = (JW.Framework.UGUI.UGUIRoot)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.OnFormSortedHandler);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_sUGUIFrameCount(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, JW.Framework.UGUI.UGUIRoot.sUGUIFrameCount);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnFormSortedHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UGUIRoot __cl_gen_to_be_invoked = (JW.Framework.UGUI.UGUIRoot)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.OnFormSortedHandler = translator.GetDelegate<JW.Framework.UGUI.UGUIRoot.OnFormSortedDelegate>(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_sUGUIFrameCount(RealStatePtr L)
        {
		    try {
                
			    JW.Framework.UGUI.UGUIRoot.sUGUIFrameCount = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
