--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-12-05
*		描述： 机器监控器
*********************************************************************--]]
local MachineMoniterClass = DeclareClass("MachineMoniterClass")
function MachineMoniterClass:ctor()

end

-- 初始化
-- @return 无
function MachineMoniterClass:Initialize()
    MachineNetwork:AddCommandHandler(MachineNet.Cmd.cs_notify_update_machine,self,self.OnUpdateMachine)
    MachineNetwork:AddCommandHandler(MachineNet.Cmd.cs_notify_delete_machine,self,self.OnDeletedMachine)
    MachineNetwork:AddCommandHandler(MachineNet.Cmd.cs_notify_restart_machine,self,self.OnRestartMachine)
end

-- 反初始化
-- @return 无
function MachineMoniterClass:UnInitialize()
    
    MachineNetwork:RemoveCommandHandlerByObj(self)
    ScheduleService:RemoveTimer(self)

end

--判断是否可以重启 或者 重新登录 
function MachineMoniterClass:JudgeIsCanKill(  )
    
   return GameStateService:GetCurStateIsCanKill()

end


--更新
function MachineMoniterClass:OnUpdateMachine()
    
    LogD("-----------MachineMoniter OnUpdateMachine-----------")
    local isCanKill = self:JudgeIsCanKill()
    if isCanKill then
        ScheduleService:RemoveTimer(self, self.OnUpdateMachine)
        --跳转至机器登陆状态
        LogD("-----------MachineMoniter ReLogin Machine-----------")
        GameStateService:ChangeState("LoginGameState")
    else
        ScheduleService:RemoveTimer(self)
        return ScheduleService:AddTimer(self, self.OnUpdateMachine, 10, false)
    end
end

--删除
function MachineMoniterClass:OnDeletedMachine()
    LogD("-----------MachineMoniter OnDeletedMachine-----------")
    PlayerPrefs:DeleteKey("MachineID")
    PlayerPrefs:Save()
    local isCanKill = self:JudgeIsCanKill()
    if isCanKill then
        ScheduleService:RemoveTimer(self, self.OnDeletedMachine)
        NativeService:RebootDevice()
    else
        ScheduleService:RemoveTimer(self)
        return ScheduleService:AddTimer(self, self.OnDeletedMachine, 10, false)
    end
end

--重启
function MachineMoniterClass:OnRestartMachine()
    local isCanKill = self:JudgeIsCanKill()
    if isCanKill then
        LogD("-----------MachineMoniter RestartMachine-----------")
        ScheduleService:RemoveTimer(self, self.OnRestartMachine)
        NativeService:RebootDevice()
    else
        ScheduleService:RemoveTimer(self)
        return ScheduleService:AddTimer(self, self.OnRestartMachine, 10, false)
    end
end

MachineMoniter=MachineMoniterClass.new()