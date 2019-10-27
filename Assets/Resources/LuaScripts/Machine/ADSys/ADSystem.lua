--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-19
*		描述： 广告系统
*********************************************************************--]]
local ADSystemClass = DeclareClass("ADSystemClass")

function ADSystemClass:ctor()
    
    --广告列表 集合
    self._adListSet = {}
    --当前正在播放的广告列表索引
    self._curPlayListIndex = 0
    --
    self._adPlayer=false
    --
    self._handler = false

end

function ADSystemClass:Initialize()

    self._curPlayListIndex = 1
    self._adPlayer=ClassLib.ADPlayerClass.new()
    self._adPlayer:Initialize()

end

function ADSystemClass:UnInitialize()
    
    if self._adListSet then
       table.clear(self._adListSet)
       self._adListSet=false
    end
    self._curPlayListIndex = 0

    if self._adPlayer then
        self._adPlayer:UnInitialize()
        self._adPlayer=false
    end
    self._handler = false
end

--初始化广告数据
function ADSystemClass:InitAdData(adListData)
    
    if not adListData or #adListData==0 then
        LogE("Init AdData Error ")
        return
    end
    --读取广告列表
    local i = 0
    for k,v in pairs(adListData) do
        i = #self._adListSet + 1
        local adList = ClassLib.AdInfoListClass.new()
        --暂时用索引作为列表的Id
        adList:InitList(i,v)
        self._adListSet[i] = adList
    end
end

--[[
    @desc: 对外播放
    @return:
]]
function ADSystemClass:Play(obj,handler)

    if not obj or not handler then
        LogE("ADSystem Play Error Arg")
        return
    end
    --
    local adlist=self._adListSet[self._curPlayListIndex]
    if not adlist then
        handler(obj,"No AD Data")
        return
    end
    LogD("开始播放广告列表 "..adlist.Id)
    self._handler = Pool:CreateTable()
    self._handler.obj = obj
    self._handler.func = handler
    --
    self._adPlayer:Play(adlist,self,self.OnPlayADListFinished)
end

--[[
    播放列表完成
]]
function ADSystemClass:OnPlayADListFinished()
    --更新当前列表索引
    local nextListIndex = self._curPlayListIndex + 1
    if nextListIndex > #self._adListSet then
        nextListIndex = 1
    end
    self._curPlayListIndex = nextListIndex 
    --执行回调
    self._handler.func(self._handler.obj)
end

--[[
    @desc: 对外关闭
    @return:
]]
function ADSystemClass:Close()
    --
    if self._adPlayer then
        self._adPlayer:Close()
    end
    --
    if self._handler then
        self._handler = false
    end
end

function ADSystemClass:GetAdAssetsUrls()
    local urls = {}
    for i = 1,#self._adListSet do
        local adList = self._adListSet[i].InfoList
        for j = 1,#adList do
            local info = adList[j]
            if not string.IsNullOrEmpty(info.Url) then
                table.insert(urls,info.Url)
            end
            if info.Type == ADType.Image then
                if not string.IsNullOrEmpty(info.AudioUrl) then
                    table.insert(urls,info.AudioUrl)
                end
            end
        end
    end
    return urls
end

--判断广告信息列表中是否有数据
function ADSystemClass:IsAdDataEmpty()
    if not self._adListSet or #self._adListSet == 0 then
        return true
    end
    return false
end


ADSystem = ClassLib.ADSystemClass.new()