--[[********************************************************************
*      作者： jordenwu
*      时间： 07/17/17 15:59:29
*      描述： 资产定义
*********************************************************************--]]
LoadPriority =
{
    Wait = 0,
    ImmediateShow = 1,
    SpareShow = 10000,
    Silent = 20000,
    Preprocess = 30000
}

AssetLoadResult =
{
    Success = 0,
    BundleFail = 1,
    ResourceFail = 2
}

AssetType =
{
    UI = 0,
    UIParticle = 1,
    Model = 2,
    Texture = 3,
    Instantiate = 4,
    Primitive = 5,
    Audio = 6,
    Sprite=7,
    BaseAssetTypeCount = 8,
    UIForm = 50,
    UnityScene = 99,
    External = 100,
}

LifeType =
{
    Resident = 0,
    UIState = 2,
    Immediate = 3,
    Manual=4,
}