--[[********************************************************************
*		作者： yangfan
*		时间： 2018-12-4
*		描述： 娃娃机通用UI服务
*********************************************************************--]]
local WWJUICommonClass = DeclareClass("WWJUICommonClass")

function WWJUICommonClass:ctor()
    --等待弹窗对象
    self._waitingForm = false
    
    self._isShowWaiting=false
    
end

function WWJUICommonClass:Initialize( )
    -- body
end

function WWJUICommonClass:UnInitialize( )
    
    if self._waitingForm then
        self._waitingForm:Destroy()
        self._waitingForm = false
    end
    self._isShowWaiting = false

end

--显示等待
function  WWJUICommonClass:ShowWaiting(isShow,tip)
    
    if isShow then

        if self._isShowWaiting then
            LogE("WWJUICommon ShowWaiting Time Sequence Error")
        end
        --ArcadeInputService:ForbidInput(true)
        self._isShowWaiting=true
        if not self._waitingForm  then
            self._waitingForm = ClassLib.WWJUIWaitingClass.new()
            self._waitingForm:Create()
        end
        self._waitingForm:ActiveForm(true)
        self._waitingForm:ShowTip(tip)

    else
        --ArcadeInputService:ForbidInput(false)
        self._isShowWaiting=false
        self._waitingForm:ActiveForm(false)
    end
end

function  WWJUICommonClass:CleanWaiting()
    
    --ArcadeInputService:ForbidInput(false)
    self._isShowWaiting=false
    if self._waitingForm then
        self._waitingForm:ActiveForm(false)
    end
    
end

function WWJUICommonClass:ShowBubble(content)
    -- body
    local bb = ClassLib.WWJUIBubbleClass.new()
    bb:Create()
    bb:InitShow(content)
end
