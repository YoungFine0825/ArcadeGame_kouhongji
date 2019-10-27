--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-06
*		描述： 娃娃机爪子上升状态
*********************************************************************--]]
local WWJClawRiseClass = DeclareClass("WWJClawRiseClass")
--
function WWJClawRiseClass:ctor()
    
    self._claw=false
    self._curRopeExtension=0
    self._cacahedRopeCom=false

    self._isRising=false
    self._riseSpeed=-0.005

    --标记是否需要扔掉公仔
    self._isNeedThrow=false
    --打开扔掉
    self._throwSpeed = -5.0
    self._curOpenAngle = 0
    self._throwBeginExtension=0

end

function WWJClawRiseClass:Init(entity)
    
    self._claw=entity
    self._cacahedRopeCom=entity.RopeCom
    

end

function WWJClawRiseClass:UnInit()
    
end

--进入状态
function WWJClawRiseClass:Enter( )
    
    LogD("-------------上升-----------")
    --当前长度
    self._curRopeExtension=self._cacahedRopeCom.m_fCurrentExtension
    self._isRising=true
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
    if self._isNeedThrow then
        LogD("需要扔掉")
        self._curOpenAngle=self._claw.CurAngle
        self._throwBeginExtension=self._curRopeExtension-0.2
        --推算扔掉的速度
        self._throwSpeed=self._curOpenAngle/(self._throwBeginExtension/self._riseSpeed)
        LogD("扔掉速度"..tostring(self._throwSpeed))
    end

end

--是否相应Action
function WWJClawRiseClass:Reason( )
    
    if not self._isRising then
        return false
    end
    return true
end

--更新驱动
function WWJClawRiseClass:Action( )
    
    if not self._cacahedRopeCom then
        return
    end
    --扔掉公仔 上升会才扔
    if self._isNeedThrow and self._curRopeExtension<self._throwBeginExtension then
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
             --
             self._claw.CurAngle=self._curOpenAngle
        end
    end
    --
    if math.EqualFloat(self._curRopeExtension,0) then
        self._isRising=false
        --上升完成
        self._claw:ChangeState(WWJClawStateType.Back)
        return
    end
    self._curRopeExtension=self._cacahedRopeCom:DoExtendRopeLinear(self._riseSpeed)

end

--离开状态
function WWJClawRiseClass:Leave( )
    
end