--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-08-02
*		描述： RES系统对接
*********************************************************************--]]
local ResServiceClass = DeclareClass("ResServiceClass")
--
function ResServiceClass:ctor()
    --
    self._csService=Interaction.ResService
    
end

function ResServiceClass:RegisterResPackConfig(fileName)
    -- body
    if not self._csService then
        return
    end
    self._csService:MergeResPackConfig(fileName)
end

ResService=ResServiceClass.new()