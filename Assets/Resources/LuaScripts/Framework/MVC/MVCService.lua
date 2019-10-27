--[[********************************************************************
*      作者： jordenwu
*      时间： 07/17/17 16:04:36
*      描述： MVC 服务
*********************************************************************--]]
local MVCServiceClass = DeclareClass("MVCServiceClass")

function MVCServiceClass:ctor()
    
    --标记操作类型
    self.EnterGameStateOp = 1
    self.LeaveGameStateOp = 2
    
    local interaction = Interaction
    self._luaInteraction = interaction.LuaInteraction
    self._uguiRoot = interaction.UGUIRoot
    self._uiStateService = interaction.UIStateService
    
    --所有模块
    self._module = {}
end

function MVCServiceClass:Uninitialize()
    if #self._module > 0 then
        LogE("MVCServiceClass.Uninitialize : module is not empty")
    end
end

--c#端调用callType 调用类型 0-stay 1-out 2 -in 
function MVCServiceClass.OnCsChangeUIState(callType, changeType, oldStateName, newStateName, stateParam)
    local self = MVCService
    --通知模块
    for i = 1, #self._module do
        local m = self._module[i]
        m:ChangeUIState(callType, changeType, oldStateName, newStateName, stateParam)
    end
end

--游戏状态改变调用
function MVCServiceClass:OnChangeGameState(op,state1, state2, userData)

    --通知模块
    for i = 1, #self._module do
        local md = self._module[i]
        md:ChangeGameState(op,state1, state2, userData)
    end
    return true
end


-- 创建模块
-- @param moduleClass 模块类定义
-- @param ... Mediator类定义
function MVCServiceClass:CreateModule(moduleClass, ...)
    local md = moduleClass.new()
    md:Create()
    
    for i = 1, select('#', ...) do
        local arg = select(i, ...)
        md:CreateMediator(arg)
    end
    
    self._module[#self._module+1] = md
    return md
end

function MVCServiceClass:DestroyAllModule()
    for i = 1, #self._module do
        local md = self._module[i]
        md:Destroy()
    end
    
    self._module = {}
end

function MVCServiceClass:DestroyModule(md)
    
    for i=#self._module,1,-1 do
        if self._module[i]==md then
            self._module[i]:Destroy()
            table.remove(self._module,i)
            break
        end
    end
end

function MVCServiceClass:ChangeUIState(stateName,param)
    
    if not stateName then
        LogE("MVCServiceClass.ChangeState : invalid parameter")
        return
    end

    LogD("MVCService ChangeUIState "..stateName)
    
    self._uiStateService:ChangeState(stateName, param)
    
end

function MVCServiceClass:PushUIState(stateName, stateParam)
    if not stateName then
        LogE("MVCServiceClass.PushState : invalid parameter")
        return
    end

    self._uiStateService:PushState(stateName, stateParam)
end

-- 压入多个状态
-- @param stateName 多个状态名（以/分割多个状态名）
-- @param lastStateParam 最后一个状态参数
function MVCServiceClass:PushMultiUIState(stateName, lastStateParam)
    if not stateName then
        LogE("MVCServiceClass.PushState : invalid parameter")
        return
    end
    self._uiStateService:PushMultiState(stateName, lastStateParam)
end

-- 弹出UI状态
-- @param callChangeStateHandler 是否调用状态切换回调（默认为true）
-- @param newPopStateParam 新的状态切换参数（默认为null）
-- @return 无
function MVCServiceClass:PopUIState(callChangeStateHandler, newPopStateParam)
    if callChangeStateHandler ~= nil and newPopStateParam then
        self._uiStateService:PopState(callChangeStateHandler, newPopStateParam)
    elseif callChangeStateHandler ~= nil then
        self._uiStateService:PopState(callChangeStateHandler)
    else
        self._uiStateService:PopState()
    end
end

-- 清除状态
-- @param stateName 待清除的状态，不能为栈顶的状态，=nil时清除所有状态
-- @return 无
function MVCServiceClass:ClearUIState(stateName)
    self._uiStateService:ClearState(stateName)
end

--[[
    @desc: 重置UI状态参数 
    --@stateName:状态名
	--@stateParam: 参数
    @return:返回空
]]
function MVCServiceClass:SetUIStateParam(stateName, stateParam)
    if not stateName then
        LogE("MVCServiceClass.SetStateParam : invalid parameter")
        return
    end
    self._uiStateService:SetStateParam(stateName, stateParam)
end

--[[
    @desc: 打开窗口
    --@prefab: 窗口实例
    @return:
]]
function MVCServiceClass:OpenForm(formClass)
    if not formClass then
        LogE("MVCServiceClass.OpenForm : invalid parameter")
        return
    end
    self._uguiRoot:OpenForm(formClass._form)
end

--[[
    @desc: 关闭窗口
    --@prefab: 窗口
    @return:
]]
function MVCServiceClass:CloseForm(formClass)
    if not formClass then
        LogE("MVCServiceClass.CloseForm : invalid parameter")
        return
    end
    self._uguiRoot:CloseForm(formClass._form)
end
--[[
    @desc: 获取当前UIState

    @return:UIState Name
]]
function MVCServiceClass:GetCurUIStateName()
    return self._uiStateService.CurrentStateName
end
MVCService = MVCServiceClass.new()
