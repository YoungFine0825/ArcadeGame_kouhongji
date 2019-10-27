/********************************************************************
	created:	2018-05-28   
	filename: 	AssetService
	author:		jordenwu
	
	purpose:	逻辑层资产服务
*********************************************************************/
using JW.Common;
using System;
using JW.Res;
using JW.Framework.MVC;
using UnityEngine;

namespace JW.Framework.Asset
{
    /// <summary>
    /// 整个游戏的资产服务
    /// </summary>
    public class AssetService : Singleton<AssetService>
    {
        //UI状态缓存 状态数目
        private const int CacheUIStateCount = 1;
        //todo
        public static bool AlwaysGc=true;
        //
        public System.Action<bool> LuaGCHook = null;

        //根
        private Transform _root;
        //资产管理
        private AssetManager _assetManager;
        //资产加载器
        private AssetLoader _loader;
        //资产预加载器
        private AssetPreloader _preloader;
        //bundle 资源中间控制
        private BundleMediator _bundleMediator;

        private JWArrayList<string> _tempList;
        private JWArrayList<string> _tempList2;

        public override bool Initialize()
        {
            _root = new GameObject("AssetService").transform;
            _root.gameObject.ExtDontDestroyOnLoad();

            _assetManager = new AssetManager();
            _assetManager.Create(_root);

            //必须先构建
            _bundleMediator= _root.gameObject.AddComponent<BundleMediator>();
            _bundleMediator.Initialize();
             //
            _loader = _root.gameObject.AddComponent<AssetLoader>();
            _loader.Initialize(_assetManager);
            //
            _preloader = _root.gameObject.AddComponent<AssetPreloader>();
            _preloader.Initialize(_loader);

            _tempList = new JWArrayList<string>();
            _tempList2 = new JWArrayList<string>();

            return true;
        }

        public override void Uninitialize()
        {
            Destroy();

            _assetManager.Destroy();
            _assetManager = null;

            _root.gameObject.ExtDestroy();
            _root = null;

            _tempList = null;
            _tempList2 = null;

            LuaGCHook = null;
        }

        public void Destroy()
        {
            if (_preloader != null)
            {
                _preloader.Uninitialize();
                _preloader.ExtDestroy();
                _preloader = null;
            }

            if (_loader != null)
            {
                _loader.Uninitialize();
                _loader.ExtDestroy();
                _loader = null;
            }
            if (_bundleMediator != null)
            {
                _bundleMediator.Uninitialize();
                _bundleMediator.ExtDestroy();
                _bundleMediator = null;
            }
        }

        //游戏开始预加载 加载常驻资源包
        public void StartPreloadResidentBundle(Action callBack)
        {
            if (_preloader != null)
            {
                _preloader.StartResidentBundle(callBack);
            }
        }


        /// 游戏主资源更新检查后启动预加载
        public void StartPreloadAfterResident(Action<bool> allCompletedCallback)
        {
            if (_preloader != null)
            {
                _preloader.StartAfterUpdate(allCompletedCallback);
            }
        }

        /// <summary>
        /// 清理缓存的资源
        /// </summary>
        /// <param name="uiStateHistory">UI状态历史列表（=null标识没有UI状态）</param>
        public void ClearUIStateCache(JWArrayList<string> uiStateHistory)
        {
            if (_loader == null)
            {
                return;
            }

            _tempList.Clear();
            _tempList2.Clear();
            if (uiStateHistory != null)
            {
                _assetManager.GetCacheUIState(ref _tempList);
                _loader.GetUIState(ref _tempList);

                for (int i = uiStateHistory.Count - 1; i >= 0; i--)
                {
                    string stateName = uiStateHistory[i];
                    if (string.IsNullOrEmpty(stateName) || _tempList.IndexOf(stateName, StringComparer.OrdinalIgnoreCase) == -1)
                    {
                        continue;
                    }

                    _tempList2.Add(stateName);
                    if (_tempList2.Count == CacheUIStateCount)
                    {
                        break;
                    }
                }
            }

            _assetManager.ClearCache(_tempList2);
            _loader.ClearTask(_tempList2);

            if (AlwaysGc)
            {
                if (LuaGCHook != null)
                {
                    LuaGCHook(true);
                }
                ResService.GetInstance().UnloadUnusedAssets();
                return;
            }
        }

