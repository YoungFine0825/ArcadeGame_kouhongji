--[[********************************************************************
*		作者： XH
*		时间： 2018-12-11
*		描述： 金币不足界面
*********************************************************************--]]

local LSKNoEnoughCoinFormClass = DeclareClass("LSKNoEnoughCoinFormClass", ClassLib.UIFormClass)
function LSKNoEnoughCoinFormClass:ctor()
	self._uiQRImgQRImageIndex = 0;
	self._uiCountDownTextIndex = 1;
	self._uiBottomRectTransformIndex = 2;
	self._uiAnimAnimationIndex = 3;
	
	self._uiQRImgQRImage = false
	self._uiCountDownText = false
	self._uiBottomRectTransform = false
	self._uiAnimAnimation = false
	

	self._bottomGo = false
end

function LSKNoEnoughCoinFormClass:vGetPath()
	return 'LSK/Select/LSKNoEnoughCoinForm'
end

function LSKNoEnoughCoinFormClass:vOnResourceLoaded()
	self._uiQRImgQRImage = self:GetComponent(self._uiQRImgQRImageIndex)
	self._uiCountDownText = self:GetComponent(self._uiCountDownTextIndex)
	self._uiBottomRectTransform = self:GetComponent(self._uiBottomRectTransformIndex)
	self._uiAnimAnimation = self:GetComponent(self._uiAnimAnimationIndex)
	
	self._bottomGo = self:GetGameObject(self._uiBottomRectTransformIndex)
end

function LSKNoEnoughCoinFormClass:vOnResourceUnLoaded()
	self._uiQRImgQRImage = false
	self._uiCountDownText = false
	self._uiBottomRectTransform = false
	self._uiAnimAnimation = false
	
	self._bottomGo = false
end

function LSKNoEnoughCoinFormClass:vOnInitialize(argument)
    if self._uiQRImgQRImage then
        self._uiQRImgQRImage:SetQRUrl(LSKData.LoginQrCode)
	end
	if self._bottomGo then
		self._bottomGo:SetActive(false)
	end
	if self._uiAnimAnimation then
		self._uiAnimAnimation:Play('Anim_Open')
	end
end

function LSKNoEnoughCoinFormClass:vOnUninitialize()

end

function LSKNoEnoughCoinFormClass:ShowCountdown(seconds)
	if self._bottomGo then
		self._bottomGo:SetActive(true)
	end
	self:Countdown(seconds)
end

function LSKNoEnoughCoinFormClass:Countdown(seconds)
	if self._uiCountDownText then
		self._uiCountDownText.text = tostring(seconds)
	end
end

function LSKNoEnoughCoinFormClass:Close()
	if self._uiAnimAnimation then
		self._uiAnimAnimation:Play('Anim_Close')
	end
	ScheduleService:AddTimer(self,self.OnDelayClose,0.4,false)
end

function LSKNoEnoughCoinFormClass:OnDelayClose()
	self:Destroy()
end
