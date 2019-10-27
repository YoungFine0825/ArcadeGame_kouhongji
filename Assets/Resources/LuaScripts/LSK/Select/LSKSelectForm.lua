--[[********************************************************************
*		作者： XH
*		时间： 2018-12-11
*		描述： 口红选择界面
*********************************************************************--]]

local LSKSelectFormClass = DeclareClass("LSKSelectFormClass", ClassLib.UIFormClass)

local TotalCount = 36
local TotalColumn = 6
local TotalLine = 6

function LSKSelectFormClass:ctor()
    self._uiContentRectTransformIndex = 0;
    self._uiSelectLipstickHttpImageIndex = 1;
    self._uiSelectDescTextIndex = 2;
    self._uiSelectPriceDescTextIndex = 3;
    self._uiSelectNeedCoinTextIndex = 4;
    self._uiQRImgQRImageIndex = 5;
    self._uiCountdownLabelTextIndex = 6;
    self._uiQrTipsTextIndex = 7;
    self._uiLoginAnimAnimationIndex = 8;
    self._uiQRCodeRectTransformIndex = 9;
    self._uiPlayTipRectTransformIndex = 10;
    self._uiExperienceToggleIndex = 11;
    
    self._uiContentRectTransform = false
    self._uiSelectLipstickHttpImage = false
    self._uiSelectDescText = false
    self._uiSelectPriceDescText = false
    self._uiSelectNeedCoinText = false
    self._uiQRImgQRImage = false
    self._uiCountdownLabelText = false
    self._uiQrTipsText = false
    self._uiLoginAnimAnimation = false
    self._uiQRCodeRectTransform = false
    self._uiPlayTipRectTransform = false
    self._uiExperienceToggle = false
    
    self._uiQRCodeGo = false
    self._uiLoginAnimGo = false

    self._linesAssets = {}
    self._lipsticks = {}

    self._curSelectIdx = 0
    self._toggleGo = false
end

function LSKSelectFormClass:vGetPath()
	return 'LSK/Select/LSKSelectForm'
end

function LSKSelectFormClass:vOnResourceLoaded()
    self._uiContentRectTransform = self:GetComponent(self._uiContentRectTransformIndex)
    self._uiSelectLipstickHttpImage = self:GetComponent(self._uiSelectLipstickHttpImageIndex)
    self._uiSelectDescText = self:GetComponent(self._uiSelectDescTextIndex)
    self._uiSelectPriceDescText = self:GetComponent(self._uiSelectPriceDescTextIndex)
    self._uiSelectNeedCoinText = self:GetComponent(self._uiSelectNeedCoinTextIndex)
    self._uiQRImgQRImage = self:GetComponent(self._uiQRImgQRImageIndex)
    self._uiCountdownLabelText = self:GetComponent(self._uiCountdownLabelTextIndex)
    self._uiQrTipsText = self:GetComponent(self._uiQrTipsTextIndex)
    self._uiLoginAnimAnimation = self:GetComponent(self._uiLoginAnimAnimationIndex)
    self._uiQRCodeRectTransform = self:GetComponent(self._uiQRCodeRectTransformIndex)
    self._uiPlayTipRectTransform = self:GetComponent(self._uiPlayTipRectTransformIndex)
    self._uiExperienceToggle = self:GetComponent(self._uiExperienceToggleIndex)
    
    self._uiQRCodeGo = self:GetGameObject(self._uiQRCodeRectTransformIndex)
    self._uiLoginAnimGo = self:GetGameObject(self._uiLoginAnimAnimationIndex) 
    self._uiLoginAnimGo:SetActive(false)

    --
    self._uiExperienceToggle = self:GetComponent(self._uiExperienceToggleIndex)
    self._uiExperienceToggle.isOn = false
    self._toggleGo = self:GetGameObject(self._uiExperienceToggleIndex)
    --
end

function LSKSelectFormClass:vOnResourceUnLoaded()
    self._uiContentRectTransform = false
    self._uiSelectLipstickHttpImage = false
    self._uiSelectDescText = false
    self._uiSelectPriceDescText = false
    self._uiSelectNeedCoinText = false
    self._uiQRImgQRImage = false
    self._uiCountdownLabelText = false
    self._uiQrTipsText = false
    self._uiLoginAnimAnimation = false
    self._uiQRCodeRectTransform = false
    self._uiPlayTipRectTransform = false
    self._uiExperienceToggle = false
    
    self._uiQRCodeGo = false
    self._uiLoginAnimGo = false
    self._toggleGo = false

end

