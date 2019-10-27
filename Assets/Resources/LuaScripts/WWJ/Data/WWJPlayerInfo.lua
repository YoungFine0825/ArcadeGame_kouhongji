--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-06
*		描述： 娃娃机当前玩家信息
*********************************************************************--]]
local WWJPlayerInfoClass = DeclareClass("WWJPlayerInfoClass")
--
function WWJPlayerInfoClass:ctor()
    
    self.UserName=""
    self.NickName=""
    self.PhotoUrl=""
    self.PlayCount=0
    self.AwardNum=0
    
end

function WWJPlayerInfoClass:InitData(data)
    self.UserName = data.userName;
    self.NickName = data.nickName;
    self.PhotoUrl = data.photoURL;
    self.PlayCount = data.playCount;
    self.AwardNum = data.awardNum;
end