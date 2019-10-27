--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-09-03
*		描述： APP监控类
*********************************************************************--]]
local luaInteraction = CS.JW.Lua.LuaInteraction

local AppMoniterClass = DeclareClass("AppMoniterClass")

function AppMoniterClass:ctor()

end

function AppMoniterClass.OnCSAppEnterBackGround()
	LogD("----------App进入后台----------")
    EventService:SendEvent(LuaEventID.AppEnterBackGround)
end

function AppMoniterClass.OnCSAppEnterForeGround()
	
    LogD("----------App进入前台----------")
    EventService:SendEvent(LuaEventID.AppEnterForeGround)
    
end

--全局监控
AppMoniter = AppMoniterClass.new() 