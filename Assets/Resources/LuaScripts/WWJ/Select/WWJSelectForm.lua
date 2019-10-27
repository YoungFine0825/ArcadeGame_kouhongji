--[[********************************************************************
*		作者： yangfan
*		时间： 2018-12-05
*		描述： 娃娃机选择界面
*********************************************************************--]]

local WWJSelectFormClass = DeclareClass("WWJSelectFormClass", ClassLib.UIFormClass)
function WWJSelectFormClass:ctor()
	self._uiDollListListViewIndex = 0;
	self._uiQRCodeHttpImageIndex = 1;
	self._uiCntDownTextIndex = 2;
	self._uiDollNameTextIndex = 3;
	self._uiDollImgImageIndex = 4;
	self._uiDollDecTextIndex = 5;
	self._uiDollPriceTextIndex = 6;
	self._uiQRCodeStateTextIndex = 7;
	
	self._uiDollListListView = false
	self._uiQRCodeHttpImage = false
	self._uiCntDownText = false
	self._uiDollNameText = false
	self._uiDollImgImage = false
	self._uiDollDecText = false
	self._uiDollPriceText = false
	self._uiQRCodeStateText = false	
    
    self._dollsListView = false
	self._dollInfoList = false
	self._dollsCount = 0
	self._curSelectedDollIndex = 0
	self._onSelectedDollHandler = false
end

function WWJSelectFormClass:vGetPath()
	return 'WWJ/Select/WWJSelectForm'
end

function WWJSelectFormClass:vOnResourceLoaded()
	self._uiDollListListView = self:GetComponent(self._uiDollListListViewIndex)
	self._uiQRCodeHttpImage = self:GetComponent(self._uiQRCodeHttpImageIndex)
	self._uiCntDownText = self:GetComponent(self._uiCntDownTextIndex)
	self._uiDollNameText = self:GetComponent(self._uiDollNameTextIndex)
	self._uiDollImgImage = self:GetComponent(self._uiDollImgImageIndex)
	self._uiDollDecText = self:GetComponent(self._uiDollDecTextIndex)
	self._uiDollPriceText = self:GetComponent(self._uiDollPriceTextIndex)
	self._uiQRCodeStateText = self:GetComponent(self._uiQRCodeStateTextIndex)
end

function WWJSelectFormClass:vOnResourceUnLoaded()
	self._uiDollListListView = false
	self._uiQRCodeHttpImage = false
	self._uiCntDownText = false
	self._uiDollNameText = false
	self._uiDollImgImage = false
	self._uiDollDecText = false
	self._uiDollPriceText = false
	self._uiQRCodeStateText = false	
end

function WWJSelectFormClass:vOnInitialize(argument)
	self._dollInfoList = {}
	self._dollsCount = 10
	self._dollsListView = self:InitListView(self._uiDollListListView,self.OnEnableDollListItem,self.OnSelectedDollListItem)
	self._dollsListView:SetListViewCount(self._dollsCount)
	self:DoSelectDoll(1)
end

function WWJSelectFormClass:vOnUninitialize()
	self._onSelectedDollHandler = false
end

function WWJSelectFormClass:InitListView(parent,onEnabelCallback,onSelectedCallback)
    if(parent) then
        local view = ClassLib.UIListViewClass.new();
        view:Init(parent,self,onEnabelCallback,onSelectedCallback);
        return view;
    end
end

function WWJSelectFormClass:UpdateUI(id,argument)
    if id == "UpdateCntDown" then
        if self._uiCntDownText then
            self._uiCntDownText.text = argument
        end
	elseif id == "OnArcadeRockerInput" then
		self:OnSelectNextDoll(argument)
    end
end

function WWJSelectFormClass:OnEnableDollListItem(index,itemLink)
    
end

function WWJSelectFormClass:OnSelectedDollListItem(item,lastItem)

end

--选择下一个娃娃
function WWJSelectFormClass:OnSelectNextDoll(dir)
	local nextIndex = 0
	if dir == ArcadeInput.RockerState.RockerMoveForward then--上
		if self._curSelectedDollIndex % 2 == 0 then
			nextIndex = self._curSelectedDollIndex - 1
		end
	elseif dir == ArcadeInput.RockerState.RockerMoveBack then--下
		if self._curSelectedDollIndex % 2 ~= 0 then
			nextIndex = self._curSelectedDollIndex + 1
		end
	elseif dir == ArcadeInput.RockerState.RockerMoveLeft then--左
		nextIndex = self._curSelectedDollIndex - 2
	elseif dir == ArcadeInput.RockerState.RockerMoveRight then--右
		nextIndex = self._curSelectedDollIndex + 2
	end
	--越界检测
	if nextIndex <= 0 or nextIndex > self._dollsCount then
		nextIndex = 0
	end
	--
	self:DoSelectDoll(nextIndex)
end

function WWJSelectFormClass:DoSelectDoll(index)
	if index ~= 0 then
		self._curSelectedDollIndex = index
		self._dollsListView:SelectElement(index)
		if self._onSelectedDollHandler then
			self._onSelectedDollHandler.func(self._onSelectedDollHandler.obj,index)
		end
	end
end

function WWJSelectFormClass:SetSelectedDollHandler(obj,func)
	if not obj or not func then
		return
	end
	self._onSelectedDollHandler = {}
	self._onSelectedDollHandler.obj = obj
	self._onSelectedDollHandler.func = func
end



