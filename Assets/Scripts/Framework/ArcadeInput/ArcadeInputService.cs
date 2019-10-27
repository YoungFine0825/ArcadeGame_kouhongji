/********************************************************************
	created:	2018-10-9
	author:		jordenwu
	
	purpose:	街机输入管理
*********************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using JW.Common;

namespace JW.Framework.ArcadeInput
{
    //摇杆
    public enum RockerState
    {
        RockerMoveForward=1,
        RockerMoveForwardLeft=2,
        RockerMoveForwardRight=3,
        RockerMoveBack=4,
        RockerMoveBackLeft=5,
        RockerMoveBackRight=6,
        RockerMoveLeft=7,
        RockerMoveRight=8,
        RockerMoveMiddle=9,
    }

    //物理按钮
    public enum PressState
    {
        Ready=1,
        OnPress=2,
        OnClick=3,
    }

    //
    public enum PressEvent
    {
        PressBegin=1,
        PressEnd=2,
        PressClick=3,
        ShortPress=4,
        LongPress=5,
    }


    //委托申明
    public delegate void ArcadeInputRockerDelegate(int rs);
    public delegate void ArcadeInputPressDelegate(int ps);
    public delegate void ArcadeInputRotateDelegate(float rotate);
    public delegate void ArcadeInputRefreshDelegate(int ps);

    /// <summary>
    /// 街机输入服务
    /// </summary>
    public class ArcadeInputService : Singleton<ArcadeInputService>
    {
        private float longPressTime;
        private float longPressStarTime;
        private float onceRotationAngle;
        private float currentRotationAngle;
        private float recordRotationAngle;
        private PressState startPressState;
        private PressState refreshPressState;
        private RockerState rockerState;

#if UNITY_EDITOR
        private bool PCControl = true;
#else
        private bool PCControl = false;
#endif

        public ArcadeInputRockerDelegate RockerHandler;
        public ArcadeInputPressDelegate PressHandler;
        public ArcadeInputRotateDelegate RotateHandler;
        public ArcadeInputRefreshDelegate RefreshHandler;

        public override bool Initialize()
        {
            currentRotationAngle = 0;
            recordRotationAngle = 0;
            rockerState = RockerState.RockerMoveMiddle;
            startPressState = PressState.Ready;
            refreshPressState = PressState.Ready;
            longPressTime = 2.0f;

#if UNITY_EDITOR
            PCControl = true;
#else
            //启动串口
            McuInput.GetInstance();
            PCControl = false;
#endif
            return true;
        }

        public override void Uninitialize()
        {
            RockerHandler = null;
            PressHandler = null;
            RotateHandler = null;
            McuInput.DestroyInstance();
            //
        }

        /// <summary>
        /// 逻辑驱动
        /// </summary>
        public void LogicUpdate()
        {
            if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha1))
            {
                PCControl = !PCControl;
#if JW_DEBUG
                JW.Common.Log.LogE("-----输入切换------>PCControl<------" + PCControl.ToString());
#endif
            }

            //摇杆 旋转
            MachineInputCheck();
            //确定按键
            MachinePressCheck();
            //刷新按钮
            RefreshPressCheck();
        }

        //摇杆检查
        private void MachineInputCheck()
        {
            if (PCControl)
            {
                if (UnityEngine.Input.GetKey(KeyCode.Q))
                {
                    if (rockerState != RockerState.RockerMoveForwardLeft)
                    {
                        rockerState = RockerState.RockerMoveForwardLeft;
                        if (RockerHandler != null)
                        {
                            RockerHandler((int)rockerState);
                        }
                    }
                }
                else if (UnityEngine.Input.GetKey(KeyCode.E))
                {
                    if (rockerState != RockerState.RockerMoveForwardRight)
                    {
                        rockerState = RockerState.RockerMoveForwardRight;
                        if (RockerHandler != null)
                        {
                            RockerHandler((int)rockerState);
                        }
                    }
                }
                else if (UnityEngine.Input.GetKey(KeyCode.W))
                {
                    if (rockerState != RockerState.RockerMoveForward)
                    {
                        rockerState = RockerState.RockerMoveForward;
                        if (RockerHandler != null)
                        {
                            RockerHandler((int)rockerState);
                        }
                    }
                }
                else if (UnityEngine.Input.GetKey(KeyCode.Z))
                {
                    if (rockerState != RockerState.RockerMoveBackLeft)
                    {
                        rockerState = RockerState.RockerMoveBackLeft;
                        if (RockerHandler != null)
                        {
                            RockerHandler((int)rockerState);
                        }
                    }
                }
                else if (UnityEngine.Input.GetKey(KeyCode.C))
                {
                    if (rockerState != RockerState.RockerMoveBackRight)
                    {
                        rockerState = RockerState.RockerMoveBackRight;
                        if (RockerHandler != null)
                        {
                            RockerHandler((int)rockerState);
                        }
                    }
                }
                else if (UnityEngine.Input.GetKey(KeyCode.S))
                {
                    if (rockerState != RockerState.RockerMoveBack)
                    {
                        rockerState = RockerState.RockerMoveBack;
                        if (RockerHandler != null)
                        {
                            RockerHandler((int)rockerState);
                        }
                    }
                }
                else if (UnityEngine.Input.GetKey(KeyCode.A))
                {
                    if (rockerState != RockerState.RockerMoveLeft)
                    {
                        rockerState = RockerState.RockerMoveLeft;
                        if (RockerHandler != null)
                        {
                            RockerHandler((int)rockerState);
                        }
                    }
                }
                else if (UnityEngine.Input.GetKey(KeyCode.D))
                {
                    if (rockerState != RockerState.RockerMoveRight)
                    {
                        rockerState = RockerState.RockerMoveRight;
                        if (RockerHandler != null)
                        {
                            RockerHandler((int)rockerState);
                        }
                    }
                }
                else if (!UnityEngine.Input.GetKey(KeyCode.Q) && !UnityEngine.Input.GetKey(KeyCode.W) && !UnityEngine.Input.GetKey(KeyCode.E) && !UnityEngine.Input.GetKey(KeyCode.A) && !UnityEngine.Input.GetKey(KeyCode.D) && !UnityEngine.Input.GetKey(KeyCode.Z) && !UnityEngine.Input.GetKey(KeyCode.X) && !UnityEngine.Input.GetKey(KeyCode.C))
                {
                    if (rockerState != RockerState.RockerMoveMiddle)
                    {
                        rockerState = RockerState.RockerMoveMiddle;
                        if (RockerHandler != null)
                        {
                            RockerHandler((int)rockerState);
                        }
                    }
                }
                //旋转
                if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
                {
                    currentRotationAngle = 5f;
                }
                else if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
                {
                    currentRotationAngle = -5f;
                }
                else
                {
                    currentRotationAngle = 0f;
                }
                if (recordRotationAngle != currentRotationAngle)
                {
                    recordRotationAngle = currentRotationAngle;
                    if (RotateHandler != null)
                    {
                        RotateHandler(recordRotationAngle);
                    }
                }
            }
            else
            {
                if (McuInput.GetInstance().GetKey(MCUKeyCode.LeftUp))
                {
                    if (rockerState != RockerState.RockerMoveForwardLeft)
                    {
                        rockerState = RockerState.RockerMoveForwardLeft;
                        if (RockerHandler != null)
                        {
                            RockerHandler((int)rockerState);
                        }

                    }
                }
                else if (McuInput.GetInstance().GetKey(MCUKeyCode.RightUp))
                {
                    if (rockerState != RockerState.RockerMoveForwardRight)
                    {
                        rockerState = RockerState.RockerMoveForwardRight;
                        if (RockerHandler != null)
                        {
                            RockerHandler((int)rockerState);
                        }
                    }
                }
                else if (McuInput.GetInstance().GetKey(MCUKeyCode.Up))
                {
                    if (rockerState != RockerState.RockerMoveForward)
                    {
                        rockerState = RockerState.RockerMoveForward;
                        if (RockerHandler != null)
                        {
                            RockerHandler((int)rockerState);
                        }
                    }
                }
                else if (McuInput.GetInstance().GetKey(MCUKeyCode.LeftDown))
                {
                    if (rockerState != RockerState.RockerMoveBackLeft)
                    {
                        rockerState = RockerState.RockerMoveBackLeft;
                        if (RockerHandler != null)
                        {
                            RockerHandler((int)rockerState);
                        }
                    }
                }
                else if (McuInput.GetInstance().GetKey(MCUKeyCode.RightDown))
                {
                    if (rockerState != RockerState.RockerMoveBackRight)
                    {
                        rockerState = RockerState.RockerMoveBackRight;
                        if (RockerHandler != null)
                        {
                            RockerHandler((int)rockerState);
                        }
                    }
                }
                else if (McuInput.GetInstance().GetKey(MCUKeyCode.Down))
                {
                    if (rockerState != RockerState.RockerMoveBack)
                    {
                        rockerState = RockerState.RockerMoveBack;
                        if (RockerHandler != null)
                        {
                            RockerHandler((int)rockerState);
                        }
                    }
                }
                else if (McuInput.GetInstance().GetKey(MCUKeyCode.Left))
                {
                    if (rockerState != RockerState.RockerMoveLeft)
                    {
                        rockerState = RockerState.RockerMoveLeft;
                        if (RockerHandler != null)
                        {
                            RockerHandler((int)rockerState);
                        }
                    }
                }
                else if (McuInput.GetInstance().GetKey(MCUKeyCode.Right))
                {
                    if (rockerState != RockerState.RockerMoveRight)
                    {
                        rockerState = RockerState.RockerMoveRight;
                        if (RockerHandler != null)
                        {
                            RockerHandler((int)rockerState);
                        }
                    }
                }
                else if (!McuInput.GetInstance().GetKey(MCUKeyCode.Up) && !McuInput.GetInstance().GetKey(MCUKeyCode.Down) && !McuInput.GetInstance().GetKey(MCUKeyCode.Left) && !McuInput.GetInstance().GetKey(MCUKeyCode.Right) && !McuInput.GetInstance().GetKey(MCUKeyCode.LeftUp) && !McuInput.GetInstance().GetKey(MCUKeyCode.RightUp) && !McuInput.GetInstance().GetKey(MCUKeyCode.LeftDown) && !McuInput.GetInstance().GetKey(MCUKeyCode.RightDown))
                {
                    if (rockerState != RockerState.RockerMoveMiddle)
                    {
                        rockerState = RockerState.RockerMoveMiddle;
                        if (RockerHandler != null)
                        {
                            RockerHandler((int)rockerState);
                        }
                    }
                }

                onceRotationAngle = -McuInput.GetInstance().GetRotation();
                currentRotationAngle += onceRotationAngle;
                //
                if (recordRotationAngle != 0 && onceRotationAngle == 0)
                {
                    recordRotationAngle = onceRotationAngle;
                    if (RotateHandler != null)
                    {
                        RotateHandler(recordRotationAngle);
                    }
                    return;
                }
                if (currentRotationAngle > 7.2f || currentRotationAngle < -7.2f)
                {
                    recordRotationAngle = currentRotationAngle;
                    currentRotationAngle = 0;
                    if (RotateHandler != null)
                    {
                        RotateHandler(recordRotationAngle);
                    }
                }
            }
        }

        //确定按键检查
        private void MachinePressCheck()
        {
            if (PCControl)
            {
                if (startPressState == PressState.Ready)
                {
                    if (UnityEngine.Input.GetKey(KeyCode.Return))
                    {
                        startPressState = PressState.OnPress;
                        longPressStarTime = Time.time;
                        //
                        if (PressHandler != null)
                        {
                            PressHandler((int)PressEvent.PressBegin);
                        }
                    }
                }
                else if (startPressState == PressState.OnPress)
                {
                    if (Time.time - longPressStarTime > longPressTime)
                    {
                        startPressState = PressState.OnClick;
                        longPressStarTime = Time.time;
                        //
                        if (PressHandler != null)
                        {
                            PressHandler((int)PressEvent.LongPress);
                        }
                    }
                    else if (!UnityEngine.Input.GetKey(KeyCode.Return))
                    {
                        startPressState = PressState.OnClick;
                        longPressStarTime = Time.time;
                        //
                        if (PressHandler != null)
                        {
                            PressHandler((int)PressEvent.PressEnd);
                        }
                        //
                        if (PressHandler != null)
                        {
                            PressHandler((int)PressEvent.PressClick);
                        }

                    }
                }
                else if (startPressState == PressState.OnClick && !UnityEngine.Input.GetKey(KeyCode.Return))
                {
                    //重置
                    startPressState = PressState.Ready;
                }
            }
            else
            {
                if (startPressState == PressState.Ready)
                {
                    if (McuInput.GetInstance().GetKey(MCUKeyCode.IN3))
                    {
                        startPressState = PressState.OnPress;
                        longPressStarTime = Time.time;

                        if (PressHandler != null)
                        {
                            PressHandler((int)PressEvent.PressBegin);
                        }
                    }
                }
                else if (startPressState == PressState.OnPress)
                {
                    if (Time.time - longPressStarTime > longPressTime)
                    {

                        startPressState = PressState.OnClick;
                        longPressStarTime = Time.time;

                        if (PressHandler != null)
                        {
                            PressHandler((int)PressEvent.LongPress);
                        }

                    }
                    else if (!McuInput.GetInstance().GetKey(MCUKeyCode.IN3))
                    {

                        startPressState = PressState.OnClick;
                        longPressStarTime = Time.time;

                        if (PressHandler != null)
                        {
                            PressHandler((int)PressEvent.PressEnd);
                        }

                        if (PressHandler != null)
                        {
                            PressHandler((int)PressEvent.PressClick);
                        }

                    }
                }
                else if (startPressState == PressState.OnClick && !McuInput.GetInstance().GetKey(MCUKeyCode.IN3))
                {
                    startPressState = PressState.Ready;
                }
            }
        }

        //刷新按键检查
        private void RefreshPressCheck()
        {
            if (PCControl)
            {
                if (refreshPressState == PressState.Ready)
                {
                    if (UnityEngine.Input.GetKey(KeyCode.R))
                    {
                        refreshPressState = PressState.OnPress;
                    }
                }
                else if (refreshPressState == PressState.OnPress)
                {
                    if (!UnityEngine.Input.GetKey(KeyCode.R))
                    {
                        refreshPressState = PressState.OnClick;
                        if (RefreshHandler != null)
                        {
                            RefreshHandler((int)PressEvent.PressClick);
                        }
                    }
                }
                else if (refreshPressState == PressState.OnClick && !UnityEngine.Input.GetKey(KeyCode.R))
                {
                    refreshPressState = PressState.Ready;
                }
            }
            else
            {
                if (refreshPressState == PressState.Ready)
                {
                    if (McuInput.GetInstance().GetKey(MCUKeyCode.IN2))
                    {
                        refreshPressState = PressState.OnPress;
                    }
                }
                else if (refreshPressState == PressState.OnPress)
                {
                    if (!McuInput.GetInstance().GetKey(MCUKeyCode.IN2))
                    {
                        if (RefreshHandler != null)
                        {
                            RefreshHandler((int)PressEvent.PressClick);
                        }
                        refreshPressState = PressState.OnClick;
                    }
                }
                else if (refreshPressState == PressState.OnClick && !McuInput.GetInstance().GetKey(MCUKeyCode.IN2))
                {
                    //重置
                    refreshPressState = PressState.Ready;
                }
            }
        }


        /// <summary>
        /// 获取设备密码 
        /// </summary>
        /// <returns></returns>
        public string GetDeviceKey()
        {
            string key = string.Empty;
#if UNITY_EDITOR || JW_DEBUG
            key = "123456";
#else
            key = ReadKey.GetKey();
#endif
            if (string.IsNullOrEmpty(key))
            {
                JW.Common.Log.LogE("---->Get Device Password Empty!");
                key = "";
            }
            else
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(key);
                key= System.Convert.ToBase64String(plainTextBytes).Replace('+', '-').Replace('/', '_');
                key = key.TrimEnd();
            }
            JW.Common.Log.LogD("----><color=yellow>Get Device Password:</color>" + key);
            return key;
        }

    }
}
