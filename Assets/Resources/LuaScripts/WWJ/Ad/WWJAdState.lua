--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-12-03
*		描述： 娃娃机广告状态
*********************************************************************--]]
local WWJAdStateClass = DeclareClass("WWJAdStateClass",ClassLib.LogicStateBaseClass)

function WWJAdStateClass:ctor()
   self._waitSeconds = 150
end

function WWJAdStateClass:vInitializeState()
    ScheduleService:AddTimer(self,self.OnJumpToAdState,self._waitSeconds,false)
end

function WWJAdStateClass:vUninitializeState()
    ScheduleService:RemoveTimer(self)
    ADSystem:Close()
end

function WWJAdStateClass:vGetName()

    return "WWJ_Ad_LogicState"
end


function WWJAdStateClass:vOnStateEnter(param,oldSt)

    LogD("Enter Ad State")

    ArcadeInputService:RegisterOkInput(self,self.OnArcadeBtnInput)
    ArcadeInputService:RegisterRefreshInput(self,self.OnArcadeBtnInput)
    ArcadeInputService:RegisterRockerInput(self,self.OnArcadeRockerInput)
    ArcadeInputService:RegisterRotateInput(self,self.OnArcadeRockerInput)
    --
    ADSystem:Play(self,self.OnPlayAdListFinished)
end


function WWJAdStateClass:vOnStateLeave(param)
    ArcadeInputService:UnRegisterOkInput(self)
    ArcadeInputService:UnRegisterRefreshInput(self)
    ArcadeInputService:UnRegisterRockerInput(self)
    ArcadeInputService:UnRegisterRotateInput(self)
    --
    ADSystem:Close()
end

--触发机器摇杆事件
function WWJAdStateClass:OnArcadeRockerInput(args)
    WWJBoot:ChangeState('WWJ_Select_LogicState', nil)
end

--触发机器按钮事件
function WWJAdStateClass:OnArcadeBtnInput(args)
    if args ~= ArcadeInput.PressEvent.PressClick then
        return
    end
    WWJBoot:ChangeState('WWJ_Select_LogicState', nil)
end

--广告列表播放完成
function WWJAdStateClass:OnPlayAdListFinished()
    ScheduleService:AddTimer(self,self.OnJumpToAdState,self._waitSeconds,false)
    WWJBoot:ChangeState('WWJ_Wait_LogicState', nil)
end

function WWJAdStateClass:OnRecvRelayMsg(data, result, cmd)
    --Nothing
end

function WWJAdStateClass:OnJumpToAdState()
    LogD('Play Ads. Next Wait : '..self._waitSeconds)
    self._waitSeconds = math.random(100, 200)
    if WWJBoot:IsCanPlayAds() then
        return WWJBoot:ChangeState('WWJ_Ad_LogicState')
    else
        ScheduleService:RemoveTimer(self)
        return ScheduleService:AddTimer(self,self.OnJumpToAdState,self._waitSeconds,false)
    end
end