        #region 对外

        //清理所有
        public void UnloadAllUsingAssets()
        {
            //
            _assetManager.CleanAllUsing();
            //
            ResService.GetInstance().UnloadUnusedAssets();
        }


        private void AddLoad(int loadPriority, IAssetLoadCallback callback, int type, string filename, int life, string stateName, int count)
        {
            if (string.IsNullOrEmpty(filename) || count < 1)
            {
                JW.Common.Log.LogE("AssetService.AddLoad : invalid parameter");
                return;
            }
            if (_loader == null)
            {
                return;
            }

            AssetData data=new AssetData();
            data.Priority = loadPriority;
            data.Callback = callback;
            data.Type = type;
            data.Name = filename;
            //data.Filename = todo Lod
            data.Filename = filename;

            data.Factory = AssetClassFactory.GetFactory(type);
            data.Life = life;

            data.StateName = stateName;
            data.Count = count;

            _loader.AddTask(ref data);
        }


        //异步加载
        public void LoadAsyn(int type, int loadPriority, string filename, int life, IAssetLoadCallback callback,int cnt=1)
        {
            string stateName = string.Empty;

            if (life == LifeType.Manual)
            {
                stateName = "Manual";
            }
            else if (life == LifeType.UIState)
            {
                stateName = UIStateService.IsValidate() ? UIStateService.GetInstance().CurrentStateName : string.Empty;
            }
            //
            AddLoad(loadPriority, callback, type, filename, life, stateName, cnt);
        }

        /// <summary>
        /// 取消异步加载
        /// </summary>
        /// <param name="callback">回调接口</param>
        /// <param name="assetName">资源名或者类名（=null 标识取消所有跟回调接口相关的异步加载）</param>
        public void CancelAsyn(IAssetLoadCallback callback, string assetName = null)
        {
            if (_loader == null)
            {
                return;
            }
            _loader.CancelTask(callback, assetName);
        }


        /// <summary>
        /// 装载具体窗口对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="life">生命周期</param>
        /// <returns>对象</returns>
        public UIFormAsset LoadFormAsset<T>(int life = LifeType.Immediate) where T : UIFormAsset, new()
        {
            //创建
            T instance = new T();
            Type t = typeof(T);

            AssetData data;
            data.Priority = LoadPriority.Wait;
            data.Callback = null;

            data.Type = AssetType.UIForm;
            data.Name = t.Name;
            data.Filename = instance.GetPath();

            data.Factory = AssetClassFactory.GetUIFormAssetClassFactory(instance);

            data.Life = life;
            data.StateName = string.Empty;

            data.Count = 1;
            //
            BaseAsset ba = _assetManager.Load(ref data, false);
            if (ba == null)
            {
                JW.Common.Log.LogE("AssetService.LoadAsset : failed to load asset - {0}", t.Name);
                return null;
            }
            //
            AdjustAssetLife(ba, life);

            return (UIFormAsset)ba;
        }


        /// <summary>
        /// 加载UI资产
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="lifeType"></param>
        /// <returns></returns>
        public UIAsset LoadUIAsset(string filename,int lifeType = LifeType.Immediate)
        {
            return (UIAsset)LoadAsset(AssetType.UI, filename, lifeType);
        }


        /// <summary>
        /// 装载模型资源
        /// </summary>
        /// <param name="filename">资源名</param>
        /// <param name="lifeType">强制以什么生命周期加载</param>
        /// <returns>资源</returns>
        public ModelAsset LoadModelAsset(string filename, int lifeType = LifeType.Immediate)
        {
            return (ModelAsset)LoadAsset(AssetType.Model, filename, lifeType);
        }


