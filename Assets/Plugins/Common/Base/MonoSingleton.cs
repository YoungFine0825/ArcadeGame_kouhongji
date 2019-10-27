/********************************************************************
	created:	17:5:2018   
	filename: 	MonoSingleton
	author:		jordenwu
	
	purpose:	Monoµ¥Àý¶¨Òå
*********************************************************************/
namespace JW.Common
{
    public abstract class MonoSingleton<T> : MonoSingleton where T : MonoSingleton
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
                _instance = SingletonManager.MonoSingletonGo.ExtAddComponent<T>();
                if (!SingletonManager.Add(_instance))
                {
                    _instance.ExtDestroy();
                    _instance = null;
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
            _instance.ExtDestroy();
            _instance = null;
        }
    }
}
