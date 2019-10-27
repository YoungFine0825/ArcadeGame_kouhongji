/********************************************************************
	created:	2018-06-14
	filename: 	UIWaiting
	author:		jordenwu
	
	purpose:	菊花窗口
*********************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JW.Framework.UGUI;
using JW.Framework.MVC;
using UnityEngine.UI;

namespace JW.Framework.UGUI
{
    public class UIWaiting : UIFormClass
    {
        private const int _uiTipTextIndex = 0;

        private Text _uiTipText;

        public override string GetPath()
        {
            return "Fixed/UICommon/UIWaiting";
        }

        /// <summary>
        /// 资源加载后回调
        /// </summary>
        protected override void OnResourceLoaded()
        {
            _uiTipText = GetComponent(_uiTipTextIndex) as Text;

        }

        /// <summary>
        /// 资源卸载后回调
        /// </summary>
        protected override void OnResourceUnLoaded()
        {
            _uiTipText = null;

        }

        /// <summary>
        /// 逻辑初始化
        /// </summary>
        /// <param name="parameter">初始化参数</param>
        protected override void OnInitialize(object parameter)
        {
            if (_uiTipText)
            {
                _uiTipText.gameObject.SetActive(false);
                _uiTipText.text = "";
            }
        }

        /// <summary>
        /// 逻辑反初始化
        /// </summary>
        protected override void OnUninitialize()
        {

        }

        public void ShowTip(string tip)
        {
            if (string.IsNullOrEmpty(tip))
            {
                if (_uiTipText)
                {
                    _uiTipText.gameObject.SetActive(false);
                }
            }
            else
            {
                if (_uiTipText)
                {
                    _uiTipText.gameObject.SetActive(true);
                    _uiTipText.text = tip;
                }
            }
        }
    }
}
