local LSKGameClass = DeclareClass("LSKGameClass")

local RoundTime = 30
local KniveCount = 8

LSKGameOpCmd = {
    Start  = 1, -- 开始游戏
    NextLevel = 2, -- 下一关
    Replay = 3, -- 重玩本关
    Throw  = 4, -- 掷飞刀
    Over   = 5, -- 正常结束后调用游戏结束
    Stop   = 6, -- 直接结束游戏
    ShowWheel = 7, --显示圆盘
    ShowScene = 8,
    HideScene = 9,
    DropCoin = 10,
    StopRotate = 11,
}

LSKGameMode =
{
    None = 1,
    Normal = 2,--正常模式
    Trick = 3,--作弊模式
    Simulate = 4,--演示模式
    Trial = 5,--试玩模式
}

local NoThrowNotice = 5
function LSKGameClass:ctor()
    self._env = false
    -- 圆盘组件
    self._wheel = false
    -- 口红发射器
    self._gun = false
    self._hud = false
    self._config = false
    self._wheelLskNode = false
    self._leftSeconds = 0
    self._delegate = false
    self._effect = false
    self._trickSystem = false

    self.CurLevel = false
    self._pinSuccCnt = 0
    self._in_playing = false
    self._timer_interval = 0.1
    self._game_mode = LSKGameMode.None
    self._left_lsk_cnt = 0
    self._throw_notice_timer = 0
    self._prizeInfo = false
    self._knifeData = false
end


function LSKGameClass:SetLevelConfig(config)
    if not config then
        return
    end
    RoundTime = config.roundTime
    KniveCount = config.kniveCount
end


function LSKGameClass:Init()
    -- 加载场景
    self._env = ClassLib.LSKEnvClass.new()
    self._env:Init()
    local prefabLink = self._env:GetPrefabLink()
    if not prefabLink then
        return
    end

    local wheelComponent = prefabLink:GetCacheComponent(1)
    self._wheelLskNode = prefabLink:GetCacheComponent(2)
    local lskGenNode = prefabLink:GetCacheComponent(3)
    local shakeAnim = prefabLink:GetCacheComponent(4)

    local wheelRadius = wheelComponent:GetRadius()
    self._wheel = ClassLib.LSKWheelClass.new()
    self._wheel:Init(wheelComponent,self, shakeAnim, wheelRadius)

    self._config = ClassLib.LSKGameConfigClass.new()
    self._config:Initialize()

    self._knifeData = ClassLib.LSKKnifeDataClass.new()
    self._knifeData:Init(self._config.KnifeWidth, wheelRadius)

    self._gun = ClassLib.LSKGunClass.new()
    self._gun:Init(lskGenNode,wheelRadius,self,self._config.LSKSpeed)

    self._hud = ClassLib.LSKGameHudClass.new()

    
    self._hud:Create(nil,{MaxLevel=self._config.MaxLevel,MaxLipstick=self._config:GetMaxLSK()})


    self._effect = ClassLib.LSKEffectMgrClass.new()
    self._effect:Init()

    self._trickSystem = ClassLib.LSKTrickSystemClass.new()
    self._trickSystem:Init(self._knifeData, self._wheel)
    self.CurLevel = 1
    self._env:Hide()
end

function LSKGameClass:ResetGameStyle()
    local wheel,wallpaper,knife,bullet = self._config:GetGameStyle()
    
    self._env:SetWallpaper(wallpaper)
    self._wheel:LoadSkin(wheel)
    self._gun:SetKnifeSkin(knife)
    self._hud:SetBullet(bullet)

end

function LSKGameClass:DealOpCmd(opCmd, arg)
    -- 处理操作命令
    if opCmd == LSKGameOpCmd.Start then
        self:StartGame(arg)
    elseif opCmd == LSKGameOpCmd.NextLevel then
        self:NextLevel()
    elseif opCmd == LSKGameOpCmd.Replay then
        self:Replay(arg)
    elseif opCmd == LSKGameOpCmd.Over then
        if arg and arg.stop then
            self:StopLevel()
        end
        self:Reset()
    elseif opCmd == LSKGameOpCmd.ShowWheel then
        self._wheel:Show()
    elseif opCmd == LSKGameOpCmd.ShowScene then
        self._env:Show()
    elseif opCmd == LSKGameOpCmd.HideScene then
        self._env:Hide()
    elseif opCmd == LSKGameOpCmd.StopRotate then
        if self._wheel then
            self._wheel:StopRotate()
        end
    elseif opCmd == LSKGameOpCmd.Throw then
        self._throw_notice_timer = NoThrowNotice
    end
    if self._in_playing then
        self._gun:OnDealOpCmd(opCmd,arg)
    end
end

