using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JW.Common;

namespace JW.Framework.UGUI
{
    public enum UIAnimatorEventType
    {
        AnimatorStart,
        AnimatorEnd
    };

    public class UIAnimator : UIComponent
    {
        //Unity Animator
        private Animator _animator;

        //正在播放的Animator State 信息
        public string CurrentAnimatorStateName
        {
            get;
            private set;
        }
        //
        private int _currentAnimatorStateCounter;

        //被隐藏前自己是否启用
        private bool _isEnableBeHide = true;
        //被隐藏前Animator是否启用          
        private bool _isEnableAnimatorBeHide = true;

        /// 初始化
        public override void Initialize(UIForm form)
        {
            if (_isInited)
            {
                return;
            }

            base.Initialize(form);

            _animator = this.gameObject.GetComponent<Animator>();
            if (_animator == null)
            {
                JW.Common.Log.LogE("UIAnimator Need Animator");
            }
            else
            {
                _isEnableBeHide = this.enabled;
                _isEnableAnimatorBeHide = _animator.enabled;
            }

        }

        /// Update
        void Update()
        {
            //if (BelongedForm != null && BelongedForm.IsClosed())
            //{
            //    return;
            //}

            if (CurrentAnimatorStateName == null)
            {
                return;
            }

            //normalizedTime的整数位表示第几次播放，小数位表示播放进度(很奇怪的animator会循环播放，这个数字会不断增大)
            //if (false)//((_animator.GetCurrentAnimatorStateInfo(0).normalizedTime) > 1.0f && !_animator.IsInTransition(0))
            if ((int)_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > _currentAnimatorStateCounter)
            {
                _animator.StopPlayback();

                string tempStr = CurrentAnimatorStateName;
                CurrentAnimatorStateName = null;
                _currentAnimatorStateCounter = 0;

                //派发事件
                DispatchAnimatorEvent(UIAnimatorEventType.AnimatorEnd, tempStr);

                /*
                //禁用组件节省开销
                if (_animator.enabled == true)
                {
                    _animator.enabled = false;
                }
                this.enabled = false;
                */
            }
        }

        /// 播放动画
        public void PlayAnimator(string stateName)
        {
            if (_animator == null)
            {
                _animator = this.gameObject.GetComponent<Animator>();
            }
            if (_animator == null)
            {
                return;
            }

            if (!_animator.enabled)
            {
                _animator.enabled = true;
            }

            _animator.Play(stateName, 0, 0f);
            CurrentAnimatorStateName = stateName;

            //为了正确调用[animator.GetCurrentAnimatorStateInfo(0)]，必须要两次Update
            _animator.Update(0);
            _animator.Update(0);

            //记录normalizedTime
            _currentAnimatorStateCounter = (int)_animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            //派发事件
            DispatchAnimatorEvent(UIAnimatorEventType.AnimatorStart, CurrentAnimatorStateName);
        }

        /// 设置动画条件值
        /// @name
        /// @value
        public void SetBool(string name, bool value)
        {
            _animator.SetBool(name, value);
        }

        public void SetAnimatorEnable(bool isEnable)
        {
            if (_animator)
            {
                _animator.enabled = isEnable;
                this.enabled = isEnable;
            }
        }

        /// 设置动画条件值
        public void SetInteger(string name, int value)
        {
            _animator.SetInteger(name, value);
        }

        /// 停止播放动画
        public void StopAnimator()
        {
            //_animator.StopPlayback();
        }

        /// 指定动画是否处于停止播放状态
        public bool IsAnimationStopped(string animationName)
        {
            if (string.IsNullOrEmpty(animationName))
            {
                return true;
            }

            return (!string.Equals(CurrentAnimatorStateName, animationName));
        }

        //--------------------------------------------------
        /// 派发动画相关事件
        /// @animationEventType
        //--------------------------------------------------
        private void DispatchAnimatorEvent(UIAnimatorEventType animatorEventType, string stateName)
        {

        }

        public override void OnHide()
        {
            base.OnHide();
            _isEnableBeHide = this.enabled;
            _isEnableAnimatorBeHide = _animator.enabled;
            this.enabled = false;
            this._animator.enabled = false;
        }

        public override void OnAppear()
        {
            base.OnAppear();
            this.enabled = _isEnableBeHide;
            this._animator.enabled = _isEnableAnimatorBeHide;
        }

    };
};