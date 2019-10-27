--[[********************************************************************
*		作者： yangfan
*		时间： 2018-12-12
*		描述： 准备状态
*********************************************************************--]]

local WWJReadyStateClass = DeclareClass("WWJReadyStateClass",ClassLib.LogicStateBaseClass)

function WWJReadyStateClass:ctor()
   self._readyForm = false
   self._waitSeconds = 0
end

function WWJReadyStateClass:vInitializeState()
    
   

end

function WWJReadyStateClass:vUninitializeState()


end

function WWJReadyStateClass:vGetName()

    return "WWJ_Ready_LogicState"
end


function WWJReadyStateClass:vOnStateEnter(param,oldSt)

    LogD("-------------->WWJ Enter Ready Logic State<---------------")
    --刷新娃娃位置
    WWJGame:DealOpCmd(WWJGameOpCmd.RefreshDoll)
    if not self._readyForm then
        self._readyForm = ClassLib.WWJReadyFormClass.new()
        self._readyForm:Create(nil,nil,0)
        self._waitSeconds = 1
        self._readyForm:UpdateUI("UpdateCntDown",1)
        ScheduleService:AddTimer(self,self.OnTimerUpdate,1,true)
        
    end
    
end


function WWJReadyStateClass:vOnStateLeave(param)

    LogD("-------------->WWJ Leave Ready Logic State<---------------")
    if self._readyForm then
        self._readyForm:Destroy()
        self._readyForm = false
    end
end

function WWJReadyStateClass:OnTimerUpdate()
    self._waitSeconds = self._waitSeconds + 1
    if self._waitSeconds <= 4 then
        self._readyForm:UpdateUI("UpdateCntDown",self._waitSeconds)
    else
        ScheduleService:RemoveTimer(self)
        WWJBoot:ChangeState("WWJ_Play_LogicState")
    end
end