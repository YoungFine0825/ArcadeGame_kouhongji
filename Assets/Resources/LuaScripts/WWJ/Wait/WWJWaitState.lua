--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-07
*		描述： 娃娃机等待玩家状态
*********************************************************************--]]
local WWJWaitStateClass = DeclareClass("WWJWaitStateClass",ClassLib.LogicStateBaseClass)
--等待状态最大停留时间
local MAX_WAIT_SECONDS = 20
function WWJWaitStateClass:ctor()
   self._waitForm = false
end

function WWJWaitStateClass:vInitializeState()
    
end

function WWJWaitStateClass:vUninitializeState()
    if self._waitForm then
        self._waitForm:Destroy()
        self._waitForm = false
    end
    ScheduleService:RemoveTimer(self)
end

function WWJWaitStateClass:vGetName()

    return "WWJ_Wait_LogicState"
end


function WWJWaitStateClass:vOnStateEnter(param,oldSt)
    LogD("Enter WWJ Wait State")
    if not self._waitForm then
        self._waitForm = ClassLib.WWJWaitFormClass.new()
        self._waitForm:Create(nil,nil,0)
    end
    self:AddArcadeInputEvents()
    ScheduleService:AddTimer(self,self.OnJumpToSelectState,MAX_WAIT_SECONDS,false)
end


function WWJWaitStateClass:vOnStateLeave(param)
    ScheduleService:RemoveTimer(self)
    self:RemoveArcadeInputEvents()
    if self._waitForm then
        self._waitForm:Destroy()
        self._waitForm = false
    end
end


function WWJWaitStateClass:AddArcadeInputEvents()
    ArcadeInputService:RegisterOkInput(self,self.OnTriggerKey)
    ArcadeInputService:RegisterRefreshInput(self,self.OnTriggerKey)
    ArcadeInputService:RegisterRockerInput(self,self.OnTriggerRocker)
    ArcadeInputService:RegisterRotateInput(self,self.OnTriggerRocker)
end

function WWJWaitStateClass:RemoveArcadeInputEvents()
    ArcadeInputService:UnRegisterOkInput(self)
    ArcadeInputService:UnRegisterRefreshInput(self)
    ArcadeInputService:UnRegisterRockerInput(self)
    ArcadeInputService:UnRegisterRotateInput(self)
end

function WWJWaitStateClass:OnJumpToSelectState()
    WWJBoot:ChangeState("WWJ_Select_LogicState")
end

function WWJWaitStateClass:OnTriggerKey(eventType)
    if eventType ~= ArcadeInput.PressEvent.PressClick then
        return
    end
    WWJBoot:ChangeState('WWJ_Select_LogicState', nil)
end

function WWJWaitStateClass:OnTriggerRocker()
    WWJBoot:ChangeState('WWJ_Select_LogicState', nil)

end

function WWJWaitStateClass:OnRecvRelayMsg(data,result,cmd)

end


