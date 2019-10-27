--[[********************************************************************
*		作者： XH
*		时间： 2018-12-18
*		描述： 幸运转盘
*********************************************************************--]]

local LSKSpinWinFormClass = DeclareClass("LSKSpinWinFormClass", ClassLib.UIFormClass)
-----------------------
-- 转盘配置
-----------------------
local PerCircleDuration = 0.75
local SectorCount = 12
local SlowSectorDuration = 0.4

local Vector3 = CS.UnityEngine.Vector3
local RotateMode = CS.DG.Tweening.RotateMode
local Ease = CS.DG.Tweening.Ease
local GameObject = CS.UnityEngine.GameObject

function LSKSpinWinFormClass:ctor()
    self._uiRotateRectTransformIndex = 0;
    self._uiAwardPrefabLinkIndex = 1;
    self._uiContentRectTransformIndex = 2;
    self._uiSorryAnimationIndex = 3;
    self._uiWinAnimationIndex = 4;
    self._uiLeftSecondsTextIndex = 5;
    self._uiWinContentHttpImageIndex = 6;
    self._uiQRImgQRImageIndex = 7;
    self._uiTipAnimationIndex = 8;
    self._uiCoinRectTransformIndex = 9;
    self._uiPrizeRectTransformIndex = 10;
    self._uiAnimAnimationIndex = 11;
    self._uiAwardCountTextIndex = 12;
    self._uiEF_UIWinFormTransformIndex = 13;
    self._uiWinDescTextIndex = 14;
    
    self._uiRotateRectTransform = false
    self._uiAwardPrefabLink = false
    self._uiContentRectTransform = false
    self._uiSorryAnimation = false
    self._uiWinAnimation = false
    self._uiLeftSecondsText = false
    self._uiWinContentHttpImage = false
    self._uiQRImgQRImage = false
    self._uiTipAnimation = false
    self._uiCoinRectTransform = false
    self._uiPrizeRectTransform = false
    self._uiAnimAnimation = false
    self._uiAwardCountText = false
    self._uiEF_UIWinFormTransform = false
    self._uiWinDescText = false
    
    
    self._winGo = false
    self._loseGo = false
    self._tipGo = false
    self._awards = false
    self._awardPrefab = false
    self._coinSpriteAsset = false
    self._creditsSpriteAsset = false
    self._delegate = false
    self._coinGo = false
    self._prizeGo = false
    self._winEffGo = false
end

function LSKSpinWinFormClass:vGetPath()
	return 'LSK/Play/LSKSpinWinForm'
end

function LSKSpinWinFormClass:vOnResourceLoaded()
    self._uiRotateRectTransform = self:GetComponent(self._uiRotateRectTransformIndex)
    self._uiAwardPrefabLink = self:GetComponent(self._uiAwardPrefabLinkIndex)
    self._uiContentRectTransform = self:GetComponent(self._uiContentRectTransformIndex)
    self._uiSorryAnimation = self:GetComponent(self._uiSorryAnimationIndex)
    self._uiWinAnimation = self:GetComponent(self._uiWinAnimationIndex)
    self._uiLeftSecondsText = self:GetComponent(self._uiLeftSecondsTextIndex)
    self._uiWinContentHttpImage = self:GetComponent(self._uiWinContentHttpImageIndex)
    self._uiQRImgQRImage = self:GetComponent(self._uiQRImgQRImageIndex)
    self._uiTipAnimation = self:GetComponent(self._uiTipAnimationIndex)
    self._uiCoinRectTransform = self:GetComponent(self._uiCoinRectTransformIndex)
    self._uiPrizeRectTransform = self:GetComponent(self._uiPrizeRectTransformIndex)
    self._uiAnimAnimation = self:GetComponent(self._uiAnimAnimationIndex)
    self._uiAwardCountText = self:GetComponent(self._uiAwardCountTextIndex)
    self._uiEF_UIWinFormTransform = self:GetComponent(self._uiEF_UIWinFormTransformIndex)
    self._uiWinDescText = self:GetComponent(self._uiWinDescTextIndex)
    
    self._winGo = self:GetGameObject(self._uiWinAnimationIndex)
    self._loseGo = self:GetGameObject(self._uiSorryAnimationIndex)
    self._tipGo = self:GetGameObject(self._uiTipAnimationIndex)
    self._awardPrefab = self:GetGameObject(self._uiAwardPrefabLinkIndex)
    self._coinGo = self:GetGameObject(self._uiCoinRectTransformIndex)
    self._prizeGo = self:GetGameObject(self._uiPrizeRectTransformIndex)
    self._winEffGo = self:GetGameObject(self._uiEF_UIWinFormTransformIndex)

