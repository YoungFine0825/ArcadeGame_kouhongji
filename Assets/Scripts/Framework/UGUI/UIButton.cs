/********************************************************************
	created:	2018-8-15
	author:		jordenwu
	
	purpose:	UGUI  Button扩展
*********************************************************************/
using UnityEngine;
using UnityEngine.EventSystems;
using JW.Common;
using JW.Framework.Audio;
using UnityEngine.UI;
using JW.Res;
using UnityEngine.Events;

namespace JW.Framework.UGUI
{
    [AddComponentMenu("JW_UGUI/UIButton")]
    public class UIButton: MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
    {
        public delegate void PlayAudioDelegate(string resName);

        public enum State
        {
            Normal,
            Hover,
            Pressed,
            Disabled,
        }

        public enum TriggerType
        {
            None,
            PressDown,
            PressUp,
            Click
        }

        public enum BehaviorType
        {
            None,
            Preset,
            Custom
        }

        private enum PlayType
        {
            Stop,
            Positive,
            Half,
            Negative
        }

        public const string ConfigFilename = "Fixed/UICfg/UIButtonCfg";
        private static UICurveCfg _cmCurve;
        private static UIAudioCfg _cmAudio;

        // 全局事件
        public static PlayAudioDelegate PlayAudioHandler;

        // 点击动画配置
        public BehaviorType PressCurveType = BehaviorType.Preset;
        public string PressPresetName = "Normal";
        public AnimationCurve PressCurve;

        // 音效配置
        public TriggerType AudioTrigger = TriggerType.Click;

        public BehaviorType AudioType = BehaviorType.Preset;
        public string AudioPresetName = "Click";
        //自定义声音资源名称
        public string CustomAudioName;

        //
        private Vector2 _downPosition;
        public UnityEvent onClick=new UnityEvent();
        //

        //原本信息
        private Transform _cacheTf;
        private Vector3 _originalScale;
        // 点击动画
        private PlayType _pressPlayType = PlayType.Stop;
        private float _pressPlayTime;
        [System.NonSerialized] protected bool mInitDone = false;
        [System.NonSerialized] protected State mState = State.Normal;
        public State state { get { return mState; } set { SetState(value, false); } }

        private void Awake()
        {
            if (_cmCurve == null)
            {
                GameObject go = GameObject.Find(ConfigFilename);
                if (go == null)
                {
#if UNITY_EDITOR
                    go = (GameObject)Resources.Load(ConfigFilename).ExtInstantiate();
#else
                    ResObj resource = ResService.GetResource(ConfigFilename);
                    if (resource != null)
                    {
                        go = (GameObject)resource.Content.ExtInstantiate();
                        ResService.UnloadResource(resource);
                    }
#endif
                    if (null != go)
                    {
                        ExtObject.ExtDontDestroyOnLoad(go);
                    }
                }

                if (go != null)
                {
                    go.name = ConfigFilename;
                    _cmCurve = go.ExtGetComponent<UICurveCfg>();
                    _cmAudio = go.ExtGetComponent<UIAudioCfg>();
                }
            }

            _cacheTf = transform;
            _originalScale = _cacheTf.localScale;
        }

        private void Start()
        {
            if (!mInitDone) OnInit();
            if (!isActiveAndEnabled) SetState(State.Disabled, true);
        }

        private void OnInit()
        {
            mInitDone = true;
        }

        private void OnEnable()
        {
            if (mInitDone)
            {
                SetState(State.Normal, true);
            }
        }

        private void OnDisable()
        {
            if (mInitDone)
            {
                SetState(State.Normal, true);
            }
        }

        public void SetState(State s, bool instant)
        {
            if (!mInitDone)
            {
                mInitDone = true;
                OnInit();
            }

            //JW.Common.Log.LogD("UIButtonExt SetState:"+s.ToString());
            if (mState != s)
            {
                mState = s;
                UpdatePressScale(instant);
            }
        }

        private void OnPress(bool isPressed)
        {
            if (isActiveAndEnabled)
            {
                if (!mInitDone) OnInit();

                if (isPressed)
                {
                    SetState(State.Pressed, false);
                }
                else if (EventSystem.current.currentSelectedGameObject == gameObject)
                {
                    SetState(State.Hover, false);
                }
                else
                {
                    SetState(State.Normal, false);
                }
                //
                PlayAudio(isPressed ? TriggerType.PressDown : TriggerType.PressUp);
            }
        }

        protected void Update()
        {
            if (_pressPlayType == PlayType.Stop || _pressPlayType == PlayType.Half)
            {
                return;
            }

            AnimationCurve curve = GetPressCurve();
            if (curve == null)
            {
                return;
            }

            Keyframe lastKf = curve[curve.length - 1];
            float halfTime = lastKf.time * 0.5f;

            _pressPlayTime += Time.deltaTime;
            if (_pressPlayType == PlayType.Positive)
            {
                _pressPlayTime = Mathf.Clamp(_pressPlayTime, 0.0f, halfTime);

                float scale = curve.Evaluate(_pressPlayTime);
                _cacheTf.localScale = _originalScale * scale;

                if (Mathf.Approximately(_pressPlayTime, halfTime))
                {
                    _pressPlayType = PlayType.Half;
                    _pressPlayTime = halfTime;
                }
            }
            else if (_pressPlayType == PlayType.Negative)
            {
                _pressPlayTime = Mathf.Clamp(_pressPlayTime, halfTime, lastKf.time);

                if (Mathf.Approximately(_pressPlayTime, lastKf.time))
                {
                    _cacheTf.localScale = _originalScale;
                    _pressPlayType = PlayType.Stop;
                    _pressPlayTime = 0.0f;
                }
                else
                {
                    float scale = curve.Evaluate(_pressPlayTime);
                    _cacheTf.localScale = _originalScale * scale;
                    if (scale > 0f)
                    {
                        scale = 1f / scale;
                    }
                }
            }
        }

