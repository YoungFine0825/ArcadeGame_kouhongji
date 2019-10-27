--[[********************************************************************
*      作者： jordenwu
*      时间： 07/20/17 17:02:27
*      描述： 游戏网络服务 负责统一的分发游戏逻辑包 和处理游戏逻辑收包
*********************************************************************--]]
--机器网络
local MachineNetworkClass = DeclareClass("MachineNetworkClass")
--
function MachineNetworkClass:ctor()
    --套接字连接
    self._socket = ClassLib.SocketTCPClass.new()
    --定时减弱
    self._socket.TickTime = 1
    self._socket.Name = "MachineNetSocketTcp"

    --消息处理
    self._commandHandlers = {}
    --标记
    self._isConnected = false
    --组解包
    self._packer = ClassLib.MachineNetPackerClass.new()
    --连接状态代理
    self.Delegate = false
    --
    --服务器时间戳
    self._serverTimeStamp = false
    self._timeOffset = false
    --
    self._ip = ""
    self._port = 0
    self._uid = 0
end

-- 初始化
-- @return 无
function MachineNetworkClass:Initialize()
    LogD("----MachineNetwork Initialize-------------")
    if self._socket then
        self._socket.EventDelegate = self
    end
    LogD("-----------MachineNetwork Register Pb Files----------")
    self:RegisterPbFiles()
end

-- 注册通用的 pb协议到pb
-- @return 无
function MachineNetworkClass:RegisterPbFiles()
    local buffer = Interaction.LuaInteraction.GetResourceBytes("PbFiles/pb_common.pb")
    self._packer:RegistePbFile(buffer)
    buffer = Interaction.LuaInteraction.GetResourceBytes("PbFiles/pb_cs_msg.pb")
    self._packer:RegistePbFile(buffer)
end

-- 反初始化
-- @return 无
function MachineNetworkClass:UnInitialize()
    if self._socket then
        self._socket:UnInit()
        self._socket = false
    end
    if self._packer then
        self._packer:UnInit()
        self._packer = false
    end

    if next(self._commandHandlers) then
        LogE("MachineNetwork Uninitialize : command handler is not empty check module")
        for _, v in pairs(self._commandHandlers) do
            LogE("Need RemoveCommand Class-->" .. v.Obj.__classname)
        end
    end
    self.Delegate = false
end

-- 添加网络消息接收回调
-- @param cid  Net.Command.?
-- @param obj
-- @param func
-- @return 无
function MachineNetworkClass:AddCommandHandler(cid, obj, func)
    --
    if not cid or not obj or not func then
        local message =
            string.format(
            "MachineNetwork AddCommandHandler: invalid parameter(msgID:%x, class:%s)",
            cid or -1,
            obj and obj.__classname or "nil"
        )
        LogE(message)
        return
    end
    --
    if self._commandHandlers[cid] then
        LogE("MachineNetwork AddCommandHandler: duplicate add command handler - %x", cid)
        return
    end
    ---
    local data = Pool:CreateTable()
    data.Obj = obj
    data.Func = func
    self._commandHandlers[cid] = data
end

-- 移除回调
-- @param cid
-- @return 无
function MachineNetworkClass:RemoveCommandHandlerByCID(cid)
    if not cid then
        LogE("MachineNetwork RemoveCommandHandlerByCID : invalid parameter")
    end
    local data = self._commandHandlers[cid]
    if data then
        self._commandHandlers[cid] = nil
        Pool:DestroyTable(data)
    end
end

-- 移除回调
-- @param obj
-- @return 无
function MachineNetworkClass:RemoveCommandHandlerByObj(obj)
    if not obj then
        LogE("MachineNetworkClass.RemoveCommandHandlerByObj : invalid parameter")
        return
    end
    for k, v in pairs(self._commandHandlers) do
        if v.Obj == obj then
            self._commandHandlers[k] = nil
            Pool:DestroyTable(v)
        end
    end
end

function MachineNetworkClass:RefreshNetInfo(ip, port, uid)
    if not ip or not port or not uid then
        LogE("MachineNetwork RefreshNetInfo Error Args")
        return
    end
    self._ip = ip
    self._port = port
    self._uid = uid
    --
    if self._socket then
        self._socket:Init(ip, port, false)
    end
    if self._packer then
        self._packer.Uid = tonumber(uid)
    end
end

-- 打开连接
-- @param ip
-- @param port
-- @return 无
function MachineNetworkClass:OpenConnect()
    if not self._socket then
        return
    end
    if not self._ip or not self._port or not self._uid then
        LogE("MachineNetwork OpenConnect Error No Ip Port  Do RefreshNetInfo First")
        return
    end
    LogD("MachineNetwork OpenConnect: ip = %s, port = %s", self._ip, self._port)
    self._socket:Connect()
end

-- 关闭连接
function MachineNetworkClass:CloseConnnect()
    if not self._socket then
        return
    end
    self._socket:Close(true)
    self._isConnected = false
end

--发送网络消息
-- @param cid   CommandId
-- @param params 参数
-- @return 无
function MachineNetworkClass:SendMsg(cid, params)
    if not self._socket then
        return false
    end
    if not self._isConnected then
        return false
    end
    LogD("----------<color=yellow>MachineNetwork SendMsg CommandID:%d</color>--------", cid)
    local packet, pbName = self._packer:CreatePacket(cid, params)
    if packet then
        self._socket:Send(packet)
    end
    return true
end

