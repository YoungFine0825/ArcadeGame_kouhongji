--[[********************************************************************
*		作者： yangfan
*		时间： 2018-11-19
*		描述： 口红机播放广告状态
*********************************************************************--]]

local LSKAdStateClass = DeclareClass("LSKAdStateClass")

function LSKAdStateClass:ctor( )
    self._waitSeconds = 180
end

function LSKAdStateClass:vInitializeState( )
    -- body
    ScheduleService:AddTimer(self,self.OnTimerUpdate,1,true)
end

function LSKAdStateClass:vUninitializeState()
    ScheduleService:RemoveTimer(self,self.OnTimerUpdate)
    ADSystem:Close()
end

function LSKAdStateClass:vGetName( )
    -- body
    return "LSK_Ad_LogicState"
end

function LSKAdStateClass:vOnStateEnter(param,oldSt)
    LogD("<color=lime>Enter Ad State</color>")
    LSKAudio:StopMusic()

    ArcadeInputService:RegisterOkInput(self,self.OnArcadeBtnInput)
    ArcadeInputService:RegisterRefreshInput(self,self.OnArcadeBtnInput)
    ArcadeInputService:RegisterRockerInput(self,self.OnArcadeRockerInput)
    ArcadeInputService:RegisterRotateInput(self,self.OnArcadeRockerInput)
    --
    ADSystem:Play(self,self.OnPlayAdListFinished)
end

function LSKAdStateClass:vOnStateLeave(param)
    LSKAudio:PlayMusic()
    ArcadeInputService:UnRegisterOkInput(self)
    ArcadeInputService:UnRegisterRefreshInput(self)
    ArcadeInputService:UnRegisterRockerInput(self)
    ArcadeInputService:UnRegisterRotateInput(self)
    --
    ADSystem:Close()

end

--触发机器摇杆事件
function LSKAdStateClass:OnArcadeRockerInput(args)
    LSKBoot:ChangeState('LSK_Select_LogicState', nil)
end

--触发机器按钮事件
function LSKAdStateClass:OnArcadeBtnInput(args)
    if args ~= ArcadeInput.PressEvent.PressClick then
        return
    end
    LSKBoot:ChangeState('LSK_Select_LogicState', nil)
end

--广告列表播放完成
function LSKAdStateClass:OnPlayAdListFinished()
    LSKBoot:ChangeState('LSK_Select_LogicState', nil)
end

function LSKAdStateClass:OnRecvRelayMsg(data, result, cmd)
    --Nothing
end

function LSKAdStateClass:OnTimerUpdate()
    if LSKBoot:IsCanPlayAds() then
        self._waitSeconds = self._waitSeconds - 1
        if self._waitSeconds <= 0 then
            LogD('Play Ads. Next Wait : '..self._waitSeconds)
            LSKBoot:ChangeState('LSK_Ad_LogicState')
            self._waitSeconds = LSKUtil:GetRandom(100, 200)
        end
    else

    end
end