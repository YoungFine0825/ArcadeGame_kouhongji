--[[********************************************************************
*		作者： yangfan
*		时间： 2018-11-16
*		描述： 口红机通用UI服务
*********************************************************************--]]
local LSKUICommonClass = DeclareClass("LSKUICommonClass")

function LSKUICommonClass:ctor()
    --等待弹窗对象
    self._waitingForm = false
    
    self._isShowWaiting=false
    
    self._bubbleList = {}
end

function LSKUICommonClass:Initialize( )
    -- body
end

function LSKUICommonClass:UnInitialize( )
    
    if self._waitingForm then
        self._waitingForm:Destroy()
        self._waitingForm = false
    end
    self._isShowWaiting = false

    -- 清除所有的气泡
    for i=#self._bubbleList, 1, -1 do
        if self._bubbleList[i] then
            self._bubbleList[i]:OnClose()
            table.remove(self._bubbleList,i)
        end
    end
end

--显示等待
function  LSKUICommonClass:ShowWaiting(isShow,tip)
    
    if isShow then

        if self._isShowWaiting then
            LogE("LSKUICommon ShowWaiting Time Sequence Error")
        end
        ArcadeInputService:ForbidInput(true)
        self._isShowWaiting=true
        if not self._waitingForm  then
            self._waitingForm = ClassLib.LSKUIWaitingClass.new()
            self._waitingForm:Create()
        end
        self._waitingForm:Enter()
        self._waitingForm:ShowTip(tip)

    else
        ArcadeInputService:ForbidInput(false)
        self._isShowWaiting=false
        self._waitingForm:Leave()
    end
end

function  LSKUICommonClass:CleanWaiting()
    
    ArcadeInputService:ForbidInput(false)
    self._isShowWaiting=false
    if self._waitingForm then
        self._waitingForm:ActiveForm(false)
    end
    
end

function LSKUICommonClass:ShowBubble(content)
    -- body
    local bb = ClassLib.LSKUIBubbleClass.new()
    bb:Create(nil,self)
    bb:InitShow(content)
    self._bubbleList[#self._bubbleList + 1] = bb
end

function LSKUICommonClass:OnBubbleDestroy(bubble)
    for i=#self._bubbleList, 1, -1 do
        if self._bubbleList[i] == bubble then
            table.remove(self._bubbleList,i)
        end
    end
end