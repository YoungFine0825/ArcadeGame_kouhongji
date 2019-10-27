--[[********************************************************************
*		作者： XH
*		时间： 2018-12-11
*		描述： 演示模式
*********************************************************************--]]

local LSKPresentationStateClass = DeclareClass("LSKPresentationStateClass")


local Min_Throw_Duration = 0.2 -- 自动扔飞刀最小时间间隔
local Max_Throw_Duration = 0.8 -- 自动扔飞刀最大时间间隔

local Min_Stay_Seconds   = 60 -- 最小停留时间

function LSKPresentationStateClass:ctor()
    self._end_level_arg = false
    self._update_interval = 0.1

    self._ready_form = false
    self._can_throw = false
    self._throw_duration = 1
    self._voiceSeconds = 15
    self._elapsedSeconds = 0
    self._continue = false
end

function LSKPresentationStateClass:vInitializeState()
    
end

function LSKPresentationStateClass:vUninitializeState()
    self:Leave()
end

function LSKPresentationStateClass:vGetName()
    return "LSK_Presentation_State"
end


function LSKPresentationStateClass:vOnStateEnter(param,oldSt)
    self._elapsedSeconds = 0
    self._voiceSeconds = LSKUtil:GetRandom(LSKPickVoiceMin,LSKPickVoiceMax)
    self._can_throw = false
    self._throw_duration = LSKUtil:GetFloatRandom(Min_Throw_Duration, Max_Throw_Duration)

    LogD("Enter LSK Presentation State!");

    self:AddInputEvents()
    ScheduleService:AddTimer(self,self.OnTimerUpdate,self._update_interval,true)

    LSKGame:DealOpCmd(LSKGameOpCmd.ShowScene)

    self:Begin()
end
--开始游戏
--演示模式
function LSKPresentationStateClass:Begin()
    ScheduleService:RemoveTimer(self,self.Begin)
    if not self._ready_form then
        self._ready_form = ClassLib.LSKReadyStartFormClass.new()
        local level = self._continue and self._end_level_arg.level or 1
        self._ready_form:Create(nil,level)
    end
    ScheduleService:AddTimer(self,self.StartGame,3,false)
end

-------------------------------------
-- 演示模式驱动
-------------------------------------
function LSKPresentationStateClass:OnTimerUpdate()
    if not self._can_throw then
        return
    end
    -- 自动扔飞刀
    self._throw_duration = self._throw_duration - self._update_interval
    if self._throw_duration <= 0 then
        LSKGame:DealOpCmd(LSKGameOpCmd.Throw, nil)
        self._throw_duration = LSKUtil:GetFloatRandom(Min_Throw_Duration, Max_Throw_Duration)
    end

    -- 邀请音效播放逻辑
    self._voiceSeconds = self._voiceSeconds or 0
    self._voiceSeconds = self._voiceSeconds - self._update_interval
    if self._voiceSeconds <= 0 then
	    AudioService:Play(AudioChannelType.ACT_UI, 'LSK/Audio/Voice_7', false)
        self._voiceSeconds = LSKUtil:GetRandom(LSKPickVoiceMin,LSKPickVoiceMax)
    end

    self._elapsedSeconds = self._elapsedSeconds + self._update_interval
end


function LSKPresentationStateClass:vOnStateLeave(param)
    self:Leave()
end

function LSKPresentationStateClass:Leave()
    if self._ready_form then
        self._ready_form:Destroy()
        self._ready_form = false
    end
    self._continue = false
    self._elapsedSeconds = 0
    ScheduleService:RemoveTimer(self)
    self:RemoveInputEvents()
    if LSKGame then
        LSKGame:DealOpCmd(LSKGameOpCmd.StopRotate)
        LSKGame:DealOpCmd(LSKGameOpCmd.Over,{stop=true})
        LSKGame:DealOpCmd(LSKGameOpCmd.HideScene)
        LSKAudio:StopSound()
    end
end


