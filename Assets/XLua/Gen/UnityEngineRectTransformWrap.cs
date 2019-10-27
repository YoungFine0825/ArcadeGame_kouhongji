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
using DG.Tweening;using JW.Common;

namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class UnityEngineRectTransformWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UnityEngine.RectTransform);
			Utils.BeginObjectRegister(type, L, translator, 0, 24, 9, 8);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetLocalCorners", _m_GetLocalCorners);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetWorldCorners", _m_GetWorldCorners);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetInsetAndSizeFromParentEdge", _m_SetInsetAndSizeFromParentEdge);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSizeWithCurrentAnchors", _m_SetSizeWithCurrentAnchors);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ForceUpdateRectTransforms", _m_ForceUpdateRectTransforms);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOAnchorPos", _m_DOAnchorPos);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOAnchorPosX", _m_DOAnchorPosX);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOAnchorPosY", _m_DOAnchorPosY);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOAnchorPos3D", _m_DOAnchorPos3D);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOAnchorMax", _m_DOAnchorMax);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOAnchorMin", _m_DOAnchorMin);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOPivot", _m_DOPivot);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOPivotX", _m_DOPivotX);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOPivotY", _m_DOPivotY);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOSizeDelta", _m_DOSizeDelta);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOPunchAnchorPos", _m_DOPunchAnchorPos);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOShakeAnchorPos", _m_DOShakeAnchorPos);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOJumpAnchorPos", _m_DOJumpAnchorPos);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ExtSetActive", _m_ExtSetActive);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ExtSetLocalScaleXYZ", _m_ExtSetLocalScaleXYZ);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ExtSetLocalScaleX", _m_ExtSetLocalScaleX);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ExtSetLocalScaleY", _m_ExtSetLocalScaleY);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ExtSetLocalScaleZ", _m_ExtSetLocalScaleZ);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ExtSetLocalScale", _m_ExtSetLocalScale);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "rect", _g_get_rect);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "anchorMin", _g_get_anchorMin);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "anchorMax", _g_get_anchorMax);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "anchoredPosition3D", _g_get_anchoredPosition3D);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "anchoredPosition", _g_get_anchoredPosition);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "sizeDelta", _g_get_sizeDelta);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "pivot", _g_get_pivot);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "offsetMin", _g_get_offsetMin);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "offsetMax", _g_get_offsetMax);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "anchorMin", _s_set_anchorMin);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "anchorMax", _s_set_anchorMax);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "anchoredPosition3D", _s_set_anchoredPosition3D);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "anchoredPosition", _s_set_anchoredPosition);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "sizeDelta", _s_set_sizeDelta);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "pivot", _s_set_pivot);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "offsetMin", _s_set_offsetMin);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "offsetMax", _s_set_offsetMax);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 2, 0, 0);
			
			Utils.RegisterFunc(L, Utils.CLS_IDX, "reapplyDrivenProperties", _e_reapplyDrivenProperties);
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					UnityEngine.RectTransform __cl_gen_ret = new UnityEngine.RectTransform();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.RectTransform constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLocalCorners(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Vector3[] fourCornersArray = (UnityEngine.Vector3[])translator.GetObject(L, 2, typeof(UnityEngine.Vector3[]));
                    
                    __cl_gen_to_be_invoked.GetLocalCorners( fourCornersArray );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetWorldCorners(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Vector3[] fourCornersArray = (UnityEngine.Vector3[])translator.GetObject(L, 2, typeof(UnityEngine.Vector3[]));
                    
                    __cl_gen_to_be_invoked.GetWorldCorners( fourCornersArray );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetInsetAndSizeFromParentEdge(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.RectTransform.Edge edge;translator.Get(L, 2, out edge);
                    float inset = (float)LuaAPI.lua_tonumber(L, 3);
                    float size = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    __cl_gen_to_be_invoked.SetInsetAndSizeFromParentEdge( edge, inset, size );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSizeWithCurrentAnchors(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.RectTransform.Axis axis;translator.Get(L, 2, out axis);
                    float size = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    __cl_gen_to_be_invoked.SetSizeWithCurrentAnchors( axis, size );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ForceUpdateRectTransforms(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.ForceUpdateRectTransforms(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOAnchorPos(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 4&& translator.Assignable<UnityEngine.Vector2>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Vector2 endValue;translator.Get(L, 2, out endValue);
                    float duration = (float)LuaAPI.lua_tonumber(L, 3);
                    bool snapping = LuaAPI.lua_toboolean(L, 4);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOAnchorPos( endValue, duration, snapping );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& translator.Assignable<UnityEngine.Vector2>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector2 endValue;translator.Get(L, 2, out endValue);
                    float duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOAnchorPos( endValue, duration );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.RectTransform.DOAnchorPos!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOAnchorPosX(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    float endValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float duration = (float)LuaAPI.lua_tonumber(L, 3);
                    bool snapping = LuaAPI.lua_toboolean(L, 4);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOAnchorPosX( endValue, duration, snapping );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    float endValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOAnchorPosX( endValue, duration );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.RectTransform.DOAnchorPosX!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOAnchorPosY(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    float endValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float duration = (float)LuaAPI.lua_tonumber(L, 3);
                    bool snapping = LuaAPI.lua_toboolean(L, 4);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOAnchorPosY( endValue, duration, snapping );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    float endValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOAnchorPosY( endValue, duration );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.RectTransform.DOAnchorPosY!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOAnchorPos3D(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Vector3 endValue;translator.Get(L, 2, out endValue);
                    float duration = (float)LuaAPI.lua_tonumber(L, 3);
                    bool snapping = LuaAPI.lua_toboolean(L, 4);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOAnchorPos3D( endValue, duration, snapping );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 endValue;translator.Get(L, 2, out endValue);
                    float duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOAnchorPos3D( endValue, duration );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.RectTransform.DOAnchorPos3D!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOAnchorMax(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 4&& translator.Assignable<UnityEngine.Vector2>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Vector2 endValue;translator.Get(L, 2, out endValue);
                    float duration = (float)LuaAPI.lua_tonumber(L, 3);
                    bool snapping = LuaAPI.lua_toboolean(L, 4);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOAnchorMax( endValue, duration, snapping );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& translator.Assignable<UnityEngine.Vector2>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector2 endValue;translator.Get(L, 2, out endValue);
                    float duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOAnchorMax( endValue, duration );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.RectTransform.DOAnchorMax!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOAnchorMin(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 4&& translator.Assignable<UnityEngine.Vector2>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Vector2 endValue;translator.Get(L, 2, out endValue);
                    float duration = (float)LuaAPI.lua_tonumber(L, 3);
                    bool snapping = LuaAPI.lua_toboolean(L, 4);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOAnchorMin( endValue, duration, snapping );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& translator.Assignable<UnityEngine.Vector2>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector2 endValue;translator.Get(L, 2, out endValue);
                    float duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOAnchorMin( endValue, duration );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.RectTransform.DOAnchorMin!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOPivot(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Vector2 endValue;translator.Get(L, 2, out endValue);
                    float duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOPivot( endValue, duration );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOPivotX(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float endValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOPivotX( endValue, duration );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOPivotY(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float endValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOPivotY( endValue, duration );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOSizeDelta(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 4&& translator.Assignable<UnityEngine.Vector2>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Vector2 endValue;translator.Get(L, 2, out endValue);
                    float duration = (float)LuaAPI.lua_tonumber(L, 3);
                    bool snapping = LuaAPI.lua_toboolean(L, 4);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOSizeDelta( endValue, duration, snapping );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& translator.Assignable<UnityEngine.Vector2>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector2 endValue;translator.Get(L, 2, out endValue);
                    float duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOSizeDelta( endValue, duration );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.RectTransform.DOSizeDelta!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOPunchAnchorPos(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 6&& translator.Assignable<UnityEngine.Vector2>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)) 
                {
                    UnityEngine.Vector2 punch;translator.Get(L, 2, out punch);
                    float duration = (float)LuaAPI.lua_tonumber(L, 3);
                    int vibrato = LuaAPI.xlua_tointeger(L, 4);
                    float elasticity = (float)LuaAPI.lua_tonumber(L, 5);
                    bool snapping = LuaAPI.lua_toboolean(L, 6);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOPunchAnchorPos( punch, duration, vibrato, elasticity, snapping );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 5&& translator.Assignable<UnityEngine.Vector2>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    UnityEngine.Vector2 punch;translator.Get(L, 2, out punch);
                    float duration = (float)LuaAPI.lua_tonumber(L, 3);
                    int vibrato = LuaAPI.xlua_tointeger(L, 4);
                    float elasticity = (float)LuaAPI.lua_tonumber(L, 5);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOPunchAnchorPos( punch, duration, vibrato, elasticity );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& translator.Assignable<UnityEngine.Vector2>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Vector2 punch;translator.Get(L, 2, out punch);
                    float duration = (float)LuaAPI.lua_tonumber(L, 3);
                    int vibrato = LuaAPI.xlua_tointeger(L, 4);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOPunchAnchorPos( punch, duration, vibrato );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& translator.Assignable<UnityEngine.Vector2>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector2 punch;translator.Get(L, 2, out punch);
                    float duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOPunchAnchorPos( punch, duration );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.RectTransform.DOPunchAnchorPos!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOShakeAnchorPos(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 7&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 7)) 
                {
                    float duration = (float)LuaAPI.lua_tonumber(L, 2);
                    float strength = (float)LuaAPI.lua_tonumber(L, 3);
                    int vibrato = LuaAPI.xlua_tointeger(L, 4);
                    float randomness = (float)LuaAPI.lua_tonumber(L, 5);
                    bool snapping = LuaAPI.lua_toboolean(L, 6);
                    bool fadeOut = LuaAPI.lua_toboolean(L, 7);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOShakeAnchorPos( duration, strength, vibrato, randomness, snapping, fadeOut );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 6&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)) 
                {
                    float duration = (float)LuaAPI.lua_tonumber(L, 2);
                    float strength = (float)LuaAPI.lua_tonumber(L, 3);
                    int vibrato = LuaAPI.xlua_tointeger(L, 4);
                    float randomness = (float)LuaAPI.lua_tonumber(L, 5);
                    bool snapping = LuaAPI.lua_toboolean(L, 6);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOShakeAnchorPos( duration, strength, vibrato, randomness, snapping );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    float duration = (float)LuaAPI.lua_tonumber(L, 2);
                    float strength = (float)LuaAPI.lua_tonumber(L, 3);
                    int vibrato = LuaAPI.xlua_tointeger(L, 4);
                    float randomness = (float)LuaAPI.lua_tonumber(L, 5);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOShakeAnchorPos( duration, strength, vibrato, randomness );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    float duration = (float)LuaAPI.lua_tonumber(L, 2);
                    float strength = (float)LuaAPI.lua_tonumber(L, 3);
                    int vibrato = LuaAPI.xlua_tointeger(L, 4);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOShakeAnchorPos( duration, strength, vibrato );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    float duration = (float)LuaAPI.lua_tonumber(L, 2);
                    float strength = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOShakeAnchorPos( duration, strength );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    float duration = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOShakeAnchorPos( duration );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 7&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Vector2>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 7)) 
                {
                    float duration = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Vector2 strength;translator.Get(L, 3, out strength);
                    int vibrato = LuaAPI.xlua_tointeger(L, 4);
                    float randomness = (float)LuaAPI.lua_tonumber(L, 5);
                    bool snapping = LuaAPI.lua_toboolean(L, 6);
                    bool fadeOut = LuaAPI.lua_toboolean(L, 7);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOShakeAnchorPos( duration, strength, vibrato, randomness, snapping, fadeOut );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 6&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Vector2>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)) 
                {
                    float duration = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Vector2 strength;translator.Get(L, 3, out strength);
                    int vibrato = LuaAPI.xlua_tointeger(L, 4);
                    float randomness = (float)LuaAPI.lua_tonumber(L, 5);
                    bool snapping = LuaAPI.lua_toboolean(L, 6);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOShakeAnchorPos( duration, strength, vibrato, randomness, snapping );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Vector2>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    float duration = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Vector2 strength;translator.Get(L, 3, out strength);
                    int vibrato = LuaAPI.xlua_tointeger(L, 4);
                    float randomness = (float)LuaAPI.lua_tonumber(L, 5);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOShakeAnchorPos( duration, strength, vibrato, randomness );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Vector2>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    float duration = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Vector2 strength;translator.Get(L, 3, out strength);
                    int vibrato = LuaAPI.xlua_tointeger(L, 4);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOShakeAnchorPos( duration, strength, vibrato );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Vector2>(L, 3)) 
                {
                    float duration = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Vector2 strength;translator.Get(L, 3, out strength);
                    
                        DG.Tweening.Tweener __cl_gen_ret = __cl_gen_to_be_invoked.DOShakeAnchorPos( duration, strength );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.RectTransform.DOShakeAnchorPos!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOJumpAnchorPos(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 6&& translator.Assignable<UnityEngine.Vector2>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)) 
                {
                    UnityEngine.Vector2 endValue;translator.Get(L, 2, out endValue);
                    float jumpPower = (float)LuaAPI.lua_tonumber(L, 3);
                    int numJumps = LuaAPI.xlua_tointeger(L, 4);
                    float duration = (float)LuaAPI.lua_tonumber(L, 5);
                    bool snapping = LuaAPI.lua_toboolean(L, 6);
                    
                        DG.Tweening.Sequence __cl_gen_ret = __cl_gen_to_be_invoked.DOJumpAnchorPos( endValue, jumpPower, numJumps, duration, snapping );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 5&& translator.Assignable<UnityEngine.Vector2>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    UnityEngine.Vector2 endValue;translator.Get(L, 2, out endValue);
                    float jumpPower = (float)LuaAPI.lua_tonumber(L, 3);
                    int numJumps = LuaAPI.xlua_tointeger(L, 4);
                    float duration = (float)LuaAPI.lua_tonumber(L, 5);
                    
                        DG.Tweening.Sequence __cl_gen_ret = __cl_gen_to_be_invoked.DOJumpAnchorPos( endValue, jumpPower, numJumps, duration );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.RectTransform.DOJumpAnchorPos!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ExtSetActive(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool active = LuaAPI.lua_toboolean(L, 2);
                    
                    __cl_gen_to_be_invoked.ExtSetActive( active );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ExtSetLocalScaleXYZ(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float x = (float)LuaAPI.lua_tonumber(L, 2);
                    float y = (float)LuaAPI.lua_tonumber(L, 3);
                    float z = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    __cl_gen_to_be_invoked.ExtSetLocalScaleXYZ( x, y, z );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ExtSetLocalScaleX(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float x = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    __cl_gen_to_be_invoked.ExtSetLocalScaleX( x );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ExtSetLocalScaleY(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float y = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    __cl_gen_to_be_invoked.ExtSetLocalScaleY( y );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ExtSetLocalScaleZ(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float z = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    __cl_gen_to_be_invoked.ExtSetLocalScaleZ( z );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ExtSetLocalScale(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Vector3 value;translator.Get(L, 2, out value);
                    
                    __cl_gen_to_be_invoked.ExtSetLocalScale( value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rect(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.rect);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_anchorMin(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector2(L, __cl_gen_to_be_invoked.anchorMin);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_anchorMax(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector2(L, __cl_gen_to_be_invoked.anchorMax);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_anchoredPosition3D(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector3(L, __cl_gen_to_be_invoked.anchoredPosition3D);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_anchoredPosition(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector2(L, __cl_gen_to_be_invoked.anchoredPosition);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_sizeDelta(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector2(L, __cl_gen_to_be_invoked.sizeDelta);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_pivot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector2(L, __cl_gen_to_be_invoked.pivot);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_offsetMin(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector2(L, __cl_gen_to_be_invoked.offsetMin);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_offsetMax(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector2(L, __cl_gen_to_be_invoked.offsetMax);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_anchorMin(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector2 __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.anchorMin = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_anchorMax(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector2 __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.anchorMax = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_anchoredPosition3D(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector3 __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.anchoredPosition3D = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_anchoredPosition(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector2 __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.anchoredPosition = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_sizeDelta(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector2 __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.sizeDelta = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_pivot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector2 __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.pivot = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_offsetMin(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector2 __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.offsetMin = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_offsetMax(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.RectTransform __cl_gen_to_be_invoked = (UnityEngine.RectTransform)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector2 __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.offsetMax = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _e_reapplyDrivenProperties(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    int __gen_param_count = LuaAPI.lua_gettop(L);
                UnityEngine.RectTransform.ReapplyDrivenProperties __gen_delegate = translator.GetDelegate<UnityEngine.RectTransform.ReapplyDrivenProperties>(L, 2);
                if (__gen_delegate == null) {
                    return LuaAPI.luaL_error(L, "#2 need UnityEngine.RectTransform.ReapplyDrivenProperties!");
                }
                
				
				if (__gen_param_count == 2 && LuaAPI.xlua_is_eq_str(L, 1, "+")) {
					UnityEngine.RectTransform.reapplyDrivenProperties += __gen_delegate;
					return 0;
				} 
				
				
				if (__gen_param_count == 2 && LuaAPI.xlua_is_eq_str(L, 1, "-")) {
					UnityEngine.RectTransform.reapplyDrivenProperties -= __gen_delegate;
					return 0;
				} 
				
			} catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
			return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.RectTransform.reapplyDrivenProperties!");
        }
        
    }
}
