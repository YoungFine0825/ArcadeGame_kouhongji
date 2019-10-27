local LSKEffectMgrClass = DeclareClass("LSKEffectMgrClass")

function LSKEffectMgrClass:ctor()
    self._cache_list = {}
    self._working_list = {}
end

function LSKEffectMgrClass:Init()
end

function LSKEffectMgrClass:Uninit()
    for i=#self._working_list,1,-1 do
        self._working_list[i]:Uninit()
        table.remove(self._working_list,i)
    end
    for j=#self._cache_list,1,-1 do
        self._cache_list[j]:Uninit()
        table.remove(self._cache_list,j)
    end
end

function LSKEffectMgrClass:Play(path, duration, parent, pos)
    local effect = self:GetEffect(path)
    if effect then
        effect:Init(path, duration, self, parent, pos)
        if effect._instantiated then
            self._working_list[#self._working_list+1] = effect
            effect:Play()
        end
    end
end

function LSKEffectMgrClass:Stop(path)
    if not self._working_list or #self._working_list <= 0 then
        return
    end

    for i=#self._working_list, 1, -1 do
        if self._working_list[i].Name == path then
            local effect = self._working_list[i]
            effect:Stop()
            self._cache_list[#self._cache_list+1] = effect
            table.remove(self._working_list,i)
        end
    end
end

function LSKEffectMgrClass:StopAll()
    if not self._working_list or #self._working_list <= 0 then
        return
    end

    for i=#self._working_list, 1, -1 do
        local effect = self._working_list[i]
        effect:Stop()
        self._cache_list[#self._cache_list+1] = effect
        -- LogD('Remove Effect Working List : '..tostring(i)..',Count = '..#self._working_list)
        table.remove(self._working_list,i)
    end
end

function LSKEffectMgrClass:OnUpdate(deltaTime)
    if #self._working_list == 0 then
        return
    end
    for k, v in ipairs(self._working_list) do
        v:OnUpdate(deltaTime)
    end
end

function LSKEffectMgrClass:OnRecycle(effect)
    for i=#self._working_list, 1, -1 do
        if self._working_list[i] == effect then
            self._cache_list[#self._cache_list+1] = self._working_list[i]
            table.remove(self._working_list,i)
            break
        end
    end
end

function LSKEffectMgrClass:GetEffect(name)
    --首先从缓存里取
    for k,v in ipairs(self._cache_list) do
        if v.Name == name then
            table.remove(self._cache_list,k)
            -- LogD('GetCahche Eff:'..name)
            return v
        end
    end
    --缓存里没有，直接创建
    local effect = ClassLib.LSKEffectClass.new()
    return effect
end