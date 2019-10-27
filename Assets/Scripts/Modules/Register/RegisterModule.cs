/********************************************************************
	created:	2018-11-1
	author:		jordenwu
	
	purpose:	机器注册模块
*********************************************************************/
using UnityEngine;
using JW.Framework.MVC;
using JW.Framework.Http;
using JW.Framework.State;
using JW.Framework.ArcadeInput;
using JW.Common;
using JW.Framework.Schedule;
using System.Collections.Generic;
using JW.Framework.UGUI;

public class RegisterModule : ModuleBase, IScheduleHandler
{
    private string _machineId = string.Empty;
    //倒计时时长
    private int _rebootDuration = 10;
    //重启倒计时
    private int _rebotCntDown = 0;
    //
    private bool _isInRegistering = false;//是否在注册中
    //
    protected override void OnInitializeModule()
    {

    }

    protected override void OnUninitializeModule()
    {

    }

    protected override void OnAction(string id, object param)
    {
        if (id.Equals("DoRegisterMachine"))
        {
            if (_isInRegistering)
            {
                return;
            }
            //
            this.UpdateUI("ShowTip", "");
            _isInRegistering = true;
            //发送注册机器码HTTP请求
            _machineId = "M" + (string)param;
            string password = ArcadeInputService.GetInstance().GetDeviceKey();
            if (string.IsNullOrEmpty(password))
            {
                Log.LogE("Get Device Key  Nil Error");
                UpdateUI("ShowTip", "获取设备密钥异常，即将重启设备!");
                //开始重启倒计时
                _rebotCntDown = _rebootDuration;
                this.AddTimer(1000, true);
                return;
            }
            else
            {
                string urlFormat = string.Empty;
#if JW_DEBUG
                urlFormat = "http://ghtest.scbczx.com:60/machineUse?machineId={0}&password={1}";
#else
                urlFormat = "http://ghtest.scbczx.com:60/machineUse?machineId={0}&password={1}";
#endif
                string fullUrl = string.Format(urlFormat,_machineId, password);
                Log.LogD("Register Machine :" + fullUrl);
                //
                UICommonService.GetInstance().ShowWaiting("Register", true, "正在注册...");
                //
                HttpService.GetInstance().AsyncGetText(fullUrl, OnMachineRegisterBack);
            }
            return;
        }
    }

    //注册回来
    private void OnMachineRegisterBack(string args)
    {
        UICommonService.GetInstance().ShowWaiting("Register", false);
        Log.LogD("Register Machine Back:" + args);
        if (string.IsNullOrEmpty(args) || string.Equals(args, "error"))
        {
            //取消锁定
            _isInRegistering = false;
            this.UpdateUI("ShowTip", "注册失败！ 请重试!");
            return;
        }
        //是否真正成功
        if (args.Equals("ok"))
        {
            //注册成功，保存机器码
            PlayerPrefs.SetString("MachineID", _machineId);
            PlayerPrefs.Save();

            Log.LogD("<color=green>Register MachineId [{0}] Successed!</color>", _machineId);
            UpdateUI("ShowTip", "注册成功! 即将重启!");
#if UNITY_EDITOR
            //取消锁定
            _isInRegistering = false;
            //到资源自动更新
            StateService.GetInstance().ChangeState("Update");
#else
            //开始重启倒计时
            _rebotCntDown = _rebootDuration;
            this.AddTimer(1000, true);
#endif
        }
        else
        {
            //错误
            Dictionary<string, object> dd = JW.Common.Json.Deserialize(args) as Dictionary<string, object>;
            if (dd != null)
            {
                object vv;
                if (dd.TryGetValue("errCode", out vv))
                {
                    string stateStr = vv as string;
                    this.UpdateUI("ShowTip", "注册失败! ErrorCode:" + stateStr);
                }
                else
                {
                    this.UpdateUI("ShowTip", "注册失败! 请重试!");
                }
            }
            else
            {
                this.UpdateUI("ShowTip", "注册失败! 请重试!");
            }
            //取消锁定
            _isInRegistering = false;
        }
    }

    public void OnScheduleHandle(ScheduleType type, uint id)
    {
        _rebotCntDown = _rebotCntDown - 1;
        if (_rebotCntDown < 0)
        {
            //计时结束,移除计时器
            this.RemoveTimer();

            Application.OpenURL(Application.streamingAssetsPath + "/Reboot.exe");
            Application.Quit();
        }
        else
        {
            if (_rebotCntDown <= 5)
            {
                UpdateUI("ShowTip", "重启倒计时:" + (_rebotCntDown).ToString());
            }
        }
    }
}
