--[[********************************************************************
*		作者： XH
*		时间： 2018-11-09
*		描述： 口红机工具类
*********************************************************************--]]

local socket = require("socket")

local LSKUtilClass = DeclareClass("LSKUtilClass")

function LSKUtilClass:ctor()
    self._emptySprite = false
end

-------------------------------------
-- 返回一个在min和max之间的整数
-- @param min 最小值.
-- @param max 最大值.
-------------------------------------
function LSKUtilClass:GetRandom(min, max)
    if not min or not max then
        return
    end
    if type(min) ~= 'number' or type(max) ~= 'number' then
        return
    end

    math.randomseed(tostring(socket.gettime()):reverse():sub(1, 6))
    return math.random(min, max)
end

-------------------------------------
-- 返回一个百分比(0-100)
-------------------------------------
function LSKUtilClass:GetPercentageRandom()
    return self:GetRandom(0,100)
end

-------------------------------------
-- 返回一个在min和max之间的浮点数
-- @param min 最小值.
-- @param max 最大值.
-------------------------------------
function LSKUtilClass:GetFloatRandom(min, max)
    math.randomseed(tostring(socket.gettime()):reverse():sub(1, 6))
    if min == nil or max == nil then
        return math.random()
    end
    local range = max - min
    return min + range*math.random()
end

function LSKUtilClass:GetFloatTimeStamp()
    return socket.gettime()
end

function LSKUtilClass:GetRandomKeyList(maxIdx)
    local list = {}
    for i = 1, maxIdx do
        list[i] = i
    end
    local randomList = {}
    local debugStr = ''
    for j = 1, maxIdx do
        randomList[j] = self:PopRandomIndexFromList(list)
        debugStr = debugStr .. '['..tostring(j)..'] = '..randomList[j]..','
    end
    LogD('Get Random List : '..debugStr)
    return randomList
end

function LSKUtilClass:PopRandomIndexFromList(list)
    if list and #list > 0 then
        local randomIdx = self:GetRandom(1, #list)
        local value = list[randomIdx]
        table.remove(list, randomIdx)
        return value
    end
end

function LSKUtilClass:GetLSKRealIdx(idx)
    if idx <= 8 then
        return idx
    end
    if idx > 8 and idx <= 14 then
        return idx - 2
    end
    return idx - 4
end

function LSKUtilClass:GetEmptySprite()
    if not self._emptySprite then
        self._emptySprite = AssetService:LoadSpriteAsset('LSK/UICommon/Image_Empty',LifeType.Manual)
    end
    if self._emptySprite then
        return self._emptySprite.SpriteObj
    end
end

function LSKUtilClass:Clear()
    if self._emptySprite then
        AssetService:Unload(self._emptySprite)
        self._emptySprite = false
    end
end