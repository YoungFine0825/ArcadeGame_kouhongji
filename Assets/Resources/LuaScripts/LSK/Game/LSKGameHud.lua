local LSKGameHudClass = DeclareClass("LSKGameHudClass", ClassLib.UIFormClass)
local GameObject = CS.UnityEngine.GameObject
function LSKGameHudClass:ctor()
	self._uiCountDownTextIndex = 0;
	self._uiLipsticksRectTransformIndex = 1;
	self._uiPinSuccNumTextIndex = 2;
	self._uiRightBottomRectTransformIndex = 3;
	self._uiInSimulateRectTransformIndex = 4;
	self._uiLevelItemRectTransformIndex = 5;
	self._uiLipstickItemRectTransformIndex = 6;
	self._uiNoticeTimeOverRectTransformIndex = 7;
	self._uiEF_Launch_NoticeRectTransformIndex = 8;
	self._uiTimeOutRectTransformIndex = 9;
	self._uiPrizeHttpImageIndex = 10;
	self._uiPrizeValueTextIndex = 11;
	
	self._uiCountDownText = false
	self._uiLipsticksRectTransform = false
	self._uiPinSuccNumText = false
	self._uiRightBottomRectTransform = false
	self._uiInSimulateRectTransform = false
	self._uiLevelItemRectTransform = false
	self._uiLipstickItemRectTransform = false
	self._uiNoticeTimeOverRectTransform = false
	self._uiEF_Launch_NoticeRectTransform = false
	self._uiTimeOutRectTransform = false
	self._uiPrizeHttpImage = false
	self._uiPrizeValueText = false
	
	self._uiTextText = false
	self._uiEF_Notice_TimeoverGo = false	

	self._rootGo = false
	self._lskItems = false
	self._levelItems = false
	self._simulateGo = false
	self._throwNoticeGo = false
	self._lskSize = 0
	self._timeoutGo = false

	self._leftSeconds = false

	self._bulletSpriteAsset = false
	self._bulletBgSpriteAsset = false
	self._bulletAssetName = ''
end

function LSKGameHudClass:vGetPath()
	return 'LSK/Game/LSKGameHud'
end

function LSKGameHudClass:vOnResourceLoaded()
	self._uiCountDownText = self:GetComponent(self._uiCountDownTextIndex)
	self._uiLipsticksRectTransform = self:GetComponent(self._uiLipsticksRectTransformIndex)
	self._uiPinSuccNumText = self:GetComponent(self._uiPinSuccNumTextIndex)
	self._uiRightBottomRectTransform = self:GetComponent(self._uiRightBottomRectTransformIndex)
	self._uiInSimulateRectTransform = self:GetComponent(self._uiInSimulateRectTransformIndex)
	self._uiLevelItemRectTransform = self:GetComponent(self._uiLevelItemRectTransformIndex)
	self._uiLipstickItemRectTransform = self:GetComponent(self._uiLipstickItemRectTransformIndex)
	self._uiNoticeTimeOverRectTransform = self:GetComponent(self._uiNoticeTimeOverRectTransformIndex)
	self._uiEF_Launch_NoticeRectTransform = self:GetComponent(self._uiEF_Launch_NoticeRectTransformIndex)
	self._uiTimeOutRectTransform = self:GetComponent(self._uiTimeOutRectTransformIndex)
	self._uiPrizeHttpImage = self:GetComponent(self._uiPrizeHttpImageIndex)
	self._uiPrizeValueText = self:GetComponent(self._uiPrizeValueTextIndex)
	
	self._uiEF_Notice_TimeoverGo = self:GetGameObject(self._uiNoticeTimeOverRectTransformIndex)
	self._throwNoticeGo = self:GetGameObject(self._uiEF_Launch_NoticeRectTransformIndex)
	self._simulateGo = self:GetGameObject(self._uiInSimulateRectTransformIndex)
	self._timeoutGo = self:GetGameObject(self._uiTimeOutRectTransformIndex)
	self._timeoutGo:SetActive(false)
	self._uiEF_Notice_TimeoverGo:SetActive(false)
end

