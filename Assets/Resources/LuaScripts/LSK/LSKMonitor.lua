local LSKMonitorClass = DeclareClass("LSKMonitorClass")

LSKNetResult = 
{
    OK = 1,
    InvalidMsg = 2,
    AuthFailed = 3,
    NotLogin = 4,
    SystemError = 5,
    ServerFull = 6,
    MachineStateDontMatch = 7,
    NoPlayer = 8,
    InvalidPrizeId = 9,
    GoldNotEnough = 10,
}

LSKNetResultDesc = 
{
    [LSKNetResult.OK] = 'Okay',
    [LSKNetResult.InvalidMsg] = '解包出错',
    [LSKNetResult.AuthFailed] = '登录鉴权失败',
    [LSKNetResult.NotLogin] = '未登录',
    [LSKNetResult.SystemError] = '系统错误',
    [LSKNetResult.ServerFull] = '服务器已满',
    [LSKNetResult.MachineStateDontMatch] = '请求与街机状态不匹配',
    [LSKNetResult.NoPlayer] = '街机上没有玩家',
    [LSKNetResult.InvalidPrizeId] = '无效的礼品ID',
    [LSKNetResult.GoldNotEnough] = '游戏币不足',
}

function LSKMonitorClass:ctor()

end

function LSKMonitorClass:Initialize()
    self:AddNetMsg()
end

function LSKMonitorClass:Uninitialize()
    self:RemoveNetMsg()
end

function LSKMonitorClass:AddNetMsg()
    LSKNetwork:AddCommandHandler(LSKNet.Cmd.cs_notify_m_player_login,self,self.OnRecvNetMsg)
    LSKNetwork:AddCommandHandler(LSKNet.Cmd.cs_notify_m_player_buy_in,self,self.OnRecvNetMsg)
    LSKNetwork:AddCommandHandler(LSKNet.Cmd.cs_notify_player_logout,self,self.OnRecvNetMsg)
    LSKNetwork:AddCommandHandler(LSKNet.Cmd.cs_rsp_player_logout,self,self.OnRecvNetMsg)
    LSKNetwork:AddCommandHandler(LSKNet.Cmd.cs_rsp_start_lipstick,self,self.OnRecvNetMsg)
    LSKNetwork:AddCommandHandler(LSKNet.Cmd.cs_rsp_end_lipstick,self,self.OnRecvNetMsg)
    LSKNetwork:AddCommandHandler(LSKNet.Cmd.cs_rsp_retry_lipstick,self,self.OnRecvNetMsg)
    LSKNetwork:AddCommandHandler(LSKNet.Cmd.cs_rsp_lottery,self,self.OnRecvNetMsg)
    LSKNetwork:AddCommandHandler(LSKNet.Cmd.cs_rsp_buy_prize,self,self.OnRecvNetMsg)
    LSKNetwork:AddCommandHandler(LSKNet.Cmd.cs_rsp_lottery_end,self,self.OnRecvNetMsg)
    LSKNetwork:AddCommandHandler(LSKNet.Cmd.cs_notify_point,self,self.OnRecvNetMsg)
end

function LSKMonitorClass:RemoveNetMsg()
    LSKNetwork:RemoveCommandHandlerByObj(self)
end

function LSKMonitorClass:OnRecvNetMsg(data, result, cmd)
    LSKBoot:OnRelayNetMsg(data, result, cmd)
end
