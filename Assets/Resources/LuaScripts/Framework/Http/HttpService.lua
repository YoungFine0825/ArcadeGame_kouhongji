--[[********************************************************************
*      作者： jordenwu
*      时间： 11/22/17 18:05:41
*      描述： Http请求对接
*********************************************************************--]]
local HttpServiceClass = DeclareClass("HttpServiceClass")

function HttpServiceClass:ctor()
    
    self._csService=Interaction.HttpService
    
end


-- 异步获取文本
-- @param url 
-- @param handler System.Action<string>
-- @return 无
function HttpServiceClass:AsyncGetText(url,handler)
    
    if not self._csService then
        return 
    end
    self._csService:AsyncGetText(url,handler)
    
end

function HttpServiceClass:SyncGetText(url)
   
    if not self._csService then
        return "error"
    end
    return self._csService:SyncGetText(url)
end

function HttpServiceClass:AsyncGetTextByPostUrl(url, postData, callback)
    if not self._csService then
        return
    end
    return self._csService:AsyncGetTextByPostUrl(url, postData, callback)
end

function HttpServiceClass:RegisteFileHttpDownload(url,filePath,callback)
    if not self._csService then
        return
    end
    return self._csService:RegisteFileHttpDownload(url,filePath,callback)
end

HttpService=HttpServiceClass.new()
