--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-06
*		描述： 娃娃机爪子IDLE状态
*********************************************************************--]]
local WWJClawIdleClass = DeclareClass("WWJClawIdleClass")
--
function WWJClawIdleClass:ctor()
    
    self._claw=false

end

function WWJClawIdleClass:Init(entity)
    
    self._claw=entity

end

function WWJClawIdleClass:UnInit()
    
end

--进入状态
function WWJClawIdleClass:Enter( )
    
    LogD("------------Claw IDLE---------")
    if not self._claw then
        return
    end
    --归位
    self._claw.MoveCubeTf:ExtSetLocalPositionXZ(self._claw.OriPosX,self._claw.OriPosZ)
    self._claw.VTrunkTf:ExtSetLocalPositionX(self._claw.OriPosX)
    --手指根移动
    self._claw.Finger0JointCom:SetTargetPosition(0,0,0)
    --3根手指 打开
    self._claw.Finger1JointCom:SetTargetRotationEuler(0,0,0)
    self._claw.Finger2JointCom:SetTargetRotationEuler(0,0,0)
    self._claw.Finger3JointCom:SetTargetRotationEuler(0,0,0)
    --
    self._claw.CurAngle=0

end

--是否相应Action
function WWJClawIdleClass:Reason( )
    
    return false
end

--更新驱动
function WWJClawIdleClass:Action( )
    
end

--离开状态
function WWJClawIdleClass:Leave( )
    
end