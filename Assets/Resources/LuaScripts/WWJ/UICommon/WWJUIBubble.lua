--[[********************************************************************
*		作者： yangfan
*		时间： 2018-12-4
*		描述： 娃娃机UIBubble
*********************************************************************--]]

local WWJUIBubbleClass = DeclareClass("WWJUIBubbleClass", ClassLib.UIFormClass)
function WWJUIBubbleClass:ctor()
	self._uiInfoTextIndex = 0

	self._uiInfoText = false

end

function WWJUIBubbleClass:vGetPath()
	return 'WWJ/UICommon/WWJUIBubble'
end

function WWJUIBubbleClass:vOnResourceLoaded()
	self._uiInfoText = self:GetComponent(self._uiInfoTextIndex)

end

function WWJUIBubbleClass:vOnResourceUnLoaded()
	self._uiInfoText = false
end

function WWJUIBubbleClass:vOnInitialize(argument)

end

function WWJUIBubbleClass:vOnUninitialize()

end

function WWJUIBubbleClass:InitShow(content)
    if(self._uiInfoText) then
        self._uiInfoText.text = content
    end
    --开始倒计时
    ScheduleService:AddTimer(self,self.Destroy,2.5,false)
end 

