--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-14
*		描述： 娃娃机内部 公仔实体
*********************************************************************--]]
local WWJDollClass = DeclareClass("WWJDollClass")
--
function WWJDollClass:ctor()
    self._bindedAsset = false
    self.RootTf=false

    self.DollKey=""

end

function WWJDollClass:Init(parentTf,index,posX,posY,posZ)

    self._bindedAsset=AssetService:LoadInstantiateAsset("WWJ/Dolls/21/Doll21",LifeType.Manual)
    if not self._bindedAsset then
        LogE("Load WWJ Machine Prefab Error")
        return
    end
    self.RootTf=self._bindedAsset.RootTf
    --
    index=index or 0
    self.DollKey="Doll_"..tostring(index)
    self._bindedAsset.RootGo.name= self.DollKey
    --
    if parentTf then
        self.RootTf.parent=parentTf
    end
    self.RootTf:ExtSetLocalPositionXYZ(posX,posY,posZ)
    self._bindedAsset.RootGo:ExtSetActive(true)

end

function WWJDollClass:UnInit()

    self.RootTf=false
    if self._bindedAsset then
        AssetService:Unload(self._bindedAsset)
        self._bindedAsset=false
    end

end