-------------------------------------
-- 插入圆盘.
-- @param lipstick : 口红实体.
-- @param angle : 当前圆盘角度
-------------------------------------
function LSKGameClass:OnHitWheel(lipstick, angle)
    local angle = self._wheel:GetZAngle()
    local isEasyCfg = self._game_mode == LSKGameMode.Normal
    local levelCfg = self._config:GetLevelConfig(self.CurLevel, isEasyCfg)
    if not levelCfg then
        return
    end
    local isLast = (self._pinSuccCnt + 1) == KniveCount
    if (self._pinSuccCnt + 1) < KniveCount then
        self._gun:GenLipstick(self._left_lsk_cnt)
    end
    local isSuccess = self._knifeData:Insert(angle)

    LSKAudio:PlaySound(LSKAudioID.HitWheel)
    if self._game_mode == LSKGameMode.Trick and isLast and self.CurLevel == self._config.MaxLevel then
        isSuccess = false
    end
    if isSuccess then
        self._wheel:PlayHitEffect()
        self._pinSuccCnt = self._pinSuccCnt + 1
        lipstick:SetPinResult(isSuccess)
        lipstick:AttachToWheel(self._wheel.RootTf, self._wheel.Radius)
        
        if KniveCount <= self._pinSuccCnt then
            local isFinalWin = self.CurLevel == self._config.MaxLevel
            self._wheel:PlayBoomEffect()
            self._gun:PlayBoomEffect()

            if self._delegate then
                self._delegate:OnLevelEnd(self.CurLevel, true, isFinalWin)
            end
            self:StopLevel(true)
            return
        end
    else
        self:PinFailed(lipstick)
    end
end

-------------------------------------
-- 打到了已经插入的口红.
-- @param lsk : 发射中的口红.
-- @param lskId : 打中的口红ID.
-------------------------------------
function LSKGameClass:OnHitLipstick(lsk, lskId)
    if (self._pinSuccCnt + 1) < KniveCount then
        self._gun:GenLipstick(self._left_lsk_cnt)
    end
    self:PinFailed(lsk)
end

--[[
    @desc: 口红探测异常 add by jordenwu
    --@lsk: 
    @return:
]]
function LSKGameClass:OnLipstickHitException(lsk)
    self:PinFailed(lsk)
end


function LSKGameClass:PinFailed(lipstick)
    -- self._wheel._rotateCtrl:PauseGame()
    LSKAudio:PlaySound(LSKAudioID.HitLsk)
    lipstick:SetPinResult(false)
    lipstick:PlayHitLskAnim()
    self:OnPlayEffect('LSK/Lipsticks/Hit_LSK',2,nil, lipstick:GetTransform().position)
    if self._delegate then
        self._delegate:OnLevelEnd(self.CurLevel, false)
    end
    self:StopLevel()
end

-------------------------------------
-- 开始游戏.
-- @param arg :游戏参数.
-------------------------------------
function LSKGameClass:StartGame(arg)
    if not arg then
        LogE('开始游戏必须传入参数！')
        return
    end
    if self._effect then
        self._effect:StopAll()
    end
    self:Reset()
    self._delegate = arg.delegate
    self._game_mode = arg.gameMode
    self._prizeInfo = arg.prize or false
    self:StartLevel(1)
end

-------------------------------------
-- 重玩本关.
-------------------------------------
function LSKGameClass:Replay(arg)
    if arg then
        self._game_mode = arg.gameMode
    end
    self:StartLevel(self.CurLevel, true)
end

-------------------------------------
-- 下一关.
-------------------------------------
function LSKGameClass:NextLevel(arg)
    self._gun:Reset()
    self._wheel:Reset()
    self._wheel:Show()
    self._pinSuccCnt = 0
    self._knifeData:Clear()
    if self.CurLevel < self._config.MaxLevel then
        self:StartLevel(self.CurLevel+1)
    else
        LogE('逻辑错误')
    end
end

