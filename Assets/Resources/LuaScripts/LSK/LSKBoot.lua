--[[********************************************************************
*		作者： XH
*		时间： 2018-11-09
*		描述： 口红机启动类
*********************************************************************--]]
local LSKBootClass = DeclareClass("LSKBootClass")

--全局
LSKBoot = false
LSKNetwork = false
LSKData = false
LSKGame = false
LSKAudio = false
LSKUICommon = false

function LSKBootClass:ctor()
    self._curLogicState = false
    self._registedLogicStates = {}
    self._lskMonitor = false
    self.PlayerHud = false
end

--[[
    @desc: 加载所有定义脚本
]]
function LSKBootClass:Load()
    require('LSK.Game.Config.LSKLevel')
    require('LSK.LSKPreloader')
    require("LSK.Utils.LSKUtil")
    --net
    require("LSK.Network.LSKProtocol")
    require("LSK.Network.LSKNetPacker")
    require("LSK.Network.LSKNetwork")
    require("LSK.LSKMonitor")
    --data
    require("LSK.Data.LipstickInfo")
    require("LSK.Data.LSKPlayer")
    require("LSK.Data.LSKData")
    -- audio
    require("LSK.Audio.LSKAudioMgr")

    --uicommon
    require("LSK.UICommon.LSKUIWaiting")
    require("LSK.UICommon.LSKUIBubble")
    require("LSK.UICommon.LSKUICommon")

    --login
    require("LSK.Login.LSKLoginState")
    require("LSK.Login.LSKLoginForm")
    --select
    require("LSK.Select.LSKSelectState")
    require("LSK.Select.LSKSelectItem")
    require("LSK.Select.LSKSelectForm")
    require("LSK.Select.LSKScanStartForm")
    require("LSK.Select.LSKNoEnoughCoinForm")
    require("LSK.Select.LSKQuitConfirmForm")
    --play
    require("LSK.Play.LSKPlayState")
    require("LSK.Play.LSKSubReadyState")
    require("LSK.Play.LSKSubWinState")
    require("LSK.Play.LSKSubLoseState")
    require("LSK.Play.LSKSubLotteryState")
    require("LSK.Play.LSKSubInGameState")
    require("LSK.Play.LSKWinForm")
    require("LSK.Play.LSKLoseForm")
    require("LSK.Play.LSKReadyStartForm")
    require("LSK.Play.LSKSubSecurityBuyState")
    require("LSK.Play.LSKSecurityBuyForm")
    require('LSK.Play.LSKLoseAnimForm')
    require('LSK.Play.LSKSpinWinForm')
    --Game

    require("LSK.Game.Config.LSKLevel")
    require("LSK.Game.Config.LSKGameConfig")
    require("LSK.Game.Entity.LSKWheel")
    require("LSK.Game.Entity.LSKLipstick")

    require("LSK.Game.LSKGameHud")
    require("LSK.Game.LSKGame")
    require("LSK.Game.LSKEnv")
    require("LSK.Game.LSKGun")
    require("LSK.Game.LSKTrickSystem")
    require("LSK.Game.Effect.LSKEffect")
    require("LSK.Game.Effect.LSKEffectMgr")
    require('LSK.Game.LSKKnifeData')
    --Presentation
    require("LSK.Presentation.LSKPresentationState")
    --Ad
    require("LSK.Ad.LSKAdState")
    require("LSK.UICommon.LSKPlayerHud")
    --Trial 
    require('LSK.Trial.LSKTrialState')
end

--开始
function LSKBootClass:Start(gameInfo, uid)
    LSKBoot = self

    LSKUtil = ClassLib.LSKUtilClass.new()
    LSKAudio = ClassLib.LSKAudioMgrClass.new()
    LSKAudio:Init()
    LSKAudio:SetSoundVolume(0.7)
    LSKAudio:SetMusicVolume(0.7)
    --
    LSKNetwork = ClassLib.LSKNetworkClass.new()
    LSKNetwork:Initialize()
    LSKNetwork:RefreshNetInfo(gameInfo.GameHost, gameInfo.GamePort, uid)

    self._lskMonitor = ClassLib.LSKMonitorClass.new()
    self._lskMonitor:Initialize()

    LSKData = ClassLib.LSKDataClass.new()
    LSKData:Initialize()

    LSKGame = ClassLib.LSKGameClass.new()
    LSKGame:Init()
    --
    LSKUICommon = ClassLib.LSKUICommonClass.new()
    LSKUICommon:Initialize()
    --
    local login = ClassLib.LSKLoginStateClass.new()
    local select = ClassLib.LSKSelectStateClass.new()
    local play = ClassLib.LSKPlayStateClass.new()
    local ad = ClassLib.LSKAdStateClass.new()
    local presentation = ClassLib.LSKPresentationStateClass.new()
    local trial = ClassLib.LSKTrialStateClass.new()
    --
    self:RegisterState(login:vGetName(), login)
    self:RegisterState(select:vGetName(), select)
    self:RegisterState(play:vGetName(), play)
    self:RegisterState(ad:vGetName(), ad)
    self:RegisterState(presentation:vGetName(), presentation)
    self:RegisterState(trial:vGetName(), trial)
    --登录
    self:ChangeState("LSK_Login_LogicState")

    self.PlayerHud = ClassLib.LSKPlayerHudClass.new()
    self.PlayerHud:Create()
    self.PlayerHud:Hide()
end

