--[[********************************************************************
*      作者： jordenwu
*      时间： 08/03/17 09:52:20
*      描述： 网络协议名称定义 这里只定义 游戏大厅和游戏周边相关的  
*********************************************************************--]]
--
WWJNet=WWJNet or {}
--全局
-- Net.Route=Route
WWJNet.Route = {
    
    MSG_QUERY_CONNECTOR = "gate.gateHandler.queryConnector";
    MSG_MACHINE_USE = "machine.machineHandler.machineUse";

    MSG_STREET_LOGIN = "connector.connectorHandler.streetLogin";
    MSG_MACHINE_ACCOUNTS = "machine.machineHandler.machineAccounts";
    MSG_MACHINE_GAME_START = "machine.machineHandler.machineGameStart";
    MSG_MACHINE_GAME_STOP = "machine.machineHandler.machineGameStop";
    MSG_MACHINE_GAME_SPECIAL_START = "machine.machineHandler.machineSpecialGameStart";
    MSG_MACHINE_GAME_SPECIAL_STOP = "machine.machineHandler.machineSpecialGameStop";
    MSG_MACHINE_GAME_OVER = "machine.machineHandler.gameOver";
    MSG_MACHINE_HEART = "machine.machineHandler.machineHeart";
    MSG_MACHINE_BOOT = "machine.machineHandler.machineReboot";

    MSG_MACHINE_USER_LINK = "onMachineUserLink";
    MSG_MACHINE_USER_UPDATE = "onMachineUserUpdate";
    MSG_MACHINE_NOTICE = "onMachineNotice";
    MSG_WE_CHAT_LINK_CHANGE = "onWeChatLinkChange";
    MSG_MACHINE_PRIZE_UPDATE = "onMachinePrizeUpdate";
    MSG_MACHINE_PRIZE_ADD = "onMachinePrizeAdd";
    MSG_MACHINE_PRIZE_DELETE = "onMachinePrizeDelete";
    MSG_STREET_STATE = "onStreetState";
    MSG_MACHINE_REMOTE_BOOT = "onMachineReboot";

}

