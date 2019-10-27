local LSKLoseAnimFormClass = DeclareClass("LSKLoseAnimFormClass", ClassLib.UIFormClass)
function LSKLoseAnimFormClass:ctor()
	self._uiAnimAnimationIndex = 0;

	self._uiAnimAnimation = false

end

function LSKLoseAnimFormClass:vGetPath()
	return 'LSK/Play/LSKLoseAnimForm'
end

function LSKLoseAnimFormClass:vOnResourceLoaded()
	self._uiAnimAnimation = self:GetComponent(self._uiAnimAnimationIndex)

end

function LSKLoseAnimFormClass:vOnResourceUnLoaded()
	self._uiAnimAnimation = false

end

function LSKLoseAnimFormClass:vOnInitialize(argument)

end

function LSKLoseAnimFormClass:vOnUninitialize()

end

function LSKLoseAnimFormClass:vOnUpdateUI(id, arg)

end

function LSKLoseAnimFormClass:Close()

	ScheduleService:AddTimer(self,self.OnDelayClose,0.4,false)
end

function LSKLoseAnimFormClass:OnDelayClose()
	self:Destroy()
end