/********************************************************************
	created:	17:5:2018   
	filename: 	ObjectFactory
	author:		jordenwu
	
	purpose:	简单对象工厂
*********************************************************************/
namespace JW.Common
{
    /// <summary>
    /// 对象工厂接口
    /// </summary>
    /// <typeparam name="T">对象类型，如果有多个类实现同一个接口的话，这就是那个接口</typeparam>
    public interface IObjectFactory<T>
    {
        /// <summary>
        /// 创建对象
        /// </summary>
        /// <returns>创建的对象</returns>
        T CreateObject();

        /// <summary>
        /// 销毁对象
        /// </summary>
        /// <param name="o">待销毁对象</param>
        void DestroyObject(T o);
    }

    /// <summary>
    /// new对象工厂
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <typeparam name="I">对象基础类型，最有可能的就是T或者是几个实现类的公共接口</typeparam>
    public class NewObjectFactory<T, I> : IObjectFactory<I> where T : I, new()
    {
        public I CreateObject()
        {
            return new T();
        }

        public void DestroyObject(I o)
        {
        }
    }

    /// <summary>
    /// 对象工厂方法
    /// </summary>
    /// <typeparam name="T">对象基础类型</typeparam>
    public class ObjectFactoryMethod<T>
    {
        private struct Data
        {
            public int Type;
            public IObjectFactory<T> Factory;
        }

        private readonly JWArrayList<Data> _data = new JWArrayList<Data>();

        /// <summary>
        /// 添加工厂
        /// </summary>
        /// <param name="type">对象标识</param>
        /// <param name="factory">工厂</param>
        public void AddFactory(int type, IObjectFactory<T> factory)
        {
            for (int i = 0; i < _data.Count; i++)
            {
                if (_data[i].Type == type)
                {
                    return;
                }
            }

            Data data;
            data.Type = type;
            data.Factory = factory;
            _data.Add(data);
        }

        /// <summary>
        /// 移除工厂
        /// </summary>
        /// <param name="type">对象标识</param>
        public void RemoveFactory(int type)
        {
            for (int i = 0; i < _data.Count; i++)
            {
                if (_data[i].Type != type)
                {
                    continue;
                }

                _data.RemoveAt(i);
                return;
            }
        }

        /// <summary>
        /// 移除所有工厂
        /// </summary>
        public void RemoveAllFactory()
        {
            _data.Clear();
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="type">对象标识</param>
        /// <returns>对象，返回default(T)标识创建失败</returns>
        public T CreateObject(int type)
        {
            for (int i = 0; i < _data.Count; i++)
            {
                Data data = _data[i];

                if (data.Type != type)
                {
                    continue;
                }

                return data.Factory == null ? default(T) : data.Factory.CreateObject();
            }

            return default(T);
        }
    }
}
