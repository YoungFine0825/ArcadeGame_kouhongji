--[[********************************************************************
*		作者： yangfan
*		时间： 2018-12-12
*		描述： 准备界面
*********************************************************************--]]
local WWJReadyFormClass = DeclareClass("WWJReadyFormClass", ClassLib.UIFormClass)
function WWJReadyFormClass:ctor()
	self._uiCntDownTextIndex = 0;

	self._uiCntDownText = false

end

function WWJReadyFormClass:vGetPath()
	return 'WWJ/Game/WWJReadyForm'
end

function WWJReadyFormClass:vOnResourceLoaded()
	self._uiCntDownText = self:GetComponent(self._uiCntDownTextIndex)

end

function WWJReadyFormClass:vOnResourceUnLoaded()
	self._uiCntDownText = false

end

function WWJReadyFormClass:vOnInitialize(argument)

end

function WWJReadyFormClass:vOnUninitialize()

end

function WWJReadyFormClass:UpdateUI(id,argument)
    if id == "UpdateCntDown" then
        if self._uiCntDownText then
            self._uiCntDownText.text = argument
        end
    end
end

