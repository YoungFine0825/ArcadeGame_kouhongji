--[[********************************************************************
*		作者： XH
*		时间： 2018-12-11
*		描述： 保底购买UI
*********************************************************************--]]

local LSKSecurityBuyFormClass = DeclareClass("LSKSecurityBuyFormClass", ClassLib.UIFormClass)
function LSKSecurityBuyFormClass:ctor()
    self._uiDescTextIndex = 0;
    self._uiOfficialPriceTextIndex = 1;
    self._uiCoinBuyTextIndex = 2;
    self._uiContentHttpImageIndex = 3;
    self._uiCountdownTextIndex = 4;
    self._uiNeedCoinTextIndex = 5;
    self._uiConfirmSelectRectTransformIndex = 6;
    self._uiCancelSelectRectTransformIndex = 7;
    self._uiQRCodeQRImageIndex = 8;
    self._uiBuyStepRectTransformIndex = 9;
    self._uiBuySuccRectTransformIndex = 10;
    self._uiAnimAnimationIndex = 11;
    
    self._uiDescText = false
    self._uiOfficialPriceText = false
    self._uiCoinBuyText = false
    self._uiContentHttpImage = false
    self._uiCountdownText = false
    self._uiNeedCoinText = false
    self._uiConfirmSelectRectTransform = false
    self._uiCancelSelectRectTransform = false
    self._uiQRCodeQRImage = false
    self._uiBuyStepRectTransform = false
    self._uiBuySuccRectTransform = false
    self._uiAnimAnimation = false
    
    self._askBuyGo = false
    self._buySuccGo = false
    self._confirmSelectGo = false
    self._cancelSelectGo = false
end

function LSKSecurityBuyFormClass:vGetPath()
	return 'LSK/Play/LSKSecurityBuyForm'
end

function LSKSecurityBuyFormClass:vOnResourceLoaded()
    self._uiDescText = self:GetComponent(self._uiDescTextIndex)
    self._uiOfficialPriceText = self:GetComponent(self._uiOfficialPriceTextIndex)
    self._uiCoinBuyText = self:GetComponent(self._uiCoinBuyTextIndex)
    self._uiContentHttpImage = self:GetComponent(self._uiContentHttpImageIndex)
    self._uiCountdownText = self:GetComponent(self._uiCountdownTextIndex)
    self._uiNeedCoinText = self:GetComponent(self._uiNeedCoinTextIndex)
    self._uiConfirmSelectRectTransform = self:GetComponent(self._uiConfirmSelectRectTransformIndex)
    self._uiCancelSelectRectTransform = self:GetComponent(self._uiCancelSelectRectTransformIndex)
    self._uiQRCodeQRImage = self:GetComponent(self._uiQRCodeQRImageIndex)
    self._uiBuyStepRectTransform = self:GetComponent(self._uiBuyStepRectTransformIndex)
    self._uiBuySuccRectTransform = self:GetComponent(self._uiBuySuccRectTransformIndex)
    self._uiAnimAnimation = self:GetComponent(self._uiAnimAnimationIndex)
    
    self._askBuyGo = self:GetGameObject(self._uiBuyStepRectTransformIndex)
    self._buySuccGo = self:GetGameObject(self._uiBuySuccRectTransformIndex)
    self._confirmSelectGo = self:GetGameObject(self._uiConfirmSelectRectTransformIndex)
    self._cancelSelectGo = self:GetGameObject(self._uiCancelSelectRectTransformIndex)
    self._confirmSelectGo:SetActive(false)
    self._cancelSelectGo:SetActive(false)
end

function LSKSecurityBuyFormClass:vOnResourceUnLoaded()
    self._uiDescText = false
    self._uiOfficialPriceText = false
    self._uiCoinBuyText = false
    self._uiContentHttpImage = false
    self._uiCountdownText = false
    self._uiNeedCoinText = false
    self._uiConfirmSelectRectTransform = false
    self._uiCancelSelectRectTransform = false
    self._uiQRCodeQRImage = false
    self._uiBuyStepRectTransform = false
    self._uiBuySuccRectTransform = false
    self._uiAnimAnimation = false
    
    self._askBuyGo = false
    self._buySuccGo = false
    self._confirmSelectGo = false
    self._cancelSelectGo = false
end

function LSKSecurityBuyFormClass:vOnInitialize(argument)
    if not argument then
        return
    end
    if self._askBuyGo then
        self._askBuyGo:SetActive(true)
    end
    if self._buySuccGo then
        self._buySuccGo:SetActive(false)
    end
    local id = argument.prizeId
    local cost = argument.cost
    if self._uiNeedCoinText then
        self._uiNeedCoinText.text = tostring(cost)
    end
    if self._uiCoinBuyText then
        self._uiCoinBuyText.text = '抵扣价格：   '..tostring(cost)
    end
    local config = PrizeSystem:GetPrizeInfo(id)
    if config then
        if self._uiDescText then
            self._uiDescText.text = config.Desc
        end
        if self._uiOfficialPriceText then
            self._uiOfficialPriceText.text = tostring(config.ShowPrice)
        end
        if self._uiContentHttpImage then
            self._uiContentHttpImage:SetSprite(LSKUtil:GetEmptySprite())
            self._uiContentHttpImage:SetImageUrl(config:GetIconUrl(3))
        end
    end

    if self._uiQRCodeQRImage then
        self._uiQRCodeQRImage:SetQRUrl(LSKData.ExchangeQrCode)
    end

    if self._uiAnimAnimation then
        self._uiAnimAnimation:Play('Anim_Open')
    end
end

function LSKSecurityBuyFormClass:vOnUninitialize()
    ScheduleService:RemoveTimer(self)
end

function LSKSecurityBuyFormClass:Countdown(seconds)
    if self._uiCountdownText then
        self._uiCountdownText.text = tostring(seconds)
    end
end

function LSKSecurityBuyFormClass:OnBuySuccess()
    if self._askBuyGo then
        self._askBuyGo:SetActive(false)
    end
    if self._buySuccGo then
        self._buySuccGo:SetActive(true)
    end
end

function LSKSecurityBuyFormClass:OnSelect(isConfirm)
    if self._confirmSelectGo then
        self._confirmSelectGo:SetActive(isConfirm)
    end
    if self._cancelSelectGo then
        self._cancelSelectGo:SetActive(not isConfirm)
    end
end

function LSKSecurityBuyFormClass:Close()
	if self._uiAnimAnimation then
		self._uiAnimAnimation:Play('Anim_Close')
	end
	ScheduleService:AddTimer(self,self.OnDelayClose,0.4,false)
end

function LSKSecurityBuyFormClass:OnDelayClose()
	self:Destroy()
end