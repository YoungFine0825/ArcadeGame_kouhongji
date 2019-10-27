--[[********************************************************************
*		作者： XH
*		时间： 2018-12-11
*		描述： 挑战成功
*********************************************************************--]]

local LSKSubWinStateClass = DeclareClass("LSKSubWinStateClass")

local MAX_Wait = 15
local Min_Wait = 5
function LSKSubWinStateClass:ctor()
    self._winForm = false
    self._playFsm = false
    self._leftSeconds = false
end

function LSKSubWinStateClass:vInitializeState(fsm)
    self._playFsm = fsm
end

function LSKSubWinStateClass:vUninitializeState()
    self._playFsm = false

end

function LSKSubWinStateClass:vGetName()
    return "LSK_Play_Sub_WinState"
end


function LSKSubWinStateClass:vOnStateEnter(param,oldSt)

    if not self._winForm then
        self._winForm = ClassLib.LSKWinFormClass.new()
        self._winForm:Create(nil, {leftSeconds = MAX_Wait, prizeId=param.prizeId}, 0)
    end
   
    self._leftSeconds = MAX_Wait
    if param and param.timeout then
        self._leftSeconds = param.timeout
    end
    LSKAudio:PlaySound(LSKAudioID.Win)
    LSKGame:DealOpCmd(LSKGameOpCmd.Over)

    ScheduleService:AddTimer(self,self.OnTimerUpdate,1,true)
end

function LSKSubWinStateClass:OnTimerUpdate()
    if not self._leftSeconds then
        return
    end

    self._leftSeconds = self._leftSeconds - 1
    if self._winForm then
        self._winForm:vOnUpdateUI('Countdown', self._leftSeconds)
    end
    if self._leftSeconds <= 0 then
        LSKBoot:ChangeState('LSK_Select_LogicState')
        self._leftSeconds = false
    end
    
end

function LSKSubWinStateClass:vOnStateLeave(param)
    ScheduleService:RemoveTimer(self,self.OnTimerUpdate)
    self._leftSeconds = false

    if self._winForm then
        self._winForm:Close()
        self._winForm = false
    end
end
function LSKSubWinStateClass:OnRecvRelayMsg(data, result, cmd)

end