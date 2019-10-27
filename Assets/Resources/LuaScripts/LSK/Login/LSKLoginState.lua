--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-15
*		描述： 口红机登录逻辑状态
*********************************************************************--]]
--断线重连处理
local ReConnectSession={
    IsWorking=false,
    _retryCnt = 0,
    _maxRetryCnt = 10
}

local LSKLoginStateClass = DeclareClass("LSKLoginStateClass")
function LSKLoginStateClass:ctor()
    
    self._loginForm = false
    self._rebootDelay = 8
    self._rebootCntDown = 0

    self._tryConSvrCnt = 0
    self._tryConSvrMaxCnt = 10
    self._reloginCnt = 0
    self._reloginMaxCnt = 10

    self._tryGetCfgCnt=0
    self._tryGetCfgMaxCnt=5
    self._cachedPrizeIds=false
    self._preloader = false
end

function LSKLoginStateClass:vInitializeState()
    LSKNetwork:AddCommandHandler(LSKNet.Cmd.cs_rsp_m_login_game, self, self.OnLoginLSKSvrRsp)
    self._preloader = ClassLib.LSKPreloaderClass.new()
    self._preloader:Load()
end

function LSKLoginStateClass:vUninitializeState()
    LSKNetwork:RemoveCommandHandlerByCID(LSKNet.Cmd.cs_rsp_m_login_game)
    ScheduleService:RemoveTimer(self)
    ReConnectSession.IsWorking=false
    ScheduleService:RemoveTimer(ReConnectSession)
    if self._preloader then
        self._preloader:Unload()
        self._preloader = false
    end
end

function LSKLoginStateClass:vGetName()
    return "LSK_Login_LogicState"
end

function LSKLoginStateClass:vOnStateEnter(param, oldSt)
    LogD("Enter LSK Login State!")
    --
    if not self._loginForm then
        self._loginForm = ClassLib.LSKLoginFormClass.new()
        self._loginForm:Create(nil, nil, 0)
    end
    -- --建立连接
    self:DoConncetLSKSvr()
end

function LSKLoginStateClass:vOnStateLeave(param)
    if self._loginForm then
        self._loginForm:Destroy()
        self._loginForm = false
    end
end

------------------------------------------网络连接-----------------------------

--连接口红机服务器
function LSKLoginStateClass:DoConncetLSKSvr()
    --连接机器大厅服务器
    LSKNetwork.Delegate = self
    self._loginForm:UpdateUI("UpdateTip", "连接游戏服务器")
    LSKNetwork:OpenConnect()
end

--网络连接关闭
function LSKLoginStateClass:OnNetworkClosed()

    self._loginForm:UpdateUI("UpdateTip", "连接游戏服务器失败!等待重试")
    ScheduleService:RemoveTimer(self)
    ScheduleService:AddTimer(self,self.Delay2TryConnectSvr,5,false)

end

--连接服务器回调
function LSKLoginStateClass:OnConnectToSvrBack(isSucceed)
    
    if isSucceed then
        --请求登陆口红机机服务器
        self._tryConSvrCnt = 0
        self._loginForm:UpdateUI("UpdateTip", "连接游戏服务器成功,开始登录！")
        --
        self:DoLoginLSKSvr()
    else
        self._loginForm:UpdateUI("UpdateTip", "连接游戏服务器失败！等待重试！")
        ScheduleService:RemoveTimer(self)
        ScheduleService:AddTimer(self, self.Delay2TryConnectSvr, 5, false)
    end

end

--延迟重试连接服务器
function LSKLoginStateClass:Delay2TryConnectSvr()
    
    if self._tryConSvrCnt >= self._tryConSvrMaxCnt then
        LSKNetwork:CloseConnnect(true)
        self._loginForm:UpdateUI("UpdateTip", "连接游戏服务器失败,即将重启！")
        self._rebootCntDown = self._rebootDelay
        ScheduleService:RemoveTimer(self)
        ScheduleService:AddTimer(self, self.OnRebootTimer, 1, true)
        self._tryConSvrCnt = 0
        return
    end
    --重新连接
    self._tryConSvrCnt = self._tryConSvrCnt + 1
    self._loginForm:UpdateUI("UpdateTip", string.format("重试连接游戏服务器%d次", self._tryConSvrCnt))
    LSKNetwork.Delegate = self
    LSKNetwork:OpenConnect()
    --
end

--请求登陆口红机
function LSKLoginStateClass:DoLoginLSKSvr()
    --始终用机器那边的 Token
    local args = {
        token = MachineData.Token
    }
    LSKNetwork:SendMsg(LSKNet.Cmd.cs_req_m_login_game, args)
end