function LSKGameHudClass:vOnResourceUnLoaded()
	self._uiCountDownText = false
	self._uiLipsticksRectTransform = false
	self._uiPinSuccNumText = false
	self._uiRightBottomRectTransform = false
	self._uiInSimulateRectTransform = false
	self._uiLevelItemRectTransform = false
	self._uiLipstickItemRectTransform = false
	self._uiNoticeTimeOverRectTransform = false
	self._uiEF_Launch_NoticeRectTransform = false
	self._uiTimeOutRectTransform = false
	self._uiPrizeHttpImage = false
	self._uiPrizeValueText = false
	
	self._throwNoticeGo = false
	self._uiEF_Notice_TimeoverGo = false	
	self._rootGo = false
	self._lskItems = false
	self._levelItems = false
	self._timeoutGo = false

	self._simulateGo = false
	self._leftSeconds = false
end

function LSKGameHudClass:vOnInitialize(argument)
	self._rootGo = self:GetRootGo()
	self._lskItems = {}
	self._levelItems = {}
	if self._uiEF_Notice_TimeoverGo then
		self._uiEF_Notice_TimeoverGo:SetActive(false)
	end

	self:GenLevelItems(argument.MaxLevel)
	self:GenLipsticks(argument.MaxLipstick)
	self:SetThrowNoticeActive(false)
	self:Hide()
end

function LSKGameHudClass:vOnUninitialize()

	self:RemoveLevelItems()
	self:RemoveLipsticks()
	if self._bulletSpriteAsset then
		AssetService:Unload(self._bulletSpriteAsset)
		self._bulletSpriteAsset = false
	end
	if self._bulletBgSpriteAsset then
		AssetService:Unload(self._bulletBgSpriteAsset)
		self._bulletBgSpriteAsset = false
	end

	self._bulletAssetName = ''
end


function LSKGameHudClass:Show(prizeInfo)
	if self._rootGo then
		self._rootGo:SetActive(true)
	end
	if self._timeoutGo then
		self._timeoutGo:SetActive(false)
	end
	if self._uiEF_Notice_TimeoverGo then
		self._uiEF_Notice_TimeoverGo:SetActive(false)
	end
	if prizeInfo then
		if self._uiPrizeHttpImage then
			self._uiPrizeHttpImage:SetImageUrl(prizeInfo:GetIconUrl(1))
		end
		if self._uiPrizeValueText then
			self._uiPrizeValueText.text = tostring(prizeInfo.ShowCredits)
		end
	end
end

function LSKGameHudClass:Hide()
	if self._rootGo then
		self._rootGo:SetActive(false)
	end
	self:SetThrowNoticeActive(false)
end

function LSKGameHudClass:SetBullet(bulletName)
	if self._bulletAssetName == bulletName then
		return
	end

	if self._bulletSpriteAsset then
		AssetService:Unload(self._bulletSpriteAsset)
	end
	
	if self._bulletBgSpriteAsset then
		AssetService:Unload(self._bulletBgSpriteAsset)
	end

	self._bulletSpriteAsset = AssetService:LoadSpriteAsset('LSK/UICommon/'..bulletName, LifeType.Manual) or false
	self._bulletBgSpriteAsset = AssetService:LoadSpriteAsset('LSK/UICommon/'..bulletName..'_Bg', LifeType.Manual) or false

	local foreSprite = self._bulletSpriteAsset and self._bulletSpriteAsset.SpriteObj or nil
	local bgSprite = self._bulletBgSpriteAsset and self._bulletBgSpriteAsset.SpriteObj or nil

	for k,v in ipairs(self._lskItems) do
		v.ForeImg.sprite = foreSprite
		v.BgImg.sprite = bgSprite
	end

end

function LSKGameHudClass:GenLipsticks(lskCnt)
	self:RemoveLipsticks()
	if not self._uiLipstickItemRectTransform then
		return
	end

	local obj = self._uiLipstickItemRectTransform.gameObject
	obj:SetActive(true)
	local foreSprite = self._bulletSpriteAsset and self._bulletSpriteAsset.SpriteObj or nil
	local bgSprite = self._bulletBgSpriteAsset and self._bulletBgSpriteAsset.SpriteObj or nil
	for i = 1, lskCnt do
		local objClone = GameObject.Instantiate(obj,self._uiLipsticksRectTransform)
		local prefabLink = objClone:GetComponent('PrefabLink')
		local item = {}
		item.Obj = objClone
		item.FullObj = prefabLink:GetCacheGameObject(0)
		item.ForeImg = prefabLink:GetCacheComponent(1)
		item.BgImg = prefabLink:GetCacheComponent(2)
		if foreSprite and bgSprite then
			item.ForeImg.sprite = foreSprite
			item.BgImg.sprite = bgSprite
		end

		self._lskItems[i] = item
		self._lskItems[i].FullObj:SetActive(true)
	end
	self._lskSize = lskCnt
	obj:SetActive(false)
