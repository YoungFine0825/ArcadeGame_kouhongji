/********************************************************************
	created:	2018-07-02
	filename: 	LuaGenerator
	author:		jordenwu
	
	purpose:	Lua导出配置
*********************************************************************/
using System;
using System.Collections.Generic;
using JW.Common;
using XLua;
using UnityEngine;
using Action = System.Action;
using JW.Framework.UGUI;
using JW.Framework.MVC;
using JW.Framework.Asset;
using JW.PLink;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using JW.Framework.Audio;
using JW.Framework.QR;

namespace JW.Lua
{
    public static class LuaGenExport
    {
        [LuaCallCSharp]
        public static List<Type> LuaCallCSharp = new List<Type>()
        {
                    // Unity
                    typeof(object),
                    typeof(UnityEngine.Object),
                    typeof(Application),
                    typeof(GameObject),
                    typeof(Component),
                    typeof(Behaviour),
                    typeof(Transform),
                    typeof(Vector2),
                    typeof(Vector3),
                    typeof(Vector4),
                    typeof(Quaternion),
                    typeof(Resources),
                    typeof(TextAsset),
                    typeof(Renderer),
                    typeof(Collider),
                    typeof(BoxCollider),
                    typeof(BoxCollider2D),
                    typeof(MeshRenderer),
                    typeof(Bounds),
                    typeof(Texture),
                    typeof(Texture2D),
                    typeof(RenderTexture),
                    typeof(Color),
                    typeof(PlayerPrefs),
                    typeof(Time),
                    typeof(Input),
                    typeof(Collider2D),
                    typeof(Animation),
                    typeof(PlayMode),
                    typeof(AnimationCurve),
                    typeof(ParticleSystem),
                    typeof(SkinnedMeshRenderer),
                    typeof(Camera),
                    typeof(Screen),
                    typeof(ScreenOrientation),
                    typeof(Animator),
                    typeof(RaycastHit),
                    typeof(Material),
                    typeof(Texture),
                    typeof(Texture2D),
                    typeof(MaterialPropertyBlock),
                    //
                    typeof(SpriteRenderer),
                    typeof(Sprite),
                    typeof(Input),
                    typeof(TouchPhase),
                    typeof(Touch),
                    typeof(Application),
                    typeof(Camera),
                    typeof(GameObject),
                    typeof(UnityEngine.Object),
                    typeof(UnityEngine.Random),
                    typeof(RectTransform),
                    typeof(Image),
                    typeof(Text),
                    typeof(Button),
                    typeof(UnityAction),
                    typeof(UnityEvent),
                    typeof(Toggle.ToggleEvent),
                    typeof(Canvas),
                    typeof(GraphicRaycaster),
                    typeof(Slider),
                    typeof(UnityEngine.UI.ScrollRect),
                    typeof(InputField),
                    typeof(CanvasGroup),
                    typeof(AnimationCurve),
                    typeof(Animation),
                    typeof(AnimatorControllerParameter),
                    typeof(TextMesh),
                    typeof(ParticleSystem),
                    typeof(ParticleSystemRenderer),

                    typeof(Physics),
                    typeof(Ray),
                    typeof(RaycastHit),
                    typeof(Physics2D),
                    typeof(Ray2D),
                    typeof(RaycastHit2D),
                    typeof(ValueType),
                    typeof(PointerEventData),

                    typeof(DG.Tweening.AutoPlay),
                    typeof(DG.Tweening.AxisConstraint),
                    typeof(DG.Tweening.Ease),
                    typeof(DG.Tweening.LogBehaviour),
                    typeof(DG.Tweening.LoopType),
                    typeof(DG.Tweening.PathMode),
                    typeof(DG.Tweening.PathType),
                    typeof(DG.Tweening.RotateMode),
                    typeof(DG.Tweening.ScrambleMode),
                    typeof(DG.Tweening.TweenType),
                    typeof(DG.Tweening.UpdateType),

                    typeof(DG.Tweening.DOTween),
                    typeof(DG.Tweening.DOVirtual),
                    typeof(DG.Tweening.EaseFactory),
                    typeof(DG.Tweening.Tweener),
                    typeof(DG.Tweening.Tween),
                    typeof(DG.Tweening.Sequence),
                    typeof(DG.Tweening.TweenParams),
                    typeof(DG.Tweening.Core.ABSSequentiable),
                    typeof(DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions>),
                    typeof(DG.Tweening.TweenCallback),
                    typeof(DG.Tweening.TweenExtensions),
                    typeof(DG.Tweening.TweenSettingsExtensions),
                    typeof(DG.Tweening.ShortcutExtensions),
                    typeof(DG.Tweening.ShortcutExtensions43),
                    typeof(DG.Tweening.ShortcutExtensions46),
                    typeof(DG.Tweening.ShortcutExtensions50),
                    //
                    typeof(PlayerPrefs),
                    typeof(BoxCollider2D),
                    typeof(Bounds),
                    typeof(Event),
                    typeof(EventType),
                    typeof(Mathf),
                    typeof(System.DateTime),
                    typeof(Ray),
                    typeof(RaycastHit),
                    //物理关节
                    typeof(ConfigurableJoint),
                    typeof(ExtConfigurableJoint),
                    //Ext
                    typeof(ExtGameObject),
                    typeof(ExtObject),
                    typeof(ExtTransform),
                    typeof(ExtRectTransform),
                    //PLink
                    typeof(PrefabLink),
                    //UGUI
                    typeof(UIFormLink),
                    typeof(UIForm),
                    typeof(UIStateService),
                    typeof(UGUIRoot),
                    typeof(JW.Framework.UGUI.UIComponent),
                    typeof(JW.Framework.UGUI.UIEventListener),
                    typeof(UnityEvent<PointerEventData>),
                    typeof(UnityEvent),
                    typeof(UIListenerEvent),
                    typeof(UIListView),
                    typeof(UIListViewItem),
                    typeof(Slider.SliderEvent),
                    typeof(UIHttpImage),
                    typeof(UIQRImage),
                    //Res
                    typeof(JW.Res.FileUtil),
                    typeof(JW.Res.ResService),
                    //IFS
                    typeof(JW.Framework.IFS.IFSService),
                    typeof(JW.Framework.IFS.IFSState),
                    typeof(JW.Framework.IFS.IFSUpdateCheckerResult),
                    typeof(JW.Framework.IFS.IFSLocalCheckerResult),
                    //Asset
                    typeof(BaseAsset),
                    typeof(UIAsset),
                    typeof(ModelAsset),
                    typeof(AssetService),
                    //Audio
                    typeof(AudioService),
                    typeof(JW.Framework.Scene.SceneService),
                    typeof(LuaInteraction),
                    typeof(LuaGlobal),
                    typeof(LuaService),
                    typeof(UIDropDown),

                    typeof(JW.Framework.Http.HttpService),
                    typeof(JW.Framework.Native.NativeResult),
                    typeof(JW.Framework.Native.NativeService),
                    //Network
                    typeof(JW.Framework.Network.NetConnector),
                    typeof(JW.Framework.Network.NetworkService),
                    //
                    typeof(XH.Pin.LSKRotateComponent),
                    typeof(XH.Pin.LSKMoveComponent),
                    typeof(XH.SlotScroll.SlotScroll),
                    typeof(XH.SlotScroll.SlotScrollGrid),

                    //Quality
                    typeof(JW.Framework.Quality.QualityService),
                    //NetAsset
                    typeof(JW.Framework.NetAsset.NetAssetInfo),
                    typeof(JW.Framework.NetAsset.NetAssetService),
                    //绳子插件导出
                    typeof(UltimateRope),
                    //物理监视器
                    typeof(PhyMonitor3D),
                    //视频播放
                    typeof(RenderHeads.Media.AVProVideo.MediaPlayer),

        };

