/********************************************************************
	created:	21:5:2018   
	filename: 	BundleService
	author:		jordenwu
	
	purpose:	统一的AssetBundle资源加载服务
*********************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using JW.Common;
using UnityEngine;
using UnityEngine.Networking;

namespace JW.Res
{
    // AB数据引用
    public class BundleRef
    {
        public string Path;
        public int RefCnt = 0;
        public AssetBundle Bundle;
        //对应的AssetBundle 信息
        BundlePackInfo _info;
        public BundlePackInfo PackInfo {
            get
            {
                return _info;
            }
        }

        public BundleRef(BundlePackInfo info)
        {
            _info = info;

            Path = info.Path;
            RefCnt = 1;
        }
    }

    // 延迟卸载数据
    public class DeferUnloadData
    {
        //延迟帧数
        const int DEFER_FRAME = 5;
        public int Frame = DEFER_FRAME;

        static JWObjList<DeferUnloadData> _pool = new JWObjList<DeferUnloadData>();

        public static DeferUnloadData Get()
        {
            DeferUnloadData data = null;
            if (_pool.Count > 0)
            {
                data = _pool[_pool.Count - 1];
                _pool.RemoveAt(_pool.Count - 1);
            }
            else
            {
                data = new DeferUnloadData();
            }

            return data;
        }

        public static void Recycle(DeferUnloadData data)
        {
            if (data != null)
            {
                data.Reset();
                _pool.Add(data);
            }
        }

        void Reset()
        {
            Frame = DEFER_FRAME;
        }
    }
    
    //回调定义
    public delegate void BundleLoadedDelegate(BundleRef bundleRef);    
    public delegate void BundleLoadingDelegate(BundlePackInfo packInfo, float progress);
    public delegate void BundleLoadFailedDelegate(BundlePackInfo packInfo, string error);
    public delegate void BundleBatchLoadingDelegate(float progress);

    //统一的AssetBundle服务 
    public class BundleService : MonoSingleton<BundleService>
    {
        // 已加载bundle列表
        JWObjDictionary<string, BundleRef> _bundleDict = new JWObjDictionary<string, BundleRef>(StringComparer.OrdinalIgnoreCase);  
        // 加载中bundle列表
        List<string> _loadingBundle = new List<string>();
        // 延迟卸载bundle列表
        Dictionary<string, DeferUnloadData> _unloadingBundle = new Dictionary<string, DeferUnloadData>(); 

#if UNITY_EDITOR || UNITY_STANDALOND
        public JWObjDictionary<string, BundleRef> BundleDict
        {
            get
            {
                return _bundleDict;
            }
        }
#endif

        public override bool Initialize()
        {
            InitCaching();
            return true;
        }

        public override void Uninitialize()
        {
            _loadingBundle.Clear();
            _loadingBundle = null;
            _unloadingBundle.Clear();
            _unloadingBundle = null;
            StopAllCoroutines();
            UnloadAll();
        }

        void Update()
        {
            //卸载
            DeferUnloadUpdate();
        }

        #region Caching

        //初始化Unity自身Cache 大小
        void InitCaching()
        {
            /* 根据机型设置缓存空间大小
            float availableMem = 0;
			#if !UNITY_EDITOR
            //availableMem = NativeHelper.GetAvailableExternalMemorySize();
			#endif

            if (availableMem <= 0 || availableMem > 4096) // 4G
            {
                Caching.maximumAvailableDiskSpace = 419430400;//400M
            }
            else if (availableMem > 1024) // 1G
            {
                Caching.maximumAvailableDiskSpace = 314572800;//300M
            }
            else
            {
                Caching.maximumAvailableDiskSpace = 209715200;//200M
            }*/

#if UNITY_EDITOR
            StartCoroutine(CheckCachingReady());
#endif
        }

#if UNITY_EDITOR
        /// <summary>
        /// Editor下每次都清缓存
        /// </summary>
        /// <returns></returns>
        private IEnumerator CheckCachingReady()
        {
            while (!Caching.ready)
            {
                yield return null;
            }
            Caching.ClearCache();
        }
