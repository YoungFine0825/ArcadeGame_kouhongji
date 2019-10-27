--[[********************************************************************
*		作者： XH
*		时间： 2018-12-11
*		描述： 扫码开始界面
*********************************************************************--]]

local LSKScanStartFormClass = DeclareClass("LSKScanStartFormClass", ClassLib.UIFormClass)
function LSKScanStartFormClass:ctor()
	self._uiQRImgQRImageIndex = 0;
	self._uiAnimAnimationIndex = 1;
	
	self._uiQRImgQRImage = false
	self._uiAnimAnimation = false
	
end

function LSKScanStartFormClass:vGetPath()
	return 'LSK/Select/ScanStartForm'
end

function LSKScanStartFormClass:vOnResourceLoaded()
	self._uiQRImgQRImage = self:GetComponent(self._uiQRImgQRImageIndex)
	self._uiAnimAnimation = self:GetComponent(self._uiAnimAnimationIndex)
	
end

function LSKScanStartFormClass:vOnResourceUnLoaded()
	self._uiQRImgQRImage = false
	self._uiAnimAnimation = false
	
end

function LSKScanStartFormClass:vOnInitialize(argument)
	LogD('Set Scan Url:'..LSKData.LoginQrCode)
	if self._uiQRImgQRImage then
		self._uiQRImgQRImage:SetQRUrl(LSKData.LoginQrCode)
	end
	if self._uiAnimAnimation then
		self._uiAnimAnimation:Play('Anim_Open')
	end
end

function LSKScanStartFormClass:vOnUninitialize()

end

function LSKScanStartFormClass:Close()
	if self._uiAnimAnimation then
		self._uiAnimAnimation:Play('Anim_Close')
	end
	ScheduleService:AddTimer(self,self.OnDelayClose,0.4,false)
end

function LSKScanStartFormClass:OnDelayClose()
	self:Destroy()
end