/********************************************************************
	created:	2018-06-14
	filename: 	UIMsgBox
	author:		jordenwu
	
	purpose:	消息弹窗
*********************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JW.Framework.UGUI;
using JW.Framework.MVC;
using UnityEngine.UI;

namespace JW.Framework.UGUI
{
    public class UIMsgBox : UIFormClass
    {
        private const int _uiTitleTextIndex = 0;
        private const int _uiContentTextIndex = 1;
        private const int _uiOkButtonIndex = 2;
        private const int _uiCancelButtonIndex = 3;
        private const int _uiOkTextIndex = 4;
        private const int _uiCancelTextIndex = 5;

        private Text _uiTitleText;
        private Text _uiContentText;
        private Button _uiOkButton;
        private Button _uiCancelButton;
        private Text _uiOkText;
        private Text _uiCancelText;


        private UIMsgBoxDelegate _handler;

        public override string GetPath()
        {
            return "Fixed/UICommon/UIMsgBox";
        }

        /// <summary>
        /// 资源加载后回调
        /// </summary>
        protected override void OnResourceLoaded()
        {
            _uiTitleText = GetComponent(_uiTitleTextIndex) as Text;
            _uiContentText = GetComponent(_uiContentTextIndex) as Text;
            _uiOkButton = GetComponent(_uiOkButtonIndex) as Button;
            _uiCancelButton = GetComponent(_uiCancelButtonIndex) as Button;
            _uiOkText = GetComponent(_uiOkTextIndex) as Text;
            _uiCancelText = GetComponent(_uiCancelTextIndex) as Text;

        }

        /// <summary>
        /// 资源卸载后回调
        /// </summary>
        protected override void OnResourceUnLoaded()
        {
            _uiTitleText = null;
            _uiContentText = null;
            _uiOkButton = null;
            _uiCancelButton = null;
            _uiOkText = null;
            _uiCancelText = null;

        }

        /// <summary>
        /// 逻辑初始化
        /// </summary>
        /// <param name="parameter">初始化参数</param>
        protected override void OnInitialize(object parameter)
        {
            if (_uiOkButton != null)
            {
                _uiOkButton.onClick.AddListener(OnOkClick);
            }
            if (_uiCancelButton != null)
            {
                _uiCancelButton.onClick.AddListener(OnCancelClick);
            }
        }

        /// <summary>
        /// 逻辑反初始化
        /// </summary>
        protected override void OnUninitialize()
        {
            if (_uiOkButton != null)
            {
                _uiOkButton.onClick.RemoveListener(OnOkClick);
            }
            if (_uiCancelButton != null)
            {
                _uiCancelButton.onClick.RemoveListener(OnCancelClick);
            }
            _handler = null;
        }

        public void InitShow(UIMsgBoxStyle style, UIMsgBoxDelegate handler, string title, string content, string ok, string cancel)
        {
            _handler = handler;
            if (style == UIMsgBoxStyle.JustOk)
            {
                _uiOkButton.gameObject.SetActive(true);
                _uiCancelButton.gameObject.SetActive(false);
            }
            else
            {
                _uiOkButton.gameObject.SetActive(true);
                _uiCancelButton.gameObject.SetActive(true);
            }
            //
            _uiTitleText.text = title;
            _uiContentText.text = content;
            _uiOkText.text = ok;
            _uiCancelText.text = cancel;
        }

        private void OnOkClick()
        {
            if (_handler != null)
            {
                _handler(UIMsgBoxResult.Ok);
            }
        }

        private void OnCancelClick()
        {
            if (_handler != null)
            {
                _handler(UIMsgBoxResult.Cancel);
            }
        }
    }
}