#endif

        /// <summary>
        /// cachi中剩余空间
        /// </summary>
        /// <returns></returns>
        public static long AvailableCache()
        {
            return Caching.defaultCache.spaceFree;
        }

        /// <summary>
        /// 已使用ccache
        /// </summary>
        /// <returns></returns>
        public static long UsedCache()
        {
            return Caching.defaultCache.spaceOccupied;
        }

        /// <summary>
        /// 清空cache WWW 使用的C
        /// </summary>
        public static bool CleanCache()
        {
            Caching.ClearCache();
            return true;
        }

        #endregion

        #region 同步加载AB

        /// <summary>
        /// 同步加载bundle
        /// </summary>
        /// <param name="packInfo">bundle打包信息</param>
        public void LoadSync(BundlePackInfo packInfo)
        {
            if (packInfo == null)
            {
                JW.Common.Log.LogE("Sync loading bundle with null pack info.");
                return;
            }

            //是否已经加载
            BundleRef br = null;
            if (_bundleDict.TryGetValue(packInfo.Path, out br))
            {
                br.RefCnt++;

                if (_unloadingBundle.ContainsKey(packInfo.Path))
                {
                    _unloadingBundle.Remove(packInfo.Path);
                }

                return;
            }

            // 是否正在加载
            if (_loadingBundle.Contains(packInfo.Path))
            {
                JW.Common.Log.LogE("Bundle is loading when load sync. bundle:{0}", packInfo.Path);
                return;
            }

            // 依赖
            for (int i = 0; packInfo.Dependencies != null && i < packInfo.Dependencies.Count; i++)
            {
                LoadSync(packInfo.Dependencies[i]);
            }

            //干活
            AssetBundle ab = null;
            string path = GetBundleFullPath(packInfo, false);
            JW.Common.Log.LogD("Sync load bundle path:{0}", path);
            //
            if (packInfo.HasFlag(EBundleFlag.UnCompress)|| packInfo.HasFlag(EBundleFlag.LZ4))
            {
                ab = AssetBundle.LoadFromFile(path);
            }
            else
            {
                //LZMA 压缩的AssetBundle
                //LZMA压缩的 u2017可以直接通过 LoadFromFile 创建支持StreamingAssetPath 内存占用大
                ab = AssetBundle.LoadFromFile(path);

                /*
                byte[] bytes = null;
                //优先外围目录
                if (FileUtil.IsExistInIFSExtraFolder(packInfo.Path))
                {
                    bytes = File.ReadAllBytes(path);
                }
                else
                {
                    
                    //StreamingAsset目录的
#if UNITY_ANDROID && !UNITY_EDITOR
                    bytes = NativeHelper.Android_ReadFileInAssets(packInfo.Path);
#else
                    bytes = File.ReadAllBytes(path);
#endif
                }

                if (null != bytes)
                {
                    //注意
                    //https://unity3d.com/cn/learn/tutorials/topics/best-practices/assetbundle-fundamentals?playlist=30089
                    //The peak amount of memory consumed by this API will be at least twice the size of the AssetBundle:
                    ab = AssetBundle.LoadFromMemory(bytes);
                }*/
            }

            // 完成
            if (ab != null)
            {
                br = new BundleRef(packInfo);
                br.Bundle = ab;
                _bundleDict.Add(packInfo.Path, br);
            }
        }

#endregion
        //异步
