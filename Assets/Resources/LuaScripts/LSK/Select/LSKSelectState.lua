--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-15
*		描述： 选择口红状态
*********************************************************************--]]
local LSKSelectStateClass = DeclareClass("LSKSelectStateClass")

local TotalCount = 36
local TotalColumn = 6
local TotalLine = 6

local Max_Wait_Seconds = 120

function LSKSelectStateClass:ctor()
    self._selectForm = false
    self._scanStartForm = false
    self._noEnoughCoinForm = false
    self._quitConfirmForm = false

    self._waitSeconds = false
    self._trigger_key = false
    self._voiceSeconds = false

    self._confirmQuit = false
    self._selectIdx = 1
    self._scanStartTime = 0
end

function LSKSelectStateClass:vInitializeState()
    
end

function LSKSelectStateClass:vUninitializeState()
    self:CloseAllForm()
    ScheduleService:RemoveTimer(self)
end

function LSKSelectStateClass:vGetName()
    return "LSK_Select_LogicState"
end

function LSKSelectStateClass:EnterAgain(arg)
    if not LSKData:IsLogin() then
        self:CloseForm(self._scanStartForm)
        self._scanStartForm = false
        self:CloseForm(self._noEnoughCoinForm)
        self._noEnoughCoinForm = false
        self:CloseForm(self._quitConfirmForm)
        self._quitConfirmForm = false

        self._confirmQuit = false
        LSKBoot.PlayerHud:Show()
        LSKBoot.PlayerHud:Refresh()
        self._waitSeconds = LSKData:IsLogin() and LSKData.LoginTimeout or  Max_Wait_Seconds
    end

end

function LSKSelectStateClass:vOnStateEnter(param,oldSt)
    LogD("Enter LSK_Select_LogicState");
    LSKBoot.PlayerHud:Show()

    self._trigger_key = false
    self._voiceSeconds = LSKUtil:GetRandom(LSKPickVoiceMin,LSKPickVoiceMax)

    self._waitSeconds = LSKData:IsLogin() and LSKData.LoginTimeout or  Max_Wait_Seconds
    if param and param.timeout then
        self._waitSeconds = param.timeout
    end

    self._selectIdx = 1
    if not self._selectForm then
        self._selectForm = ClassLib.LSKSelectFormClass.new()
        self._selectForm:Create(nil, self._waitSeconds, 0)
        local isLoginSwitch = (param and param.loginNow) or false
        if isLoginSwitch then
            self._selectForm:vOnUpdateUI('On_Player_Login', self._waitSeconds)
        end
    end
    self:AddInputEvents()
    ScheduleService:AddTimer(self,self.OnTimerUpdate,1,true)
end

function LSKSelectStateClass:OnTimerUpdate()
    self._waitSeconds = self._waitSeconds - 1

    if self._selectForm then
        if self._waitSeconds >= 0 then
            self._selectForm:vOnUpdateUI('Left_Seconds', self._waitSeconds)
        end
    end

    if self._waitSeconds <= 0 then
        if not LSKData:IsLogin() then
            LSKBoot:ChangeState('LSK_Presentation_State', nil)
        end
    end

    if not LSKData:IsLogin() then
        self._voiceSeconds = self._voiceSeconds or 0
        self._voiceSeconds = self._voiceSeconds - 1
        if self._voiceSeconds <= 0 then
            AudioService:Play(AudioChannelType.ACT_UI, 'LSK/Audio/Voice_7', false)
            self._voiceSeconds = LSKUtil:GetRandom(LSKPickVoiceMin,LSKPickVoiceMax)
        end
    end
end

function LSKSelectStateClass:vOnStateLeave(param)
    self._trigger_key = false
    
    ScheduleService:RemoveTimer(self,self.OnTimerUpdate)
    self:RemoveInputEvents()
    self:CloseAllForm()
    self._confirmQuit = false

end

function LSKSelectStateClass:CloseAllForm()
    self:CloseForm(self._selectForm)
    self._selectForm = false
    self:CloseForm(self._scanStartForm)
    self._scanStartForm = false
    self:CloseForm(self._noEnoughCoinForm)
    self._noEnoughCoinForm = false
    self:CloseForm(self._quitConfirmForm)
    self._quitConfirmForm = false
end

function LSKSelectStateClass:AddInputEvents()
    ArcadeInputService:RegisterOkInput(self,self.OnTriggerOK)
    ArcadeInputService:RegisterRefreshInput(self,self.OnTriggerRefreshKey)
    ArcadeInputService:RegisterRockerInput(self,self.OnTriggerRocker)
    ArcadeInputService:RegisterRotateInput(self,self.OnTriggerKey)
    LogW("Register Select State Arcade Input Events")
