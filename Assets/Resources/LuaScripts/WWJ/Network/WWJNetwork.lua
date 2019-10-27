--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-01
*		描述： 娃娃机游戏网络控制
*********************************************************************--]]
local WWJNetworkClass = DeclareClass("WWJNetworkClass")
--
function WWJNetworkClass:ctor()
    
    --消息处理
    self._commandHandlers={}
    --连接状态代理
    self.ConnectDelegate=false
    --
    self._csNetwork=false
    
end

-- 初始化
-- @return 无
function WWJNetworkClass:Initialize()
    --测试服
    if Global.IsDebug then
        local gateIp = "ghtest.scbczx.com"
        local gatePort = 8001
        local gateQueryRouteId="gate.gateHandler.queryConnector"
        self._csNetwork= Interaction.NetworkService:GetConnector(gateIp,gatePort,gateQueryRouteId)
    else
        local gateIp = "ghtest.scbczx.com"
        local gatePort = 8001
        local gateQueryRouteId="gate.gateHandler.queryConnector"
        self._csNetwork= Interaction.NetworkService:GetConnector(gateIp,gatePort,gateQueryRouteId)
    end
    if not self._csNetwork then
        LogE("WWJNetwork Error Get Nil Connector")
        return
    end
    self._csNetwork.ConnectHook=function(state)
        self:OnCsConnectChanged(state)
    end

    self._csNetwork.RecvMsgHook=function (route,msg)
        self:OnCsRecvMsg(route,msg)
    end

end

-- 反初始化
-- @return 无
function WWJNetworkClass:UnInitialize()
    
    --销毁
    if self._csNetwork then
        self._csNetwork.ConnectHook=nil
        self._csNetwork.RecvMsgHook=nil
        Interaction.NetworkService:DestroyConnector(self._csNetwork)
        self._csNetwork=false
    end
    --
    if next(self._commandHandlers) then
        LogD("<color=yellow>Error</color>WWJNetworkClass.Uninitialize : command handler is not empty check module")
        for _,v in pairs(self._commandHandlers) do
           LogD("<color=yellow>Error</color>--> Need RemoveCommand Class-->"..v.Obj.__classname) 
        end
    end
    self.ConnectDelegate=false
    
end


function WWJNetworkClass:OnCsConnectChanged(state)
    LogD("On CS Net OnCsConnectChanged")
    --通知代理
    if self.ConnectDelegate and self.ConnectDelegate.OnConnectToSvrBack then
        self.ConnectDelegate:OnConnectToSvrBack(state)
    end

end

function WWJNetworkClass:OnCsRecvMsg(route,msg)
    
    LogD("<color=yellow>[WWJNetwork Recv]</color>Route:%s,Msg:%s",route,msg)
    if string.IsNullOrEmpty(msg) then
        return
    end
    local dmsg=RapidJson.Decode(msg)
    
    if not dmsg then
        LogE("Net Decode Msg Error")
        return
    end
    --
    local data = self._commandHandlers[route]
    if data then
        data.Func(data.Obj, dmsg)
    end

end


-- 添加网络消息接收回调
-- @param cid  Net.Command.?
-- @param obj 
-- @param func 
-- @return 无
function WWJNetworkClass:AddMsgHandler(cid,obj,func)
    
    if not cid or not obj or not func then
        local message = string.format( "WWJNetworkClass.AddCommandHandler : invalid parameter(msgID:%x, class:%s)", cid or -1, obj and obj.__classname or "nil")
        LogE(message)
        return
    end
    --
    if self._commandHandlers[cid] then
        LogE("WWJNetwork : duplicate add command handler - %x", cid)
        return
    end
    ---
    local data=Pool:CreateTable()
    data.Obj=obj
    data.Func=func
    self._commandHandlers[cid]=data
    
end

-- 移除回调
-- @param cid 
-- @return 无
function WWJNetworkClass:RemoveMsgHandlerByRoute(cid)
    
    if not cid then
        LogD("<color=yellow>Error</color>---->WWJNetworkClass.RemoveCommandHandlerByCID : invalid parameter")
    end
    local data=self._commandHandlers[cid]
    if data then
       self._commandHandlers[cid]=nil
       Pool:DestroyTable(data)
    end

end

-- 移除回调
-- @param obj 
-- @return 无
function WWJNetworkClass:RemoveMsgHandlerByObj(obj)
    
    if not obj then
        LogD("<color=yellow>Error</color>--->WWJNetworkClass.RemoveCommandHandlerByObj : invalid parameter")
        return
    end

    for k, v in pairs(self._commandHandlers) do
        if v.Obj == obj then
            self._commandHandlers[k] = nil
            Pool:DestroyTable(v)
        end
    end
    
end

-- 打开连接
-- @param ip 
-- @param port 
-- @return 无
function WWJNetworkClass:OpenConnect()
    
    if not self._csNetwork then
        return
    end
    self._csNetwork:OpenConnect()

end


-- 关闭连接 
-- @return 无
function WWJNetworkClass:CloseConnnect()
    
    if not self._csNetwork then
        return
    end
    self._csNetwork:CloseConnect()
end


--发送网络消息
-- @param gameId 
-- @param cid   CommandId
-- @param params 参数
-- @param session 可不要
-- @return 无
function WWJNetworkClass:SendMsg(route,params,session)
    
    if not self._csNetwork then
        return
    end
    local msg=RapidJson.Encode(params)
    if not msg then
        LogE("Sen Msg Encode Error")
        return
    end
    LogD("<color=yellow>[WWJNet Send]</color>:Route:<color=yellow>%s</color>,Msg:%s",route,msg)
    self._csNetwork:SendMsg(route,msg)
    return true

end

