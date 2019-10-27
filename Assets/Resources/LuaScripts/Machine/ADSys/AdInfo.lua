--[[********************************************************************
*		作者： yangfan
*		时间： 2018-11-21
*		描述： 单个广告信息
*********************************************************************--]]
--广告类型
ADType = 
{
    None = 0,--
    Video = 1,--视频广告
    Image = 2,--图片广告
}

local AdInfoClass = DeclareClass("AdInfoClass")
function AdInfoClass:ctor()
    
    --广告类型
    self.Type = 0
    --广告资源地址
    self.Url = ""
    
    --图片类型使用
    --停留时间
    self.ExistTime = 0 
    --附带的音频文件的地址
    self.AudioUrl = ""

end

--[[
    @desc: 初始化
    --@args:  {
        "adType": 1,
        "adUrl": "",
        "circleTime": 0,
        "voiceUrl": "",
        "playType": "循环播放",
        "sortNum": 3
      }
    @return:
]]
function AdInfoClass:InitInfo(args)
    
    if args then
        self.Type = args.adType
        self.Url = args.adUrl
        self.ExistTime = args.circleTime
        self.AudioUrl = args.voiceUrl
        return true
    end
    return false
end

