/********************************************************************
	created:	17:5:2018   
	filename: 	SingletonManager
	author:		jordenwu
	
	purpose:	全局单例管理器
*********************************************************************/
using UnityEngine;

namespace JW.Common
{
    public class SingletonStateEnum
    {
        /// <summary>
        /// 未定义
        /// </summary>
        public const int Undefine = 0;

        /// <summary>
        /// 正在创建中
        /// </summary>
        public const int Creating = 1;

        /// <summary>
        /// 正常
        /// </summary>
        public const int Normal = 2;

        /// <summary>
        /// 正在销毁中
        /// </summary>
        public const int Destroying = 3;
    }

    public abstract class Singleton
    {
        /// <summary>
        /// 单件状态，取值SingletonStateEnum
        /// </summary>
        public int SingletonState;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns>初始化成功/失败</returns>
        public abstract bool Initialize();

        /// <summary>
        /// 反初始化
        /// </summary>
        /// <returns></returns>
        public abstract void Uninitialize();

        /// <summary>
        /// 内部反初始化，不需要具体派生类实现
        /// </summary>
        public abstract void InternalUninitialize();
    }

    public abstract class MonoSingleton : MonoBehaviour
    {
        /// <summary>
        /// 单件状态，取值SingletonStateEnum
        /// </summary>
        public int SingletonState;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns>初始化成功/失败</returns>
        public abstract bool Initialize();

        /// <summary>
        /// 反初始化
        /// </summary>
        /// <returns></returns>
        public abstract void Uninitialize();

        /// <summary>
        /// 内部反初始化，不需要具体派生类实现
        /// </summary>
        public abstract void InternalUninitialize();
    }

    /// <summary>
    /// 全局单例管理
    /// </summary>
    public static class SingletonManager
    {
        private static readonly JWArrayList<Singleton> Singletons = new JWArrayList<Singleton>();

        private static GameObject _monoSingletonGo;
        private static readonly JWArrayList<MonoSingleton> MonoSingletons = new JWArrayList<MonoSingleton>();

        public static GameObject MonoSingletonGo
        {
            get
            {
                if (_monoSingletonGo == null)
                {
                    _monoSingletonGo = new GameObject("MonoSingleton");
                    _monoSingletonGo.ExtDontDestroyOnLoad();
                }

                return _monoSingletonGo;
            }
        }

        public static bool Add(Singleton instance)
        {
            if (instance == null || Singletons.Contains(instance))
            {
                return false;
            }

            instance.SingletonState = SingletonStateEnum.Creating;
            if (!instance.Initialize())
            {
                instance.SingletonState = SingletonStateEnum.Undefine;
                return false;
            }

            instance.SingletonState = SingletonStateEnum.Normal;
            Singletons.Add(instance);
            return true;
        }

        public static bool Add(MonoSingleton instance)
        {
            if (instance == null || MonoSingletons.Contains(instance))
            {
                return false;
            }

            instance.SingletonState = SingletonStateEnum.Creating;
            if (!instance.Initialize())
            {
                instance.SingletonState = SingletonStateEnum.Undefine;
                return false;
            }

            instance.SingletonState = SingletonStateEnum.Normal;
            MonoSingletons.Add(instance);
            return true;
        }

        public static void Remove(Singleton instance)
        {
            if (instance == null)
            {
                return;
            }

            Singletons.Remove(instance);

            instance.SingletonState = SingletonStateEnum.Destroying;
            instance.Uninitialize();
            instance.InternalUninitialize();
            instance.SingletonState = SingletonStateEnum.Undefine;
        }

        public static void Remove(MonoSingleton instance)
        {
            if (instance == null)
            {
                return;
            }

            MonoSingletons.Remove(instance);

            instance.SingletonState = SingletonStateEnum.Destroying;
            instance.Uninitialize();
            instance.InternalUninitialize();
            instance.SingletonState = SingletonStateEnum.Undefine;
        }

        public static void Clear()
        {
            for (int i = Singletons.Count - 1; i >= 0; --i)
            {
                Singletons[i].SingletonState = SingletonStateEnum.Destroying;
                Singletons[i].Uninitialize();
                Singletons[i].InternalUninitialize();
                Singletons[i].SingletonState = SingletonStateEnum.Undefine;
            }

            Singletons.Clear();

            for (int i = MonoSingletons.Count - 1; i >= 0; --i)
            {
                MonoSingletons[i].SingletonState = SingletonStateEnum.Destroying;
                MonoSingletons[i].Uninitialize();
                MonoSingletons[i].InternalUninitialize();
                MonoSingletons[i].SingletonState = SingletonStateEnum.Undefine;
            }

            MonoSingletons.Clear();

            if (null != _monoSingletonGo)
            {
                _monoSingletonGo.ExtDestroy();
            }
        }
    }
}
