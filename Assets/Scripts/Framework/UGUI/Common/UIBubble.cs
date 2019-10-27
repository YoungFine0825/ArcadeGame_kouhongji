/********************************************************************
	created:	2018-06-14
	filename: 	UIBubble
	author:		jordenwu
	
	purpose:	提示窗口
*********************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JW.Framework.UGUI;
using JW.Framework.MVC;
using UnityEngine.UI;

namespace JW.Framework.UGUI
{
    public class UIBubble : UIFormClass
    {
        private const int _uiInfoTextIndex = 0;
        private const int _uiCloseTimerTimerIndex = 1;

        private Text _uiInfoText;
        private UITimer _uiCloseTimerTimer;

        private System.Action<UIBubble> _closeHandler;

        public override string GetPath()
        {
            return "Fixed/UICommon/UIBubble";
        }

        /// <summary>
        /// 资源加载后回调
        /// </summary>
        protected override void OnResourceLoaded()
        {
            _uiInfoText = GetComponent(_uiInfoTextIndex) as Text;
            _uiCloseTimerTimer = GetComponent(_uiCloseTimerTimerIndex) as UITimer;

        }

        /// <summary>
        /// 资源卸载后回调
        /// </summary>
        protected override void OnResourceUnLoaded()
        {
            _uiInfoText = null;
            _uiCloseTimerTimer = null;
        }

        /// <summary>
        /// 逻辑初始化
        /// </summary>
        /// <param name="parameter">初始化参数</param>
        protected override void OnInitialize(object parameter)
        {
            if (_uiCloseTimerTimer != null)
            {
                _uiCloseTimerTimer.onTimer = delegate ()
                {
                    if (_closeHandler != null)
                    {
                        _closeHandler(this);
                    }
                    _closeHandler = null;
                };
            }
        }

        /// <summary>
        /// 逻辑反初始化
        /// </summary>
        protected override void OnUninitialize()
        {
            if (_uiCloseTimerTimer != null)
            {
                _uiCloseTimerTimer.onTimer = null;
            }
        }


        public void InitShow(string bubble, System.Action<UIBubble> onTimer)
        {
            if (_uiInfoText != null)
            {
                _uiInfoText.text = bubble;
            }
            _closeHandler = onTimer;
        }
    }

}