#region Load Asynchronize

        /// <summary>
        /// 异步加载bundle
        /// </summary>
        /// <param name="packInfo">bundle打包信息</param>
        /// <param name="complete">加载完成回调</param>
        /// <param name="failure">加载失败回调</param>
        /// <param name="loading">加载进度回调</param>
        /// <returns></returns>
        public IEnumerator LoadAsync(BundlePackInfo packInfo, BundleLoadedDelegate complete, BundleLoadFailedDelegate failure, BundleLoadingDelegate loading = null)
        {
            if (packInfo == null)
            {
                JW.Common.Log.LogE("Async loading bundle with null pack info.");
                yield break;
            }

            //判断是否已经加载
            BundleRef br = null;
            if (_bundleDict.TryGetValue(packInfo.Path, out br))
            {
                br.RefCnt++;

                if (_unloadingBundle.ContainsKey(packInfo.Path))
                {
                    _unloadingBundle.Remove(packInfo.Path);
                }

                if (loading != null)
                {
                    loading(packInfo, 1f);
                }

                if (complete != null)
                {
                    complete(br);
                }

                yield break;
            }
            
            // 正在加载
            if (_loadingBundle.Contains(packInfo.Path))
            {
                yield return null;
                while (_loadingBundle.Contains(packInfo.Path))
                {
                    yield return null;
                }

                if (_bundleDict.TryGetValue(packInfo.Path, out br))
                {
                    br.RefCnt++;

                    if (complete != null)
                    {
                        complete(br);
                    }
                }

                yield break;
            }
            //LoadFormFile 同步加载 
            if (packInfo.HasFlag(EBundleFlag.UnCompress)||packInfo.HasFlag(EBundleFlag.LZ4))
            {
                LoadSync(packInfo);
                yield break;
            }

            //LZMA
            _loadingBundle.Add(packInfo.Path);
            // 依赖
            for (int i = 0; packInfo.Dependencies != null && i < packInfo.Dependencies.Count; i++)
            {
                yield return StartCoroutine(LoadAsync(packInfo.Dependencies[i], null, null));
            }
            // 内部含有 
            if (packInfo.IsNoBundle() && !packInfo.Outside)
            {
                _loadingBundle.Remove(packInfo.Path);
                if (failure != null)
                {
                    failure(packInfo, "");
                }
                yield break;
            }
            
            {
                //对于LZMA AssetBundle.LoadFromFileAsync 会占用解压内存
                //手册说用UnityWebRequest 可以首次解压 后缓存，但对于本地的包又说用AssetBundle.LoadFromFileAsync
                string path = GetBundleFullPath(packInfo, false);
                JW.Common.Log.LogD("Async load bundle path:{0}", path);
                AssetBundleCreateRequest www = AssetBundle.LoadFromFileAsync(path);
                float progress = 0.0f;
                while (!www.isDone)
                {
                    if (loading != null)
                    {
                        if (www.progress < 0.00001 && www.progress > -0.00001)
                        {
                            progress += 0.01f;
                            loading(packInfo, progress);
                        }
                        else
                        {
                            loading(packInfo, www.progress);
                        }
                    }
                    yield return null;
                }

                _loadingBundle.Remove(packInfo.Path);
                // failed
                if (www.assetBundle == null)
                {
                    JW.Common.Log.LogE("Async loading bundle {0} failed, error:{1}", path, "");
                    if (failure != null)
                    {
                        failure(packInfo, "");
                    }
                    www = null;
                    yield break;
                }

                if (loading != null)
                {
                    loading(packInfo, 1f);
                }

                // succeed
                if (www.assetBundle != null)
                {
                    br = new BundleRef(packInfo);
                    br.Bundle = www.assetBundle;
                    _bundleDict.Add(packInfo.Path, br);

                    if (complete != null)
                    {
                        complete(br);
                    }
                }
                www = null;
            }
            /*Test2
            {
                string path = GetBundleFullPath(packInfo, true);
                JW.Common.Log.LogD("Async load bundle use UnityWebRequest path:{0}", path);
                UnityWebRequest www = UnityWebRequest.GetAssetBundle(path);
                yield return www.SendWebRequest();
                float progress = 0.0f;
                while (!www.isDone)
                {
                    if (loading != null)
                    {
                        if (www.downloadProgress < 0.00001 && www.downloadProgress > -0.00001)
                        {
                            progress += 0.01f;
                            loading(packInfo, progress);
                        }
                        else
                        {
                            loading(packInfo, www.downloadProgress);
                        }
                    }
                    yield return null;
                }

                _loadingBundle.Remove(packInfo.Path);
                // failed
                AssetBundle ab = DownloadHandlerAssetBundle.GetContent(www);
                if (ab == null)
                {
                    JW.Common.Log.LogE("Async loading bundle use UnityWebRequest {0} failed, error:{1}", path, "");
                    if (failure != null)
                    {
                        failure(packInfo, "");
                    }
                    www.Dispose();
                    www = null;
                    yield break;
                }

                if (loading != null)
                {
                    loading(packInfo, 1f);
                }

                // succeed
                if (ab != null)
                {
                    br = new BundleRef(packInfo);
                    br.Bundle = ab;
                    _bundleDict.Add(packInfo.Path, br);

                    if (complete != null)
                    {
                        complete(br);
                    }
                }
                www.Dispose();
                www = null;
            }*/
        }


        /// <summary>
        /// 异步加载bundle列表
        /// </summary>
        /// <param name="packInfo">bundle打包信息</param>
        /// <param name="complete">加载完成回调</param>
        /// <param name="failure">加载失败回调</param>
        /// <param name="loading">加载进度回调</param>
        /// <returns></returns>
        public IEnumerator LoadAsync(JWObjList<BundlePackInfo> packInfos,
            BundleLoadedDelegate complete, 
            BundleLoadFailedDelegate failure, 
            BundleLoadingDelegate loading = null)
        {
            if (packInfos == null)
            {
                JW.Common.Log.LogE("Async loading bundle with null pack info.");
                yield break;
            }

            //JWObjList<BundleRef> bundleRefs = new JWObjList<BundleRef>();

            for (int i = 0; i < packInfos.Count; ++i)
            {
                BundlePackInfo packInfo = packInfos[i];
                if (packInfo != null)
                {
                    // ab has been loaded
                    BundleRef br = null;
                    if (_bundleDict.TryGetValue(packInfo.Path, out br))
                    {
                        br.RefCnt++;

                        if (_unloadingBundle.ContainsKey(packInfo.Path))
                        {
                            _unloadingBundle.Remove(packInfo.Path);
                        }

                        if (loading != null)
                        {
                            loading(packInfo, 1f);
                        }

                        if (complete != null)
                        {
                            complete(br);
                        }
                        continue;
                    }

                    // loading
                    if (_loadingBundle.Contains(packInfo.Path))
                    {
                        yield return null;
                        while (_loadingBundle.Contains(packInfo.Path))
                        {
                            yield return null;
                        }

                        if (_bundleDict.TryGetValue(packInfo.Path, out br))
                        {
                            br.RefCnt++;

                            if (complete != null)
                            {
                                complete(br);
                            }
                        }

                        continue;
                    }

                    if (packInfo.HasFlag(EBundleFlag.UnCompress)|| packInfo.HasFlag(EBundleFlag.LZ4))
                    {
                        LoadSync(packInfo);
                        yield break;
                    }                   
                    //LZMA
                    // 放在加载依赖前，因为加载依赖表示已经开始加载了
                    _loadingBundle.Add(packInfo.Path);

                    // dependency
                    for (int j = 0; packInfo.Dependencies != null && j < packInfo.Dependencies.Count; j++)
                    {
                        yield return StartCoroutine(LoadAsync(packInfo.Dependencies[j], null, null));
                    }

                    // no bundle
                    if (packInfo.IsNoBundle() && !packInfo.Outside)
                    {
                        _loadingBundle.Remove(packInfo.Path);
                        yield break;
                    }

                    // load
                    string path = GetBundleFullPath(packInfo, true);
                    JW.Common.Log.LogD("Async load bundle liat path:{0}", path);
                    UnityWebRequest www = null;
                    www = UnityWebRequest.GetAssetBundle(path);
                    yield return www.SendWebRequest();
                    // loading progress
                    if (loading != null)
                    {
                        float progress = 0.0f;
                        while (string.IsNullOrEmpty(www.error) && !www.isDone)
                        {
                            if (www.downloadProgress < 0.00001 && www.downloadProgress > -0.00001)
                            {
                                progress += 0.01f;
                                loading(packInfo, progress);
                            }
                            else
                            {
                                loading(packInfo, www.downloadProgress);
                            }

                            yield return null;
                        }

                        if (string.IsNullOrEmpty(www.error))
                        {
                            loading(packInfo, 1f);
                        }
                    }

                    _loadingBundle.Remove(packInfo.Path);

                    // failed
                    if (!string.IsNullOrEmpty(www.error))
                    {
                        JW.Common.Log.LogE("Async loading bundle {0} failed, error:{1}", packInfo.Path, www.error);

                        if (failure != null)
                        {
                            failure(packInfo, www.error);
                        }

                        www.Dispose();

                        continue;
                    }

                    AssetBundle ab = DownloadHandlerAssetBundle.GetContent(www);
                    // succeed
                    if (ab!= null)
                    {
                        br = new BundleRef(packInfo);
                        br.Bundle = ab;
                        _bundleDict.Add(packInfo.Path, br);

                        if (complete != null)
                        {
                            complete(br);
                        }
                    }
                    www.Dispose();
                }
            }
        }

        /// <summary>
        /// 批量并发加载bundle
        /// </summary>
        /// <param name="bundleList">bundle打包信息列表</param>
        /// <param name="complete">全部加载完成回调</param>
        /// <param name="progress">加载进度，0-1</param>
        /// <returns></returns>
        public IEnumerator BatchLoadAsync(JWObjList<BundlePackInfo> bundleList, Action complete, BundleBatchLoadingDelegate progress = null, BundleLoadedDelegate loaded = null, BundleLoadFailedDelegate failure = null)
        {
            if (bundleList == null || bundleList.Count == 0)
            {
                if (progress != null)
                {
                    progress(1f);
                }

                if (complete != null)
                {
                    complete();
                }

                yield break;
            }

            // 并发加载多个bundle
            JWObjList<string> failedBundles = null;
            Dictionary<string, float> progressDict = new Dictionary<string, float>(bundleList.Count); // 因为是并发，所以计算进度稍微复杂一些，需要每个bundle的进度收集计算
            float step = 1f / bundleList.Count; // 大致平分每个bundle的加载时间
            for (int i = 0; i < bundleList.Count; i++)
            {
                BundlePackInfo bundleInfo = bundleList[i];
                StartCoroutine(LoadAsync(bundleInfo, delegate (BundleRef bundleRef)
                {
                    // complete one
                    if (bundleRef != null)
                    {
                        // 已加载完成某一个bundle
                        if (loaded != null)
                        {
                            loaded(bundleRef);
                        }

                        if (progressDict.ContainsKey(bundleRef.Path))
                        {
                            progressDict[bundleRef.Path] = 1f;
                        }
                        else
                        {
                            progressDict.Add(bundleRef.Path, 1f);
                        }
                    }
                }, delegate (BundlePackInfo packInfo, string error)
                {
                    if (failedBundles == null)
                    {
                        failedBundles = new JWObjList<string>();
                    }

                    if (!failedBundles.Contains(packInfo.Path))
                    {
                        failedBundles.Add(packInfo.Path);
                    }

                    if (failure != null)
                    {
                        failure(packInfo, error);
                    }

                }, delegate (BundlePackInfo packInfo, float prog)
                {
                    // 加载进度回调，会有多次
                    if (progress != null)
                    {
                        // 记录每次每个bundle的进度
                        if (progressDict.ContainsKey(packInfo.Path))
                        {
                            progressDict[packInfo.Path] = prog;
                        }
                        else
                        {
                            progressDict.Add(packInfo.Path, prog);
                        }

                        // 计算总进度
                        float totalProgress = 0;
                        for (int j = 0; j < bundleList.Count; j++)
                        {
                            float eachProg = 0;
                            if (progressDict.TryGetValue(bundleList[j].Path, out eachProg))
                            {
                                totalProgress += eachProg * step;
                            }
                        }

                        progress(totalProgress);
                    }
                }));
            }

            // 检查是否全部完成
            while (true)
            {
                bool completed = true;
                for (int i = 0; i < bundleList.Count; i++)
                {
                    BundlePackInfo bundleInfo = bundleList[i];
                    if (_bundleDict.ContainsKey(bundleInfo.Path))
                    {
                        continue;
                    }

                    if (failedBundles != null && failedBundles.Contains(bundleInfo.Path))
                    {
                        continue;
                    }
                    
                    completed = false;
                    yield return null;
                    break;
                }

                if (completed)
                {
                    if (complete != null)
                    {
                        complete();
                    }

                    yield break;
                }
            }
        }

        public IEnumerator BatchLoadAsync(JWObjList<BundlePackInfo>[] bundleLists, Action complete, BundleBatchLoadingDelegate progress = null)
        {
            if (bundleLists == null || bundleLists.Length == 0)
            {
                if (progress != null)
                {
                    progress(1f);
                }

                if (complete != null)
                {
                    complete();
                }

                yield break;
            }

            int totalBundles = 0;
            for (int i = 0; i < bundleLists.Length; ++i)
            {
                JWObjList<BundlePackInfo> bundles = bundleLists[i];
                if (bundles != null)
                {
                    for (int j = 0; j < bundles.Count; ++j)
                    {
                        if (bundles[j] != null)
                        {
                            ++totalBundles;
                        }
                    }
                }
            }

            // 并发加载多个bundle
            JWObjList<string> failedBundles = null;
            Dictionary<string, float> progressDict = new Dictionary<string, float>(totalBundles); // 因为是并发，所以计算进度稍微复杂一些，需要每个bundle的进度收集计算
            float step = 1f / totalBundles; // 大致平分每个bundle的加载时间
            for (int i = 0; i < bundleLists.Length; ++i)
            {
                JWObjList<BundlePackInfo> bundles = bundleLists[i];
                StartCoroutine(LoadAsync(bundles, delegate (BundleRef bundleRef)
                {
                    // complete one
                    if (bundleRef != null)
                    {
                        // 已加载完成某一个bundle
                        if (progressDict.ContainsKey(bundleRef.Path))
                        {
                            progressDict[bundleRef.Path] = 1f;
                        }
                        else
                        {
                            progressDict.Add(bundleRef.Path, 1f);
                        }
                    }

                }, delegate (BundlePackInfo packInfo, string error)
                {
                    if (failedBundles == null)
                    {
                        failedBundles = new JWObjList<string>();
                    }

                    if (!failedBundles.Contains(packInfo.Path))
                    {
                        failedBundles.Add(packInfo.Path);
                    }
                }, delegate (BundlePackInfo packInfo, float prog)
                {
                    // 加载进度回调，会有多次
                    if (progress != null)
                    {
                        // 记录每次每个bundle的进度
                        if (progressDict.ContainsKey(packInfo.Path))
                        {
                            progressDict[packInfo.Path] = prog;
                        }
                        else
                        {
                            progressDict.Add(packInfo.Path, prog);
                        }

                        // 计算总进度
                        float totalProgress = 0;
                        for (int bundleListIndex = 0; bundleListIndex < bundleLists.Length; ++bundleListIndex)
                        {
                            JWObjList<BundlePackInfo> bundlePackInfos = bundleLists[bundleListIndex];
                            if (bundlePackInfos != null)
                            {
                                for (int bundleIndex = 0; bundleIndex < bundlePackInfos.Count; ++bundleIndex)
                                {
                                    if (bundlePackInfos[bundleIndex] != null)
                                    {
                                        float eachProg = 0;
                                        if (progressDict.TryGetValue(bundlePackInfos[bundleIndex].Path, out eachProg))
                                        {
                                            totalProgress += eachProg * step;
                                        }
                                    }

                                }
                            }
                        }

                        progress(totalProgress);
                    }
                }));
            }

            // 检查是否全部完成
            while (true)
            {
                bool completed = true;
                for (int bundleListIndex = 0; bundleListIndex < bundleLists.Length; ++bundleListIndex)
                {
                    JWObjList<BundlePackInfo> bundlePackInfos = bundleLists[bundleListIndex];
                    if (bundlePackInfos != null)
                    {
                        for (int bundleIndex = 0; bundleIndex < bundlePackInfos.Count; ++bundleIndex)
                        {
                            BundlePackInfo bundleInfo = bundlePackInfos[bundleIndex];
                            if (_bundleDict.ContainsKey(bundleInfo.Path))
                            {
                                continue;
                            }

                            if (failedBundles != null && failedBundles.Contains(bundleInfo.Path))
                            {
                                continue;
                            }

                            completed = false;
                            yield return null;
                            break;
                        }
                    }

                    if (!completed)
                        break;
                }

                if (completed)
                {
                    if (complete != null)
                    {
                        complete();
                    }

                    yield break;
                }
            }
        }

