--[[********************************************************************
*      作者： jordenwu
*      时间： 07/20/17 17:02:27
*      描述： 游戏网络服务 负责统一的分发游戏逻辑包 和处理游戏逻辑收包
*********************************************************************--]]
local LSKNetworkClass = DeclareClass("LSKNetworkClass")
--
function LSKNetworkClass:ctor()
    --套接字连接
    self._socket = ClassLib.SocketTCPClass.new()
    --消息处理
    self._commandHandlers = {}
    --标记
    self._isConnected = false
    --组解包
    self._packer = ClassLib.LSKNetPackerClass.new()
    --连接状态代理
    self.Delegate = false
    --
    --服务器时间戳
    self._serverTimeStamp = false
    self._timeOffset = false

    --网络配置
    self._ip = ""
    self._port = 0
    self._uid = 0
end

-- 初始化
-- @return 无
function LSKNetworkClass:Initialize()
    LogD("----LSKNetwork Initialize-------------")
    if self._socket then
        self._socket.EventDelegate = self
    end
    LogD("-----------LSKNetwork Register Pb Files----------")
    local buffer = Interaction.LuaInteraction.GetResourceBytes("PbFiles/pb_common.pb")
    self._packer:RegistePbFile(buffer)
    buffer = Interaction.LuaInteraction.GetResourceBytes("PbFiles/pb_cs_msg.pb")
    self._packer:RegistePbFile(buffer)
end

-- 反初始化
-- @return 无
function LSKNetworkClass:UnInitialize()
    if self._socket then
        self._socket:UnInit()
        self._socket = false
    end
    if self._packer then
        self._packer:UnInit()
        self._packer = false
    end

    if next(self._commandHandlers) then
        LogE("LSKNetwork Uninitialize : command handler is not empty check module")
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
function LSKNetworkClass:AddCommandHandler(cid, obj, func)
    if not cid or not obj or not func then
        local message =
            string.format(
            "LSKNetwork.AddCommandHandler : invalid parameter(msgID:%x, class:%s)",
            cid or -1,
            obj and obj.__classname or "nil"
        )
        LogE(message)
        return
    end
    --
    if self._commandHandlers[cid] then
        LogE("LSKNetwork: duplicate add command handler - %x", cid)
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
function LSKNetworkClass:RemoveCommandHandlerByCID(cid)
    if not cid then
        LogE("LSKNetwork RemoveCommandHandlerByCID : invalid parameter")
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
function LSKNetworkClass:RemoveCommandHandlerByObj(obj)
    if not obj then
        LogE("LSKNetwork RemoveCommandHandlerByObj : invalid parameter")
        return
    end

    for k, v in pairs(self._commandHandlers) do
        if v.Obj == obj then
            self._commandHandlers[k] = nil
            Pool:DestroyTable(v)
        end
    end
end

--刷新连接信息
function LSKNetworkClass:RefreshNetInfo(ip, port, uid)
    if not ip or not port or not uid then
        LogE("LSKNetwork RefreshNetInfo Error Args")
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
function LSKNetworkClass:OpenConnect()
    if not self._socket then
        return
    end
    LogD("LSKNetwork OpenConnect: ip = %s, port = %s", self._ip, self._port)
    self._socket:Connect()
end

-- 关闭连接
-- @return 无
function LSKNetworkClass:CloseConnnect()
    if not self._socket then
        return
    end
    self._socket:Close()
    self._isConnected = false
end

--发送网络消息
-- @param cid   CommandId
-- @param params 参数
-- @return 无
function LSKNetworkClass:SendMsg(cid, params)
    if not self._socket then
        return false
    end
    if not self._isConnected then
        return false
    end
    LogD("----------<color=yellow>LSKNetwork SendMsg CommandID:%d </color>--------", cid)
    local packet, pbName = self._packer:CreatePacket(cid, params)
    if packet then
        self._socket:Send(packet)
    end
    return true
end

