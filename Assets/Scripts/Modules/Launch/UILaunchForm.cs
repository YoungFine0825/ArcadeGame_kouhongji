/********************************************************************
	created:	2018-05-31   
	filename: 	UILaunchForm
	author:		jordenwu
	
	purpose:	游戏启动窗口类
*********************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JW.Framework.MVC;
using System;
using JW.Framework.UGUI;

public class UILaunchForm : UIFormClass
{
    public override string GetPath()
    {
        return "Fixed/Launch/UILaunchForm";
    }


    /// <summary>
    /// 资源加载后回调
    /// </summary>
    protected override void OnResourceLoaded()
    {
       

    }

    /// <summary>
    /// 资源卸载后回调
    /// </summary>
    protected override void OnResourceUnLoaded()
    {
       

    }

    /// <summary>
    /// 逻辑初始化
    /// </summary>
    /// <param name="parameter">初始化参数</param>
    protected override void OnInitialize(object parameter)
    {
        
    }

    /// <summary>
    /// 逻辑反初始化
    /// </summary>
    protected override void OnUninitialize()
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
        return true;
    }
}
