--[[********************************************************************
*		作者： XH
*		时间： 2018-12-11
*		描述： 资源预加载器
*********************************************************************--]]
local LSKPreloaderClass = DeclareClass("LSKPreloaderClass")

local PreloadConfig = 
{
    'LSK/Game/LSKMain',
    'LSK/Lipsticks/Hit_LSK',
    'LSK/Lipsticks/LSK_001',
    'LSK/Lipsticks/LSK_002',
    'LSK/Lipsticks/LSK_003',
    'LSK/Wheels/Wheel_001',
    'LSK/Wheels/Wheel_001_boom',
    'LSK/Wheels/Wheel_001_hit',
    'LSK/Wheels/Wheel_002',
    'LSK/Wheels/Wheel_002_boom',
    'LSK/Wheels/Wheel_002_hit',
    'LSK/Wheels/Wheel_003',
    'LSK/Wheels/Wheel_003_boom',
    'LSK/Wheels/Wheel_003_hit',
    'LSK/Wheels/Wheel_004',
    'LSK/Wheels/Wheel_004_boom',
    'LSK/Wheels/Wheel_004_hit',
    'LSK/Wheels/Wheel_005',
    'LSK/Wheels/Wheel_005_boom',
    'LSK/Wheels/Wheel_005_hit',
    'LSK/Wheels/Wheel_006',
    'LSK/Wheels/Wheel_006_boom',
    'LSK/Wheels/Wheel_006_hit',
}

function LSKPreloaderClass:ctor()
    self._index = 0
    self._interval = 0.1
    self._piceCnt = 4

    self._delegate = false
    self._assets = false
end

function LSKPreloaderClass:Load()
    self._assets = {}
    ScheduleService:AddTimer(self,self.OnTimerUpdate,self._index,true)
end

function LSKPreloaderClass:OnTimerUpdate()
    local finish = false
    for i = 1, self._piceCnt do
        self._index = self._index + 1
        local path = PreloadConfig[self._index]
        if path then
            -- load
            self._assets[#self._assets+1] = AssetService:LoadPrimitiveAsset(path, LifeType.Manual)
        end
        if self._index == #PreloadConfig then
            finish = true
            ScheduleService:RemoveTimer(self,self.OnTimerUpdate)
            break
        end
    end

    if self._delegate then
        local progress = finish and 1 or self._index/#PreloadConfig
        self._delegate:OnPreloadProgress(progress)
    end
end


function LSKPreloaderClass:Unload()
    ScheduleService:RemoveTimer(self)
    if self._assets then
        for k,v in ipairs(self._assets) do
            AssetService:Unload(v)
        end
    end

    self._delegate = false
    self._assets = false
end