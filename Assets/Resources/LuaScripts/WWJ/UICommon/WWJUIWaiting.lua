--[[********************************************************************
*		作者： yangfan
*		时间： 2018-12-4
*		描述： 娃娃机等待UI弹窗
*********************************************************************--]]
local WWJUIWaitingClass = DeclareClass("WWJUIWaitingClass", ClassLib.UIFormClass)
function WWJUIWaitingClass:ctor()
	self._uiTipTextIndex = 0;

	self._uiTipText = false

end

function WWJUIWaitingClass:vGetPath()
	return 'WWJ/UICommon/WWJUIWaiting'
end

function WWJUIWaitingClass:vOnResourceLoaded()
	self._uiTipText = self:GetComponent(self._uiTipTextIndex)

end

function WWJUIWaitingClass:vOnResourceUnLoaded()
	self._uiTipText = false

end

function WWJUIWaitingClass:vOnInitialize(argument)
    if(self._uiTipText) then
        self._uiTipText.gameObject:SetActive(false)
    end
end

function WWJUIWaitingClass:vOnUninitialize()

end

--显示文本内容
function WWJUIWaitingClass:ShowTip(tip)
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