end

function LSKSpinWinFormClass:vOnResourceUnLoaded()
    self._uiRotateRectTransform = false
    self._uiAwardPrefabLink = false
    self._uiContentRectTransform = false
    self._uiSorryAnimation = false
    self._uiWinAnimation = false
    self._uiLeftSecondsText = false
    self._uiWinContentHttpImage = false
    self._uiQRImgQRImage = false
    self._uiTipAnimation = false
    self._uiCoinRectTransform = false
    self._uiPrizeRectTransform = false
    self._uiAnimAnimation = false
    self._uiAwardCountText = false
    self._uiEF_UIWinFormTransform = false
    self._uiWinDescText = false
    
    self._winGo = false
    self._loseGo = false
    self._tipGo = false
    self._delegate = false
    self._awardPrefab = false
    self._coinGo = false
    self._prizeGo = false
    self._winEffGo = false
    self._creditsSpriteAsset = false

end

function LSKSpinWinFormClass:vOnInitialize(argument)

    self._winGo:SetActive(false)
    self._loseGo:SetActive(false)
    self:PlayTipAnim(false)
    if not argument then
        LogE('Bad Argument!')
        return
    end

    self._delegate = argument.delegate
    if self._uiLeftSecondsText then
        self._uiLeftSecondsText.text = ''
    end
    self._uiRotateRectTransform.localEulerAngles = Vector3_Zero
    
    self:CreateAwards()
    if self._uiAnimAnimation then
		self._uiAnimAnimation:Play('Anim_Open')
    end
    self._uiRotateRectTransform:DOKill(false)
end

function LSKSpinWinFormClass:vOnUninitialize()
    ScheduleService:RemoveTimer(self)
    self:DestroyAwards()
    if self._coinSpriteAsset then
        AssetService:Unload(self._coinSpriteAsset)
        self._coinSpriteAsset = false
    end
    if self._creditsSpriteAsset then
        AssetService:Unload(self._creditsSpriteAsset)
        self._creditsSpriteAsset = false
    end
    self._uiRotateRectTransform.localEulerAngles = Vector3_Zero

    self._winGo = false
    self._loseGo = false
    self._tipGo = false
    self._delegate = false
    self._awards = false
end

