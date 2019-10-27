--[[********************************************************************
*		作者： XH
*		时间： 2018-12-12
*		描述： 试玩模式,允许玩家最多玩两关
*********************************************************************--]]

local LSKTrialStateClass = DeclareClass("LSKTrialStateClass")

function LSKTrialStateClass:ctor()
    self._end_level_arg = false
    self._ready_form = false
    self._selectPrize = false
end

function LSKTrialStateClass:vInitializeState()
    
end

function LSKTrialStateClass:vUninitializeState()

end

function LSKTrialStateClass:vGetName()
    return "LSK_Trial_State"
end

function LSKTrialStateClass:vOnStateEnter(param,oldSt)
    if not param then
        LogE('Bad Param When Change LSKState : '..self:vGetName())
        return
    end

    LogD("Enter LSK Presentation State!")
    self._selectPrize = param.prize
    self:AddInputEvents()
    --开始游戏
    if not self._ready_form then
        self._ready_form = ClassLib.LSKReadyStartFormClass.new()
        self._ready_form:Create(nil,1)
    end

    ScheduleService:AddTimer(self,self.StartGame,3,false)
    LSKGame:DealOpCmd(LSKGameOpCmd.ShowScene)
    LSKBoot.PlayerHud:Refresh()
end

function LSKTrialStateClass:vOnStateLeave(param)
    self:RemoveInputEvents()
    if self._ready_form then
        self._ready_form:Destroy()
        self._ready_form = false
    end

    ScheduleService:RemoveTimer(self)
    
    LSKGame:DealOpCmd(LSKGameOpCmd.StopRotate)
    LSKGame:DealOpCmd(LSKGameOpCmd.Over, {stop=true})
    LSKGame:DealOpCmd(LSKGameOpCmd.HideScene)
    LSKAudio:StopSound()
    LSKBoot.PlayerHud:Refresh()
end

function LSKTrialStateClass:AddInputEvents()
    ArcadeInputService:RegisterOkInput(self,self.OnTriggerOK)
    ArcadeInputService:RegisterRefreshInput(self,self.OnTriggerKey)
    ArcadeInputService:RegisterRockerInput(self,self.OnTriggerKey)
    ArcadeInputService:RegisterRotateInput(self,self.OnTriggerKey)
end

function LSKTrialStateClass:RemoveInputEvents()
    ArcadeInputService:UnRegisterOkInput(self)
    ArcadeInputService:UnRegisterRefreshInput(self)
    ArcadeInputService:UnRegisterRockerInput(self)
    ArcadeInputService:UnRegisterRotateInput(self)
end

function LSKTrialStateClass:OnTriggerOK(eventType)
    if eventType ~= ArcadeInput.PressEvent.PressBegin then
        return
    end
    LSKGame:DealOpCmd(LSKGameOpCmd.Throw, nil)
end

function LSKTrialStateClass:OnTriggerKey()
    
end

-- 接收网络转播消息
function LSKTrialStateClass:OnRecvRelayMsg(data, result, cmd)

end

-- 当前关结束回调
function LSKTrialStateClass:OnLevelEnd(level, isCurLevelPass, isFinalWin)
    self._end_level_arg = {level=level,isCurLevelPass=isCurLevelPass,isFinalWin=isFinalWin}
    ScheduleService:AddTimer(self,self.OnDelayChangeState,1.5,false)
end

-- 当前关卡结束，游戏状态切换
function LSKTrialStateClass:OnDelayChangeState()
    ScheduleService:RemoveTimer(self,self.OnDelayChangeState)
    -- 只能试玩两关
    local toNextLevel = self._end_level_arg.isCurLevelPass and self._end_level_arg.level == 1
    if toNextLevel then
        AudioService:Play(AudioChannelType.ACT_UI, 'LSK/Audio/Voice_10', false)
        ScheduleService:AddTimer(self,self.Ready,1.5,false)
    else
        LSKGame:DealOpCmd(LSKGameOpCmd.Over,{stop=true})
        LSKUICommon:ShowBubble('体验结束')
        ScheduleService:AddTimer(self,self.DelayToSelectState,3,false)
    end
end

-- 准备 3 2 1 
function LSKTrialStateClass:Ready()
    ScheduleService:RemoveTimer(self,self.Ready)
    if not self._ready_form then
        self._ready_form = ClassLib.LSKReadyStartFormClass.new()
        self._ready_form:Create(nil,self._end_level_arg.level+1)
    end
    ScheduleService:AddTimer(self,self.NextLevel,3,false)
end

-- 开始游戏
function LSKTrialStateClass:StartGame()
    ScheduleService:RemoveTimer(self,self.StartGame)
    if self._ready_form then
        self._ready_form:Destroy()
        self._ready_form = false
    end
    LSKGame:DealOpCmd(LSKGameOpCmd.Start,{delegate=self,gameMode=LSKGameMode.Trial,prize=self._selectPrize})
end

-- 下一关
function LSKTrialStateClass:NextLevel()
    ScheduleService:RemoveTimer(self,self.NextLevel)
    if self._ready_form then
        self._ready_form:Destroy()
        self._ready_form = false
    end
    LSKGame:DealOpCmd(LSKGameOpCmd.NextLevel,{delegate=self,gameMode=LSKGameMode.Simulate})
end

function LSKTrialStateClass:DelayToSelectState()
    ScheduleService:RemoveTimer(self,self.DelayToSelectState)
    LSKBoot:ChangeState('LSK_Select_LogicState')
end
