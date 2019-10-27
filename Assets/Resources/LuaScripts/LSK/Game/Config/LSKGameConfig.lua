local LSKGameConfigClass = DeclareClass("LSKGameConfigClass")


local Wallpaper_LSK_Map = 
{
    [1] = {Wheel='Wheel_001',Wallpapers={'Wall_001','Wall_003'},},
    [2] = {Wheel='Wheel_002',Wallpapers={'Wall_001','Wall_003'},},
    [3] = {Wheel='Wheel_003',Wallpapers={'Wall_002','Wall_003'},},
    [4] = {Wheel='Wheel_004',Wallpapers={'Wall_002','Wall_003'},},
    [5] = {Wheel='Wheel_005',Wallpapers={'Wall_004'},},
    [6] = {Wheel='Wheel_006',Wallpapers={'Wall_004'},},
}

local Bullet_Map = 
{
	[1] = {Bullet = 'LSK_001', Icon = 'Bullet_001'},
	[2] = {Bullet = 'LSK_002', Icon = 'Bullet_002'},
    [3] = {Bullet = 'LSK_003', Icon = 'Bullet_003'},
	[4] = {Bullet = 'LSK_004', Icon = 'Bullet_004'},
}

function LSKGameConfigClass:ctor()
    self.LevelConfig = false
    self.MaxLevel = 3
    -- 总的分片数
    self.TotalSlice = 36

    self.KnifeWidth = 0.8
    self.LSKSpeed = 60
    self.EasyLevelConfig = false
end

function LSKGameConfigClass:Initialize()
    local config = require('LSK.Game.Config.LSKLevel')
    self.LevelConfig = config.Normal
    self.EasyLevelConfig = config.Easy

    self.MaxLevel = #self.LevelConfig
end

function LSKGameConfigClass:Uninitialize()

end

function LSKGameConfigClass:GetGameStyle()
    local index = LSKUtil:GetRandom(1,#Wallpaper_LSK_Map)
    local config = Wallpaper_LSK_Map[index]
    local wallIdx = LSKUtil:GetRandom(1,#config.Wallpapers)
    local wallpaperName = config.Wallpapers[wallIdx]
    local bulletId = (LSKData.KnifeList and #LSKData.KnifeList > 0 ) and LSKData.KnifeList[LSKUtil:GetRandom(1, #LSKData.KnifeList)] or 1
    local bulletCfg = Bullet_Map[bulletId]

    return config.Wheel, wallpaperName, bulletCfg.Bullet, bulletCfg.Icon
end
-------------------------------------
-- 获取对应关卡配置.
-- @param level 关卡id.
-------------------------------------
function LSKGameConfigClass:GetLevelConfig(level, isEasy)
    if isEasy then
        return self.EasyLevelConfig[level]
    end
    return self.LevelConfig[level]
end

function LSKGameConfigClass:GetMaxLSK()
    local max = 0
    for k,v in ipairs(self.LevelConfig) do
        if v.shootcount>max then
            max = v.shootcount
        end
    end
    return max
end