--开始发送心跳包
function LSKNetworkClass:StartHearbeat(interval)
    local hi = interval or 30
    if hi == 0 then
        hi = 30
    end
    self._socket:StartHearbeat(hi)
end

--发送心跳逻辑包
function LSKNetworkClass:SendHearbeart()
    LogD("LSKNetwork SendHearbeart")
    self:SendMsg(LSKNet.Cmd.cs_req_heart_beat, {haha = true})
end

-- 处理套接字收到的数据
-- @param param
-- @return 无
function LSKNetworkClass:_DealSocketData(param)
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
function LSKNetworkClass:_DealRspMsgs(msgs)
    if (not msgs) or (#msgs == 0) then
        return
    end
    --
    for _, v in ipairs(msgs) do
        LogD("-----><color=yellow>LSKNetwork Receive cmd %d</color><----", v.cmd)
        LogBlock(v)
        self:_DealRspCommand(v.cmd, v)
    end
end

-- 处理回包到具体的业务CMD
-- @return 无
function LSKNetworkClass:_DealRspCommand(cmd, pbcMsg)
    if not pbcMsg then
        return
    end

    --心跳包
    if cmd == LSKNet.Cmd.cs_rsp_heart_beat then
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
        data.Func(data.Obj, pbcMsg[msgKey], pbcMsg.result, pbcMsg.cmd)
    end
end

-- 处理心跳回报
-- @return 无
function LSKNetworkClass:_DealHearbeatRsp(msg)
    if not msg or not msg.timestamp then
        return
    end
    local timeStamp = msg.timestamp
    LogD("-->LSKNetwork Recv Hearbeat%d<--", timeStamp)
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
function LSKNetworkClass:OnSocketTcpEvent(eventType, param)
    --数据来
    if eventType == SocketTCPEventType.EVENT_DATA then
        self:_DealSocketData(param)
        return
    end

    --套接字关闭
    if eventType == SocketTCPEventType.EVENT_CLOSE then
        LogD("LSK套接字关闭ing")
        self._isConnected = false
        self._packer:Reset()
        return
    end

    --套接字已经关闭
    if eventType == SocketTCPEventType.EVENT_CLOSED then
        LogD("LSKNetwork Scoket Final Closed")
        self:CloseConnnect()
        self._isConnected = false
        if self.Delegate and self.Delegate.OnNetworkClosed then
            self.Delegate:OnNetworkClosed()
        end
        return
    end

    --套接字连接成功
    if eventType == SocketTCPEventType.EVENT_CONNECTED then
        LogD("LSKNetwork Scoket Connected")
        self._isConnected = true
        --通知代理
        if self.Delegate and self.Delegate.OnConnectToSvrBack then
            self.Delegate:OnConnectToSvrBack(true)
        end
        return
    end
    --连接失败
    if eventType == SocketTCPEventType.EVENT_CONNECT_FAILURE then
        LogD("LSKNetwork Scoket Connect Failed")
        self._isConnected = false
        --通知代理
        if self.Delegate and self.Delegate.OnConnectToSvrBack then
            self.Delegate:OnConnectToSvrBack(false)
        end
    end

    --这里是套接字内部的 重新连接
    if eventType == SocketTCPEventType.EVENT_CONNECT_RETRY then
        self._isConnected = false
        self._packer:Reset()
        return
    end

    --套接字内部重连失败
    if eventType == SocketTCPEventType.EVENT_CONNECT_RETRY_FAILED then
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
function LSKNetworkClass:GetServerTime()
    local timeStamp = os.time()
    if self._timeOffset then
        timeStamp = timeStamp + self._timeOffset
    end
    return timeStamp
end

-- 获取服务器format格式化日期、时间的字串或表
function LSKNetworkClass:GetServerFormatTime(arg)
    local formatStr = arg or "%X"
    return os.date(formatStr, self:GetServerTime())
end

--获取网络延迟毫秒
function LSKNetworkClass:GetNetDealyTime()
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
