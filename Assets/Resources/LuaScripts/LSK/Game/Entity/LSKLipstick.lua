local LSKLipstickClass = DeclareClass("LSKLipstickClass")

LSKMoveState = {
    None = 1,
    Ready = 2,
    Shoot = 3,
    Stop = 4
}
local Vector3 = CS.UnityEngine.Vector3

function LSKLipstickClass:ctor()
    self._asset = false
    self._assetGo = false
    self._config = false
    self._state = LSKMoveState.None
    self._moveCtrl = false
    self._delegate = false
    self._rootTf = false
    self.PinSuccess = false
    self.ID = -1
end

function LSKLipstickClass:Init(parent, speed, radius, delegate, id, name)

    self._asset = AssetService:LoadInstantiateAsset("LSK/Lipsticks/"..name, LifeType.Manual)
    if not self._asset or not self._asset.RootGo then
        return
    end

    self._assetGo = self._asset.RootGo
    self._rootTf = self._asset.RootTf

    self._moveCtrl = self._asset.RootTf:GetComponent("LSKMoveComponent")
    self._moveCtrl.ID = id
    self.ID = id
    self._delegate = delegate
    self._moveCtrl.OnHitRotateTarget = function(angleZ)
        self:HitWheel(angleZ)
    end
    self._moveCtrl.OnHitMoveTarget = function(lipstickId)
        self:HitLipstick(lipstickId)
    end
    self.PinSuccess = false
    self._moveCtrl:SetSpeed(0, speed, 0)

    local assetTf = self._asset.RootTf
    if assetTf then
        assetTf:SetParent(parent)
        assetTf.localScale = Vector3_One
        assetTf.localPosition = Vector3_Zero
        assetTf.localEulerAngles = Vector3_Zero
    end

    self:Show()
    self._state = LSKMoveState.Ready
end

function LSKLipstickClass:HitWheel(angleZ)

    if self._delegate then
        self._delegate:OnHitWheel(self, angleZ)
    end
end

function LSKLipstickClass:HitLipstick(lipstickId)
    --射线判定失效特殊情况
    if lipstickId==-1 then
        LogE("LSKLipstick Ray Exception")
        if self._delegate then
            self._delegate:OnLipstickHitException(self)
        end
        return
    end
    --
    if self._delegate then
        self._delegate:OnHitLipstick(self, lipstickId)
    end
end

function LSKLipstickClass:SetPinResult(isPinSuccess)
    self.PinSuccess = isPinSuccess
end

function LSKLipstickClass:AttachToWheel(target, radius)
    self._moveCtrl:AttachToWheel(target, radius)
end

function LSKLipstickClass:Shoot()
    if self._state == LSKMoveState.Shoot then
        return
    end

    self._state = LSKMoveState.Shoot
    self._moveCtrl:Shoot()
end

function LSKLipstickClass:Show()
    if self._assetGo then
        self._assetGo:SetActive(true)
    end
end

function LSKLipstickClass:Hide()
    if self._assetGo then
        self._assetGo:SetActive(false)
    end
end

function LSKLipstickClass:Destroy()
    if self._asset then
        AssetService:Unload(self._asset)
        self._asset = false
    end
    if self._moveCtrl then
        self._moveCtrl.OnHitRotateTarget = nil
        self._moveCtrl.OnHitMoveTarget = nil
    end
    self._config = false
    self._moveCtrl = false
    self._delegate = false
    self._rootTf = false
end

function LSKLipstickClass:PlayHitLskAnim()
    if not self._moveCtrl then
        return
    end
    self._moveCtrl:SetPBMGravity(-50)
    self._moveCtrl:SetPBMRotateRange(Vector3(600, 100, 600), Vector3(800, 200, 800))
    self._moveCtrl:SetPBMInitSpeedRange(Vector3(0, -0.4, 0), Vector3(0, 0.4, 0))
    local tmpPos = self._rootTf.position
    tmpPos.z = -1
    self._rootTf.position = tmpPos
    
    self._moveCtrl:DoParabolaMove(true)
end

function LSKLipstickClass:PlayBoomAnim()
    if not self._moveCtrl then
        return
    end

    self._moveCtrl:SetPBMGravity(-40)
    self._moveCtrl:SetPBMRotateRange(Vector3(-200, -50, -200), Vector3(300, 100, 300))
    self._moveCtrl:SetPBMInitSpeedRange(Vector3(0.04, 0.04, 0), Vector3(0.06, 0.06, 0))
    self._moveCtrl:DoParabolaMove(false)
end

function LSKLipstickClass:GetTransform()
    return self._rootTf
end
