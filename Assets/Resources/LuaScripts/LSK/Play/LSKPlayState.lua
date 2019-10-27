
local LSKPlayStateClass = DeclareClass("LSKPlayStateClass")

local LSKEndRspStep =
{
    --1: 下一轮，2: 抽奖，3: 保底购买
    Next = 1,
    Lottery = 2,
    SecurityBuy = 3,
}
function LSKPlayStateClass:ctor()
   
    self._curSubState=false
    self._registedSubStates={}

end

function LSKPlayStateClass:vInitializeState()
    local readyState = ClassLib.LSKSubReadyStateClass.new()
    local gameState = ClassLib.LSKSubInGameStateClass.new()
    local winState = ClassLib.LSKSubWinStateClass.new()
    local loseState = ClassLib.LSKSubLoseStateClass.new()
    local lotteryState = ClassLib.LSKSubLotteryStateClass.new()
    local securityState = ClassLib.LSKSubSecurityBuyStateClass.new()

    self:RegisterState(readyState:vGetName(), readyState)
    self:RegisterState(winState:vGetName(), winState)
    self:RegisterState(loseState:vGetName(), loseState)
    self:RegisterState(lotteryState:vGetName(), lotteryState)
    self:RegisterState(gameState:vGetName(), gameState)
    self:RegisterState(securityState:vGetName(), securityState)
end

function LSKPlayStateClass:vUninitializeState()
    if self._curSubState then
        self._curSubState:vOnStateLeave()
        self._curSubState = false
    end

    if self._registedSubStates then
		for k ,v in pairs(self._registedSubStates) do
       		v:vUninitializeState()
            self._registedSubStates[k] = nil
    	end
    	self._registedSubStates=false
	end
    ScheduleService:RemoveTimer(self)
end

function LSKPlayStateClass:vGetName()
    return "LSK_Play_LogicState"
end

function LSKPlayStateClass:vOnStateEnter(param,oldSt)
    LSKGame:DealOpCmd(LSKGameOpCmd.ShowScene)

    LogD("Enter LSK Play State!");
    local arg = {level=1,opCmd=LSKGameOpCmd.Start,gameMode=param.gameMode, prize = param.prize}
    self:ChangeState('LSK_Play_Sub_ReadyState', arg)

end

function LSKPlayStateClass:vOnStateLeave(param)
    LSKGame:DealOpCmd(LSKGameOpCmd.HideScene)
    if self._curSubState then
        self._curSubState:vOnStateLeave()
        self._curSubState=false
    end
end

--注册逻辑状态
function LSKPlayStateClass:RegisterState(name,state)

	if self._registedSubStates[name] then
		LogE("Already Registered Logic State "..name)
		return
	end
    if state then
       state:vInitializeState(self)
    end
    self._registedSubStates[name]=state
    
end

--切换逻辑状态
function LSKPlayStateClass:ChangeState(name,param)

	if not self._registedSubStates[name] then
		LogE("No Registered Play SubState "..name)
	end
	local old=""
	if self._curSubState then
		
		old=self._curSubState:vGetName()
		if self._curSubState:vGetName()==name then
            LogD("------------------Aleary In Play State----------------")
            LogD(' State Name = '..name)
            return
        else
            self._curSubState:vOnStateLeave(param)
        end
	end
    ---
	self._curSubState=self._registedSubStates[name]
    self._curSubState:vOnStateEnter(param,old)
    LSKBoot.PlayerHud:Refresh()

    LogD('Change Play State ->'..name)
    return self._curSubState
end

function LSKPlayStateClass:CanShowAwardQRCode()
    local canShow = not self._curSubState or (self._curSubState:vGetName() == 'LSK_Play_Sub_LotteryState' and
                                              self._curSubState:vGetName() == 'LSK_Play_Sub_SecurityBuyState' and
                                              self._curSubState:vGetName() == 'LSK_Play_Sub_WinState'
                                              )
end

function LSKPlayStateClass:OnNotifyPlayerDropCoin(msg,result)
    if result ~= LSKNetResult.OK then
        LSKUICommon:ShowBubble(LSKNetResultDesc[result] or 'Error '..tostring(result))
        return
    end
    LSKData:SetDropCoin(msg.cur_buy_in)
end

function LSKPlayStateClass:OnRecvRelayMsg(data, result, cmd)
   if cmd == LSKNet.Cmd.cs_notify_m_player_buy_in then
        if not LSKData.User then
            LSKUICommon:ShowBubble('玩家登录错误')
            return
        end
        -- 
        data = {cur_buy_in = data.cur_buy_in, add = data.cur_buy_in - LSKData.User.DropCoin,coin = data.cur_buy_in}
        self:OnNotifyPlayerDropCoin(data, result)
    elseif cmd == LSKNet.Cmd.cs_rsp_end_lipstick then
        self:OnEndLSKGameRsp(data, result)
        LSKUICommon:ShowWaiting(false)
    end

    if self._curSubState then
        self._curSubState:OnRecvRelayMsg(data, result, cmd)
    end
end

-- message CSRspEndLipstick
-- {
--     optional uint32 next_step       = 3; // 下一步，1: 下一轮，2: 抽奖，3: 保底购买
--     optional uint32 timeout         = 1; // 下一步倒计时
--     optional uint32 prize_id        = 2; // next_step 1: 中奖id，没中不传；next_step 3: 保底购买id
--     optional uint64 buy_cost        = 4; // 保底购买需要的价格
--     optional uint32 game_succ_timeout       = 7; // next_step 1时通关成功等待时间
-- }
function LSKPlayStateClass:OnEndLSKGameRsp(data, result)
    if result ~= LSKNetResult.OK then
        LSKUICommon:ShowBubble(LSKNetResultDesc[result] or 'Error '..tostring(result))
        LSKBoot:ChangeState('LSK_Select_LogicState')
        return
    end
    if data.prize_num and LSKData.User.PrizeCount ~= data.prize_num then
        LSKData:SetPrizeCount(data.prize_num)
        LSKBoot.PlayerHud:Refresh()
    end
    local lotterys = data.lotterys
    local nextStep = data.next_step
    local nextTimeout = data.timeout

    if nextStep == LSKEndRspStep.Next then
        local isWinPrize = data.prize_id ~= nil
        if isWinPrize then
            self:ChangeState('LSK_Play_Sub_WinState', {prizeId = data.prize_id,timeout=data.game_succ_timeout})
        else
            -- 开始下一轮
            LSKBoot:ChangeState('LSK_Select_LogicState',{timeout=nextTimeout})
        end
    elseif nextStep == LSKEndRspStep.SecurityBuy then
        -- 保底购买
        local msg = {prizeId = data.prize_id, cost = data.buy_cost, timeout=nextTimeout}
        self:ChangeState('LSK_Play_Sub_SecurityBuyState', msg)
    elseif nextStep == LSKEndRspStep.Lottery then
        -- 抽奖
        self:ChangeState('LSK_Play_Sub_LotteryState',{timeout=data.timeout})
    end
end