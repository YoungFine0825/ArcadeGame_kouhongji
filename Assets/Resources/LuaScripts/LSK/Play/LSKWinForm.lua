--[[********************************************************************
*		作者： XH
*		时间： 2018-12-11
*		描述： 挑战成功界面
*********************************************************************--]]

local LSKWinFormClass = DeclareClass("LSKWinFormClass", ClassLib.UIFormClass)
function LSKWinFormClass:ctor()
	self._uiQRImgQRImageIndex = 0;
	self._uiCountDownTextIndex = 1;
	self._uiAnimAnimationIndex = 2;
	self._uiContentHttpImageIndex = 3;
	
	self._uiQRImgQRImage = false
	self._uiCountDownText = false
	self._uiAnimAnimation = false
	self._uiContentHttpImage = false
	
end

function LSKWinFormClass:vGetPath()
	return 'LSK/Play/LSKWinForm'
end

function LSKWinFormClass:vOnResourceLoaded()
	self._uiQRImgQRImage = self:GetComponent(self._uiQRImgQRImageIndex)
	self._uiCountDownText = self:GetComponent(self._uiCountDownTextIndex)
	self._uiAnimAnimation = self:GetComponent(self._uiAnimAnimationIndex)
	self._uiContentHttpImage = self:GetComponent(self._uiContentHttpImageIndex)
	
end

function LSKWinFormClass:vOnResourceUnLoaded()
	self._uiQRImgQRImage = false
	self._uiCountDownText = false
	self._uiAnimAnimation = false
	self._uiContentHttpImage = false
	
end

function LSKWinFormClass:vOnInitialize(argument)
	if self._uiAnimAnimation then
		self._uiAnimAnimation:Play('Win_Once')
	end
	ScheduleService:AddTimer(self,self.OnDelayPlayLoop,1.5,false)
	if argument then
		self:SetCountdown(argument.leftSeconds)
		if self._uiContentHttpImage then
			self._uiContentHttpImage:SetSprite(LSKUtil:GetEmptySprite())
			local prize = PrizeSystem:GetPrizeInfo(argument.prizeId)
			if prize then
				self._uiContentHttpImage:SetImageUrl(prize:GetIconUrl(2))
			end
		end
	end
	if self._uiQRImgQRImage then
		self._uiQRImgQRImage:SetQRUrl(LSKData.ExchangeQrCode)
	end
end

function LSKWinFormClass:OnDelayPlayLoop()
	if self._uiAnimAnimation then
		self._uiAnimAnimation:Play('Win_Loop')
	end
end


function LSKWinFormClass:vOnUninitialize()
	ScheduleService:RemoveTimer(self)
end


function LSKWinFormClass:vOnUpdateUI(id, arg)
	if id == 'Countdown' then
		self:SetCountdown(arg)
	end
end

function LSKWinFormClass:SetCountdown(seconds)
	if self._uiCountDownText then
		self._uiCountDownText.text = tostring(seconds)
	end
end

function LSKWinFormClass:Close()
	if self._uiAnimAnimation then
		self._uiAnimAnimation:Play('Win_End')
	end
	ScheduleService:AddTimer(self,self.OnDelayClose,1,false)
end

function LSKWinFormClass:OnDelayClose()
	self:Destroy()
end