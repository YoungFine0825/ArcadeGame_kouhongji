/********************************************************************
	created:	2018-11-1
	author:		jordenwu
	
	purpose:	机器注册中间件
*********************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JW.Framework.MVC;
using JW.Framework.Http;
public class UIRegisterMediator : UIMediator
{
    protected override string[] GetBelongsUIStateName()
    {
        return new string[] { "UIRegister" };
    }

    private UIRegisterForm _registerForm;    

    /// <summary>
    /// 当初始化时调用
    /// </summary>
    protected override void OnInitialize()
    {

    }

    /// <summary>
    /// 当销毁时调用调用
    /// </summary>
    protected override void OnUninitialize()
    {
        if (_registerForm != null)
        {
            DisposeFormClass<UIRegisterForm>(ref _registerForm);
            _registerForm = null;
        }
    }

    /// <summary>
    /// 当UIState切入时调用
    /// </summary>
    /// <param name="changeType">切换类型</param>
    /// <param name="oldStateName">老状态名，可能为空和null</param>
    /// <param name="newStateName">新状态名，可能为空和null</param>
    /// <param name="stateParam">状态参数</param>
    protected override void OnUIStateIn(int changeType, string oldStateName, string newStateName, object stateParam)
    {
        if (string.Equals(newStateName, "UIRegister"))
        {
            if (_registerForm == null)
            {
                _registerForm = CreateFormClass<UIRegisterForm>(false);
            }
        }
    }

    /// <summary>
    /// 当UIState切出时调用
    /// <param name="changeType">切换类型</param>
    /// <param name="oldStateName">老状态名，可能为空和null</param>
    /// <param name="newStateName">新状态名，可能为空和null</param>
    /// </summary>
    protected override void OnUIStateOut(int changeType, string oldStateName, string newStateName)
    {
        if (oldStateName.Equals("UIRegister"))
        {
            if (_registerForm != null)
            {
                DisposeFormClass<UIRegisterForm>(ref _registerForm);
                _registerForm = null;
            }
        }
    }

    /// <summary>
    /// 状态刷新时调用
    /// <param name="changeType">切换类型</param>
    /// <param name="oldStateName">老状态名，可能为空和null</param>
    /// <param name="newStateName">新状态名，可能为空和null</param>
    /// <param name="stateParam">状态参数</param>
    /// </summary>
    protected override void OnUIStateStay(int changeType, string oldStateName, string newStateName, object stateParam)
    {

    }

    /// <summary>
    /// 更新UI
    /// </summary>
    /// <param name="id">更新ID标识</param>
    /// <param name="param">更新参数</param>
    /// <returns>是否阻断向下传递刷新</returns>
    protected override bool OnUpdateUI(string id, object param)
    {
        return false;
    }

    /// <summary>
    /// 操作
    /// </summary>
    /// <param name="id">操作ID标识</param>
    /// <param name="param">操作参数</param>
    /// <returns>是否阻断向上传递操作</returns>
    protected override bool OnAction(string id, object param)
    {
        return false;
    }
}
