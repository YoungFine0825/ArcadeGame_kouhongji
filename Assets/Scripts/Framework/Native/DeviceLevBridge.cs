/********************************************************************
	created:	2018-10-13
	author:		jordenwu
	
	purpose:	设备等级判定
*********************************************************************/
using System;
using UnityEngine;

namespace JW.Framework.Native
{
    /// <summary>
    /// 设备性能等级
    /// </summary>
    public enum DeviceLevel
    {
        VeryPoor = 0,   //垃圾
        Lowend,         //低端
        Mediumend,      //中端
        Highend,        //高端
    }

    public class DeviceLevBridge
    {
        //当前设备等级
        private DeviceLevel _deviceLevel = DeviceLevel.Highend;

        //初始化
        public void Initialize()
        {
            CheckDeviceLevWin();
        }

        //反初始化
        public void Uninitialize()
        {

            return;
        }

        public DeviceLevel CurDeviceLevel
        {
            get
            {
                return _deviceLevel;
            }
        }

        /// 是否处于低内存告警
        public bool IsLowMemoryWarning
        {
            get
            {
                return false;
            }
        }


        private void CheckDeviceLevWin()
        {
            //CPU
            int processorCount = SystemInfo.processorCount;
            int processorFrequency = SystemInfo.processorFrequency;
            string processorType = SystemInfo.processorType;
            JW.Common.Log.LogD("处理器{0}:核心{1:D}:频率:{2:D}", processorType, processorCount, processorFrequency);

            // 显卡的供应商
            string graphicsDeviceVendor = UnityEngine.SystemInfo.graphicsDeviceVendor;
            int graphicsDeviceVendorID = UnityEngine.SystemInfo.graphicsDeviceVendorID;
            string graphicsDeviceName = UnityEngine.SystemInfo.graphicsDeviceName;
            string graphicsDeviceVersion = UnityEngine.SystemInfo.graphicsDeviceVersion;
            //int graphicsDeviceID = UnityEngine.SystemInfo.graphicsDeviceID;
            int graphicsMemorySize = UnityEngine.SystemInfo.graphicsMemorySize;
            //bool graphicsMultiThreaded = UnityEngine.SystemInfo.graphicsMultiThreaded;
            int graphicsShaderLevel = UnityEngine.SystemInfo.graphicsShaderLevel;
            int maxTextureSize = UnityEngine.SystemInfo.maxTextureSize;

            JW.Common.Log.LogD("显卡供应商{0}:版本:{1}:名称:{2}:显存:{3:D}:Max纹理:{4:D}:Shader等级:{5:D}", graphicsDeviceVendor+"ID="+graphicsDeviceVendorID.ToString(), graphicsDeviceVersion,graphicsDeviceName, graphicsMemorySize, maxTextureSize, graphicsShaderLevel);

            if (graphicsMemorySize > 2000)
            {
                _deviceLevel = DeviceLevel.Highend;
            }
            else
            {
                _deviceLevel = DeviceLevel.Lowend;
            }
        }
    }
}