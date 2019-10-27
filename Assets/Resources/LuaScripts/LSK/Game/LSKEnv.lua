--[[********************************************************************
*		作者： XH
*		时间： 2018-11-09
*		描述： 口红机场景
*********************************************************************--]]

local LSKEnvClass = DeclareClass("LSKEnvClass")

function LSKEnvClass:ctor()
    self._EnvAsset = false
    self._rootTf = false
    self._prefabLink = false

    self._rootGo = false
    self._material = false
    self._wallpaperAsset = false
    self._wallpaperName = false
end

function LSKEnvClass:GetPrefabLink()
    return self._prefabLink
end

function LSKEnvClass:GetRootTf()
    return self._rootTf
end


function LSKEnvClass:Init()
    self:LoadEnv()

    if not self._EnvAsset or not self._EnvAsset.RootTf then
        return
    end
    self._prefabLink = self._EnvAsset.RootTf:GetComponent('PrefabLink')
    self._rootTf = self._prefabLink:GetCacheComponent(0)

    local meshRenderer = self._prefabLink:GetCacheComponent(5)
    if meshRenderer then
        self._material = meshRenderer.material
    end

end

-------------------------------------
-- 加载口红机场景
-------------------------------------
function LSKEnvClass:LoadEnv()
    local EnvAsset = AssetService:LoadInstantiateAsset('LSK/Game/LSKMain',LifeType.Manual)
    if not EnvAsset or not EnvAsset.RootGo then
        return
    end

    self._EnvAsset = EnvAsset
    self._rootGo = self._EnvAsset.RootGo
    self._rootGo:SetActive(true)
    local tf = self._EnvAsset.RootTf
    if tf then
        tf:SetParent(nil)
    end

end

-------------------------------------
-- 卸载口红机场景
-------------------------------------
function LSKEnvClass:UnloadEnv()
    if self._EnvAsset then
        AssetService:Unload(self._EnvAsset)
        self._EnvAsset = false
    end
end


function LSKEnvClass:SetWallpaper(wallpaper)
    if self._wallpaperName == wallpaper then
        return
    end
    if self._wallpaperAsset then
        AssetService:Unload(self._wallpaperAsset)
        self._wallpaperAsset = false
    end
    if self._material then
        self._wallpaperAsset = AssetService:LoadPrimitiveAsset('LSK/Wallpaper/'..wallpaper,LifeType.Manual)
        if self._wallpaperAsset then
            self._material.mainTexture = self._wallpaperAsset.Resource.Content
        end
    end
    self._wallpaperName = wallpaper
end

function LSKEnvClass:Show()
    if self._rootGo then
        self._rootGo:SetActive(true)
    end
end

function LSKEnvClass:Hide()
    if self._rootGo then
        self._rootGo:SetActive(false)
    end
end


function LSKEnvClass:Uninit()
    self._prefabLink = false
    self._rootTf = false
    self._rootGo = false
    self._material = false
    if self._wallpaperAsset then
        AssetService:Unload(self._wallpaperAsset)
        self._wallpaperAsset = false
    end
    self._wallpaperName = false
    self:UnloadEnv()
end
