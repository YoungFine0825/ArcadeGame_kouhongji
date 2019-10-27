--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-06
*		描述： 娃娃机爪子移动状态
*********************************************************************--]]
local WWJClawMoveClass = DeclareClass("WWJClawMoveClass")
--
function WWJClawMoveClass:ctor()
    
    self._claw = false
    self._cachedMoveCubeTf = false
    self._cachedVTrunkTf = false
    --当前移动
    self._curMoveSpeedXZ = {x = 0, z = 0}
    --当前移动最终XZ位置
    self._curTargetXZ = {x = 0, z = 0}
    --当前爪子所在位置
    self._curPosXZ = {x = 0, z = 0}
    --插值速度
    self.LerpSpeed=12
    self.MoveSpeed=0.01
    --移动边界
    self.MoveXMax=0
    self.MoveXMin=0
    self.MoveZMax=0
    self.MoveZMin=0
    --
end

function WWJClawMoveClass:Init(entity)
    self._claw = entity
    self._cachedMoveCubeTf = entity.MoveCubeTf
    self._cachedVTrunkTf = entity.VTrunkTf
    --
    self.MoveXMax=entity.MoveXMax
    self.MoveXMin=entity.MoveXMin
    self.MoveZMax=entity.MoveZMax
    self.MoveZMin=entity.MoveZMin
    --
end

function WWJClawMoveClass:UnInit()
    self._claw = false
    self._cachedMoveCubeTf = false
    self._cachedVTrunkTf = false
end

--处理移动命令
function WWJClawMoveClass:DealMoveCmd(opCmd)
   
    if opCmd == WWJGameOpCmd.RockerMoveMiddle then
        self._curMoveSpeedXZ.x = 0
        self._curMoveSpeedXZ.z = 0
    end

    if opCmd == WWJGameOpCmd.RockerMoveRight then
        self._curMoveSpeedXZ.x = -self.MoveSpeed
    end
    if opCmd == WWJGameOpCmd.RockerMoveLeft then
        self._curMoveSpeedXZ.x =self.MoveSpeed
    end

    if opCmd == WWJGameOpCmd.RockerMoveForward then
        self._curMoveSpeedXZ.z = -self.MoveSpeed
    end
    if opCmd == WWJGameOpCmd.RockerMoveBack then
        self._curMoveSpeedXZ.z = self.MoveSpeed
    end

end

--进入状态
function WWJClawMoveClass:Enter()
    
    local lp=self._claw.MoveCubeTf.localPosition
    self._curPosXZ.x=lp.x
    self._curPosXZ.z=lp.z
    --
    self._curTargetXZ.x=lp.x
    self._curTargetXZ.z=lp.z
    --

end

--是否相应Action
function WWJClawMoveClass:Reason()
    
    return true

end

--更新驱动
function WWJClawMoveClass:Action(delta)
    
    local isXEqual = math.EqualFloat(self._curPosXZ.x, self._curTargetXZ.x, 0.001)
    local isZEqual = math.EqualFloat(self._curPosXZ.z, self._curTargetXZ.z, 0.001)
    --判断是否到达目标点
    if (not isXEqual) or (not isZEqual) then
       
        local lerpX = self._curPosXZ.x
        if not isXEqual then
            lerpX = math.Lerp(self._curPosXZ.x, self._curTargetXZ.x, delta * self.LerpSpeed)
        end

        local lerpZ = self._curPosXZ.z
        if not isZEqual then
            lerpZ = math.Lerp(self._curPosXZ.z, self._curTargetXZ.z, delta * self.LerpSpeed)
        end
        --
        lerpX=math.Clamp(lerpX,self.MoveXMin,self.MoveXMax)
        lerpZ=math.Clamp(lerpZ,self.MoveZMin,self.MoveZMax)
        --移动
        self._cachedVTrunkTf:ExtSetLocalPositionX(lerpX)
        --
        self._cachedMoveCubeTf:ExtSetLocalPositionXZ(lerpX, lerpZ)
        --
        self._curPosXZ.x = lerpX
        self._curPosXZ.z = lerpZ
        --
    end

    ---加
    if self._curMoveSpeedXZ.x ~= 0 then
        self._curTargetXZ.x = self._curTargetXZ.x + self._curMoveSpeedXZ.x
        self._curTargetXZ.x=math.Clamp(self._curTargetXZ.x,self.MoveXMin,self.MoveXMax)
    end
    if self._curMoveSpeedXZ.z ~= 0 then
        self._curTargetXZ.z = self._curTargetXZ.z + self._curMoveSpeedXZ.z
        self._curTargetXZ.z=math.Clamp(self._curTargetXZ.z,self.MoveZMin,self.MoveZMax)
    end

end

--离开状态
function WWJClawMoveClass:Leave()
    
    self._curMoveSpeedXZ.x=0
    self._curMoveSpeedXZ.z=0

end
