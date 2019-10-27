--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-07-04
*		描述： UI 事件包装
*********************************************************************--]]
local UIEventClass = DeclareClass("UIEventClass")
local null = {}

function UIEventClass:ctor(interaction)
    self._interaction = interaction
    self._idCount = 0
    self._data = {}
    self._processing = false
end

function UIEventClass:Uninitialize()
    for i = 1, #self._data do
        if self._data[i] ~= null then
            LogE("UIEventClass.Uninitialize : data is not empty")
            self._interaction.RemoveUIEventHandle(self._data[i].target, self._data[i].id)
            self._data[i] = null
        end
    end
    self._interaction = false
    self._idCount = 0
    self._data = {}
end

function UIEventClass:AddEvent(target, tbl, func, oneShot)
    if not target or not tbl or not func then
        return
    end

    oneShot = oneShot or false

    for i = 1, #self._data do
        local data = self._data[i]
        if data ~= null and data.target == target and data.tbl == tbl then
            LogE("UIEventClass.AddEvent : duplicate add event , target name = " .. tbl.__classname)
            return
        end
    end

    self._idCount = self._idCount + 1

    local data = Pool:CreateTable()
    data.id = self._idCount
    data.target = target
    data.tbl = tbl
    data.func = func
    data.oneShot = oneShot
    self._data[#self._data + 1] = data

    self._interaction.AddUIEventHandle(target, data.id, data.oneShot)
end

function UIEventClass:RemoveEvent(target, tbl)
    if not target or not tbl then
        LogE("UIEventClass.RemoveEvent : invalid parameter")
        return
    end

    for i = 1, #self._data do
        local data = self._data[i]

        if data ~= null and data.target == target and data.tbl == tbl then
            self._interaction.RemoveUIEventHandle(data.target, data.id)
            self._data[i] = null
            Pool:DestroyTable(data)
            return
        end
    end
end

function UIEventClass:AddEventListener(target, tbl, func, oneShot)
    if not target or not tbl or not func then
        return
    end

    oneShot = oneShot or false

    for i = 1, #self._data do
        local data = self._data[i]
        if data ~= null and data.target == target and data.tbl == tbl then
            LogE("UIEventClass.AddEvent : duplicate add event , target name = " .. tbl.__classname)
            return
        end
    end

    self._idCount = self._idCount + 1

    local data = Pool:CreateTable()
    data.id = self._idCount
    data.target = target
    data.tbl = tbl
    data.func = func
    data.oneShot = oneShot
    self._data[#self._data + 1] = data

    self._interaction.AddUIEventListenerHandle(target, data.id, data.oneShot)
end

function UIEventClass:RemoveEventListener(target, tbl)
    if not target or not tbl then
        LogE("UIEventClass.RemoveEvent : invalid parameter")
        return
    end

    for i = 1, #self._data do
        local data = self._data[i]

        if data ~= null and data.target == target and data.tbl == tbl then
            self._interaction.RemoveUIEventListenerHandle(data.target, data.id)
            self._data[i] = null
            Pool:DestroyTable(data)
            return
        end
    end
end

function UIEventClass:RemoveEventByTable(tbl)
    if not tbl then
        LogE("UIEventClass.RemoveEventByTable : invalid parameter")
        return
    end

    for i = 1, #self._data do
        local data = self._data[i]

        if data ~= null and data.tbl == tbl then
            self._interaction.RemoveUIEventHandle(data.target, data.id)
            self._data[i] = null
            Pool:DestroyTable(data)
        end
    end
end

function UIEventClass:OnEvent(id)
    if self._processing then
        LogE("UIEventClass.OnEvent : event is processing")
        return
    end

    self._processing = true

    local remove = false

    for i = #self._data, 1, -1 do
        local data = self._data[i]
        if not data then
            LogE("UIEventClass.OnEvent : data is nil")
            self._processing = false
            return
        end

        if data == null then
            remove = true
        elseif data.id == id then
            local tbl = data.tbl
            local func = data.func

            if data.oneShot then
                self._data[i] = null
                Pool:DestroyTable(data)
                remove = true
            end

            if Global.IsDebug then
                local ff = function()
                    if tbl and func then
                        func(tbl)
                    end
                end
                local ok, errors = pcall(ff)
                if not ok then
                    LogE("Catch Error OnUIEvent Deal:%s", tostring(errors))
                end
            else
                if tbl and func then
                    func(tbl)
                end
            end
        end
    end

    if remove then
        for i = #self._data, 1, -1 do
            if self._data[i] == null then
                table.remove(self._data, i)
            end
        end
    end

    self._processing = false
end

function UIEventClass:OnEventListener(id, pointData)
    if self._processing then
        LogE("UIEventClass.OnEventListener : event is processing")
        return
    end

    self._processing = true

    local remove = false

    for i = #self._data, 1, -1 do
        local data = self._data[i]
        if not data then
            LogE("UIEventClass.OnEvent : data is nil")
            self._processing = false
            return
        end

        if data == null then
            remove = true
        elseif data.id == id then
            local tbl = data.tbl
            local func = data.func

            if data.oneShot then
                self._data[i] = null
                Pool:DestroyTable(data)
                remove = true
            end
            if Global.IsDebug then
                local ff = function()
                    if tbl and func then
                        func(tbl, pointData)
                    end
                end
                local ok, errors = pcall(ff)
                if not ok then
                    LogE("Catch Error OnUIEvent Deal:%s", tostring(errors))
                end
            else
                if tbl and func then
                    func(tbl, pointData)
                end
            end
        end
    end

    if remove then
        for i = #self._data, 1, -1 do
            if self._data[i] == null then
                table.remove(self._data, i)
            end
        end
    end

    self._processing = false
end
