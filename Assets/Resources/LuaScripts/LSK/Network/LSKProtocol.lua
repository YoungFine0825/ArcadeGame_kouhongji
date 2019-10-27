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
    --登陆游戏
    cs_req_m_login_game     =   1012,
    cs_rsp_m_login_game     =   1013,
    --通知玩家登录
    cs_notify_m_player_login    = 1014,
    --通知玩家投币
    cs_notify_m_player_buy_in   = 1015,
    --开始游戏
    cs_req_start_lipstick       = 1016,
    cs_rsp_start_lipstick       = 1017,
    --结束游戏
    cs_req_end_lipstick         = 1018,
    cs_rsp_end_lipstick         = 1019,
    --玩家登出
    cs_req_player_logout        = 1020,
    cs_rsp_player_logout        = 1021,
    cs_notify_player_logout     = 1022,
    --续命
    cs_req_retry_lipstick       = 1023,
    cs_rsp_retry_lipstick       = 1024,
    --抽奖
    cs_req_lottery              = 1025,
    cs_rsp_lottery              = 1026,
    cs_req_lottery_end          = 1032,
    cs_rsp_lottery_end          = 1033,
    --保底购买
    cs_req_buy_prize            = 1027,
    cs_rsp_buy_prize            = 1028,

    --积分变动
    cs_notify_point             = 2000,
}

--
local Protocol = {
	
    Req = {
    	[Cmd.cs_req_heart_beat]			= "cs_req_heart_beat",
        [Cmd.cs_req_m_login_game]       = "cs_req_m_login_game",
        [Cmd.cs_req_start_lipstick]     = "cs_req_start_lipstick",
        [Cmd.cs_req_end_lipstick]       = "cs_req_end_lipstick",--结束
        [Cmd.cs_req_player_logout]      = "cs_req_player_logout",--请求退出
        [Cmd.cs_req_end_lipstick]       = "cs_req_end_lipstick",--请求退出
        [Cmd.cs_req_retry_lipstick]     = "cs_req_retry_lipstick",
        [Cmd.cs_req_lottery]            = "cs_req_lottery",
        [Cmd.cs_req_buy_prize]          = "cs_req_buy_prize",
        [Cmd.cs_req_lottery_end]        = "cs_req_lottery_end",
    },

    -- receive 必须一一配置，指定对应的解析包格式
    Rsp = {
        [Cmd.cs_rsp_heart_beat]          = "cs_rsp_heart_beat",
        [Cmd.cs_notify_common_err]       = "cs_notify_common_err",
        [Cmd.cs_rsp_m_login_game]        = "cs_rsp_m_login_game",
        [Cmd.cs_rsp_start_lipstick]      = "cs_rsp_start_lipstick",
        [Cmd.cs_rsp_player_logout]       = "cs_rsp_player_logout",

        [Cmd.cs_rsp_end_lipstick]        = "cs_rsp_end_lipstick",
        [Cmd.cs_notify_m_player_login]   = "cs_notify_m_player_login",
        [Cmd.cs_notify_m_player_buy_in]  = "cs_notify_m_player_buy_in",
        [Cmd.cs_notify_player_logout]    = "cs_notify_player_logout",--通知退出(超时等)
        [Cmd.cs_rsp_retry_lipstick]      = "cs_rsp_retry_lipstick",
        [Cmd.cs_rsp_lottery]             = "cs_rsp_lottery",
        [Cmd.cs_rsp_buy_prize]           = "cs_rsp_buy_prize",
        [Cmd.cs_rsp_lottery_end]         = "cs_rsp_lottery_end",
        [Cmd.cs_notify_point]            = 'cs_notify_point',
    },
}
--
LSKNet=LSKNet or {}
--全局
LSKNet.Cmd=Cmd
LSKNet.Protocol=Protocol