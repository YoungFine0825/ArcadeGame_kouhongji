using UnityEngine;
using System.Collections.Generic;
using System;

namespace JW.Framework.Network
{
    public class PoolManager2 : ObjectBase
    {
        internal IDictionary<object, SpawnPool2> mPoolDict;

        public PoolManager2()
        {
            mPoolDict = new Dictionary<object, SpawnPool2>();
        }
        public SpawnPool2 this[object poolKey]
        {
            get
            {
                if (poolKey == null)
                    throw new ArgumentNullException();
                SpawnPool2 pool = TryGetPool(poolKey);
                if (null == pool)
                {
                    //audo 简单实现SpwanPool的使用
                    pool = new SpawnPool2();
                    pool.mPoolManager = this;
                    pool.mPoolKey = poolKey;
                    mPoolDict[poolKey] = pool;
                }
                return pool;
            }
        }
        public SpawnPool2 TryGetPool(object poolKey)
        {
            if (poolKey == null)
            {
                throw new ArgumentNullException("poolKey CAN'T be null");
            }
            SpawnPool2 result = null;
            if (!this.mPoolDict.TryGetValue(poolKey, out result))
            {
                result = null;
            }
            return result;
        }
        public void ClosePool(object poolKey)
        {
            if (poolKey == null)
            {
                throw new ArgumentNullException("poolKey CAN'T be null");
            }
            SpawnPool2 pool = TryGetPool(poolKey);
            if (null != pool)
            {
                pool.DisposeAllObjects();
                mPoolDict.Remove(poolKey);
            }
        }
        public void CloseAllPools()
        {
            IEnumerator<KeyValuePair<object, SpawnPool2>> itor = mPoolDict.GetEnumerator();
            while (itor.MoveNext())
            {
                itor.Current.Value.DisposeAllObjects();
            }
            mPoolDict.Clear();
        }
        internal void RemovePool(object poolKey)
        {
            if (mPoolDict.ContainsKey(poolKey))
                mPoolDict.Remove(poolKey);
        }
    }
}

