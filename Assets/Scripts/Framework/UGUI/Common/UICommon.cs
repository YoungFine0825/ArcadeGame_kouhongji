/********************************************************************
	created:	2018-06-14
	filename: 	UICommon
	author:		jordenwu
	
	purpose:	UI 公共窗口管理
*********************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JW.Framework.UGUI;
using JW.Framework.MVC;
using JW.Common;

namespace JW.Framework.UGUI
{
    /// <summary>
    /// 消息弹窗样式定义
    /// </summary>
    public enum UIMsgBoxStyle
    {
        JustOk,
        OkAndCancel,
    }

    /// <summary>
    /// 消息弹窗点击结果
    /// </summary>
    public enum UIMsgBoxResult
    {
        Ok,
        Cancel,
    }

    /// <summary>
    /// 消息弹窗点击委托定义 
    /// </summary>
    /// <param name="result">点击结果</param>
    public delegate void UIMsgBoxDelegate(UIMsgBoxResult result);

    public delegate void UIMsgBoxLuaDelegate(int result);

    /// <summary>
    /// UI公共管理类
    /// </summary>
    public class UICommonService : Singleton<UICommonService>
    {
        //MsgBox
        private UIMsgBox _msgBox;
        private string _msgBoxDefaultOk;
        private string _msgBoxDefaultCancel;
        private string _msgBoxDefaultTitle;
        private UIMsgBoxDelegate _msgBoxHandler;
        private UIMsgBoxLuaDelegate _msgBoxLuaHandler;
        //Waiting
        private UIWaiting _waiting;
        private readonly JWObjList<string> _waitingKeyList = new JWObjList<string>();

        public override bool Initialize()
        {
            _msgBoxDefaultOk = "确定";
            _msgBoxDefaultCancel = "取消";
            _msgBoxDefaultTitle = "消息";
            return true;
        }

        public override void Uninitialize()
        {
            if (_msgBox != null)
            {
                UIFormHelper.DisposeFormClass<UIMsgBox>(ref _msgBox);
                _msgBox = null;
            }
            if (null != _waiting)
            {
                UIFormHelper.DisposeFormClass<UIWaiting>(ref _waiting);
                _waiting = null;
            }

            _msgBoxDefaultCancel = null;
            _msgBoxDefaultOk = null;
            _msgBoxDefaultTitle = null;
            _msgBoxHandler = null;
            _msgBoxLuaHandler = null;
        }

        /// <summary>
        /// 显示消息弹窗
        /// </summary>
        /// <param name="style">样式</param>
        /// <param name="handler">点击处理</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="ok">OK按钮文本</param>
        /// <param name="cancel">取消按钮文本</param>
        public void ShowMsgBox(UIMsgBoxStyle style, UIMsgBoxDelegate handler, string content, string title = "", string ok = "", string cancel = "")
        {
            if (string.IsNullOrEmpty(content))
            {
                JW.Common.Log.LogE("ShowMsgBox Error Content");
                return;
            }
            if (_msgBox != null)
            {
                UIFormHelper.DisposeFormClass<UIMsgBox>(ref _msgBox);
                _msgBox = null;
            }
            _msgBox = UIFormHelper.CreateResidentFormClass<UIMsgBox>();
            if (_msgBox != null)
            {
                if (string.IsNullOrEmpty(title))
                {
                    title = _msgBoxDefaultTitle;
                }
                if (string.IsNullOrEmpty(ok))
                {
                    ok = _msgBoxDefaultOk;
                }
                if (string.IsNullOrEmpty(cancel))
                {
                    cancel = _msgBoxDefaultCancel;
                }
                _msgBoxHandler = handler;
                _msgBox.InitShow(style, OnMsgBoxResult, title, content, ok, cancel);
            }
        }

        public void ShowMsgBoxLua(int style, UIMsgBoxLuaDelegate handler, string content, string title = "", string ok = "", string cancel = "")
        {
            if (string.IsNullOrEmpty(content))
            {
                JW.Common.Log.LogE("ShowMsgBox Error Content");
                return;
            }
            if (_msgBox != null)
            {
                UIFormHelper.DisposeFormClass<UIMsgBox>(ref _msgBox);
                _msgBox = null;
            }
            _msgBox = UIFormHelper.CreateResidentFormClass<UIMsgBox>();
            if (_msgBox != null)
            {
                if (string.IsNullOrEmpty(title))
                {
                    title = _msgBoxDefaultTitle;
                }
                if (string.IsNullOrEmpty(ok))
                {
                    ok = _msgBoxDefaultOk;
                }
                if (string.IsNullOrEmpty(cancel))
                {
                    cancel = _msgBoxDefaultCancel;
                }
                _msgBoxLuaHandler = handler;
                _msgBox.InitShow((UIMsgBoxStyle)style, OnMsgBoxResultLua, title, content, ok, cancel);
            }
        }

        public void CloseMsgBox()
        {
            if (_msgBox != null)
            {
                UIFormHelper.DisposeFormClass<UIMsgBox>(ref _msgBox);
                _msgBox = null;
            }
            _msgBoxHandler = null;
            _msgBoxLuaHandler = null;
        }

        private void OnMsgBoxResult(UIMsgBoxResult result)
        {
            if (_msgBox != null)
            {
                UIFormHelper.DisposeFormClass<UIMsgBox>(ref _msgBox);
                _msgBox = null;
            }
            //
            if (_msgBoxHandler != null)
            {
                _msgBoxHandler(result);
            }
            _msgBoxHandler = null;
        }

        private void OnMsgBoxResultLua(UIMsgBoxResult result)
        {
            if (_msgBox != null)
            {
                UIFormHelper.DisposeFormClass<UIMsgBox>(ref _msgBox);
                _msgBox = null;
            }
            //
            if (_msgBoxLuaHandler != null)
            {
                _msgBoxLuaHandler((int)result);
            }
            _msgBoxLuaHandler = null;
        }


        /// <summary>
        /// 显示或者关闭菊花 
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="isShow">是否显示</param>
        public void ShowWaiting(string key, bool isShow,string tip="")
        {
            if (isShow)
            {
                if (_waitingKeyList.Contains(key))
                {
                    //JW.Common.Log.LogE("ShowWaiting Logic Error Repeat Key:" + key);
                    if (_waiting != null)
                    {
                        _waiting.ShowTip(tip);
                    }
                    return;
                }
                _waitingKeyList.Add(key);
                if (_waiting == null)
                {
                    _waiting = UIFormHelper.CreateResidentFormClass<UIWaiting>();
                    _waiting.ShowTip(tip);
                }
                else
                {
                    _waiting.ActiveForm(true);
                    _waiting.ShowTip(tip);
                }
            }
            else
            {
                int firstIndex = _waitingKeyList.IndexOf(key);
                if (firstIndex >= 0 && firstIndex < _waitingKeyList.Count)
                {
                    _waitingKeyList.RemoveAt(firstIndex);
                }
                if (_waitingKeyList.Count == 0)
                {
                    if (null != _waiting)
                    {
                        _waiting.ActiveForm(false);
                        //UIFormHelper.DisposeFormClass<UIWaiting>(ref _waiting);
                        //_waiting = null;
                    }
                }
            }

        }

        /// <summary>
        /// 清除菊花
        /// </summary>
        public void CleanWaiting()
        {
            _waitingKeyList.Clear();
            if (null != _waiting)
            {
                UIFormHelper.DisposeFormClass<UIWaiting>(ref _waiting);
                _waiting = null;
            }
        }

        /// <summary>
        /// 显示提示
        /// </summary>
        /// <param name="info"></param>
        public void ShowBubble(string info)
        {
            UIBubble bb = UIFormHelper.CreateFormClass<UIBubble>(false, null);
            if (bb != null)
            {
                bb.InitShow(info, OnBubbleTiming);
            }
        }

        private void OnBubbleTiming(UIBubble bb)
        {
            if (bb != null)
            {
                UIFormHelper.DisposeFormClass<UIBubble>(ref bb);
            }
        }

    }
}
