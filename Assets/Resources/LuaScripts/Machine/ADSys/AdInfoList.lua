--[[********************************************************************
*		作者： yangfan
*		时间： 2018-11-22
*		描述： 广告列表
*********************************************************************--]]
local AdInfoListClass = DeclareClass("AdInfoListClass")

function AdInfoListClass:ctor()
    
    --唯一标识符
    self.Id = 0
    --广告信息列表
    self.InfoList = {}

end

function AdInfoListClass:InitList(id,args)

    self.Id = id
    for k,v in ipairs(args) do
        if not string.IsNullOrEmpty(v.adUrl) then
            local info = ClassLib.AdInfoClass.new()
            info:InitInfo(v)
            self.InfoList[#self.InfoList+1] = info
        end
    end

end

function AdInfoListClass:GetAdCount()

    return #self.InfoList

end

function AdInfoListClass:GetAdInfo(index)
    return self.InfoList[index]
end