--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-12-03
*		描述： 娃娃机等待玩家状态
*********************************************************************--]]

local WWJSelectStateClass = DeclareClass("WWJSelectStateClass",ClassLib.LogicStateBaseClass)
local MAX_WAIT_SECONDS = 120
function WWJSelectStateClass:ctor()
   self._selectForm = false
   self._exitForm = false
   self._waitSeconds = 0
end

function WWJSelectStateClass:vInitializeState()
    
   

end

function WWJSelectStateClass:vUninitializeState()
    ScheduleService:RemoveTimer(self)
end

function WWJSelectStateClass:vGetName()

    return "WWJ_Select_LogicState"
end


function WWJSelectStateClass:vOnStateEnter(param,oldSt)

    LogD("Enter WWJ Select State")
    if not self._selectForm then
        self._selectForm = ClassLib.WWJSelectFormClass.new()
        self._selectForm:SetSelectedDollHandler(self,self.OnSelectedDoll)
        self._selectForm:Create(nil,nil,0)
    end
    --开启倒计时
    self._waitSeconds = MAX_WAIT_SECONDS
    self._selectForm:UpdateUI("UpdateCntDown",self._waitSeconds)
    ScheduleService:AddTimer(self, self.OnUpdateCntDownTimer, 1, true)
    --
    self:EnableArcadeInput(true)
end


function WWJSelectStateClass:vOnStateLeave(param)
    ScheduleService:RemoveTimer(self)
    --
    self:EnableArcadeInput(false)
   if self._selectForm then
    self._selectForm:Destroy()
    self._selectForm = false
   end
   self:DoCloseExitForm()
end

function WWJSelectStateClass:EnableArcadeInput(enable)
    if enable then
        ArcadeInputService:RegisterOkInput(self,self.OnArcadeOkBtnClick)
        ArcadeInputService:RegisterRefreshInput(self,self.OnArcadeRefreshBtnClick)
        ArcadeInputService:RegisterRockerInput(self,self.OnArcadeRockerInput)
        ArcadeInputService:RegisterRotateInput(self,self.OnArcadeRotateInput)
    else
        ArcadeInputService:UnRegisterOkInput(self)
        ArcadeInputService:UnRegisterRefreshInput(self)
        ArcadeInputService:UnRegisterRockerInput(self)
        ArcadeInputService:UnRegisterRotateInput(self)
    end
end

function WWJSelectStateClass:OnUpdateCntDownTimer()
    self._waitSeconds = self._waitSeconds - 1
    if self._waitSeconds < 0 then
        ScheduleService:RemoveTimer(self,self.OnUpdateCntDownTimer)
        WWJBoot:ChangeState("WWJ_Wait_LogicState")
    else
        self._selectForm:UpdateUI("UpdateCntDown",self._waitSeconds)
    end
end

function WWJSelectStateClass:OnSelectedDoll(index)
    
end

--询问是否退出的弹窗--
function WWJSelectStateClass:DoCreateExitForm()
    if not self._exitForm then
        self._exitForm = ClassLib.WWJExitFormClass.new()
        self._exitForm:DoCreate(self,self.OnJumpToWaitState,self.DoCloseExitForm)
    end
end

function WWJSelectStateClass:DoCloseExitForm()
    if self._exitForm then
        self._exitForm:DoDestroy()
        self._exitForm = false
    end
end

function WWJSelectStateClass:OnJumpToWaitState()
    WWJBoot:ChangeState("WWJ_Wait_LogicState")
end
-----

function WWJSelectStateClass:OnArcadeRockerInput(args)
    if not self._exitForm then
        self._selectForm:UpdateUI("OnArcadeRockerInput",args)
    else
        self._exitForm:UpdateUI("OnArcadeRockerInput",args)
    end
    
end

function WWJSelectStateClass:OnArcadeOkBtnClick(eventType)
    if eventType ~= ArcadeInput.PressEvent.PressClick then
        return
    end
    if self._exitForm then
        self._exitForm:UpdateUI("OnArcadeOkBtnClick")
    else
        WWJBoot:ChangeState("WWJ_Ready_LogicState")
    end
end

function WWJSelectStateClass:OnArcadeRefreshBtnClick(eventType)
    if eventType ~= ArcadeInput.PressEvent.PressClick then
        return
    end
    --创建窗口，询问是否退出
    self:DoCreateExitForm()
end

function WWJSelectStateClass:OnArcadeRotateInput(angle)
end


