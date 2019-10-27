--[[********************************************************************
*		作者： yangfan
*		时间： 2018-12-13
*		描述： 游戏结束状态
*********************************************************************--]]

local WWJGameOverStateClass = DeclareClass("WWJGameOverStateClass",ClassLib.LogicStateBaseClass)

function WWJGameOverStateClass:ctor()
    self._failedForm = false
    self._successForm = false
    self._continueForm = false
end

function WWJGameOverStateClass:vInitializeState()
end

function WWJGameOverStateClass:vUninitializeState()

end

function WWJGameOverStateClass:vGetName()

    return "WWJ_GameOver_LogicState"
end


function WWJGameOverStateClass:vOnStateEnter(param,oldSt)

    LogD("Enter GameOver State")
    if not param then
        --失败
        if not self._failedForm then
            self._failedForm = ClassLib.WWJFailedFormClass.new()
            self._failedForm:Create(nil,nil,0)
        end
        ScheduleService:AddTimer(self,self.DoCreateContinueForm,1,false)
    else
        --成功
        --摄像机对准出口处
        WWJGame:DealOpCmd(WWJGameOpCmd.CameraLookAtExit)
        LogD("<color=yellow>"..param.RootTf.name.."</color>")
        --1秒钟后弹出成功弹窗
        ScheduleService:AddTimer(self,self.DoCreateSuccessForm,1,false)
    end
end


function WWJGameOverStateClass:vOnStateLeave(param)
    --摄像机位置复原
    WWJGame:DealOpCmd(WWJGameOpCmd.RevertCameraPosition)
    --
    if self._successForm then
        self._successForm:Destroy()
        self._successForm = false
    end
    if self._failedForm then
        self._failedForm:Destroy()
        self._failedForm = false
    end
end

function WWJGameOverStateClass:DoCreateSuccessForm()
    ScheduleService:RemoveTimer(self,self.DoCreateSuccessForm)
    if not self._successForm then
        self._successForm = ClassLib.WWJSuccessFormClass.new()
        self._successForm:Create(nil,nil,0)
    end
    --2秒后跳转至选择状态
    ScheduleService:AddTimer(self,self.JumpToSelectState,2,false)
end

function WWJGameOverStateClass:DoCreateContinueForm()
    ScheduleService:RemoveTimer(self,self.DoCreateContinueForm)
    if not self._continueForm then
        self._continueForm = ClassLib.WWJContinueFormClass.new()
        self._continueForm:DoCreate(self,self.JumpToReadyState,self.JumpToSelectState)
        self:EnableArcadeInput(true)
    end
end

function WWJGameOverStateClass:JumpToReadyState()
    if self._continueForm then
        self._continueForm:DoDestroy()
        self._continueForm = false
        self:EnableArcadeInput(false)
    end
    WWJBoot:ChangeState("WWJ_Ready_LogicState")
end

function WWJGameOverStateClass:JumpToSelectState()
    if self._continueForm then
        self._continueForm:DoDestroy()
        self._continueForm = false
        self:EnableArcadeInput(false)
    end
    ScheduleService:RemoveTimer(self,self.JumpToSelectState)
    WWJBoot:ChangeState("WWJ_Select_LogicState")
end

--监听硬件输入
function WWJGameOverStateClass:EnableArcadeInput(enable)
    
    if enable then

        ArcadeInputService:RegisterOkInput(self,self.OnArcadeInputOk)
        ArcadeInputService:RegisterRockerInput(self,self.OnArcadeInputRocker)
        ArcadeInputService:RegisterRotateInput(self,self.OnArcadeInputRotate)
        ArcadeInputService:RegisterRefreshInput(self,self.OnArcadeInputRefresh)

    else

        ArcadeInputService:UnRegisterOkInput(self)
        ArcadeInputService:UnRegisterRockerInput(self)
        ArcadeInputService:UnRegisterRotateInput(self)
        ArcadeInputService:UnRegisterRefreshInput(self)

    end
    
end

--OK 点击
function WWJGameOverStateClass:OnArcadeInputOk(pEvent)
    if pEvent ~= ArcadeInput.PressEvent.PressClick then
        return
    end
    if self._continueForm then
        self._continueForm:UpdateUI("OnArcadeOkBtnClick")
    end
end

function WWJGameOverStateClass:OnArcadeInputRefresh(pEvent)

end

--摇杆
function WWJGameOverStateClass:OnArcadeInputRocker(rst)
    if self._continueForm then
        self._continueForm:UpdateUI("OnArcadeRockerInput",rst)
    end
end

function WWJGameOverStateClass:OnArcadeInputRotate(angle)

end