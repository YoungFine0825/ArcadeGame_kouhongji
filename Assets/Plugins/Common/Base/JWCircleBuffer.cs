/********************************************************************
	created:	17:5:2018   
	filename: 	TZCircleBuffer
	author:		jordenwu
	
	purpose:	循环缓冲
*********************************************************************/
namespace JW.Common
{
    /// <summary>
    /// 循环缓冲
    /// </summary>
    /// <typeparam name="T">缓冲中的数据类型</typeparam>
    public class CircleBuffer<T> where T : class
    {
        // 缓冲区
        private T[] _buffer;

        // 位置
        private int _begin;
        private int _end;

        // 当前放入的数量
        private int _count;

        /// <summary>
        /// 容量
        /// </summary>
        public int Capacity
        {
            get
            {
                return _buffer.Length;
            }
        }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count
        {
            get
            {
                return _count;
            }
        }

        /// <summary>
        /// 空
        /// </summary>
        public bool Empty
        {
            get
            {
                return _count == 0;
            }
        }

        /// <summary>
        /// 满
        /// </summary>
        public bool Full
        {
            get
            {
                return _count == _buffer.Length;
            }
        }

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>索引值</returns>
        public T this[int index]
        {
            get
            {
                if (index >= Count)
                {
                    return null;
                }

                index += _begin;
                index %= Capacity;

                return _buffer[index];
            }

            set
            {
                if (index >= Count)
                {
                    return;
                }

                index += _begin;
                index %= Capacity;

                _buffer[index] = value;
            }
        }

        public CircleBuffer()
        {
        }

        public CircleBuffer(int capacity)
        {
            Create(capacity);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="capacity">缓冲大小</param>
        public void Create(int capacity)
        {
            _buffer = new T[capacity];

            _begin = 0;
            _end = 0;

            _count = 0;
        }

        public void Create(T[] buffer)
        {
            if (null == buffer)
            {
                return;
            }
            _buffer = buffer;

            _begin = 0;
            _end = 0;

            _count = buffer.Length;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Destroy()
        {
            _buffer = null;

            _begin = 0;
            _end = 0;

            _count = 0;
        }

        /// <summary>
        /// 重设大小
        /// </summary>
        /// <param name="capacity">重设的大小</param>
        public void Resize(int capacity)
        {
            if (capacity <= Count)
            {
                return;
            }

            T[] buf = new T[capacity];
            for (int i = 0; i < Count; i++)
            {
                buf[i] = this[i];
            }

            _buffer = buf;
            _begin = 0;
            _end = _count % capacity;
        }

        /// <summary>
        /// 清除
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < _buffer.Length; i++)
            {
                _buffer[i] = default(T);
            }

            _begin = 0;
            _end = 0;

            _count = 0;
        }

        /// <summary>
        /// 添加一个数据
        /// </summary>
        /// <param name="value"></param>
        public bool Push(T value)
        {
            if (Full)
            {
                return false;
            }

            if (_end < 0 || _end >= _buffer.Length)
            {
                return false;
            }

            _buffer[_end] = value;

            ++_end;
            _end %= Capacity;

            ++_count;

            return true;
        }

        /// <summary>
        /// 弹出一个数据
        /// </summary>
        /// <param name="value">返回数据</param>
        /// <returns>返回成功/失败</returns>
        public bool Pop(out T value)
        {
            if (Empty)
            {
                value = default(T);
                return false;
            }

            if (_begin < 0 || _begin >= _buffer.Length)
            {
                value = default(T);
                return false;
            }

            value = _buffer[_begin];
            _buffer[_begin] = default(T);

            ++_begin;
            _begin %= Capacity;

            --_count;

            return true;
        }

        /// <summary>
        /// 弹出数据
        /// </summary>
        public void Pop()
        {
            T value;
            Pop(out value);
        }

        /// <summary>
        /// 获取一个数据
        /// </summary>
        /// <param name="value">返回数据</param>
        /// <returns>获取成功/失败</returns>
        public bool Peek(out T value)
        {
            if (Empty)
            {
                value = default(T);
                return false;
            }

            if (_begin < 0 || _begin >= _buffer.Length)
            {
                value = default(T);
                return false;
            }

            value = _buffer[_begin];

            return true;
        }

        /// <summary>
        /// 最后一个数据
        /// </summary>
        /// <param name="value">返回数据</param>
        /// <returns>获取成功/失败</returns>
        public bool Last(out T value)
        {
            if (Empty)
            {
                value = default(T);
                return false;
            }

            value = this[_count - 1];
            return true;
        }
    }
}
