--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-06
*		描述： 娃娃机游戏本省总控
*********************************************************************--]]

--定义娃娃机本身接收的操作控制命令
WWJGameOpCmd={

    --无操作命令
    None=0,
    --开始
    Start=1, --{IsCheat=?}
    --停止
    Stop=2,
    --刷新公仔
    RefreshDoll=3,--参数{}
    --摇杆操作命令
    RockerMoveMiddle=1000,
    RockerMoveRight=1001,
    RockerMoveLeft=1002,
    RockerMoveForward=1003,
    RockerMoveBack=1004,
    --旋转加移动视口
    RotateMoveViewPort=2000, --{}
    --下去抓
    ClawDown=2001,
    --摄像对准出口
    CameraLookAtExit = 2002,
    --复原摄像机位置
    RevertCameraPosition = 2003 

}

--娃娃机游戏事件
WWJGameEventType = {
    OnCatchStart = 1,
    OnCatchEnd = 2
}

local WWJGameClass = DeclareClass("WWJGameClass")
--
function WWJGameClass:ctor()
    
    self._env=false
    self._machine=false
    self._claw=false
    self._camera=false

    self._isStarted=false
    --
    self._catchedDoll = false
    self._delegate = false
end
--
function WWJGameClass:Initialize()
    
    self._env=ClassLib.WWJEnvClass.new()
    self._env:Init()
 
    self._camera=ClassLib.WWJCameraClass.new()
    self._camera:Init()
 
    self._machine=ClassLib.WWJMachineClass.new()
    self._machine:Init(self)

end

function WWJGameClass:UnInitialize(  )
    ScheduleService:RemoveTimer(self)

    if self._catchedDoll then
        self._catchedDoll = false
    end
    
    self._env:UnInit()
    self._env=false

    self._camera:UnInit()
    self._camera=false

    self._machine:UnInit()
    self._machine=false

end


--逻辑更新
function WWJGameClass:LogicUpdate(delta)

    self._machine:LogicUpdate(delta)
    self._env:LogicUpdate(delta)
    self._camera:LogicUpdate(delta)

end

--处理玩家操作命令
function WWJGameClass:DealOpCmd(opCmd,arg)
    
    if opCmd==WWJGameOpCmd.Start then
        LogD("WWJGame Start")
        if arg and arg.Delegate then
            self._delegate = arg.Delegate
        end
        self._machine:DealOpCmd(opCmd,arg)  
        self._isStarted=true
        return
    elseif opCmd == WWJGameOpCmd.RefreshDoll then
        if self._machine then
            self._machine:DealOpCmd(opCmd,arg) 
        end
    end

    if not self._isStarted then
        LogE("WWJGame Not Start DealOpCmd")
        return
    end

    --机器处理
    if self._machine then
        --摇杆映射
        if opCmd==WWJGameOpCmd.RockerMoveForward or 
            opCmd==WWJGameOpCmd.RockerMoveBack or
            opCmd==WWJGameOpCmd.RockerMoveRight or
            opCmd==WWJGameOpCmd.RockerMoveLeft then
                
            local realCmd=self:DealRockerCmdMap(opCmd)
            self._machine:DealOpCmd(realCmd,arg)     

        else

            self._machine:DealOpCmd(opCmd,arg)     
        end
    end

    --相机控制
    if opCmd==WWJGameOpCmd.RotateMoveViewPort then
        if self._camera then
            self._camera:DealOpCmd(opCmd,arg)
        end
    elseif opCmd == WWJGameOpCmd.CameraLookAtExit then
        if self._camera then
            self._camera:LookAtExit()
        end
    elseif opCmd == WWJGameOpCmd.RevertCameraPosition then
        if self._camera then
            self._camera:RevertPosition()
        end
    end

end

--处理摇杆命令映射 根据当前相机的方位处理
function WWJGameClass:DealRockerCmdMap(opCmd)
    
    local ret=WWJGameOpCmd.RockerMoveMiddle 

    local curCameraMode=self._camera.CurCtrMode 
    --中间正常
    if curCameraMode==WWJCameraCtrMode.MiddleView then
        return opCmd
    end
    --左边相机
    if curCameraMode==WWJCameraCtrMode.LeftView then
        
        if opCmd==WWJGameOpCmd.RockerMoveRight then
            ret=WWJGameOpCmd.RockerMoveBack
        end
        if opCmd==WWJGameOpCmd.RockerMoveLeft then
            ret=WWJGameOpCmd.RockerMoveForward
        end
        if opCmd==WWJGameOpCmd.RockerMoveForward then
            ret=WWJGameOpCmd.RockerMoveRight
        end
        if opCmd==WWJGameOpCmd.RockerMoveBack then
            ret=WWJGameOpCmd.RockerMoveLeft
        end
        return ret
    end

    --右边相机
    if curCameraMode==WWJCameraCtrMode.RightView then
        
        if opCmd==WWJGameOpCmd.RockerMoveRight then
            ret=WWJGameOpCmd.RockerMoveForward
        end
        if opCmd==WWJGameOpCmd.RockerMoveLeft then
            ret=WWJGameOpCmd.RockerMoveBack
        end
        if opCmd==WWJGameOpCmd.RockerMoveForward then
            ret=WWJGameOpCmd.RockerMoveLeft
        end
        if opCmd==WWJGameOpCmd.RockerMoveBack then
            ret=WWJGameOpCmd.RockerMoveRight
        end
        return ret
    end
end

--[[
    @desc: --机台事件处理
    --@lastClawSt:上次爪子状态
	--@nowClawSt: 单签爪子状态
]]
function WWJGameClass:OnMachineEventCallBack(eventType,args)
    if eventType == MachineEventType.OnClawStateChanged then
        if args.NowState == WWJClawStateType.Back then
            --相机归位
            if self._camera then
                self._camera:ResetMiddleView()
            end
        end
        if args.NowState == WWJClawStateType.Idle and args.LastState == WWJClawStateType.Open then
            LogD("<color=yellow>抓取结束</color>")
            ScheduleService:AddTimer(self,self.DoCheckResult,1,false)
        end
    elseif eventType == MachineEventType.OnCatchedDoll then
        --抓中娃娃
        if args then
            self._catchedDoll = args
        end
    end
end

--检查是否抓取到娃娃
function WWJGameClass:DoCheckResult()
    if self._delegate then
        local dollObj = self._catchedDoll
        self._catchedDoll = false
        self._delegate.func(self._delegate.obj,WWJGameEventType.OnCatchEnd,dollObj)
    end
end