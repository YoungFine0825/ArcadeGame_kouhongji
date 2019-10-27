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
    public class JWFrameworkUGUIUIEventListenerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(JW.Framework.UGUI.UIEventListener);
			Utils.BeginObjectRegister(type, L, translator, 0, 12, 16, 16);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Initialize", _m_Initialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnPointerDown", _m_OnPointerDown);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnPointerUp", _m_OnPointerUp);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnPointerClick", _m_OnPointerClick);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnPointerEnter", _m_OnPointerEnter);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnPointerExit", _m_OnPointerExit);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnBeginDrag", _m_OnBeginDrag);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnDrag", _m_OnDrag);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnEndDrag", _m_OnEndDrag);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnDrop", _m_OnDrop);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ClearInputStatus", _m_ClearInputStatus);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ExecuteInputStatus", _m_ExecuteInputStatus);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "DragNext", _g_get_DragNext);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnDownAudio", _g_get_OnDownAudio);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnClickedAudio", _g_get_OnClickedAudio);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ApplyClickTween", _g_get_ApplyClickTween);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onDown", _g_get_onDown);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onUp", _g_get_onUp);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onClick", _g_get_onClick);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onHoldStart", _g_get_onHoldStart);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onHold", _g_get_onHold);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onHoldEnd", _g_get_onHoldEnd);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onDragStart", _g_get_onDragStart);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onDrag", _g_get_onDrag);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onDragEnd", _g_get_onDragEnd);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onDrop", _g_get_onDrop);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onPointerEnter", _g_get_onPointerEnter);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onPointerExit", _g_get_onPointerExit);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "DragNext", _s_set_DragNext);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnDownAudio", _s_set_OnDownAudio);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnClickedAudio", _s_set_OnClickedAudio);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ApplyClickTween", _s_set_ApplyClickTween);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onDown", _s_set_onDown);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onUp", _s_set_onUp);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onClick", _s_set_onClick);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onHoldStart", _s_set_onHoldStart);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onHold", _s_set_onHold);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onHoldEnd", _s_set_onHoldEnd);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onDragStart", _s_set_onDragStart);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onDrag", _s_set_onDrag);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onDragEnd", _s_set_onDragEnd);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onDrop", _s_set_onDrop);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onPointerEnter", _s_set_onPointerEnter);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onPointerExit", _s_set_onPointerExit);
            
			
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
					
					JW.Framework.UGUI.UIEventListener __cl_gen_ret = new JW.Framework.UGUI.UIEventListener();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.UGUI.UIEventListener constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Initialize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_OnPointerDown(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.EventSystems.PointerEventData eventData = (UnityEngine.EventSystems.PointerEventData)translator.GetObject(L, 2, typeof(UnityEngine.EventSystems.PointerEventData));
                    
                    __cl_gen_to_be_invoked.OnPointerDown( eventData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnPointerUp(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.EventSystems.PointerEventData eventData = (UnityEngine.EventSystems.PointerEventData)translator.GetObject(L, 2, typeof(UnityEngine.EventSystems.PointerEventData));
                    
                    __cl_gen_to_be_invoked.OnPointerUp( eventData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnPointerClick(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.EventSystems.PointerEventData eventData = (UnityEngine.EventSystems.PointerEventData)translator.GetObject(L, 2, typeof(UnityEngine.EventSystems.PointerEventData));
                    
                    __cl_gen_to_be_invoked.OnPointerClick( eventData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnPointerEnter(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.EventSystems.PointerEventData eventData = (UnityEngine.EventSystems.PointerEventData)translator.GetObject(L, 2, typeof(UnityEngine.EventSystems.PointerEventData));
                    
                    __cl_gen_to_be_invoked.OnPointerEnter( eventData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnPointerExit(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.EventSystems.PointerEventData eventData = (UnityEngine.EventSystems.PointerEventData)translator.GetObject(L, 2, typeof(UnityEngine.EventSystems.PointerEventData));
                    
                    __cl_gen_to_be_invoked.OnPointerExit( eventData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnBeginDrag(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.EventSystems.PointerEventData eventData = (UnityEngine.EventSystems.PointerEventData)translator.GetObject(L, 2, typeof(UnityEngine.EventSystems.PointerEventData));
                    
                    __cl_gen_to_be_invoked.OnBeginDrag( eventData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnDrag(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.EventSystems.PointerEventData eventData = (UnityEngine.EventSystems.PointerEventData)translator.GetObject(L, 2, typeof(UnityEngine.EventSystems.PointerEventData));
                    
                    __cl_gen_to_be_invoked.OnDrag( eventData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnEndDrag(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.EventSystems.PointerEventData eventData = (UnityEngine.EventSystems.PointerEventData)translator.GetObject(L, 2, typeof(UnityEngine.EventSystems.PointerEventData));
                    
                    __cl_gen_to_be_invoked.OnEndDrag( eventData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnDrop(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.EventSystems.PointerEventData eventData = (UnityEngine.EventSystems.PointerEventData)translator.GetObject(L, 2, typeof(UnityEngine.EventSystems.PointerEventData));
                    
                    __cl_gen_to_be_invoked.OnDrop( eventData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearInputStatus(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.ClearInputStatus(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ExecuteInputStatus(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.ExecuteInputStatus(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DragNext(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.DragNext);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnDownAudio(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.OnDownAudio);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnClickedAudio(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.OnClickedAudio);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ApplyClickTween(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.ApplyClickTween);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onDown(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.onDown);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onUp(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.onUp);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onClick(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.onClick);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onHoldStart(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.onHoldStart);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onHold(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.onHold);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onHoldEnd(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.onHoldEnd);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onDragStart(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.onDragStart);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onDrag(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.onDrag);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onDragEnd(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.onDragEnd);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onDrop(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.onDrop);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onPointerEnter(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.onPointerEnter);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onPointerExit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.onPointerExit);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DragNext(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.DragNext = (JW.Framework.UGUI.UIEventListener)translator.GetObject(L, 2, typeof(JW.Framework.UGUI.UIEventListener));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnDownAudio(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.OnDownAudio = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnClickedAudio(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.OnClickedAudio = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ApplyClickTween(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.ApplyClickTween = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onDown(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.onDown = (JW.Framework.UGUI.UIListenerEvent)translator.GetObject(L, 2, typeof(JW.Framework.UGUI.UIListenerEvent));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onUp(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.onUp = (JW.Framework.UGUI.UIListenerEvent)translator.GetObject(L, 2, typeof(JW.Framework.UGUI.UIListenerEvent));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onClick(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.onClick = (UnityEngine.Events.UnityEvent)translator.GetObject(L, 2, typeof(UnityEngine.Events.UnityEvent));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onHoldStart(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.onHoldStart = (JW.Framework.UGUI.UIListenerEvent)translator.GetObject(L, 2, typeof(JW.Framework.UGUI.UIListenerEvent));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onHold(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.onHold = (JW.Framework.UGUI.UIListenerEvent)translator.GetObject(L, 2, typeof(JW.Framework.UGUI.UIListenerEvent));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onHoldEnd(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.onHoldEnd = (JW.Framework.UGUI.UIListenerEvent)translator.GetObject(L, 2, typeof(JW.Framework.UGUI.UIListenerEvent));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onDragStart(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.onDragStart = (JW.Framework.UGUI.UIListenerEvent)translator.GetObject(L, 2, typeof(JW.Framework.UGUI.UIListenerEvent));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onDrag(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.onDrag = (JW.Framework.UGUI.UIListenerEvent)translator.GetObject(L, 2, typeof(JW.Framework.UGUI.UIListenerEvent));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onDragEnd(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.onDragEnd = (JW.Framework.UGUI.UIListenerEvent)translator.GetObject(L, 2, typeof(JW.Framework.UGUI.UIListenerEvent));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onDrop(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.onDrop = (JW.Framework.UGUI.UIListenerEvent)translator.GetObject(L, 2, typeof(JW.Framework.UGUI.UIListenerEvent));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onPointerEnter(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.onPointerEnter = (JW.Framework.UGUI.UIListenerEvent)translator.GetObject(L, 2, typeof(JW.Framework.UGUI.UIListenerEvent));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onPointerExit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIEventListener __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIEventListener)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.onPointerExit = (JW.Framework.UGUI.UIListenerEvent)translator.GetObject(L, 2, typeof(JW.Framework.UGUI.UIListenerEvent));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
