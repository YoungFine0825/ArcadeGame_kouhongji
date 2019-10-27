--[[********************************************************************
*		作者： yangfan
*		时间： 2018-12-07
*		描述： 娃娃机个人信息界面
*********************************************************************--]]

local WWJPlayerInfoFormClass = DeclareClass("WWJPlayerInfoFormClass", ClassLib.UIFormClass)
function WWJPlayerInfoFormClass:ctor()
	self._uiBalanceTextIndex = 0;
	self._uiConvertiblePrizeTextIndex = 1;
	self._uiPortraitHttpImageIndex = 2;
	self._uiLevelTextIndex = 3;

	self._uiBalanceText = false
	self._uiConvertiblePrizeText = false
	self._uiPortraitHttpImage = false
	self._uiLevelText = false

end

function WWJPlayerInfoFormClass:vGetPath()
	return 'WWJ/PlayerInfo/WWJPlayerInfoForm'
end

function WWJPlayerInfoFormClass:vOnResourceLoaded()
	self._uiBalanceText = self:GetComponent(self._uiBalanceTextIndex)
	self._uiConvertiblePrizeText = self:GetComponent(self._uiConvertiblePrizeTextIndex)
	self._uiPortraitHttpImage = self:GetComponent(self._uiPortraitHttpImageIndex)
	self._uiLevelText = self:GetComponent(self._uiLevelTextIndex)

end

function WWJPlayerInfoFormClass:vOnResourceUnLoaded()
	self._uiBalanceText = false
	self._uiConvertiblePrizeText = false
	self._uiPortraitHttpImage = false
	self._uiLevelText = false

end

function WWJPlayerInfoFormClass:vOnInitialize(argument)

end

function WWJPlayerInfoFormClass:vOnUninitialize()

end

function WWJPlayerInfoFormClass:UpdateUI(id,argument)

end

