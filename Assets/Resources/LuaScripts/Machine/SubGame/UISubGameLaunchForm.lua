--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-16
*		描述： 子游戏启动窗口
*********************************************************************--]]
local UISubGameLaunchFormClass = DeclareClass("UISubGameLaunchFormClass", ClassLib.UIFormClass)

function UISubGameLaunchFormClass:ctor()

    self._uiProgressSliderIndex = 0;
    self._uiTipTextTextIndex = 1;

    self._uiProgressSlider = false
    self._uiTipTextText = false


end

function UISubGameLaunchFormClass:vGetPath()
    return "Login/UISubGameLaunchForm"
end

function UISubGameLaunchFormClass:vOnResourceLoaded()

    self._uiProgressSlider = self:GetComponent(self._uiProgressSliderIndex)
    self._uiTipTextText = self:GetComponent(self._uiTipTextTextIndex)

end

function UISubGameLaunchFormClass:vOnResourceUnLoaded()
    
    self._uiProgressSlider = false
    self._uiTipTextText = false

end


function UISubGameLaunchFormClass:vOnInitialize(argument)
    
  
end

function UISubGameLaunchFormClass:vOnUninitialize()

  

end

function UISubGameLaunchFormClass:vOnUpdateUI(id, argument)
   
    if id=="SubGameLaunchEvent" then

        if self._uiTipTextText then
            self._uiTipTextText.text=argument.StateInfo
        end
        if self._uiProgressSlider then
            self._uiProgressSlider.value=argument.ProgressValue
        end

    end

end

