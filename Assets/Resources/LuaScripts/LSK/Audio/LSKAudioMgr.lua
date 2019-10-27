
LSKAudioID = 
{
    Throw = 'Throw',
    HitWheel = 'HitWheel',
    HitLsk = 'HitLSK',
    Win = 'Win',
    Lose = 'Lose',
    Boom = 'Boom',
}

LSKPickVoiceMin = 15
LSKPickVoiceMax = 40

local CurIdx = 0
local LSKAudioMgrClass = DeclareClass("LSKAudioMgrClass")

local LSKAudioBGM = {
    [1] = {name='Music_1',duration=181},
    [2] = {name='Music_2',duration=200},
    [3] = {name='Music_3',duration=231},
    [4] = {name='Music_4',duration=167},
    [5] = {name='Music_5',duration=214},
    [6] = {name='Music_6',duration=232},
}

--
local LSKHitSounds =
{
    [1] = 'Audio_LipstickImpact',
}

local LSKWin = 
{
    [1] = 'Audio_win',
}
local LSKPinSuccSounds =
{
    [1] = 'Audio_Hit1',
    [2] = 'Audio_Hit2',
    [3] = 'Audio_Hit3',
}

local LSKBoomSounds =
{
    [1] = 'Audio_Sucess5',
}

local LSKThrowSounds = 
{
    [1] = 'Audio_Throw1',
    [2] = 'Audio_Throw2',
    [3] = 'Audio_Throw3',
}

function LSKAudioMgrClass:ctor()
    self._update_interval = 1
    self._music_left_seconds = 0
    self._working = false
end

function LSKAudioMgrClass:Init()
    ScheduleService:AddTimer(self,self.OnTimerUpdate,self._update_interval,true)
    self:PlayMusic()
end

function LSKAudioMgrClass:Uninit()
    ScheduleService:RemoveTimer(self,self.OnTimerUpdate)
end

function LSKAudioMgrClass:PlaySound(id)
    local sounds = self:GetSounds(id)
    if not sounds then
        return
    end
    
    local idx = #sounds == 1 and 1 or LSKUtil:GetRandom(1, #sounds)
    local name = sounds[idx]
    
    AudioService:Play(AudioChannelType.ACT_UI, 'LSK/Audio/'..name, false)
end

function LSKAudioMgrClass:StopSound()
    AudioService:StopOneChannel(AudioChannelType.ACT_UI)
end

function LSKAudioMgrClass:StopAll()
    AudioService:StopAll()
end

function LSKAudioMgrClass:PlayMusic()
    self._working = true
    local idx = #LSKAudioBGM == 1 and 1 or LSKUtil:GetRandom(1, #LSKAudioBGM)
    self._music_left_seconds = LSKAudioBGM[idx].duration
    local name = LSKAudioBGM[idx].name

    AudioService:Play(AudioChannelType.ACT_BGM, 'LSK/Audio/'..name, true)
end

function LSKAudioMgrClass:StopMusic()
    AudioService:StopOneChannel(AudioChannelType.ACT_BGM)
    self._working = false
end

function LSKAudioMgrClass:SetMusicVolume(value)
    AudioService:SetVolumeByChannel(AudioChannelType.ACT_BGM, value)
end

function LSKAudioMgrClass:SetSoundVolume(value)
    AudioService:SetVolumeByChannel(AudioChannelType.ACT_UI, value)
end

function LSKAudioMgrClass:OnTimerUpdate()
    if not self._working then
        return
    end
    
    self._music_left_seconds = self._music_left_seconds - self._update_interval

    if self._music_left_seconds <= 0 then
        self:PlayMusic()
    end
end


function LSKAudioMgrClass:GetSounds(id)
    if id == LSKAudioID.Throw then
        return LSKThrowSounds
    elseif id == LSKAudioID.HitLsk then
        return LSKHitSounds
    elseif id == LSKAudioID.HitWheel then
        return LSKPinSuccSounds
    elseif id == LSKAudioID.Win then
        return LSKWin
    elseif id == LSKAudioID.Boom then
        return LSKBoomSounds
    end
end