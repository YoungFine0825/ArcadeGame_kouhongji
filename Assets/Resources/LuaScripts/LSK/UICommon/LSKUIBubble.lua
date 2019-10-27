--[[********************************************************************
*		作者： yangfan
*		时间： 2018-11-16
*		描述： 口红机UIBubble
*********************************************************************--]]

local LSKUIBubbleClass = DeclareClass("LSKUIBubbleClass", ClassLib.UIFormClass)
function LSKUIBubbleClass:ctor()
	self._uiInfoTextIndex = 0

	self._uiInfoText = false
    self._delegate = false
end

function LSKUIBubbleClass:vGetPath()
	return 'LSK/UICommon/LSKUIBubble'
end

function LSKUIBubbleClass:vOnResourceLoaded()
	self._uiInfoText = self:GetComponent(self._uiInfoTextIndex)

end

function LSKUIBubbleClass:vOnResourceUnLoaded()
	self._uiInfoText = false
end

function LSKUIBubbleClass:vOnInitialize(argument)
    self._delegate = argument or false
end

function LSKUIBubbleClass:vOnUninitialize()
    ScheduleService:RemoveTimer(self)
end

function LSKUIBubbleClass:InitShow(content)
    if(self._uiInfoText) then
        self._uiInfoText.text = content
    end
    --开始倒计时
    ScheduleService:AddTimer(self,self.OnClose,2,false)
end 

function LSKUIBubbleClass:OnClose()
    if self._delegate then
        self._delegate:OnBubbleDestroy(self)
        self._delegate = false
    end
    if self._form then
        self:Destroy()
    end
end