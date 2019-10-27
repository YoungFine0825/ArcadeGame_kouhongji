--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-15
*		描述： 子游戏状态
*********************************************************************--]]
---子游戏资源状态文本定义
local SubGameIFSState2Txt = {
    --开始
    [IFSState.Start] = "开始准备游戏...",
    --首次zip包移动状态
    [IFSState.FirstMoveInit] = "解压游戏资源包...",
    [IFSState.FirstMoving] = "解压游戏资源包...",
    [IFSState.FirstMoveSuccess] = "解压游戏成功...",
    [IFSState.FirstMoveFailed] = "解压游戏资源失败...",
    --首次zip包网络下载状态
    [IFSState.FirstDownloadInit] = "准备下载游戏资源包...",
    [IFSState.FirstDownloading] = "下载游戏资源包...",
    [IFSState.FirstDownloadSuccess] = "下载游戏资源包成功",
    [IFSState.FirstDownloadFailed] = "下载游戏资源包失败",
    --首次解压
    [IFSState.FirstUnZip] = "解压游戏资源包...",
    --本地文件列表初始化
    [IFSState.LocalFileListInit] = "游戏资源包检查...",
    --本地文件完整性检查
    [IFSState.LocalFileListCheck] = "游戏资源包检查...",
    --网络文件列表下载
    [IFSState.NetFileListDownload] = "检查游戏更新...",
    --Diff文件列表
    [IFSState.LocalDiffNetFileList] = "检查游戏更新...",
    --下载差异开始
    [IFSState.DownloadDiffFileListBegin] = "下载游戏更新...",
    --Diff下载中
    [IFSState.DownloadingDiff] = "下载游戏更新...",
    --Diff下载完成
    [IFSState.DownloadDiffSuccess] = "下载游戏完成...",
    --生成最新文件列表
    [IFSState.GenerateLastFileList] = "下载游戏完成...",
    --结束
    [IFSState.Over] = "结束"
}

--子游戏类型
local SubGameType = {
    LSK = 1,
    WWJ = 2
}

--子游戏
local SubGameTypeKey = {
    [SubGameType.LSK] = "LSK",
    [SubGameType.WWJ] = "WWJ"
}

--
local SubGameStateClass = DeclareClass("SubGameStateClass")

function SubGameStateClass:ctor()
    --
    self._gameBoot = false
    --
    self._launchEventArg = false
    --
    self._curSubGameInfo = false
end

function SubGameStateClass:vInitializeState()
end

function SubGameStateClass:vUninitializeState()
    if self._gameBoot then
        self._gameBoot:Over()
        self._gameBoot = false
    end
end

function SubGameStateClass:vGetName()
    return "SubGameState"
end

function SubGameStateClass:vGetIsCanKill()
    
    if self._gameBoot then
        return self._gameBoot:IsCanKill()
    end
    return false

end


function SubGameStateClass:vOnStateEnter(param, oldSt)
    --
    if not param then
        LogE("SubGame Sate Enter Error param")
        return
    end

    local isok = self:InitLaunchSubGameInfo(param)
    --
    if not isok then
        LogE("SubGame Sate Enter Init SubGameInfo Error")
        return
    end
    --开发模式直接开始 Global.IsEditor and (not Global.IsUsePackRes)
    if  Global.IsEditor and (not Global.IsUsePackRes) then
        --
        self:LaunchSubGame()
    else
        --更新子游戏资源
        self:UpdateSubGameRes(true)
    end
end

--[[
    @desc: 初始化当前玩的子游戏信息
    --@param: CSRspMLogin
    @return:
]]
function SubGameStateClass:InitLaunchSubGameInfo(param)
    if not param then
        return false
    end
    --测试WWJ
    local kk = SubGameTypeKey[param.game_type]
    --local kk = SubGameTypeKey[SubGameType.WWJ]
    if not kk then
        LogE("No Define SubGameType:" .. tostring(param.game_type))
        return false
    end
    --
    self._curSubGameInfo = {}
    self._curSubGameInfo.GameType = param.game_type
    self._curSubGameInfo.GameHost = param.game_host
    self._curSubGameInfo.GamePort = param.game_port
    --
    self._curSubGameInfo.GameKey = SubGameTypeKey[param.game_type]
    self._curSubGameInfo.FirstZipName = kk .. "Zip.bytes"
    self._curSubGameInfo.FileListFileName = kk .. "FileList.json"
    --
    local urlRoot = ""
    if Global.IsDebug then
        urlRoot = "https://gamehalldownloadcdn.scbczx.com/Arcade/IFS/Debug/"
    else
        urlRoot = "https://gamehalldownloadcdn.scbczx.com/Arcade/IFS/Release/"
    end
    self._curSubGameInfo.FirstZipUrl = urlRoot .. self._curSubGameInfo.FirstZipName
    self._curSubGameInfo.FileListUrl = urlRoot .. self._curSubGameInfo.FileListFileName
    self._curSubGameInfo.NetFileRootUrl = urlRoot
    --
    self._curSubGameInfo.FirstReqLuaFile = "WWJ.WWJBoot"--kk .. "." .. kk .. "Boot"
    self._curSubGameInfo.LuaBootClass = "WWJBootClass"--kk .. "BootClass"
    self._curSubGameInfo.LuaZipFile = kk .. "_Lua"
    self._curSubGameInfo.ResPackConfigFileName=kk.."ResCfg.bytes"
    --
    LogDump(self._curSubGameInfo,"Launch Sub GameInfo")
    --
    return true