end

function LSKGameHudClass:RemoveLipsticks()
	if self._lskItems and #self._lskItems > 0 then
		for i=#self._lskItems,1,-1 do
			GameObject.Destroy(self._lskItems[i].Obj)
			table.remove(self._lskItems,i)
		end
	end
	self._lskSize = 0
end

function LSKGameHudClass:GenLevelItems(levelCnt)
	self:RemoveLevelItems()
	if not self._uiLevelItemRectTransform then
		return
	end

	local obj = self._uiLevelItemRectTransform.gameObject
	obj:SetActive(true)
	
	for i = 1, levelCnt do
		local objClone = GameObject.Instantiate(obj,self._uiRightBottomRectTransform)
		local prefabLink = objClone:GetComponent('PrefabLink')
		local item = {}
		item.PassGo = prefabLink:GetCacheGameObject(0)
		item.NoPassGo = prefabLink:GetCacheGameObject(1)
		item.LevelText = prefabLink:GetCacheComponent(2)
		item.NoticeGo = prefabLink:GetCacheGameObject(3)
		local key = levelCnt - i + 1
		item.LevelText.text = tostring(key)
		item.Obj = objClone
		self._levelItems[key] = item
	end
	obj:SetActive(false)
end

function LSKGameHudClass:RemoveLevelItems()
	if self._levelItems and #self._levelItems > 0 then
		for i=#self._levelItems,1,-1 do
			GameObject.Destroy(self._levelItems[i].Obj)
			table.remove(self._levelItems,i)
		end
	end
end

function LSKGameHudClass:SetThrowNoticeActive(isActive)
	if self._throwNoticeGo then
		if isActive then
			self._throwNoticeGo:SetActive(false)
		end
		self._throwNoticeGo:SetActive(isActive)
	end
end
function LSKGameHudClass:vOnUpdateUI(id, arg)
	
end


function LSKGameHudClass:SetLSKSize(size)
	self._lskSize = size
	if size > #self._lskItems then
		self:RemoveLipsticks()
		self:GenLipsticks(size)
	else
		for i=size+1,#self._lskItems do
			self._lskItems[i].Obj:SetActive(false)
		end
		for j=1,size do
			self._lskItems[j].Obj:SetActive(true)
			self._lskItems[j].FullObj:SetActive(true)
		end
	end
end

function LSKGameHudClass:Countdown(seconds)
	if not self._leftSeconds then
		self._leftSeconds = seconds
	end
	if self._leftSeconds == seconds then
		return
	end
	self._leftSeconds = seconds
	if seconds == 10 then
		if self._uiEF_Notice_TimeoverGo then
			self._uiEF_Notice_TimeoverGo:SetActive(true)
			self._uiCountDownText.text = ''
		end
		AudioService:Play(AudioChannelType.ACT_UI, 'LSK/Audio/Voice_1', false)
	end
	if seconds <= 10 and seconds > 0 then
		AudioService:Play(AudioChannelType.ACT_UI, 'LSK/Audio/Audio_CountDown', false)
	else
		if self._uiCountDownText then
			self._uiCountDownText.text = tostring(seconds)
		end
	end
	if seconds == 0 then
		if self._timeoutGo then
			self._timeoutGo:SetActive(false)
			self._timeoutGo:SetActive(true)
		end
		AudioService:Play(AudioChannelType.ACT_UI, 'LSK/Audio/Audio_timeout', false)
	end
end

function LSKGameHudClass:SetSimulate(isSimulate)
	if self._simulateGo then
		self._simulateGo:SetActive(isSimulate)
	end
end

function LSKGameHudClass:SetCurLevel(level)
	if not self._levelItems or #self._levelItems == 0 then
		return
	end
	
	for i=1,#self._levelItems do
		local pass = i<level
		local isCurLevel = i == level
		self._levelItems[i].PassGo:SetActive(pass)
		self._levelItems[i].NoticeGo:SetActive(isCurLevel)
		self._levelItems[i].NoPassGo:SetActive(not pass)
	end
end

function LSKGameHudClass:SetLeftCount(num)
	if not self._lskItems or self._lskSize == 0 then
		return
	end
	local pinSuccCnt = self._lskSize - num
	self._uiPinSuccNumText.text = tostring(num)
	for i=1,self._lskSize  do
		local isActive = i > pinSuccCnt
		self._lskItems[i].FullObj:SetActive(isActive)
	end
end