function LSKSelectFormClass:vOnInitialize(argument)
    self._curSelectIdx = 0
    self:GenRowContainers()
    self:GenLipsticks()
    self:Show()
    self:Select(1)
    if argument then
        if self._uiCountdownLabelText then
            self._uiCountdownLabelText.text = tostring(argument)
        end
    end
    if self._toggleGo then
        self._toggleGo:SetActive(LSKData.IsCanTrial)
    end
    self:Refresh()
end

function LSKSelectFormClass:vOnUpdateUI(id, arg)
    if id == 'Left_Seconds' then
        local left = math.floor(arg)
        if self._uiCountdownLabelText then
            self._uiCountdownLabelText.text = tostring(left)
        end
    elseif id == 'Select_Lipstick' then
        self:Select(arg)
    elseif id == 'Select_ExperienceMode' then
        if self._uiExperienceToggle then
            self._uiExperienceToggle.isOn = arg
        end
    elseif id == 'On_Player_Login' then
        if arg and self._uiCountdownLabelText then
            self._uiCountdownLabelText.text = tostring(arg)
        end
        self:PlayLoginAnim()
        self:Refresh()
    end
end

function LSKSelectFormClass:Select(idx)
    -- if self._curSelectIdx == idx then
    --     return
    -- end
    self._curSelectIdx = idx
    for k, v in ipairs(self._lipsticks) do
        v:Select(idx)
    end
    if self._curSelectIdx == -1 then
        return
    end

    local lipstick = self._lipsticks[self._curSelectIdx]
    if lipstick then
        local config = LSKData:GetPrizeByIdx(lipstick.Index)
        if config and config.PrizeInfo then
            local prize = config.PrizeInfo
            self._uiSelectDescText.text = prize.Desc
            self._uiSelectNeedCoinText.text = tostring(LSKData.Cost)..'/次'
            self._uiSelectPriceDescText.text = prize.ShowCredits
            self._uiSelectLipstickHttpImage:SetSprite(LSKUtil:GetEmptySprite())
            self._uiSelectLipstickHttpImage:SetImageUrl(prize:GetIconUrl(3))
        end
    end
end

function LSKSelectFormClass:vOnUninitialize()
    self:DestroyLipsticks()
    self:DestroyRowContainers()
    self._curSelectIdx = 0
end

function LSKSelectFormClass:GenRowContainers()
    local lineCnt = TotalCount/TotalColumn
    for i = 1, lineCnt do
        local asset = AssetService:LoadInstantiateAsset('LSK/Select/SelectLineLayout',LifeType.Manual)
        self._linesAssets[#self._linesAssets+1] = asset
        if asset and asset.RootTf then
            asset.RootTf:SetParent(self._uiContentRectTransform)
            asset.RootTf.localPosition = Vector3_Zero
            asset.RootTf.localScale = Vector3_One
        end
    end
end

function LSKSelectFormClass:DestroyRowContainers()
    for i = #self._linesAssets, 1, -1 do
        AssetService:Unload(self._linesAssets[i])
        self._linesAssets[i] = nil
    end
end

function LSKSelectFormClass:GenLipsticks()

    for i = 1, TotalCount do
        local parentIdx = math.floor(((i-1)/TotalColumn))+1
        local parentAsset = self._linesAssets[parentIdx]
        if parentAsset then
            local asset = AssetService:LoadInstantiateAsset('LSK/Select/LipstickItem',LifeType.Manual)
            local lipstick = ClassLib.LSKSelectItemClass.new()
            lipstick:Initialize(asset, i)
            asset.RootTf:SetParent(parentAsset.RootTf)
            asset.RootTf.localPosition = Vector3_Zero
            asset.RootTf.localScale = Vector3_One

            self._lipsticks[i] = lipstick
        else
            LogE('Error')
        end
    end

end

function LSKSelectFormClass:DestroyLipsticks()
    for i = #self._lipsticks, 1, -1 do
        self._lipsticks[i]:Uninitialize()
        self._lipsticks[i] = nil
    end

end

function LSKSelectFormClass:Show()
    for _,v in ipairs(self._linesAssets) do
        v.RootGo:SetActive(true)
    end
    for k, v in ipairs(self._lipsticks) do
        v:Show()
    end
end

function LSKSelectFormClass:PlayLoginAnim()
    if self._uiLoginAnimGo then
        self._uiLoginAnimGo:SetActive(false)
        self._uiLoginAnimGo:SetActive(true)
    end
end


function LSKSelectFormClass:Refresh()
    local isLogin = LSKData:IsLogin()
    if self._uiQRImgQRImage then
        self._uiQRImgQRImage:SetQRUrl(LSKData.LoginQrCode)
    end
    if self._uiQRCodeGo then
        self._uiQRCodeGo:SetActive(not isLogin)
    end
end

function LSKSelectFormClass:IsExperienceTogOn()
    if self._uiExperienceToggle then
        return self._uiExperienceToggle.isOn
    end
end
