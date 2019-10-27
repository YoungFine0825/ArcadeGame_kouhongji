--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-05
*		描述： 娃娃机机器实体
*********************************************************************--]]
--公仔控制
local DollCtr={

}
--机器出口控制
local DoorCtr={

}

--机器事件
MachineEventType = {
    OnClawStateChanged = 1,
    OnCatchedDoll = 2
}

-------------------------------------机台实体----------------
local WWJMachineClass = DeclareClass("WWJMachineClass")
--
function WWJMachineClass:ctor()
    --资产
    self._bindedAsset=false
    --爪子实体
    self._claw=false
    --代理
    self.Delegate=false
    --作弊模式
    self.IsCheatMode=false

end

function WWJMachineClass:Init(delegate)
    
    self._bindedAsset=AssetService:LoadInstantiateAsset("WWJ/Machine/MachineRoot",LifeType.Manual)
    if not self._bindedAsset then
        LogE("Load WWJ Machine Prefab Error")
        return
    end
    self._bindedAsset.RootGo:ExtSetActive(true)
    --
    local plink =self._bindedAsset.RootPLink
    if not plink then
        LogE("Machine Prefab Error Loss PrefabLink ")
        return
    end
    --爪子
    local clawRootTrans = plink:GetCacheTransform(0)
    local clawRootPLink = plink:GetCacheComponent(0)
    --公仔控制
    local dollRootTrans=plink:GetCacheComponent(1)
    DollCtr:Init(dollRootTrans,self)
    --
    local airColliderGo=plink:GetCacheGameObject(2)
    local winTrigger =plink:GetCacheComponent(3)
    --出口控制
    DoorCtr:Init(self,airColliderGo,winTrigger)

    --爪子实体
    self._claw=ClassLib.WWJClawClass.new()
    self._claw:Init(clawRootTrans,clawRootPLink,self)
    --代理
    if delegate and delegate.OnMachineEventCallBack then
        self.Delegate=delegate
    end
end

function WWJMachineClass:UnInit()
    
    if self._claw then
        self._claw:UnInit()
    end
    DollCtr:UnInit()
    DoorCtr:UnInit()
    --
    self.Delegate=false
    --
    if self._bindedAsset then
        AssetService:Unload(self._bindedAsset)
        self._bindedAsset=false
    end

end

--逻辑更新
function WWJMachineClass:LogicUpdate(delta)

   --LogD("娃娃机 机台驱动")
    if self._claw then
        self._claw:LogicUpdate(delta)
    end
end

--处理玩家操作命令
function WWJMachineClass:DealOpCmd(opCmd,arg)
    
    LogD("Machine Deal OpCmd:%d",opCmd)
    --开始
    if opCmd==WWJGameOpCmd.Start then
        if arg and arg.IsCheat then
            LogD("<color=yellow>机器启动 是否作弊:"..tostring(arg.IsCheat).."</color>")
            self.IsCheatMode=arg.IsCheat
        else
            LogD("<color=yellow>机器启动 是否作弊:"..tostring(false).."</color>")
            self.IsCheatMode=false
        end
        --爪子
        self._claw.IsCheatMode=self.IsCheatMode
        --门控制
        if self.IsCheatMode then
            --空气门
            DoorCtr:OpenAirCollider(true)
        end
        --
        return
    end
    
    --刷新内部公仔
    if opCmd==WWJGameOpCmd.RefreshDoll then
        if DollCtr.IsRefreshing then
            return
        end
        DollCtr:DoRefreshDolls()
        return
    end
    --爪子
    if self._claw then
        self._claw:DealOpCmd(opCmd)        
    end

end

--爪子状态变更
function WWJMachineClass:OnClawStateChanged(lastST,nowST)
    
    if self.Delegate then
        local args = {
            LastState = lastST,
            NowState = nowST
        }
        self.Delegate:OnMachineEventCallBack(MachineEventType.OnClawStateChanged,args)
    end
end

--抓到娃娃
function WWJMachineClass:OnCatchedDoll(doll)
    if self.Delegate then
        self.Delegate:OnMachineEventCallBack(MachineEventType.OnCatchedDoll,doll)
    end
end

-----------------------机器内部公仔控制---------------------
function DollCtr:Init(dollRootTrans,machine)
    --内部放的公仔实体
    self._dolls={}
    --放公仔区域
    self.DollAreaWidth=0.5
    self.DollAreaDepth=0.5
    --公仔区域中间格子数
    self.DollAreaGridCnt=5
    --
    self.DollAreaInitY=0.3
    --放公仔的区域点集合
    self.DollAreaPoints={}
    --公仔挂接根
    self._dollRootTrans=dollRootTrans
    --
    self:GenerateDollAreaData()
    --机器
    self._bindedMachine=machine
    --标记是否正在刷新
    self.IsRefreshing=false

end

