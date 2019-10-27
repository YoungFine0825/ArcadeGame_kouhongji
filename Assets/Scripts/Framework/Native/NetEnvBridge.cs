/********************************************************************
	created:	24:11:2017   19:14
	author:		jordenwu
	purpose:	网络环境 变化 对接
*********************************************************************/
using System.Collections.Generic;
using UnityEngine;
using JW.Common;

namespace JW.Framework.Native
{
    /// <summary>
    /// 网络环境定义 //0为未知网络；1为WIFI；2为2G；3为3G；4为4G
    /// </summary>
    public enum NetEnv
    {
        UnConnect = -1,//未连接网络
        UnKnow = 0,//未知网络
        Wifi = 1,
        Net2G = 2,
        Net3G = 3,
        Net4G = 4,
        Net5G = 5,
        NetWWAN = 6, //蜂窝网络统称
    }

    //桥接到本地代码
    public class NetEnvBridge : MonoBehaviour
    {
        //构建
        private static GameObject _sGo = null;
        //创建
        public static NetEnvBridge CreateBridge()
        {
            NetEnvBridge ret = null;
            if (_sGo == null)
            {
                _sGo = new GameObject("NetEnvBridge");
                _sGo.transform.parent = JW.Common.SingletonManager.MonoSingletonGo.transform;
            }
            ret = _sGo.ExtAddComponent<NetEnvBridge>(true);
            return ret;
        }
        //销毁
        public static void DestroyBridge()
        {
            if (_sGo != null)
            {
                Destroy(_sGo);
                _sGo = null;
            }
        }

        //获取自己ip地址url
        private static readonly string REQ_IP_Url = "http://pv.sohu.com/cityjson?ie=utf-8";
        //变化回调
        private System.Action<int> _handler;
        //初始化
        public void Initialize(System.Action<int> callBack)
        {
            _handler = callBack;
        }

        //反初始化
        public void Uninitialize()
        {

            return;
        }

        /// <summary>
        /// 获取本地Ip地址
        /// </summary>
        /// <returns></returns>
        public string GetLocalIP()
        {
            if (GetNetIsReachable() == false)
            {
                //无网络
                return "0.0.0.0";
            }
            //同步获取
            string ret = JW.Framework.Http.HttpHelper.SyncGetHttpResponse(REQ_IP_Url);
            if (string.IsNullOrEmpty(ret) || ret.Equals("error"))
            {
                return "0.0.0.0";
            }
            else
            {
                int f = ret.IndexOf('{');
                int l = ret.IndexOf('}');
                if ((f != -1) && (l != -1))
                {
                    string json = ret.Substring(f, (l - f) + 1);
                    Dictionary<string, object> dd = Json.Deserialize(json) as Dictionary<string, object>;
                    if (dd != null)
                    {
                        object vv;
                        if (dd.TryGetValue("cip", out vv))
                        {
                            return vv as string;
                        }
                        else
                        {
                            return "0.0.0.0";
                        }
                    }
                    else
                    {
                        return "0.0.0.0";
                    }
                }
                else
                {
                    return "0.0.0.0";
                }
            }
        }

        /// <summary>
        /// 获取当前设备的网络类型
        /// </summary>
        /// <returns></returns>
        public NetEnv GetNetEnv()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                return NetEnv.UnConnect;
            }
            if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                return NetEnv.Net4G;
            }
            if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {
                return NetEnv.Wifi;
            }
            return NetEnv.UnConnect;
        }

        /// <summary>
        /// 获取当前设备是否能联网
        /// </summary>
        /// <returns></returns>
        public bool GetNetIsReachable()
        {
            return Application.internetReachability != NetworkReachability.NotReachable;
        }

        public void OnNetStatusChanged(string msg)
        {
            JW.Common.Log.LogD("OnNetStatusChanged:" + msg);
            int netType = 0;
            int.TryParse(msg, out netType);
            if (_handler != null)
            {
                _handler(netType);
            }
        }

        public void StartListen()
        {
            return;
        }
        public void StopListen()
        {
            return;
        }
    }
}
