--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-15
*		描述： 机器登录界面
*********************************************************************--]]
local UIMachineLoginFormClass = DeclareClass("UIMachineLoginFormClass", ClassLib.UIFormClass)

function UIMachineLoginFormClass:ctor()
    self._uiTipTextIndex = 0
    self._uiProgressSliderIndex = 1

    self._uiTipText = false
    self._uiProgressSlider = false
end

function UIMachineLoginFormClass:vGetPath()
    return "Login/UILoginForm"
end

function UIMachineLoginFormClass:vOnResourceLoaded()
    self._uiTipText = self:GetComponent(self._uiTipTextIndex)
    self._uiProgressSlider = self:GetComponent(self._uiProgressSliderIndex)
end

function UIMachineLoginFormClass:vOnResourceUnLoaded()
    self._uiTipText = false
    self._uiProgressSlider = false
end


function UIMachineLoginFormClass:vOnInitialize(argument)
    
    if self._uiTipText  then
        self._uiTipText.text = ""
    end
    if self._uiProgressSlider then
        self._uiProgressSlider.value = 0
    end
  
end

function UIMachineLoginFormClass:vOnUninitialize()

  

end

function UIMachineLoginFormClass:vOnUpdateUI(id, argument)
   
    if(id == "ShowTip") then
        if(self._uiTipText) then
            self._uiTipText.text = argument
        end
    end 
    if id == "UpdateProgress" then
        if self._uiProgressSlider then
            self._uiProgressSlider.value = argument
        end
    end  
end

