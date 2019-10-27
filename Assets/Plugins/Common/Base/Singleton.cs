/********************************************************************
	created:	17:5:2018   
	filename: 	Singleton
	author:		jordenwu
	
	purpose:	单例定义
*********************************************************************/
namespace JW.Common
{
    public abstract class Singleton<T> : Singleton where T : Singleton, new()
    {
        private static T _instance;

        public static bool IsValidate()
        {
            return _instance != null;
        }

        public static T Instance { get { return GetInstance(); } }

        public static T GetInstance()
        {
            if (_instance == null)
            {
                _instance = new T();
                if (!SingletonManager.Add(_instance))
                {
                    _instance = null;
                    return null;
                }
            }

            return _instance;
        }

        public static void DestroyInstance()
        {
            if (_instance == null)
            {
                return;
            }

            SingletonManager.Remove(_instance);
        }

        public override void InternalUninitialize()
        {
            _instance = null;
        }
    }
}