#endregion

        /// <summary>
        /// 取bundle
        /// </summary>
        /// <param name="bundlePath">路径</param>
        /// <returns></returns>
        public AssetBundle GetBundle(string bundlePath)
        {
            BundleRef bundleRef = null;
            if (_bundleDict.TryGetValue(bundlePath, out bundleRef))
            {
                if (null != bundleRef && null != bundleRef.Bundle)
                {
                    return bundleRef.Bundle;
                }
            }

            return null;
        }

#region Immediate Unload

        /// <summary>
        /// bundle资源已加载完
        /// </summary>
        /// <param name="pi"></param>
        public void OnAssetLoaded(BundlePackInfo pi)
        {
            if (pi == null)
            {
                return;
            }

            // 立即卸载 (延帧)
            if (pi.Life == EBundleLife.Immediate)
            {
                if (!_unloadingBundle.ContainsKey(pi.Path))
                {
                    _unloadingBundle.Add(pi.Path, DeferUnloadData.Get());
                }
                else
                {
                    JW.Common.Log.LogE("Immediately unload bundle when it's already in unloading queue. bundle:{0}", pi.Path);
                }
            }
        }
        
        /// <summary>
        /// bundle中资源已被加载
        /// </summary>
        /// <param name="assetPath">资源路径</param>
        public void OnAssetLoaded(string assetPath)
        {
            BundlePackInfo pi = GetPackInfo(assetPath);
            OnAssetLoaded(pi);
        }

        /// <summary>
        /// 立即卸载
        /// </summary>
        /// <param name="path"></param>
        void UnloadImmediately(string path)
        {
            BundleRef br = null;
            if (_bundleDict.TryGetValue(path, out br))
            {
                if (br.Bundle != null)
                {
                    br.Bundle.Unload(false);
                }

                _bundleDict.Remove(br.Path);
            }
        }

        /// <summary>
        /// 延迟卸载，每帧检查
        /// </summary>
        void DeferUnloadUpdate()
        {
            JWObjList<string> unloaded = null;
            var itor = _unloadingBundle.GetEnumerator();
            while (itor.MoveNext())
            {
                if (itor.Current.Value.Frame < 0)
                {
                    if (unloaded == null)
                    {
                        unloaded = new JWObjList<string>();
                    }

                    unloaded.Add(itor.Current.Key);
                    DeferUnloadData.Recycle(itor.Current.Value);
                }
                else
                {
                    itor.Current.Value.Frame--;
                }
            }

            for (int i = 0; unloaded != null && i < unloaded.Count; i++)
            {
                UnloadImmediately(unloaded[i]);
                _unloadingBundle.Remove(unloaded[i]);
            }
        }

