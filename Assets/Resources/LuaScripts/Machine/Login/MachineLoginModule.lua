--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-01
*		描述： 机器登录模块
*********************************************************************--]]
--重连处理
local ReConnectSession = {
    IsWorking = false
}
--断线重连使用
local CachedAuthUrl=""

--
local MachineLoginModuleClass = DeclareClass("MachineLoginModuleClass", ClassLib.ModuleBaseClass)

function MachineLoginModuleClass:ctor()
    self._rebootDelay = 8
    self._rebootCntDown = 0
    --
    self._cachedPassword = ""
    --
    self._tryAuthedCnt = 0
    self._tryAuthMaxCnt = 10
    --
    self._tryConSvrCnt = 0
    self._tryConSvrMaxCnt = 10

    self._authUrlFormat = ""

    self._reloginCnt = 0
    self._reloginMaxCnt = 10
end

function MachineLoginModuleClass:vOnInitializeModule()
    MachineNetwork:AddCommandHandler(MachineNet.Cmd.cs_rsp_m_login, self, self.OnLoginMachineSvrRsp)
    self._authUrlFormat = MachineUrlRoot .. "/machineLogin?machineId=%s&password=%s"
end

function MachineLoginModuleClass:vOnUninitializeModule()
    MachineNetwork:RemoveCommandHandlerByCID(MachineNet.Cmd.cs_rsp_m_login)

    ScheduleService:RemoveTimer(self)
    ScheduleService:RemoveTimer(ReConnectSession)
    ReConnectSession = nil
end

function MachineLoginModuleClass:vOnGameStateEnter(state, fromState, userData)
    --正常开始
    if fromState == "" and state == "LoginGameState" then
        --
        self:UpdateUI("OnEnterLogin")
        --鉴权
        self:DoAuthMachineLogin()
        return
    end
    --子游戏重新回来
    if fromState=="SubGameState" and state=="LoginGameState" then

        MachineNetwork.Delegate = false
        MachineNetwork:CloseConnnect()
        ScheduleService:RemoveTimer(self)
        ScheduleService:RemoveTimer(ReConnectSession)
        ReConnectSession.IsWorking=false
        --
        self:UpdateUI("OnEnterLogin")
        --鉴权
        self:DoAuthMachineLogin()
        --
    end
end

function MachineLoginModuleClass:vOnGameStateLeave(state, toState, userData)
end

function MachineLoginModuleClass:vOnAction(id, argument)
end

--[[
    @desc: 鉴权机器登录
    @return:
]]
function MachineLoginModuleClass:DoAuthMachineLogin()
    --
    local machineId = MachineData.MachineId
    --
    if string.IsNullOrEmpty(self._cachedPassword) then
        local password = ArcadeInputService:GetDeviceKey()
        if string.IsNullOrEmpty(password) then
            --获取设备密码失败，准备重启
            LogE("-----------Get Device Password Error----------")
            self:UpdateUI("ShowTip", "获取设备"..MachineData.MachineId.."密码异常!即将重启！")
            self._rebootCntDown = self._rebootDelay
            ScheduleService:AddTimer(self, self.OnRebootTimer, 1, true)
            return
        end
        self._cachedPassword = password
    end
    --
    local fullUrl = string.format(self._authUrlFormat, machineId, self._cachedPassword)
    LogD("Auth Machine:%s", fullUrl)
    if self._tryAuthedCnt == 0 then
        self:UpdateUI("ShowTip", "开始机器鉴权")
    else
        self:UpdateUI("ShowTip", "重试机器鉴权" .. tostring(self._tryAuthedCnt) .. "次")
    end
    --断线重连 使用
    CachedAuthUrl=fullUrl
    --发送登陆机器大厅Http请求
    HttpService:AsyncGetText(
        fullUrl,
        function(args)
            self:OnMachineAuthRsp(args)
        end
    )
end

