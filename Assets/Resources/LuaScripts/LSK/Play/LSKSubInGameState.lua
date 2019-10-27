local LSKSubInGameStateClass = DeclareClass("LSKSubInGameStateClass")

function LSKSubInGameStateClass:ctor()
    self._playFsm = false
    self._endLevelParam = false

end

function LSKSubInGameStateClass:vInitializeState(fsm)
    self._playFsm = fsm
end

function LSKSubInGameStateClass:vUninitializeState()
    self._playFsm = false


end

function LSKSubInGameStateClass:vGetName()
    return "LSK_Play_Sub_GameState"
end

function LSKSubInGameStateClass:vOnStateEnter(param,oldSt)
    if not param then
        LogE('Bad LSKGameOpCmd')
        return
    end

    self:AddInputEvents()

    param.delegate = self
    LSKGame:DealOpCmd(param.opCmd, param)
end

function LSKSubInGameStateClass:vOnStateLeave(param)
    ScheduleService:RemoveTimer(self)
    self:RemoveInputEvents()
    self._endLevelParam = false
    LSKGame:DealOpCmd(LSKGameOpCmd.StopRecvCmd, nil)
end

function LSKSubInGameStateClass:AddInputEvents()
    ArcadeInputService:RegisterOkInput(self,self.OnTriggerOK)
    ArcadeInputService:RegisterRefreshInput(self,self.OnTriggerRefresh)
end

function LSKSubInGameStateClass:RemoveInputEvents()
    ArcadeInputService:UnRegisterOkInput(self)
    ArcadeInputService:UnRegisterRefreshInput(self)
end

-- 按下开始键
function LSKSubInGameStateClass:OnTriggerOK(eventType)
    if eventType ~= ArcadeInput.PressEvent.PressBegin then
        return
    end
    LSKGame:DealOpCmd(LSKGameOpCmd.Throw)
end

-- 有其他按键触发
function LSKSubInGameStateClass:OnTriggerRefresh()
    if eventType ~= ArcadeInput.PressEvent.PressClick then
        return
    end
end

function LSKSubInGameStateClass:OnLevelEnd(level, isCurLevelPass, isFinalWin)
    self._endLevelParam = {level=level,isCurLevelPass=isCurLevelPass,isFinalWin=isFinalWin}
    ScheduleService:AddTimer(self,self.OnDelay2ChangeState,1.5,false)

    -- 是否结束，如果未结束就开始下一关
    local isOver = not isCurLevelPass or isFinalWin
    if isOver then
        if isFinalWin then
            AudioService:Play(AudioChannelType.ACT_UI, 'LSK/Audio/Voice_4', false)
        end
    else
        local audio = false
        if level == 1 then
            audio = 'Voice_10'
        elseif level == 2 then
            audio = 'Voice_11'
        end
		AudioService:Play(AudioChannelType.ACT_UI, 'LSK/Audio/'..audio, false)
    end

end

function LSKSubInGameStateClass:OnRecvRelayMsg(data, result, cmd)
    if cmd == LSKNet.Cmd.cs_notify_m_player_buy_in then
        if not LSKData.User then
            LSKUICommon:ShowBubble('玩家登录错误')
            return
        end
    end
end


function LSKSubInGameStateClass:OnDelay2ChangeState()
    local level = self._endLevelParam.level
    local isCurLevelPass = self._endLevelParam.isCurLevelPass
    local isFinalWin = self._endLevelParam.isFinalWin
    -- 是否结束，如果未结束就开始下一关
    local isOver = not isCurLevelPass or isFinalWin
    if isOver then
        if isFinalWin then
            local msg = {get_prize=true}
            LSKNetwork:SendMsg(LSKNet.Cmd.cs_req_end_lipstick, msg)
        else
            self._playFsm:ChangeState('LSK_Play_Sub_LoseState')
        end
    else
        local arg = {level=level+1,opCmd=LSKGameOpCmd.NextLevel}
        self._playFsm:ChangeState('LSK_Play_Sub_ReadyState',arg)
    end
    ScheduleService:RemoveTimer(self,self.OnDelay2ChangeState)
end