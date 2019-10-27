--[[********************************************************************
*      作者： jordenwu
*      时间： 2018-07-02
*      描述： Lua 启动入口
*********************************************************************--]]
--VS Code 调试
local VsCodeBreakInfoFun = require("LuaDebug")("localhost", 7003) 

-- 全局
require("Global")
--
local scripts = {}
function CustomRequire(script)
    if not scripts then
        LogE("Do not call CustomRequire() after Boot.")
        return
    end
    scripts[#scripts + 1] = script
end

--收集脚本
function CollectScripts(flag)
    require("Global")
    require("EventDeclare")
    require("Common.CommonBoot")
    require("Framework.FrameworkBoot")
    require("Machine.MachineBoot")
    --
    CustomRequire("AppMoniter")
    CustomRequire("LoginState")
    CustomRequire("SubGameState")
    --
    local _scripts = scripts
    scripts = nil
    return _scripts
end

--初始化框架层
function InitFramework(init)
    if init then
        LogD("-------------->Init Lua Framework<----------------------")
    else
        LogD("-------------->UnInit Lua Framework<----------------------")
    end
end

--初始化逻辑
function InitLogic(init)
    if init then
        LogD("------------->Lua InitLogic<-------------")
        --注册模块
        MachineBoot:InitLogic(true)
    else
        
        LogD("------------>Lua UnInitLua<--------------")
        if MVCService then
            MVCService:DestroyAllModule()
            MVCService:Uninitialize()
        end
        if MachineBoot then
            MachineBoot:InitLogic(false)
        end
        if GameStateService then
            GameStateService:Uninitialize()
        end
    end
end

--c#预加载完成所有lua 文件以后
--InitFramework
--InitLogic
--开始启动Lua 游戏机
--开始
function StartGame(flag)
    LogD("------------->Lua StartGame<-------------")
    local loginM = ClassLib.LoginStateClass.new()
    GameStateService:RegisterState(loginM:vGetName(), loginM)

    local subGame = ClassLib.SubGameStateClass.new()
    GameStateService:RegisterState(subGame:vGetName(), subGame)
    
    --登陆机器状态
    GameStateService:ChangeState("LoginGameState")
end

--游戏驱动
function GameUpdate(delta)
    if VsCodeBreakInfoFun then
        VsCodeBreakInfoFun()
    end
    --时钟
    ScheduleService:Update(delta)
    --GC
    GCService:Update()
    --
end