end

function LSKSelectStateClass:RemoveInputEvents()
    ArcadeInputService:UnRegisterOkInput(self)
    ArcadeInputService:UnRegisterRefreshInput(self)
    ArcadeInputService:UnRegisterRockerInput(self)
    ArcadeInputService:UnRegisterRotateInput(self)
    LogW("Remove Select State Arcade Input Events")
end

-- 按下开始键
function LSKSelectStateClass:OnTriggerOK(eventType)
    self._trigger_key = true
    if eventType ~= ArcadeInput.PressEvent.PressClick then
        return
    end
    -- 处理玩家是否退出游戏逻辑
    if self._quitConfirmForm then
        if self._confirmQuit then
            self:Logout()
        else
            self:CancelLogout()
        end
    else
    -- 处理玩家登录游戏后，选择口红->开始游戏逻辑
        if self._selectForm:IsExperienceTogOn() then
            --进入体验模式
            local prize = LSKData:GetPrizeByIdx(1)
            LSKBoot:ChangeState('LSK_Trial_State', {prize=prize.PrizeInfo})
        else
            self:TryStart()
        end
    end
   
end

function LSKSelectStateClass:TryStart()
    -- 是否有玩家登录
    local isLogin = LSKData:IsLogin()
    if isLogin then
        -- 游戏币是否够
        local isEnough = LSKData.User.DropCoin >= LSKData.Cost
        if isEnough then
            AudioService:Play(AudioChannelType.ACT_UI, 'LSK/Audio/Voice_8', false)
            local realIdx = LSKUtil:GetLSKRealIdx(self._selectIdx)
            local prize = LSKData:GetPrizeByIdx(realIdx)
            local msg = {prize_id=prize.PrizeInfo.Id}
            LSKNetwork:SendMsg(LSKNet.Cmd.cs_req_start_lipstick, msg)
            LSKUICommon:ShowWaiting(true)
        else
            if not self._noEnoughCoinForm then
                self._noEnoughCoinForm = ClassLib.LSKNoEnoughCoinFormClass.new()
                self._noEnoughCoinForm:Create()
                AudioService:Play(AudioChannelType.ACT_UI, 'LSK/Audio/Voice_9', false)
            end
        end
    else
        if not self._scanStartForm then
            if LSKUtil:GetFloatTimeStamp() - self._scanStartTime < 1 then
                return
            end
            self._scanStartForm = ClassLib.LSKScanStartFormClass.new()
            self._scanStartForm:Create(nil, nil, 0)
            self._scanStartTime = LSKUtil:GetFloatTimeStamp()
        end
    end
end

function LSKSelectStateClass:Logout()
    -- 退出
    LSKNetwork:SendMsg(LSKNet.Cmd.cs_req_player_logout, {})
end

function LSKSelectStateClass:CancelLogout()
    self:CloseForm(self._quitConfirmForm)
    self._quitConfirmForm = false
end


-- 有其他按键触发
function LSKSelectStateClass:OnTriggerKey()
    self._trigger_key = true
end

function LSKSelectStateClass:OnTriggerRefreshKey()
    self._trigger_key = true
    if LSKData:IsLogin() then
        if not self._quitConfirmForm then
            self._confirmQuit = false
            self._quitConfirmForm = ClassLib.LSKQuitConfirmFormClass.new()
            self._quitConfirmForm:Create(nil,15)
            self._quitConfirmForm:OnSelect(self._confirmQuit)
        end
    else
        if LSKUtil:GetFloatTimeStamp() - self._scanStartTime > 0.5 then
            self:CloseForm(self._scanStartForm)
            self._scanStartForm = false
        end
    end
end

-------------------------------------
-- 遥感选择，选择口红.
-- @param eventType ： 摇杆方向.
-- 排除第2、3行的第3、4个
-------------------------------------
function LSKSelectStateClass:OnTriggerRocker(eventType)
    self._trigger_key = true

    if self._scanStartForm then
        LogW("Please Scan QRCode To Login!")
        return
    end

    if self._quitConfirmForm then
        self:ChooseQuit(eventType)
    else
        self:SelectLsk(eventType)
    end
end

function LSKSelectStateClass:ChooseQuit(eventType)
    local isChange = false
    -- 上
    if eventType == ArcadeInput.RockerState.RockerMoveForward then
    -- 下
    elseif eventType == ArcadeInput.RockerState.RockerMoveBack then
    -- 左
    elseif eventType == ArcadeInput.RockerState.RockerMoveLeft then
        self._confirmQuit = not self._confirmQuit
        isChange = true
    -- 右
    elseif eventType == ArcadeInput.RockerState.RockerMoveRight then
        self._confirmQuit = not self._confirmQuit
        isChange = true
    end
    if self._quitConfirmForm then
        self._quitConfirmForm:OnSelect(self._confirmQuit)
    end
