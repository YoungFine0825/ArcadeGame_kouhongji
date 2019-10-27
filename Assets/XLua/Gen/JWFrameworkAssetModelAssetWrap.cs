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
    public class JWFrameworkAssetModelAssetWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(JW.Framework.Asset.ModelAsset);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 3, 3);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Render", _g_get_Render);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "AnimationCpt", _g_get_AnimationCpt);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "AnimatorCtrl", _g_get_AnimatorCtrl);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "Render", _s_set_Render);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "AnimationCpt", _s_set_AnimationCpt);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "AnimatorCtrl", _s_set_AnimatorCtrl);
            
			
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
					
					JW.Framework.Asset.ModelAsset __cl_gen_ret = new JW.Framework.Asset.ModelAsset();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.Asset.ModelAsset constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Render(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.ModelAsset __cl_gen_to_be_invoked = (JW.Framework.Asset.ModelAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.Render);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AnimationCpt(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.ModelAsset __cl_gen_to_be_invoked = (JW.Framework.Asset.ModelAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.AnimationCpt);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AnimatorCtrl(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.ModelAsset __cl_gen_to_be_invoked = (JW.Framework.Asset.ModelAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.AnimatorCtrl);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Render(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.ModelAsset __cl_gen_to_be_invoked = (JW.Framework.Asset.ModelAsset)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.Render = (UnityEngine.Renderer[])translator.GetObject(L, 2, typeof(UnityEngine.Renderer[]));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_AnimationCpt(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.ModelAsset __cl_gen_to_be_invoked = (JW.Framework.Asset.ModelAsset)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.AnimationCpt = (UnityEngine.Animation)translator.GetObject(L, 2, typeof(UnityEngine.Animation));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_AnimatorCtrl(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.ModelAsset __cl_gen_to_be_invoked = (JW.Framework.Asset.ModelAsset)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.AnimatorCtrl = (UnityEngine.Animator)translator.GetObject(L, 2, typeof(UnityEngine.Animator));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
