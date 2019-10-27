--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-07-31
*		描述： 滚动列表 控件
*********************************************************************--]]
local UIListViewClass = DeclareClass("UIListViewClass")

function UIListViewClass:ctor()
    
    self._csListView=false
    self._delegate=false
    self._onEnableCallback=false
    self._onSelectCallback=false
end

--[[
    @desc: 初始化
    --@csListView:cs 组件
    --@delegate: 处理回调类 OnListViewItemEnable(index,itemLink) 
    --@enableCallback:初始化回调函数 func(index,itemLink)
    --@selectCallback:选中回调函数  func(selectPlink, lastPlink)
    @return:
]]
function UIListViewClass:Init(csListView,delegate,enableCallback,selectCallback)
    
    if not csListView then
        LogE("UIListViewClass Init Error Nil UIListView Component")
    end
    self._csListView=csListView
    self._csListView.OnEnableItemHandler=function( item )
        self:OnCsListViewEnableItem(item)
    end
    self._csListView.SelectedChangedHandler=function(item,lastItem)
        self:OnCsListViewSelectedItem(item,lastItem)
    end
    self._delegate=delegate

    self._onEnableCallback = enableCallback or false
    self._onSelectCallback = selectCallback or false
end

function UIListViewClass:SelectElement(idx)
    self._csListView:SelectElement(idx-1)
end

function UIListViewClass:UnInit( )
    
    if self._csListView then
        self._csListView:SetElementAmount(0)
        self._csListView.OnEnableItemHandler=nil
        self._csListView=false
    end
    self._delegate=false
    self._onEnableCallback=false
    self._onSelectCallback=false
end

--[[
    @desc: c#回调
    @return:无
]]
function UIListViewClass:OnCsListViewEnableItem(item)
    
    if not item then
        return
    end

    if not self._delegate then
        return
    end

    if self._delegate.OnListViewItemEnable then
        self._delegate:OnListViewItemEnable(item.m_index,item.PLink)
    end

    if self._onEnableCallback then
        self._onEnableCallback(self._delegate,item.m_index,item.PLink)
    end
end 
--[[
    @desc: 选中某个元素
    @return:无
]]
function UIListViewClass:OnCsListViewSelectedItem(item,lastItem)

    if not self._delegate then
        return
    end

    if  self._delegate.OnSelectedItem then
        self._delegate:OnSelectedItem(item,lastItem)
    end

    if self._onSelectCallback then
        self._onSelectCallback(self._delegate,item,lastItem)
    end
end

--[[
    @desc: 设置元素个数
    --@cnt: 个数
    @return:无
]]
function UIListViewClass:SetListViewCount(cnt)
    
    if self._csListView then
        self._csListView:SetElementAmount(cnt)
    end
end

--[[
    @desc:将某项元素移动到滚动区域内(使其可见)
    --@eIndex:
	--@isImmediately: 
    @return:
]]
function UIListViewClass:MoveElementInScrollArea(eIndex,isImmediately)
    
    if self._csListView then
        self._csListView:MoveElementInScrollArea(eIndex,isImmediately)
    end
end

--[[
    @desc: 重置内容位置
    @return:
]]
function UIListViewClass:ResetContentPosition( )
    if self._csListView then
        self._csListView:ResetContentPosition()
    end
end

