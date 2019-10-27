--[[********************************************************************
*      作者： jordenwu
*      时间： 10/11/17 16:01:20
*      描述： 对接游戏的声音服务
*********************************************************************--]]

local AudioServiceClass = DeclareClass("AudioServiceClass")

function AudioServiceClass:ctor()
    self._audioService = Interaction.AudioService
    
end

-- 播放
-- @param channel 对应通道
-- @param resName 资源名称
-- @param 是否循环
-- @return 无
function AudioServiceClass:Play(channel,resName,loop)
    
    local isLoop=loop or false
    if not self._audioService then
        return
    end
    self._audioService:Play(channel,resName,isLoop,false)
    
end

--播放网络资产 背景音乐
function AudioServiceClass:PlayAudioWithNetAsset(url,loop,channelType)
    
    local isLoop=loop or false
    if not self._audioService then
        return
    end
    self._audioService:Play(channelType,url,isLoop,true)
end

--
function AudioServiceClass:FadeOut(channelType)
    if not self._audioService then
        return
    end
    self._audioService:FadeOut(channelType)
end

function AudioServiceClass:FadeIn(channelType)
    if not self._audioService then
        return
    end
    self._audioService:FadeOut(channelType)
end

-- 停止所有
-- @return 无
function AudioServiceClass:StopAll()
    
    if not self._audioService then
        return
    end
    self._audioService:StopAll()
    
end

--关闭某个通道所有的音效
function AudioServiceClass:StopOneChannel(channel)
    if not self._audioService then
        return
    end
    self._audioService:Stop(channel)
end

-- 关闭所有
-- @return 无
function AudioServiceClass:CloseAll()
    
    if not self._audioService then
        return
    end
    self._audioService:CloseAll()
    
end

-- 打开所有
-- @return 无
function AudioServiceClass:OpenAll()
    
    if not self._audioService then
        return
    end
    self._audioService:OpenAll()
    
end


-- 获取通道音量
-- @param channel 
-- @return 音量
function AudioServiceClass:GetVolumeByChannel(channel)
    
    if not self._audioService then
        return 0
    end
    return self._audioService:GetVolumeByChannel(channel)
    
end

-- 设置通道音量
-- @param channel 
-- @param volumn 
-- @return 无
function AudioServiceClass:SetVolumeByChannel(channel,volumn)
    
    if not self._audioService then
        return
    end
    self._audioService:SetChannelVolume(channel,volumn)
    
end


--全局
AudioService = AudioServiceClass.new()