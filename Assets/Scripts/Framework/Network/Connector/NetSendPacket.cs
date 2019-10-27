using UnityEngine;
using System.Collections.Generic;
using SimpleJson;

namespace JW.Framework.Network
{
    /// <summary>
    /// 发送消息的数据缓存结构
    /// </summary>
    public class NetSendPacket : ReferBase
    {
        public static NetSendPacket New(string route, JsonObject data, uint id)
        {
            NetSendPacket packet = NetVendor.GetInstance()[NetVendor.PoolKey.SendPacket].Spawn<NetSendPacket>();
            packet.InitData(route, data, id);
            packet.Retain();//计数器+1;
            return packet;
        }
        /// <summary>
        /// 是否已经发送过标识位
        /// </summary>
        internal bool mSended = false;

        //
        public bool ReSended = false;

        /// <summary>
        /// 路由
        /// </summary>
        internal string mRoute = null;

        /// <summary>
        /// id
        /// </summary>
        internal uint mId;
        /// <summary>
        /// 携带的信息
        /// </summary>
        internal JsonObject mData = new JsonObject();
        /// <summary>
        /// 添加数据到缓存器里面
        /// </summary>
        /// <param name="args">Arguments.</param>
        public void InitData(string route, JsonObject data, uint id)
        {
            mRoute = route;
            mData = data;
            mId = id;
        }

        public void SetSended(bool sended = true)
        {
            mSended = sended;
        }

        protected virtual void Reset()
        {
            mData.Clear();
            mSended = false;
            mRoute = null;
        }
        protected override void OnDispose()
        {
            Reset();
        }

    }
}

