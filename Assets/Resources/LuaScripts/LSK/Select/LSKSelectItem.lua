--[[********************************************************************
*		作者： XH
*		时间： 2018-12-11
*		描述： 口红选择界面 口红item
*********************************************************************--]]

local LSKSelectItemClass = DeclareClass("LSKSelectItemClass")

function LSKSelectItemClass:ctor()
    self._idx = false
    self.Index = false

    self._asset = false
    self._mainObj = false
    self._ContentImage = false
    self._DescText = false
    self._priceText = false
    self._normalFrameObj = false
    self._selectObj = false
    self._selectAnim = false

    self._rootGo = false
end

function LSKSelectItemClass:Show()
    if self._rootGo then
        self._rootGo:SetActive(true)
    end
    self:Refresh()
end

function LSKSelectItemClass:Hide()
    if self._rootGo then
        self._rootGo:SetActive(false)
    end
end

function LSKSelectItemClass:Initialize(asset, idx)
    self._asset = asset
    self._rootGo = self._asset.RootGo
    local prefabLink = self._asset.RootTf:GetComponent('PrefabLink')
    if prefabLink then
        self._mainObj = prefabLink:GetCacheGameObject(4)
        self._selectObj = prefabLink:GetCacheGameObject(3)
        self._selectAnim = prefabLink:GetCacheGameObject(2)

        self._ContentImage = prefabLink:GetCacheComponent(0)
        self._DescText = prefabLink:GetCacheComponent(1)
        self._normalFrameObj = prefabLink:GetCacheGameObject(5)
        self._priceText = prefabLink:GetCacheComponent(6)

        self._idx = idx
    end
    local isValid = self._idx ~= 9 and self._idx ~= 10 and self._idx ~=15 and self._idx ~= 16
    if self._mainObj then
        self._mainObj:SetActive(isValid)
        self._rootGo:SetActive(isValid)
    end

    if isValid then
        self.Index = LSKUtil:GetLSKRealIdx(self._idx)
    end
    -- self:Refresh()
end

function LSKSelectItemClass:Refresh()
    if not self.Index then
        if self._mainObj then
            self._mainObj:SetActive(false)
        end
        return
    end
    local config = LSKData:GetPrizeByIdx(self.Index)
    if config and config.PrizeInfo then
        local prizeInfo = config.PrizeInfo
        if self._ContentImage then
            self._ContentImage:SetImageUrl(prizeInfo:GetIconUrl())
        end
        if self._DescText then
            self._DescText.text = prizeInfo.Desc
        end
        -- if self._priceText then
        --     self._priceText.text = '官方售价:￥'..prizeInfo.ShowPrice
        -- end
    else
        if self._mainObj then
            self._mainObj:SetActive(false)
        end
    end
end

function LSKSelectItemClass:Uninitialize()
    self._rootGo = false
    self._mainObj = false
    self._ContentImage = false
    self._DescText = false
    self._selectObj = false
    self._selectAnim = false
    self._priceText = false
    if self._asset then
        AssetService:Unload(self._asset)
        self._asset = false
    end
end

function LSKSelectItemClass:Select(id)
    local isSelect = id == self._idx
    if self._selectObj then
        self._selectObj:SetActive(isSelect)
    end
    
    if self._selectAnim then
        self._selectAnim:SetActive(isSelect)
    end

    if self._normalFrameObj then
        self._normalFrameObj:SetActive(not isSelect)
    end
end

