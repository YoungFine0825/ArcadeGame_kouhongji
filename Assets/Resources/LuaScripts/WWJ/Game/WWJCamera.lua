--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-05
*		描述： 娃娃机相机控制
*********************************************************************--]]
--相机控制模式
WWJCameraCtrMode = {
    --玩家左边
    LeftView = 1,
    --玩家正中
    MiddleView = 2,
    --玩家右边
    RightView = 3
}

local WWJCameraClass = DeclareClass("WWJCameraClass")
--
function WWJCameraClass:ctor()
    self._bindedAsset = false
    --控制移动旋转
    self._pivotTf = false
    --主相机组件
    self._cameraCom = false
    --主相机Transform组件
    self._cameraTf = false
    --当前视野模式 决定摇杆操作方位
    self.CurCtrMode = WWJCameraCtrMode.MiddleView

    --旋转控制
    self._curRotateOffY = 0
    self._curRotateY = 0
    self._curTargetRotateY = 0
    --
    self._isReseted = true
end

function WWJCameraClass:Init()
    self._bindedAsset = AssetService:LoadInstantiateAsset("WWJ/Env/CameraRoot", LifeType.Manual)
    if not self._bindedAsset then
        LogE("Load WWJ Cameara Prefab Error")
        return
    end
    self._bindedAsset.RootGo:ExtSetActive(true)
    --
    local plink = self._bindedAsset.RootPLink
    if not plink then
        LogE("WWJ Camera Root Loss PrefabLink")
        return
    end
    self._pivotTf = plink:GetCacheComponent(0)
    self._cameraCom = plink:GetCacheComponent(1)
    self._cameraTf = self._cameraCom.transform
    --
    self._curRotateY = 0
    --
end

function WWJCameraClass:UnInit()
    self._pivotTf = false
    self._cameraCom = false
    self._cameraTf = false
    if self._bindedAsset then
        AssetService:Unload(self._bindedAsset)
        self._bindedAsset = false
    end
end

--逻辑更新
function WWJCameraClass:LogicUpdate(delta)
    
    if not self._pivotTf then
        return
    end

    --更新旋转 到目标
    if not math.EqualFloat(self._curRotateY, self._curTargetRotateY,0.01) then
        
        local lerpV = math.Lerp(self._curRotateY, self._curTargetRotateY, delta * 10)
        lerpV=math.Clamp(lerpV,-90,90)
        self._pivotTf:ExtSetLocalEulerAnglesY(lerpV)
        self._curRotateY = lerpV

    else
        --调整到3方位视觉
        if self._curRotateOffY == 0 and not self._isReseted then

            self._isReseted=true
            --停下来
            if self._curRotateY > 45 then
                local doT = self._pivotTf:DOLocalRotateYYZ(0, 90, 0, 0.5)
                --doT:SetEase(DoTweenEase.OutQuad)
                self._curRotateY = 90
                self.CurCtrMode=WWJCameraCtrMode.LeftView

            elseif self._curRotateY < -45 then
                local doT = self._pivotTf:DOLocalRotateYYZ(0, -90, 0, 0.5)
                --doT:SetEase(DoTweenEase.OutQuad)
                self._curRotateY = -90
                self.CurCtrMode=WWJCameraCtrMode.RightView

            else
                local doT = self._pivotTf:DOLocalRotateYYZ(0, 0, 0, 0.5)
                --doT:SetEase(DoTweenEase.OutQuad)
                self._curRotateY = 0
                self.CurCtrMode=WWJCameraCtrMode.MiddleView

            end

            self._curTargetRotateY=self._curRotateY

        end
    end

    --继续检查
    if self._curRotateOffY ~= 0 then
        
        self._curTargetRotateY = self._curTargetRotateY + self._curRotateOffY
        --
        if self._curTargetRotateY > 90 then
            self._curTargetRotateY = 90
        end
        if self._curTargetRotateY < -90 then
            self._curTargetRotateY = -90
        end
        self._isReseted=false
    else
        --
    end

end

--处理玩家操作命令
function WWJCameraClass:DealOpCmd(opCmd, arg)
    
    LogD("WWJCamera Deal OpCmd:%d Arg:%d", opCmd, arg)
    if opCmd ~= WWJGameOpCmd.RotateMoveViewPort then
        return
    end
    self._curRotateOffY = arg * 0.35

end


function WWJCameraClass:ResetMiddleView()
    --dotween
    local doT = self._pivotTf:DOLocalRotateYYZ(0, 0, 0, 1.0)
    --doT:SetEase(DoTweenEase.OutQuad)
    self._curRotateY = 0
    self._curRotateOffY=0
    self.CurCtrMode=WWJCameraCtrMode.MiddleView
    self._isReseted=true

end

--对准出口处
function WWJCameraClass:LookAtExit()
    if self._cameraTf then
        self:ResetMiddleView()
        self._cameraTf:DOLocalMove(CS.UnityEngine.Vector3(-0.078,-0.81,1.016),1)
        self._cameraTf:DOLocalRotate(CS.UnityEngine.Vector3(9.798,180,0),1)
    end
end

--复原位置
function WWJCameraClass:RevertPosition()
    if self._cameraTf then
        local doT = self._pivotTf:DOLocalRotateYYZ(0, 0, 0, 1)
        self._curRotateY = 0
        self._curRotateOffY=0
        self.CurCtrMode=WWJCameraCtrMode.MiddleView
        self._cameraTf:DOLocalMove(CS.UnityEngine.Vector3(0,0,1.335),1)
        self._cameraTf:DOLocalRotate(CS.UnityEngine.Vector3(2,180,0),1)
    end

end