--Http Back
function MachineLoginModuleClass:OnMachineAuthRsp(args)
    LogD("Auth Machine Back:%s", args)
    local isSucceed = true
    local errorInfo = ""
    if string.IsNullOrEmpty(args) or args == "error" then
        isSucceed = false
        errorInfo = "鉴权网络错误!"
    end
    local jsonObj = RapidJson.Decode(args)
    if not jsonObj then
        isSucceed = false
        errorInfo = "鉴权返回数据错误!"
    end
    if jsonObj and jsonObj.errCode then
        errorInfo = "鉴权失败:" .. tostring(jsonObj.errCode)
        isSucceed = false
        --街机被删除时，提示重新注册
        if jsonObj.errCode == "20002" then
            self:UpdateUI("ShowTip", "机器ID:"..MachineData.MachineId.."不存在，即将重启设备！请重新注册机器码！")
            --删除机器ID
            PlayerPrefs:DeleteKey("MachineID")
            --重启倒计时
            self._rebootCntDown = self._rebootDelay
            ScheduleService:RemoveTimer(self)
            return ScheduleService:AddTimer(self, self.OnRebootTimer, 1, true)
        end
    end
    if not isSucceed then
        self:UpdateUI("ShowTip", errorInfo.."机器ID:"..MachineData.MachineId)
        ScheduleService:AddTimer(self, self.DelayAuthMachine, 5, false)
        return
    end
    --
    self:UpdateUI("ShowTip", "鉴权成功:"..MachineData.MachineId)
    ScheduleService:RemoveTimer(self)
    self._tryAuthedCnt = 0
    MachineNetwork:RefreshNetInfo(jsonObj.linkUrl, jsonObj.linkPort, jsonObj.uid)
    MachineData.Uid = jsonObj.uid
    MachineData.Token = jsonObj.token
    --初始化广告数据
    ADSystem:InitAdData(jsonObj.adList)
    --下载网络资源
    self:DoDownloadNetAssets()
end

function MachineLoginModuleClass:DelayAuthMachine()
    if self._tryAuthedCnt >= self._tryAuthMaxCnt then
        --重启设备
        self:UpdateUI("ShowTip", "鉴权重试失败,即将重启！"..MachineData.MachineId)
        self._rebootCntDown = self._rebootDelay
        ScheduleService:RemoveTimer(self)
        ScheduleService:AddTimer(self, self.OnRebootTimer, 1, true)
        return
    end
    --
    self._tryAuthedCnt = self._tryAuthedCnt + 1
    self:DoAuthMachineLogin()
end

----------下载资源---------------
function MachineLoginModuleClass:DoDownloadNetAssets()
    local assetsUrls = ADSystem:GetAdAssetsUrls()
    if not assetsUrls or #assetsUrls == 0 then
        --连接机器大厅服务器
        self:DoConnectToSvr()
        return
    end
    --显示下载进度条
    self:UpdateUI("UpdateProgress", 0)
    self:UpdateUI("ShowTip", "正在下载网络资源......")
    NetAssetService:PrepareNetAssets(
        assetsUrls,
        function(progress)
            self:OnDownloadAssetsCallback(progress)
        end
    )
end

function MachineLoginModuleClass:OnDownloadAssetsCallback(progress)
    self:UpdateUI("UpdateProgress", progress)
    if progress >= 1 then
        --连接机器大厅服务器
        self:DoConnectToSvr()
    end
end

---------------------------------------------网络连接------------------
function MachineLoginModuleClass:DoConnectToSvr()
    self:UpdateUI("ShowTip", "连接街机服务器...")
    MachineNetwork.Delegate = self
    MachineNetwork:OpenConnect()
end

--连接服务器或者登录服务器 时候网络断开处理
function MachineLoginModuleClass:OnNetworkClosed()
    self:UpdateUI("ShowTip", "连接街机服务器失败!等待重试")
    ScheduleService:RemoveTimer(self)
    ScheduleService:AddTimer(self, self.Delay2TryConnectSvr, 5, false)
end

--连接服务器回调
function MachineLoginModuleClass:OnConnectToSvrBack(isSucceed)
    if isSucceed then
        self._tryConSvrCnt = 0
        ScheduleService:RemoveTimer(self)
        self:UpdateUI("ShowTip", "连接街机服务器成功,开始登录！")
        self:DoLoginMachineSvr()
    else
        self:UpdateUI("ShowTip", "连接街机服务器失败!等待重试")
        ScheduleService:RemoveTimer(self)
        ScheduleService:AddTimer(self, self.Delay2TryConnectSvr, 5, false)
    end
end

