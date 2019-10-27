using System;
using System.Collections.Generic;
using JW.Common;
namespace JW.Framework.Network
{
    public class AutoReleaseManager : Singleton<AutoReleaseManager>
    {
        private IList<ReferBase> mPool;

        internal void UpdateInternal()
        {
            for (int i = 0; i < mPool.Count; ++i)
            {
                ReferBase item = mPool[i];
                item.autoReleaseCount--;
                item.Release();
            }
        }
        public void Add(ReferBase obj)
        {
            mPool.Add(obj);
            obj.autoReleaseCount++;
        }

        public override bool Initialize()
        {
            mPool = new List<ReferBase>();
            return true;
        }

        public override void Uninitialize()
        {
            mPool = null;
        }
    }
}

