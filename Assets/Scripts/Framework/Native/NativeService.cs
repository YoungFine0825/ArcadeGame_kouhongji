/********************************************************************
	created:	18:10:2017   15:59
	author:		jordenwu
	purpose:	系统本地服务 
*********************************************************************/
using JW.Common;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JW.Framework.Native
{
    public class NativeResult
    {
        public int returnType;          //返回数据类型,1:电量数据, 2:wifi状态，3:地理位置信息 4:电池链接充电状态变化
        public int chargeLevel;         //电量数据
        public string locationJson;     //地理位置信息
        public int networkType;         //网络状态
        public bool batteryConnected;   //电池充电
    }

    public delegate void NativeServiceBaseDelegate(NativeResult result);


    public class NativeService : MonoSingleton<NativeService>
    {
        //网络环境桥接
        private NetEnvBridge _netEnv;
        //设备等级
        private DeviceLevBridge _deviceLevel;

        //电量、网络、地理位置等接口消息回复
        private NativeServiceBaseDelegate _baseHandler;

        public override bool Initialize()
        {
            _netEnv=NetEnvBridge.CreateBridge();
            _netEnv.Initialize(null);

            _deviceLevel = new DeviceLevBridge();
            _deviceLevel.Initialize();
            //
            return true;
        }

        public override void Uninitialize()
        {

            if (_netEnv != null)
            {
                _netEnv.Uninitialize();
                NetEnvBridge.DestroyBridge();
                _netEnv = null;
            }
            if (_deviceLevel != null)
            {
                _deviceLevel.Uninitialize();
                _deviceLevel = null;
            }
            return;
        }
        
        public void RegisterBaseHandler(NativeServiceBaseDelegate _handler)
        {
            _baseHandler += _handler;
        }

        //UUID
        public string GetDeviceUUID()
        {
            return UnityEngine.SystemInfo.deviceUniqueIdentifier;
        }

        public DeviceLevel GetDeviceLevel()
        {
            if (_deviceLevel != null)
            {
                return _deviceLevel.CurDeviceLevel;
            }
            return DeviceLevel.Highend; 
        }


        //获取设备基础信息 modelName手机型号  os系统名称  simType手卡类型  tel电话号码 
        public string GetDeviceInfo()
        {
            Dictionary<string, object> retDic = new Dictionary<string, object>();
            retDic.Add("Os", "windows");
            retDic.Add("ModelName", UnityEngine.SystemInfo.deviceName + DateTime.Now.ToString("MM-dd HH:mm:ss").Trim());
            retDic.Add("SimType", 0);
            retDic.Add("DeviceTel", "028-88888888");
            retDic.Add("DeviceUID", UnityEngine.SystemInfo.deviceUniqueIdentifier);
            string retJson = Json.Serialize(retDic);
            return retJson;
        }

        //获取当前平台名称
        public string GetPlatformName()
        {

#if UNITY_IOS
            return "ios";
#elif UNITY_ANDROID
            return "android";
#elif UNITY_EDITOR || UNITY_STANDALONE
			return "win";
#endif
        }

        //0为未知网络；1为WIFI；2为2G；3为3G；4为4G
        public int GetNetType()
        {
            if (_netEnv != null)
            {
                return (int)_netEnv.GetNetEnv();
            }
            return (int)NetEnv.UnKnow;
        }

        //当前网络可用
        public bool GetIsNetEnable()
        {
            if (_netEnv != null)
            {
                return _netEnv.GetNetIsReachable();
            }
            return false;
        }

        //获得自己Ip地址
        public string GetIPAddress()
        {
            if (_netEnv != null)
            {
                return _netEnv.GetLocalIP();
            }
            return "0.0.0.0";
        }

        //获取设备磁盘可用空间
        public float GetDeviceDiskSpace()
        {
            return 0;
        }

        //获取屏幕分辨率
        public Vector2 GetScreenSize()
        {
            return new Vector2(Screen.width, Screen.height);
        }

        public void RebootDevice()
        {
            Application.OpenURL(Application.streamingAssetsPath + "/Reboot.exe");
            Application.Quit();
        }
    }
}
