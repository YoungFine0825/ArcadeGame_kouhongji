--[[********************************************************************
*      作者： jordenwu
*      时间： 07/20/17 10:46:22
*      描述： luasocket 套接字封装
*********************************************************************--]]
--套接字事件类型定义
SocketTCPEventType = {
    --数据来
    EVENT_DATA = 1,
    --套接字关闭
    EVENT_CLOSE = 2,
    --套接字链接
    EVENT_CONNECTED = 3,
    --连接失败
    EVENT_CONNECT_FAILURE = 4,
    --重新连接
    EVENT_CONNECT_RETRY = 5,
    --重连失败
    EVENT_CONNECT_RETRY_FAILED = 6,
    --重连成功
    EVENT_CONNECT_RETRY_SUCCEED = 7,
    --心跳请求
    EVENT_SEND_HEARBEAT = 8,
    --套接字彻底结束 事务
    EVENT_CLOSED=9,
}

-- TCP 套接字包装
local SocketTCPClass = DeclareClass("SocketTCPClass")

--默认构造函数
function SocketTCPClass:ctor()
    --lua socket
    self._socket = require("socket")
    Assert(self._socket)
    --
    self._host = false
    self._port = 0
    --
    self._tcp = false

    --是否重连 当连接失败
    self._isRetryConenct = false
    --已经重连尝试次数
    self._reconnectTimes = 0
    --标记是否已经连接
    self._isConnected = false
    --上次心跳发包时间
    self._lastSendHearbeatTime = self._socket.gettime()
    --网络延迟时间毫秒
    self._netDealyTime = 0

    --等待心跳
    self._waitHearbeatRsp = false
    --暂停标记
    self._paused = false
    --标记是否正在连接
    self._isConnecting = false
    --等待连接时间
    self._waitConnect = 0
    --是否已经发送心跳包
    self._isStaredHearbeat = false

    self.Name = "SocketTCP"
    --tick 时间间隔 s
    self.TickTime = 0.03
    --重连延时 s
    self.ReconnectDelayTime = 5
    --套接字连接失败时间 s
    self.ConnectFailTimeout = 3
    --重连尝试次数
    self.ReconnectTryTimes = 3
    --心跳包时间间隔 s
    self.HearbeatTime = 10
    --心跳超时时间
    self.HearbeatTimeOut = 30

    --lua socket 标记
    self.STATUS_CLOSED = "closed"
    self.STATUS_NOT_CONNECTED = "Socket is not connected"
    self.STATUS_ALREADY_CONNECTED = "already connected"
    self.STATUS_TIMEOUT = "timeout"

    --事件委托 OnSocketTcpEvent(eventType)
    self.EventDelegate = false

end

-- 初始化函数
-- @param host 主机
-- @param port 端口
-- @param retryConnectWhenFailure  失败是否自动重连
function SocketTCPClass:Init(host, port, retryConnectWhenFailure)
    self._host = host
    self._port = port
    self._isRetryConenct = retryConnectWhenFailure or false
end


-- 反初始化
function SocketTCPClass:UnInit()
    if self._tcp then
        self._tcp:close()
        self._tcp = false
    end
    self._socket = false
    self._waitHearbeatRsp = false
    self._isConnected = false
    self._isConnecting = false
    self._isStaredHearbeat = false
    --移除所有定时器
    ScheduleService:RemoveTimer(self)
end

-- 套接字时间
-- @return 无
function SocketTCPClass:GetTime()
    if not self._socket then
        return 0
    end
    --精确到 小数毫秒
    return self._socket.gettime()
end

-- 暂停
-- @return 无
function SocketTCPClass:Pause()
    self._paused = true
end

-- 重启
-- @return 无
function SocketTCPClass:Resume()
    self._lastSendHearbeatTime = self:GetTime()
    self._paused = false
end

