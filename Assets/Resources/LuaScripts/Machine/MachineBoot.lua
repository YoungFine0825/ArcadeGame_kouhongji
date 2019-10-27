CustomRequire("Machine.Network.MachineProtocol")
CustomRequire("Machine.Network.MachineNetPacker")
CustomRequire("Machine.Network.MachineNetwork")
--
CustomRequire("Machine.Data.MachineData")

CustomRequire("Machine.Login.MachineLoginModule")
CustomRequire("Machine.Login.UIMachineLoginMediator")
CustomRequire("Machine.Login.UIMachineLoginForm")

CustomRequire("Machine.SubGame.SubGameModule")
CustomRequire("Machine.SubGame.UISubGameMediator")
CustomRequire("Machine.SubGame.UISubGameLaunchForm")

--
CustomRequire("Machine.ADSys.AdInfo")
CustomRequire("Machine.ADSys.AdInfoList")
CustomRequire("Machine.ADSys.ADPlayer")
CustomRequire("Machine.ADSys.ADSystem")

CustomRequire("Machine.PrizeSys.PrizeInfo")
CustomRequire("Machine.PrizeSys.PrizeSystem")

CustomRequire("Machine.MachineMoniter")


--全局
MachineBoot={}
--全局请求根 这里控制了 游戏具体连接那个服务器
MachineUrlRoot=""
--
if Global.IsDebug then
    --开发
    --MachineUrlRoot = "http://ghtest.scbczx.com:65"
    --测试
    MachineUrlRoot = "http://ghtest.scbczx.com:60"
else
    MachineUrlRoot = "http://ghtest.scbczx.com:60"
end
--
function MachineBoot:InitLogic(isInit) 
    
    if isInit then
        --UI 模块
        MVCService:CreateModule(ClassLib.MachineLoginModuleClass,ClassLib.UIMachineLoginMediatorClass)
        MVCService:CreateModule(ClassLib.SubGameModuleClass,ClassLib.UISubGameMediatorClass)

        --初始化机器数据模块
        MachineData:Initialize()
        --初始化机器网络模块
        MachineNetwork:Initialize()
        --
        PrizeSystem:Initialize()
        --
        ADSystem:Initialize()
        --
        MachineMoniter:Initialize()

    else
        
        MachineData:UnInitialize()

        PrizeSystem:UnInitialize()
        --
        ADSystem:UnInitialize()
        --
        MachineMoniter:UnInitialize()
        --
        MachineNetwork:UnInitialize()
    end
end

