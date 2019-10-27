--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-07-04
*		描述： 事件服务
*********************************************************************--]]
local EventServiceClass = DeclareClass("EventServiceClass")

function EventServiceClass:ctor()
    local interaction = Interaction.LuaInteraction
    self._uiEvent = ClassLib.UIEventClass.new(interaction)
    self._event = ClassLib.EventClass.new()
end

function EventServiceClass:Uninitialize()
    self._uiEvent:Uninitialize()
    self._event:Uninitialize()
    self._uiEvent=false
    self._event=false
end

--c# call
function EventServiceClass.OnUIEvent(id)
    EventService._uiEvent:OnEvent(id)
end

--c# call
function EventServiceClass.OnUIEventListener(id,pd)
    EventService._uiEvent:OnEventListener(id,pd)
end

function EventServiceClass:AddUIEvent(target, tbl, func, oneShot)
    self._uiEvent:AddEvent(target, tbl, func, oneShot)
end


function EventServiceClass:RemoveUIEvent(target, tbl)
    self._uiEvent:RemoveEvent(target, tbl)
end


function EventServiceClass:AddUIEventListener(target, tbl, func, oneShot)
    self._uiEvent:AddEventListener(target, tbl, func, oneShot)
end


function EventServiceClass:RemoveUIEventListener(target, tbl)
    self._uiEvent:RemoveEventListener(target, tbl)
end


function EventServiceClass:RemoveUIEventByTable(tbl)
    self._uiEvent:RemoveEventByTable(tbl)
end


function EventServiceClass:AddEvent(id, tbl, func)
    self._event:AddEvent(id, tbl, func)
end


function EventServiceClass:RemoveEvent(tbl, id)
    self._event:RemoveEvent(tbl, id)
end


function EventServiceClass:AddEventHook(id, tbl, func)
    self._event:AddEventHook(id, tbl, func)
end


function EventServiceClass:RemoveEventHook(tbl, id)
    self._event:RemoveEventHook(tbl, id)
end


function EventServiceClass:SendEvent(id, arg)
    self._event:SendEvent(id, arg)
end

EventService = EventServiceClass.new()
