/********************************************************************
	created:	2018-05-28   
	filename: 	AssetLoader
	author:		jordenwu
	
	purpose:	资产异步加载器
*********************************************************************/
using System;
using System.Collections;
using JW.Common;
using JW.Res;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JW.Framework.Asset
{
    public class AssetLoader : MonoBehaviour
    {
        public static int LoadingWait = 1;
        //锁
        private static readonly object InstructionEnd = new object();
        //加载状态
        private const int LoadStateLoading = 0;
        private const int LoadStateSuccess = 1;
        private const int LoadStateFail = 2;
        //记录数据
        private struct LoadData
        {
            public AssetData Data;
            public int LoadBundleState;
            public ResObjRequest Request;
        }

        // 常量
        private AssetManager _assetManager;
       
        // 运行时数据记录需要加载的资源数据
        private JWArrayList<AssetData> _data;
        private int _loadAssetBundlePriority;

        //正在请求加载的数据
        private JWArrayList<LoadData> _resourceRequesting;
        
        private ResObjRequest _resourceDiscardRequest;

        //是否正在同步加载
        private bool _synLoading;

        /// <summary>
        /// 加载是否处于繁忙状态
        /// </summary>
        public bool IsBusy
        {
            get
            {
                if (_synLoading)
                {
                    return true;
                }

                for (int i = 0; i < _resourceRequesting.Count; i++)
                {
                    if (_resourceRequesting[i].Data.Priority < LoadPriority.Preprocess)
                    {
                        return true;
                    }
                }

                for (int i = 0; i < _data.Count; i++)
                {
                    if (_data[i].Priority < LoadPriority.Preprocess)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="assetManager">资产管理器</param>
        public void Initialize(AssetManager assetManager)
        {
            _assetManager = assetManager;
            _data = new JWArrayList<AssetData>();
            _resourceRequesting = new JWArrayList<LoadData>();
            //异步一直在运行
            StartCoroutine(AsynchronousLoad());
        }

        /// <summary>
        /// 反初始化
        /// </summary>
        public void Uninitialize()
        {
            StopAllCoroutines();

            _assetManager = null;
            _data.Clear();
            _data = null;

            _resourceRequesting = null;
            _resourceDiscardRequest = null;

            _synLoading = false;

        }

        /// <summary>
        /// 获取所有正在请求加载资产关联的 的UI状态名
        /// </summary>
        /// <param name="uiStateName">返回UI状态名</param>
        public void GetUIState(ref JWArrayList<string> uiStateName)
        {
            for (int i = 0; i < _resourceRequesting.Count; i++)
            {
                LoadData loadData = _resourceRequesting[i];
                if (loadData.Data.Life == LifeType.UIState && !string.IsNullOrEmpty(loadData.Data.StateName) &&
                    uiStateName.IndexOf(loadData.Data.StateName, StringComparer.OrdinalIgnoreCase) == -1)
                {
                    uiStateName.Add(loadData.Data.StateName);
                }
            }

            for (int i = 0; i < _data.Count; i++)
            {
                AssetData data = _data[i];
                if (data.Priority == LoadPriority.Wait)
                {
                    continue;
                }

                if (data.Life == LifeType.UIState && !string.IsNullOrEmpty(data.StateName) &&
                    uiStateName.IndexOf(data.StateName, StringComparer.OrdinalIgnoreCase) == -1)
                {
                    uiStateName.Add(data.StateName);
                }
            }
        }


        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="assetData">资源数据</param>
        public void AddTask(ref AssetData assetData)
        {
            if (assetData.Type==AssetType.UnityScene&& assetData.Priority != LoadPriority.Wait)
            {
                JW.Common.Log.LogE("Loader.AddTask : invalid parameter - {0}", assetData.Name);
                assetData.Priority = LoadPriority.Wait;
            }

            if (_synLoading&&(assetData.Priority == LoadPriority.Wait))
            {
                JW.Common.Log.LogE("Loader.AddTask : load is runing, can't add wait load asset - {0}", assetData.Name);
                return;
            }

            if (assetData.Priority != LoadPriority.Wait)
            {
                //进行中 重复
                for (int i = 0; i < _resourceRequesting.Count; i++)
                {
                    LoadData loadData = _resourceRequesting[i];
                    if (assetData.Type == loadData.Data.Type &&
                        assetData.Name.Equals(loadData.Data.Name, StringComparison.OrdinalIgnoreCase) &&
                        (assetData.Callback == loadData.Data.Callback))
                    {
                        loadData.Data = assetData;
                        _resourceRequesting.RemoveAt(i);

                        bool insert = false;
                        for (int j = _resourceRequesting.Count - 1; j >= 0; j--)
                        {
                            if (_resourceRequesting[j].Data.Priority <= loadData.Data.Priority)
                            {
                                _resourceRequesting.Insert(j + 1, loadData);
                                insert = true;
                                break;
                            }
                        }

                        if (!insert)
                        {
                            _resourceRequesting.Insert(0, loadData);
                        }

                        return;
                    }
                }
            }

            //重复？
            for (int i = 0; i < _data.Count; i++)
            {
                AssetData data = _data[i];

                if (assetData.Callback == data.Callback &&
                    assetData.Type == data.Type &&
                    assetData.Name.Equals(data.Name, StringComparison.OrdinalIgnoreCase))
                {
                    _data.RemoveAt(i);
                    break;
                }
            }


            for (int i = _data.Count - 1; i >= 0; --i)
            {
                if (_data[i].Priority <= assetData.Priority)
                {
                    _data.Insert(i + 1, assetData);
                    return;
                }
            }

            _data.Insert(0, assetData);
        }

        /// <summary>
        /// 清除UIState 相关的任务
        /// </summary>
        /// <param name="uiState">保留的UI状态</param>
        public void ClearTask(JWArrayList<string> uiState)
        {
            if (uiState == null)
            {
                JW.Common.Log.LogE("Loader.ClearTask : invalid parameter");
                return;
            }

            if (_synLoading)
            {
                JW.Common.Log.LogE("Loader.ClearTask : load is running");
                return;
            }

            for (int i = _resourceRequesting.Count - 1; i >= 0; --i)
            {
                LoadData loadData = _resourceRequesting[i];
                if (!AssetAssistor.IsAssetDead(ref loadData.Data,uiState))
                {
                    continue;
                }

                if (loadData.Request != null)
                {
                    if (_resourceDiscardRequest != null)
                    {
                        JW.Common.Log.LogE("Loader.ClearTask : _resourceDiscardRequest != null");
                    }

                    _resourceDiscardRequest = loadData.Request;
                }

                _resourceRequesting.RemoveAt(i);
            }

            for (int i = _data.Count - 1; i >= 0; --i)
            {
                AssetData data = _data[i];
                if (data.Priority == LoadPriority.Wait)
                {
                    continue;
                }

                if (AssetAssistor.IsAssetDead(ref data,uiState))
                {
                    _data.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// 取消任务
        /// </summary>
        /// <param name="callback">回调</param>
        /// <param name="assetName">资源名或者类名</param>
        public void CancelTask(IAssetLoadCallback callback, string assetName)
        {
            if (callback == null)
            {
                JW.Common.Log.LogE("Loader.CancelTask : invalid parameter");
                return;
            }

            if (_synLoading)
            {
                JW.Common.Log.LogE("Loader.CancelTask : load is running");
                return;
            }

            for (int i = _resourceRequesting.Count - 1; i >= 0; --i)
            {
                LoadData loadData = _resourceRequesting[i];
                if (loadData.Data.Callback != callback ||
                    (!string.IsNullOrEmpty(assetName) && !loadData.Data.Name.Equals(assetName, StringComparison.OrdinalIgnoreCase)))
                {
                    continue;
                }

                if (loadData.Request != null)
                {
                    if (_resourceDiscardRequest != null)
                    {
                        JW.Common.Log.LogE("Loader.ClearTask : _resourceDiscardRequest != null");
                    }

                    _resourceDiscardRequest = loadData.Request;
                }

                _resourceRequesting.RemoveAt(i);
            }

            for (int i = _data.Count - 1; i >= 0; --i)
            {
                AssetData data = _data[i];

                if (data.Callback == callback &&
                    (string.IsNullOrEmpty(assetName) || data.Name.Equals(assetName, StringComparison.OrdinalIgnoreCase)))
                {
                    _data.RemoveAt(i);
                }
            }
        }

        #region Work
        private IEnumerator AsynchronousLoad()
        {
            JWObjList<string> stringList = new JWObjList<string>();
            JWArrayList<AssetData> assetDataList = new JWArrayList<AssetData>();

            IEnumerator loadBundleEnumerator = AsynchronousLoad_LoadAssetBundle(stringList, assetDataList);
            IEnumerator loadResourceEnumerator = AsynchronousLoad_LoadResource();
            IEnumerator instantiateEnumerator = AsynchronousLoad_InstantiateResource();
            if (loadBundleEnumerator == null || loadResourceEnumerator == null || instantiateEnumerator == null)
            {
                yield break;
            }

            while (true)
            {
                yield return null;

                if (_synLoading)
                {
                    continue;
                }

                while (_resourceDiscardRequest != null)
                {
                    if (!_resourceDiscardRequest.isDone)
                    {
                        yield return null;
                        continue;
                    }

                    if (_resourceDiscardRequest.resource != null)
                    {
                        ResService.UnloadResource(_resourceDiscardRequest.resource);
                    }

                    _resourceDiscardRequest = null;
                    yield return null;
                }

                if (_resourceRequesting.Count > 0)
                {
                    while (instantiateEnumerator.MoveNext())
                    {
                        if (instantiateEnumerator.Current == InstructionEnd)
                        {
                            break;
                        }

                        yield return null;
                    }
                }

                if (_resourceRequesting.Count > 0)
                {
                    while (loadResourceEnumerator.MoveNext())
                    {
                        if (loadResourceEnumerator.Current == InstructionEnd)
                        {
                            break;
                        }

                        yield return null;
                    }
                }

                _loadAssetBundlePriority = AsynchronousLoad_CalculatePriority();
                if (_loadAssetBundlePriority != int.MaxValue &&
                    (_loadAssetBundlePriority < LoadPriority.Preprocess ||
                    (!BundleService.GetInstance().IsWorking() && _resourceDiscardRequest == null && _resourceRequesting.Count == 0)))
                {
                    while (loadBundleEnumerator.MoveNext())
                    {
                        if (loadBundleEnumerator.Current == InstructionEnd)
                        {
                            break;
                        }

                        yield return null;
                    }
                }
            }
        }

        private int AsynchronousLoad_CalculatePriority()
        {
            int priority = int.MaxValue;
            for (int i = 0; i < _resourceRequesting.Count; i++)
            {
                LoadData loadData = _resourceRequesting[i];
                if (loadData.Data.Priority < priority)
                {
                    priority = loadData.Data.Priority;
                }
            }

            for (int i = 0; i < _data.Count; i++)
            {
                AssetData data = _data[i];
                if (data.Priority != LoadPriority.Wait && data.Priority < priority)
                {
                    priority = data.Priority;
                    break;
                }
            }

            return priority;
        }

        private IEnumerator AsynchronousLoad_LoadAssetBundle(JWObjList<string> stringList, JWArrayList<AssetData> assetDataList)
        {
            while (true)
            {
                stringList.Clear();
                assetDataList.Clear();
                for (int i = 0; i < _data.Count;)
                {
                    AssetData data = _data[i];
                    if (data.Priority != _loadAssetBundlePriority)
                    {
                        ++i;
                        continue;
                    }

                    _data.RemoveAt(i);

                    if (_assetManager.GetCacheCount(data.Name) >= data.Count)
                    {
                        if (data.Callback != null)
                        {
                            assetDataList.Add(data);
                        }
                        continue;
                    }

                    LoadData loadData;
                    loadData.Data = data;
                    loadData.LoadBundleState = LoadStateLoading;
                    loadData.Request = null;

                    bool insert = false;
                    for (int j = _resourceRequesting.Count - 1; j >= 0; --j)
                    {
                        if (_resourceRequesting[j].Data.Priority <= data.Priority)
                        {
                            _resourceRequesting.Insert(j + 1, loadData);
                            insert = true;
                            break;
                        }
                    }

                    if (!insert)
                    {
                        _resourceRequesting.Insert(0, loadData);
                    }

                    stringList.Add(data.Filename);

                    if (_loadAssetBundlePriority >= LoadPriority.Preprocess)
                    {
                        break;
                    }
                }

                yield return null;

                if (stringList.Count > 0)
                {
#if USE_PACK_RES
                    BundleMediator.GetInstance().LoadBundle(stringList, OnBundleLoadCompleted);
#else
                    OnBundleLoadCompleted(stringList, true);
#endif
                }

                yield return null;

                for (int i = 0; i < assetDataList.Count; i++)
                {
                    AssetData data = assetDataList[i];
                    if (data.Callback != null)
                    {
                        data.Callback.OnLoadAssetCompleted(data.Name, AssetLoadResult.Success, null);
                        yield return null;
                    }
                }

                yield return InstructionEnd;
            }
        }

        private IEnumerator AsynchronousLoad_LoadResource()
        {
            while (true)
            {
                int find = 0;
                for (; find < _resourceRequesting.Count; find++)
                {
                    if (_resourceRequesting[find].LoadBundleState != LoadStateLoading)
                    {
                        break;
                    }
                }

                if (find >= _resourceRequesting.Count)
                {
                    yield return InstructionEnd;
                    continue;
                }

                LoadData loadData = _resourceRequesting[find];
                if (_assetManager.GetCacheCount(loadData.Data.Name) >= loadData.Data.Count)
                {
                    _resourceRequesting.RemoveAt(find);

                    if (loadData.Data.Callback != null)
                    {
                        loadData.Data.Callback.OnLoadAssetCompleted(loadData.Data.Name, AssetLoadResult.Success, null);
                        yield return null;
                    }

                    yield return InstructionEnd;
                    continue;
                }

                if (loadData.LoadBundleState == LoadStateFail)
                {
                    JW.Common.Log.LogE("Loader.AsynchronousLoad_Resource : load bundle failed - {0}", loadData.Data.Name);
                    _resourceRequesting.RemoveAt(find);

                    if (loadData.Data.Callback != null)
                    {
                        loadData.Data.Callback.OnLoadAssetCompleted(loadData.Data.Name, AssetLoadResult.BundleFail, null);
                        yield return null;
                    }

                    yield return InstructionEnd;
                    continue;
                }

                loadData.Request = ResService.GetInstance().GetResourceAsync(loadData.Data.Filename);
                _resourceRequesting[find] = loadData;

                yield return InstructionEnd;
            }
        }

        private IEnumerator AsynchronousLoad_InstantiateResource()
        {
            while (true)
            {
                int find = 0;
                for (; find < _resourceRequesting.Count; find++)
                {
                    if (_resourceRequesting[find].Request != null)
                    {
                        break;
                    }
                }

                if (find >= _resourceRequesting.Count)
                {
                    yield return InstructionEnd;
                    continue;
                }

                LoadData loadData = _resourceRequesting[find];
                if (!loadData.Request.isDone)
                {
                    yield return null;
                    continue;
                }

                _resourceRequesting.RemoveAt(find);

                if (loadData.Request.resource == null)
                {
                    JW.Common.Log.LogE("Loader.AsynchronousLoad_InstantiateResource : load failed - {0}", loadData.Data.Name);
                    if (loadData.Data.Callback != null)
                    {
                        loadData.Data.Callback.OnLoadAssetCompleted(loadData.Data.Name, AssetLoadResult.ResourceFail, null);
                        yield return null;
                    }

                    yield return InstructionEnd;
                    continue;
                }

                if (loadData.Request.resource.Content == null)
                {
                    JW.Common.Log.LogE("Loader.AsynchronousLoad_InstantiateResource : load failed - {0}", loadData.Data.Name);
                    ResService.UnloadResource(loadData.Request.resource);

                    if (loadData.Data.Callback != null)
                    {
                        loadData.Data.Callback.OnLoadAssetCompleted(loadData.Data.Name, AssetLoadResult.ResourceFail, null);
                        yield return null;
                    }

                    yield return InstructionEnd;
                    continue;
                }

                if (_assetManager.AddCache(loadData.Data, loadData.Request.resource) == null)
                {
                    JW.Common.Log.LogE("Loader.AsynchronousLoad_Resource : add cache failed - {0}", loadData.Data.Name);
                    ResService.UnloadResource(loadData.Request.resource);

                    if (loadData.Data.Callback != null)
                    {
                        loadData.Data.Callback.OnLoadAssetCompleted(loadData.Data.Name, AssetLoadResult.ResourceFail, null);
                        yield return null;
                    }

                    yield return InstructionEnd;
                    continue;
                }

                yield return null;
                //个数
                if (_assetManager.GetCacheCount(loadData.Data.Name) < loadData.Data.Count)
                {
                    loadData.Request.resource = ResService.GetResource(loadData.Data.Filename);
                    _resourceRequesting.Insert(find, loadData);

                    yield return InstructionEnd;
                    continue;
                }

                if (loadData.Data.Callback != null)
                {
                    loadData.Data.Callback.OnLoadAssetCompleted(loadData.Data.Name, AssetLoadResult.Success, null);
                    yield return null;
                }

                yield return InstructionEnd;
            }
            
        }

        /// <summary>
        /// 异步Bundle加载完成回调
        /// </summary>
        /// <param name="resourcePath"></param>
        /// <param name="succeed"></param>
        private void OnBundleLoadCompleted(JWObjList<string> resourcePath, bool succeed)
        {
            if (resourcePath == null || resourcePath.Count == 0)
            {
                JW.Common.Log.LogE("Loader.OnBundleLoadCompleted : invalid parameter");
                return;
            }

            for (int i = 0; i < _resourceRequesting.Count; i++)
            {
                LoadData loadData = _resourceRequesting[i];
                if (loadData.LoadBundleState != LoadStateLoading || !resourcePath.Contains(loadData.Data.Filename))
                {
                    continue;
                }

                loadData.LoadBundleState = succeed ? LoadStateSuccess : LoadStateFail;
                _resourceRequesting[i] = loadData;
            }
        }
        #endregion
    }
}
