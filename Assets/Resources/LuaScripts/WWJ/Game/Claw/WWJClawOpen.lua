--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-06
*		描述： 娃娃机爪子打开状态
*********************************************************************--]]
local WWJClawOpenClass = DeclareClass("WWJClawOpenClass")
--
function WWJClawOpenClass:ctor()
    self._claw = false

    --打开
    self.OpenSpeed = -5.0
    self._curOpenAngle = 0
    --
    self.TargetAngle=0

end

function WWJClawOpenClass:Init(entity)
    self._claw = entity
    --
    self.TargetAngle=entity.AngleMax

end

function WWJClawOpenClass:UnInit()
end

--进入状态
function WWJClawOpenClass:Enter()
    LogD("------------Open---------")
    self._curOpenAngle=self._claw.CurAngle

end

--是否相应Action
function WWJClawOpenClass:Reason()
    return true
end

--更新驱动
function WWJClawOpenClass:Action(delta)

    self._curOpenAngle=self._curOpenAngle+self.OpenSpeed
    --
    if self._curOpenAngle<=0 then
        --手指根移动
        self._curOpenAngle=0
        self._claw.Finger0JointCom:SetTargetPosition(0,0,0)
        --3根手指
        self._claw.Finger1JointCom:SetTargetRotationEuler(0,0,0)
        self._claw.Finger2JointCom:SetTargetRotationEuler(0,0,0)
        self._claw.Finger3JointCom:SetTargetRotationEuler(0,0,0)
        self._claw:ChangeState(WWJClawStateType.Idle)
        return
    else
        --手指根移动
        self._claw.Finger0JointCom:SetTargetPosition(0,(self._curOpenAngle/self.TargetAngle) * (-0.02),0)
        --3根手指
        self._claw.Finger1JointCom:SetTargetRotationEuler(self._curOpenAngle,0,0)
        self._claw.Finger2JointCom:SetTargetRotationEuler(self._curOpenAngle,0,0)
        self._claw.Finger3JointCom:SetTargetRotationEuler(self._curOpenAngle,0,0)
    end
    
end

--离开状态
function WWJClawOpenClass:Leave()
end
