--[[********************************************************************
*		作者： XH
*		时间： 2018-12-13
*		描述： 飞刀数据
*********************************************************************--]]

local LSKKnifeDataClass = DeclareClass("LSKKnifeDataClass")

function LSKKnifeDataClass:ctor()
    self._knifeWidth = 0.6
    self._halfKnifeWidth = 0.3
    self._insertWidth = 10
    self._knifeList = {}
    self._gapList = {}
end

function LSKKnifeDataClass:Init(width, wheelRadius)
    self._knifeWidth = width
    self._halfKnifeWidth = self._knifeWidth/2
    local maxCnt = (2*3.1415926535*wheelRadius)/self._knifeWidth
    self._insertWidth = 360/maxCnt + 0.2
end

function LSKKnifeDataClass:Uninit()
    self:Clear()
end

function LSKKnifeDataClass:Insert(center)
    self._knifeList[#self._knifeList+1] = center
    self:Sort()
    for i, v in ipairs(self._knifeList) do
        local nextIdx = i%#self._knifeList + 1
        local add = i == #self._knifeList and 360 or 0
        local distance = math.abs(self._knifeList[nextIdx] + add - v)
        if distance <= self._insertWidth then
            local key = center == v and i or nextIdx
            table.remove(self._knifeList,key)
            return false
        end
    end
    self:GenGapList()
    return true
end

function LSKKnifeDataClass:GetMaxGap()
    if #self._gapList > 0 then
        return self._gapList[1]
    end
end

function LSKKnifeDataClass:IsCanQuickPass()
    local gap = self:GetMaxGap()
    if gap then
        return gap.gap < 10*self._insertWidth
    end
    return false
end

function LSKKnifeDataClass:GetRandomValidGap()
    local maxIdx = 3
    if #self._gapList < 3 then
        maxIdx = #self._gapList
    end
    local idx = LSKUtil:GetRandom(1, maxIdx)
    idx = 1
    local gap = self._gapList[idx]
    if gap and gap.gap >= self._insertWidth then
        return gap
    else
        for i = idx+1, idx + #self._gapList - 1, 1 do
            local realIdx = i == #self._gapList and #self._gapList or i%self._gapList
            gap = self._gapList[realIdx]
            if gap and gap.gap >= self._insertWidth then
                return gap
            end
        end
    end
end

function LSKKnifeDataClass:GenGapList()
    for j = #self._gapList, 1, -1 do
        table.remove(self._gapList,j)
    end
    --
    for i = 1, #self._knifeList do
        local nextKnife = i%#self._knifeList + 1
        local max = self._knifeList[nextKnife]
      
        if i == #self._knifeList then
            max = max + 360
        end
        local gap = {}
        gap.min = self._knifeList[i] + self._halfKnifeWidth
        gap.max = max - self._halfKnifeWidth
        
        gap.gap = math.abs(gap.max - gap.min)
        self._gapList[#self._gapList+1] = gap
    end

    table.sort(self._gapList,function(a,b)
        if a.gap > b.gap then
            return true
        else
            return false
        end
    end)

    -- local log = ''
    -- for i = 1, #self._knifeList do
    --     local gap = self._gapList[i]
    --     log = log .. string.format('[%d] = {min=%f,max=%f,gap= %f},', i, gap.min, gap.max, math.abs(gap.max - gap.min))
    -- end
    -- LogD('Gap List = '..log)
end

function LSKKnifeDataClass:GetGapList()

end

function LSKKnifeDataClass:Sort()
    table.sort(self._knifeList, function(a, b)
        if a < b then
            return true
        else
            return false
        end
    end)
    -- local log = ''
    -- for i, v in ipairs(self._knifeList) do
    --     log = log .. string.format('[%d] = %f ,', i, v)
    -- end
    -- LogD('Sort Knife Data : '..log)
end

-------------------------------------
-- 清空数据.
-------------------------------------
function LSKKnifeDataClass:Clear()
    for i = #self._knifeList, 1, -1 do
        table.remove(self._knifeList,i)
    end

    for j = #self._gapList, 1, -1 do
        table.remove(self._gapList,j)
    end
end