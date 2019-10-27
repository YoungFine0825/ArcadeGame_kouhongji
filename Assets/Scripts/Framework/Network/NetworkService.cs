/********************************************************************
	created:	2018-7-25
	author:		jordenwu
	
	purpose:	网络连接全局服务 目前主要为了支持 pemolo
*********************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JW.Common;

namespace JW.Framework.Network
{
    public class NetworkService : Singleton<NetworkService>
    {
        private JWArrayList<NetConnector> _connectors;
        public override bool Initialize()
        {
            _connectors = new JWArrayList<NetConnector>(1);
            return true;
        }

        public override void Uninitialize()
        {
           
        }

        /// <summary>
        /// 获得一个连接
        /// </summary>
        /// <param name="gateIp"></param>
        /// <param name="gatePort"></param>
        /// <param name="gateQueryRoute"></param>
        /// <returns></returns>
        public NetConnector GetConnector(string gateIp,int gatePort,string gateQueryRoute)
        {
            NetConnector cc = new NetConnector();
            cc.Initialize(gateIp, gatePort, gateQueryRoute);
            _connectors.Add(cc);
            return cc;
        }

        /// <summary>
        /// 释放连接
        /// </summary>
        /// <param name="con"></param>
        public void DestroyConnector(NetConnector con)
        {
            if (con == null)
            {
                return;
            }
            if (_connectors != null)
            {
                con.CloseConnect();
                _connectors.Remove(con);
            }
        }

        //驱动
        public void LogicUpdate()
        {
            for(int i = 0; i < _connectors.Count; i++)
            {
                _connectors[i].LogicUpdate(Time.deltaTime);
            }
        }

    }
}
