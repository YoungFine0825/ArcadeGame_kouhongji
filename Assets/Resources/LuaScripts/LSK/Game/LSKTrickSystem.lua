--[[********************************************************************
*		作者： XH
*		时间： 2018-11-26
*		描述： 作弊系统
*********************************************************************--]]
local LSKTrickSystemClass = DeclareClass("LSKTrickSystemClass")

local TrickMode = 
{
    None = 0,
    Back = 1,
    Pass = 2,
}
function LSKTrickSystemClass:ctor()
    self._config = false
    self._duration = 0
    self._running = false
    self._wheel = false

    self._trickRate = 0
    self._curCnt = 0

    self._knifeData = false
    self._curMode = false

    self._circleTrick = false

    self._lastTrickMode = TrickMode.None
    self._trickCnt = 0
end

function LSKTrickSystemClass:Init(knifeData, wheelCtrl)
    self._knifeData = knifeData
    self._wheel = wheelCtrl
    self._wheel._rotateCtrl.OnTrickOver = function()
        if self._circleTrick then
            self:Trick()
        end
    end
end

function LSKTrickSystemClass:Uninit()
    self._knifeData = false
    self._wheel = false
    self._wheel._rotateCtrl.OnTrickOver = nil
end
-------------------------------------
-- 开启作弊系统.
-- @param config:作弊配置.
-- @param wheelCtrl:转盘控制器.
-------------------------------------
function LSKTrickSystemClass:Lanuch(config)
    self._circleTrick = false
    self._running = true
    self._config = config
    self._curCnt = 0
    self._curMode = TrickMode.None
    self:CalculateDuration()
end

-------------------------------------
-- 关闭作弊系统.
-------------------------------------
function LSKTrickSystemClass:Shutdown()
    -- Reset Data
    self._running = false

    self._trickRate = 0
    self._config = false
    self._circleTrick = false
end

function LSKTrickSystemClass:SetCurCount(count, total, circleTrick)
    if not self._config then
        LogE('Error')
        return
    end

    self._curCnt = count
    --self._config.cheatrate
    local cnt = #self._config.cheatrate
    if total - count + 1 <= cnt then
        self._trickRate = self._config.cheatrate[total - count + 1] or 0
    end
    self._circleTrick = circleTrick or false
    if self._circleTrick then
        --LogD('<color=lime>开启循环作弊</color>')
    end
    self:CalculateDuration()
end

-------------------------------------
-- 作弊系统驱动函数.
-- @param deltaTime:Delta Time.
-------------------------------------
function LSKTrickSystemClass:OnUpdate(deltaTime)
    if not self._running then
        return
    end

    self._duration = self._duration - deltaTime

    if self._duration <= 0 then
        self:Trick()
        self:CalculateDuration()
    end
end

function LSKTrickSystemClass:CalculateDuration()
    if not self._config then
        return
    end
    self._duration = LSKUtil:GetFloatRandom(self._config.cheatfrequency[1],self._config.cheatfrequency[2])
end

function LSKTrickSystemClass:DoTrickImmediate()
    

end

-------------------------------------
-- 新版的作弊系统
-------------------------------------
function LSKTrickSystemClass:Trick()
    if not self._wheel then
        return
    end

    if self._trickRate <= 0 then
        return
    end

    local gap = self._knifeData:GetRandomValidGap()
    if not gap then
        return
    end

    local trickRate = LSKUtil:GetRandom(0,100)
    local isTrick = trickRate <= self._trickRate
    if not isTrick then
        return
    end
    self._circleTrick = true
    local trickMode = TrickMode.Back
    if self._knifeData:IsCanQuickPass() then
        trickMode = LSKUtil:GetRandom(1,2)
    end
    
    if self._lastTrickMode == trickMode then
        self._trickCnt = self._trickCnt + 1
    else
        self._trickCnt = 0
        self._lastTrickMode = trickMode
    end

    if  self._trickCnt >= 1 then
        trickMode = trickMode == TrickMode.Back and TrickMode.Pass or TrickMode.Back
        self._lastTrickMode = trickMode
        self._trickCnt = 0
    end
    LogD('开始作弊：Mode = '..((trickMode == 1) and '勾引' or '快速通过'))

    self._wheel:Trick(trickMode, gap.min, gap.max)
end