end

function LSKSelectStateClass:SelectLsk(eventType)
    if eventType == ArcadeInput.RockerState.RockerMoveMiddle then
        return
    end
    local idx = self._selectIdx
    local nextIdx = idx
    local isCircle = false
    local isExperienceTogOn = self._selectForm:IsExperienceTogOn()
    local curLine = math.floor((idx-1)/TotalLine + 1)
    local curColumn = (idx-1)%TotalColumn + 1

    -- 上
    if eventType == ArcadeInput.RockerState.RockerMoveForward then
        if LSKData.IsCanTrial then
            if isExperienceTogOn then
                self._selectForm:vOnUpdateUI("Select_ExperienceMode",false)
                nextIdx = self:GetMaxIdxBycolumn(curColumn)
            else
                nextIdx,isCircle = self:Up(idx)
                if isCircle then
                    self._selectForm:vOnUpdateUI("Select_ExperienceMode",true)
                    nextIdx = -1
                end
            end
        else
            nextIdx,isCircle = self:Up(idx)
        end
    -- 下
    elseif eventType == ArcadeInput.RockerState.RockerMoveBack then
        if LSKData.IsCanTrial then
            if isExperienceTogOn then
                self._selectForm:vOnUpdateUI("Select_ExperienceMode",false)
                nextIdx = curColumn
            else
                nextIdx,isCircle = self:Down(idx)
                if isCircle then
                    self._selectForm:vOnUpdateUI("Select_ExperienceMode",true)
                    nextIdx = -1
                end
            end
        else
            nextIdx,isCircle = self:Down(idx)
        end
    -- 左
    elseif eventType == ArcadeInput.RockerState.RockerMoveLeft then
        if LSKData.IsCanTrial and isExperienceTogOn then
            return
        end
        nextIdx = self:Left(idx)
    -- 右
    elseif eventType == ArcadeInput.RockerState.RockerMoveRight then
        if LSKData.IsCanTrial and isExperienceTogOn then
            return
        end
        nextIdx = self:Right(idx)
    end
    
    AudioService:Play(AudioChannelType.ACT_UI, 'LSK/Audio/Audio_Choose', false)
    if self._selectForm then
        self._selectForm:vOnUpdateUI('Select_Lipstick', nextIdx)
        if nextIdx ~= -1 then
            self._selectIdx = nextIdx
        end
    end
end

function LSKSelectStateClass:GetMaxIdxBycolumn(column)
    for i = TotalLine, 1, -1 do
        local idx = (TotalLine - 1)*TotalColumn + column
        if self:IsValid(idx) then
            return idx
        end
    end
    LogE('Error')
end

function LSKSelectStateClass:Up(idx)
    local curLine = math.floor((idx-1)/TotalLine + 1)
    local curRow = (idx-1)%TotalColumn + 1
    local nextLine = curLine - 1
    local isCircle = false
    if nextLine < 0  then
        nextLine = TotalLine
        isCircle = true
    end
    if (curLine == 4) and (curRow == 3 or curRow == 4) then
        nextLine = 1
    end
    local nextIdx = (nextLine - 1)*TotalColumn + curRow
    if self:IsValid(nextIdx) then
        return nextIdx,isCircle
    else
        nextIdx,isCircle = self:Up(nextIdx)
        return nextIdx,isCircle
    end
end

function LSKSelectStateClass:Down(idx)
    local curLine = math.floor((idx-1)/TotalLine + 1)
    local curRow = (idx-1)%TotalColumn + 1
    local nextLine = curLine + 1 
    local isCircle = false
    if nextLine > TotalLine then
        nextLine = 1
        isCircle = true
    end
    if (curLine == 1) and (curRow == 3 or curRow == 4) then
        nextLine = 4
    end
    local nextIdx = (nextLine - 1)*TotalColumn + curRow
    if self:IsValid(nextIdx) then
        return nextIdx,isCircle
    else
        nextIdx = self:Down(nextIdx)
        return nextIdx,isCircle
    end
end


