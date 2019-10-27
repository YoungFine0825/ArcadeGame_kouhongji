/********************************************************************
	created:	2018-05-30   
	filename: 	LaunchState
	author:		jordenwu
	
	purpose:	游戏启动状态
*********************************************************************/
using JW.Common;
using JW.Framework.State;
using JW.Framework.MVC;
using UnityEngine;

public class RegisterState : IState
{

    public void InitializeState()
    {

    }

    public string Name()
    {
        return "Register";
    }

    public void OnStateEnter(object usrData = null)
    {
        JW.Common.Log.LogD("Enter Register Machine State");
        //判断是否已经注册
        string machineId = PlayerPrefs.GetString("MachineID", string.Empty);
        if (string.IsNullOrEmpty(machineId))
        {
            UIStateService.GetInstance().ChangeState("UIRegister");
        }
        else
        {
            JW.Common.Log.LogD("<color=yellow>MachineID : {0}</color>", machineId);
            //到资源自动更新
            StateService.GetInstance().ChangeState("Update");
        }
    }

    public void OnStateLeave()
    {
        JW.Common.Log.LogD("Leave Register Machine GameState");
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

}
