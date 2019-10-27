--[[********************************************************************
*		作者： yangfan
*		时间： 2018-12-13
*		描述： 抓取成功界面
*********************************************************************--]]
local WWJSuccessFormClass = DeclareClass("WWJSuccessFormClass", ClassLib.UIFormClass)
function WWJSuccessFormClass:ctor()
	self._uiPrizeImgHttpImageIndex = 0;
	self._uiQRCodeHttpImageIndex = 1;
	self._uiPrizeCountTextIndex = 2;

	self._uiPrizeImgHttpImage = false
	self._uiQRCodeHttpImage = false
	self._uiPrizeCountText = false

end

function WWJSuccessFormClass:vGetPath()
	return 'WWJ/Game/WWJSuccessForm'
end

function WWJSuccessFormClass:vOnResourceLoaded()
	self._uiPrizeImgHttpImage = self:GetComponent(self._uiPrizeImgHttpImageIndex)
	self._uiQRCodeHttpImage = self:GetComponent(self._uiQRCodeHttpImageIndex)
	self._uiPrizeCountText = self:GetComponent(self._uiPrizeCountTextIndex)

end

function WWJSuccessFormClass:vOnResourceUnLoaded()
	self._uiPrizeImgHttpImage = false
	self._uiQRCodeHttpImage = false
	self._uiPrizeCountText = false

end

function WWJSuccessFormClass:vOnInitialize(argument)

end

function WWJSuccessFormClass:vOnUninitialize()

end

