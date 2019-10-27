/********************************************************************
	created:	2018-05-28   
	filename: 	AssetCache
	author:		jordenwu
	
	purpose:	资产Cache
*********************************************************************/
using System;
using System.Collections.Generic;
using JW.Common;
using JW.Res;
using UnityEngine;

namespace JW.Framework.Asset
{
    public class AssetCache
    {
        private Transform _parentTf;

        private Dictionary<string, JWArrayList<BaseAsset>> _assets;

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="rootTf">根节点</param>
        public void Create(Transform rootTf)
        {
            _parentTf = new GameObject("AssetCache").transform;
            _parentTf.parent = rootTf;
            
            _assets = new Dictionary<string, JWArrayList<BaseAsset>>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Destroy()
        {
            foreach (KeyValuePair<string, JWArrayList<BaseAsset>> ast in _assets)
            {
                for (int i = 0; i < ast.Value.Count; i++)
                {
                    BaseAsset ba = ast.Value[i];
                    
                    UIFormAsset oa = ba as UIFormAsset;
                    if (oa != null)
                    {
                        oa.OnFormAssetDestroy();
                    }

                    if (ba.RootGo != null)
                    {
                        ba.RootGo.ExtDestroy();
                    }

                    if (ba.Resource != null)
                    {
                        ResService.UnloadResource(ba.Resource);
                    }
                }
            }

            _assets = null;
            _parentTf.gameObject.ExtDestroy();
            _parentTf = null;
        }

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="name">资产名</param>
        /// <returns>数量</returns>
        public int GetCount(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                JW.Common.Log.LogE("AssetCache.GetCount : invalid parameter");
                return 0;
            }

            JWArrayList<BaseAsset> ast;
            return _assets.TryGetValue(name, out ast) ? ast.Count : 0;
        }

        /// <summary>
        /// 获取所有的UI状态名
        /// </summary>
        /// <param name="uiStateName">返回UI状态名</param>
        public void GetUIState(ref JWArrayList<string> uiStateName)
        {
            Dictionary<string, JWArrayList<BaseAsset>>.Enumerator enumerator = _assets.GetEnumerator();
            while (enumerator.MoveNext())
            {
                JWArrayList<BaseAsset> val = enumerator.Current.Value;
                if (val == null)
                {
                    continue;
                }

                for (int i = 0; i < val.Count; i++)
                {
                    BaseAsset ba = val[i];
                    if (ba == null || ba.BaseData.Life != LifeType.UIState || string.IsNullOrEmpty(ba.BaseData.StateName))
                    {
                        continue;
                    }

                    if (uiStateName.IndexOf(ba.BaseData.StateName, StringComparer.OrdinalIgnoreCase) == -1)
                    {
                        uiStateName.Add(ba.BaseData.StateName);
                    }
                }
            }
        }

        /// <summary>
        /// 添加资产
        /// </summary>
        /// <param name="ba">资产</param>
        public void AddAsset(BaseAsset ba)
        {
            if (ba == null || string.IsNullOrEmpty(ba.BaseData.Name))
            {
                JW.Common.Log.LogE("AssetCache.AddAsset : invalid parameter");
                return;
            }

            JWArrayList<BaseAsset> ast;
            if (!_assets.TryGetValue(ba.BaseData.Name, out ast))
            {
                ast = new JWArrayList<BaseAsset>();
                _assets.Add(ba.BaseData.Name, ast);
            }

            AssetProcessor.ProcessRestore(ba);

            if (ba.RootGo != null)
            {
                ba.RootGo.ExtSetActive(false);
            }

            if (ba.RootTf != null)
            {
                ba.RootTf.SetParent(_parentTf, false);
            }

            ast.Add(ba);
        }

        /// <summary>
        /// 获取资产
        /// </summary>
        /// <param name="name">资产名</param>
        /// <returns>资产</returns>
        public BaseAsset GetAsset(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                JW.Common.Log.LogE("AssetCache.GetAsset : invalid parameter");
                return null;
            }

            JWArrayList<BaseAsset> ast;
            if (!_assets.TryGetValue(name, out ast) || ast.Count <= 0)
            {
                return null;
            }

            BaseAsset ba = ast[ast.Count - 1];
            ast.RemoveAt(ast.Count - 1);
            return ba;
        }

        /// <summary>
        /// 清理
        /// </summary>
        /// <param name="uiState">保留的UI状态名</param>
        public void Clear(JWArrayList<string> uiState)
        {
            if (uiState == null)
            {
                JW.Common.Log.LogE("AssetCache.ClearCache : invalid parameter");
                return;
            }

            Dictionary<string, JWArrayList<BaseAsset>>.Enumerator enumerator = _assets.GetEnumerator();
            while (enumerator.MoveNext())
            {
                JWArrayList<BaseAsset> val = enumerator.Current.Value;
                if (val == null)
                {
                    continue;
                }

                for (int i = val.Count - 1; i >= 0; i--)
                {
                    BaseAsset ba = val[i];
                    if (ba == null)
                    {
                        val.RemoveAt(i);
                        continue;
                    }

                    if (!AssetAssistor.IsAssetDead(ref ba.BaseData,uiState))
                    {
                        continue;
                    }

                    if (AssetProcessor.ProcessDestroy(ba))
                    {
                        if (ba.Resource != null)
                        {
                            ResService.UnloadResource(ba.Resource);
                            ba.Resource = null;
                        }

                        ba.BaseData.Factory.DestroyObject(ba);
                    }
                    else
                    {
                        JW.Common.Log.LogE("AssetCache.ClearCache : failed to process Destroy - {0}", ba.BaseData.Name);
                    }

                    val.RemoveAt(i);
                }
            }
        }
    }
}