--登陆口红机回调
function LSKLoginStateClass:OnLoginLSKSvrRsp(rsp,result)
   
    if ReConnectSession.IsWorking then
        ReConnectSession:OnLoginLSKSvrRsp(rsp,result)
    else
        if result ~= MachineNet.Result.EN_Result_OK then
            --
            self._loginForm:UpdateUI("UpdateTip", "登录游戏服务器失败,等待重试！")
            LSKNetwork.Delegate = false
            LSKNetwork:CloseConnnect()
            ScheduleService:RemoveTimer(self)
            ScheduleService:AddTimer(self, self.Delay2ReLogin, 10, false)
        else
            --
            if rsp then
                --托管断线重连
                LSKNetwork.Delegate=ReConnectSession
                ScheduleService:RemoveTimer(self)
                self._reloginCnt=0
                --开始发送心跳包
                if rsp.use_heart_beat then
                    LSKNetwork:StartHearbeat(rsp.heart_beat_interval)
                end
                --初始化游戏信息
                if rsp.lipsitck_info then
    
                    if rsp.lipsitck_info.prize_ids then
                        
                        LSKData:SetLoginData(rsp.lipsitck_info.prize_ids,rsp.lipsitck_info.cost, rsp.lipsitck_info.max_retry_time)
                        --
                        self._cachedPrizeIds=rsp.lipsitck_info.prize_ids
                        LSKData:SetLotteryData(rsp.lipsitck_info.lotterys)

                        local prizeIds = self:GenPrizeIdArray(rsp.lipsitck_info.prize_ids, LSKData.LotteryList)
                        
                        self:DoPreparePrizeCfgAndRes(prizeIds)
                        HttpService:AsyncGetTextByPostUrl(MachineUrlRoot.."/getGameConfig",RapidJson.Encode({uid=MachineData.Uid,gameType=1}),function (ret)
                            LogD('Recv Game Config From HttpServer:'..ret)
                            if ret=="error" then
                                LogE('Get LSK Config Error !')
                            else
                                local rcvData = RapidJson.Decode(ret)
                                if rcvData.errCode == 1 then
                                    LSKData.KnifeList = rcvData.knivesList
                                    LSKData.IsCanTrial = rcvData.experienceSwitch
                                else
                                    LogE('Error')
                                end
                            end
                        end)
                    else
                        LogE("LSK Login  Error  No lipsitck_info.prize_ids")
                        --直接到等待
                        LSKBoot:ChangeState("LSK_Select_LogicState")
                    end
                    
                else
                    LogE("LSK Login  Error  No lipsitck_info")
                    --直接到等待
                    LSKBoot:ChangeState("LSK_Select_LogicState")
                end
               
            else
                self._loginForm:UpdateUI("UpdateTip", "登录游戏服务器失败,等待重试！")
                LSKNetwork.Delegate = false
                LSKNetwork:CloseConnnect()
                ScheduleService:RemoveTimer(self)
                ScheduleService:AddTimer(self, self.Delay2ReLogin, 10, false)
            end
        end
    end
end

