--[[********************************************************************
*      作者： jordenwu
*      时间： 07/17/17 15:31:13
*      描述： 统一的协成服务
*********************************************************************--]]

local CoroutineServiceClass = DeclareClass("CoroutineServiceClass")

function CoroutineServiceClass:ctor()
    self._instructionTime = 0
    self._instructionFrame = 1
    self._instructionCoroutine = 2
    
    -- func : 原始函数
    -- thread : 协同
    -- instruction : 指令
    -- parameter : 指令参数
    self._data = {}
end

function CoroutineServiceClass:Uninitialize()
    for i = 1, #self._data do
        if self._data[i] and self._data[i] ~= null then
            LogE("CoroutineServiceClass.Uninitialize : some coroutine is running")
        end
    end
end

function CoroutineServiceClass:Update(deltaTime)
    for i = #self._data, 1, -1 do
        local data = self._data[i]
        if data and data ~= null then
            if self:Process(data, deltaTime) then
                Pool:DestroyTable(data)
                self._data[i] = null
            end
        end        
    end
    
    table.removeNullFromArray(self._data)
end

-- 开始协同
-- @param func 协同函数，原型为：
--              void func(...)
--              ... - 可变参数（也可以没有参数）
--              返回值 - 无
-- @return 无
function CoroutineServiceClass:StartCoroutine(func, ...)
    if not func then
        LogE("CoroutineServiceClass.StartCoroutine : invalid parameter")
        return
    end
    
    local _, mainThread = coroutine.running()
    if not mainThread then
        LogE("CoroutineServiceClass.StartCoroutine : don't call StartCoroutine in coroutine, replace with WaitForCoroutine")
        return
    end
    
    local thread = coroutine.create(func)
    if not thread then
        LogE("CoroutineServiceClass.StartCoroutine : failed to create coroutine")
        return
    end
    
    local result, instruction, parameter = coroutine.resume(thread, ...)
    if not result then
        error(string.format("CoroutineServiceClass.StartCoroutine : failed to resume coroutine - %s", instruction))
        return
    end
    
    if not instruction or not parameter then
        return
    end
    
    local data = Pool:CreateTable()
    data.func = func
    data.thread = thread
    data.instruction = instruction
    data.parameter = parameter
    self._data[#self._data + 1] = data
end

-- 停止协同
-- @param func 协同函数
-- @return 无
function CoroutineServiceClass:StopCoroutine(func)
    if not func then
        LogE("CoroutineServiceClass.StopCoroutine : invalid parameter")
        return
    end
    
    local _, mainThread = coroutine.running()
    if not mainThread then
        LogE("CoroutineServiceClass.StopCoroutine : don't call StopCoroutine in coroutine")
        return
    end
     
    for i = 1, #self._data do
        local data = self._data[i]
        if data and data ~= null and data.func == func then
            self._data[i] = null
            Pool:DestroyTable(data)
        end
    end
end

-- 挂起协同到一定的时长
-- @param time 时长（单位：秒）
-- @return 用于yield
function CoroutineServiceClass:WaitForTime(time)
    return self._instructionTime, time or 0
end

-- 挂起协同到一定的帧数
-- @param frame 帧数
-- @return 用于yield
function CoroutineServiceClass:WaitForFrame(frame)
    return self._instructionFrame, frame or 0
end

-- 挂起协同到子协同完成
-- @param func 协同函数，原型为：
--              void func(...)
--              ... - 可变参数（也可以没有参数）
--              返回值 - 无
-- @return 用于yield
function CoroutineServiceClass:WaitForCoroutine(func, ...)
    if not func then
        LogE("CoroutineServiceClass.WaitForCoroutine : invalid parameter")
        return self:WaitForFrame(1)
    end
    
    local thread = coroutine.create(func)
    if not thread then
        LogE("CoroutineServiceClass.WaitForCoroutine : failed to create coroutine")
        return self:WaitForFrame(1)
    end
    
    local result, instruction, parameter = coroutine.resume(thread, ...)
    if not result then
        error(string.format("CoroutineServiceClass.WaitForCoroutine : failed to resume coroutine - %s", instruction))
        return self:WaitForFrame(1)
    end
    
    if not instruction or not parameter then
        return self:WaitForFrame(1)
    end
    
    local data = Pool:CreateTable()
    data.func = func
    data.thread = thread
    data.instruction = instruction
    data.parameter = parameter
    
    return self._instructionCoroutine, data
end

function CoroutineServiceClass:Process(data, deltaTime)    
    if data.instruction == self._instructionTime then
        data.parameter = data.parameter - deltaTime
        if data.parameter > 0 then
            return false
        end
        
    elseif data.instruction == self._instructionFrame then
        data.parameter = data.parameter - 1
        if data.parameter > 0 then
            return false
        end
        
    elseif data.instruction == self._instructionCoroutine then
        if not self:Process(data.parameter, deltaTime) then
            return false
        end
        
        Pool:DestroyTable(data.parameter)
        data.parameter = false
        
    else
        LogE("CoroutineServiceClass.Process : invalid instruction")
        return true
    end
    
    local result, instruction, parameter = coroutine.resume(data.thread)
    if not result then
        data.instruction = -1
        error(string.format("CoroutineServiceClass.Process : failed to resume coroutine - %s", instruction))
        return true
    end
    
    if not instruction or not parameter then
        return true
    end
    
    data.instruction = instruction
    data.parameter = parameter    
    return false
end

CoroutineService = CoroutineServiceClass.new()


