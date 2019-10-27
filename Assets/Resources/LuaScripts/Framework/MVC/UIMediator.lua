--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-07-04
*		描述： 模块UI中间件
*********************************************************************--]]
local UIMediatorClass = DeclareClass("UIMediatorClass")
local null = {}

function UIMediatorClass:ctor()
    
    self._module = false
    self._forms = {}
    self._belongUIStateName = false
    self._inState = false
end

function UIMediatorClass:Create(md)
    self._module = md
    self._belongUIStateName = self:vGetBelongUIStateName() or false

    self:vOnInitialize()
end

function UIMediatorClass:Destroy()
    self:vOnUninitialize()
    self._module = false

    for i = 1, #self._forms do
        if self._forms[i] ~= null then
            LogE("UIMediatorClass.Destroy : FormClass %s of %s is living", self._forms[i].__classname, self.__classname)
        end
    end
    
    self._forms = {}
    self._belongUIStateName = false
    self._inState = false
end

--UIState改变处理
function UIMediatorClass:ChangeUIState(callType, changeType, oldStateName, newStateName, stateParam)
    
    if not self._belongUIStateName then
        return
    end

    local old = false
    local new = false
    for i = 1, #self._belongUIStateName do
        local name = self._belongUIStateName[i]

        if oldStateName and oldStateName == name then
            old = true
        end

        if newStateName and newStateName == name then
            new = true
        end

        if old and new then
            break
        end
    end

    if old ~= new then
        if old then
            if callType == 1 then
                self._inState = false
                self:vOnUIStateOut(oldStateName, newStateName, changeType)
           end
        else
            if callType == 2 then
                self._inState = true
                self:vOnUIStateIn(oldStateName, newStateName, stateParam, changeType)
            end
        end
    elseif old and new then
        if callType == 0 then
            self:vOnUIStateStay(oldStateName, newStateName, stateParam, changeType)
        end
    end
end

-----------
function UIMediatorClass:vGetBelongUIStateName()
end


function UIMediatorClass:vOnInitialize()
end


function UIMediatorClass:vOnUninitialize()
end


function UIMediatorClass:vOnUIStateIn(oldStateName, newStateName, stateParam, changeType)
end


function UIMediatorClass:vOnUIStateOut(oldStateName, newStateName, changeType)
end


function UIMediatorClass:vOnUIStateStay(oldStateName, newStateName, stateParam, changeType)
end

function UIMediatorClass:vOnUpdateUI(id, argument)
end

--[[
    @desc: 操作回调
    --@id:操作标识，推荐使用字符串和数字
	--@argument: 操作参数
    @return:是否阻断向上传递操作
]]
function UIMediatorClass:vOnAction(id, argument)
end


function UIMediatorClass:IsInState()
    return self._inState
end


--[[
    @desc: 创建窗口实例
    --@cls:类名
	--@argument:参数
	--@customID: 自定义ID
    @return:
]]
function UIMediatorClass:CreateFormClass(cls, argument, customID)
    if not cls then
        LogE("UIMediatorClass.CreatePrefabClass : invalid parameter check Class Required ?")
        return nil
    end

    local obj = cls.new()
    obj:Create(self, argument, customID)
    self._forms[#self._forms+1] = obj
    return obj
end


--[[
    @desc: 销毁窗口类
    --@obj: 实例
    @return:无
]]
function UIMediatorClass:DestroyFormClass(obj)
    if not obj then
        LogE("UIMediatorClass.DestroyFormClass : invalid parameter")
        return
    end

    for i = 1, #self._forms do
        if self._forms[i] == obj then
            self._forms[i] = null
            break
        end
    end

    obj:Destroy()
end


--[[
    @desc: 通过ID销毁窗口实例
    --@customID: 自定义ID
    @return:
]]
function UIMediatorClass:DestroyFormClassByCustomID(customID)
    if not customID then
        LogE("UIMediatorClass.DestroyFormClassByCustomID : invalid parameter")
        return
    end
    
    for i = #self._forms, 1, -1 do
        local pc = self._forms[i]
        if pc ~= null and pc._customID == customID then
            self._forms[i] = null
            pc:Destroy()
        end
    end

end

--[[
    @desc: 销毁所有
    @return:无
]]
function UIMediatorClass:DestroyAllFormClass()
    for i = 1, #self._forms do
        local obj = self._forms[i]
        if obj ~= null then
            self._forms[i] = null
            obj:Destroy()
        end        
    end
end

--[[
    @desc: 发送更新UI（只能在实例内部调用)
    --@id:刷新ID标识
	--@argument:参数
	--@dummy: 不要管这个参数
    @return:
]]
function UIMediatorClass:UpdateUI(id, argument, dummy)
    if not id then
        LogE("UIMediatorClass.UpdateUI : invalid parameter")
        return
    end

    if dummy then
        if self:vOnUpdateUI(id, argument) then
            return
        end
    end
    
    for i = #self._forms, 1, -1 do
        local obj = self._forms[i]
        if obj ~= null then
            obj:vOnUpdateUI(id, argument, true)
        end
    end
end

--[[
    @desc: 发送操作
    --@id:操作ID标识
	--@argument:参数
	--@dummy: 不要管这个参数
    @return:
]]
function UIMediatorClass:Action(id, argument, dummy)
    if not id then
        LogE("UIMediatorClass.SendAction : invalid parameter")
        return
    end

    if dummy then
        if self:vOnAction(id, argument) then
            return
        end
    end
    
    self._module:vOnAction(id, argument)
end
