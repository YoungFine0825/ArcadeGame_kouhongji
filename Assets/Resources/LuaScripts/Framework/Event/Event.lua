--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-07-04
*		描述： 事件处理
*********************************************************************--]]
local EventClass = DeclareClass("EventClass")
local null={}

function EventClass:ctor()

    self._argument = {{}, {}, {}, {}, {}, {}, {}, {}, {}, {}}
    self._useArgumentIndex = 1
    
    self._eventHandler = {}
    self._eventHook = {}
end

function EventClass:Uninitialize()
    self._interaction = false
    
    for i = 1, #self._eventHandler do
        local data = self._eventHandler[i]
        if data ~= null then
            LogE("EventClass.Uninitialize : event is not empty " .. data.tbl.__classname)
        end
    end
    
    for i = 1, #self._eventHook do
        local data = self._eventHook[i]
        if data ~= null then
            LogE("EventClass.Uninitialize : hook is not empty " .. data.tbl.__classname)
        end
    end
end

function EventClass:OnEvent(id, arg)
    if not id then
        LogE("EventClass.OnEvent : invalid parameter")
        return false
    end
    
    for i = 1, #self._eventHook do
        local data = #self._eventHook[i]
        if not data then
            LogE("EventClass.OnEvent : event hook have nil")
        elseif data ~= null and data.id == id then
            if data.func(data.tbl, id, arg) then
                return true
            end
        end
    end    
    
    for i = 1, #self._eventHandler do
        local data = self._eventHandler[i]
        if not data then
            LogE("EventClass.OnEvent : event handler have nil")
        elseif data ~= null and data.id == id then
            data.func(data.tbl, arg)
        end
    end
        
    return false
end

function EventClass:Clear()
   
    for i = #self._eventHook, 1, -1 do
        if tbl[i] == null then
            table.remove(self._eventHook, i)
        end
    end
    for i = #self._eventHandler, 1, -1 do
        if tbl[i] == null then
            table.remove(self._eventHandler, i)
        end
    end
end


function EventClass:AddEvent(id, tbl, func)
    if not id or not tbl or not func then
        LogE("EventClass.AddEventHandler : invalid parameter")
        return
    end
    
    for i = 1, #self._eventHandler do
        local data = self._eventHandler[i]
        if data ~= null and data.id == id and data.tbl == tbl then
            LogE("EventClass.AddEventHandler : duplicate add event id = "..data.id)
            return
        end
    end
    
    local data = Pool:CreateTable()
    data.id = id
    data.tbl = tbl
    data.func = func
    self._eventHandler[#self._eventHandler+1] = data
end


function EventClass:RemoveEvent(tbl, id)
    if not tbl then
        LogE("EventClass.RemoveEventHandler : invalid parameter")
        return
    end
    
    for i = 1, #self._eventHandler do
        local data = self._eventHandler[i]
        
        if data ~= null and data.tbl == tbl and (not id or data.id == id) then
            self._eventHandler[i] = null
            Pool:DestroyTable(data)
            if id then return end
        end
    end
end

function EventClass:AddEventHook(id, tbl, func)
    if not id or not tbl or not func then
        LogE("EventClass.AddEventHook : invalid parameter")
        return
    end
    
    for i = 1, #self._eventHook do
        local data = self._eventHook[i]
        if data ~= null and data.id == id and data.tbl == tbl then
            LogE("EventClass.AddEventHook : duplicate add event hook")
        end
    end
    
    local data = Pool:CreateTable()
    data.id = id
    data.tbl = tbl
    data.func = func
    self._eventHook[#self._eventHook+1] = data
end


function EventClass:RemoveEventHook(tbl, id)
    if not tbl then
        LogE("EventClass.RemoveEventHook : invalid parameter")
        return
    end
    
    for i = 1, #self._eventHook do
        local data = self._eventHook[i]
        
        if data ~= null and data.tbl == tbl and (not id or data.id == id) then
            self._eventHook[i] = null
            Pool:DestroyTable(data)
            if id then return end
        end
    end
end


function EventClass:SendEvent(id, arg)
    if not id then
        LogE("EventClass.SendEvent : invalid parameter")
        return
    end 
    self:OnEvent(id, arg)

end



