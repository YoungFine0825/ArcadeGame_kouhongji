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
    public class JWFrameworkUGUIUIListViewItemWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(JW.Framework.UGUI.UIListViewItem);
			Utils.BeginObjectRegister(type, L, translator, 0, 7, 15, 15);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Initialize", _m_Initialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Enable", _m_Enable);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Disable", _m_Disable);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnSelected", _m_OnSelected);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ChangeDisplay", _m_ChangeDisplay);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetComponentBelongedList", _m_SetComponentBelongedList);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetRect", _m_SetRect);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_selectFrontObj", _g_get_m_selectFrontObj);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_selectedSprite", _g_get_m_selectedSprite);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ApplyClickSelTween", _g_get_ApplyClickSelTween);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_defaultSprite", _g_get_m_defaultSprite);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_defaultColor", _g_get_m_defaultColor);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_defaultTextColor", _g_get_m_defaultTextColor);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_textObj", _g_get_m_textObj);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_selectTextColor", _g_get_m_selectTextColor);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_defaultSize", _g_get_m_defaultSize);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_index", _g_get_m_index);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_rect", _g_get_m_rect);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_useSetActiveForDisplay", _g_get_m_useSetActiveForDisplay);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_autoAddUIEventScript", _g_get_m_autoAddUIEventScript);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_belongedListScript", _g_get_m_belongedListScript);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PLink", _g_get_PLink);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_selectFrontObj", _s_set_m_selectFrontObj);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_selectedSprite", _s_set_m_selectedSprite);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ApplyClickSelTween", _s_set_ApplyClickSelTween);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_defaultSprite", _s_set_m_defaultSprite);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_defaultColor", _s_set_m_defaultColor);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_defaultTextColor", _s_set_m_defaultTextColor);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_textObj", _s_set_m_textObj);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_selectTextColor", _s_set_m_selectTextColor);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_defaultSize", _s_set_m_defaultSize);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_index", _s_set_m_index);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_rect", _s_set_m_rect);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_useSetActiveForDisplay", _s_set_m_useSetActiveForDisplay);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_autoAddUIEventScript", _s_set_m_autoAddUIEventScript);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_belongedListScript", _s_set_m_belongedListScript);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "PLink", _s_set_PLink);
            
			
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
					
					JW.Framework.UGUI.UIListViewItem __cl_gen_ret = new JW.Framework.UGUI.UIListViewItem();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.UGUI.UIListViewItem constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Initialize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    JW.Framework.UGUI.UIForm formScript = (JW.Framework.UGUI.UIForm)translator.GetObject(L, 2, typeof(JW.Framework.UGUI.UIForm));
                    
                    __cl_gen_to_be_invoked.Initialize( formScript );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Enable(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    JW.Framework.UGUI.UIListView belongedList = (JW.Framework.UGUI.UIListView)translator.GetObject(L, 2, typeof(JW.Framework.UGUI.UIListView));
                    int index = LuaAPI.xlua_tointeger(L, 3);
                    string name = LuaAPI.lua_tostring(L, 4);
                    JW.Framework.UGUI.stRect rect;translator.Get(L, 5, out rect);
                    bool selected = LuaAPI.lua_toboolean(L, 6);
                    
                    __cl_gen_to_be_invoked.Enable( belongedList, index, name, ref rect, selected );
                    translator.Push(L, rect);
                        translator.Update(L, 5, rect);
                        
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Disable(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Disable(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnSelected(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.EventSystems.BaseEventData baseEventData = (UnityEngine.EventSystems.BaseEventData)translator.GetObject(L, 2, typeof(UnityEngine.EventSystems.BaseEventData));
                    
                    __cl_gen_to_be_invoked.OnSelected( baseEventData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ChangeDisplay(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool selected = LuaAPI.lua_toboolean(L, 2);
                    
                    __cl_gen_to_be_invoked.ChangeDisplay( selected );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetComponentBelongedList(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.GameObject gameObject = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    
                    __cl_gen_to_be_invoked.SetComponentBelongedList( gameObject );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetRect(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    JW.Framework.UGUI.stRect rect;translator.Get(L, 2, out rect);
                    
                    __cl_gen_to_be_invoked.SetRect( ref rect );
                    translator.Push(L, rect);
                        translator.Update(L, 2, rect);
                        
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_selectFrontObj(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.m_selectFrontObj);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_selectedSprite(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.m_selectedSprite);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ApplyClickSelTween(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.ApplyClickSelTween);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_defaultSprite(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.m_defaultSprite);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_defaultColor(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineColor(L, __cl_gen_to_be_invoked.m_defaultColor);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_defaultTextColor(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineColor(L, __cl_gen_to_be_invoked.m_defaultTextColor);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_textObj(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.m_textObj);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_selectTextColor(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineColor(L, __cl_gen_to_be_invoked.m_selectTextColor);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_defaultSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector2(L, __cl_gen_to_be_invoked.m_defaultSize);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_index(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.m_index);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_rect(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.m_rect);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_useSetActiveForDisplay(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.m_useSetActiveForDisplay);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_autoAddUIEventScript(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.m_autoAddUIEventScript);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_belongedListScript(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.m_belongedListScript);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PLink(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.PLink);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_selectFrontObj(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_selectFrontObj = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_selectedSprite(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_selectedSprite = (UnityEngine.Sprite)translator.GetObject(L, 2, typeof(UnityEngine.Sprite));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ApplyClickSelTween(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.ApplyClickSelTween = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_defaultSprite(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_defaultSprite = (UnityEngine.Sprite)translator.GetObject(L, 2, typeof(UnityEngine.Sprite));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_defaultColor(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                UnityEngine.Color __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.m_defaultColor = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_defaultTextColor(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                UnityEngine.Color __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.m_defaultTextColor = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_textObj(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_textObj = (UnityEngine.UI.Text)translator.GetObject(L, 2, typeof(UnityEngine.UI.Text));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_selectTextColor(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                UnityEngine.Color __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.m_selectTextColor = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_defaultSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector2 __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.m_defaultSize = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_index(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_index = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_rect(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                JW.Framework.UGUI.stRect __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.m_rect = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_useSetActiveForDisplay(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_useSetActiveForDisplay = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_autoAddUIEventScript(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_autoAddUIEventScript = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_belongedListScript(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_belongedListScript = (JW.Framework.UGUI.UIListView)translator.GetObject(L, 2, typeof(JW.Framework.UGUI.UIListView));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_PLink(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIListViewItem __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIListViewItem)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.PLink = (JW.PLink.PrefabLink)translator.GetObject(L, 2, typeof(JW.PLink.PrefabLink));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
