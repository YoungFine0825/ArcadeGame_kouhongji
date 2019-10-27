--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-06
*		描述： 娃娃机爪子归位状态
*********************************************************************--]]
local WWJClawBackClass = DeclareClass("WWJClawBackClass")
--
function WWJClawBackClass:ctor()
    
    self._claw=false
    --初始位置
    self.OriPosX=0
    self.OriPosZ=0
    self.BackSpeed=0.08
    --
    self._curPosX=0
    self._curPosZ=0
    --当前偏移初始位置值
    self._curPosOffX=0
    self._curPosOffZ=0

    --
    --标记是否需要扔掉公仔
    self._isNeedThrow=false
    --打开扔掉
    self._throwSpeed = -1.0
    self._curOpenAngle = 0


end

function WWJClawBackClass:Init(entity)
    
    self._claw=entity
    self.OriPosX=entity.OriPosX
    self.OriPosZ=entity.OriPosZ
    --
   
end

function WWJClawBackClass:UnInit()
    
end

--进入状态
function WWJClawBackClass:Enter( )
    
    LogD("--------------Claw Back------------")
    local lp=self._claw.MoveCubeTf.localPosition
    self._curPosX=lp.x
    self._curPosZ=lp.z

    self._curPosOffX=self._curPosX-self.OriPosX
    self._curPosOffZ=self._curPosZ-self.OriPosX
    --
    --作弊模式
    local gettedCnt=self._claw.MainTriggerCom:GetEnteredGameObjectCnt()
    --抓到2个 必须扔掉
    if self._claw.IsCheatMode then
        self._isNeedThrow=true
    else
        if gettedCnt>=2 then
            self._isNeedThrow=true
        else
            self._isNeedThrow=false
        end
    end
    --ToDo 根据距离测算 扔掉速度
    if self._isNeedThrow then
        LogD("归位需要扔掉")
        self._curOpenAngle=self._claw.CurAngle
        if math.abs(self._curPosOffX)<0.2 or math.abs( self._curPosOffZ)<0.2 then
            self._throwSpeed = -5.0
        else
            self._throwSpeed = -1.0
        end
        LogD("扔掉速度"..tostring(self._throwSpeed))
    end
    --
end

--是否相应Action
function WWJClawBackClass:Reason( )
    
    return true

end

--更新驱动
function WWJClawBackClass:Action(delta)
    
    --返回过程扔掉公仔 
    if self._isNeedThrow  then
        if self._curOpenAngle>0 then
            self._curOpenAngle=self._curOpenAngle+self._throwSpeed
            if self._curOpenAngle<=0 then
                self._curOpenAngle=0
            end
             --手指根移动
             self._claw.Finger0JointCom:SetTargetPosition(0,(self._curOpenAngle/self._claw.AngleMax) * (-0.02),0)
             --3根手指
             self._claw.Finger1JointCom:SetTargetRotationEuler(self._curOpenAngle,0,0)
             self._claw.Finger2JointCom:SetTargetRotationEuler(self._curOpenAngle,0,0)
             self._claw.Finger3JointCom:SetTargetRotationEuler(self._curOpenAngle,0,0)
             self._claw.CurAngle=self._curOpenAngle
        end
    end
    --

    --先左右方向回去
    local isXReach = math.EqualFloat(self._curPosX, self.OriPosX, 0.001)
    if not isXReach then
        local bx = math.Lerp(self._curPosX, self.OriPosX, self.BackSpeed)
        self._claw.VTrunkTf:ExtSetLocalPositionX(bx)
        self._claw.MoveCubeTf:ExtSetLocalPositionX(bx)
        self._curPosX=bx
    else
        --再前后方向回去
        local isZReach=math.EqualFloat(self._curPosZ, self.OriPosZ, 0.001)
        if not isZReach  then
            --
            local bz = math.Lerp(self._curPosZ, self.OriPosZ, self.BackSpeed)
            self._claw.MoveCubeTf:ExtSetLocalPositionZ(bz)
            self._curPosZ=bz
        else
            --
            self._curPosX=self.OriPosX
            self._curPosZ=self.OriPosZ
            --到了起始点 Open爪子
            self._claw:ChangeState(WWJClawStateType.Open)
        end
    end

end

--离开状态
function WWJClawBackClass:Leave( )
    
end