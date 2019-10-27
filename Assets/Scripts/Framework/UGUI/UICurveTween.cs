using UnityEngine;
using UnityEngine.EventSystems;
using JW.Common;
using JW.Framework.Audio;
using UnityEngine.UI;
using JW.Res;
using UnityEngine.Events;

namespace JW.Framework.UGUI
{
    [AddComponentMenu("JW_UGUI/UICurveTween")]
    public class UICurveTween : MonoBehaviour
    {
        [Header("动画曲线")]
        public AnimationCurve _curve;

        [Header("使用配置的按钮动画曲线")]
        public bool ApplyButtonClickCurve=true;

        public const string ConfigFilename = "Fixed/UICfg/UIButtonCfg";

        //原本信息
        private Transform _cacheTf;
        private Vector3 _originalScale;
        //
        private float _playedTime;
        private float _lastKeyTime;
        private bool _isPlay = false;

        private void Awake()
        {
            if (ApplyButtonClickCurve)
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
                    UICurveCfg cfg= go.ExtGetComponent<UICurveCfg>();
                    _curve = cfg.GetCurve("Normal");
                }
            }
            //
            if (_curve != null)
            {
                Keyframe lastKf = _curve[_curve.length - 1];
                _lastKeyTime = lastKf.time;
            }
            _cacheTf = transform;
            _originalScale = _cacheTf.localScale;
        }

        public void Play()
        {
            _cacheTf.localScale = _originalScale;
            _playedTime = 0.0f;
            _isPlay = true;
        }

        protected void Update()
        {
            if (_curve == null)
            {
                return;
            }
            if (_isPlay)
            {
                _playedTime += Time.deltaTime;
                if (Mathf.Approximately(_playedTime, _lastKeyTime))
                {
                    _cacheTf.localScale = _originalScale;
                    _playedTime = 0.0f;
                    _isPlay = false;
                }
                else
                {
                    float scale = _curve.Evaluate(_playedTime);
                    _cacheTf.localScale = _originalScale * scale;
                }
            }
        }

        protected void OnEnable()
        {
            _cacheTf.localScale = _originalScale;
            _playedTime = 0.0f;
            _isPlay = false;
        }

        protected void OnDisable()
        {
            _cacheTf.localScale = _originalScale;
            _playedTime = 0.0f;
            _isPlay = false;
        }
    }
}