--延迟重试连接服务器
function MachineLoginModuleClass:Delay2TryConnectSvr()
    if self._tryConSvrCnt >= self._tryConSvrMaxCnt then
        MachineNetwork:CloseConnnect(true)
        self:UpdateUI("ShowTip", "连接街机服务器失败,即将重启！")
        self._rebootCntDown = self._rebootDelay
        ScheduleService:RemoveTimer(self)
        ScheduleService:AddTimer(self, self.OnRebootTimer, 1, true)
        self._tryConSvrCnt = 0
        return
    end
    --重新连接
    self._tryConSvrCnt = self._tryConSvrCnt + 1
    self:UpdateUI("ShowTip", string.format("重试连接街机服务器%d次", self._tryConSvrCnt))
    MachineNetwork.Delegate = self
    MachineNetwork:OpenConnect()
    --
end

------------------------------------------------------------------------
--请求登陆街机大厅
function MachineLoginModuleClass:DoLoginMachineSvr()
    local args = {
        token = MachineData.Token
    }
    MachineNetwork:SendMsg(MachineNet.Cmd.cs_req_m_login, args)
end

--[[
    @desc: 登陆街机大厅回调
    --@rsp:CSRspMLoginGame
	--@result: MachineNet.Result
    @return:
]]
function MachineLoginModuleClass:OnLoginMachineSvrRsp(rsp, result)
    if ReConnectSession.IsWorking then
        ReConnectSession:OnLoginMachineSvrRsp(rsp, result)
    else
        --结果
        if result ~= MachineNet.Result.EN_Result_OK then
            --
            if result == MachineNet.Result.EN_Result_Auth_Failed then
                self:UpdateUI("ShowTip", "登录街机服务器鉴权失败,等待重试！")
                self._tryAuthedCnt = 0
                MachineNetwork.Delegate = false
                MachineNetwork:CloseConnnect()
                ScheduleService:RemoveTimer(self)
                ScheduleService:AddTimer(self, self.DelayAuthMachine, 5, false)
            else
                self:UpdateUI("ShowTip", "登录街机服务器失败,等待重试！")
                MachineNetwork.Delegate = false
                MachineNetwork:CloseConnnect()
                ScheduleService:RemoveTimer(self)
                ScheduleService:AddTimer(self, self.Delay2ReLogin, 10, false)
            end
        else
            if rsp then
                --托管断线重连
                MachineNetwork.Delegate = ReConnectSession
                ScheduleService:RemoveTimer(self)
                self._reloginCnt = 0
                self:UpdateUI("ShowTip", "登录街机服务器成功，即将进入游戏！")
                --开始发送心跳包
                if rsp.use_heart_beat then
                    MachineNetwork:StartHearbeat(rsp.heart_beat_interval)
                end
                --判断游戏类型并进入游戏
                GameStateService:ChangeState("SubGameState", rsp)
            else
                self:UpdateUI("ShowTip", "登录街机服务器失败,等待重试！")
                MachineNetwork.Delegate = false
                MachineNetwork:CloseConnnect()
                ScheduleService:RemoveTimer(self)
                ScheduleService:AddTimer(self, self.Delay2ReLogin, 10, false)
            end
        end
    end
end

--重试登录
function MachineLoginModuleClass:Delay2ReLogin()
    if self._reloginCnt >= self._reloginMaxCnt then
        MachineNetwork:CloseConnnect()
        self:UpdateUI("ShowTip", "登录街机服务器失败,即将重启！")
        self._rebootCntDown = self._rebootDelay
        ScheduleService:RemoveTimer(self)
        ScheduleService:AddTimer(self, self.OnRebootTimer, 1, true)
        self._reloginCnt = 0
        return
    end
    --重新连接
    self._reloginCnt = self._reloginCnt + 1
    self:UpdateUI("ShowTip", "连接街机服务器...")
    MachineNetwork.Delegate = self
    MachineNetwork:OpenConnect()
end

-----------------------------------------------------------------------------
function MachineLoginModuleClass:OnRebootTimer()
    if (self._rebootCntDown < 0) then
        ScheduleService:RemoveTimer(self)
        NativeService:RebootDevice()
    else
        if (self._rebootCntDown <= 5) then
            self:UpdateUI("ShowTip", "重启倒计时：" .. self._rebootCntDown)
        end
        self._rebootCntDown = self._rebootCntDown - 1
    end
