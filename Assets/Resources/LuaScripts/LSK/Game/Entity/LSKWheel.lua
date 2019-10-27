local LSKWheelClass = DeclareClass("LSKWheelClass")

local HilightUtil = CS.XH.Pin.LSKRotateComponent
function LSKWheelClass:ctor()
    self._entityObj = false
    self._config = false
    self._asset = false
    self._rotateCtrl = false
    self.RootTf = false
    self._rootGo = false
    self._assetGo = false
    self._delegate = false
    self._modelName = false
    self._shakeAnim = false
    self.Radius = false
    self._rootPos = false
    self._material = false
    self._hilight = false
    self._hilightCnt = 0
end

function LSKWheelClass:Init(rotateCtrl,delegate,shakeAnim, radius)
    self._shakeAnim = shakeAnim
    self._rotateCtrl = rotateCtrl
    self.RootTf = rotateCtrl.transform
    self._delegate = delegate
    self.Radius = radius
    self._rootPos = self.RootTf.position

end

function LSKWheelClass:LoadSkin(name)
    self._modelName = name
    self:UnloadSkin()
    self._asset = AssetService:LoadInstantiateAsset('LSK/Wheels/'..name,LifeType.Manual)
    self._assetGo = self._asset.RootGo
    local assetTf = self._asset.RootTf
    if assetTf then
        assetTf:SetParent(self.RootTf)
        assetTf.localScale = Vector3_One
        assetTf.localPosition = CS.UnityEngine.Vector3(0,0,-0.25)
        assetTf.localEulerAngles = Vector3_Zero

        local meshRenderer = assetTf:GetComponent('MeshRenderer')
        if meshRenderer then
            self._material = meshRenderer.material
        end
    end
    self:Show()
end

function LSKWheelClass:PlayHitEffect()
    if self._material then
        self:SetMatUnlitColor(1.2, 1.2, 1.2)
        self._hilight = true
    end
    self._delegate:OnPlayEffect('LSK/Wheels/'..self._modelName..'_hit', 1, nil, self._rootPos)
    self:PlayShakeAnim()
end

function LSKWheelClass:SetMatUnlitColor(r, g, b)
    if not self._material then
        return
    end
    
    local color = self._material:GetVector('_Color')
    color.x = r
    color.y = g
    color.z = b
    self._material:SetVector("_Color", color)
end

function LSKWheelClass:PlayBoomEffect()
    self:Hide()
    LSKAudio:PlaySound(LSKAudioID.Boom)

    self._delegate:OnPlayEffect('LSK/Wheels/'..self._modelName..'_boom', 4)
end

function LSKWheelClass:OnUpdate()
    if self._hilight then
        self._hilightCnt = self._hilightCnt + 1
        if self._hilightCnt >= 2 then
            self:SetMatUnlitColor(1, 1, 1)
            self._hilight = false
        end
    end
end

function LSKWheelClass:PlayShakeAnim()
    if self._shakeAnim then
        self._shakeAnim:Play()
    end
end
function LSKWheelClass:UnloadSkin()
    if self._asset then
        self._assetGo = false
        AssetService:Unload(self._asset)
        self._asset = false
        self._material = false

    end
end

function LSKWheelClass:SetCtrlData(config)
    if config then
        local arg = {
            InitSpeed = {},
            ChangeSpeed = {},
            ChangeDuration = {},
        }
        for _,initSpeed in ipairs(config.initspeed) do
            local area = {}
            area.Min = initSpeed[1]
            area.Max = initSpeed[2]
            arg.InitSpeed[#arg.InitSpeed+1] = area
        end
        for _,speedChange in ipairs(config.speedchange) do
            local area = {}
            area.Min = speedChange[1]
            area.Max = speedChange[2]
            arg.ChangeSpeed[#arg.ChangeSpeed+1] = area
        end
        -- for _,changeDuration in ipairs(config.changefrequency) do
        local area = {}
        area.Min = config.changefrequency[1]
        area.Max = config.changefrequency[2]
        arg.ChangeDuration[#arg.ChangeDuration+1] = area
        -- end
        self._rotateCtrl:InitData(arg)
    end
end

function LSKWheelClass:StartRotate(config, isContinue)
    if not self._rotateCtrl then
        return
    end
    if not isContinue then
        self:SetCtrlData(config)
    end
    self._rotateCtrl:StartRotate()
end

function LSKWheelClass:StopRotate()
    if self._rotateCtrl then
        self._rotateCtrl:StopRotate()
    end
end

function LSKWheelClass:Trick(trickMode, min, max)
    if self._rotateCtrl then
        self._rotateCtrl:Trick(trickMode, min, max)
    end
end

function LSKWheelClass:GetSpeed()
    if self._rotateCtrl then
        return self._rotateCtrl:GetSpeed()
    end
end

function LSKWheelClass:GetZAngle()
    if self.RootTf then
        return self.RootTf.eulerAngles.z
    end
end

function LSKWheelClass:Uninit(_rotateCtrl)
    self._config = false
    self._entityObj = false
    self._config = false
    self._rotateCtrl = false
    self.RootTf = false
    self._rootGo = false
    self._delegate = false
    self._modelName = false
    self._shakeAnim = false
    self.Radius = false
    self._rootPos = false
    self:UnloadSkin()
end

function LSKWheelClass:Show()
    if self._assetGo then
        self._assetGo:SetActive(true)
        if self._material then
            self:SetMatUnlitColor( 1, 1, 1)
            self._hilight = false
        end
    end
end

function LSKWheelClass:Hide()
    if self._assetGo then
        self._assetGo:SetActive(false)
    end
end

function LSKWheelClass:Reset()

end