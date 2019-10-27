using System;
using System.Collections;

namespace JW.Framework.Network
{
    public abstract class ReferBase : ObjectBase, IReference
    {
        protected int mReferCount;

        internal int autoReleaseCount;

        private static int sNextObjectId;

        public int objectId
        {
            get;
            private set;
        }

        public void Retain()
        {
            if (this.mReferCount < 0)
            {
                throw new ObjectDisposedException(this.ToString());
            }
            this.mReferCount++;
        }

        public virtual void Release()
        {
            if (this.mReferCount <= 0)
            {
                throw new ObjectDisposedException(this.ToString());
            }
            if (this.mReferCount - this.autoReleaseCount <= 0)
            {
                throw new InvalidOperationException();
            }
            if (--this.mReferCount == 0)
            {
                this.OnDispose();
            }
        }
        /// <summary>
        /// 将对象加入autoReleaseManager队列中
        /// </summary>
        /// <returns>The release.</returns>
        public IReference AutoRelease()
        {
            if (this.mReferCount <= 0)
            {
                throw new ObjectDisposedException(this.ToString());
            }
            if (this.mReferCount - this.autoReleaseCount <= 0)
            {
                throw new InvalidOperationException();
            }
            AutoReleaseManager.GetInstance().Add(this);
            return this;
        }

        public int GetReferenceCount()
        {
            return this.mReferCount;
        }

        public int GetAutoReleaseCount()
        {
            return this.autoReleaseCount;
        }
        protected abstract void OnDispose();

        protected ReferBase()
        {
            this.objectId = ReferBase.sNextObjectId++;
        }

        protected void CheckAvailability()
        {
            if (this.mReferCount <= 0)
            {
                throw new ObjectDisposedException(this.ToString());
            }
        }

        ~ReferBase()
        {
            if (this.mReferCount != 0 || this.autoReleaseCount != 0)
            {
                JW.Common.Log.LogE("ReferBase(), mReferCount");
            }
        }
    }
}

