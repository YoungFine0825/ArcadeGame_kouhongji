--[[********************************************************************
*		作者： yangfan
*		时间： 2018-11-16
*		描述： 口红机等待UI弹窗
*********************************************************************--]]
local LSKUIWaitingClass = DeclareClass("LSKUIWaitingClass", ClassLib.UIFormClass)
function LSKUIWaitingClass:ctor()
    self._uiTipTextIndex = 0;
    self._uiAnimAnimationIndex = 1;
    
    self._uiTipText = false
    self._uiAnimAnimation = false    
end

function LSKUIWaitingClass:vGetPath()
	return 'LSK/UICommon/LSKUIWaiting'
end

function LSKUIWaitingClass:vOnResourceLoaded()
    self._uiTipText = self:GetComponent(self._uiTipTextIndex)
    self._uiAnimAnimation = self:GetComponent(self._uiAnimAnimationIndex)
    
end

function LSKUIWaitingClass:vOnResourceUnLoaded()
    self._uiTipText = false
    self._uiAnimAnimation = false
    
end

function LSKUIWaitingClass:Enter()
    self:ActiveForm(true)
    ScheduleService:RemoveTimer(self)
    if self._uiAnimAnimation then
        self._uiAnimAnimation:Play('Waiting_Once')
        ScheduleService:AddTimer(self,self.OnPlayLoopAnimation,1.5,false)
    end
end

function LSKUIWaitingClass:Leave()
    ScheduleService:RemoveTimer(self)
    self:ActiveForm(false)
end

function LSKUIWaitingClass:vOnInitialize(argument)
    if(self._uiTipText) then
        self._uiTipText.gameObject:SetActive(false)
    end
end

function LSKUIWaitingClass:OnPlayLoopAnimation()
    ScheduleService:RemoveTimer(self,self.OnPlayLoopAnimation)
    if self._uiAnimAnimation then
        self._uiAnimAnimation:Play('Waiting_Loop')
    end
end

function LSKUIWaitingClass:vOnUninitialize()
    self:Leave()
end

--显示文本内容
function LSKUIWaitingClass:ShowTip(tip)
    if(not string.IsNullOrEmpty(tip)) then
        if self._uiTipText then
            self._uiTipText.text = tip
            self._uiTipText.gameObject:SetActive(true)
        end
    else
        if(self._uiTipText) then
            self._uiTipText.gameObject:SetActive(false)
        end
    end
end