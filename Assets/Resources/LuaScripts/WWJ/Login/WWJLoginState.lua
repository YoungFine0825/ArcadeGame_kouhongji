--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-05
*		描述： 娃娃机游戏登录与加载 状态
*********************************************************************--]]

--断线重连处理
local ReConnectSession={
    IsWorking=false,
    _retryCnt = 0,
    _maxRetryCnt = 10
}

local WWJLoginStateClass = DeclareClass("WWJLoginStateClass",ClassLib.LogicStateBaseClass)

function WWJLoginStateClass:ctor()
   
   self._loginForm = false
   self._rebootDelay = 8
   self._rebootCntDown = 0

   self._tryConSvrCnt = 0
   self._maxTryConSvrCnt = 10
   self._reloginCnt = 0
   self._maxReloginCnt = 10
end

function WWJLoginStateClass:vInitializeState()
    
    

end

function WWJLoginStateClass:vUninitializeState()
    ScheduleService:RemoveTimer(self)
    ReConnectSession.IsWorking=false
    ScheduleService:RemoveTimer(ReConnectSession)

end

function WWJLoginStateClass:vGetName()

    return "WWJ_Login_LogicState"
end


function WWJLoginStateClass:vOnStateEnter(param,oldSt)
    LogD("Enter WWJ Login State!");
    if not self._loginForm then
        self._loginForm = ClassLib.WWJLoginFormClass.new()
        self._loginForm:Create(nil,nil,0)
    end
    self:DoConnectWWJServer()
end


function WWJLoginStateClass:vOnStateLeave(param)
    if self._loginForm then
        self._loginForm:UpdateUI("UpdateProgress", -1)
        self._loginForm:Destroy()
        self._loginForm = false
    end
end

--连接娃娃机服务器
function WWJLoginStateClass:DoConnectWWJServer()
    self._loginForm:UpdateUI("UpdateTip","连接娃娃机服务器！")
    --
    WWJNetwork.ConnectDelegate = self
    WWJNetwork:OpenConnect()
end

function WWJLoginStateClass:OnConnectToSvrBack(isSucceed)
    if isSucceed then
        --连接服务器成功
        self._tryConSvrCnt = 0
        self._loginForm:UpdateUI("UpdateTip","连接娃娃机服务器成功，开始登录！")
        --self:DoLoginWWJServer()
        ScheduleService:AddTimer(self, self.DoLoginWWJServer, 2, false)
    else
        --连接服务器失败
        self._loginForm:UpdateUI("UpdateTip", "连接娃娃机服务器失败！等待重试！")
        ScheduleService:RemoveTimer(self)
        ScheduleService:AddTimer(self, self.ReConnectWWJServer, 5, false)
    end
end

function WWJLoginStateClass:OnNetworkClosed()
    --连接服务器失败
    self._loginForm:UpdateUI("UpdateTip", "连接娃娃机服务器失败！等待重试！")
    ScheduleService:RemoveTimer(self)
    ScheduleService:AddTimer(self, self.ReConnectWWJServer, 5, false)
end

--重新连接娃娃机服务器
function WWJLoginStateClass:ReConnectWWJServer()
    if self._tryConSvrCnt >= self._maxTryConSvrCnt then
        --始终连接服务器失败，重启设备
        WWJNetwork:CloseConnnect(true)
        self._loginForm:UpdateUI("UpdateTip", "连接娃娃机服务器失败,即将重启！")
        self._rebootCntDown = self._rebootDelay
        ScheduleService:RemoveTimer(self)
        ScheduleService:AddTimer(self, self.OnRebootTimer, 1, true)
        self._tryConSvrCnt = 0
        return
    end
    --重新连接
    self._tryConSvrCnt = self._tryConSvrCnt + 1
    self._loginForm:UpdateUI("UpdateTip", string.format("重试连接口红机服务器%d次", self._tryConSvrCnt))
    WWJNetwork.ConnectDelegate = self
    WWJNetwork:OpenConnect()
end

--登陆娃娃机服务器
function WWJLoginStateClass:DoLoginWWJServer()
    WWJBoot:ChangeState("WWJ_Wait_LogicState")
end

