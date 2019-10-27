--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-07-04
*		描述： 窗口类
*********************************************************************--]]
local UIFormClass = DeclareClass("UIFormClass")

function UIFormClass:ctor()
    self._uiAsset = false
    self._rootGo = false
    self._rootTf = false
    self._form = false

    self._mediator = false
    self._customID = 0
end

--[[
    @desc: 创建禁止调用
    --@mediator:
	--@argument:
	--@customID: 
    @return:
]]
function UIFormClass:Create(mediator, argument, customID)
    
    local resourcePath = self:vGetPath()
    if not resourcePath then
        LogE("UIFormClass.Create : resource path error")
        return
    end

    self._uiAsset = AssetService:LoadUIAsset(resourcePath, LifeType.UIState)
    if not self._uiAsset then
        LogE("UIFormClass.Create : load resource failed - %s", resourcePath)
        return
    end

    self._mediator = mediator or false
    self._rootGo = self._uiAsset.Go
    self._rootTf = self._uiAsset.Tf
    self._form = self._uiAsset.FormCom
    self._form.FormPath=resourcePath

    if not self._form then
        LogE("UIFormClass.Create : load resource failed not have UIForm MonoBehavier - %s", resourcePath)
        return
    end

    self._customID = customID or 0
    self:vOnResourceLoaded()

    MVCService:OpenForm(self)
    
    self._rootGo:SetActive(true)
    self:vOnInitialize(argument)

    
end

--[[
    @desc: 销毁禁止外部调用
    @return:
]]
function UIFormClass:Destroy()

    self:vOnUninitialize()

    MVCService:CloseForm(self)
    
    self:vOnResourceUnLoaded()

    AssetService:Unload(self._uiAsset)

    self._uiAsset = false
    self._rootGo = false
    self._rootTf = false
    self._form = false

    self._mediator = false

end

--------------------
function UIFormClass:vGetPath()
end


function UIFormClass:vOnResourceLoaded()
end


function UIFormClass:vOnResourceUnLoaded()
end


function UIFormClass:vOnInitialize(argument)
end


function UIFormClass:vOnUninitialize()
end


function UIFormClass:vOnUpdateUI(id, argument)
end

------------------

function UIFormClass:GetCustomID()
    return self._customID
end


function UIFormClass:GetRootGo()
    return self._rootGo
end


function UIFormClass:GetRootTf()
    return self._rootTf
end


function UIFormClass:GetGameObject(index)
    local prefab = self._form
    if not prefab then
        return nil
    end

    return prefab:GetCacheGameObject(index)
end

function UIFormClass:GetTransform(index)
    local prefab = self._form
    if not prefab then
        return nil
    end
    return prefab:GetCacheTransform(index)
end

function UIFormClass:GetComponent(index)
    local prefab = self._form
    if not prefab then
        return false
    end
    return prefab:GetCacheComponent(index)
end

function UIFormClass:ActiveForm(active)
    if self._form then
        self._form:SetActive(active)
    end
end

--[[
    @desc: 发送操作
    --@id:操作ID标识
	--@argument:参数
	--@dummy: 不要管这个参数
    @return:
]]
function UIFormClass:Action(id, argument, dummy)
    if not id then
        LogE("UIFormClass.SendAction : invalid parameter")
        return
    end

    if dummy then
        if self:vOnAction(id, argument) then
            return
        end
    end
    
    self._mediator:Action(id, argument, true)
end