-- 开始连接
-- @param host 主机
-- @param port 端口
-- @param retryConnectWhenFailure  是否失败重连
function SocketTCPClass:Connect(host, port)
    
    if host then
        self._host = host
    end
    if port then
        self._port = port
    end
    --
    Assert(self._host or self._port, "Host and port are necessary!")
    --
    local isipv6_only = false
    local addrinfo, err = self._socket.dns.getaddrinfo(self._host)
    for i, v in ipairs(addrinfo) do
        if v.family == "inet6" then
            isipv6_only = true
            self._host = v.addr
            break
        end
    end
    LogD("isipv6_only->%s", tostring(isipv6_only))
    LogD(string.format("%s.connect(%s, %d)", self.Name, self._host, self._port))
    for k, v in pairs(addrinfo) do
        for m, n in pairs(v) do
            print(m, n)
        end
    end

    if isipv6_only then
        self._tcp = self._socket.tcp6()
    else
        self._tcp = self._socket.tcp()
    end

    -- 异常处理
    if not self._tcp then
        LogE("SocketTCP Error Get TCP")
        self:Close()
        self:_connectFailure()
        return
    end
    --
    self._tcp:settimeout(0)
    self._tcp:setoption("keepalive", true)
    self._tcp:setoption("tcp-nodelay", true)

    --检查连接
    -- not connected
    if not self:_checkConnect() then
        ScheduleService:RemoveTimer(self, self._connectTimeTick)
        --定时去判断连接是否成功
        self._waitConnect = 0
        ScheduleService:AddTimer(self, self._connectTimeTick, self.TickTime, true)
    end
end

-- 清除心跳标记 当逻辑层收到心跳回复 调用
function SocketTCPClass:ClearBeatTimeout()
    local curT = self:GetTime()
    self._netDealyTime = curT - self._lastSendHearbeatTime
    self._lastSendHearbeatTime = curT
    self._waitHearbeatRsp = false
end

--发送数据
function SocketTCPClass:Send(data)
    if not self._isConnected then
        LogE(self.Name .. "is not connected,not send")
        return
    end
    self._tcp:send(data)
end

--关闭连接
function SocketTCPClass:Close(otherCall)
    if self._tcp then
        self._tcp:close()
    end
    self._waitHearbeatRsp = false
    self._isConnected = false
    self._isConnecting = false
    self._isStaredHearbeat = false
    ScheduleService:RemoveTimer(self)
    if not otherCall then
        self:_sendEvent(SocketTCPEventType.EVENT_CLOSE)
    end
end

--开始发送心跳
function SocketTCPClass:StartHearbeat(interval)
    --心跳间隔
    self.HearbeatTime = interval
    self.HearbeatTimeOut = interval * 3
    --
    --请求发送心跳第一个
    self._lastSendHearbeatTime = self:GetTime()
    self._isStaredHearbeat = true
    self:_sendEvent(SocketTCPEventType.EVENT_SEND_HEARBEAT)
    --
end

--发送事件给委托
function SocketTCPClass:_sendEvent(etype, param)
    if (self.EventDelegate) and (self.EventDelegate.OnSocketTcpEvent) then
        self.EventDelegate:OnSocketTcpEvent(etype, param)
    end
end

-----------------------------------建立连接----------------------------
function SocketTCPClass:_connectTimeTick()
    if self._isConnected then
        return
    end
    self._waitConnect = self._waitConnect + self.TickTime
    self._isConnecting = true
    --建立连接超时判断
    if self._waitConnect >= (self.ConnectFailTimeout) then
        LogD("SocketTcp Connect Time Out")
        self._waitConnect = 0
        self:Close()
        self:_connectFailure()
        return
    end
    --定时检查连接是否成功
    self:_checkConnect()
end

--检查连接状态
function SocketTCPClass:_checkConnect()
    local succ = self:_connect()
    if succ then
        self:_onConnected()
    end
    return succ
end

-- 连接成功
-- @return 无
function SocketTCPClass:_onConnected()
    
    self._isConnected = true
    self._isConnecting = false
    self._waitHearbeatRsp = false
    self._waitConnect = 0
    --事件
    self:_sendEvent(SocketTCPEventType.EVENT_CONNECTED)
    --
    if(self._reconnectTimes>=0) then
        self:_sendEvent(SocketTCPEventType.EVENT_CONNECT_RETRY_SUCCEED)
        self._reconnectTimes = 0
    end
    --移除所有定时器
    ScheduleService:RemoveTimer(self)
    -- 开始接收数据
    ScheduleService:AddTimer(self, self._tick, self.TickTime, true)