-------------------------------------
-- 开始关卡.
-- @param level:Level No.
-- @param isContinue:是否是继续本关
-------------------------------------
function LSKGameClass:StartLevel(level,isContinue)

    self.CurLevel = level
    isContinue = isContinue or false
    local isEasyCfg = self._game_mode == LSKGameMode.Normal
    local config = self._config:GetLevelConfig(self.CurLevel, isEasyCfg)
    
    if self._game_mode == LSKGameMode.Simulate or self._game_mode == LSKGameMode.Trial then
        RoundTime = 30
        KniveCount = config.shootcount
    else
        local roundTime, kniveCount = LSKData:GetLevelSettings(self.CurLevel)
        if roundTime and kniveCount then
            RoundTime = roundTime
            KniveCount = kniveCount
        end
    end

    if self._game_mode == LSKGameMode.Trick then
        self._trickSystem:Lanuch(config)
    end

    self._wheel:StartRotate(config, isContinue)
    self._leftSeconds = RoundTime

    self._hud:SetLSKSize(KniveCount)
    self._hud:Countdown(self._leftSeconds)
    self._hud:SetSimulate(self._game_mode == LSKGameMode.Simulate)
    self._hud:SetCurLevel(self.CurLevel)
    self._left_lsk_cnt = KniveCount - self._pinSuccCnt
    self._hud:SetLeftCount(self._left_lsk_cnt)
    
    self._hud:Show(self._prizeInfo)
    self._throw_notice_timer = NoThrowNotice

    if isContinue then
        self._gun:RemovePinFailed()
        local levelCfg = self._config:GetLevelConfig(self.CurLevel, isEasyCfg)
        local idx = KniveCount - self._left_lsk_cnt + 1
        if self._game_mode == LSKGameMode.Trick then
            self._trickSystem:SetCurCount(idx, KniveCount, self.CurLevel == self._config.MaxLevel and (idx+1) == KniveCount)
        end
    else
        self._pinSuccCnt = 0
        self:ResetGameStyle()
    end
    self._gun:GenLipstick(self._left_lsk_cnt)

    ScheduleService:AddTimer(self,self.OnTimerUpdate,self._timer_interval,true)
    self._in_playing = true
end

function LSKGameClass:OnPlayEffect(path, duration, parent, pos)
    parent = parent or self._env:GetRootTf()
    self._effect:Play(path, duration, parent, pos)
end

-------------------------------------
-- 结束本关.
-------------------------------------
function LSKGameClass:StopLevel(succ)
    if succ then
        self._wheel:StopRotate()
    end
    ScheduleService:RemoveTimer(self,self.OnTimerUpdate)
    self._in_playing = false
    if self._game_mode == LSKGameMode.Trick then
        self._trickSystem:Shutdown()
    end
end

-------------------------------------
-- 重置游戏.
-------------------------------------
function LSKGameClass:Reset()
    self.CurLevel = 1
    self._wheel:Reset()
    self._gun:Reset()
    self._hud:Hide()
    self._knifeData:Clear()
    self._pinSuccCnt = 0
end

-------------------------------------
-- 更新-倒计时处理.
-------------------------------------
function LSKGameClass:OnTimerUpdate()
    self._leftSeconds = self._leftSeconds - self._timer_interval
    if self._leftSeconds <= 0 then
        if self._delegate then
            self._delegate:OnLevelEnd(self.CurLevel, false)
        end
        self:StopLevel(false)
    end
    -- 倒计时更新
    self._hud:Countdown(math.floor(self._leftSeconds))
    -- 特效驱动
    if self._effect then
        self._effect:OnUpdate(self._timer_interval)
    end
    -- 作弊系统驱动
    if self._game_mode == LSKGameMode.Trick and self._trickSystem then
        self._trickSystem:OnUpdate(self._timer_interval)
    end
    if self._throw_notice_timer then
        self._throw_notice_timer = self._throw_notice_timer - self._timer_interval
        if self._throw_notice_timer <= 0 then
            self._hud:SetThrowNoticeActive(true)
            self._throw_notice_timer = NoThrowNotice
        end
    end
    -- 
    if self._wheel then
        self._wheel:OnUpdate(self._timer_interval)
    end
end

function LSKGameClass:OnThrow()
    self._left_lsk_cnt = self._left_lsk_cnt - 1
    if self._left_lsk_cnt >= 0 then
        self._hud:SetLeftCount(self._left_lsk_cnt)
    end
    local isEasyCfg = self._game_mode == LSKGameMode.Normal
    if self._game_mode == LSKGameMode.Trick then
        local levelCfg = self._config:GetLevelConfig(self.CurLevel, isEasyCfg)
        local idx = KniveCount - self._left_lsk_cnt + 1
        self._trickSystem:SetCurCount(idx, KniveCount)
    end
end

function LSKGameClass:InPlay()
    return self._in_playing
end

function LSKGameClass:Uninit()
    self.CurLevel = false
    self._wheelLskNode = false
    if self._wheel then
        self._wheel:Uninit()
        self._wheel = false
    end

    if self._gun then
        self._gun:Uninit()
        self._gun = false
    end

    if self._env then
        self._env:Uninit()
        self._env = false
    end

    if self._hud then
        self._hud:Destroy()
        self._hud = false
    end

    if self._config then
        self._config:Uninitialize()
        self._config = false
    end

    if self._effect then
        self._effect:Uninit()
        self._effect = false
    end

    if self._trickSystem then
        self._trickSystem = false
    end

    if self._knifeData then
        self._knifeData:Uninit()
        self._knifeData = false
    end
end
