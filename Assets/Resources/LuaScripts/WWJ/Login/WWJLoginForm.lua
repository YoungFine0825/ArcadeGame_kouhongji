--[[********************************************************************
*		作者： yangfan
*		时间： 2018-12-4
*		描述： 娃娃机登录窗口
*********************************************************************--]]
local WWJLoginFormClass = DeclareClass("WWJLoginFormClass", ClassLib.UIFormClass)
function WWJLoginFormClass:ctor()
    self._uiInfoTextIndex = 0
    self._uiProgressSliderIndex = 1
    self._uiProgressTipTextIndex = 2

    self._uiInfoText = false
    self._uiProgressSlider = false
    self._uiProgressSliderGo = false
    self._uiProgressTipText = false
end

function WWJLoginFormClass:vGetPath()
    return "WWJ/Login/WWJLoginForm"
end

function WWJLoginFormClass:vOnResourceLoaded()
    self._uiInfoText = self:GetComponent(self._uiInfoTextIndex)
    self._uiProgressSlider = self:GetComponent(self._uiProgressSliderIndex)
    if self._uiProgressSlider then
        self._uiProgressSliderGo = self._uiProgressSlider.gameObject
    end
    self._uiProgressTipText = self:GetComponent(self._uiProgressTipTextIndex)
end

function WWJLoginFormClass:vOnResourceUnLoaded()
    self._uiInfoText = false
    self._uiProgressSlider = false
    self._uiProgressSliderGo = false
    self._uiProgressTipText = false
end

function WWJLoginFormClass:vOnInitialize(argument)

    if self._uiProgressSlider then
        self._uiProgressSlider.value = 0
    end
end

function WWJLoginFormClass:vOnUninitialize()

end

function WWJLoginFormClass:UpdateUI(id, argument)
    if id == "UpdateTip" then
        if self._uiInfoText then
            self._uiInfoText.text = argument
        end
    end
    if id == "UpdateProgress" then
        if self._uiProgressSlider then
            self._uiProgressSlider.value = argument
            self._uiProgressTipText.text = (argument * 100) .. "%"
        end
    end
end