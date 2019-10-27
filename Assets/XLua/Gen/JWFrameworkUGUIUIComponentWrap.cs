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
    public class JWFrameworkUGUIUIComponentWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(JW.Framework.UGUI.UIComponent);
			Utils.BeginObjectRegister(type, L, translator, 0, 6, 3, 3);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Initialize", _m_Initialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnClose", _m_OnClose);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnHide", _m_OnHide);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnAppear", _m_OnAppear);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSortingOrder", _m_SetSortingOrder);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetBelongedList", _m_SetBelongedList);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "BelongedForm", _g_get_BelongedForm);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "BelongedListView", _g_get_BelongedListView);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IndexInListView", _g_get_IndexInListView);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "BelongedForm", _s_set_BelongedForm);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "BelongedListView", _s_set_BelongedListView);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "IndexInListView", _s_set_IndexInListView);
            
			
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
					
					JW.Framework.UGUI.UIComponent __cl_gen_ret = new JW.Framework.UGUI.UIComponent();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.UGUI.UIComponent constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Initialize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIComponent __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    JW.Framework.UGUI.UIForm form = (JW.Framework.UGUI.UIForm)translator.GetObject(L, 2, typeof(JW.Framework.UGUI.UIForm));
                    
                    __cl_gen_to_be_invoked.Initialize( form );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnClose(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIComponent __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.OnClose(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnHide(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIComponent __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.OnHide(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnAppear(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIComponent __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.OnAppear(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSortingOrder(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIComponent __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int sortingOrder = LuaAPI.xlua_tointeger(L, 2);
                    
                    __cl_gen_to_be_invoked.SetSortingOrder( sortingOrder );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetBelongedList(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JW.Framework.UGUI.UIComponent __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    JW.Framework.UGUI.UIListView belongedListScript = (JW.Framework.UGUI.UIListView)translator.GetObject(L, 2, typeof(JW.Framework.UGUI.UIListView));
                    int index = LuaAPI.xlua_tointeger(L, 3);
                    
                    __cl_gen_to_be_invoked.SetBelongedList( belongedListScript, index );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BelongedForm(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIComponent __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIComponent)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.BelongedForm);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BelongedListView(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIComponent __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIComponent)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.BelongedListView);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IndexInListView(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIComponent __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIComponent)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.IndexInListView);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_BelongedForm(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIComponent __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIComponent)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.BelongedForm = (JW.Framework.UGUI.UIForm)translator.GetObject(L, 2, typeof(JW.Framework.UGUI.UIForm));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_BelongedListView(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIComponent __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIComponent)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.BelongedListView = (JW.Framework.UGUI.UIListView)translator.GetObject(L, 2, typeof(JW.Framework.UGUI.UIListView));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IndexInListView(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.UGUI.UIComponent __cl_gen_to_be_invoked = (JW.Framework.UGUI.UIComponent)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.IndexInListView = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