        /// <summary>
        /// 装载实例化资源
        /// </summary>
        /// <param name="filename">资源名</param>
        /// <param name="lifeType">强制以什么生命周期加载</param>
        /// <returns>资源</returns>
        public BaseAsset LoadInstantiateAsset(string filename, int lifeType = LifeType.Immediate)
        {
            return LoadAsset(AssetType.Instantiate, filename, lifeType);
        }


        /// <summary>
        /// 加载声音资源
        /// </summary>
        /// <param name="filename">资源名</param>
        /// <param name="lifeType">强制以什么生命周期加载</param>
        /// <returns>资源</returns>
        public AudioAsset LoadAudioAsset(string filename, int lifeType = LifeType.Manual, string fileExt = ".mp3")
        {
            return (AudioAsset)LoadAsset(AssetType.Audio, filename, lifeType);
        }

        /// <summary>
        /// 加载精灵资源
        /// </summary>
        /// <param name="filename">资源名</param>
        /// <param name="lifeType">强制以什么生命周期加载</param>
        /// <returns>资源</returns>
        public SpriteAsset LoadSpriteAsset(string filename, int lifeType = LifeType.Immediate)
        {
            return (SpriteAsset)LoadAsset(AssetType.Sprite, filename, lifeType);
        }

        /// <summary>
        /// 装载原始资源
        /// </summary>
        /// <param name="filename">资源名</param>
        /// <param name="lifeType">强制以什么生命周期加载</param>
        /// <returns>资源</returns>
        public BaseAsset LoadPrimitiveAsset(string filename, int lifeType = LifeType.Immediate)
        {
            return LoadAsset(AssetType.Primitive, filename, lifeType);
        }


        /// <summary>
        /// 卸载
        /// </summary>
        /// <param name="ba">资源</param>
        public void Unload(BaseAsset ba)
        {
            if (ba == null)
            {
                return;
            }

            if (_assetManager != null)
            {
                _assetManager.Unload(ba, SingletonState == SingletonStateEnum.Destroying);
            }
            else
            {
                if (!AssetProcessor.ProcessDestroy(ba))
                {
                    return;
                }

                if (ba.Resource != null)
                {
                    ResService.UnloadResource(ba.Resource);
                    ba.Resource = null;
                }

                ba.BaseData.Factory.DestroyObject(ba);
            }
        }


        #endregion
        /// 加载
        private BaseAsset LoadAsset(int type, string filename, int life, bool clone = false)
        {
            AssetData data;
            data.Priority = LoadPriority.Wait;
            data.Callback = null;

            data.Type = type;

            data.Name = filename;
            //data.Filename = todo Lod FileName
            data.Filename = filename;

            data.Factory = AssetClassFactory.GetFactory(type);

            data.Life = life;
            data.StateName = GetLifeStateName(life);

            data.Count = 1;

            BaseAsset ba = _assetManager.Load(ref data, clone);

            if (ba == null)
            {
                JW.Common.Log.LogE("AssetService.LoadAsset : failed to load asset - {0}", data.Filename);
                return null;
            }

            if (ba.BaseData.Type != type)
            {
                JW.Common.Log.LogE("AssetService.LoadAsset : type mismatch - {0}, type - {1};{2}", data.Filename, ba.BaseData.Type, type);
                Unload(ba);
                return null;
            }

            AdjustAssetLife(ba, life);

            return ba;
        }

        private void AdjustAssetLife(BaseAsset ba, int life)
        {
            if (life >= ba.BaseData.Life)
            {
                return;
            }

            ba.BaseData.Life = life;
            ba.BaseData.StateName = GetLifeStateName(life);
        }

        private string GetLifeStateName(int life)
        {
            switch (life)
            {
                case LifeType.Manual:
                    return "Manual";

                case LifeType.UIState:
                    return UIStateService.IsValidate() ? UIStateService.GetInstance().CurrentStateName : string.Empty;
            }

            return string.Empty;
        }

        public AssetManager GetAssetManager()
        {
            return _assetManager;
        }


    }
}
