--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-07-04
*		描述： c#交互类
*********************************************************************--]]
local InteractionClass = DeclareClass("InteractionClass")
function InteractionClass:ctor()
    
    local luaInteraction = CS.JW.Lua.LuaInteraction
    --
    self.UIStateService = luaInteraction.GetUIStateService()
    --
    self.UGUIRoot = luaInteraction.GetUGUIRoot()
    --
    self.AssetService = luaInteraction.GetAssetService()
    --
    self.NetworkService=luaInteraction.GetNetworkService()
    --
    self.NativeService=luaInteraction.GetNativeService()
    --
    self.UICommonService=luaInteraction.GetUICommonService()
    --
    self.AudioService=luaInteraction.GetAudioService()
    --
    self.SceneService=luaInteraction.GetSceneService()
    --
    self.IFSService=luaInteraction.GetIFSService()
    --
    self.ResService=luaInteraction.GetResService()

    self.LuaInteraction = luaInteraction 
    
    self.HttpService = luaInteraction.GetHttpService()

    self.ArcadeInputService = luaInteraction.GetArcadeInputService()

    self.QualityService=luaInteraction.GetQualityService()
    
    self.NetAssetService=luaInteraction.GetNetAssetService()

end

-- 全局
Interaction = InteractionClass.new()
