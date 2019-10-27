--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-16
*		描述： 子游戏控制模块
*********************************************************************--]]
local SubGameModuleClass = DeclareClass("SubGameModuleClass", ClassLib.ModuleBaseClass)

function SubGameModuleClass:ctor()
end

function SubGameModuleClass:vOnInitializeModule()
    
    EventService:AddEvent(LuaEventID.SubGameLaunchEvent,self,self.OnSubGameLaunchEvent)

end

function SubGameModuleClass:vOnUninitializeModule()
    
    EventService:RemoveEvent(self,LuaEventID.SubGameLaunchEvent)

end

function SubGameModuleClass:vOnGameStateEnter(state, fromState, userData)
   
end

function SubGameModuleClass:vOnGameStateLeave(state, toState, userData)
end

--处理子游戏启动事件
function SubGameModuleClass:OnSubGameLaunchEvent(eventArg)
    
    self:UpdateUI("SubGameLaunchEvent",eventArg)

end
