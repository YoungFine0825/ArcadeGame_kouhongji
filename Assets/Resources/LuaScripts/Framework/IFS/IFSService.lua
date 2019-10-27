--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-08-02
*		描述： IFS 文件系统对接
*********************************************************************--]]
local IFSServiceClass = DeclareClass("IFSServiceClass")
--
function IFSServiceClass:ctor()
    --
    self._csService=Interaction.IFSService

end

--[[
    @desc: 开始完整的检查加更新下载事务
    --@sessionName:
	--@firstZipName:
	--@fileListFileName:
	--@firstZipUrl:
	--@fileListFileUrl:
	--@netFileRootUrl:
	--@handler: 
    @return:
]]
function IFSServiceClass:BeginSession(sessionName,firstZipName,fileListFileName,firstZipUrl,fileListFileUrl,netFileRootUrl,handler)
    
    if not self._csService then
        return false
    end
    self._csService:BeginSessionLua(sessionName,firstZipName,fileListFileName,firstZipUrl,fileListFileUrl,netFileRootUrl,handler)
    
end

--[[
    @desc: 停止事务
    --@sessionName: 
    @return:
]]
function IFSServiceClass:StopSession(sessionName)
    
    if not self._csService then
        return 
    end
    self._csService:StopSession(sessionName)

end

--[[
    @desc: 检查是否有更新
    --@name:名称
	--@localFileList:本地文件列表
	--@netFileUrl:网络文件列表地址
	--@handler:  cs public delegate void IFSUpdateCheckerDelegate(string checkName, IFSUpdateCheckerResult result);
    @return:无
]]
function IFSServiceClass:BeginUpdateChecker(name,localFileList,netFileUrl,handler)
    if not self._csService then
        return 
    end
    self._csService:BeginUpdateChecker(name,localFileList,netFileUrl,handler)
end

--[[
    @desc: 停止更新检查
    --@name: 名字
    @return:
]]
function IFSServiceClass:StopUpdateChecker(name)
    
    if not self._csService then
        return 
    end
    self._csService:StopUpdateChecker(name)

end


--[[
    @desc: 检查本地是否完整
    --@name:名称
	--@localFileList:本地文件列表
	--@handler:  cs public delegate void IFSLocalCheckerDelegate(string checkName, IFSLocalCheckerResult result);
]]
function IFSServiceClass:BeginLocalChecker(name,localFileList,handler)
    if not self._csService then
        return 
    end
    self._csService:BeginLocalChecker(name,localFileList,handler)
end

--[[
    @desc: 停止本地检查
    --@name: 名字
]]
function IFSServiceClass:StopLocalChecker(name)
    
    if not self._csService then
        return 
    end
    self._csService:StopLocalChecker(name)

end


IFSService=IFSServiceClass.new()