        private void UpdatePressScale(bool instant)
        {
            AnimationCurve curve = GetPressCurve();
            if (curve == null)
            {
                return;
            }

            Keyframe lastKf = curve[curve.length - 1];
            float halfTime = lastKf.time * 0.5f;

            bool isPress = mState == State.Pressed;

            // 立即生效
            if (instant)
            {
                if (isPress)
                {
                    if (_pressPlayType != PlayType.Half)
                    {
                        float scale = curve.Evaluate(halfTime);
                        _cacheTf.localScale = _originalScale * scale;
                        _pressPlayType = PlayType.Half;
                        _pressPlayTime = halfTime;

                        if (scale > 0f)
                        {
                            scale = 1f / scale;
                        }
                    }
                }
                else
                {
                    if (_pressPlayType != PlayType.Stop)
                    {
                        _cacheTf.localScale = _originalScale;
                        _pressPlayType = PlayType.Stop;
                        _pressPlayTime = 0.0f;
                    }
                }

                return;
            }

            // 动画过程
            switch (_pressPlayType)
            {
                case PlayType.Stop:
                    if (isPress)
                    {
                        _pressPlayType = PlayType.Positive;
                        _pressPlayTime = 0.0f;
                    }
                    break;

                case PlayType.Positive:
                    if (!isPress)
                    {
                        _pressPlayType = PlayType.Negative;

                        if (!GetCurveTime(curve, halfTime, lastKf.time, curve.Evaluate(_pressPlayTime), out _pressPlayTime))
                        {
                            _pressPlayTime = halfTime;
                        }
                    }
                    break;

                case PlayType.Half:
                    if (!isPress)
                    {
                        _pressPlayType = PlayType.Negative;
                        _pressPlayTime = halfTime;
                    }
                    break;

                case PlayType.Negative:
                    if (isPress)
                    {
                        _pressPlayType = PlayType.Positive;

                        if (!GetCurveTime(curve, 0f, halfTime, curve.Evaluate(_pressPlayTime), out _pressPlayTime))
                        {
                            _pressPlayTime = 0f;
                        }
                    }
                    break;
            }
        }

        private AnimationCurve GetPressCurve()
        {
            AnimationCurve curve = null;

            switch (PressCurveType)
            {
                case BehaviorType.Preset:
                    curve = _cmCurve != null ? _cmCurve.GetCurve(PressPresetName) : null;
                    break;

                case BehaviorType.Custom:
                    curve = PressCurve;
                    break;
            }

            return curve == null || curve.length < 3 ? null : curve;
        }

        private bool GetCurveTime(AnimationCurve curve, float startTime, float endTime, float value, out float time)
        {
            const float stepTime = 0.02f;
            time = 0f;

            bool less = value < curve.Evaluate(startTime);

            for (; startTime < endTime; startTime += stepTime)
            {
                startTime = Mathf.Clamp(startTime, 0f, endTime);

                float v = curve.Evaluate(startTime);
                if ((less && v <= value) || (!less && v >= value))
                {
                    time = startTime;
                    return true;
                }
            }

            return false;
        }

        private void PlayAudio(TriggerType triggerType)
        {
            if (triggerType != AudioTrigger)
            {
                return;
            }
            string resName = null;
            //
            switch (AudioType)
            {
                case BehaviorType.None:
                    break;

                case BehaviorType.Preset:
                    _cmAudio.GetAudio(AudioPresetName, out resName);
                    break;

                case BehaviorType.Custom:
                    resName = CustomAudioName;
                    break;
            }

            if (string.IsNullOrEmpty(resName))
            {
                return;
            }

            if (PlayAudioHandler != null)
            {
                PlayAudioHandler(resName);
            }
        }

        //-----------------------------------
        public void OnPointerClick(PointerEventData eventData)
        {
            //JW.Common.Log.LogD("UIButtonExt OnPointerClick");
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //JW.Common.Log.LogD("UIButtonExt OnPointerDown");
            _downPosition = eventData.position;
            OnPress(true);

        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //JW.Common.Log.LogD("UIButtonExt OnPointerUp");
            OnPress(false);

            //响应click
            if (Vector2.SqrMagnitude(eventData.position - _downPosition) < 1200)
            {
                PlayAudio(TriggerType.Click);
                if (onClick != null)
                {
                    onClick.Invoke();
                }
            }

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //JW.Common.Log.LogD("UIButtonExt OnPointerExit");
            if (isActiveAndEnabled)
            {
                if (!mInitDone) OnInit();
                SetState(State.Normal, false);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //JW.Common.Log.LogD("UIButtonExt OnPointerEnter");
            if (isActiveAndEnabled)
            {
                if (!mInitDone) OnInit();
                SetState(State.Hover, false);
            }
        }
        //-----------------------------------------
    }
}