end

--启动子游戏
function SubGameStateClass:LaunchSubGame()
    
    --注入子游戏资源配置
    if Global.IsUsePackRes then
        ResService:RegisterResPackConfig(self._curSubGameInfo.ResPackConfigFileName)
    end
    --注入LuaZip
    if Global.IsUseLuaPack then
        Interaction.LuaInteraction.LoadLuaZip(self._curSubGameInfo.LuaZipFile)
    end

    --切换场景
    SceneService:LoadUnitySceneAsync(
        "SubGame",
        0,
        function(pro, isDone)
            if isDone then
                ArcadeInputService:ForbidInput(false)
                --
                MVCService:ChangeUIState("UIState_SubGameEmpty")
                --
                local reqfile = self._curSubGameInfo.FirstReqLuaFile
                local bootClassName = self._curSubGameInfo.LuaBootClass
                require(reqfile)
                self._gameBoot = ClassLib[bootClassName].new()
                self._gameBoot:Load()
                self._gameBoot:Start(self._curSubGameInfo, MachineData.Uid, MachineData.Token)
                --TODO 延迟关闭
                if Global.IsUseLuaPack then
                     Interaction.LuaInteraction.UnLoadLuaZip(self._curSubGameInfo.LuaZipFile)
                end
            end
        end
    )
end

--更新
function SubGameStateClass:UpdateSubGameRes(isFirst)
    --开始子游戏资源更新
    if isFirst then
        MVCService:ChangeUIState("UIState_SubGameLaunch")
    end
    self._launchEventArg = {}
    --
    local sessionName = self._curSubGameInfo.GameKey
    local firstZipName = self._curSubGameInfo.FirstZipName
    local fileListFileName = self._curSubGameInfo.FileListFileName
    local firstZipUrl = self._curSubGameInfo.FirstZipUrl
    local fileListUrl = self._curSubGameInfo.FileListUrl
    local netFileRootUrl = self._curSubGameInfo.NetFileRootUrl
    --
    IFSService:BeginSession(
        sessionName,
        firstZipName,
        fileListFileName,
        firstZipUrl,
        fileListUrl,
        netFileRootUrl,
        function(sessionName, state, progress)
            self:OnIFSSessionCallBack(sessionName, state, progress)
        end
    )
    --
end

--
function SubGameStateClass:OnIFSSessionCallBack(sessionName, state, progress)
    if state ~= IFSState.Over then
        --TO UI
        self._launchEventArg.StateInfo = SubGameIFSState2Txt[state]
        self._launchEventArg.ProgressValue = progress
        EventService:SendEvent(LuaEventID.SubGameLaunchEvent, self._launchEventArg)
        return
    end
    --
    if state == IFSState.Over then
        --检查
        self._launchEventArg.StateInfo = "开始游戏资源完整性检查"
        self._launchEventArg.ProgressValue = 1.0
        EventService:SendEvent(LuaEventID.SubGameLaunchEvent, self._launchEventArg)
        local fileListFileName = self._curSubGameInfo.FileListFileName
        --
        IFSService:BeginLocalChecker(
            sessionName,
            fileListFileName,
            function(name, result)
                self:OnSubGameLocalResCheck(name, result.IsSuccess, result.LocalIsFull)
                --
            end
        )
    end
end

--资源完整性检查回调
function SubGameStateClass:OnSubGameLocalResCheck(name, success, isfull)
    --开始子游戏
    if success and isfull then
        --
        self._launchEventArg = false
        --开始
        self:LaunchSubGame()
        --
    else
        --
        self._launchEventArg.StateInfo = "游戏检查失败，即将重新更新资源"
        self._launchEventArg.ProgressValue = 1.0
        EventService:SendEvent(LuaEventID.SubGameLaunchEvent, self._launchEventArg)
        --
        ScheduleService:AddTimer(self,self.Delay2ReUpdate,2,false)
    end

end

function SubGameStateClass:Delay2ReUpdate()
    
    self:UpdateSubGameRes(false)

end


--状态离开
function SubGameStateClass:vOnStateLeave(param)
    --
    ScheduleService:CleanAll()
    --
    if self._gameBoot then
        self._gameBoot:Over()
        self._gameBoot = false
    end

    if self._curSubGameInfo then
        if Global.IsUseLuaPack then
            Interaction.LuaInteraction.UnLoadLuaZip(self._curSubGameInfo.LuaZipFile)
        end
        self._curSubGameInfo=false
    end
    
    ArcadeInputService:UnRegisterOkInput()
    ArcadeInputService:UnRegisterRefreshInput()
    ArcadeInputService:UnRegisterRockerInput()
    ArcadeInputService:UnRegisterRotateInput()
    --
    GCService:CollectGarbage(true)
    --
end