end

--连接失败
function SocketTCPClass:_connectFailure(reconnect)
    -- 已经重连过就继续重连
    if self._reconnectTimes ~= 0 then
        self:_reconnect()
    else
        self:_sendEvent(SocketTCPEventType.EVENT_CONNECT_FAILURE)
    end
    
end

--重新连接
function SocketTCPClass:_reconnect(immediately)
    --不重连
    if not self._isRetryConenct then
        --彻底结束事务
        self:_sendEvent(SocketTCPEventType.EVENT_CLOSED)
        return
    end
    --重连次数超过最大尝试次数
    if self._reconnectTimes >= self.ReconnectTryTimes then
        self:Close()
        self:_sendEvent(SocketTCPEventType.EVENT_CONNECT_RETRY_FAILED)
        --彻底结束事务
        self:_sendEvent(SocketTCPEventType.EVENT_CLOSED)
        --
        self._reconnectTimes = 0
        return
    end
    --立即重连
    if immediately then
        self:Connect()
        return
    end
    ScheduleService:RemoveTimer(self, self._reconnectTimer)
    --延时重连
    ScheduleService:AddTimer(self, self._reconnectTimer, self.ReconnectDelayTime, false)
end

--重连时钟
function SocketTCPClass:_reconnectTimer()
    self._reconnectTimes = self._reconnectTimes + 1
    LogD(self.Name.."正在进行第%d次重连", self._reconnectTimes)
    self:Connect()
    self:_sendEvent(SocketTCPEventType.EVENT_CONNECT_RETRY, {times = self._reconnectTimes})
end

--- When connect a connected socket server, it will return "already connected"
-- @see: http://lua-users.org/lists/lua-l/2009-10/msg00584.html
function SocketTCPClass:_connect()
    local succ, status = self._tcp:connect(self._host, self._port)
    return succ == 1 or status == self.STATUS_ALREADY_CONNECTED
end

--被断开
function SocketTCPClass:_onDisconnect()
    
    LogD("%s._onDisConnect", self.Name)
    self._isConnected = false
    self._isConnecting = false
    self._reconnectTimes = 0
    self:_reconnect()

end

function SocketTCPClass:_tick()
    
    if self._isConnected then
        
        local diffTime = self:GetTime() - (self._lastSendHearbeatTime or self:GetTime())
        --这里是默认心跳时间
        if diffTime >= self.HearbeatTime then
            --判断先设备网络
            if not NativeService:GetIsNetEnable() then
                LogD(self.Name.."----> Device Net Closed  Will Close Socket")
                self:Close()
                --连接断开
                self:_onDisconnect()
                return
            end
        end

        --最后的
        if self._isStaredHearbeat and self.HearbeatTime > 0 and not self._paused then
            -- 心跳超时
            if (diffTime >= self.HearbeatTimeOut) and self._waitHearbeatRsp then
                LogD(self.Name.."-->Hearbeat Time Out  Will Close Socket")
                self:Close()
                --连接断开
                self:_onDisconnect()
                return
            elseif diffTime >= self.HearbeatTime and not self._waitHearbeatRsp then
                -- 检查是否要发心跳包距离上一次
                self._waitHearbeatRsp = true
                self:_sendEvent(SocketTCPEventType.EVENT_SEND_HEARBEAT)
            end
        end
    end
    --
    while true do
        -- if use "*l" pattern, some buffer will be discarded
        local __body, __status, __partial = self._tcp:receive("*a") 
        ---print("body:", __body, "__status:", __status, "__partial:", __partial)
        if __status == self.STATUS_CLOSED or __status == self.STATUS_NOT_CONNECTED then
            self:Close()
            if self._isConnected then
                self:_onDisconnect()
            else
                self:_connectFailure()
            end
            return
        end

        if (__body and string.len(__body) == 0) or (__partial and string.len(__partial) == 0) then
            return
        end

        if __body and __partial then
            __body = __body .. __partial
        end
        --处理数据
        self:_sendEvent(
            SocketTCPEventType.EVENT_DATA,
            {data = (__partial or __body), partial = __partial, body = __body}
        )
    end
end

