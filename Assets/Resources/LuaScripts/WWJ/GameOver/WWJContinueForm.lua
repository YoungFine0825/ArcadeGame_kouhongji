--[[********************************************************************
*		作者： yangfan
*		时间： 2018-12-14
*		描述： 继续游戏弹窗。询问玩家是否再玩一次
*********************************************************************--]]

local WWJContinueFormClass = DeclareClass("WWJContinueFormClass", ClassLib.UIFormClass)
local MAX_WAIT_SECONDS = 20
function WWJContinueFormClass:ctor()
	self._uiCntDownTextIndex = 0;
	self._uiPriceTextIndex = 1;
	self._uiContinueToggleIndex = 2;
	self._uiBackToggleIndex = 3;

	self._uiCntDownText = false
	self._uiPriceText = false
	self._uiContinueToggle = false
    self._uiBackToggle = false
    
    self._handlerObj = false
    self._onContinueBtnHandler = false
    self._onBackBtnHandler = false
    self._waitSeconds = 0

end

function WWJContinueFormClass:vGetPath()
	return 'WWJ/Game/WWJContinueForm'
end

function WWJContinueFormClass:vOnResourceLoaded()
	self._uiCntDownText = self:GetComponent(self._uiCntDownTextIndex)
	self._uiPriceText = self:GetComponent(self._uiPriceTextIndex)
	self._uiContinueToggle = self:GetComponent(self._uiContinueToggleIndex)
	self._uiBackToggle = self:GetComponent(self._uiBackToggleIndex)

end

function WWJContinueFormClass:vOnResourceUnLoaded()
	self._uiCntDownText = false
	self._uiPriceText = false
	self._uiContinueToggle = false
	self._uiBackToggle = false

end

function WWJContinueFormClass:vOnInitialize(argument)
    self._uiContinueToggle.isOn = true
    self._uiContinueToggle.graphic.gameObject:SetActive(true)
    self._uiBackToggle.graphic.gameObject:SetActive(false)
    --
    self._waitSeconds = MAX_WAIT_SECONDS
    self._uiCntDownText.text = self._waitSeconds
    ScheduleService:AddTimer(self,self.OnUpdateCntDown,1,true)
end

function WWJContinueFormClass:vOnUninitialize()

end

function WWJContinueFormClass:UpdateUI(id,argument)
    if id == "OnArcadeRockerInput" then
        if argument == ArcadeInput.RockerState.RockerMoveForward then
            if not self._uiContinueToggle.isOn then
                self:DoSelectBtn("Continue")
            end
        elseif argument == ArcadeInput.RockerState.RockerMoveBack then
            if not self._uiBackToggle.isOn then
                self:DoSelectBtn("Back")
            end
        end
    elseif id == "OnArcadeOkBtnClick" then
        if self._uiContinueToggle.isOn then
            if self._handlerObj and self._onContinueBtnHandler then
                return self._onContinueBtnHandler(self._handlerObj)
            end
        else
            if self._handlerObj and self._onBackBtnHandler then
                return self._onBackBtnHandler(self._handlerObj)
            end
        end
    end
end

function WWJContinueFormClass:DoCreate(handler,continueBtnHandler,backBtnHandler)
    self._handlerObj = handler
    self._onContinueBtnHandler = continueBtnHandler
    self._onBackBtnHandler = backBtnHandler
    self:Create(nil,nil,0)
end

function WWJContinueFormClass:DoDestroy()
    ScheduleService:RemoveTimer(self)
    self:Destroy()
end

function WWJContinueFormClass:DoSelectBtn(name)
    if name == "Continue" then
        self._uiContinueToggle.isOn = true
        self._uiContinueToggle.graphic.gameObject:SetActive(true)
        self._uiBackToggle.graphic.gameObject:SetActive(false)
    elseif name == "Back" then
        self._uiBackToggle.isOn = true
        self._uiBackToggle.graphic.gameObject:SetActive(true)
        self._uiContinueToggle.graphic.gameObject:SetActive(false)
    end 
end

function WWJContinueFormClass:OnUpdateCntDown()
    self._waitSeconds = self._waitSeconds - 1
    if self._waitSeconds < 0 then
        ScheduleService:RemoveTimer(self)
        if self._handlerObj and self._onBackBtnHandler then
            return self._onBackBtnHandler(self._handlerObj)
        end
    else
        self._uiCntDownText.text = self._waitSeconds
    end
end