end

-----------------------------断线重连处理-------------------------------------------
function ReConnectSession:BeginSession()
    LogD("Machine 开始断线重连")
    self.IsWorking = true
    MachineNetwork.Delegate = self
    MachineNetwork:OpenConnect()
end

function ReConnectSession:OnNetworkClosed()
    LogD("----------------------街机网络断开------------------")
    self:BeginSession()
end

function ReConnectSession:OnConnectToSvrBack(isSucceed)
    if isSucceed then
        LogD("Machine 重新连接成功")
        --重新发登录
        local args = {
            token = MachineData.Token
        }
        MachineNetwork:SendMsg(MachineNet.Cmd.cs_req_m_login, args)
    else
        --
        LogD("Machine 重新连接失败")
        MachineNetwork.Delegate = false
        MachineNetwork:CloseConnnect()
        ScheduleService:RemoveTimer(self)
        ScheduleService:AddTimer(self, self.Delay2RetryConSvrSession, 5, false)
    end
end

function ReConnectSession:Delay2RetryConSvrSession()
    LogD("Machine 重新建立连接")
    self.IsWorking = true
    MachineNetwork.Delegate = self
    MachineNetwork:OpenConnect()
end

--
function ReConnectSession:Delay2AuthAndRetryConSvrSession()
    LogD("Machine 重新鉴权和建立连接")
    self.IsWorking = true

    --发送登陆机器大厅Http请求
    HttpService:AsyncGetText(
        CachedAuthUrl,
        function(args)
            
            LogD(" ReConnectSession Auth Machine Back:%s", args)
            local isSucceed = true
            local errorInfo = ""
            if string.IsNullOrEmpty(args) or args == "error" then
                isSucceed = false
                errorInfo = "ReConnectSession鉴权网络错误!"
            end
            local jsonObj = RapidJson.Decode(args)
            if not jsonObj then
                isSucceed = false
                errorInfo = "ReConnectSession鉴权返回数据错误!"
            end
            if jsonObj and jsonObj.errCode then
                errorInfo = "ReConnectSession鉴权失败:" .. tostring(jsonObj.errCode)
                isSucceed = false
            end
            --
            if not isSucceed then
                LogE(errorInfo)
                --继续延迟鉴权
                ScheduleService:RemoveTimer(self)
                ScheduleService:AddTimer(self, self.Delay2AuthAndRetryConSvrSession, 5, false)
                return
            end
            --
            LogD("ReConnectSession鉴权成功")
            MachineNetwork:RefreshNetInfo(jsonObj.linkUrl, jsonObj.linkPort, jsonObj.uid)
            MachineData.Uid = jsonObj.uid
            MachineData.Token = jsonObj.token
            --
            MachineNetwork.Delegate = self
            MachineNetwork:OpenConnect()
        end
    )
end


function ReConnectSession:OnLoginMachineSvrRsp(rsp, result)
    
    if result ~= MachineNet.Result.EN_Result_OK then
        --
        if result == MachineNet.Result.EN_Result_Auth_Failed then
            LogD("Machine重新登录鉴权失败")
            MachineNetwork.Delegate = false
            MachineNetwork:CloseConnnect()
            ScheduleService:RemoveTimer(self)
            ScheduleService:AddTimer(self, self.Delay2AuthAndRetryConSvrSession, 5, false)
            --
        else
            LogD("Machine 重新登录失败")
            MachineNetwork.Delegate = false
            MachineNetwork:CloseConnnect()
            ScheduleService:RemoveTimer(self)
            ScheduleService:AddTimer(self, self.Delay2RetryConSvrSession, 5, false)
        end
    else
        if rsp then
            self.IsWorking = false
            MachineNetwork.Delegate = self
            LogD("Machine 重新登录成功!")
            --开始发送心跳包
            if rsp.use_heart_beat then
                MachineNetwork:StartHearbeat(rsp.heart_beat_interval)
            end
            --TODO 这里断线回来 具体启动的游戏怕有变更 


            --
        else
            LogD("Machine 重新登录失败")
            MachineNetwork.Delegate = false
            MachineNetwork:CloseConnnect()
            ScheduleService:RemoveTimer(self)
            ScheduleService:AddTimer(self, self.Delay2RetryConSvrSession, 5, false)
        end
    end
end
