local LSKPlayerClass = DeclareClass("LSKPlayerClass")

function LSKPlayerClass:ctor()
    -- 用户ID
    self.UID = false
    -- 昵称
    self.NickName = false
    -- 头像URL
    self.HeadUrl = false
    -- 目前投币数量
    self.DropCoin = 0
    -- 礼品数
    self.PrizeCount = 0
    self.Credits = 0
end

function LSKPlayerClass:InitData(data)
    self.NickName = data.nick or ''
    self.HeadUrl = data.pic_url or ''
    self.UID = data.uid or 0
    self.DropCoin = 0
    self.PrizeCount = data.prize_num or 0
end
