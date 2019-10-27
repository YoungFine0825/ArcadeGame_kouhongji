/********************************************************************
	created:	2018-05-28   
	filename: 	AssetManager
	author:		jordenwu
	
	purpose:	资产管理
*********************************************************************/
using JW.Common;
using JW.Res;
using UnityEngine;

namespace JW.Framework.Asset
{
    public class AssetManager
    {
        private AssetCache _cache;
        //正在使用的资产
        private JWArrayList<BaseAsset> _usingAsset;

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="rootTf">根节点</param>
        public void Create(Transform rootTf)
        {
            _cache = new AssetCache();
            _cache.Create(rootTf);

            _usingAsset = new JWArrayList<BaseAsset>();
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Destroy()
        {
            _cache.Destroy();
            _cache = null;

            if (_usingAsset.Count > 0)
            {
                JW.Common.Log.LogE("AssetManager.Destroy : using asset is not empty");
                for (int i = 0; i < _usingAsset.Count; i++)
                {
                    JW.Common.Log.LogE(_usingAsset[i].BaseData.Name);
                }
                JW.Common.Log.LogE("---End---");
            }

            _usingAsset = null;
        }

        public void CleanAllUsing()
        {
            if (_usingAsset == null)
            {
                return;
            }
            for (int i = _usingAsset.Count-1; i >=0; i--)
            {
                BaseAsset ba = _usingAsset[i];
                if (!AssetProcessor.ProcessDestroy(ba))
                {
                    continue;
                }

                if (ba.Resource != null)
                {
                    ResService.UnloadResource(ba.Resource);
                    ba.Resource = null;
                }

                ba.BaseData.Factory.DestroyObject(ba);
                //
                _usingAsset.RemoveAt(i);
            }
            
        }


        /// <summary>
        /// 获取缓存数量
        /// </summary>
        /// <param name="name">资产名</param>
        /// <returns>数量</returns>
        public int GetCacheCount(string name)
        {
            return _cache.GetCount(name);
        }

        /// <summary>
        /// 获取缓存中关联的UI状态
        /// </summary>
        /// <param name="uiStateName">返回UI状态名</param>
        public void GetCacheUIState(ref JWArrayList<string> uiStateName)
        {
            _cache.GetUIState(ref uiStateName);
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="data">资产数据</param>
        /// <param name="resource">资源</param>
        /// <returns>添加到缓存的资产（null标识添加失败）</returns>
        public BaseAsset AddCache(AssetData data, ResObj resource)
        {
            BaseAsset ba = CreateAsset(data, resource);
            if (ba == null)
            {
                return null;
            }

            _cache.AddAsset(ba);

            return ba;
        }

        /// <summary>
        /// 清理UI 状态关联的
        /// </summary>
        /// <param name="uiState">保留的UI状态名</param>
        public void ClearCache(JWArrayList<string> uiState)
        {
            _cache.Clear(uiState);
        }

        /// <summary>
        /// 装载
        /// </summary>
        /// <param name="data">资产数据</param>
        /// <param name="clone">是否Clone一个副本</param>
        /// <returns>资产</returns>
        public BaseAsset Load(ref AssetData data, bool clone)
        {
            if (string.IsNullOrEmpty(data.Filename))
            {
                JW.Common.Log.LogE("AssetManager.Load : invalid parameter");
                return null;
            }

            //缓存找
            BaseAsset ba = _cache.GetAsset(data.Name);
            if (ba != null)
            {
                //缓存复制
                if (clone)
                {
                    BaseAsset cloneBa = AssetProcessor.ProcessClone(ba);
                    _cache.AddAsset(ba);
                    ba = cloneBa;
                }

                if (ba != null)
                {
                    if (!AssetProcessor.ProcessCreate(ba))
                    {
                        JW.Common.Log.LogE("AssetManager.CreateAsset : failed to process create - {0}", data.Name);
                        ba.BaseData.Callback = null;
                        ba.BaseData.Factory = null;
                        ba.Resource = null;
                        data.Factory.DestroyObject(ba);
                        return null;
                    }
                    _usingAsset.Add(ba);
                }
                return ba;
            }

            //没在缓存 同步创建
            ResObj resource = ResService.GetResource(data.Filename);
            if (resource == null)
            {
                JW.Common.Log.LogE("AssetManager.Load : failed to load resource - {0}", data.Filename);
                return null;
            }

            if (resource.Content == null)
            {
                JW.Common.Log.LogE("AssetManager.Load : failed to load resource - {0}", data.Filename);

                ResService.UnloadResource(resource);
                return null;
            }

            ba = CreateAsset(data, resource);
            if (ba == null)
            {
                ResService.UnloadResource(resource);
                return null;
            }

            if (clone)
            {
                BaseAsset cloneBa = AssetProcessor.ProcessClone(ba);
                _cache.AddAsset(ba);
                ba = cloneBa;
            }

            if (ba != null)
            {
                _usingAsset.Add(ba);
            }

            return ba;
        }

        /// <summary>
        /// 卸载
        /// </summary>
        /// <param name="ba">待卸载的资产</param>
        /// <param name="forceDestroy">是否强制销毁</param>
        public void Unload(BaseAsset ba, bool forceDestroy = false)
        {
            if (ba == null)
            {
                JW.Common.Log.LogE("AssetManager.Unload : invalid parameter");
                return;
            }

            int found = _usingAsset.IndexOf(ba);
            if (found == -1)
            {
                JW.Common.Log.LogE("AssetManager.Unload : can't find asset - {0}", ba.BaseData.Name);
                return;
            }

            _usingAsset.RemoveAt(found);

            //销毁手动和Imediate 
            if (forceDestroy || ba.BaseData.Life == LifeType.Immediate||ba.BaseData.Life==LifeType.Manual)
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
            else
            {
                _cache.AddAsset(ba);
            }
        }

        private BaseAsset CreateAsset(AssetData data, ResObj resource)
        {
            if (resource == null || resource.Content == null)
            {
                JW.Common.Log.LogE("AssetManager.CreateAsset : invalid parameter - {0}", data.Name);
                return null;
            }

            data.Callback = null;

            BaseAsset ba = data.Factory.CreateObject();
            if (ba == null)
            {
                JW.Common.Log.LogE("AssetManager.CreateAsset : failed to create asset - {0}", data.Name);
                return null;
            }

            ba.BaseData = data;
            ba.Resource = resource;

            if (!AssetProcessor.ProcessCreate(ba))
            {
                JW.Common.Log.LogE("AssetManager.CreateAsset : failed to process create - {0}", data.Name);

                ba.BaseData.Callback = null;
                ba.BaseData.Factory = null;
                ba.Resource = null;
                data.Factory.DestroyObject(ba);
                return null;
            }

            //AssetProcessor.ProcessAssociateResource(ba);

            return ba;
        }

#if UNITY_EDITOR
        public JWArrayList<BaseAsset> GetUsingAssetList()
        {
            return _usingAsset;
        }
#endif
    }
}
