local LSKEffectClass = DeclareClass("LSKEffectClass")

function LSKEffectClass:ctor()
    self.Name = false
    self._duration = 0
    self._elapsed_time = 0
    self._delegate = false
    self._playing = false
    self._asset = false
    self._obj = false
    self._instantiated = false
end

function LSKEffectClass:Instantiate(path,parent,pos)

    self._asset = AssetService:LoadInstantiateAsset(path,LifeType.Manual)
    self.Name = path
    if self._asset then
        self._obj = self._asset.RootGo
        local transform = self._asset.RootTf
        if  transform then
            transform:SetParent(parent or nil)
            transform.position = pos or Vector3_Zero
        end
        self._instantiated = true
    else
        LogE("LSKEffect Load Asset Failed.."..path)
        self._instantiated = false
    end
end

function LSKEffectClass:Init(path, duration, delegate, parent, pos)
    self._duration = duration
    self._delegate = delegate
    if not self._instantiated then
        self:Instantiate(path, parent, pos)
    end
end

function LSKEffectClass:Play()
    if self._obj then
        self._obj:SetActive(true)
    end
    self._elapsed_time = 0
    self._playing = true
end

function LSKEffectClass:Stop()
    if  self._obj then
        self._obj:SetActive(false)
    end
    self._delegate:OnRecycle(self)
    self._playing = false
end

function LSKEffectClass:OnUpdate(deltaTime)
    
    if not self._playing then
        return
    end
    self._elapsed_time = self._elapsed_time + deltaTime
    if self._elapsed_time >= self._duration then
        self:Stop()
        return
    end
end

function LSKEffectClass:Uninit()
    self._obj = false
    if self._asset then
        AssetService:Unload(self._asset)
        self._asset = false
    end
    self._instantiated = false
end