-------------------------------------
-- 创建奖品
-------------------------------------
function LSKSpinWinFormClass:CreateAwards()
    self._awards = self._awards or {}
    self._awardPrefab:SetActive(true)
    local angle = 360/SectorCount
    for i = 1, SectorCount do
        local awardObj = GameObject.Instantiate(self._awardPrefab,self._uiContentRectTransform)
        local prefabLink = awardObj:GetComponent('PrefabLink')
        local transform = awardObj.transform
        transform.localScale = Vector3_One
        transform.localPosition = Vector3_Zero
        transform.localEulerAngles = Vector3(0,0,(i-1)*angle)

        local iconHttpImage = prefabLink:GetCacheComponent(0)
        local countText = prefabLink:GetCacheComponent(1)
        local awardGo = prefabLink:GetCacheGameObject(2)
        local thanksGo = prefabLink:GetCacheGameObject(3)

        local idx = (i-1)%#LSKData.LotteryList + 1
        local lottery = LSKData.LotteryList[idx]
        local prize = false
        if lottery.type == LSKAwardType.Prize then
            prize = PrizeSystem:GetPrizeInfo(lottery.id)
        end
        local isAwardVisible = false
        if lottery then
            isAwardVisible = lottery.type ~= LSKAwardType.None
            local cntStr = 'x'..tostring(lottery.count)
            if lottery.type == LSKAwardType.Prize then
                iconHttpImage:SetImageUrl(prize:GetIconUrl(1))
            elseif lottery.type == LSKAwardType.Gold then
                if not self._coinSpriteAsset then
                    self._coinSpriteAsset = AssetService:LoadSpriteAsset('LSK/UICommon/Coin')
                end
                iconHttpImage:SetSprite(self._coinSpriteAsset.SpriteObj)
                -- if lottery.count >= 10000 then
                --     cntStr = string.format('%0.2f万', lottery.count/10000)
                --     cntStr = string.gsub(cntStr, '.00','')
                -- end
            elseif lottery.type == LSKAwardType.Point then
                if not self._creditsSpriteAsset then
                    self._creditsSpriteAsset = AssetService:LoadSpriteAsset('LSK/UICommon/Credits')
                end
                iconHttpImage:SetSprite(self._creditsSpriteAsset.SpriteObj)
            end
            countText.text = cntStr
        end
        awardGo:SetActive(isAwardVisible)
        thanksGo:SetActive(not isAwardVisible)
        self._awards[#self._awards+1] = awardObj
    end
    self._awardPrefab:SetActive(false)
end

-------------------------------------
-- 销毁所有的奖品
-------------------------------------
function LSKSpinWinFormClass:DestroyAwards()
    for i = #self._awards, 1, -1 do
        if self._awards[i] then
            GameObject.Destroy(self._awards[i])
        end
        table.remove(self._awards,i)
    end
    self._awards = false
end

function LSKSpinWinFormClass:PlayTipAnim(isStop)
    if not self._uiTipAnimation then
        return
    end
    
    self._tipGo:SetActive(true)

    if isStop then
        -- 按'开始键'结束提示   
        self._uiTipAnimation:Play('Lottery_Stop')
    else
        -- 按'开始键'开始提示
        self._uiTipAnimation:Play('Lottery_Launch')
    end
end

function LSKSpinWinFormClass:StopTipAnim()
    self._tipGo:SetActive(false)
end

-------------------------------------
-- 圆盘开始旋转.
-------------------------------------
function LSKSpinWinFormClass:StartSpin()
    self:StopTipAnim()
    if self._uiRotateRectTransform then
        self._uiRotateRectTransform.localEulerAngles = Vector3_Zero
        self._uiRotateRectTransform:DORotate(Vector3(0,0,-360), PerCircleDuration, RotateMode.LocalAxisAdd)
                                   :SetLoops(-1)
                                   :SetEase(Ease.Linear)
                                   .onComplete = function() 
                                        self._uiRotateRectTransform.localEulerAngles = Vector3_Zero
                                   end
    end
end

-------------------------------------
-- 停止旋转.
-- @param idx:扇形的索引1-12 
-- 特殊情况：如果为0，代表没中奖,
-- 并且没有'谢谢参与'的扇形item，转到第一个交界出.
-------------------------------------
function LSKSpinWinFormClass:StopSpin(idx)
    self:StopTipAnim()
    LogD('Lottery End Index = '..idx)
    local isWinPrize = idx >= 1
    local duration = isWinPrize and SlowSectorDuration*idx or SlowSectorDuration
    local perAngle = 360/SectorCount
    local angle = idx == 0 and 15 or ((idx-1)*-1*perAngle + LSKUtil:GetRandom(-1*perAngle/3, perAngle/3))
    self._uiRotateRectTransform:DOKill(true)
    self._uiRotateRectTransform:DORotate(Vector3(0,0,angle), duration, RotateMode.FastBeyond360).onComplete = function ()
        ScheduleService:AddTimer(self,self.OnDelayEndSpin,0.5,false)
    end
end

-------------------------------------
-- 动画播放结束回调.
-------------------------------------
function LSKSpinWinFormClass:OnDelayEndSpin()
    ScheduleService:RemoveTimer(self,self.OnDelayEndSpin)
    if self._delegate then
        self._delegate:OnSpinEnd()
    end
end

-------------------------------------
-- 倒计时.
-- @param seconds:剩余秒数.
-------------------------------------
function LSKSpinWinFormClass:Countdown(seconds)
    if self._uiLeftSecondsText then
        self._uiLeftSecondsText.text = tostring(seconds)
    end
end

-------------------------------------
-- 抽奖结果展示.
-- @param isWin:是否中奖.
-- @param award:中奖数据.
-------------------------------------
function LSKSpinWinFormClass:ShowResult(isWin, award)
    if isWin and award then
        if self._winEffGo then
            self._winEffGo:SetActive(true)
        end
        
        self._uiWinContentHttpImage:SetSprite(LSKUtil:GetEmptySprite())
        local isCoin = award.type == LSKAwardType.Gold
        local isPrize = award.type == LSKAwardType.Prize
        self._coinGo:SetActive(isCoin)
        self._prizeGo:SetActive(isPrize)
        self._uiAwardCountText.text = 'x'..tostring(award.count or 1)
        if isCoin then
            if not self._coinSpriteAsset then
                self._coinSpriteAsset = AssetService:LoadSpriteAsset('LSK/UICommon/Coin')
            end
            self._uiWinContentHttpImage:SetSprite(self._coinSpriteAsset.SpriteObj)
            self._uiWinDescText.text = '恭喜您获得'..tostring(award.count)..'游戏币\n已存入您的账户中！'
        elseif isPrize then
            local prize = PrizeSystem:GetPrizeInfo(award.id)
            self._uiWinContentHttpImage:SetImageUrl(prize:GetIconUrl(3))
            if self._uiQRImgQRImage then
                self._uiQRImgQRImage:SetQRUrl(LSKData.ExchangeQrCode)
            end
        elseif award.type == LSKAwardType.Point then
            if not self._creditsSpriteAsset then
                self._creditsSpriteAsset = AssetService:LoadSpriteAsset('LSK/UICommon/Credits')
            end
            self._uiWinContentHttpImage:SetSprite(self._creditsSpriteAsset.SpriteObj)
            self._coinGo:SetActive(true)
            self._uiWinDescText.text = '恭喜您获得'..tostring(award.count)..'积分\n已存入您的账户中！'
        end
        self._uiWinContentHttpImage:SetNativeSize()
        if self._uiWinAnimation then
            self._uiWinAnimation:Play('LotteryWin_Once')
            ScheduleService:AddTimer(self,self.DelayPlayWinLoopAnim,0.65,false)
        end
    end
    self._winGo:SetActive(isWin)
    self._loseGo:SetActive(not isWin)
end

function LSKSpinWinFormClass:DelayPlayWinLoopAnim()
    ScheduleService:RemoveTimer(self,self.DelayPlayWinLoopAnim)
    if self._uiWinAnimation then
        self._uiWinAnimation:Play('LotteryWin_Loop')
    end
end

function LSKSpinWinFormClass:Close()
    if self._winGo then
        self._winGo:SetActive(false)
    end
    if self._winEffGo then
        self._winEffGo:SetActive(false)
    end
	if self._uiAnimAnimation then
		self._uiAnimAnimation:Play('Anim_Close')
	end
	ScheduleService:AddTimer(self,self.OnDelayClose,1,false)
end

function LSKSpinWinFormClass:OnDelayClose()
	self:Destroy()
end