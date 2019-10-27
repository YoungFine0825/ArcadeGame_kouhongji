local UISubGameMediatorClass = DeclareClass("UISubGameMediatorClass", ClassLib.UIMediatorClass)

function UISubGameMediatorClass:ctor()
   
    self._launchForm=false
   
end

function UISubGameMediatorClass:vOnInitialize(argument)
    

end

function UISubGameMediatorClass:vOnUninitialize()
    
   if self._launchForm then
        self:DestroyFormClass(self._launchForm)
        self._launchForm=false
   end
   
end

function UISubGameMediatorClass:vGetBelongUIStateName()
    
    return {"UIState_SubGameLaunch"}
    
end


function UISubGameMediatorClass:vOnUIStateIn(oldStateName, newStateName, stateParam,changeType)
    
   if newStateName=="UIState_SubGameLaunch" then

        if not self._launchForm then
            self._launchForm=self:CreateFormClass(ClassLib.UISubGameLaunchFormClass,stateParam)
        end

   end

end


function UISubGameMediatorClass:vOnUIStateOut(oldStateName, newStateName, changeType)
   
    if self._launchForm then
        self:DestroyFormClass(self._launchForm)
        self._launchForm=false
   end
    
end

function UISubGameMediatorClass:vOnAction(id, argument)
    --
  
    return false
end

function UISubGameMediatorClass:vOnUpdateUI(id, argument)

   
end


