--[[********************************************************************
*		作者： XH
*		时间： 2018-12-11
*		描述： 准备倒计时 3 2 1 
*********************************************************************--]]

local LSKSubReadyStateClass = DeclareClass("LSKSubReadyStateClass")

function LSKSubReadyStateClass:ctor()
    self._readyForm = false
    self._playFsm = false
    self._cacheArg = false
end

function LSKSubReadyStateClass:vInitializeState(fsm)
    self._playFsm = fsm
end

function LSKSubReadyStateClass:vUninitializeState()
    self._playFsm = false
end

function LSKSubReadyStateClass:vGetName()
    return "LSK_Play_Sub_ReadyState"
end


function LSKSubReadyStateClass:vOnStateEnter(param,oldSt)
    if not param then
        LogE('未传入开始关卡参数!!!')
        return
    end
    LSKGame:DealOpCmd(LSKGameOpCmd.ShowWheel)
    self._cacheArg = param
    local curLevel = param.level
    if not self._readyForm then
        self._readyForm = ClassLib.LSKReadyStartFormClass.new()
        self._readyForm:Create(nil, curLevel, 0)
    end
    ScheduleService:AddTimer(self,self.Delay2Start,3,false)
end

function LSKSubReadyStateClass:Delay2Start()
    if self._playFsm then
        self._playFsm:ChangeState('LSK_Play_Sub_GameState', self._cacheArg)
    end
end

function LSKSubReadyStateClass:vOnStateLeave(param)
    self._cacheArg = false
    ScheduleService:RemoveTimer(self,self.Delay2Start)
    if self._readyForm then
        self._readyForm:Destroy()
        self._readyForm = false
    end
end

function LSKSubReadyStateClass:OnRecvRelayMsg(data, result, cmd)
    
end