function LSKPresentationStateClass:AddInputEvents()
    ArcadeInputService:RegisterOkInput(self,self.OnTriggerKey)
    ArcadeInputService:RegisterRefreshInput(self,self.OnTriggerKey)
    ArcadeInputService:RegisterRockerInput(self,self.OnTriggerKey)
    ArcadeInputService:RegisterRotateInput(self,self.OnTriggerKey)
end

function LSKPresentationStateClass:RemoveInputEvents()
    ArcadeInputService:UnRegisterOkInput(self)
    ArcadeInputService:UnRegisterRefreshInput(self)
    ArcadeInputService:UnRegisterRockerInput(self)
    ArcadeInputService:UnRegisterRotateInput(self)
end

function LSKPresentationStateClass:OnTriggerKey()
    LSKBoot:ChangeState('LSK_Select_LogicState', nil)
end

function LSKPresentationStateClass:OnRecvRelayMsg(data, result, cmd)

end

function LSKPresentationStateClass:OnLevelEnd(level, isCurLevelPass, isFinalWin)
    self._can_throw = false
    self._end_level_arg = {level=level,isCurLevelPass=isCurLevelPass,isFinalWin=isFinalWin}
    ScheduleService:AddTimer(self,self.OnDelayChangeState,1.5,false)
end

function LSKPresentationStateClass:OnDelayChangeState()
    if not self._end_level_arg then
        LSKBoot:ChangeState('LSK_Select_LogicState')
        return
    end
    if self._elapsedSeconds >= Min_Stay_Seconds then
        LSKBoot:ChangeState('LSK_Select_LogicState')
        return
    end
    ScheduleService:RemoveTimer(self,self.OnDelayChangeState)
    local isFinalEnd = self._end_level_arg.isFinalWin or (not self._end_level_arg.isCurLevelPass)
    if isFinalEnd then
        if self._end_level_arg.isFinalWin then
            LSKGame:DealOpCmd(LSKGameOpCmd.Over,{stop=true})
            LSKBoot:ChangeState('LSK_Select_LogicState')
        else
            self._continue = LSKUtil:GetRandom(1,2) == 1 and true or false
             
            self:Begin()
        end
    else
        if self._end_level_arg.level < 3 then
            ScheduleService:AddTimer(self,self.Ready,1.5,false)
        else
            LSKBoot:ChangeState('LSK_Select_LogicState')
        end
    end
end

function LSKPresentationStateClass:Ready()
    ScheduleService:RemoveTimer(self,self.Ready)
    if not self._ready_form then
        self._ready_form = ClassLib.LSKReadyStartFormClass.new()
        self._ready_form:Create(nil,self._end_level_arg.level+1)
    end
    ScheduleService:AddTimer(self,self.NextLevel,3,false)
end

function LSKPresentationStateClass:StartGame()
    self._can_throw = true
    ScheduleService:RemoveTimer(self,self.StartGame)
    if self._ready_form then
        self._ready_form:Destroy()
        self._ready_form = false
    end
    if not LSKData._lskList or #LSKData._lskList < 1 then
        
        return
    end
    local prize = LSKData:GetPrizeByIdx(LSKUtil:GetRandom(1,#LSKData._lskList))
    if prize ~= nil then
        local cmd = self._continue and LSKGameOpCmd.Replay or LSKGameOpCmd.Start
        LSKGame:DealOpCmd(cmd,{delegate=self,gameMode=LSKGameMode.Simulate,prize=prize.PrizeInfo})
        self._continue = false
        return
    end
    
    LSKBoot:ChangeState('LSK_Select_LogicState')
end

function LSKPresentationStateClass:NextLevel()
    ScheduleService:RemoveTimer(self,self.NextLevel)
    self._can_throw = true
    if self._ready_form then
        self._ready_form:Destroy()
        self._ready_form = false
    end
    LSKGame:DealOpCmd(LSKGameOpCmd.NextLevel,{delegate=self,gameMode=LSKGameMode.Simulate})
end