function DollCtr:UnInit(  )
   
   if self._dolls then
       for k,v in ipairs(self._dolls) do
           v:UnInit()
       end
       table.clear(self._dolls)
       self._dolls=false
   end
   CoroutineService:StopCoroutine(self.RefreshDollCoroutine)

end

--生成放公仔区域数据
function DollCtr:GenerateDollAreaData()
   
   self.DollAreaPoints={}
   --
   local left=self.DollAreaWidth/2.0
   local back=-self.DollAreaDepth/2.0
   local offZ=self.DollAreaDepth/(self.DollAreaGridCnt)
   local offX=self.DollAreaWidth/(self.DollAreaGridCnt)

   local pointCnt=self.DollAreaGridCnt+1
   --一条线段 2等分 3个点
   for i=1,pointCnt do
       local posZ=back+offZ*(i-1)
       for j=1,pointCnt do
           local posX=left-(j-1)*offX
           if posZ>0 and posX >0 then
               --不要出口处
           else
               --收集到
               local pp={x=posX,y=self.DollAreaInitY, z=posZ}
               self.DollAreaPoints[#self.DollAreaPoints+1]=pp
           end
       end
   end
end

--刷新机器内部公仔
function DollCtr:DoRefreshDolls()
   
   self.IsRefreshing=true
   CoroutineService:StartCoroutine(self.RefreshDollCoroutine,self)
   
end
function DollCtr:RefreshDollCoroutine()
    --
    --关门
    DoorCtr:OpenAirCollider(true)
    --
    coroutine.yield(CoroutineService:WaitForFrame(1))

    if self._dolls then
        for k, v in ipairs(self._dolls) do
            v:UnInit()
            coroutine.yield(CoroutineService:WaitForFrame(1))
        end
        table.clear(self._dolls)
    end
    --
   local dollCnt=10
   local areaPCnt=#self.DollAreaPoints
   --
   if areaPCnt<dollCnt then
       --
       LogE("Gen Doll Too Much!")
       dollCnt=areaPCnt
   end
   --从区域点 里面随机找出 一些点
   local indexT={}
   --
   for i=1,areaPCnt do
       indexT[#indexT+1]=i
   end
   --
   local needIndexT={}
   --
   for i=1,dollCnt do
       local random=math.random(1,#indexT)
       needIndexT[#needIndexT+1]=indexT[random]
       --移除索引表
       table.remove(indexT,random)
   end
   --
   for i=1,dollCnt do
       local doll=ClassLib.WWJDollClass.new()
       local pos=self.DollAreaPoints[needIndexT[i]]
       --初始化位置+math.random()*0.4
       coroutine.yield(CoroutineService:WaitForFrame(1))
       doll:Init(self._dollRootTrans,i,pos.x,pos.y,pos.z)
       --
       self._dolls[i]=doll
   end
   coroutine.yield(CoroutineService:WaitForTime(2))
   --空气门
   if not self._bindedMachine.IsCheatMode then
        DoorCtr:OpenAirCollider(false)
   end
   --
   self.IsRefreshing=false
   
end
--
function DollCtr:GetDollByKey(dollKey)

    for i=#self._dolls,1,-1 do
        if self._dolls[i].DollKey==dollKey then
            return self._dolls[i]
        end
    end
    return nil
end
--删除公仔关联
function DollCtr:DelDollByKey(dollKey)
 
    for i=#self._dolls,1,-1 do
        if self._dolls[i].DollKey==dollKey then
            table.remove( self._dolls,i)
            break
        end
    end

end


-----------------------机器出口门控制---------------------
function DoorCtr:Init(machineEntity,airColliderGo,winTrigger)
   
    self._cachedAirColliderGo=airColliderGo
    self._cachedWinTriggerCom=winTrigger
    self._machineEntity = machineEntity

     --监听停止触发器 
    self._cachedWinTriggerCom:InitHandler(1,function (eventType,go)
        self:OnWinTriggerEvent(eventType,go)
    end)

    --
    self:OpenAirCollider(false)

end

function DoorCtr:UnInit( )
    
    self._cachedAirColliderGo=false
    if self._cachedWinTriggerCom then
        self._cachedWinTriggerCom:CleanHandler()
        self._cachedWinTriggerCom=false
    end
  
end

--打开或者关闭空气出口
function DoorCtr:OpenAirCollider(isOpen)
    if self._cachedAirColliderGo then
        self._cachedAirColliderGo:ExtSetActive(isOpen)
    end
end

--
function DoorCtr:OnWinTriggerEvent(eventType,go)

    local nn=go.name
    LogD("Doll Enter Win Trigger"..nn)
    --移除公仔
    --
    local capturedDoll = DollCtr:GetDollByKey(nn)
    if self._machineEntity then
        self._machineEntity:OnCatchedDoll(capturedDoll)
    end
end