--开始发送心跳包
function MachineNetworkClass:StartHearbeat(interval)
    local hi = interval or 30
    if hi == 0 then
        hi = 30
    end
    self._socket:StartHearbeat(hi)
end

--发送心跳逻辑包
function MachineNetworkClass:SendHearbeart()
    LogD("MachineNetwork SendHearbeart")
    self:SendMsg(MachineNet.Cmd.cs_req_heart_beat, {haha = true})
end

-- 处理套接字收到的数据
-- @param param
-- @return 无
function MachineNetworkClass:_DealSocketData(param)
    if not param then
        return
    end
    local data = param.data
    if not data then
        return
    end

    local rspMsgs = self._packer:ParsePackets(data)
    if rspMsgs then
        --处理回包
        self:_DealRspMsgs(rspMsgs)
    end
end

-- 处理回报到具体的RSP
-- @param msgs
-- @return 无
function MachineNetworkClass:_DealRspMsgs(msgs)
    if (not msgs) or (#msgs == 0) then
        return
    end
    for _, v in ipairs(msgs) do
        LogD("-----><color=yellow>MachineNetwork Receive cmd %d</color><----", v.cmd)
        LogBlock(v)
        self:_DealRspCommand(v.cmd, v)
    end
end

-- 处理回包到具体的业务CMD
-- @return 无
function MachineNetworkClass:_DealRspCommand(cmd, pbcMsg)
    if not pbcMsg then
        return
    end

    --心跳包
    if cmd == MachineNet.Cmd.cs_rsp_heart_beat then
        self:_DealHearbeatRsp(pbcMsg.cs_rsp_heart_beat)
        return
    end

    --oneof name
    local msgKey = self._packer:GetPBCSMsgLogicMsgKey(cmd)
    if not msgKey then
        return
    end

    --业务包
    local data = self._commandHandlers[cmd]
    if data then
        data.Func(data.Obj, pbcMsg[msgKey],pbcMsg.result)
    end
end

-- 处理心跳回报
-- @return 无
function MachineNetworkClass:_DealHearbeatRsp(msg)
    if not msg or not msg.timestamp then
        return
    end
    local timeStamp = msg.timestamp
    LogD("-->MachineNetwork Recv Hearbeat%d<--", timeStamp)
    if self._socket then
        self._socket:ClearBeatTimeout()
    end
    self._serverTimeStamp = timeStamp
    self._timeOffset = timeStamp - os.time()
end

-- Socket事件处理
-- @param eventType
-- @param param
-- @return 无
function MachineNetworkClass:OnSocketTcpEvent(eventType, param)
    --数据来
    if eventType == SocketTCPEventType.EVENT_DATA then
        self:_DealSocketData(param)
        return
    end
    --套接字关闭
    if eventType == SocketTCPEventType.EVENT_CLOSE then
        LogD("MachineNetwork Scoket Close")
        self._isConnected = false
        self._packer:Reset()
        return
    end
    --套接字彻底结束事务
    if eventType == SocketTCPEventType.EVENT_CLOSED then
        LogD("MachineNetwork Scoket Final Closed")
        self:CloseConnnect()
        self._isConnected = false
        if self.Delegate and self.Delegate.OnNetworkClosed then
            self.Delegate:OnNetworkClosed()
        end
        return
    end

    --套接字连接成功
    if eventType == SocketTCPEventType.EVENT_CONNECTED then
        LogD("MachineNetwork Scoket Connected")
        self._isConnected = true
        --通知代理
        if self.Delegate and self.Delegate.OnConnectToSvrBack then
            self.Delegate:OnConnectToSvrBack(true)
        end
        return
    end
    --连接失败
    if eventType == SocketTCPEventType.EVENT_CONNECT_FAILURE then
        LogD("MachineNetwork Scoket Connect Failed")
        self._isConnected = false
        --通知代理
        if self.Delegate and self.Delegate.OnConnectToSvrBack then
            self.Delegate:OnConnectToSvrBack(false)
        end
    end

    --这里是套接字内部的 重新连接
    if eventType == SocketTCPEventType.EVENT_CONNECT_RETRY then
        LogD("MachineNetwork Scoket Retry Connect")
        self._isConnected = false
        self._packer:Reset()
        return
    end

    --套接字内部重连失败
    if eventType == SocketTCPEventType.EVENT_CONNECT_RETRY_FAILED then
        LogD("MachineNetwork Scoket Retry Connect Failed")
        self._isConnected = false
        self._packer:Reset()
        return
    end
    --心跳发送请求
    if eventType == SocketTCPEventType.EVENT_SEND_HEARBEAT then
        self:SendHearbeart()
        return
    end
end

-------------------------------------------------------
-- 获取服务器时间戳
function MachineNetworkClass:GetServerTime()
    local timeStamp = os.time()
    if self._timeOffset then
        timeStamp = timeStamp + self._timeOffset
    end
    return timeStamp
end

-- 获取服务器format格式化日期、时间的字串或表
function MachineNetworkClass:GetServerFormatTime(arg)
    local formatStr = arg or "%X"
    return os.date(formatStr, self:GetServerTime())
end

--获取网络延迟毫秒
function MachineNetworkClass:GetNetDealyTime()
    if not self._socket then
        return 0
    end
    local delay = self._socket._netDealyTime
    if delay <= 0 then
        delay = 0.01
    end
    local vv = math.floor(delay * 1000 - 100)
    if vv <= 0 then
        vv = 10
    end
    return vv
end
--------------------------------------------------------------
---全局
MachineNetwork = MachineNetworkClass.new()
