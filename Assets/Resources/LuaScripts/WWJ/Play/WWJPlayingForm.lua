--[[********************************************************************
*		作者： yangfan
*		时间： 2018-12-12
*		描述： desc
*********************************************************************--]]
local WWJPlayingFormClass = DeclareClass("WWJPlayingFormClass", ClassLib.UIFormClass)
function WWJPlayingFormClass:ctor()
	self._uiCntDownTextIndex = 0;

	self._uiCntDownText = false

end

function WWJPlayingFormClass:vGetPath()
	return 'WWJ/Game/WWJPlayingForm'
end

function WWJPlayingFormClass:vOnResourceLoaded()
	self._uiCntDownText = self:GetComponent(self._uiCntDownTextIndex)

end

function WWJPlayingFormClass:vOnResourceUnLoaded()
	self._uiCntDownText = false

end

function WWJPlayingFormClass:vOnInitialize(argument)

end

function WWJPlayingFormClass:vOnUninitialize()

end

function WWJPlayingFormClass:UpdateUI(id,argument)
    if id == "UpdateCntDownText" then
        if self._uiCntDownText then
            self._uiCntDownText.text = argument
        end
    end
end