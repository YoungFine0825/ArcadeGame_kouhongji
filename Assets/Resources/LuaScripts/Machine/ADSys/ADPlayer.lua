--[[********************************************************************
*		作者： yangfan
*		时间： 2018-11-20
*		描述： 广告播放器 负责播放一个 广告列表
*********************************************************************--]]

local MediaVideoEvent =
{
    MetaDataReady   = 0,
    ReadyToPlay     = 1,
    Started         = 2,
    FirstFrameReady = 3,
    FinishedPlaying = 4,
    Closing         = 5,
    Error           = 6,
    SubtitleChange  = 7,
    Stalled         = 8,
    Unstalled       = 9,
}

local MediaVideoErrorCode=
{
    None = 0,
    LoadFailed = 100,
    DecodeFailed = 200,
}


local ADPlayerClass = DeclareClass("ADPlayerClass")

function ADPlayerClass:ctor()
    --播放器预制件
    self._asset = false
    
    --VideoPlayer组件
    self._mediaPlayerGo=false
    self._mediaPlayerCom = false
    --用于播放图片广告的http image组件
    self._imagePalyerGo=false
    self._httpImageCom = false

    self._cachedRootGo=false

    --当前播放的广告列表
    self._curPlayAdList = false
    --当前播放的单个广告
    self._curAdInfoIndex = 0
    --
    --处理
    self._handler=false

end

function ADPlayerClass:Initialize()
    --加载播放器预制体
    local asset=AssetService:LoadInstantiateAsset("Fixed/ADPlayer/ADPlayer")
    if not asset then
        LogE("Load ADPlayer Prefab Error")
        return
    end
    self._asset=asset
    --
    self._cachedRootGo=self._asset.RootGo
    CS.UnityEngine.Object.DontDestroyOnLoad(self._cachedRootGo)
    --获取播放相关组件
    local plink=self._asset.RootPLink
    self._mediaPlayerCom = plink:GetCacheComponent(0)
    self._mediaPlayerGo =plink:GetCacheGameObject(0)

    self._imagePalyerGo=plink:GetCacheGameObject(1)
    self._httpImageCom = plink:GetCacheComponent(2)
    --
    self._asset.RootGo:ExtSetActive(false)

end

function ADPlayerClass:UnInitialize()
   
    --销毁预制件
    if self._asset then
        AssetService:Unload(self._asset)
        self._asset=false
    end
    self._mediaPlayerCom = false
    self._httpImageCom = false
    self._imagePalyerGo=false
    self._curPlayAdList = false
    self._curAdInfoIndex = 0
    self._cachedRootGo=false
    self._mediaPlayerGo=false
    --
    ScheduleService:RemoveTimer(self)

end

--播放
function ADPlayerClass:Play(adList,obj,func)

    if not adList or adList:GetAdCount()==0 then
        LogE("Play AdvertisementsList Failed！List Data Is Invalid！")
        if obj and func then
            func(obj)
        end
        return
    end 
    --
    if not self._curPlayAdList then
        self._curPlayAdList = adList
        self._curAdInfoIndex=0
    end
    --
    self._handler=Pool:CreateTable()
    self._handler.obj=obj
    self._handler.func=func
    --
    self._cachedRootGo:ExtSetActive(true)
    --
    self:DoPlay()
end

--关闭
function ADPlayerClass:Close(  )
    ScheduleService:RemoveTimer(self)
    --情况广告列表
    self._curPlayAdList = false
    --让背景音乐淡出
    AudioService:FadeOut(AudioChannelType.ACT_VOICE)
    --
    if self._mediaPlayerCom then
        self._mediaPlayerCom:CloseVideo()
    end
    
    if self._cachedRootGo then
        self._cachedRootGo:ExtSetActive(false)
    end
    if self._handler then
        self._handler = false
    end
end


--[[
    @desc: 播放列表
    @return:
]]
function ADPlayerClass:DoPlay()
    
    self._curAdInfoIndex=self._curAdInfoIndex+1
    --
    if self._curAdInfoIndex>self._curPlayAdList:GetAdCount() then
        --列表播放完
        if self._handler then
            self._curAdInfoIndex = 0
            self._curPlayAdList = false
            --播放结束
            self._handler.func(self._handler.obj)
        end
        return
    end
    --
    local playAdInfo=self._curPlayAdList:GetAdInfo(self._curAdInfoIndex)
    if not playAdInfo then
        --播放下一条
        return self:DoPlay()
    end
    --
    if playAdInfo.Type==ADType.Video then
        self:DoPlayVideoAd(playAdInfo)
    elseif playAdInfo.Type==ADType.Image then
        self:DoPlayImageAd(playAdInfo)
    else
        LogE("未知广告类型："..playAdInfo.Type)
        --播放下一条
        return self:DoPlay()
    end
end


function ADPlayerClass:DoPlayVideoAd(adInfo)
    if not self._mediaPlayerCom then
        LogE("MedidPlayer Component Is Invalid！")
        return self:DoPlay()
    end

    if self._imagePalyerGo then
        self._imagePalyerGo:ExtSetActive(false)
    end
    --
    AudioService:FadeOut(AudioChannelType.ACT_VOICE)
    --
    if self._mediaPlayerGo then
        self._mediaPlayerGo:ExtSetActive(true)
    end
    ---[[
    local rt = self._mediaPlayerCom:OpenVideoFromNetAsset(adInfo.Url,function(eventType,errorCode)
            if eventType==MediaVideoEvent.FinishedPlaying then
                LogD("播放视频完毕！")
                self:DoPlay() 
            elseif eventType == MediaVideoEvent.Error then
                LogE("Play Video Error！Errcode :"..errorCode)
                self:DoPlay()
            end
         end
        )--]]
    if not rt then
        return self:DoPlay()
    end
end

function ADPlayerClass.OnPlayVideoFinished(eventType,errorCode)
    if eventType==MediaVideoEvent.FinishedPlaying then
        LogD("播放视频完毕！")
        self:DoPlay() 
    elseif eventType == MediaVideoEvent.Error then
        LogE("Play Video Error！Errcode :"..errorCode)
        self:DoPlay()
    end
end


function ADPlayerClass:DoPlayImageAd(adInfo)
    if not self._httpImageCom then
        LogE("ImagePlayer Component Is Invaild！")
        return self:DoPlay()
    end

    if self._imagePalyerGo then
        self._imagePalyerGo:ExtSetActive(true)
    end
    if self._mediaPlayerGo then
        self._mediaPlayerGo:ExtSetActive(false)
    end
    --
    self._httpImageCom:SetImageUrl(adInfo.Url)
    --音效
    if not string.IsNullOrEmpty(adInfo.AudioUrl) then
        AudioService:PlayAudioWithNetAsset(adInfo.AudioUrl,false,AudioChannelType.ACT_VOICE)
    end
    --
    ScheduleService:RemoveTimer(self,self.OnImageAdOver)
    ScheduleService:AddTimer(self,self.OnImageAdOver,adInfo.ExistTime,false)
end
--
function ADPlayerClass:OnImageAdOver()
    self:DoPlay() 
end
