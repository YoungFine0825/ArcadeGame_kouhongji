local LSKReadyStartFormClass = DeclareClass("LSKReadyStartFormClass", ClassLib.UIFormClass)
function LSKReadyStartFormClass:ctor()
	self._ui1LevelGoIndex = 0;
	self._ui2LevelGoIndex = 1;
	self._ui3LevelGoIndex = 2;

	self._ui1LevelGo = false
	self._ui2LevelGo = false
	self._ui3LevelGo = false

end

function LSKReadyStartFormClass:vGetPath()
	return 'LSK/Play/LSKReadyStartForm'
end

function LSKReadyStartFormClass:vOnResourceLoaded()
	self._ui1LevelGo = self:GetGameObject(self._ui1LevelGoIndex)
	self._ui2LevelGo = self:GetGameObject(self._ui2LevelGoIndex)
	self._ui3LevelGo = self:GetGameObject(self._ui3LevelGoIndex)
end

function LSKReadyStartFormClass:vOnResourceUnLoaded()
	self._ui1LevelGo = false
	self._ui2LevelGo = false
	self._ui3LevelGo = false
end

function LSKReadyStartFormClass:vOnInitialize(argument)
	if not argument then
		return
	end
    AudioService:Play(AudioChannelType.ACT_UI, 'LSK/Audio/Audio_321countdown')
	self._ui1LevelGo:SetActive(false)
	self._ui2LevelGo:SetActive(false)
	self._ui3LevelGo:SetActive(false)
	if argument == 1 then
		self._ui1LevelGo:SetActive(true)
	elseif argument == 2 then
		self._ui2LevelGo:SetActive(true)
	elseif argument == 3 then
		self._ui3LevelGo:SetActive(true)
	end
end

function LSKReadyStartFormClass:vOnUninitialize()

end