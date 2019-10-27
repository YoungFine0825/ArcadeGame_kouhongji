--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-05
*		描述： 娃娃机启动类
*********************************************************************--]]
--全局
WWJBoot=false
WWJData=false
WWJGame=false
WWJNetwork=false
WWJUICommon=false
--

local WWJBootClass = DeclareClass("WWJBootClass")
--
function WWJBootClass:ctor()
    
    self._curLogicState=false
    self._registedLogicStates={}
    
end
--
function WWJBootClass:Load()

    LogD("--------------------WWJBoot StartGame------------------")
    --网络
    require("WWJ.Network.WWJNetDeclare")
    require("WWJ.Network.WWJNetRoute")
    require("WWJ.Network.WWJNetwork")
    --数据
    require("WWJ.Data.WWJPlayerInfo")
    require("WWJ.Data.WWJPrizeInfo")
    require("WWJ.Data.WWJData")
    --游戏逻辑
    require("WWJ.Game.WWJCamera")
    require("WWJ.Game.WWJDoll")
    require("WWJ.Game.Claw.WWJClaw")
    require("WWJ.Game.Claw.WWJClawIdle")
    require("WWJ.Game.Claw.WWJClawMove")
    require("WWJ.Game.Claw.WWJClawDown")
    require("WWJ.Game.Claw.WWJClawBack")
    require("WWJ.Game.Claw.WWJClawOpen")
    require("WWJ.Game.Claw.WWJClawRise")
    require("WWJ.Game.Claw.WWJClawClose")
    require("WWJ.Game.WWJEnv")
    require("WWJ.Game.WWJMachine")
    require("WWJ.Game.WWJGame")
    --登陆
    require("WWJ.Login.WWJLoginForm")
    --UICommon
    require("WWJ.UICommon.WWJUIBubble")
    require("WWJ.UICommon.WWJUIWaiting")
    require("WWJ.UICommon.WWJUICommon")
    --等待
    require("WWJ.Wait.WWJWaitForm")
    --选择
    require("WWJ.Select.WWJSelectForm")
    require("WWJ.Select.WWJPlayerInfoForm")
    require("WWJ.Select.WWJExitForm")
    --准备
    require("WWJ.Ready.WWJReadyForm")
    require("WWJ.Ready.WWJReadyState")
    --游戏中
    require("WWJ.Play.WWJPlayingForm")
    --游戏结束
    require("WWJ.GameOVer.WWJContinueForm")
    require("WWJ.GameOver.WWJSuccessForm")
    require("WWJ.GameOver.WWJFailedForm")
    require("WWJ.GameOver.WWJGameOverState")
    --逻辑状态
    require("WWJ.Login.WWJLoginState")
    require("WWJ.Play.WWJPlayState")
    require("WWJ.Wait.WWJWaitState")
    require("WWJ.Select.WWJSelectState")
    require("WWJ.Ad.WWJAdState")

end

function WWJBootClass:Start(  )
    
     WWJBoot=self
     --
     WWJData=ClassLib.WWJDataClass.new()
     WWJData:Initialize();
     WWJNetwork=ClassLib.WWJNetworkClass.new()
     WWJNetwork:Initialize();
     WWJUICommon = ClassLib.WWJUICommonClass.new()
     WWJUICommon:Initialize()
     --
     WWJGame=ClassLib.WWJGameClass.new()
     WWJGame:Initialize()

     -- --注册状态
     local loginState = ClassLib.WWJLoginStateClass.new();
     self:RegisterState(loginState:vGetName(),loginState);
     --
     local waitState = ClassLib.WWJWaitStateClass.new()
     self:RegisterState(waitState:vGetName(),waitState)
     --
     local play = ClassLib.WWJPlayStateClass.new()
     self:RegisterState(play.vGetName(),play)
     --
     local selectState = ClassLib.WWJSelectStateClass.new()
     self:RegisterState(selectState:vGetName(),selectState)
     --
     local adState = ClassLib.WWJAdStateClass.new()
     self:RegisterState(adState:vGetName(),adState)
     --
     local readyState = ClassLib.WWJReadyStateClass.new()
     self:RegisterState(readyState:vGetName(),readyState)
     --
     local gameOverState = ClassLib.WWJGameOverStateClass.new()
     self:RegisterState(gameOverState:vGetName(),gameOverState)
    
     --驱动
     ScheduleService:AddUpdater(self,self.LogicUpdate)
    -- --
    self:ChangeState("WWJ_Login_LogicState")


end


function WWJBootClass:Over()
    
    if self._registedLogicStates then
		for k ,v in pairs(self._registedLogicStates) do
       		v:vUninitializeState()
            self._registedLogicStates[k] = nil
    	end
    	self._registedLogicStates=false
	end
    self._curLogicState=false

    WWJGame:UnInitialize()
    WWJGame=false

    WWJNetwork:UnInitialize()
    WWJNetwork=false

    WWJData:UnInitialize()
    WWJData=false

    WWJBoot=false

end

--统一驱动
function WWJBootClass:LogicUpdate(deltaTime)
    
    if WWJGame then
        WWJGame:LogicUpdate(deltaTime)
    end

end

--注册逻辑状态
function WWJBootClass:RegisterState(name,state)

	if self._registedLogicStates[name] then
		LogE("Already Registered Logic State "..name)
		return
	end
    if state then
       state:vInitializeState()
    end
    self._registedLogicStates[name]=state
    
end

--desc: 切换逻辑状态
function WWJBootClass:ChangeState(name,param)

	if not self._registedLogicStates[name] then
		LogE("No Registered State "..name)
	end

	local old=""

	if self._curLogicState then
		
		old=self._curLogicState:vGetName()
		if self._curLogicState:vGetName()==name then
            LogE("------------------Aleary In LogicState----------------")
            return
        else
            self._curLogicState:vOnStateLeave(param)
        end
	end
    ---
	self._curLogicState=self._registedLogicStates[name]
	--
	self._curLogicState:vOnStateEnter(param,old)
    --通知到模块
    return self._curLogicState
    
end

function WWJBootClass:IsCanPlayAds()
    if not self._curLogicState or ADSystem:IsAdDataEmpty() then
        return false
    end
    local state = self._curLogicState:vGetName()

    return state == "WWJ_Wait_LogicState" or 
          (state == "WWJ_Select_LogicState") or-- and not WWJData:IsLogin()) or
           state == "WWJ_Presentation_State"
end

function WWJBootClass:IsCanKill()
    return true
end


