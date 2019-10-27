/********************************************************************
	created:	2018-06-05   
	filename: 	EventDeclare
	author:		jordenwu
	
	purpose:	模块间事件参数定义
*********************************************************************/
using JW.Framework.Event;
using UnityEngine;

/// <summary>
/// 事件ID 定义
/// </summary>
public enum EventId : uint
{
    /// 内部保留事件，
    TestEventId = 0,
    //程序前后台切换
    ApplicationPause = 1,
    //更新状态改变事件
    UpdateStateChange = 2,
    /// 逻辑事件
    Count,
}

/// <summary>
/// 事件参数定义类
/// </summary>
public class EventDeclare
{
    //Test
    public struct TestEventArg : EventArg
    {
        public int Test;
    }

    /// <summary>
    /// 应用程序暂停
    /// </summary>
    public struct ApplicationPauseArg : EventArg
    {
        public bool IsPause;
    }

    /// <summary>
    /// 更新状态事件参数
    /// </summary>
    public struct UpdateStateChangeEventArg : EventArg
    {
        public string StateInfo;
        public float Progress;
    }

}

