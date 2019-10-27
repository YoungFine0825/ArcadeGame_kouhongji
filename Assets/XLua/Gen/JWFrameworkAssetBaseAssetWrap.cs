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
    public class JWFrameworkAssetBaseAssetWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(JW.Framework.Asset.BaseAsset);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 10, 8);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Go", _g_get_Go);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Tf", _g_get_Tf);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "BaseData", _g_get_BaseData);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Resource", _g_get_Resource);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "RootGo", _g_get_RootGo);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "RootTf", _g_get_RootTf);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "RootPLink", _g_get_RootPLink);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OrignalPosition", _g_get_OrignalPosition);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OrignalRotate", _g_get_OrignalRotate);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OrignalScale", _g_get_OrignalScale);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "BaseData", _s_set_BaseData);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "Resource", _s_set_Resource);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "RootGo", _s_set_RootGo);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "RootTf", _s_set_RootTf);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "RootPLink", _s_set_RootPLink);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OrignalPosition", _s_set_OrignalPosition);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OrignalRotate", _s_set_OrignalRotate);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OrignalScale", _s_set_OrignalScale);
            
			
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
					
					JW.Framework.Asset.BaseAsset __cl_gen_ret = new JW.Framework.Asset.BaseAsset();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Framework.Asset.BaseAsset constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Go(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.BaseAsset __cl_gen_to_be_invoked = (JW.Framework.Asset.BaseAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.Go);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Tf(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.BaseAsset __cl_gen_to_be_invoked = (JW.Framework.Asset.BaseAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.Tf);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BaseData(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.BaseAsset __cl_gen_to_be_invoked = (JW.Framework.Asset.BaseAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.BaseData);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Resource(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.BaseAsset __cl_gen_to_be_invoked = (JW.Framework.Asset.BaseAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.Resource);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RootGo(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.BaseAsset __cl_gen_to_be_invoked = (JW.Framework.Asset.BaseAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.RootGo);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RootTf(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.BaseAsset __cl_gen_to_be_invoked = (JW.Framework.Asset.BaseAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.RootTf);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RootPLink(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.BaseAsset __cl_gen_to_be_invoked = (JW.Framework.Asset.BaseAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.RootPLink);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OrignalPosition(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.BaseAsset __cl_gen_to_be_invoked = (JW.Framework.Asset.BaseAsset)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector3(L, __cl_gen_to_be_invoked.OrignalPosition);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OrignalRotate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.BaseAsset __cl_gen_to_be_invoked = (JW.Framework.Asset.BaseAsset)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineQuaternion(L, __cl_gen_to_be_invoked.OrignalRotate);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OrignalScale(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.BaseAsset __cl_gen_to_be_invoked = (JW.Framework.Asset.BaseAsset)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector3(L, __cl_gen_to_be_invoked.OrignalScale);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_BaseData(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.BaseAsset __cl_gen_to_be_invoked = (JW.Framework.Asset.BaseAsset)translator.FastGetCSObj(L, 1);
                JW.Framework.Asset.AssetData __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.BaseData = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Resource(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.BaseAsset __cl_gen_to_be_invoked = (JW.Framework.Asset.BaseAsset)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.Resource = (JW.Res.ResObj)translator.GetObject(L, 2, typeof(JW.Res.ResObj));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_RootGo(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.BaseAsset __cl_gen_to_be_invoked = (JW.Framework.Asset.BaseAsset)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.RootGo = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_RootTf(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.BaseAsset __cl_gen_to_be_invoked = (JW.Framework.Asset.BaseAsset)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.RootTf = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_RootPLink(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.BaseAsset __cl_gen_to_be_invoked = (JW.Framework.Asset.BaseAsset)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.RootPLink = (JW.PLink.PrefabLink)translator.GetObject(L, 2, typeof(JW.PLink.PrefabLink));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OrignalPosition(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.BaseAsset __cl_gen_to_be_invoked = (JW.Framework.Asset.BaseAsset)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector3 __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.OrignalPosition = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OrignalRotate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.BaseAsset __cl_gen_to_be_invoked = (JW.Framework.Asset.BaseAsset)translator.FastGetCSObj(L, 1);
                UnityEngine.Quaternion __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.OrignalRotate = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OrignalScale(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JW.Framework.Asset.BaseAsset __cl_gen_to_be_invoked = (JW.Framework.Asset.BaseAsset)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector3 __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.OrignalScale = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
