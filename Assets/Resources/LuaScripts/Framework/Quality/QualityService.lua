--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-09
*		描述： 全局质量管理 光照管理
*********************************************************************--]]
local QualityServiceClass = DeclareClass("QualityServiceClass")

function QualityServiceClass:ctor()
	
	self.csS=Interaction.QualityService
	
end

function QualityServiceClass:LoadGlobalReflectionCubeMap( intensity,cubemap)
    if self.csS then
        self.csS:LoadGlobalReflectionCubeMap(intensity,cubemap)
    end
end

function QualityServiceClass:UnloadGlobalReflectionCubeMap()
    if self.csS then
        self.csS:UnloadGlobalReflectionCubeMap()
    end
end

QualityService=QualityServiceClass.new()