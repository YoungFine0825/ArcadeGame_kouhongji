--[[********************************************************************
*      作者： jordenwu
*      时间： 07/17/17 15:53:31
*      描述： 调度器对接
*********************************************************************--]]
local ScheduleServiceClass = DeclareClass("ScheduleServiceClass")

function ScheduleServiceClass:ctor()
    self._updater = {}
    self._timer = {}
end

function ScheduleServiceClass:Uninitialize()
    for i = 1, #self._updater do
        if self._updater[i] ~= null then
            LogE("ScheduleServiceClass.Uninitialize : updator is not empty")
            LogE(self._updater[i].obj.__classname)
        end
    end

    for i = 1, #self._timer do
        if self._timer[i] ~= null then
            LogE("ScheduleServiceClass.Uninitialize : timer is not empty")
            LogE(self._timer[i].obj.__classname)
        end
    end
end

function ScheduleServiceClass:CleanAll()

    for i = 1, #self._updater do
        local data = self._updater[i]
        if data ~= null  then
            self._updater[i] = null
            Pool:DestroyTable(data)
        end
    end
    for i = 1, #self._timer do
        local data = self._timer[i]
        if data ~= null  then
            self._timer[i] = null
            Pool:DestroyTable(data)
        end
    end

end


function ScheduleServiceClass:Update(deltaTime)

    for i = #self._updater, 1, -1 do
        local data = self._updater[i]
        
        if data ~= null then
            data.func(data.obj,deltaTime)
        else
            table.remove(self._updater, i)
        end
    end
    
    for i = #self._timer, 1, -1 do
        local data = self._timer[i]
        
        if data == null then
            table.remove(self._timer, i)
        else
            data.remain = data.remain - deltaTime
            
            if data.remain <= 0 then
                data.func(data.obj)
                
                data = self._timer[i]
                if data ~= null then
                    if data.repetition then
                        data.remain = data.remain + data.interval
                    else
                        self._timer[i] = null
                        Pool:DestroyTable(data)
                    end
                end
            end
        end
    end
    
    CoroutineService:Update(deltaTime)

end

-- 添加更新
-- @param obj 接收更新回调的对象
-- @param func 接收更新回调的对象中的函数
function ScheduleServiceClass:AddUpdater(obj, func)
    if not obj or not func then
        LogE("ScheduleServiceClass.AddUpdator : invald parameter")
        return
    end

    for i = 1, #self._updater do
        local data = self._updater[i]
        
        if data ~= null and data.obj == obj and data.func == func then
            LogE("ScheduleServiceClass.AddUpdater : duplicate add updater")
            return
        end
    end
    
    local t = Pool:CreateTable()
    t.obj = obj
    t.func = func
    self._updater[#self._updater+1] = t
end

-- 移除更新
-- @param obj 接收更新回调的对象
-- @param func 接收更新回调的对象中的函数（为nil或者没有该参数标识移除所有obj关联的更新）
function ScheduleServiceClass:RemoveUpdater(obj, func)
    if not obj then
        LogE("ScheduleServiceClass.RemoveUpdater : invalid parameter")
        return
    end
    
    for i = 1, #self._updater do
        local data = self._updater[i]
        
        if data ~= null and data.obj == obj then
            if not func then
                self._updater[i] = null
                Pool:DestroyTable(data)
                
            elseif data.func == func then
                self._updater[i] = null
                Pool:DestroyTable(data)
                return
            end
        end
    end
end

-- 添加定时器
-- @param obj 接收定时回调的对象
-- @param func 接收回调的对象中的函数
-- @param interval 定时间隔时间，单位：秒
-- @param repetition 是否重复（默认不重复）
function ScheduleServiceClass:AddTimer(obj, func, interval, repetition)
    if not obj or not func or not interval or interval < 0 then
        LogE("ScheduleServiceClass.AddTimer : invalid parameter")
        return
    end
    
    for i = 1, #self._timer do
        local data = self._timer[i]
        
        if data ~= null and data.obj == obj and data.func == func then
            LogE("ScheduleServiceClass.AddTimer : duplicate add timer")
            return
        end
    end
    
    local t = Pool:CreateTable()
    t.obj = obj
    t.func = func
    t.interval = interval
    t.repetition = repetition or false
    t.remain = interval
    
    self._timer[#self._timer+1] = t
end

-- 移除定时器
-- @param tbl 接收回调的对象
-- @param fun 接收回调的对象中的函数（为nil或者没有该参数删除跟该对象关联的所有定时器）
function ScheduleServiceClass:RemoveTimer(obj, func)
    if not obj then
        LogE("ScheduleServiceClass.RemoveTimer : invalid parameter")
        return
    end
    
    for i = 1, #self._timer do
        local data = self._timer[i]
        
        if data ~= null and data.obj == obj then
            if not func then
                self._timer[i] = null
                Pool:DestroyTable(data)
            elseif data.func == func then
                self._timer[i] = null
                Pool:DestroyTable(data)
                return
            end
        end
    end
end

ScheduleService = ScheduleServiceClass.new()