--结束
function LSKBootClass:Over()
    if self._curLogicState then
        self._curLogicState:vOnStateLeave()
        self._curLogicState = false
    end
    if self._registedLogicStates then
        for k, v in pairs(self._registedLogicStates) do
            v:vUninitializeState()
            self._registedLogicStates[k] = nil
        end
        self._registedLogicStates = false
    end
    if self._lskMonitor then
        self._lskMonitor:Uninitialize()
        self._lskMonitor = false
    end
    if self.PlayerHud then
        self.PlayerHud:Destroy()
        self.PlayerHud = false
    end
    if LSKNetwork then
        LSKNetwork:UnInitialize()
        LSKNetwork = nil
    end

    if LSKUICommon then
        LSKUICommon:UnInitialize()
        LSKUICommon = nil
    end
    if LSKUtil then
        LSKUtil:Clear()
        LSKUtil = nil
    end
    if LSKData then
        LSKData:Uninitialize()
        LSKBoot = nil
    end
    if LSKAudio then
        LSKAudio:Uninit()
        LSKAudio = nil
    end
    if LSKGame then
        LSKGame:Uninit()
        LSKGame = nil
    end
end

--注册逻辑状态
function LSKBootClass:RegisterState(name, state)
    if self._registedLogicStates[name] then
        LogE("Already Registered Logic State " .. name)
        return
    end
    if state then
        state:vInitializeState()
    end
    self._registedLogicStates[name] = state
end

--desc: 切换逻辑状态
function LSKBootClass:ChangeState(name, param)
    if not self._registedLogicStates[name] then
        LogE("No Registered State " .. name)
    end
    local old = ""
    if self._curLogicState then
        old = self._curLogicState:vGetName()
        if self._curLogicState:vGetName() == name then
            if self._curLogicState.EnterAgain then
                self._curLogicState:EnterAgain(param)
            end
            LogD("LSK --------> Aleary In State = "..name)
            return
        else
            self._curLogicState:vOnStateLeave(param)
        end
    end
    --
    self._curLogicState = self._registedLogicStates[name]
    self._curLogicState:vOnStateEnter(param, old)
    if self.PlayerHud then
        self.PlayerHud:Refresh()
    end
    return self._curLogicState
end

--转发
function LSKBootClass:OnRelayNetMsg(data, result, cmd)
    if cmd == LSKNet.Cmd.cs_notify_player_logout or cmd == LSKNet.Cmd.cs_rsp_player_logout then
        LSKData:LogoutPlayer()
        if self.PlayerHud then
            self.PlayerHud:Refresh()
        end
        if LSKGame:InPlay() then
            LSKAudio:StopSound()
            LSKGame:DealOpCmd(LSKGameOpCmd.StopRotate)
            LSKGame:DealOpCmd(LSKGameOpCmd.Over,{stop=true})
        end
        self:ChangeState("LSK_Select_LogicState", nil)
    elseif cmd == LSKNet.Cmd.cs_notify_point then
        if LSKData:IsLogin() then
            LSKData.User.Credits = data.point or 0
        end
        if self.PlayerHud then
            self.PlayerHud:Refresh()
        end
    elseif cmd == LSKNet.Cmd.cs_notify_m_player_login then
        LSKData:LoginPlayer(data)
        if self.PlayerHud then
            self.PlayerHud:Refresh()
        end
        local postData=RapidJson.Encode({uid=LSKData.User.UID})
        --先获取基础信息
        HttpService:AsyncGetTextByPostUrl(MachineUrlRoot.."/getUserInfo",postData,function (ret)
            if ret=="error" then
                LogE('Get Player Credits Failed !')
            else
                local rcvData = RapidJson.Decode(ret)
                if rcvData.errCode == 1 then
                    LSKData:SetCredits(rcvData.userInfo.integral)
                    self.PlayerHud:Refresh()
                else
                    LogE('Error ! code = '..tostring(rcvData.errCode))
                end
            end
        end)
        
		AudioService:Play(AudioChannelType.ACT_UI, 'LSK/Audio/Audio_LoginSuccess', false)
        self:ChangeState("LSK_Select_LogicState", {loginNow=true})
    elseif cmd == LSKNet.Cmd.cs_notify_m_player_buy_in then
        if not LSKData:IsLogin() then
            return
        end
 
        local add = data.cur_buy_in - LSKData.User.DropCoin
        if add > 0 and self.PlayerHud then
            self.PlayerHud:PlayDropCoin(add)
        end
        LSKData:SetDropCoin(data.cur_buy_in)
        self.PlayerHud:Refresh()
        AudioService:Play(AudioChannelType.ACT_UI, 'LSK/Audio/Audio_AddCoin', false)
    elseif cmd == LSKNet.Cmd.cs_rsp_buy_prize then
        if not LSKData:IsLogin() then
            return
        end
        if not data.left_buy_in then
            return
        end
        
        LSKData:SetDropCoin(data.left_buy_in)

        if self.PlayerHud then
            self.PlayerHud:Refresh()
        end
    end
    if self._curLogicState then
        self._curLogicState:OnRecvRelayMsg(data, result, cmd)
    end
end

function LSKBootClass:GetCurStateName()
    if self._curLogicState then
        return self._curLogicState:vGetName()
    end
end

function LSKBootClass:CanShowAwardQRCode()
    if not LSKData:IsLogin() or (LSKData.User.PrizeCount <= 0) then
        return false
    end
    if self._curLogicState:vGetName() == 'LSK_Trial_State' then
        return false
    end
    
    if self._curLogicState:vGetName() == 'LSK_Play_LogicState' then
        return self._curLogicState:CanShowAwardQRCode()
    else
        return true
    end
end

function LSKBootClass:IsCanPlayAds()
    if not self._curLogicState or ADSystem:IsAdDataEmpty() then
        return false
    end

    local state = self._curLogicState:vGetName()

    return (state == "LSK_Select_LogicState" and not LSKData:IsLogin()) or
           state == "LSK_Presentation_State"
end

function LSKBootClass:IsCanKill()
    return not LSKData or not LSKData:IsLogin()
end