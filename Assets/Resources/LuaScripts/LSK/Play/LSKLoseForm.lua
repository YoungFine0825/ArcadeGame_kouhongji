local LSKLoseFormClass = DeclareClass("LSKLoseFormClass", ClassLib.UIFormClass)
function LSKLoseFormClass:ctor()
	self._uiLeftSecondsTextIndex = 0;
	self._uiNeedCoinTextIndex = 1;
	self._uiAnimAnimationIndex = 2;
	self._uiCountDownTextIndex = 3;
	self._uiConfirmSelectRectTransformIndex = 4;
	self._uiCancelSelectRectTransformIndex = 5;
	self._uiUsedTextIndex = 6;
	
	self._uiLeftSecondsText = false
	self._uiNeedCoinText = false
	self._uiAnimAnimation = false
	self._uiCountDownText = false
	self._uiConfirmSelectRectTransform = false
	self._uiCancelSelectRectTransform = false
	self._uiUsedText = false
	

	self._retrySelectGo = false
	self._cancelSelectGo = false
	
end

function LSKLoseFormClass:vGetPath()
	return 'LSK/Play/LSKLoseForm'
end

function LSKLoseFormClass:vOnResourceLoaded()
	self._uiLeftSecondsText = self:GetComponent(self._uiLeftSecondsTextIndex)
	self._uiNeedCoinText = self:GetComponent(self._uiNeedCoinTextIndex)
	self._uiAnimAnimation = self:GetComponent(self._uiAnimAnimationIndex)
	self._uiCountDownText = self:GetComponent(self._uiCountDownTextIndex)
	self._uiConfirmSelectRectTransform = self:GetComponent(self._uiConfirmSelectRectTransformIndex)
	self._uiCancelSelectRectTransform = self:GetComponent(self._uiCancelSelectRectTransformIndex)
	self._uiUsedText = self:GetComponent(self._uiUsedTextIndex)
	
	self._retrySelectGo = self:GetGameObject(self._uiConfirmSelectRectTransformIndex)
	self._cancelSelectGo = self:GetGameObject(self._uiCancelSelectRectTransformIndex)

	self._retrySelectGo:SetActive(false)
	self._cancelSelectGo:SetActive(false)
end

function LSKLoseFormClass:OnSelect(isRetry)
	self._retrySelectGo:SetActive(isRetry)
	self._cancelSelectGo:SetActive(not isRetry)
end

function LSKLoseFormClass:vOnResourceUnLoaded()
	self._uiLeftSecondsText = false
	self._uiNeedCoinText = false
	self._uiAnimAnimation = false
	self._uiCountDownText = false
	self._uiConfirmSelectRectTransform = false
	self._uiCancelSelectRectTransform = false
	self._uiUsedText = false
	
	self._retrySelectGo = false
	self._cancelSelectGo = false
end

function LSKLoseFormClass:vOnInitialize(argument)
	if argument and self._uiLeftSecondsText then
		self._uiLeftSecondsText.text = tostring(argument)
	end
	
	if self._uiNeedCoinText then
		self._uiNeedCoinText.text = tostring(LSKData.RetryCost)
	end

	if self._uiAnimAnimation then
		self._uiAnimAnimation:Play('Anim_Open')
	end

	if self._uiUsedText then
		local leftStr = string.format( '(%d/%d)', LSKData.MaxRetryCount-LSKData.CurRetryCount, LSKData.MaxRetryCount)
		self._uiUsedText.text = leftStr
	end
end

function LSKLoseFormClass:vOnUninitialize()
	ScheduleService:RemoveTimer(self)
end

function LSKLoseFormClass:vOnUpdateUI(id, arg)
	if id == 'LeftSeconds' and self._uiLeftSecondsText then
		self._uiLeftSecondsText.text = tostring(arg)
	end
end

function LSKLoseFormClass:Close()
	if self._uiAnimAnimation then
		self._uiAnimAnimation:Play('Anim_Close')
	end
	ScheduleService:AddTimer(self,self.OnDelayClose,0.4,false)
end

function LSKLoseFormClass:OnDelayClose()
	self:Destroy()
end