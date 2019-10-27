--[[********************************************************************
*		作者： XH
*		时间： 2018-11-16
*		描述： 口红发射器
*********************************************************************--]]
local LSKGunClass = DeclareClass("LSKGunClass")

function LSKGunClass:ctor()
    self._usedList = false
    self._startNode = false
    self._readyLipstick = false
    self._wheelRadius = false
    self._delegate = false
    self._speed = false
    self._gunName = false
end

function LSKGunClass:Init(startNode, radius, delegate, speed)
    self._usedList = {}
    self._startNode = startNode
    self._wheelRadius = radius
    self._delegate = delegate
    self._speed = speed
end

function LSKGunClass:Uninit()
    self:RemoveAll()
    if self._readyLipstick then
        self._readyLipstick:Destroy()
        self._readyLipstick = false
    end
    self._startNode = false
    self._wheelRadius = false
    self._delegate = false
end

function LSKGunClass:GenLipstick(id)
    if self._readyLipstick then
        return
    end

    local lipstick = ClassLib.LSKLipstickClass.new()

    lipstick:Init(self._startNode, self._speed, self._wheelRadius, self._delegate, id, self._gunName)
    self._readyLipstick = lipstick
end

function LSKGunClass:SetKnifeSkin(name)
    self._gunName = name
    if self._readyLipstick then
        self._readyLipstick:Destroy()
        self._readyLipstick = false
    end
end

function LSKGunClass:OnDealOpCmd(id, arg)
    if id == LSKGameOpCmd.Throw then
        LSKAudio:PlaySound(LSKAudioID.Throw)
        if self._readyLipstick then
            self._readyLipstick:Shoot()
            table.insert(self._usedList,self._readyLipstick)
            self._readyLipstick = false
            self._delegate:OnThrow()
        end
    end
end

function LSKGunClass:PlayBoomEffect()
    for k,v in ipairs(self._usedList) do
        v:PlayBoomAnim()
    end
end

function LSKGunClass:RemoveAll()
    for _, v in ipairs(self._usedList) do
        v:Destroy()
    end
    self._usedList = {}
end

function LSKGunClass:RemovePinFailed()
    for i = #self._usedList, 1, -1 do
        local lipstick = self._usedList[i]
        if not lipstick.PinSuccess then
            table.remove(self._usedList, i)
            lipstick:Destroy()
        end
    end
end

function LSKGunClass:Reset()
    self:RemoveAll()
end