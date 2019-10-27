using UnityEngine;
using System.Collections;
using JW.Common;

namespace JW.Framework.Network
{
    /// <summary>
    /// 网络发送缓存包缓存管理器
    /// </summary>
    public class NetVendor : Singleton<NetVendor>
    {
        public override bool Initialize()
        {
            mPoolManager = new PoolManager2();
            SpawnPool2 pool = mPoolManager[PoolKey.SendPacket];
            pool.onCreateObject = onCreatObject;
            pool.onSpawnObject = onSpawnObject;
            pool.onDespawnObject = onDesPawnObject;
            return true;
        }

        public override void Uninitialize()
        {
            if (null != mPoolManager)
                mPoolManager.CloseAllPools();
        }

        public enum PoolKey
        {
            SendPacket,
        }

        public PoolManager2 mPoolManager;
        public SpawnPool2 this[object poolKey]
        {
            get
            {
                if (mPoolManager == null)
                    mPoolManager = new PoolManager2();
                return mPoolManager[poolKey];
            }
        }
        public SpawnPool2 GetPool(object poolKey)
        {
            if (null == mPoolManager)
                mPoolManager = new PoolManager2();
            return mPoolManager[poolKey];
        }

        protected object onCreatObject(object poolKey, SpawnPool2 pool)
        {
            object obj = null;
            switch ((PoolKey)poolKey)
            {
                case PoolKey.SendPacket:
                    obj = new NetSendPacket();
                    break;
            }
            return obj;
        }
        protected void onSpawnObject(object obj, object poolKey, SpawnPool2 pool)
        {
            IReference refence = obj as IReference;
            if (null != refence)
            {
                refence.Retain();
                refence.AutoRelease();
            }
            //return refence;
        }
        protected void onDesPawnObject(object obj, object poolkey, SpawnPool2 pool)
        {

        }

    }
}

