--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-01
*		描述： 街机硬件输入声明
*********************************************************************--]]
ArcadeInput={

    --摇杆状态
    RockerState={

        RockerMoveForward=1,
        RockerMoveForwardLeft=2,
        RockerMoveForwardRight=3,
        RockerMoveBack=4,
        RockerMoveBackLeft=5,
        RockerMoveBackRight=6,
        RockerMoveLeft=7,
        RockerMoveRight=8,
        RockerMoveMiddle=9
    },

    --物理按钮事件
    PressEvent={
        
        PressBegin=1,
        PressEnd=2,
        PressClick=3,
        ShortPress=4,
        LongPress=5,
    }
}