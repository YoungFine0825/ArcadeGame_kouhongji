--[[********************************************************************
*      作者： jordenwu
*      时间： 10/18/17 14:30:14
*      描述： UI的公共服务 负责弹出bubble msgbox loading菊花
*********************************************************************--]]
local UICommonServiceClass = DeclareClass("UICommonServiceClass")

function UICommonServiceClass:ctor()
    
    self._csService=Interaction.UICommonService
    
end

function UICommonServiceClass:Uninitialize()
   
   self._csService=false
    
end

--显示消息弹窗
--handler =fuction(boxId,result)
function UICommonServiceClass:ShowMsgBox(style,handler,content,title)
    
    if not self._csService then
        return
    end
    return self._csService:ShowMsgBoxLua(style,handler,content,title,"","")
    
end

--显示消息弹窗
--handler =fuction(result)
function UICommonServiceClass:ShowMsgBox(style,handler,content,title,confirmStr,cancelStr)
    
    if not self._csService then
        return
    end
    return self._csService:ShowMsgBoxLua(style,handler,content,title,confirmStr,cancelStr)
    
end

--关闭消息弹窗
function UICommonServiceClass:CloseMsgBox()
    
    if not self._csService then
        return
    end
    self._csService:CloseMsgBox()
end


-- 清除菊花
-- @return 无
function UICommonServiceClass:CleanWaiting()
    
    if not self._csService then
        return
    end
    self._csService:CleanWaiting()
    
end

-- 展示菊花
-- @param key 
-- @param isShow 
-- @param msg 
-- @return 无
function UICommonServiceClass:ShowWaiting(key,isShow,msg)
    
    if not self._csService then
        return
    end
    if not key then
       LogE("ShowWaitingUI must have Key") 
       return
    end
    local showMsg=msg or ""
    local isMask=showMask or false
    local tip = msg or ""
    self._csService:ShowWaiting(key,isShow,tip)

end


-- 展示提示
-- @param tip 
-- @return 无
function UICommonServiceClass:ShowBubble(tip)
    
    if not self._csService then
        return
    end
    if not tip then
        LogE("ShowBubble invalid parameter") 
        return
    end
    self._csService:ShowBubble(tip)
end
--全局
UICommonService=UICommonServiceClass.new()
