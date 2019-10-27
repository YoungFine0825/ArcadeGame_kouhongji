--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-05
*		描述： 娃娃机爪子 实体
*********************************************************************--]]
--爪子状态定义
WWJClawStateType = {
    None = 0,
    --初始不动
    Idle = 1,
    --操控移动
    Move = 2,
    --下去抓
    Down = 3,
    --爪子合拢
    Close = 4,
    --上升
    Rise = 5,
    --回位
    Back = 6,
    --打开爪子
    Open = 7
}

--爪子定义
local WWJClawClass = DeclareClass("WWJClawClass")
--
function WWJClawClass:ctor()
    --初始位置X
    self.OriPosX = 0.20
    --初始位置Z
    self.OriPosZ = 0.20
    --爪子打开时候角度
    self.AngelMin = 0
    --爪子关闭时候角度
    self.AngleMax = 40
    --爪子当前角度
    self.CurAngle=0

    --移动边界
    self.MoveXMax = 0.22
    self.MoveXMin = -0.22
    self.MoveZMax = 0.2
    self.MoveZMin = -0.2
    --移动盒子
    self.MoveCubeTf = false
    self.VTrunkTf = false

    --绳子组件 UltimateRope
    self.RopeCom = false
    self.RopeMaxExtension = 0.4

    --爪子手指关节 ConfigurableJoint
    self.Finger0JointCom = false
    self.Finger1JointCom = false
    self.Finger2JointCom = false
    self.Finger3JointCom = false
    --触发器
    self.StopTriggerCom = false
    self.MainTriggerCom = false

    --上次状态
    self.LastStateType = WWJClawStateType.None
    self.CurStateType = WWJClawStateType.None
    --当前状态
    self._curState = false
    --状态字典
    self._stateDic = {}
    --父实体
    self._parentEntity = false
    --是否作弊
    self.IsCheatMode=false
end

--初始化
function WWJClawClass:Init(rootTrans, rootPLink, parentEntity)
    if not rootTrans then
        LogE("Claw Init No RootTrans")
        return
    end

    if not rootPLink then
        LogE("Claw Init No RootPLink")
        return
    end

    if parentEntity and parentEntity.OnClawStateChanged then
        self._parentEntity = parentEntity
    end

    ------------获取组件---------------
    self.MoveCubeTf = rootPLink:GetCacheComponent(0)
    self.VTrunkTf = rootPLink:GetCacheComponent(1)
    self.RopeCom = rootPLink:GetCacheComponent(2)
    --手指关节
    self.Finger0JointCom = rootPLink:GetCacheComponent(3)
    self.Finger1JointCom = rootPLink:GetCacheComponent(4)
    self.Finger2JointCom = rootPLink:GetCacheComponent(5)
    self.Finger3JointCom = rootPLink:GetCacheComponent(6)
    --触发器
    self.StopTriggerCom = rootPLink:GetCacheComponent(7)
    self.MainTriggerCom = rootPLink:GetCacheComponent(8)
    --生成绳子
    self.RopeCom.ExtensibleLength = self.RopeMaxExtension
    self.RopeCom:Regenerate()

    --
    --注册状态
    local idle = ClassLib.WWJClawIdleClass.new()
    idle:Init(self)

    local move = ClassLib.WWJClawMoveClass.new()
    move:Init(self)

    local down = ClassLib.WWJClawDownClass.new()
    down:Init(self)

    local rise = ClassLib.WWJClawRiseClass.new()
    rise:Init(self)

    local close = ClassLib.WWJClawCloseClass.new()
    close:Init(self)

    local back = ClassLib.WWJClawBackClass.new()
    back:Init(self)

    local open = ClassLib.WWJClawOpenClass.new()
    open:Init(self)
    --
    self._stateDic[WWJClawStateType.Idle] = idle
    self._stateDic[WWJClawStateType.Move] = move
    self._stateDic[WWJClawStateType.Down] = down
    self._stateDic[WWJClawStateType.Rise] = rise
    self._stateDic[WWJClawStateType.Close] = close
    self._stateDic[WWJClawStateType.Back] = back
    self._stateDic[WWJClawStateType.Open] = open

    --
    self:ChangeState(WWJClawStateType.Idle)
end

--反初始化
function WWJClawClass:UnInit()
    if self._stateDic then
        for k,v in pairs(self._stateDic) do
            v:UnInit()
            self._stateDic[k]=nil
        end
        self._stateDic = false
    end
    self._curState = false
    --
    self.RopeCom = false
    self.Finger0JointCom = false
    self.Finger1JointCom = false
    self.Finger2JointCom = false
    self.Finger3JointCom = false
    self.StopTriggerCom = false
    self.MainTriggerCom = false
    --
    self._parentEntity = false
end

--改变状态
function WWJClawClass:ChangeState(newSt)
    if self.CurStateType == newSt then
        return
    end
    if self._curState then
        self._curState:Leave()
    end
    if not self._stateDic[newSt] then
        LogE("No Registered Claw State Type %d", newSt)
        return
    end
    self.LastStateType = self.CurStateType
    self.CurStateType = newSt
    --
    self._curState = self._stateDic[newSt]
    self._curState:Enter()
    --
    if self._parentEntity then
        self._parentEntity.OnClawStateChanged(self._parentEntity, self.LastStateType, self.CurStateType)
    end
end

--逻辑更新
function WWJClawClass:LogicUpdate(delta)
    --状态驱动
    if self._curState then
        if self._curState:Reason() then
            self._curState:Action(delta)
        end
    end
end

--处理玩家操作命令
function WWJClawClass:DealOpCmd(opCmd)
    --开始游戏
    if opCmd == WWJGameOpCmd.Start then

        --
        self.MainTriggerCom:Reset()
        --
        if self.CurStateType ~= WWJClawStateType.Move then
            self:ChangeState(WWJClawStateType.Move)
        end
        return
    end
    --停止
    if opCmd == WWJGameOpCmd.Stop then
        if self.CurStateType ~= WWJClawStateType.Idle then
            self:ChangeState(WWJClawStateType.Idle)
        end
        return
    end

    --移动操作命令
    if opCmd == WWJGameOpCmd.RockerMoveBack or opCmd == WWJGameOpCmd.RockerMoveForward or
            opCmd == WWJGameOpCmd.RockerMoveLeft or
            opCmd == WWJGameOpCmd.RockerMoveMiddle or
            opCmd == WWJGameOpCmd.RockerMoveRight
     then
        --
        if self.CurStateType == WWJClawStateType.Idle then
            self:ChangeState(WWJClawStateType.Move)
        end

        if self.CurStateType == WWJClawStateType.Move then
            self._curState:DealMoveCmd(opCmd)
        else
            LogD("Not Response Move Cmd Must Idle First")
        end
    end
    --下爪
    if opCmd == WWJGameOpCmd.ClawDown then
        --
        if self.CurStateType ~= WWJClawStateType.Down then
            self:ChangeState(WWJClawStateType.Down)
        end
    end
end
