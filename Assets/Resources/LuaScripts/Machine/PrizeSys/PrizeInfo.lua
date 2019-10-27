--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-22
*		描述： 礼品信息
*********************************************************************--]]
--礼品图标 类型
local PrizeIconType={
    Small=1,
    Middle=2,
    Big=3,
}

local PrizeInfoClass = DeclareClass("PrizeInfoClass")
function PrizeInfoClass:ctor()

    self.Id=0
    --礼品名称
    self.Name=""
    --描述
    self.Desc=""
    --官方价格
    self.OfficialValue=0
    --显示价格
    self.ShowPrice=''
    --Icon资源地址
    self.IconUrls={
        [PrizeIconType.Big]="",
        [PrizeIconType.Middle]="",
        [PrizeIconType.Small]="",
    }
    --类型
    self.Type = ""
    --积分
    self.Credits = 0
    --积分显示的字符串
    self.ShowCredits = ''
end

--[[
    @desc: 
    --@data:
    "100000": {
        "prizeName": "Dior 740#",
        "remark": "Dior 740# 枫叶红",
        "offValue": 28800,
        "integral": 28800,
        "resUrl": {
        "big": "http://gamehalldownloadcdn.scbczx.com/prizes/100000_big_1544253173545.png",
        "middle": "http://gamehalldownloadcdn.scbczx.com/prizes/100000_middle_1544253173545.png",
        "small": "http://gamehalldownloadcdn.scbczx.com/prizes/100000_small_1544254729872.png"
        },
        "type": "代抓"
    }
    @return:
]]
function PrizeInfoClass:InitInfo(id,data)
    self.Id = id
    self.Name = data.prizeName
    self.Desc = data.remark
    self.Type = data.type
    self.OfficialValue = data.offValue or 0
    -- 分转为元
    self.ShowPrice = string.format('%0.2f', self.OfficialValue/100)
    self.ShowPrice = string.gsub(self.ShowPrice, '.00','')
    self.Credits = data.integral and data.integral or 0
    if self.Credits >= 100000 then
        self.ShowCredits = string.format('%0.2f万', self.OfficialValue/10000)--以万为单位
        self.ShowCredits = string.gsub(self.ShowCredits, '.00','')
    else
        self.ShowCredits = tostring(self.Credits)
    end
    

    local resUrl = data.resUrl
    if not resUrl then
        return
    end

    self.IconUrls[PrizeIconType.Big]=resUrl.big
    self.IconUrls[PrizeIconType.Middle]=resUrl.middle
    self.IconUrls[PrizeIconType.Small]=resUrl.small
end

--Icon图片地址
function PrizeInfoClass:GetIconUrl(iconType)
    
    if not iconType then
        return self.IconUrls[PrizeIconType.Small]
    end
    return self.IconUrls[iconType]
end