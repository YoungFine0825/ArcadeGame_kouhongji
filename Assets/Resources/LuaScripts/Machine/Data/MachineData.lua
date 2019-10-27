--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-01
*		描述： 机台相关数据
*********************************************************************--]]
local MachineDataClass = DeclareClass("MachineDataClass")

function MachineDataClass:ctor()

    --机器码
    self.MachineId = false
    --机器对应的连接Uid
    self.Uid=false
    --机器连接令牌
    self.Token=false

end

function MachineDataClass:Initialize()
    
    self.MachineId = PlayerPrefs:GetString("MachineID")
    self.Uid = ""
    self.Token = ""

end

function MachineDataClass:UnInitialize()
    

end

MachineData = MachineDataClass.new()
