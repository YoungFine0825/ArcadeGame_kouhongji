--[[********************************************************************
*      作者： jordenwu
*      时间： 07/17/17 15:31:13
*      描述： 垃圾收集控制
*********************************************************************--]]
local GCServiceClass = DeclareClass("GCServiceClass")

function GCServiceClass:ctor()
    self._stepCollectGarbage = false
end

-- C#调用
-- @param full 
-- @return 无
function GCServiceClass.CollectGarbage(full)

   if full then
        GCService._stepCollectGarbage = false
        collectgarbage("collect")
    else
        GCService._stepCollectGarbage = true
    end
    
end


--C# 调用
-- @return 无
function GCServiceClass.GetMemUsed()
    return collectgarbage("count") * 1024
end


-- GC更新
-- @return 无
function GCServiceClass:Update()

   if self._stepCollectGarbage then
        if collectgarbage("step", 200) then
            self._stepCollectGarbage = false
        end
    end
end

GCService = GCServiceClass.new()