#endregion

#region Unload

        /// <summary>
        /// 真正卸载无用bundle
        /// </summary>
        public void UnloadUnusedBundles(JWObjList<string> willUseAssets = null)
        {
            JWObjList<string> unloaded = null;
            var itor = _bundleDict.GetEnumerator();
            while (itor.MoveNext())
            {
                BundleRef br = itor.Current.Value;
                if (br.RefCnt <= 0 && !_loadingBundle.Contains(br.Path))
                {
                    // bundle对应的资源可能还会被用到，暂时不卸载
                    if (willUseAssets != null && willUseAssets.Count > 0 && br.PackInfo != null)
                    {
                        for (int i = 0; i < willUseAssets.Count; i++)
                        {
                            string asset = willUseAssets[i];
                            if (br.PackInfo.Contains(asset))
                            {
                                continue;
                            }
                        }
                    }

                    if (unloaded == null)
                    {
                        unloaded = new JWObjList<string>();
                    }

                    unloaded.Add(br.Path);

                    if (br.Bundle != null)
                    {
                        br.Bundle.Unload(false);
                    }

                    // depencency
                    for (int i = 0; br.PackInfo.Dependencies != null && i < br.PackInfo.Dependencies.Count; i++)
                    {
                        Unload(br.PackInfo.Dependencies[i]);
                    }
                }
            }
            
            for (int i = 0; unloaded != null && i < unloaded.Count; i++)
            {
                _bundleDict.Remove(unloaded[i]);
            }
        }
        

        /// <summary>
        /// 卸载bundle，只减引用计数
        /// </summary>
        /// <param name="pi"></param>
        public void Unload(BundlePackInfo pi, bool immediately = false)
        {
            if (pi == null)
            {
                return;
            }

            if (pi.Life == EBundleLife.Immediate)
            {
                return;
            }

            BundleRef br = null;
            if (_bundleDict.TryGetValue(pi.Path, out br))
            {
                if (pi.Life == EBundleLife.Cache)
                {
                    if (br.RefCnt > 0)
                    {
                        br.RefCnt--;
                    }
                    else
                    {
                        Log.LogW("Unloading bundle [{0}] while its reference count is zero.", pi.Path);
                    }

                    if (br.RefCnt == 0 && immediately)
                    {
                        UnloadImmediately(br.Path);
                    }
                }
                else if (pi.Life == EBundleLife.Resident)
                {
                    if (br.RefCnt > 1)
                    {
                        br.RefCnt--;
                    }
                    else
                    {
                        JW.Common.Log.LogE("Unloading RESIDENT bundle {0}", br.Path);
                    }
                }
            }
            else
            {
                Log.LogW("Unloading bundle which is not exist. path:{0}", pi.Path);
            }
        }
        
        /// <summary>
        /// 卸载所有bundle，包括常驻的
        /// </summary>
        public void UnloadAll()
        {
            var enumerator = _bundleDict.GetEnumerator();
            BundleRef bundleRef = null;
            while (enumerator.MoveNext())
            {
                bundleRef = enumerator.Current.Value;
                if (null != bundleRef && null != bundleRef.Bundle)
                {
                    bundleRef.Bundle.Unload(true);
                }
            }

            _bundleDict.Clear();
        }

