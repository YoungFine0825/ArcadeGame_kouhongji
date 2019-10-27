--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-05
*		描述： 娃娃机场景实体
*********************************************************************--]]
local WWJEnvClass = DeclareClass("WWJEnvClass")
--
function WWJEnvClass:ctor()
    
    self._envAsset=false

end

function WWJEnvClass:Init()
    
    self._envAsset=AssetService:LoadInstantiateAsset("WWJ/Env/EnvRoot",LifeType.Manual)
    if not self._envAsset then
        LogE("Load WWJ Env Prefab Error")
        return
    end

    self._envAsset.RootGo:ExtSetActive(true)
    --灯光控制
    QualityService:LoadGlobalReflectionCubeMap(0.6,"WWJ/Env/Env_CubeMap")

end

function WWJEnvClass:UnInit()
    
    if self._envAsset then
        AssetService:Unload(self._envAsset)
        self._envAsset=false
    end

end

--逻辑更新
function WWJEnvClass:LogicUpdate(delta)

   

end