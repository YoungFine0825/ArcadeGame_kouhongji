--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-15
*		描述： 口红机登录窗口
*********************************************************************--]]
local LSKLoginFormClass = DeclareClass("LSKLoginFormClass", ClassLib.UIFormClass)
function LSKLoginFormClass:ctor()
    self._uiInfoTextIndex = 0
    self._uiProgressSliderIndex = 1
    self._uiProgressTipTextIndex = 2

    self._uiInfoText = false
    self._uiProgressSlider = false
    self._uiProgressSliderGo = false
    self._uiProgressTipText = false
end

function LSKLoginFormClass:vGetPath()
    return "LSK/Login/LSKLoginForm"
end

function LSKLoginFormClass:vOnResourceLoaded()
    self._uiInfoText = self:GetComponent(self._uiInfoTextIndex)
    self._uiProgressSlider = self:GetComponent(self._uiProgressSliderIndex)
    if self._uiProgressSlider then
        self._uiProgressSliderGo = self._uiProgressSlider.gameObject
    end
    self._uiProgressTipText = self:GetComponent(self._uiProgressTipTextIndex)
end

function LSKLoginFormClass:vOnResourceUnLoaded()
    self._uiInfoText = false
    self._uiProgressSlider = false
    self._uiProgressSliderGo = false
    self._uiProgressTipText = false
end

function LSKLoginFormClass:vOnInitialize(argument)

    if self._uiProgressSlider then
        self._uiProgressSlider.value = 0
    end
end

function LSKLoginFormClass:vOnUninitialize()

end

function LSKLoginFormClass:UpdateUI(id, argument)
    if id == "UpdateTip" then
        if self._uiInfoText then
            self._uiInfoText.text = argument
        end
    end
    if id == "UpdateProgress" then
        if self._uiProgressSlider then
            self._uiProgressSlider.value = argument
            self._uiProgressTipText.text = math.floor(argument * 100) .. "%"
        end
    end
end
