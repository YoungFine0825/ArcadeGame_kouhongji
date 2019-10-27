--[[********************************************************************
*		作者： yangfan
*		时间： 2018-12-10
*		描述： 退出登录提示弹窗
*********************************************************************--]]

local WWJExitFormClass = DeclareClass("WWJExitFormClass", ClassLib.UIFormClass)
local MAX_WAIT_SECONDS = 20
function WWJExitFormClass:ctor()
	self._uiCntDownTextIndex = 0;
	self._uiExitToggleIndex = 1;
	self._uiCancelToggleIndex = 2;

	self._uiCntDownText = false
	self._uiExitToggle = false
	self._uiCancelToggle = false

    self._handlerObj = false
    self._onExitBtnClickHandler = false
    self._onCancelBtnClickHandler = false
    self._waitSeconds = 0
end

function WWJExitFormClass:vGetPath()
	return 'WWJ/Select/WWJExitForm'
end

function WWJExitFormClass:vOnResourceLoaded()
	self._uiCntDownText = self:GetComponent(self._uiCntDownTextIndex)
	self._uiExitToggle = self:GetComponent(self._uiExitToggleIndex)
	self._uiCancelToggle = self:GetComponent(self._uiCancelToggleIndex)

end

function WWJExitFormClass:vOnResourceUnLoaded()
	self._uiCntDownText = false
	self._uiExitToggle = false
	self._uiCancelToggle = false

end

function WWJExitFormClass:vOnInitialize(argument)
    self._uiCancelToggle.isOn = true
    self._uiCancelToggle.graphic.gameObject:SetActive(true)
    self._uiExitToggle.graphic.gameObject:SetActive(false)
    --
    self._waitSeconds = MAX_WAIT_SECONDS
    self._uiCntDownText.text = self._waitSeconds
    ScheduleService:AddTimer(self,self.OnUpdateCntDown,1,true)
end

function WWJExitFormClass:vOnUninitialize()

end

function WWJExitFormClass:UpdateUI(id,argument)
    if id == "OnArcadeRockerInput" then
        if argument == ArcadeInput.RockerState.RockerMoveForward then
            if not self._uiExitToggle.isOn then
                self:DoSelectBtn("Exit")
            end
        elseif argument == ArcadeInput.RockerState.RockerMoveBack then
            if not self._uiCancelToggle.isOn then
                self:DoSelectBtn("Cancel")
            end
        end
    elseif id == "OnArcadeOkBtnClick" then
        if self._uiExitToggle.isOn then
            if self._handlerObj and self._onExitBtnClickHandler then
                return self._onExitBtnClickHandler(self._handlerObj)
            end
        else
            if self._handlerObj and self._onCancelBtnClickHandler then
                return self._onCancelBtnClickHandler(self._handlerObj)
            end
        end
    end
end

function WWJExitFormClass:DoCreate(handler,exitBtnHandler,cancelBtnHandler)
    self._handlerObj = handler
    self._onExitBtnClickHandler = exitBtnHandler
    self._onCancelBtnClickHandler = cancelBtnHandler
    self:Create(nil,nil,0)
end

function WWJExitFormClass:DoDestroy()
    ScheduleService:RemoveTimer(self)
    self:Destroy()
end

function WWJExitFormClass:DoSelectBtn(name)
    if name == "Exit" then
        self._uiExitToggle.isOn = true
        self._uiExitToggle.graphic.gameObject:SetActive(true)
        self._uiCancelToggle.graphic.gameObject:SetActive(false)
    elseif name == "Cancel" then
        self._uiCancelToggle.isOn = true
        self._uiCancelToggle.graphic.gameObject:SetActive(true)
        self._uiExitToggle.graphic.gameObject:SetActive(false)
    end 

end

function WWJExitFormClass:OnUpdateCntDown()
    self._waitSeconds = self._waitSeconds - 1
    if self._waitSeconds < 0 then
        ScheduleService:RemoveTimer(self)
        if self._handlerObj and self._onCancelBtnClickHandler then
            return self._onCancelBtnClickHandler(self._handlerObj)
        end
    else
        self._uiCntDownText.text = self._waitSeconds
    end
end

