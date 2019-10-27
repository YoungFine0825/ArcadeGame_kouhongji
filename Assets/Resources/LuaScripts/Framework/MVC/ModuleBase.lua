--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-07-04
*		描述： 模块基类
*********************************************************************--]]
local ModuleBaseClass = DeclareClass("ModuleBaseClass")

function ModuleBaseClass:ctor()
    self._mediator = {}
end

function ModuleBaseClass:Create()
    self:vOnInitializeModule()
end

function ModuleBaseClass:Destroy()
    
    self:vOnUninitializeModule()

    for i = 1, #self._mediator do
        local mediator = self._mediator[i]
        mediator:Destroy()
    end
    self._mediator = {}
end

function ModuleBaseClass:CreateMediator(mediatorClass)
    if not mediatorClass then
        LogE("ModuleBaseClass.CreateMediator : invalid parameter")
        return
    end
    
    local mediator = mediatorClass.new()
    mediator:Create(self)
    
    self._mediator[#self._mediator+1] = mediator
end


function ModuleBaseClass:ChangeGameState(op,state1, state2, userData)
    
    local mvc = MVCService
    if op == mvc.EnterGameStateOp then
        self:vOnGameStateEnter(state2, state1, userData)
        
    elseif op == mvc.LeaveGameStateOp then
        self:vOnGameStateLeave(state1, state2, userData)
    end
    return true
end

function ModuleBaseClass:ChangeUIState(callType, changeType, oldStateName, newStateName, stateParam)
    for i = 1, #self._mediator do
        local mediator = self._mediator[i]
        mediator:ChangeUIState(callType, changeType, oldStateName, newStateName, stateParam)
    end
end

function ModuleBaseClass:vOnInitializeModule()
end


function ModuleBaseClass:vOnUninitializeModule()
end

function ModuleBaseClass:vOnGameStateEnter(state, fromState, userData)
end


function ModuleBaseClass:vOnGameStateLeave(state, toState, userData)
end


function ModuleBaseClass:vOnAction(id, argument)
end

function ModuleBaseClass:UpdateUI(id, argument)
    if not id then
        LogE("UIMediatorClass.SendUpdateUI : invalid parameter")
        return
    end
    for i = 1, #self._mediator do
        local mediator = self._mediator[i]
        mediator:UpdateUI(id, argument, true)
    end
end




