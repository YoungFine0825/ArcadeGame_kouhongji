/********************************************************************
	created:	2018-05-28   
	filename: 	AssetPreloader
	author:		jordenwu
	
	purpose:	资产预加载管理
*********************************************************************/
using System;
using System.Collections;
using JW.Common;
using UnityEngine;
using Object = UnityEngine.Object;
using JW.Res;

namespace JW.Framework.Asset
{
    public class AssetPreloader : MonoBehaviour
    {
        private AssetLoader _loader;
        private Action<bool> _allCompletedCallback;

        private JWObjList<string> _bundleFiles;
        private bool _bundleLoading;

        private AssetBundle _shaderBundle;
        private JWArrayList<string> _shaderFilename;
        private AssetBundleRequest _shaderRequest;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="loader">装载器</param>
        public void Initialize(AssetLoader loader)
        {
            _loader = loader;
        }

        /// <summary>
        /// 反初始化
        /// </summary>
        public void Uninitialize()
        {
            StopAllCoroutines();
        }

        /// <summary>
        /// 开始加载常驻的 AssetBundle
        /// </summary>
        /// <param name="completeCallBack"></param>
        public void StartResidentBundle(System.Action completeCallBack)
        {
            StartCoroutine(BundleMediator.GetInstance().LoadResidentBundles(delegate ()
            {
                if (completeCallBack != null)
                {
                    completeCallBack();
                }
            }));
        }

        /// 游戏更新检查后后启动
        public void StartAfterUpdate(Action<bool> allCompletedCallback)
        {
            _allCompletedCallback = allCompletedCallback;
#if USE_PACK_RES
            if (ResService.GetInstance().PackConfig == null)
            {
                JW.Common.Log.LogE("Preloader.StartAfterUpdate : resource initialize failed");
                return;
            }
            //
            _bundleFiles = BundleMediator.GetInstance().GetPreloadBundles();
            _bundleLoading = false;

            string filename = "main_shaders.ab";
            _shaderBundle = BundleService.GetInstance().GetBundle(filename);
            if (_shaderBundle == null)
            {
                JW.Common.Log.LogE("Preloader.StartAfterUpdate : failed to get shader bundle");
            }

            ResPackInfo pi = ResService.GetInstance().PackConfig.GetPackInfo(filename);
            if (pi == null)
            {
                _shaderFilename = new JWArrayList<string>(0);
            }
            else
            {
                _shaderFilename = new JWArrayList<string>(pi.Resources.Count);
                for (int i = 0; i < pi.Resources.Count; i++)
                {
                    _shaderFilename.Add(pi.Resources[i].Path);
                }
            }
#else
            _bundleFiles = new JWObjList<string>(0);
            _shaderBundle = null;
            _shaderFilename = new JWArrayList<string>(0);
#endif

            //真正的开始预加载协成
            StartCoroutine(PreloadCoroutine());
        }

        /// <summary>
        /// 预加载协程
        /// </summary>
        /// <returns></returns>
        private IEnumerator PreloadCoroutine()
        {
            bool bundleCompleted = false;
            bool shaderCompleted = false;

            while (true)
            {
                if (_loader.IsBusy)
                {
                    yield return null;
                }

                if (!bundleCompleted)
                {
                    bundleCompleted = PreloadCoroutine_Bundle();
                }

                if (!shaderCompleted)
                {
                    shaderCompleted = PreloadCoroutine_Shader();
                }

                //完成
                if (bundleCompleted && shaderCompleted)
                {
                    JW.Common.Log.LogD("###Preloading complete.###");
                    if (_allCompletedCallback != null)
                    {
                        _allCompletedCallback(true);
                    }
                    break;
                }

                yield return null;
            }
        }

        private bool PreloadCoroutine_Bundle()
        {
            if (_bundleFiles == null)
            {
                return false;
            }

            if (_bundleFiles.Count == 0)
            {
                return true;
            }

            if (!_bundleLoading)
            {
                _bundleLoading = true;
                BundleMediator.GetInstance().LoadBundle(_bundleFiles[0], OnLoadBundleCompleted);
            }

            return false;
        }

        //预加载Shader
        private bool PreloadCoroutine_Shader()
        {
            if (_shaderFilename == null)
            {
                return false;
            }

            if (_shaderFilename.Count == 0)
            {
                return true;
            }
            if (_shaderBundle == null)
            {
                return true;
            }
            //单个
            if (_shaderRequest != null)
            {
                if (!_shaderRequest.isDone)
                {
                    return false;
                }
                _shaderRequest = null;
                _shaderFilename.RemoveAt(_shaderFilename.Count - 1);
            }

            if (_shaderFilename.Count > 0)
            {
                _shaderRequest = _shaderBundle.LoadAssetAsync(_shaderFilename[_shaderFilename.Count - 1], typeof(Object));
            }

            return false;
        }

        private void OnLoadBundleCompleted(string filename, bool result)
        {
            _bundleFiles.RemoveAt(0);
            _bundleLoading = false;

            if (result)
            {
                BundleMediator.GetInstance().UnloadBundle(filename, true);
            }
        }
    }
}
