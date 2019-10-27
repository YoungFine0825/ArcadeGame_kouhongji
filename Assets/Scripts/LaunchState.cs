/********************************************************************
	created:	2018-05-30   
	filename: 	LaunchState
	author:		jordenwu
	
	purpose:	游戏启动状态
*********************************************************************/
using JW.Common;
using JW.Framework.Schedule;
using JW.Framework.State;
using JW.Framework.MVC;
using JW.Framework.Audio;

public class LaunchState : IState,IScheduleHandler {

    public void InitializeState()
    {
        
    }

    public string Name()
    {
        return "Launch";
    }

    public void OnStateEnter(object usrData = null)
    {
        Log.LogD("Enter Launch Game State");
        UIStateService.GetInstance().ChangeState("UILaunch");
        this.AddTimer(2000, false);

    }

    public void OnStateLeave()
    {
        JW.Common.Log.LogD("Leave Launch GameState");
    }

    public void OnStateOverride()
    {
        
    }

    public void OnStateResume()
    {
        
    }

    public void UninitializeState()
    {
        
    }

    public void OnScheduleHandle(ScheduleType type, uint id)
    {
        StateService.GetInstance().ChangeState("Register");

    }

}
