local LSKDataClass = DeclareClass("LSKDataClass")

local LoginUrl = ''
local ExchangeUrl = ''

if Global.IsDebug then
    ----[[
    --测试
    LoginUrl = 'http://ghtest.scbczx.com/machine_pay'
    ExchangeUrl = 'http://ghtest.scbczx.com/prizeHave'
    --]]

    --[[
    --开发
    ExchangeUrl = 'http://weixin.qq.com/r/6kTs9M7EF5NorYqX9xH4'
    LoginUrl = 'http://devtest.shxyzx.cn/machine_pay'
    --]]
else
    LoginUrl = 'http://ghtest.scbczx.com/machine_pay'
    ExchangeUrl = 'http://ghtest.scbczx.com/prizeHave'
end

LSKAwardType = 
{
    None       = 0,-- 空
    Gold       = 1,-- 金币
    Prize      = 2,-- 礼品
    Diamond    = 3,-- 金币
    Prop       = 4,-- 道具
    Point      = 5,-- 积分
}

function LSKDataClass:ctor()
    self.User = false
    self.CurSelectIdx = 1

    self.LoginQrCode = false
    self.ExchangeQrCode = false
    
    self.Cost = false

    self._lskList = false

    self.LoginTimeout = 120
    self.RoundTimeout = 30
    self.RetryTimeout = 15
    self.RetryCost = false
    self.LotteryList = false
    self._emptyLotteryKeys = false

    self.RoundTimeouts = false
    self.RoundKnives = false
    self.RetryTimeout = false

    -- 是否可以复活
    self.CanRetry = false
    -- 最大复活次数
    self.MaxRetryCount = 0
    -- 复活次数
    self.CurRetryCount = 0
    -- 是否体验
    self.IsCanTrial = false
    -- 可用的飞刀的ID
    self.KnifeList = false
end

function LSKDataClass:SetGameCfgData(data)
    if not data then
        return
    end
    self.CurRetryCount = 0
    self.RetryTimeout = data.retry_timeout
    self.RoundKnives=data.round_knives
    self.RoundTimeouts = data.round_timeouts
    self.RetryTimeout = data.retry_timeout
    self.CanRetry = data.retry_switch
end

function LSKDataClass:IsCanRetry()
    return self.CanRetry and (self.CurRetryCount < self.MaxRetryCount)
end

function LSKDataClass:GetLevelSettings(level)
    if not self.RoundTimeouts or level <= 0 or level > #self.RoundTimeouts then
        return
    end
    return self.RoundTimeouts[level],self.RoundKnives[level]
end

function LSKDataClass:Initialize()
    self.ExchangeQrCode = ExchangeUrl
    self._lskList = {}
    self.IsCanTrial = true
    -- 可用的飞刀的ID
    self.KnifeList = {2,3}
end

function LSKDataClass:Uninitialize()
    self.User = false
    self._lskList = false
    self.LotteryList = false
    self._emptyLotteryKeys = false
    self.RoundTimeouts = false
    self.RoundKnives = false
    self.RetryTimeout = false
    self.CanRetry = false
    self.IsCanTrial = false
    self.KnifeList = false
    
end

function LSKDataClass:LoginPlayer(userInfo)
    self.User = ClassLib.LSKPlayerClass.new()
    self.User:InitData(userInfo)
    self.LoginTimeout = userInfo.timeout
end

function LSKDataClass:IsLogin()
    return self.User and true or false
end

function LSKDataClass:LogoutPlayer(userInfo)
    self.User = false
end

function LSKDataClass:SetDropCoin(coin)
    if self.User then
        self.User.DropCoin = coin
    else
        LogE('逻辑错误')
    end
end

function LSKDataClass:SetPrizeCount(count)
    if self.User then
        self.User.PrizeCount = count or 0
    end
end

function LSKDataClass:SetCredits(credits)
    if self.User then
        self.User.Credits = credits
    end
end
function LSKDataClass:SetLoginData(data,cost,retryCnt)
    for k, id in ipairs(data) do
        local lsk = ClassLib.LipstickInfoClass.new()
        lsk:InitData(id)
        self._lskList[k] = lsk
    end

    self.Cost = cost
    self.RetryCost = self.Cost
    self.MaxRetryCount = retryCnt or 0
    local loginQrCode = string.format('%s?machineId=%s&singleCost=%d', LoginUrl, MachineData.Uid, self.Cost)

    LogD('LoginQrCode='..loginQrCode)
    self.LoginQrCode = loginQrCode
end

function LSKDataClass:InitPrizeData()
    if not self._lskList or #self._lskList == 0 then
        return
    end
    for i,v in ipairs(self._lskList) do
        local prize = PrizeSystem:GetPrizeInfo(tostring(v.ID))
        v:SetPrize(prize)
    end
end

function LSKDataClass:GetPrizeByIdx(idx)
    return self._lskList[idx]
end

function LSKDataClass:GetPrizeById(id)
    for i,v in ipairs(self._lskList) do
        if v.ID == id then
            return v
        end
    end
end

function LSKDataClass:ClearLskList()
    for k,v in pairs(self._lskList) do
        self._lskList[k] = nil
    end
end

function LSKDataClass:GetCurSelectID()
    return self._lskList[self.CurSelectIdx].ID
end

function LSKDataClass:SetLotteryData(data)
    self.LotteryList = {}
    self._emptyLotteryKeys = {}
    for k,v in ipairs(data) do
        if v.awards then
            local rawAward = v.awards[1]
            local award = {}
            award.type = rawAward.type
            award.count = rawAward.giving_num or 0
            award.id = rawAward.id or false
            local key = #self.LotteryList + 1

            self.LotteryList[key] = award
            if award.type == LSKAwardType.None then
                self._emptyLotteryKeys[#self._emptyLotteryKeys+1] = key
            end
        else
            LogE("SetLotteryData Error Empty")
        end
    end
end

function LSKDataClass:GetLotteryIdx(srvAward)
    local type = srvAward.type 
    if type == LSKAwardType.None then
        if #self._emptyLotteryKeys == 0 then
            return nil
        end
        local idx = LSKUtil:GetRandom(1, #self._emptyLotteryKeys)
        return self._emptyLotteryKeys[idx]
    end
    for i, v in ipairs(self.LotteryList) do
        if type == LSKAwardType.Gold then
            if srvAward.giving_num == v.count then
                return i
            end
        elseif type == LSKAwardType.Prize then
            if srvAward.id == v.id and v.type == LSKAwardType.Prize then
                return i
            end
        end
    end
end