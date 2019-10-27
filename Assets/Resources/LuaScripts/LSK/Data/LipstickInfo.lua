
local LipstickInfoClass = DeclareClass("LipstickInfoClass")

function LipstickInfoClass:ctor()
    self.PrizeInfo = false
    self.ID = false
end

function LipstickInfoClass:InitData(id)
    self.ID = id
end

function LipstickInfoClass:SetPrize(prize)
    self.PrizeInfo = prize
end
