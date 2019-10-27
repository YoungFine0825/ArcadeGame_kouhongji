--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-01
*		描述： 机台登录中间件
*********************************************************************--]]
local UIMachineLoginMediatorClass = DeclareClass("UIMachineLoginMediatorClass", ClassLib.UIMediatorClass)

function UIMachineLoginMediatorClass:ctor()
    
   self._loginForm=false

end

function UIMachineLoginMediatorClass:vOnInitialize(argument)
    

end

function UIMachineLoginMediatorClass:vOnUninitialize()
    
    if self._loginForm then
        self:DestroyFormClass(self._loginForm)
        self._loginForm=false
    end
   

end

function UIMachineLoginMediatorClass:vGetBelongUIStateName()
    
    return {"UIState_LoginMachine"}
    
end


function UIMachineLoginMediatorClass:vOnUIStateIn(oldStateName, newStateName, stateParam,changeType)
    
    if newStateName=="UIState_LoginMachine" then

        if not self._loginForm then
            self._loginForm=self:CreateFormClass(ClassLib.UIMachineLoginFormClass)
        end

    end

end


function UIMachineLoginMediatorClass:vOnUIStateOut(oldStateName, newStateName, changeType)
   
    if self._loginForm then
        self:DestroyFormClass(self._loginForm)
        self._loginForm=false
    end
    
end

function UIMachineLoginMediatorClass:vOnAction(id, argument)
    --
  
    return false
end

function UIMachineLoginMediatorClass:vOnUpdateUI(id, argument)

    if id=="OnEnterLogin" then
         if not self._loginForm then
            self._loginForm=self:CreateFormClass(ClassLib.UIMachineLoginFormClass)
         end
         return true
    end

    return false
end


