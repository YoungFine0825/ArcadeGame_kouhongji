--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-07-04
*		描述： 资产加载服务  
*********************************************************************--]]
local AssetServiceClass = DeclareClass("AssetServiceClass")

function AssetServiceClass:ctor()
    
    self._assetService = Interaction.AssetService
    self._callback = ClassLib.AssetLoadCallbackClass.new()
    self._luaInteraction=Interaction.LuaInteraction

end

function AssetServiceClass.OnCsLoadAssetCompleted(name, result, resource)
    
    local self = AssetService
    self._callback:OnLoadAssetCompleted(name, result, resource)
end

function AssetServiceClass:LoadAssetAsyn(loadPriority, callback, assetType, filename, life, count)
    
    if not loadPriority or not assetType or not filename or not life then
        LogE("AssetServiceClass.LoadAssetAsyn : invalid parameter")
        return
    end
    
    if callback then
        self._callback:Add(filename, callback)
    end
    
    self._luaInteraction.LoadAsynAsset(loadPriority, assetType, filename, life, count or 1)

end

function AssetServiceClass:CancelAsyn(callback, assetName)
    self._callback:Remove(callback, assetName)
end


function AssetServiceClass:LoadUIAsset(filename, lifeType)
    if not filename then
        LogE("AssetServiceClass.LoadUIAsset : invalid parameter")
        return nil
    end
        
    return self._assetService:LoadUIAsset(filename, lifeType or LifeType.Immediate)
end


function AssetServiceClass:LoadModelAsset(filename, lifeType)
    if not filename then
        LogE("AssetServiceClass.LoadModelAsset : invalid parameter")
        return nil
    end
    
    return self._assetService:LoadModelAsset(filename, lifeType or LifeType.Immediate)
end


function AssetServiceClass:LoadInstantiateAsset(filename, lifeType)
    if not filename then
        LogE("AssetServiceClass.LoadInstantiateAsset : invalid parameter")
        return nil
    end
    
    return self._assetService:LoadInstantiateAsset(filename, lifeType or LifeType.Immediate)
end

function AssetServiceClass:LoadPrimitiveAsset(fileName)
    if not fileName then
        LogE("AssetServiceClass.LoadPrimitiveAsset : invalid parameter")
        return nil
    end
    return self._assetService:LoadPrimitiveAsset(fileName)
end

--加载spriteAsset
function AssetServiceClass:LoadSpriteAsset(fileName)
    
    if not fileName then
        LogE("AssetServiceClass.LoadSpriteAsset : invalid parameter")
        return nil
    end
    return self._assetService:LoadSpriteAsset(fileName)
end


function AssetServiceClass:Unload(ba)
    if not ba then
        LogE("AssetServiceClass.Unload : invalid parameter")
        return
    end
    self._assetService:Unload(ba)
end

function AssetServiceClass:UnloadAllUsingAssets()
   
    if self._assetService then
        self._assetService:UnloadAllUsingAssets()
    end

end

AssetService = AssetServiceClass.new()