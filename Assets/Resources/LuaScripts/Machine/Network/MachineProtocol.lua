--[[********************************************************************
*      作者： jordenwu
*      时间： 08/03/17 09:52:20
*      描述： 网络协议名称定义
*********************************************************************--]]
--
local Cmd={
    --心跳包
    cs_req_heart_beat 		=	1000,	
    cs_rsp_heart_beat		=   1001,
    --通用错误（如服务器内部错误）
    cs_notify_common_err    =   1002,
    --登陆街机
    cs_req_m_login          =   1010,
    cs_rsp_m_login          =   1011,
    -- 通知街机数据更新
    cs_notify_update_machine    = 1029,
    --通知街机删除
    cs_notify_delete_machine    = 1030,
    --通知街机重启
    cs_notify_restart_machine   = 1031
}

--
local Protocol = {
	
    Req = {
    	[Cmd.cs_req_heart_beat]			= "cs_req_heart_beat",
        [Cmd.cs_req_m_login]            = "cs_req_m_login",
    },

    -- receive 必须一一配置，指定对应的解析包格式
    Rsp = {
        [Cmd.cs_rsp_heart_beat]          = "cs_rsp_heart_beat",
        [Cmd.cs_notify_common_err]       = "cs_notify_common_err",
        [Cmd.cs_rsp_m_login]             = "cs_rsp_m_login",
        [Cmd.cs_notify_update_machine]   = "cs_notify_update_machine",
        [Cmd.cs_notify_delete_machine]   = "cs_notify_delete_machine",
        [Cmd.cs_notify_restart_machine]  = "cs_notify_restart_machine"
    },
}

--网络结果
local ENResult=
{
    --OK 
    EN_Result_OK            = 1,
    --解包出错
    EN_Result_Invalid_Msg   = 2,  
    --登录鉴权失败  
    EN_Result_Auth_Failed   = 3;   
    --未登录 
    EN_Result_Not_Login     = 4;  
    --系统错误  
    EN_Result_Sys_Err       = 5;  
    --服务器已满  
    EN_Result_Server_Full   = 6;    
}
--
MachineNet=MachineNet or {}
--全局
MachineNet.Cmd=Cmd
MachineNet.Protocol=Protocol
MachineNet.Result=ENResult
