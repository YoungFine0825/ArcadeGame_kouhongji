--[[********************************************************************
*		作者： XH
*		时间： 2018-12-11
*		描述： 保底购买
*********************************************************************--]]

local LSKSubSecurityBuyStateClass = DeclareClass("LSKSubSecurityBuyStateClass")

local Wait_Seconds = 20
function LSKSubSecurityBuyStateClass:ctor()
    self._playFsm = false
    self._buyForm = false
    self._noEnoughCoinForm = false
    self._isSelectConfirm = false
    self._cost = false
    self._leftSeconds = false

    self._endOp = false
end

function LSKSubSecurityBuyStateClass:vInitializeState(fsm)
    self._playFsm = fsm
end

function LSKSubSecurityBuyStateClass:vUninitializeState()
    self._playFsm = false
end

function LSKSubSecurityBuyStateClass:vGetName()
    return "LSK_Play_Sub_SecurityBuyState"
end


function LSKSubSecurityBuyStateClass:vOnStateEnter(param,oldSt)
    self._isSelectConfirm = true
    self._endOp = false

    self._cost = param.cost
    self._leftSeconds = Wait_Seconds
    if param and param.timeout then
        self._leftSeconds = param.timeout
    end
    if not self._buyForm then
        self._buyForm = ClassLib.LSKSecurityBuyFormClass.new()
        self._buyForm:Create(nil, param)
        self._buyForm:Countdown(self._leftSeconds)
        self._buyForm:OnSelect(self._isSelectConfirm)
    end
    AudioService:Play(AudioChannelType.ACT_UI, 'LSK/Audio/Voice_6', false)

    ArcadeInputService:RegisterOkInput(self,self.OnTriggerOK)
    ArcadeInputService:RegisterRockerInput(self,self.OnTriggerRocker)
    ScheduleService:AddTimer(self,self.OnTimerUpdate,1,true)
end

function LSKSubSecurityBuyStateClass:vOnStateLeave(param)
    ScheduleService:RemoveTimer(self)
    self._cost = false
    ArcadeInputService:UnRegisterOkInput(self)
    ArcadeInputService:UnRegisterRockerInput(self)
    if self._buyForm then
        self._buyForm:Close()
        self._buyForm = false
    end
    if self._noEnoughCoinForm then
        self._noEnoughCoinForm:Close()
        self._noEnoughCoinForm = false
    end
    self._leftSeconds = false

end

function LSKSubSecurityBuyStateClass:OnTimerUpdate()
    self._leftSeconds = self._leftSeconds - 1
    if self._buyForm and self._leftSeconds >= 0 then
        self._buyForm:Countdown(self._leftSeconds)
    end
    if self._noEnoughCoinForm and self._leftSeconds >= 0 then
        self._noEnoughCoinForm:Countdown(self._leftSeconds)
    end
    if self._leftSeconds <= 0 then
        self:RequestBuy(false)
    end
end

function LSKSubSecurityBuyStateClass:OnTriggerOK(eventType)
    if eventType ~= ArcadeInput.PressEvent.PressClick then
        return
    end
    
    LSKAudio:StopSound()
    if not self._endOp and self._isSelectConfirm then
        -- 选择保底购买
        local isEnoughCoin = LSKData.User.DropCoin >= self._cost
        if isEnoughCoin then
            self:RequestBuy(true)
        else
		    AudioService:Play(AudioChannelType.ACT_UI, 'LSK/Audio/Voice_9', false)
            if not self._noEnoughCoinForm then
                self._noEnoughCoinForm = ClassLib.LSKNoEnoughCoinFormClass.new()
                self._noEnoughCoinForm:Create()
                self._noEnoughCoinForm:ShowCountdown()
            end
        end
    else
        self:RequestBuy(false)
    end
end

function LSKSubSecurityBuyStateClass:RequestBuy(isBuy)
    if not self._endOp then
        local msg = {buy=isBuy}
        LSKNetwork:SendMsg(LSKNet.Cmd.cs_req_buy_prize, msg)
        self._endOp = true
    end
end

-------------------------------------
-- 遥感选择，选择口红.
-- @param eventType ： 摇杆方向.
-- 排除第2、3行的第3、4个
-------------------------------------
function LSKSubSecurityBuyStateClass:OnTriggerRocker(eventType)

    local changed = false
    -- 上
    if eventType == ArcadeInput.RockerState.RockerMoveForward then
        
    -- 下
    elseif eventType == ArcadeInput.RockerState.RockerMoveBack then

    -- 左
    elseif eventType == ArcadeInput.RockerState.RockerMoveLeft then
        self._isSelectConfirm = not self._isSelectConfirm
        changed = true
    -- 右
    elseif eventType == ArcadeInput.RockerState.RockerMoveRight then
        self._isSelectConfirm = not self._isSelectConfirm
        changed = true
    end
    if self._buyForm and changed then
        self._buyForm:OnSelect(self._isSelectConfirm)
    end
end

function LSKSubSecurityBuyStateClass:OnRecvRelayMsg(data, result, cmd)
    if cmd == LSKNet.Cmd.cs_notify_m_player_buy_in then
        if result ~= LSKNetResult.OK then
            LSKUICommon:ShowBubble(LSKNetResultDesc[result] or 'Error '..tostring(result))
            self:BackToSelect()
            return
        end
        if self._noEnoughCoinForm then
            local isEnough = LSKData.User.DropCoin >= self._cost
            if isEnough then
                self._noEnoughCoinForm:Close()
                self._noEnoughCoinForm = false
            end
        end
    elseif cmd == LSKNet.Cmd.cs_rsp_buy_prize then
        if result ~= LSKNetResult.OK then
            LSKUICommon:ShowBubble(LSKNetResultDesc[result] or 'Error '..tostring(result))
            self:BackToSelect()
            return
        end
        if self._buyForm then
            self._buyForm:OnBuySuccess(true)
        end
        self._leftSeconds = data.timeout
        local prizeId = data.prize_id
        if data.prize_num then
            LSKData:SetPrizeCount(data.prize_num)
            LSKBoot.PlayerHud:Refresh()
        end
    end
end

function LSKSubSecurityBuyStateClass:BackToSelect()
    -- LSKBoot:ChangeState('LSK_Select_LogicState')
end
