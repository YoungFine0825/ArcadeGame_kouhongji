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
    public class JWFrameworkAssetUIAssetWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(JW.Framework.Asset.UIAsset);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 2, 2);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "FormCom", _g_get_FormCom);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PLinkCom", _g_get_PLinkCom);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "FormCom", _s_set_FormCom);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "PLinkCom", _s_set_PLinkCom);
            
			
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
					
					JW.Framework.Asset.UIAsset __cl_gen_ret = new JW.Framework.Asset.UIAsset();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.Asset.UIAsset constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_FormCom(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.UIAsset __cl_gen_to_be_invoked = (JW.Framework.Asset.UIAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.FormCom);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PLinkCom(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.UIAsset __cl_gen_to_be_invoked = (JW.Framework.Asset.UIAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.PLinkCom);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_FormCom(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.UIAsset __cl_gen_to_be_invoked = (JW.Framework.Asset.UIAsset)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.FormCom = (JW.Framework.UGUI.UIForm)translator.GetObject(L, 2, typeof(JW.Framework.UGUI.UIForm));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_PLinkCom(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.UIAsset __cl_gen_to_be_invoked = (JW.Framework.Asset.UIAsset)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.PLinkCom = (JW.PLink.PrefabLink)translator.GetObject(L, 2, typeof(JW.PLink.PrefabLink));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
