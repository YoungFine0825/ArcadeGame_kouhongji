--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-07-04
*		描述： 表池
*********************************************************************--]]
local PoolClass = DeclareClass("PoolClass")

function PoolClass:ctor()
    self._tablePool = {}
end

-- 创建表
-- @return 创建的表
function PoolClass:CreateTable()
    return next(self._tablePool) and table.remove(self._tablePool) or {}
end

-- 删除表
-- @param tbl 待删除的表
function PoolClass:DestroyTable(tbl)
    if not tbl then
        return
    end
    table.clear(tbl)    
    self._tablePool[#self._tablePool+1] = tbl
end

Pool = PoolClass.new()