--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-07
*		描述： 娃娃机玩家玩 状态
*********************************************************************--]]
local WWJPlayStateClass = DeclareClass("WWJPlayStateClass",ClassLib.LogicStateBaseClass)
local MAX_WAIT_SECONDS = 30
function WWJPlayStateClass:ctor()
   self._playingForm = false
   self._waitSeconds = 0
end

function WWJPlayStateClass:vInitializeState()
    
   

end

function WWJPlayStateClass:vUninitializeState()


end

function WWJPlayStateClass:vGetName()

    return "WWJ_Play_LogicState"
end


function WWJPlayStateClass:vOnStateEnter(param,oldSt)

    LogD("-------------->WWJ Enter Play Logic State<---------------")
    self:ListenInput(true)
    --
    local delegate = {
        obj = self,
        func = self.OnWWJGameEventCallback
    }
    WWJGame:DealOpCmd(WWJGameOpCmd.Start,{Delegate = delegate,IsCheat=false})
    if not self._playingForm then
        self._playingForm = ClassLib.WWJPlayingFormClass.new()
        self._playingForm:Create()
        --开始计时
        self._waitSeconds = MAX_WAIT_SECONDS
        self._playingForm:UpdateUI("UpdateCntDownText",self._waitSeconds)
        ScheduleService:AddTimer(self,self.OnGameCntDownUpdate,1,true)
    end
end


function WWJPlayStateClass:vOnStateLeave(param)

    LogD("-------------->WWJ Leave Play Logic State<---------------")
    self:ListenInput(false)
    WWJGame:DealOpCmd(WWJGameOpCmd.Stop)
    if self._playingForm then
        self._playingForm:Destroy()
        self._playingForm = false
    end
end

--游戏倒计时
function WWJPlayStateClass:OnGameCntDownUpdate()
    self._waitSeconds = self._waitSeconds - 1
    if self._waitSeconds < 0 then
        --计时结束，自动放下
        ScheduleService:RemoveTimer(self)
        WWJGame:DealOpCmd(WWJGameOpCmd.ClawDown)
    else
        self._playingForm:UpdateUI("UpdateCntDownText",self._waitSeconds)
    end
end

--
function WWJPlayStateClass:OnWWJGameEventCallback(eventType,args)
    if eventType == WWJGameEventType.OnCatchEnd then--抓娃娃结束
        return WWJBoot:ChangeState("WWJ_GameOver_LogicState",args)
    end
end

--监听硬件输入
function WWJPlayStateClass:ListenInput(isListen)
    
    if isListen then

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
function WWJPlayStateClass:OnArcadeInputOk(pEvent)
    --取消计时
    ScheduleService:RemoveTimer(self)
    self._playingForm:UpdateUI("UpdateCntDownText","")
    --
    WWJGame:DealOpCmd(WWJGameOpCmd.ClawDown)
end

function WWJPlayStateClass:OnArcadeInputRefresh(pEvent)

    if pEvent==ArcadeInput.PressEvent.PressClick then
        WWJGame:DealOpCmd(WWJGameOpCmd.RefreshDoll)
    end

end


--命令映射
local ArcadeInput2OpCmd = {

    [ArcadeInput.RockerState.RockerMoveForward]=WWJGameOpCmd.RockerMoveForward,
    [ArcadeInput.RockerState.RockerMoveRight]=WWJGameOpCmd.RockerMoveRight,
    [ArcadeInput.RockerState.RockerMoveMiddle]=WWJGameOpCmd.RockerMoveMiddle,
    [ArcadeInput.RockerState.RockerMoveLeft]=WWJGameOpCmd.RockerMoveLeft,
    [ArcadeInput.RockerState.RockerMoveBack]=WWJGameOpCmd.RockerMoveBack,
}

--摇杆
function WWJPlayStateClass:OnArcadeInputRocker(rst)
    
    local opCmd = ArcadeInput2OpCmd[rst]
    if opCmd then
        WWJGame:DealOpCmd(opCmd)
    end
    
end

function WWJPlayStateClass:OnArcadeInputRotate(angle)
    
    WWJGame:DealOpCmd(WWJGameOpCmd.RotateMoveViewPort,angle)

end








