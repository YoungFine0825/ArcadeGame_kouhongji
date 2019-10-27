/********************************************************************
	created:	2018-06-05   
	filename: 	UpdateModule
	author:		jordenwu
	
	purpose:	游戏更新管理模块
*********************************************************************/
using JW.Framework.MVC;
using JW.Framework.Event;

public class UpdateModule : ModuleBase {

    protected override void OnInitializeModule()
    {
        //这里定义本模块监听的网络消息

        //这里定义本模块监听的事件消息
        EventService.GetInstance().AddEventHandler((uint)EventId.UpdateStateChange, this, "OnUpdateStateChange");
    }

    protected override void OnUninitializeModule()
    {
        //反监听
        EventService.GetInstance().RemoveEventHandler(this);
    }


    protected void OnUpdateStateChange(EventArg arg)
    {
        EventDeclare.UpdateStateChangeEventArg realArg = (EventDeclare.UpdateStateChangeEventArg)arg;
        UpdateUI("OnUpdateStateInfo", realArg);
    }


    /// <summary>
    /// 游戏状态进入
    /// </summary>
    /// <param name="reason">跳转原因，取值Because×××</param>
    /// <param name="state">进入的状态</param>
    /// <param name="fromState">上一个状态</param>
    /// <param name="userData">自定义数据</param>
    public override void OnStateEnter(string state, string fromState, object userData)
    {


    }

    /// <summary>
    /// 游戏状态离开
    /// </summary>
    /// <param name="reason">跳转原因，取值Because×××</param>
    /// <param name="state">离开的状态</param>
    /// <param name="toState">下一个状态</param>
    /// <param name="userData">自定义数据</param>
    public override void OnStateLeave(string state, string toState, object userData)
    {

        this.UpdateUI("DoDestroyForm",null);
    }


    /// <summary>
    /// 操作 这里处理UI 界面层上来的动作
    /// </summary>
    /// <param name="id">操作ID标识</param>
    /// <param name="param">操作参数</param>
    protected override void OnAction(string id, object param)
    {



    }
}
