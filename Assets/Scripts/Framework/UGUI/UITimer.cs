using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JW.Framework.UGUI
{
    public class UITimer : UIComponent
    {
        public System.Action onTimer;

        public float countDown = 0;

        private bool _isWork = false;
        private float _worked = 0.0f;

        //关闭
        public override void OnClose()
        {
            _worked = 0.0f;
            _isWork = false;
        }

        //隐藏
        public override void OnHide()
        {
            _worked = 0.0f;
            _isWork = false;
        }

        //重新显示
        public override void OnAppear()
        {
            _isWork = true;
        }

        private void Update()
        {
            if (_isWork)
            {
                _worked += Time.deltaTime;
                if (_worked > countDown)
                {
                    _isWork = false;
                    if (onTimer != null)
                    {
                        onTimer();
                    }
                }
            }
        }

    }
}
