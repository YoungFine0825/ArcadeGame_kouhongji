using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

namespace JW.Framework.Network
{
    public class SpawnPool2 : ObjectBase
    {
        public delegate object DelegateCreateObject(object poolKey, SpawnPool2 pool);
        public delegate void DelegateProcessObject(object obj, object poolKey, SpawnPool2 pool);

        private DelegateCreateObject mOnCreateObject;

        private DelegateProcessObject mOnSpawnObject;

        private DelegateProcessObject mOnDespawnObject;

        private DelegateProcessObject mOnDisposeObject;

        public DelegateCreateObject onCreateObject
        {
            get
            {
                return this.mOnCreateObject;
            }
            set
            {
                this.mOnCreateObject = value;
            }
        }

        public DelegateProcessObject onSpawnObject
        {
            get
            {
                return this.mOnSpawnObject;
            }
            set
            {
                this.mOnSpawnObject = value;
            }
        }

        public DelegateProcessObject onDespawnObject
        {
            get
            {
                return this.mOnDespawnObject;
            }
            set
            {
                this.mOnDespawnObject = value;
            }
        }

        public DelegateProcessObject onDisposeObject
        {
            get
            {
                return this.mOnDisposeObject;
            }
            set
            {

                this.mOnDisposeObject = value;
            }
        }

        private IList<object> mObjectList;
        private Queue<object> mUseObjectList;
        internal PoolManager2 mPoolManager;

        internal object mPoolKey = null;
        internal bool mEnableLog = false;

        private int mLoadedCount;
        private int mAutoPreLoadMin;
        private int mAutoPreLoadMax;
        //private int mPreLoadCount = 0;
        public SpawnPool2()
        {
            mObjectList = new List<object>();
            mUseObjectList = new Queue<object>();
            Reset();
        }
        internal void Reset()
        {
            this.mEnableLog = false;
            this.mPoolManager = null;
            this.mPoolKey = null;
            this.mObjectList.Clear();
            mLoadedCount = 0;
            this.mAutoPreLoadMin = 4;
            this.mAutoPreLoadMax = 32;
            this.mOnCreateObject = null;
            this.mOnSpawnObject = (this.mOnDespawnObject = (this.mOnDisposeObject = null));
            this.mUseObjectList.Clear();
        }
        public object GetPoolKey()
        {
            return this.mPoolKey;
        }
        public int GetLoadedCount()
        {
            return mLoadedCount;
        }
        public void SetAutoMinCount(int count)
        {
            if (mEnableLog)
            {
                JW.Common.Log.LogD("SetAutoMinCount,Pre=" + mAutoPreLoadMin.ToString() + " Now=" + count.ToString());
            }
            this.mAutoPreLoadMin = count;
        }
        public void SetAutoMaxCount(int count)
        {
            if (mEnableLog)
            {
                JW.Common.Log.LogD("SetAutoMaxCount,Pre=" + mAutoPreLoadMax.ToString() + " Now=" + count.ToString());
            }
            this.mAutoPreLoadMax = count;
        }
        private bool TryCreateObject(out object item)
        {
            ++mLoadedCount;
            item = this.mOnCreateObject(mPoolKey, this);
            return item != null;
        }
        public void PreLoad(int count)
        {
            count = count - GetLoadedCount();
            if (count <= 0)
            {
                //Log.e (string.Format("Count PreLoad Less than The LoadedCount,The LoadedCount={0},Count={1}",GetLoadedCount(),count));
                return;
            }
            if (null == mOnCreateObject)
            {
                //Log.e ("The OnCreateObject Function is NUll!!");
                return;
            }
            if (count < mAutoPreLoadMin)
                count = mAutoPreLoadMin;
            else if (count > mAutoPreLoadMax)
                count = mAutoPreLoadMax;
            for (int i = 0; i < count; ++i)
            {
                object item;
                if (!TryCreateObject(out item))
                {
                    throw new InvalidProgramException("CAN'T create null object");
                }
                mObjectList.Add(item);
                mUseObjectList.Enqueue(item);
            }
        }
        /// <summary>
        /// 加载对象
        /// </summary>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public T Spawn<T>() where T : class
        {
            if (null == mOnCreateObject)
            {
                //Log.e ("Spawn Pool The OnCreateObject Function is NUll!!");
                return null;
            }
            if (mUseObjectList.Count <= 0)
            {
                int num = mAutoPreLoadMin;
                if (mEnableLog)
                {
                    //Log.w(new object[]
                    //	{
                    //		"SpawnPool(",
                    //		this.mPoolKey,
                    //		").Spawn<",
                    //		typeof(T),
                    //		">: there is no more objects to spawn, so preload ",
                    //		num,
                    //		" more"
                    //	});
                }
                PreLoad(num + GetLoadedCount());
            }
            object obj = mUseObjectList.Dequeue();
            if (null != mOnSpawnObject)
                mOnSpawnObject(obj, mPoolKey, this);
            return obj as T;
        }
        public void Despawn(object obj)
        {
            if (null == obj || obj.Equals(null))
            {
                //Log.e ("Can't Despawn Null Obj");
                return;
            }

            if (GetLoadedCount() <= mUseObjectList.Count)
            {
                //Log.e ("Despawn ", obj.ToString(), " Failed The UseCount = LoadedCount,useCount=", mUseObjectList.Count, " loadedCount=", GetLoadedCount ());
                return;
            }
            if (null != mOnDespawnObject)
            {
                mOnDespawnObject(obj, mPoolKey, this);
            }

            mUseObjectList.Enqueue(obj);
        }

        public void Close()
        {
            DisposeAllObjects();
        }
        internal void DisposeAllObjects()
        {
            if (this.mOnDisposeObject != null)
            {
                for (int i = mObjectList.Count - 1; i >= 0; --i)
                {
                    mOnDisposeObject(mObjectList[i], mPoolKey, this);
                }
            }
            mObjectList.Clear();
            mUseObjectList.Clear();
        }


    }
}

