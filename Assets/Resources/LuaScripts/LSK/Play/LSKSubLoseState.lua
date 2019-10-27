--[[********************************************************************
*		作者： XH
*		时间： 2018-12-11
*		描述： 挑战失败状态
*********************************************************************--]]

local LSKSubLoseStateClass = DeclareClass("LSKSubLoseStateClass")

function LSKSubLoseStateClass:ctor()
    self._playFsm = false
    self._leftSeconds = false
    self._loseForm = false
    self._noEnoughCoinForm = false

    self._isCanRetry = false
    self._isSelectConfirm = true

    self._sendEndOp = false
end

function LSKSubLoseStateClass:vInitializeState(fsm)
    self._playFsm = fsm
end

function LSKSubLoseStateClass:vUninitializeState()

    self._playFsm = false

end

function LSKSubLoseStateClass:vGetName()
    return "LSK_Play_Sub_LoseState"
end


function LSKSubLoseStateClass:vOnStateEnter(param,oldSt)
    self._sendEndOp = false

    self:AddInputEvents()
    -- LSKAudio:PlaySound(LSKAudioID.Lose)

    self._leftSeconds = LSKData.RetryTimeout
    if not self._loseForm then
        if LSKData:IsCanRetry() then
            self._loseForm = ClassLib.LSKLoseFormClass.new()
            self._loseForm:Create(nil, self._leftSeconds)
            self._loseForm:OnSelect(self._isSelectConfirm)
            AudioService:Play(AudioChannelType.ACT_UI, 'LSK/Audio/Voice_2', false)
        else
            self._loseForm = ClassLib.LSKLoseAnimFormClass.new()
            self._loseForm:Create()
            self._leftSeconds = 3
        end
    end

    ScheduleService:AddTimer(self,self.OnTimerUpdate,1,true)

end

function LSKSubLoseStateClass:OnTimerUpdate()
    self._leftSeconds = self._leftSeconds - 1
    if self._loseForm then
        self._loseForm:vOnUpdateUI('LeftSeconds', self._leftSeconds)
    end
    if self._noEnoughCoinForm then
        if self._leftSeconds >= 0 then
            self._noEnoughCoinForm:Countdown(self._leftSeconds)
        end
    end
    if self._leftSeconds <= 0 then
        self:Cancel()
    end
end

function LSKSubLoseStateClass:vOnStateLeave(param)
    self:RemoveInputEvents()
    if self._loseForm then
        self._loseForm:Close()
        self._loseForm = false
    end
    if self._noEnoughCoinForm then
        self._noEnoughCoinForm:Close()
        self._noEnoughCoinForm = false
    end
    ScheduleService:RemoveTimer(self,self.OnTimerUpdate)
    self._leftSeconds = false
    self._isCanRetry = false
    self._isSelectConfirm = true
    self._sendEndOp = false

end

function LSKSubLoseStateClass:AddInputEvents()
    ArcadeInputService:RegisterOkInput(self,self.OnTriggerOK)
    ArcadeInputService:RegisterRockerInput(self,self.OnTriggerRocker)
    ArcadeInputService:RegisterRefreshInput(self,self.OnTriggerRefresh)
end

function LSKSubLoseStateClass:RemoveInputEvents()
    ArcadeInputService:UnRegisterOkInput(self)
    ArcadeInputService:UnRegisterRockerInput(self)
    ArcadeInputService:UnRegisterRefreshInput(self)
end

function LSKSubLoseStateClass:OnTriggerRocker(eventType)
    if not LSKData:IsCanRetry() then
        return
    end
    
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
    if self._loseForm and changed then
        self._loseForm:OnSelect(self._isSelectConfirm)
    end
end
-- 按下开始键
-- 复活
function LSKSubLoseStateClass:OnTriggerOK(eventType)
    if eventType ~= ArcadeInput.PressEvent.PressClick then
        return
    end
    if not LSKData.User then
        LSKUICommon:ShowBubble('玩家登录错误')
        return
    end
    if self._isSelectConfirm then
        self:Retry()
    else
        self:Cancel()
    end

end

function LSKSubLoseStateClass:Retry()
    if not LSKData:IsCanRetry() then
        return
    end

    LSKAudio:StopSound()
    local isCanRetry = LSKData.RetryCost <= LSKData.User.DropCoin
    if isCanRetry then
        -- 开始续命
        local msg = {round=LSKGame.CurLevel}
        LSKNetwork:SendMsg(LSKNet.Cmd.cs_req_retry_lipstick, msg)
        self._sendEndOp = true
        LSKUICommon:ShowWaiting(true)
    else
        -- 弹出余额不足界面
        if not self._noEnoughCoinForm then
            self._noEnoughCoinForm = ClassLib.LSKNoEnoughCoinFormClass.new()
            self._noEnoughCoinForm:Create(nil, nil)
            self._noEnoughCoinForm:ShowCountdown(self._leftSeconds)
        end
    end
end

function LSKSubLoseStateClass:Cancel()
    self:SendEndGame()
    LSKGame:DealOpCmd(LSKGameOpCmd.StopRotate)
    LSKGame:DealOpCmd(LSKGameOpCmd.Over)
end
-- 按下刷新键
-- 回到选择口红
function LSKSubLoseStateClass:OnTriggerRefresh(eventType)
    if eventType ~= ArcadeInput.PressEvent.PressClick then
        return
    end
end

function LSKSubLoseStateClass:OnRecvRelayMsg(data, result, cmd)
    if cmd == LSKNet.Cmd.cs_notify_m_player_buy_in then
        if not LSKData.User then
            LSKUICommon:ShowBubble('玩家登录错误')
            return
        end
        if LSKData.User.DropCoin >= LSKData.RetryCost then
            if self._noEnoughCoinForm then
                self._noEnoughCoinForm:Close()
                self._noEnoughCoinForm = false
            end
        end
    elseif cmd == LSKNet.Cmd.cs_rsp_retry_lipstick then
        LSKUICommon:ShowWaiting(false)
        if result ~= LSKNetResult.OK then
            LSKUICommon:ShowBubble(LSKNetResultDesc[result] or 'Error '..tostring(result))
            LSKBoot:ChangeState('LSK_Select_LogicState')
            return
        end

        LSKData.CurRetryCount = data.retry_time or 0

        LSKData:SetDropCoin(data.left_buy_in)
        -- 续命成功，开始游戏
        local gmMode = data.can_get_prize and LSKGameMode.Normal or LSKGameMode.Trick
        local arg = {level=LSKGame.CurLevel,opCmd=LSKGameOpCmd.Replay, gameMode = gmMode}
        LSKGame:DealOpCmd(LSKGameOpCmd.StopRotate)
        self._playFsm:ChangeState('LSK_Play_Sub_ReadyState',arg)
    end
end

function LSKSubLoseStateClass:SendEndGame()
    if self._sendEndOp then
        return
    end
    LSKAudio:StopSound()
    local msg = {get_prize=false}
    LSKNetwork:SendMsg(LSKNet.Cmd.cs_req_end_lipstick, msg)
    LSKUICommon:ShowWaiting(true)
    self._sendEndOp = true
end

