--[[********************************************************************
*		作者： XH
*		时间： 2018-12-11
*		描述： 抽奖状态
*********************************************************************--]]

local LSKSubLotteryStateClass = DeclareClass("LSKSubLotteryStateClass")

local Wait_Seconds = 20

local LSKEndRspStep =
{
    --1: 下一轮，2: 抽奖，3: 保底购买
    Next = 1,
    Lottery = 2,
    SecurityBuy = 3,
}

local AutoLotteryCountDown = 3

local SpinWheelState = 
{
    None = -1,-- 无
    Idle = 0, -- 进入抽奖状态，等待玩家操作
    Wait = 1, -- 等待服务器响应
    Rotate = 2,-- 开始快速旋转
    Slowdown = 3,-- 减慢
    Over = 4, -- 转动结束
}
function LSKSubLotteryStateClass:ctor()
    self._playFsm = false
    self._leftSeconds = false

    self._spinWinForm = false
    self._award = false
    self._awardIdx = false
    self._state = false
    self._elapsedTime = 0
end

function LSKSubLotteryStateClass:vInitializeState(fsm)
    self._playFsm = fsm
    self._state = SpinWheelState.None
end

function LSKSubLotteryStateClass:vUninitializeState()
    self._playFsm = false
    self._award = false
    self._state = false
    self._elapsedTime = 0

end

function LSKSubLotteryStateClass:vGetName()
    return "LSK_Play_Sub_LotteryState"
end

function LSKSubLotteryStateClass:vOnStateEnter(param,oldSt)
    LogD('Enter Lottery State')
    self._state = SpinWheelState.Idle
    self._elapsedTime = 0

    if param and param.timeout then
        self._leftSeconds = param.timeout
        Wait_Seconds = self._leftSeconds
    end
    self:AddInputEvents()
    ScheduleService:AddTimer(self,self.OnTimerUpdate,1,true)
    if not self._spinWinForm then
        self._spinWinForm = ClassLib.LSKSpinWinFormClass.new()
        self._spinWinForm:Create(nil, {delegate = self})
    end
end

function LSKSubLotteryStateClass:vOnStateLeave(param)
    self._leftSeconds = false

    ScheduleService:RemoveTimer(self)
    self:RemoveInputEvents()
    if self._spinWinForm then
        self._spinWinForm:Close()
        self._spinWinForm = false
    end
    self._state = SpinWheelState.None
end

function LSKSubLotteryStateClass:OnTimerUpdate()
    if not self._leftSeconds then
        return
    end

    self._leftSeconds = self._leftSeconds - 1
    if (self._leftSeconds <= 1) then
        if self._state == SpinWheelState.Idle then
            --开始抽奖
            self:StartLottery()
            self._state = SpinWheelState.Wait
        elseif self._state == SpinWheelState.Rotate then
            self:StopSpin()
        end
    end
    
    if self._state == SpinWheelState.Rotate then
        self._elapsedTime = self._elapsedTime + 1
        if self._elapsedTime == 3 then
            -- 提示按开始键结束
            if self._spinWinForm then
                self._spinWinForm:PlayTipAnim(true)
            end
        end
    end

    if self._state == SpinWheelState.Over and self._leftSeconds >= 0 then
        if self._spinWinForm then
            self._spinWinForm:Countdown(self._leftSeconds)
        end
    end
end

function LSKSubLotteryStateClass:StartLottery()
    LSKUICommon:ShowWaiting(true)
    LSKNetwork:SendMsg(LSKNet.Cmd.cs_req_lottery, {})
end

function LSKSubLotteryStateClass:AddInputEvents()
    ArcadeInputService:RegisterOkInput(self,self.OnTriggerOK)
    ArcadeInputService:RegisterRefreshInput(self,self.OnTriggerRefresh)
end

function LSKSubLotteryStateClass:RemoveInputEvents()
    ArcadeInputService:UnRegisterOkInput(self)
    ArcadeInputService:UnRegisterRefreshInput(self)
end

-- 按下开始键
function LSKSubLotteryStateClass:OnTriggerOK(eventType)
    if eventType ~= ArcadeInput.PressEvent.PressClick then
        return
    end

    if self._state == SpinWheelState.Idle then
        self:StartLottery()
        self._state = SpinWheelState.Wait
    elseif self._state == SpinWheelState.Rotate then
        if self._elapsedTime < 3 then
            return
        end
        self:StopSpin()
    end
end

function LSKSubLotteryStateClass:StopSpin()

    if self._spinWinForm then
        self._spinWinForm:StopSpin(self._awardIdx)
    end
    self._state = SpinWheelState.Slowdown

end

-- 按下刷新键
function LSKSubLotteryStateClass:OnTriggerRefresh(eventType)
    if eventType ~= ArcadeInput.PressEvent.PressClick then
        return
    end
end

function LSKSubLotteryStateClass:OnRecvRelayMsg(data, result, cmd)
    if cmd == LSKNet.Cmd.cs_rsp_lottery then
        self:OnLotteryRsp(data, result)
    elseif cmd == LSKNet.Cmd.cs_rsp_lottery_end then
        if result ~= LSKNetResult.OK then
            LSKUICommon:ShowBubble(LSKNetResultDesc[result] or 'Error '..tostring(result))
            return
        end
        self._state = SpinWheelState.Over
        self._leftSeconds = data.timeout
    end
end

function LSKSubLotteryStateClass:OnLotteryRsp(data, result)
    LSKUICommon:ShowWaiting(false)
    if result ~= LSKNetResult.OK then
        LSKUICommon:ShowBubble(LSKNetResultDesc[result] or 'Error '..tostring(result))
        return
    end

    if data.prize_num then
        LSKData:SetPrizeCount(data.prize_num)
    end

    local lotteryIdx = data.award_index + 1
    -- 重置倒计时
    self._leftSeconds = data.timeout
    local isGotPrize = false
    if data.awards and #data.awards > 0 then
        isGotPrize = data.awards[1].type ~= LSKAwardType.None
    end


    local award = LSKData.LotteryList[lotteryIdx]
    if award then
        -- 临时存放中奖数据
        self._awardIdx = lotteryIdx
        self._award = award
        self._award.count = data.awards[1].giving_num
    else
        -- 查找中奖信息
        local idx = LSKData:GetLotteryIdx(data.awards[1])
        if not idx then
            self._awardIdx = -1
            self._award = false
        else
            -- 临时存放中奖数据
            self._awardIdx = idx
            self._award = LSKData.LotteryList[idx]
            self._award.count = data.awards[1].giving_num
        end
    end
    LogD("<color=lime>是否中奖：</color>" .. (isGotPrize and "<color=white>中奖" or "<color=yellow>未中奖") .. "</color>")

    -- 转盘开始旋转
    if self._spinWinForm then
        self._spinWinForm:StartSpin()
    end
    -- 切换抽奖子状态
    self._state = SpinWheelState.Rotate
    self._elapsedTime = 0
end

function LSKSubLotteryStateClass:OnSpinEnd()
    local isWin = false
    if self._award then
        isWin = self._award.type ~= LSKAwardType.None
    end
    if self._spinWinForm then
        self._spinWinForm:ShowResult(isWin, self._award)
    end
    if isWin then
        LSKBoot.PlayerHud:Refresh()
        AudioService:Play(AudioChannelType.ACT_UI, 'LSK/Audio/Voice_5', false)
    end
    LSKNetwork:SendMsg(LSKNet.Cmd.cs_req_lottery_end, {})
end
