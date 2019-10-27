--[[********************************************************************
*		作者： yangfan
*		时间： 2018-12-04
*		描述： 娃娃机登陆界面
*********************************************************************--]]


local WWJWaitFormClass = DeclareClass("WWJWaitFormClass", ClassLib.UIFormClass)
function WWJWaitFormClass:ctor()
	self._uiQRImgQRImageIndex = 0;

	self._uiQRImgQRImage = false

end

function WWJWaitFormClass:vGetPath()
	return 'WWJ/Wait/WWJWaitForm'
end

function WWJWaitFormClass:vOnResourceLoaded()
	self._uiQRImgQRImage = self:GetComponent(self._uiQRImgQRImageIndex)

end

function WWJWaitFormClass:vOnResourceUnLoaded()
	self._uiQRImgQRImage = false

end

function WWJWaitFormClass:vOnInitialize(argument)
	if self._uiQRImgQRImage then
		--self._uiQRImgQRImage:SetQRUrl(LSKData.LoginQrCode)
	end
end

function WWJWaitFormClass:vOnUninitialize()

end

