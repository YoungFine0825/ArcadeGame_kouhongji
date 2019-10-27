/********************************************************************
	created:	17:5:2018   
	filename: 	JWArrayList
	author:		jordenwu
	
	purpose:	基于数组的连续存储列表 用于小数据存储
*********************************************************************/
using System.Collections.Generic;
namespace JW.Common
{
    public class JWArrayList<T>
    {
        private int _initCapacity;
        private readonly int _increase;

        private T[] _buffer;
        private int _count;

        /// <summary>
        /// 容量
        /// </summary>
        public int Capacity
        {
            get
            {
                return _buffer == null ? _initCapacity : _buffer.Length;
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
        /// 数组访问
        /// </summary>
        /// <param name="i">索引</param>
        /// <returns>值</returns>
        public T this[int i]
        {
            get
            {
                if (_buffer == null || i < 0 || i >= _count)
                {
                    return default(T);
                }

                return _buffer[i];
            }

            set
            {
                if (_buffer == null || i < 0 || i >= _count)
                {
                    return;
                }

                _buffer[i] = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="initCapacity">初始容量</param>
        /// <param name="increase">增长量，默认是翻倍增长</param>
        public JWArrayList(int initCapacity = 8, int increase = 0)
        {
            _initCapacity = initCapacity;
            _increase = increase;
        }

        /// <summary>
        /// 扩容
        /// </summary>
        /// <param name="count">需要扩大的容量</param>
        public void ExpandCapacity(int count)
        {
            if (_buffer == null)
            {
                _initCapacity += count;
            }
            else
            {
                AllocateMore(count);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="item">添加值</param>
        public void Add(T item)
        {
            if (_buffer == null || _count == _buffer.Length)
            {
                AllocateMore();
            }

            _buffer[_count++] = item;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="l">添加列表</param>
        public void Add(JWArrayList<T> l)
        {
            if (l == null)
            {
                return;
            }

            for (int i = 0; i < l.Count; i++)
            {
                Add(l[i]);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="l">添加列表</param>
        public void Add(List<T> l)
        {
            if (l == null)
            {
                return;
            }

            for (int i = 0; i < l.Count; i++)
            {
                Add(l[i]);
            }
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="index">插入位置</param>
        /// <param name="item">插入值</param>
        public void Insert(int index, T item)
        {
            if (_buffer == null || _count == _buffer.Length)
            {
                AllocateMore();
            }

            if (index > -1 && index < _count)
            {
                for (int i = _count; i > index; --i)
                {
                    _buffer[i] = _buffer[i - 1];
                }

                _buffer[index] = item;
                ++_count;
            }
            else
            {
                Add(item);
            }
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="item">移除值</param>
        /// <returns>移除成功/失败</returns>
        public bool Remove(T item)
        {
            if (_buffer != null)
            {
                EqualityComparer<T> comp = EqualityComparer<T>.Default;

                for (int i = 0; i < _count; ++i)
                {
                    if (comp.Equals(_buffer[i], item))
                    {
                        --_count;
                        _buffer[i] = default(T);

                        for (int b = i; b < _count; ++b)
                        {
                            _buffer[b] = _buffer[b + 1];
                        }

                        _buffer[_count] = default(T);

                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="index">移除位置</param>
        public void RemoveAt(int index)
        {
            if (_buffer != null && index > -1 && index < _count)
            {
                --_count;
                _buffer[index] = default(T);

                for (int b = index; b < _count; ++b)
                {
                    _buffer[b] = _buffer[b + 1];
                }

                _buffer[_count] = default(T);
            }
        }

        /// <summary>
        /// 全部释放，不释放内存
        /// </summary>
        public void Clear()
        {
            if (_buffer != null)
            {
                for (int i = 0; i < _count; i++)
                {
                    _buffer[i] = default(T);
                }
            }

            _count = 0;
        }

        /// <summary>
        /// 全部释放并释放内存
        /// </summary>
        public void Release()
        {
            _buffer = null;
            _count = 0;
        }

        /// <summary>
        /// 是否包含值
        /// </summary>
        /// <param name="item">检查值</param>
        /// <returns>是否包含</returns>
        public bool Contains(T item)
        {
            if (_buffer == null)
            {
                return false;
            }

            EqualityComparer<T> comp = EqualityComparer<T>.Default;

            for (int i = 0; i < _count; ++i)
            {
                if (comp.Equals(_buffer[i], item))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 根据值查找位置
        /// </summary>
        /// <param name="item">值</param>
        /// <param name="equalityComparer">比较器</param>
        /// <returns>位置 返回-1表示没有找到</returns>
        public int IndexOf(T item, IEqualityComparer<T> equalityComparer = null)
        {
            if (_buffer == null)
            {
                return -1;
            }

            if (equalityComparer == null)
            {
                equalityComparer = EqualityComparer<T>.Default;
            }

            for (int i = 0; i < _count; ++i)
            {
                if (equalityComparer.Equals(_buffer[i], item))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 收缩内存
        /// </summary>
        public void Trim()
        {
            if (_count > 0)
            {
                if (_count < _buffer.Length)
                {
                    T[] newList = new T[_count];
                    for (int i = 0; i < _count; ++i)
                    {
                        newList[i] = _buffer[i];
                    }

                    _buffer = newList;
                }
            }
            else
            {
                _buffer = null;
            }
        }

        /// <summary>
        /// 分配
        /// </summary>
        /// <param name="inc"></param>
        private void AllocateMore(int inc = 0)
        {
            T[] newList = _buffer != null
                ? new T[inc > 0? _buffer.Length + inc : (_increase > 0 ? _buffer.Length + _increase : _buffer.Length << 1)]
                : new T[_initCapacity];
            if (_buffer != null && _count > 0)
            {
                _buffer.CopyTo(newList, 0);
            }

            _buffer = newList;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; ++i)
                yield return _buffer[i];
        }

    }
}