        [CSharpCallLua]
        public static List<Type> CSharpCallLua = new List<Type>()
        {
                    typeof(Action),
                    typeof(Action<int>),
                    typeof(Action<float>),
                    typeof(Action<bool>),
                    typeof(Action<string>),
                    typeof(Action<int, string>),
                    typeof(Action<bool,float>),
                    typeof(Action<Vector2>),
                    typeof(Action<Collider,Vector3>),
                    typeof(UnityAction<int>),
                    typeof(UnityAction<int,int>),
                    typeof(UnityAction<PointerEventData>),
                    typeof(Action<UnityEngine.Object,UnityEngine.Object>),
                    typeof(System.Action<UIListViewItem>),
                    typeof(System.Action<UIListViewItem,UIListViewItem>),
                    typeof(Action<string, Texture>),
                    typeof(Action<string, Transform>),
                    typeof(Action<string,string>),
                    typeof(Action<int, PrefabLink>),
                    //
                    typeof(LuaFramework.OnChangeUIStateDelegate),
                    typeof(LuaFramework.OnLoadAssetCompletedDelegate),
                    //
                    typeof(LuaUIEventBridge.OnUIEventDelegate),
                    typeof(JW.Framework.UGUI.UIMsgBoxLuaDelegate),
                    typeof(JW.Lua.LuaUIEventBridge.OnUIEventDelegate),
                    typeof(JW.Lua.LuaUIEventBridge.OnUIEventListnerDelegate),
                    typeof(JW.Framework.Scene.SceneService.OnUnitySceneLoadDelegate),
                    //
                    typeof(JW.Framework.IFS.IFSSessionDelegate),
                    typeof(JW.Framework.IFS.IFSUpdateCheckerDelegate),
                    typeof(JW.Framework.IFS.IFSLocalCheckerDelegate),
                    typeof(JW.Framework.Native.NativeServiceBaseDelegate),
                    //Arcade Input
                    typeof(JW.Framework.ArcadeInput.ArcadeInputPressDelegate),
                    typeof(JW.Framework.ArcadeInput.ArcadeInputRockerDelegate),
                    typeof(JW.Framework.ArcadeInput.ArcadeInputRotateDelegate),
                    typeof(JW.Framework.ArcadeInput.ArcadeInputRefreshDelegate),
                    //物理监视器
                    typeof(PhyMonitor3D.PhyMonitor3DDelegate),
                    //
                    typeof(JW.Framework.Http.HttpFileSessionDelegate)
        };

        [BlackList]
        public static List<List<string>> BlackList = new List<List<string>>()
        {
                    new List<string>{"UnityEngine.Texture2D", "alphaIsTransparency"},
                    new List<string>{"UnityEngine.Input", "IsJoystickPreconfigured","System.String"},
                    new List<string>{"UnityEngine.Texture", "imageContentsHash"},
                    new List<string>{"UnityEngine.UI.Text","OnRebuildRequested"},
                    new List<string>{"UnityEngine.UI.Graphic","OnRebuildRequested"},
                    new List<string>{ "UnityEngine.AnimatorControllerParameter", "name"},
                    new List<string>{ "UnityEngine.WWW", "GetMovieTexture"},
                    new List<string>{ "RenderHeads.Media.AVProVideo.MediaPlayer", "GetPlatformOptions"},
                    new List<string>{ "RenderHeads.Media.AVProVideo.MediaPlayer", "GetPlatformOptionsVariable"}
        };
    }

}
