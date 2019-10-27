#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using System;
using System.Collections.Generic;
using System.Reflection;


namespace XLua.CSObjectWrap
{
    public class XLua_Gen_Initer_Register__
	{
	    static XLua_Gen_Initer_Register__()
        {
		    XLua.LuaEnv.AddIniter((luaenv, translator) => {
			    
				translator.DelayWrapLoader(typeof(object), SystemObjectWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Object), UnityEngineObjectWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Application), UnityEngineApplicationWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.GameObject), UnityEngineGameObjectWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Component), UnityEngineComponentWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Behaviour), UnityEngineBehaviourWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Transform), UnityEngineTransformWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Vector2), UnityEngineVector2Wrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Vector3), UnityEngineVector3Wrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Vector4), UnityEngineVector4Wrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Quaternion), UnityEngineQuaternionWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Resources), UnityEngineResourcesWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.TextAsset), UnityEngineTextAssetWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Renderer), UnityEngineRendererWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Collider), UnityEngineColliderWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.BoxCollider), UnityEngineBoxColliderWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.BoxCollider2D), UnityEngineBoxCollider2DWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.MeshRenderer), UnityEngineMeshRendererWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Bounds), UnityEngineBoundsWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Texture), UnityEngineTextureWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Texture2D), UnityEngineTexture2DWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.RenderTexture), UnityEngineRenderTextureWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Color), UnityEngineColorWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.PlayerPrefs), UnityEnginePlayerPrefsWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Time), UnityEngineTimeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Input), UnityEngineInputWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Collider2D), UnityEngineCollider2DWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Animation), UnityEngineAnimationWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.PlayMode), UnityEnginePlayModeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.AnimationCurve), UnityEngineAnimationCurveWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.ParticleSystem), UnityEngineParticleSystemWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.SkinnedMeshRenderer), UnityEngineSkinnedMeshRendererWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Camera), UnityEngineCameraWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Screen), UnityEngineScreenWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.ScreenOrientation), UnityEngineScreenOrientationWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Animator), UnityEngineAnimatorWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.RaycastHit), UnityEngineRaycastHitWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Material), UnityEngineMaterialWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.MaterialPropertyBlock), UnityEngineMaterialPropertyBlockWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.SpriteRenderer), UnityEngineSpriteRendererWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Sprite), UnityEngineSpriteWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.TouchPhase), UnityEngineTouchPhaseWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Touch), UnityEngineTouchWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Random), UnityEngineRandomWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.RectTransform), UnityEngineRectTransformWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.UI.Image), UnityEngineUIImageWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.UI.Text), UnityEngineUITextWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.UI.Button), UnityEngineUIButtonWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Events.UnityEvent), UnityEngineEventsUnityEventWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.UI.Toggle.ToggleEvent), UnityEngineUIToggleToggleEventWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Canvas), UnityEngineCanvasWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.UI.GraphicRaycaster), UnityEngineUIGraphicRaycasterWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.UI.Slider), UnityEngineUISliderWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.UI.ScrollRect), UnityEngineUIScrollRectWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.UI.InputField), UnityEngineUIInputFieldWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.CanvasGroup), UnityEngineCanvasGroupWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.AnimatorControllerParameter), UnityEngineAnimatorControllerParameterWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.TextMesh), UnityEngineTextMeshWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.ParticleSystemRenderer), UnityEngineParticleSystemRendererWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Physics), UnityEnginePhysicsWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Ray), UnityEngineRayWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Physics2D), UnityEnginePhysics2DWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Ray2D), UnityEngineRay2DWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.RaycastHit2D), UnityEngineRaycastHit2DWrap.__Register);
				
				translator.DelayWrapLoader(typeof(System.ValueType), SystemValueTypeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.EventSystems.PointerEventData), UnityEngineEventSystemsPointerEventDataWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.AutoPlay), DGTweeningAutoPlayWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.AxisConstraint), DGTweeningAxisConstraintWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.Ease), DGTweeningEaseWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.LogBehaviour), DGTweeningLogBehaviourWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.LoopType), DGTweeningLoopTypeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.PathMode), DGTweeningPathModeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.PathType), DGTweeningPathTypeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.RotateMode), DGTweeningRotateModeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.ScrambleMode), DGTweeningScrambleModeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.TweenType), DGTweeningTweenTypeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.UpdateType), DGTweeningUpdateTypeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.DOTween), DGTweeningDOTweenWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.DOVirtual), DGTweeningDOVirtualWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.EaseFactory), DGTweeningEaseFactoryWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.Tweener), DGTweeningTweenerWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.Tween), DGTweeningTweenWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.Sequence), DGTweeningSequenceWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.TweenParams), DGTweeningTweenParamsWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.Core.ABSSequentiable), DGTweeningCoreABSSequentiableWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions>), DGTweeningCoreTweenerCore_3_UnityEngineVector3UnityEngineVector3DGTweeningPluginsOptionsVectorOptions_Wrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.TweenExtensions), DGTweeningTweenExtensionsWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.TweenSettingsExtensions), DGTweeningTweenSettingsExtensionsWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.ShortcutExtensions), DGTweeningShortcutExtensionsWrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.ShortcutExtensions43), DGTweeningShortcutExtensions43Wrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.ShortcutExtensions46), DGTweeningShortcutExtensions46Wrap.__Register);
				
				translator.DelayWrapLoader(typeof(DG.Tweening.ShortcutExtensions50), DGTweeningShortcutExtensions50Wrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Event), UnityEngineEventWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.EventType), UnityEngineEventTypeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Mathf), UnityEngineMathfWrap.__Register);
				
				translator.DelayWrapLoader(typeof(System.DateTime), SystemDateTimeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.ConfigurableJoint), UnityEngineConfigurableJointWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Common.ExtConfigurableJoint), JWCommonExtConfigurableJointWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Common.ExtGameObject), JWCommonExtGameObjectWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Common.ExtObject), JWCommonExtObjectWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Common.ExtTransform), JWCommonExtTransformWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Common.ExtRectTransform), JWCommonExtRectTransformWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.PLink.PrefabLink), JWPLinkPrefabLinkWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.UGUI.UIFormLink), JWFrameworkUGUIUIFormLinkWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.UGUI.UIForm), JWFrameworkUGUIUIFormWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.MVC.UIStateService), JWFrameworkMVCUIStateServiceWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.UGUI.UGUIRoot), JWFrameworkUGUIUGUIRootWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.UGUI.UIComponent), JWFrameworkUGUIUIComponentWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.UGUI.UIEventListener), JWFrameworkUGUIUIEventListenerWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.PointerEventData>), UnityEngineEventsUnityEvent_1_UnityEngineEventSystemsPointerEventData_Wrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.UGUI.UIListenerEvent), JWFrameworkUGUIUIListenerEventWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.UGUI.UIListView), JWFrameworkUGUIUIListViewWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.UGUI.UIListViewItem), JWFrameworkUGUIUIListViewItemWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.UI.Slider.SliderEvent), UnityEngineUISliderSliderEventWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.UGUI.UIHttpImage), JWFrameworkUGUIUIHttpImageWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.QR.UIQRImage), JWFrameworkQRUIQRImageWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Res.FileUtil), JWResFileUtilWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Res.ResService), JWResResServiceWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.IFS.IFSService), JWFrameworkIFSIFSServiceWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.IFS.IFSState), JWFrameworkIFSIFSStateWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.IFS.IFSUpdateCheckerResult), JWFrameworkIFSIFSUpdateCheckerResultWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.IFS.IFSLocalCheckerResult), JWFrameworkIFSIFSLocalCheckerResultWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.Asset.BaseAsset), JWFrameworkAssetBaseAssetWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.Asset.UIAsset), JWFrameworkAssetUIAssetWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.Asset.ModelAsset), JWFrameworkAssetModelAssetWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.Asset.AssetService), JWFrameworkAssetAssetServiceWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.Audio.AudioService), JWFrameworkAudioAudioServiceWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.Scene.SceneService), JWFrameworkSceneSceneServiceWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Lua.LuaInteraction), JWLuaLuaInteractionWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Lua.LuaGlobal), JWLuaLuaGlobalWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Lua.LuaService), JWLuaLuaServiceWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UIDropDown), UIDropDownWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.Http.HttpService), JWFrameworkHttpHttpServiceWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.Native.NativeResult), JWFrameworkNativeNativeResultWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.Native.NativeService), JWFrameworkNativeNativeServiceWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.Network.NetConnector), JWFrameworkNetworkNetConnectorWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.Network.NetworkService), JWFrameworkNetworkNetworkServiceWrap.__Register);
				
				translator.DelayWrapLoader(typeof(XH.Pin.LSKRotateComponent), XHPinLSKRotateComponentWrap.__Register);
				
				translator.DelayWrapLoader(typeof(XH.Pin.LSKMoveComponent), XHPinLSKMoveComponentWrap.__Register);
				
				translator.DelayWrapLoader(typeof(XH.SlotScroll.SlotScroll), XHSlotScrollSlotScrollWrap.__Register);
				
				translator.DelayWrapLoader(typeof(XH.SlotScroll.SlotScrollGrid), XHSlotScrollSlotScrollGridWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.Quality.QualityService), JWFrameworkQualityQualityServiceWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.NetAsset.NetAssetInfo), JWFrameworkNetAssetNetAssetInfoWrap.__Register);
				
				translator.DelayWrapLoader(typeof(JW.Framework.NetAsset.NetAssetService), JWFrameworkNetAssetNetAssetServiceWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UltimateRope), UltimateRopeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(PhyMonitor3D), PhyMonitor3DWrap.__Register);
				
				translator.DelayWrapLoader(typeof(RenderHeads.Media.AVProVideo.MediaPlayer), RenderHeadsMediaAVProVideoMediaPlayerWrap.__Register);
				
				
				
			});
		}
		
		
	}
	
}
namespace XLua
{
	public partial class ObjectTranslator
	{
		static XLua.CSObjectWrap.XLua_Gen_Initer_Register__ s_gen_reg_dumb_obj = new XLua.CSObjectWrap.XLua_Gen_Initer_Register__();
		static XLua.CSObjectWrap.XLua_Gen_Initer_Register__ gen_reg_dumb_obj {get{return s_gen_reg_dumb_obj;}}
	}
	
	internal partial class InternalGlobals
    {
	    
	    static InternalGlobals()
		{
		    extensionMethodMap = new Dictionary<Type, IEnumerable<MethodInfo>>()
			{
			    
			};
			
			genTryArrayGetPtr = StaticLuaCallbacks.__tryArrayGet;
            genTryArraySetPtr = StaticLuaCallbacks.__tryArraySet;
		}
	}
}
