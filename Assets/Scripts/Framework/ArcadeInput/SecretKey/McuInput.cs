using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.IO.Ports;
using System.IO;
using System;
using JW.Common;

namespace JW.Framework.ArcadeInput
{
    public enum MCUKeyCode
    {
        Up,
        Down,
        Left,
        Right,
        LeftUp,
        RightUp,
        LeftDown,
        RightDown,
        Sure,
        IN0,
        IN1,
        IN2,
        IN3
    }

    public enum MCUConnectState
    {
        connect,
        disconnect
    }

    public class McuInput : Singleton<McuInput>
    {
        private Thread _workThread;
        private SerialPort _serialPort;

        private Object _lockObject;
        private string[] _portNames = null;
        private int _state;

        private int _checkCount = 0;
        private byte[] _readBytes = new byte[100];
        private byte[] _frameBytes = new byte[8];

        //接收一帧的状态
        private int _frameState = 0;
        private int _frameDataCount = 0;

        private uint _keyState = 0;
        private int _rotationCount = 0;
        private int _contentErrorNum = 0;

        private MCUConnectState _connectState = MCUConnectState.disconnect;


        //第1版连接的摇杆,不再使用
        //private static byte[] MCUKeyCheckCode = new byte[5]{0x40, 0x08, 0x20, 0x10, 0x04}; //判断下位机上传的编码是什么按键，上、右、下、左、开始
        //第2版连接的摇杆,不再使用
        //private static byte[] MCUKeyCheckCode = new byte[5]{0x04, 0x08, 0x10, 0x20, 0x40}; //判断下位机上传的编码是什么按键，上、右、下、左、开始

        public override bool Initialize()
        {
            _state = 0;
            _lockObject = new Object();
            _workThread = new Thread(new ThreadStart(WorkThreadProcess));
            _workThread.IsBackground = true;
            _workThread.Start();
            return true;
        }

        public override void Uninitialize()
        {
            if (_workThread != null)
            {
                _workThread.Abort();
                _workThread = null;
            }

            if (_serialPort != null)
            {
                _serialPort.Close();
                _serialPort = null;
            }
            _lockObject = null;
        }

        /*
        得到按键值
        返回值：1：按下，0：松开
        */
        public bool GetKey(MCUKeyCode mcuKeyCode)
        {
            switch (mcuKeyCode)
            {
                case MCUKeyCode.LeftUp:
                    if (((_keyState & 0x000f) & 0x09) == 0x09)
                    {
                        return true;
                    }
                    break;
                case MCUKeyCode.RightUp:
                    if (((_keyState & 0x000f) & 0x03) == 0x03)
                    {
                        return true;
                    }
                    break;
                case MCUKeyCode.LeftDown:
                    if (((_keyState & 0x000f) & 0x0c) == 0x0c)
                    {
                        return true;
                    }
                    break;
                case MCUKeyCode.RightDown:
                    if (((_keyState & 0x000f) & 0x06) == 0x06)
                    {
                        return true;
                    }
                    break;
                case MCUKeyCode.Up:
                    if ((((_keyState & 0x000f) & 0x01) == 0x01) && (((_keyState & 0x000f) & 0x08) == 0) && (((_keyState & 0x000f) & 0x02) == 0))
                    {
                        return true;
                    }
                    break;
                case MCUKeyCode.Down:
                    if ((((_keyState & 0x000f) & 0x04) == 0x04) && (((_keyState & 0x000f) & 0x08) == 0) && (((_keyState & 0x000f) & 0x02) == 0))
                    {
                        return true;
                    }
                    break;
                case MCUKeyCode.Left:
                    if ((((_keyState & 0x000f) & 0x08) == 0x08) && (((_keyState & 0x000f) & 0x01) == 0) && (((_keyState & 0x000f) & 0x04) == 0))
                    {
                        return true;
                    }
                    break;
                case MCUKeyCode.Right:
                    if ((((_keyState & 0x000f) & 0x02) == 0x02) && (((_keyState & 0x000f) & 0x01) == 0) && (((_keyState & 0x000f) & 0x04) == 0))
                    {
                        return true;
                    }
                    break;
                case MCUKeyCode.Sure:
                    if (((_keyState & 0x00f0) & 0x10) == 0x10)
                    {
                        return true;
                    }
                    break;
                case MCUKeyCode.IN0:
                    if (((_keyState & 0x0f00) & 0x100) == 0x100)
                    {
                        return true;
                    }
                    break;
                case MCUKeyCode.IN1:
                    if (((_keyState & 0x0f00) & 0x200) == 0x200)
                    {
                        return true;
                    }
                    break;
                case MCUKeyCode.IN2:
                    if (((_keyState & 0x0f00) & 0x400) == 0x400)
                    {
                        return true;
                    }
                    break;
                case MCUKeyCode.IN3:
                    if (((_keyState & 0x0f00) & 0x800) == 0x800)
                    {
                        return true;
                    }
                    break;
                default: break;
            }

            return false;
        }

