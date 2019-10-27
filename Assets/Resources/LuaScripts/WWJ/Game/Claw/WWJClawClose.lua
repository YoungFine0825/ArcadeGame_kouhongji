--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-06
*		描述： 娃娃机爪子抓 状态
*********************************************************************--]]
local WWJClawCloseClass = DeclareClass("WWJClawCloseClass")
--
function WWJClawCloseClass:ctor()
    --关闭速度
    self.CloseSpeed = 2.0
    self.TargetAngle = 0
    self.WaitFrameCnt=30

    self._claw = false
    self._isStoped = false
    self._isWaiting = false
    self._waitedCnt = 0
    --
    self._curCloseAngle = 0
end

function WWJClawCloseClass:Init(entity)
    self._claw = entity
    self.TargetAngle = entity.AngleMax
end

function WWJClawCloseClass:UnInit()
end

--进入状态
function WWJClawCloseClass:Enter()
    LogD("----------------Claw Close-------------->")
    self._isStoped = false
    self._curCloseAngle = 0
    self._isWaiting = false
    self._waitedCnt = 0

end

--是否相应Action
function WWJClawCloseClass:Reason()
    
    if not self._isStoped then
        return true
    end
    return false

end

--更新驱动
function WWJClawCloseClass:Action()
    
    --是否等待抓
    if self._isWaiting then
        self._waitedCnt=self._waitedCnt+1
        if self._waitedCnt>=self.WaitFrameCnt then
            self._isStoped=true
            --上去
            self._claw:ChangeState(WWJClawStateType.Rise)
        end
        return
    end
    --
    if math.EqualFloat(self._curCloseAngle, self.TargetAngle, 0.001) then
        --关闭合拢了
        self._isWaiting=true
        return
    end
    --
    self._curCloseAngle = self._curCloseAngle + self.CloseSpeed
    --手指根移动
    self._claw.Finger0JointCom:SetTargetPosition(0, (self._curCloseAngle / self.TargetAngle) * (-0.02), 0)
    --3根手指
    self._claw.Finger1JointCom:SetTargetRotationEuler(self._curCloseAngle, 0, 0)
    self._claw.Finger2JointCom:SetTargetRotationEuler(self._curCloseAngle, 0, 0)
    self._claw.Finger3JointCom:SetTargetRotationEuler(self._curCloseAngle, 0, 0)
    --
    self._claw.CurAngle=self._curCloseAngle
    --

end

--离开状态
function WWJClawCloseClass:Leave()
    self._isStoped = true
    self._curCloseAngle = 0
end