-------------------------------------
-- 生成需要预先准备的礼品Id数组，并去重.
-- @param prizeList : 口红列表.
-- @param lotteryList : 抽奖礼品列表.
-------------------------------------
function LSKLoginStateClass:GenPrizeIdArray(prizeList, lotteryList)
    local prizeSet = {}
    for k, v in ipairs(prizeList) do
        prizeSet[v] = true
    end

    for k,v in ipairs(lotteryList) do
        if v.type == LSKAwardType.Prize then
            prizeSet[v.id] = true
        end
    end

    local prizeIds = {}
    for key, value in pairs(prizeSet) do
        prizeIds[#prizeIds+1] = key
    end
    return prizeIds
end

--重试登录
function LSKLoginStateClass:Delay2ReLogin()
    
    if self._reloginCnt >= self._reloginMaxCnt then
        LSKNetwork:CloseConnnect()
        self._loginForm:UpdateUI("UpdateTip", "登录游戏服务器失败,即将重启！")
        self._rebootCntDown = self._rebootDelay
        ScheduleService:RemoveTimer(self)
        ScheduleService:AddTimer(self, self.OnRebootTimer, 1, true)
        self._reloginCnt = 0
        return
    end
    --重新连接
    self._reloginCnt = self._reloginCnt + 1
    self._loginForm:UpdateUI("UpdateTip", "连接游戏服务器...")
    LSKNetwork.Delegate = self
    LSKNetwork:OpenConnect()
end

--[[
    @desc: 准备礼品相关配置信息和相关图片资源
    @return:
]]
function LSKLoginStateClass:DoPreparePrizeCfgAndRes(pids)
    --
    if not pids or #pids==0 then
        LSKBoot:ChangeState("LSK_Select_LogicState")
        return
    end
    --
    self._loginForm:UpdateUI("UpdateTip", "获取游戏配置")
    self._loginForm:UpdateUI("UpdateProgress", 0)
    --这是异步的
    PrizeSystem:PreparePrizeInfos(pids,self, self.OnPrizeSystemCallback)

end

function LSKLoginStateClass:OnPrizeSystemCallback(isError,progress)
    
    if not isError then
        self._loginForm:UpdateUI("UpdateProgress", progress)
        if progress >= 1 then
            self._tryGetCfgCnt=0
            LSKData:InitPrizeData()
            LSKBoot:ChangeState("LSK_Select_LogicState")
        end
    else
        self._loginForm:UpdateUI("UpdateTip", "获取游戏配置配置等待重试!")
        ScheduleService:RemoveTimer(self)
        ScheduleService:AddTimer(self,self.Delay2RePreparePrizeCfg,5,false)
    end
    
end

--重试拉取礼品配置
function LSKLoginStateClass:Delay2RePreparePrizeCfg()
    
    if self._tryGetCfgCnt >= self._tryGetCfgMaxCnt then
        self._loginForm:UpdateUI("UpdateTip", "登录礼品配置失败,即将重启！")
        self._rebootCntDown = self._rebootDelay
        ScheduleService:RemoveTimer(self)
        ScheduleService:AddTimer(self, self.OnRebootTimer, 1, true)
        self._tryGetCfgCnt = 0
        return
    end
    self:DoPreparePrizeCfgAndRes(self._cachedPrizeIds)

end


function LSKLoginStateClass:OnRecvRelayMsg(data, result, cmd)
    --Nothing


end


function LSKLoginStateClass:OnRebootTimer()
   
    if (self._rebootCntDown < 0) then
        ScheduleService:RemoveTimer(self)
        NativeService:RebootDevice()
    else
        if (self._rebootCntDown <= 5) then
            self._loginForm:UpdateUI("UpdateTip", "重启倒计时：" .. self._rebootCntDown)
        end
        self._rebootCntDown = self._rebootCntDown - 1
    end

end

-----------------------------断线重连处理------------------------
function ReConnectSession:BeginSession()

    LogD("LSK 开始断线重连")
    LSKUICommon:CleanWaiting()
    LSKUICommon:ShowWaiting(true,"断线重连...")
    self.IsWorking=true
    LSKNetwork.Delegate=self
    LSKNetwork:OpenConnect()

end

function ReConnectSession:OnNetworkClosed()
    LogD("----------------------LSK网络断开------------------")
    self:BeginSession()
end

function ReConnectSession:OnConnectToSvrBack(isSucceed)

    if isSucceed then
        LogD("LSK重新连接成功")
        --重新发登录
        local args = {
            token = MachineData.Token
        }
        LSKNetwork:SendMsg(LSKNet.Cmd.cs_req_m_login_game, args)
    else
        --
        LogD("LSK重新连接失败")
        LSKNetwork.Delegate=false
        LSKNetwork:CloseConnnect()
        ScheduleService:RemoveTimer(self)
        ScheduleService:AddTimer(self,self.Delay2RetryConSvr,5,false)
    end

end

function ReConnectSession:Delay2RetryConSvr()
    
    LogD("LSK重新建立连接")
    self._retryCnt = self._retryCnt + 1
    if self._retryCnt >= self._maxRetryCnt then
        
        ScheduleService:RemoveTimer(self)
        self._retryCnt = 0
        LSKUICommon:CleanWaiting()
        GameStateService:ChangeState("LoginGameState")
        return
    end

    LogD("----------->%d<----------",self._retryCnt)
    self.IsWorking=true
    LSKNetwork.Delegate=self
    LSKNetwork:OpenConnect()

end

function ReConnectSession:OnLoginLSKSvrRsp(rsp,result)
    
    if result ~= MachineNet.Result.EN_Result_OK then
        LogD("LSK 重新登录失败")
        LSKNetwork.Delegate=false
        LSKNetwork:CloseConnnect()
        ScheduleService:RemoveTimer(self)
        ScheduleService:AddTimer(self,self.Delay2RetryConSvr,5,false)
    else
        --
        if rsp then
            self.IsWorking=false
            LSKNetwork.Delegate=self
            ScheduleService:RemoveTimer(self)
            LogD("LSK 重新登录成功!")
            LSKUICommon:CleanWaiting()
            --开始发送心跳包
            if rsp.use_heart_beat then
                LSKNetwork:StartHearbeat(rsp.heart_beat_interval)
            end
            -- 
            LSKData.User = false
            LSKBoot:ChangeState('LSK_Select_LogicState')
            --
        else
            LogD("LSK 重新登录失败")
            LSKNetwork.Delegate=false
            LSKNetwork:CloseConnnect()
            ScheduleService:RemoveTimer(self)
            ScheduleService:AddTimer(self,self.Delay2RetryConSvr,5,false)
        end
    end
end

