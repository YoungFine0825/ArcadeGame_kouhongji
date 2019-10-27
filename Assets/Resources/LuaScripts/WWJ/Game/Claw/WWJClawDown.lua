--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-06
*		描述： 娃娃机爪子下爪
*********************************************************************--]]
local WWJClawDownClass = DeclareClass("WWJClawDownClass")
--
function WWJClawDownClass:ctor()
    
    self._claw=false
    self._cacahedRopeCom=false
    self._isStoped=true
    self._curRopeExtension=0
    --
    self._ropeMaxExtension=0
    self._ropeDownSpeed=0.005
    --
    self._cachedStopTriggerCom=false
    self._isStopTriggered=false

end

function WWJClawDownClass:Init(entity)
    
    self._claw=entity
    self._cacahedRopeCom=entity.RopeCom
    self._ropeMaxExtension=entity.RopeMaxExtension
    self._cachedStopTriggerCom=entity.StopTriggerCom
    self._curRopeExtension=self._cacahedRopeCom.m_fCurrentExtension
    --监听停止触发器 
    self._cachedStopTriggerCom:InitHandler(1,function (eventType,go)
        self:OnStopTriggerEvent(eventType,go)
    end)

end

function WWJClawDownClass:UnInit()
    
    self._claw=false
    self._cacahedRopeCom=false
    self._isStoped=true
    self._curRopeExtension=0
    --
    self._ropeMaxExtension=0
    if self._cachedStopTriggerCom then
        self._cachedStopTriggerCom:CleanHandler()
        self._cachedStopTriggerCom=false
    end
    
end

function WWJClawDownClass:OnStopTriggerEvent(eventType,go)
    --OnTriggerEnter
    if eventType==4  then
        self._isStopTriggered=true
    end

end

--进入状态
function WWJClawDownClass:Enter( )
    
    LogD("------------下爪Begin -------------")
    self._isStoped=false
    self._isStopTriggered=false
    self._curRopeExtension=self._cacahedRopeCom.m_fCurrentExtension

    if self._cachedStopTriggerCom then
        self._cachedStopTriggerCom:Reset()
    end
    
end

--是否相应Action
function WWJClawDownClass:Reason( )
    if self._isStoped then
        return false
    end
    return true
end

--更新驱动
function WWJClawDownClass:Action(delta)
    
    if not self._cacahedRopeCom then
        return
    end
    --停止触发了
    if self._isStopTriggered then
        self._isStoped=true
        --抓
        self._claw:ChangeState(WWJClawStateType.Close)
        return
    end
    --
    if math.EqualFloat(self._ropeMaxExtension,self._curRopeExtension,0.001) then
        self._isStoped=true
        --抓
        self._claw:ChangeState(WWJClawStateType.Close)
        return
    end
    self._curRopeExtension=self._cacahedRopeCom:DoExtendRopeLinear(self._ropeDownSpeed)

end

--离开状态
function WWJClawDownClass:Leave( )
    
end