function LSKSelectStateClass:Left(idx)
    local curLine = math.floor((idx-1)/TotalLine + 1)
    local curRow = (idx-1)%TotalColumn + 1
    
    local nextRow = curRow - 1 > 0 and (curRow - 1) or TotalColumn
    if (curLine == 2 or curLine == 3) and (curRow == 5) then
        nextRow = 2
    end
    local nextIdx = (curLine - 1)*TotalColumn + nextRow
    if self:IsValid(nextIdx) then
        return nextIdx
    else
        nextIdx = self:Left(nextIdx)
        return nextIdx
    end
end

function LSKSelectStateClass:Right(idx)
    local curLine = math.floor((idx-1)/TotalLine + 1)
    local curRow = (idx-1)%TotalColumn + 1
   
    local nextRow = curRow + 1 > TotalColumn and 1 or (curRow + 1)
    if (curLine == 2 or curLine == 3) and (curRow == 2) then
        nextRow = 5
    end
    local nextIdx = (curLine - 1)*TotalColumn + nextRow
    if self:IsValid(nextIdx) then
        return nextIdx
    else
        nextIdx = self:Right(nextIdx)
        return nextIdx
    end
end

function LSKSelectStateClass:IsValid(index)
    local valid = index >= 1 and index <= 36
    if valid then
        local realIdx = LSKUtil:GetLSKRealIdx(index)
        valid = LSKData:GetPrizeByIdx(realIdx) ~= nil
    end
    return valid
end

-- 玩家登录
function LSKSelectStateClass:OnNotifyPlayerLogin(data)
    -- 重新倒计时
    self._waitSeconds = LSKData.LoginTimeout
    self:CloseForm(self._scanStartForm)
    self._scanStartForm = false
    if self._selectForm then
        self._selectForm:vOnUpdateUI('On_Player_Login', Max_Wait_Seconds)
    end
end

function LSKSelectStateClass:OnNotifyPlayerBuyIn(data, result)
    if not data then
        return
    end
    if not LSKData:IsLogin() then
        return
    end
    
    local add = data.cur_buy_in - LSKData.User.DropCoin
    LSKData:SetDropCoin(data.cur_buy_in)
    local isEnough = data.cur_buy_in >= LSKData.Cost

    if isEnough then
        self:CloseForm(self._noEnoughCoinForm)
        self._noEnoughCoinForm = false
    end
end

function LSKSelectStateClass:OnRecvRelayMsg(data, result, cmd)
    if cmd == LSKNet.Cmd.cs_notify_m_player_login then
        self:OnNotifyPlayerLogin(data,result)
    elseif cmd == LSKNet.Cmd.cs_notify_m_player_buy_in then
        self:OnNotifyPlayerBuyIn(data,result)
    elseif cmd == LSKNet.Cmd.cs_rsp_start_lipstick then
        self:OnStartGameRsp(data, result)
    elseif cmd == LSKNet.Cmd.cs_notify_player_logout or cmd == LSKNet.Cmd.cs_rsp_player_logout then
        if result ~= LSKNetResult.OK then
            LSKUICommon:ShowBubble('状态不匹配，不允许退出')
            self:CloseForm(self._quitConfirmForm)
            self._quitConfirmForm = false
            return
        end
        self:CloseForm(self._noEnoughCoinForm)
        self._noEnoughCoinForm = false
        self:CloseForm(self._quitConfirmForm)
        self._quitConfirmForm = false
        self._waitSeconds = Max_Wait_Seconds

        if self._selectForm then
            self._selectForm:vOnUpdateUI('Select_Lipstick', 1)
            self._selectIdx = 1
            self._selectForm:Refresh()
        end
    end
end

function LSKSelectStateClass:OnStartGameRsp(msg,result)
    LSKUICommon:ShowWaiting(false)
    if result ~= LSKNetResult.OK then
        LSKUICommon:ShowBubble(LSKNetResultDesc[result] or ('Error '..result))
        return
    end
    local canGetPrize = msg.can_get_prize or false
    LogD('<color=yellow>是否开启作弊:</color>'.. (canGetPrize and '<color=white>否</color>' or '<color=red>是</color>'))

    LSKData:SetDropCoin(msg.left_buy_in)
    LSKData:SetGameCfgData(msg)
    -- 开始游戏 readyStart
    local gmMode = canGetPrize and LSKGameMode.Normal or LSKGameMode.Trick
    local prize = LSKData:GetPrizeByIdx(LSKUtil:GetLSKRealIdx(self._selectIdx))
    if not prize then
        LogE('口红选择错误！！！')
        return
    end
    LSKBoot:ChangeState('LSK_Play_LogicState', {gameMode=gmMode,prize=prize.PrizeInfo})
end

function LSKSelectStateClass:CloseForm(form)
    if form then
        if form.Close then
            form:Close()
        else
            form:Destroy()
        end
        form = false
    end
end