#endregion

#region Tool

        /// <summary>
        /// 是否有bundle在加载
        /// </summary>
        /// <returns></returns>
        public bool IsWorking()
        {
            return (_loadingBundle.Count > 0);
        }

        /// <summary>
        /// 取bundle完整路径
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        string GetBundleFullPath(BundlePackInfo pi, bool withHeader)
        {
#if UNITY_EDITOR && !UNITY_EDITOR_OSX
            // 在PC上用Windows的Shader包，解决材质丢失问题
            if (pi.Path == "Shaders.ab")
            {
                if (withHeader)
                {
                    return string.Format("file:///{0}/../../IFS/Build/Win/shaders.ab", Application.dataPath);
                }
                else
                {
                    return string.Format("{0}/../../IFS/Build/Win/shaders.ab", Application.dataPath);
                }
            }
            else
#endif
            {
				if (pi.Outside)
                {
                    if (withHeader)
                    {
                        return FileUtil.CombinePath(FileUtil.GetIFSExtractPathWithHeader(), pi.Path);
                    }
                    else
                    {
                        return FileUtil.CombinePath(FileUtil.GetIFSExtractPath(), pi.Path);
                    }
                }
                else
                {
                    if (withHeader)
                    {
                        return FileUtil.GetStreamingAssetsPathWithHeader(pi.Path);
                    }
                    else
                    {
                        return FileUtil.CombinePath(Application.streamingAssetsPath, pi.Path);
                    }
                }
            }
        }
        
        /// <summary>
        /// 获取打包信息
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <returns></returns>
        BundlePackInfo GetPackInfo(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            ResPackConfig config = ResService.GetInstance().PackConfig;
            if (config == null)
            {
                return null;
            }

            return config.GetPackInfoForResource(path) as BundlePackInfo;
        }

#endregion
    }
}
