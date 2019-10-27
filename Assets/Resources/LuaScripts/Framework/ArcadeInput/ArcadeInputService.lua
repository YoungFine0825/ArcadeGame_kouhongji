--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-01
*		描述： 对接街机物理输入
*********************************************************************--]]
local ArcadeInputServiceClass = DeclareClass("ArcadeInputServiceClass")

function ArcadeInputServiceClass:ctor()

    self._okHandler=false
    self._refreshHandler=false
    self._rockerHandler=false
    self._rotatehandler=false

    --禁止
    self._isForbid=false

end

--[[
    @desc: 硬件OK 按钮输入变化
    @return:
]]
function ArcadeInputServiceClass.OnCsOkInput(pressEvent)
    
    local self = ArcadeInputService

    if self._isForbid then
        return
    end

    if self._okHandler then
        local tbl = self._okHandler.tbl
        local func = self._okHandler.func
        func(tbl, pressEvent)
    end

end

--[[
    @desc: 硬件刷新 按钮输入变化
    @return:
]]
function ArcadeInputServiceClass.OnCsRefreshInput(pressEvent)
    
    local self = ArcadeInputService
    if self._isForbid then
        return
    end

    if self._refreshHandler then
        local tbl = self._refreshHandler.tbl
        local func = self._refreshHandler.func
        func(tbl, pressEvent)
    end

end

--[[
    @desc: 硬件摇杆
    @return:
]]
function ArcadeInputServiceClass.OnCsRockerInput(rockerSt)
    
    local self = ArcadeInputService
    
    if self._isForbid then
        return
    end

    if self._rockerHandler then
        local tbl = self._rockerHandler.tbl
        local func = self._rockerHandler.func
        func(tbl, rockerSt)
    end

end

--[[
    @desc: 硬件旋转按钮
    @return:
]]
function ArcadeInputServiceClass.OnCsRotateInput(angle)
    
    local self = ArcadeInputService
    
    if self._isForbid then
        return
    end

    if self._rotatehandler then
        local tbl = self._rotatehandler.tbl
        local func = self._rotatehandler.func
        func(tbl, angle)
    end

end

--[[
    @desc: 禁止输入 是否 
    @return:
]]
function ArcadeInputServiceClass:ForbidInput(isForbit)
    
    LogD("Arcade ForbidInput:"..tostring(isForbit))
    self._isForbid=isForbit
    
end

--------------------注册-----------
function ArcadeInputServiceClass:RegisterOkInput(tbl,func)
    
    if self._okHandler then
        LogE("Arcade Input Just Need One Handler,UnRegister First")
        LogE("----ClassName:%s",self._okHandler.tbl.__classname)
    end
     --添加回调控制
     if tbl and func then
        local delegate = Pool:CreateTable()
        delegate.tbl = tbl
        delegate.func = func
        self._okHandler =delegate
    end

end

function ArcadeInputServiceClass:UnRegisterOkInput(tbl)
    
    if self._okHandler then
        Pool:DestroyTable(self._okHandler)
        self._okHandler=false
    end

end


function ArcadeInputServiceClass:RegisterRefreshInput(tbl,func)
    
     --添加回调控制
     if self._refreshHandler then
        LogE("Arcade Input Just Need One Handler,UnRegister First")
        LogE("----ClassName:%s",self._refreshHandler.tbl.__classname)
    end
     --添加回调控制
     if tbl and func then
        local delegate = Pool:CreateTable()
        delegate.tbl = tbl
        delegate.func = func
        self._refreshHandler =delegate
    end

end

function ArcadeInputServiceClass:UnRegisterRefreshInput(tbl)
    
    if self._refreshHandler then
        Pool:DestroyTable(self._refreshHandler)
        self._refreshHandler=false
    end

end


function ArcadeInputServiceClass:RegisterRockerInput(tbl,func)
    
     --添加回调控制
     if self._rockerHandler then
        LogE("Arcade Input Just Need One Handler,UnRegister First")
        LogE("----ClassName:%s",self._rockerHandler.tbl.__classname)
    end
     --添加回调控制
    if tbl and func then
        local delegate = Pool:CreateTable()
        delegate.tbl = tbl
        delegate.func = func
        self._rockerHandler =delegate
    end
end


function ArcadeInputServiceClass:UnRegisterRockerInput(tbl)
    
    if self._rockerHandler then
        Pool:DestroyTable(self._rockerHandler)
        self._rockerHandler=false
    end

end


function ArcadeInputServiceClass:RegisterRotateInput(tbl,func)
    
    --添加回调控制
    if self._rotatehandler then
        LogE("Arcade Input Just Need One Handler,UnRegister First")
        LogE("----ClassName:%s",self._rotatehandler.tbl.__classname)
    end
     --添加回调控制
     if tbl and func then
        local delegate = Pool:CreateTable()
        delegate.tbl = tbl
        delegate.func = func
        self._rotatehandler =delegate
    end
end


function ArcadeInputServiceClass:UnRegisterRotateInput(tbl)
    
    if self._rotatehandler then
        Pool:DestroyTable(self._rotatehandler)
        self._rotatehandler=false
    end

end

function ArcadeInputServiceClass:GetDeviceKey()
    return Interaction.ArcadeInputService:GetDeviceKey();
end

---------------
ArcadeInputService = ArcadeInputServiceClass.new()