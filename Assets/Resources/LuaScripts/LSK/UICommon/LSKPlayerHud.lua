local LSKPlayerHudClass = DeclareClass("LSKPlayerHudClass", ClassLib.UIFormClass)
function LSKPlayerHudClass:ctor()
    self._uiHeadImgHttpImageIndex = 0;
    self._uiCanCollectNumTextIndex = 1;
    self._uiLeftCoinTextIndex = 2;
    self._uiQRImgQRImageIndex = 3;
    self._uiFlyQRQRImageIndex = 4;
    self._uiFlyQrCodeRectTransformIndex = 5;
    self._uiRightBottomRectTransformIndex = 6;
    self._uiDropCoinAnimTransformIndex = 7;
    self._uiTextTextIndex = 8;
    self._uiCreditsNumTextIndex = 9
    
    self._uiHeadImgHttpImage = false
    self._uiCanCollectNumText = false
    self._uiLeftCoinText = false
    self._uiQRImgQRImage = false
    self._uiFlyQRQRImage = false
    self._uiFlyQrCodeRectTransform = false
    self._uiRightBottomRectTransform = false
    self._uiDropCoinAnimTransform = false
    self._uiTextText = false
    
    self._originPos = false
    self._flyGo = false
    self._endPos = false
    self._uiDropCoinAnimGo = false
    self._uiHeadImgGo = false
    self._rightBottomGo = false
    self._uiCreditsNumText = false
end

function LSKPlayerHudClass:vGetPath()
	return 'LSK/UICommon/LSKPlayerHud'
end

function LSKPlayerHudClass:vOnResourceLoaded()
    self._uiHeadImgHttpImage = self:GetComponent(self._uiHeadImgHttpImageIndex)
    self._uiCanCollectNumText = self:GetComponent(self._uiCanCollectNumTextIndex)
    self._uiLeftCoinText = self:GetComponent(self._uiLeftCoinTextIndex)
    self._uiQRImgQRImage = self:GetComponent(self._uiQRImgQRImageIndex)
    self._uiFlyQRQRImage = self:GetComponent(self._uiFlyQRQRImageIndex)
    self._uiFlyQrCodeRectTransform = self:GetComponent(self._uiFlyQrCodeRectTransformIndex)
    self._uiRightBottomRectTransform = self:GetComponent(self._uiRightBottomRectTransformIndex)
    self._uiDropCoinAnimTransform = self:GetComponent(self._uiDropCoinAnimTransformIndex)
    self._uiTextText = self:GetComponent(self._uiTextTextIndex)
    
    if not self._originPos then
        self._originPos = self._uiFlyQrCodeRectTransform.localPosition
    end
    self._endPos = self._uiRightBottomRectTransform.localPosition
    self._flyGo = self:GetGameObject(self._uiFlyQrCodeRectTransformIndex)
    self._uiDropCoinAnimGo = self:GetGameObject(self._uiDropCoinAnimTransformIndex)
    self._uiHeadImgGo = self:GetGameObject(self._uiHeadImgHttpImageIndex)
    self._rightBottomGo = self:GetGameObject(self._uiRightBottomRectTransformIndex)
    self._uiCreditsNumText = self:GetComponent(self._uiCreditsNumTextIndex)
end

function LSKPlayerHudClass:vOnResourceUnLoaded()
    self._uiHeadImgHttpImage = false
    self._uiCanCollectNumText = false
    self._uiLeftCoinText = false
    self._uiQRImgQRImage = false
    self._uiFlyQRQRImage = false
    self._uiFlyQrCodeRectTransform = false
    self._uiRightBottomRectTransform = false
    self._uiDropCoinAnimTransform = false
    self._uiTextText = false
    
    self._uiHeadImgGo = false
    self._uiDropCoinAnimGo = false
    self._uiCreditsNumText = false
end

function LSKPlayerHudClass:vOnInitialize(argument)
    self._flyGo:SetActive(false)
    self._uiHeadImgGo:SetActive(false)
    self._rightBottomGo:SetActive(false)
end

function LSKPlayerHudClass:vOnUninitialize()
    self._uiFlyQrCodeRectTransform.localPosition = self._originPos
    self._originPos = false
end

function LSKPlayerHudClass:PlayFlyQR()
    self._uiFlyQrCodeRectTransform.localPosition = self._originPos
    self._uiFlyQrCodeRectTransform.localScale = Vector3_One
    self._flyGo:SetActive(true)

    local sequence = CS.DG.Tweening.DOTween.Sequence()
    local move = self._uiFlyQrCodeRectTransform:DOLocalMove(self._endPos, 0.5)
    local scale1 = self._uiFlyQrCodeRectTransform:DOScale(1.2, 0.2)
    local scale2 = self._uiFlyQrCodeRectTransform:DOScale(0.8, 0.3)
    local scale3 = self._uiFlyQrCodeRectTransform:DOScale(0, 0.25)
    sequence:Insert(0, scale1)
    sequence:Insert(0.2, move)
    sequence:Insert(0.2, scale2)
    sequence:Insert(0.5, scale3)
    sequence:AppendCallback(function()
        self._flyGo:SetActive(false)
        self._uiFlyQrCodeRectTransform.localPosition = self._originPos
        self._uiFlyQrCodeRectTransform.localScale = Vector3_One
    end)
end

function LSKPlayerHudClass:Show()
    self:ActiveForm(true)
    if self._uiDropCoinAnimGo then
        self._uiDropCoinAnimGo:SetActive(false)
    end
    
end

function LSKPlayerHudClass:Hide()
    self:ActiveForm(false)
end


function LSKPlayerHudClass:PlayDropCoin(coin)
    if self._uiTextText then
        self._uiTextText.text = '+'..tostring(coin)
    end
    if self._uiDropCoinAnimGo then
        self._uiDropCoinAnimGo:SetActive(false)
        self._uiDropCoinAnimGo:SetActive(true)
    end
end

function LSKPlayerHudClass:Refresh()
    local isLogin = LSKData:IsLogin()
    if self._uiHeadImgGo then
        self._uiHeadImgGo:SetActive(isLogin)
    end

    if self._uiLeftCoinText then
        self._uiLeftCoinText.text = isLogin and tostring(LSKData.User.DropCoin) or '0'
    end
    if self._uiCanCollectNumText then
        self._uiCanCollectNumText.text = isLogin and tostring(LSKData.User.PrizeCount) or '0'
    end

    if self._uiCreditsNumText then
        self._uiCreditsNumText.text = isLogin and tostring(LSKData.User.Credits) or '0'
    end

    if isLogin then
        if self._uiHeadImgHttpImage then
            self._uiHeadImgHttpImage:SetImageUrl(LSKData.User.HeadUrl)
        end
    end

    if self._uiQRImgQRImage then
        local qrUrl = isLogin and LSKData.ExchangeQrCode or LSKData.LoginQrCode
        self._uiQRImgQRImage:SetQRUrl(qrUrl)
    end

    if self._uiFlyQRQRImage then
        self._uiFlyQRQRImage:SetQRUrl(LSKData.ExchangeQrCode)
    end
    
    if self._rightBottomGo then
        self._rightBottomGo:SetActive(LSKBoot:CanShowAwardQRCode())
    end
end