        /*
        得到从上一次调用本函数时到现在的旋转值。 如果你不想要某个时间段内的旋转值，可以调用一次本函数，之后的旋转值会被清空
        返回值：旋转角度值，单位度，>0：顺时针旋转，<0：逆时针旋转。
        */
        public int GetRotation()
        {
            //这是原子操作哦
            int tmp = Interlocked.Exchange(ref _rotationCount, 0);
            return tmp;
        }


        /*下位机连接状态 */
        public MCUConnectState GetConnectState()
        {
            return _connectState;
        }

        /*
        设置灯条的模式,请在调用GetConnectState(),且返回MCUConnectState.connect后调用。但是也有极小极小可能性抛出异常。
        参数：lightBarNum 灯条序号，全部取值：0和1
        value 灯条显示模式，全部取值0到15 
        */
        public void SetLightBar(int lightBarIndex, byte value)
        {
            byte[] sendBuf = new byte[6];

            sendBuf[0] = 0x55;
            sendBuf[1] = (byte)lightBarIndex;
            sendBuf[2] = (byte)lightBarIndex;
            sendBuf[3] = value;
            sendBuf[4] = value;
            sendBuf[5] = 0xcc;

            try
            {
                lock (_lockObject)
                {
                    _serialPort.Write(sendBuf, 0, 6);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 工作线程
        /// </summary>
        private void WorkThreadProcess()
        {
            while (true)
            {
                //状态0表示在搜索串口
                if (_state == 0)
                {
                    lock (_lockObject)
                    {
                        _portNames = SerialPort.GetPortNames();
                    }
                    if (_portNames == null || _portNames.Length == 0)
                    {
                        //JW.Common.Log.LogE("没有串口 等待重新检查");
                        _state = 0;
                        Thread.Sleep(5000);
                    }
                    else
                    {
                        JW.Common.Log.LogD("全部串口如下：");
                        foreach (var name in _portNames)
                        {
                            JW.Common.Log.LogD(name);
                        }
                        _state = 1;
                        Thread.Sleep(5);
                    }
                }

                //打开不同的串口
                if (_state == 1)
                {
                    try
                    {
                        string realName = string.Empty;
                        lock (_lockObject)
                        {
                            int index = _portNames[_checkCount].IndexOf("M");
                            int A = int.Parse(_portNames[_checkCount].Substring(index + 1));
                            if (A < 9)
                            {
                                realName = _portNames[_checkCount];
                                _serialPort = new SerialPort(_portNames[_checkCount], 19200, Parity.None, 8, StopBits.One);
                            }
                            else
                            {
                                realName = "\\\\.\\" + _portNames[_checkCount];
                                _serialPort = new SerialPort("\\\\.\\" + _portNames[_checkCount], 19200, Parity.None, 8, StopBits.One);
                            }

                            _serialPort.Open();
                            //下位机会10ms发送一次信息，这里设置40ms，是发送间隔的3+倍，只要下位机正常，肯定能收到完成的一帧。至于什么，自己根据采样定律想去！
                            _serialPort.ReadTimeout = 40;
                        }
                        //开始读取
                        _state = 2;
                        _frameState = 0;
                        _frameDataCount = 0;
                        _contentErrorNum = 0;
                        JW.Common.Log.LogD("打开串口成功：" + realName);

                    }
                    catch (IOException exp)
                    {
                        //JW.Common.Log.LogE("打开串口：" + _portNames[_checkCount] + "失败:" + exp.Message);
                        _checkCount++;
                        if (_checkCount == _portNames.Length)
                        {
                            //得到的串口都检查一遍了，没有发现下位机，就重新获取串口列表重试
                            _checkCount = 0;
                            _state = 0;
                            //JW.Common.Log.LogE("没找到串口 等待重试!");
                        }
                        _serialPort = null;
                    }

                    Thread.Sleep(5);
                }
                //读取串口数据
                if (_state == 2)
                {
                    try
                    {
                        int realReadNum;
                        lock (_lockObject)
                        {
                            realReadNum = _serialPort.Read(_readBytes, 0, 100);
                        }
                        for (int i = 0; i < realReadNum; i++)
                        {
                            switch (_frameState)
                            {
                                case 0:
                                    if (_readBytes[i] == 0x55)
                                    {
                                        //有帧头了，就到接收其他的
                                        _frameState = 1;
                                        _frameDataCount = 0;
                                    }
                                    else
                                    {
                                        _contentErrorNum++;
                                    }
                                    break;
                                case 1:
                                    _frameBytes[_frameDataCount++] = _readBytes[i];
                                    if (_frameDataCount == 8)
                                    {

                                        if (_frameBytes[0] == _frameBytes[4] && _frameBytes[1] == _frameBytes[5] && _frameBytes[2] == _frameBytes[6])
                                        {
                                            _frameState = 2;
                                        }
                                        else
                                        {
                                            _frameState = 0;
                                            _contentErrorNum++;
                                        }
                                    }
                                    break;
                                case 2:
                                    if (_readBytes[i] == 0xcc)
                                    {

                                        uint keyStateL = _frameBytes[0];
                                        uint keyStateH = _frameBytes[1];
                                        _keyState = keyStateL | (keyStateH << 8);

                                        int tmpRotationCount = BitConverter.ToInt16(_frameBytes, 2);
                                        //这是原子操作哦
                                        Interlocked.Add(ref _rotationCount, tmpRotationCount);

                                        _frameState = 0;
                                        _contentErrorNum = 0;

                                        //连接了
                                        _connectState = MCUConnectState.connect;

                                    }
                                    else
                                    {
                                        _contentErrorNum++;
                                    }
                                    break;
                                default: break;
                            }

                            if (_contentErrorNum > 3)
                            {
                                //JW.Common.Log.LogE("McuInput 接收内容错误");
                                ResetState();
                            }
                        }
                    }
                    catch (TimeoutException)
                    {
                        //JW.Common.Log.LogE("McuInput 接收时间超时");
                        ResetState();
                    }
                    Thread.Sleep(5);
                }
            }
        }

        private void ResetState()
        {
            lock (_lockObject)
            {
                if (_serialPort != null)
                {
                    _serialPort.Close();
                }
            }
            //JW.Common.Log.LogE("关闭串口：" + _portNames[_checkCount]);
            _checkCount++;
            if (_checkCount == _portNames.Length)
            {
                _checkCount = 0;
                //得到的串口都检查一遍了，没有发现下位机，就重新获取串口列表重试
                _state = 0;
            }
            else
            {
                //还有没有把所有的串口都尝试一遍，所以尝试下一个了哦
                _state = 1;
            }
            _connectState = MCUConnectState.disconnect;
        }
    }
}