--登陆娃娃机服务器回调
function WWJLoginStateClass:OnLoginWWJServerRsp(args,result)
    if ReConnectSession.IsWorking then
        ReConnectSession:OnLoginWWJSvrRsp(args,result)
    else
        if result ~= MachineNet.Result.EN_Result_OK then
            --
            self._loginForm:UpdateUI("UpdateTip", "登录娃娃机服务器失败,等待重试！")
            WWJNetwork.ConnectDelegate = false
            WWJNetwork:CloseConnnect()
            ScheduleService:RemoveTimer(self)
            ScheduleService:AddTimer(self, self.ReLoginWWJServer, 10, false)
        else
            if args then
                --托管断线重连
                WWJNetwork.ConnectDelegate=ReConnectSession
                ScheduleService:RemoveTimer(self)
                self._reloginCnt=0
                --开始发送心跳包
                if args.use_heart_beat then
                    WWJNetwork:StartHearbeat(args.heart_beat_interval)
                end
                --初始化游戏信息
                if args.wwj_info then
                    --
                else
                    LogE("WWJ Login  Error")
                end
                WWJBoot:ChangeState("WWJ_Wait_LogicState")
            else
                self._loginForm:UpdateUI("UpdateTip", "登录娃娃机服务器失败,等待重试！")
                WWJNetwork.ConnectDelegate = false
                WWJNetwork:CloseConnnect()
                ScheduleService:RemoveTimer(self)
                ScheduleService:AddTimer(self, self.ReLoginWWJServer, 10, false)
            end
        end
    end
end

--重新登陆娃娃机服务器
function WWJLoginStateClass:ReLoginWWJServer()

end

function WWJLoginStateClass:OnRebootTimer()
   
    if (self._rebootCntDown < 0) then
        ScheduleService:RemoveTimer(self)
        NativeService:RebootDevice()
    else
        if (self._rebootCntDown <= 5) then
            self._loginForm:UpdateUI("UpdateTip", "重启倒计时：" .. self._rebootCntDown)
        end
        self._rebootCntDown = self._rebootCntDown - 1
    end

end

-----------------------------断线重连处理------------------------
function ReConnectSession:BeginSession()

    LogD("WWJ 开始断线重连")
    WWJUICommon:CleanWaiting()
    WWJUICommon:ShowWaiting(true,"断线重连...")
    self.IsWorking=true
    WWJNetwork.ConnectDelegate = self
    WWJNetwork:OpenConnect()
end

function ReConnectSession:OnNetworkClosed()
    LogD("----------------------WWJ网络断开------------------")
    self:BeginSession()
end

function ReConnectSession:OnConnectToSvrBack(isSucceed)

    if isSucceed then
        LogD("WWJ重新连接成功")
        --重新发登录
        local args = {
            token = MachineData.Token
        }
        --WWJNetwork:SendMsg(LSKNet.Cmd.cs_req_m_login_game, args)
    else
        --
        LogD("WWJ重新连接失败")
        WWJNetwork.ConnectDelegate = false
        WWJNetwork:CloseConnnect()
        ScheduleService:RemoveTimer(self)
        ScheduleService:AddTimer(self,self.Delay2RetryConSvr,5,false)
    end

end

function ReConnectSession:Delay2RetryConSvr()
    
    LogD("WWJ重新建立连接")
    self._retryCnt = self._retryCnt + 1
    if self._retryCnt >= self._maxRetryCnt then
        
        ScheduleService:RemoveTimer(self)
        self._retryCnt = 0
        WWJUICommon:CleanWaiting()
        GameStateService:ChangeState("LoginGameState")
        return
    end

    LogD("----------->%d<----------",self._retryCnt)
    self.IsWorking=true
    WWJNetwork.ConnectDelegate = self
    WWJNetwork:OpenConnect()
end

function ReConnectSession:OnLoginWWJSvrRsp(rsp,result)
    
    if result ~= MachineNet.Result.EN_Result_OK then
        LogD("WWJ 重新登录失败")
        WWJNetwork.ConnectDelegate = false
        WWJNetwork:CloseConnnect()
        ScheduleService:RemoveTimer(self)
        ScheduleService:AddTimer(self,self.Delay2RetryConSvr,5,false)
    else
        --
        if rsp then
            self.IsWorking=false
            WWJNetwork.ConnectDelegate=self
            ScheduleService:RemoveTimer(self)
            LogD("WWJ 重新登录成功!")
            WWJUICommon:CleanWaiting()
            --开始发送心跳包
            if rsp.use_heart_beat then
                NetNetwork:StartHearbeat(rsp.heart_beat_interval)
            end
            -- 
            if WWJBoot:GetCurStateName() == 'WWJ_Play_LogicState' then
                WWJBoot:ChangeState('WWJ_Wait_LogicState')
                WWJData.User = false
            end
            --
        else
            LogD("WWJ 重新登录失败")
            WWJNetwork.ConnectDelegate=false
            WWJNetwork:CloseConnnect()
            ScheduleService:RemoveTimer(self)
            ScheduleService:AddTimer(self,self.Delay2RetryConSvr,5,false)
        end
    end
end
