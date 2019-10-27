/********************************************************************
	created:	2018-05-28   
	filename: 	AssetClass
	author:		jordenwu
	
	purpose:	资产类 cache工厂
*********************************************************************/
using JW.Common;

namespace JW.Framework.Asset
{
    /// <summary>
    /// 资源资产类型管理
    /// </summary>
    public class ResourceAssetClassCache
    {
        private readonly JWArrayList<BaseAsset>[] _cache = new JWArrayList<BaseAsset>[AssetType.BaseAssetTypeCount];

        public bool AddClass(BaseAsset ba)
        {
            if (ba == null)
            {
                JW.Common.Log.LogE("ResourceClassCache.ReleaseClass : invalid parameter");
                return false;
            }

            if (ba.BaseData.Type < 0 || ba.BaseData.Type >= _cache.Length)
            {
                return false;
            }

            if (_cache[ba.BaseData.Type] == null)
            {
                _cache[ba.BaseData.Type] = new JWArrayList<BaseAsset>();
            }

            _cache[ba.BaseData.Type].Add(ba);

            return true;
        }

        public BaseAsset GetClass(int type)
        {
            if (type < 0 || type >= _cache.Length)
            {
                JW.Common.Log.LogE("ResourceClassCache.GetClass : invalid parameter - {0}", type);
                return null;
            }

            if (_cache[type] == null || _cache[type].Count <= 0)
            {
                return null;
            }

            BaseAsset ba = _cache[type][_cache[type].Count - 1];
            _cache[type].RemoveAt(_cache[type].Count - 1);
            return ba;
        }
    }

    public class ResourceAssetClassFactory : IObjectFactory<BaseAsset>
    {
        private readonly ResourceAssetClassCache _classCache;
        private readonly int _type;

        public ResourceAssetClassFactory(ResourceAssetClassCache classCache, int type)
        {
            _classCache = classCache;
            _type = type;
        }

        public BaseAsset CreateObject()
        {
            BaseAsset ba = _classCache.GetClass(_type);
            return ba ?? AssetProcessor.CreateAssetClass(_type);
        }

        public void DestroyObject(BaseAsset o)
        {
            _classCache.AddClass(o);
        }
    }

    public class UIFormAssetClassFactory<T> : IObjectFactory<BaseAsset> where T : UIFormAsset, new()
    {
        private T _instance;

        public UIFormAssetClassFactory(T instance)
        {
            _instance = instance;
        }

        public BaseAsset CreateObject()
        {
            T ret = _instance ?? new T();
            _instance = null;
            return ret;
        }

        public void DestroyObject(BaseAsset o)
        {
        }
    }

    /// <summary>
    /// 资产类 工厂
    /// </summary>
    public static class AssetClassFactory
    {
        private static readonly ResourceAssetClassFactory[] Factory;

        static AssetClassFactory()
        {
            ResourceAssetClassCache cache = new ResourceAssetClassCache();

            Factory = new ResourceAssetClassFactory[AssetType.BaseAssetTypeCount];
            for (int i = 0; i < AssetType.BaseAssetTypeCount; i++)
            {
                Factory[i] = new ResourceAssetClassFactory(cache, i);
            }
        }

        public static IObjectFactory<BaseAsset> GetFactory(int type)
        {
            if (type < 0 || type >= Factory.Length)
            {
                return null;
            }

            return Factory[type];
        }

        /// <summary>
        /// 根据具体UIFormClass获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static IObjectFactory<BaseAsset> GetUIFormAssetClassFactory<T>(T instance) where T : UIFormAsset, new()
        {
            return new UIFormAssetClassFactory<T>(instance);
        }
    }
}