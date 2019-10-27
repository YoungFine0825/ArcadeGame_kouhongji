--[[********************************************************************
*		作者： XH
*		时间： 2018-12-11
*		描述： 退出确认界面
*********************************************************************--]]

local LSKQuitConfirmFormClass = DeclareClass("LSKQuitConfirmFormClass", ClassLib.UIFormClass)
function LSKQuitConfirmFormClass:ctor()
	self._uiConfirmSelectRectTransformIndex = 0;
	self._uiCancelSelectRectTransformIndex = 1;
	self._uiAnimAnimationIndex = 2;
	
	self._uiConfirmSelectRectTransform = false
	self._uiCancelSelectRectTransform = false
	self._uiAnimAnimation = false
	
	self._confirmGo = false
	self._cancelGo = false
end

function LSKQuitConfirmFormClass:vGetPath()
	return 'LSK/Select/LSKQuitConfirmForm'
end

function LSKQuitConfirmFormClass:vOnResourceLoaded()
	self._uiConfirmSelectRectTransform = self:GetComponent(self._uiConfirmSelectRectTransformIndex)
	self._uiCancelSelectRectTransform = self:GetComponent(self._uiCancelSelectRectTransformIndex)
	self._uiAnimAnimation = self:GetComponent(self._uiAnimAnimationIndex)
	
	self._confirmGo = self:GetGameObject(self._uiConfirmSelectRectTransformIndex)
	self._cancelGo = self:GetGameObject(self._uiCancelSelectRectTransformIndex)

	self._confirmGo:SetActive(false)
	self._cancelGo:SetActive(false)
end

function LSKQuitConfirmFormClass:vOnResourceUnLoaded()
	self._uiConfirmSelectRectTransform = false
	self._uiCancelSelectRectTransform = false
	self._uiAnimAnimation = false
	
	self._confirmGo = false
	self._cancelGo = false
end

function LSKQuitConfirmFormClass:OnSelect(isConfirm)
	if self._confirmGo then
		self._confirmGo:SetActive(isConfirm)
	end

	if self._cancelGo then
		self._cancelGo:SetActive(not isConfirm)
	end
end
function LSKQuitConfirmFormClass:vOnInitialize(argument)
	if self._uiAnimAnimation then
		self._uiAnimAnimation:Play('Anim_Open')
	end
end

function LSKQuitConfirmFormClass:vOnUninitialize()

end

function LSKQuitConfirmFormClass:Close()
	if self._uiAnimAnimation then
		self._uiAnimAnimation:Play('Anim_Close')
	end
	ScheduleService:AddTimer(self,self.OnDelayClose,0.4,false)
end

function LSKQuitConfirmFormClass:OnDelayClose()
	self:Destroy()
end