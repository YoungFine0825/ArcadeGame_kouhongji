using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
namespace JW.Framework.Network
{

    public sealed class SafedQueue<T>
    {
        #region private Fields
        private int isTaked = 0;
        private Queue<T> queue = new Queue<T>();
        #endregion

        public void Enqueue(T t)
        {
            try
            {
                while (Interlocked.Exchange(ref isTaked, 1) != 0)
                {
                }
                this.queue.Enqueue(t);
            }
            finally
            {
                Thread.VolatileWrite(ref isTaked, 0);
            }
        }

        public T Dequeue()
        {
            try
            {
                while (Interlocked.Exchange(ref isTaked, 1) != 0)
                {
                }
                T t = this.queue.Dequeue();
                return t;
            }
            finally
            {
                Thread.VolatileWrite(ref isTaked, 0);
            }
        }

        public int Count
        {
            get
            {
                try
                {
                    while (Interlocked.Exchange(ref isTaked, 1) != 0)
                    {
                    }

                    return this.queue.Count;
                }
                finally
                {
                    Thread.VolatileWrite(ref isTaked, 0);
                }
            }
        }
        public void Clear()
        {
            try
            {
                while (Interlocked.Exchange(ref isTaked, 1) != 0)
                {
                }
                this.queue.Clear();
            }
            finally
            {
                Thread.VolatileWrite(ref isTaked, 0);
            }
        }
    }
}
