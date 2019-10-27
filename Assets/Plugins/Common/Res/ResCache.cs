
using System;
using JW.Common;

namespace JW.Res
{
    /// <summary>
    /// 资源cache
    /// </summary>
    public class ResCache
    {
        JWObjDictionary<string, ResObj> _resources;

        public ResCache()
        {
            _resources = new JWObjDictionary<string, ResObj>(StringComparer.OrdinalIgnoreCase);
        }

        ~ResCache()
        {
            _resources.Clear();
            _resources = null;
        }

        /// <summary>
        /// 添加资源到缓存
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <returns>创建的资源对象</returns>
        public ResObj Add(string path)
        {
            ResObj resource;
            if (_resources.TryGetValue(path, out resource))
            {
                if (resource.RefCnt == 0)
                {
                    resource.UsingCache = true;
                }

                resource.RefCnt++;
            }
            else
            {
                resource = ResObj.Get(path);
                _resources.Add(path, resource);
            }

            return resource;
        }

        /// <summary>
        /// 移除资源对象
        /// </summary>
        /// <param name="resource">资源对象</param>
        /// <param name="immediately">是否立即删除</param>
        /// <returns>true：表示所有同路径资源都已移除完</returns>
        public bool Remove(ResObj resource, bool immediately)
        {
            if (resource == null)
            {
                return true;
            }

            if (_resources.ContainsKey(resource.OriginPath))
            {
                if (immediately)
                {
                    resource.Unload();
                    _resources.Remove(resource.OriginPath);
                    ResObj.Recycle(resource);

                    return true;
                }
                else
                {
                    if (resource.RefCnt > 0)
                    {
                        resource.RefCnt--;
                    }
                    else
                    {
                        JW.Common.Log.LogE("Unloading resource [{0}] while its reference count is zero.", resource.Path);
                    }

                    return resource.RefCnt <= 0;
                }
            }
            else
            {
                resource.Unload();
                ResObj.Recycle(resource);
                return true;
            }
        }

        /// <summary>
        /// 资源是否存在
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public bool Exists(string resource)
        {
            return _resources.ContainsKey(resource);
        }

        /// <summary>
        /// 卸载资源
        /// </summary>
        public void UnloadUnusedResources(JWObjList<string> willUseAssets = null)
        {
            JWObjList<string> unloaded = null;

            var itor = _resources.GetEnumerator();
            while (itor.MoveNext())
            {
                if (itor.Current.Value.RefCnt <= 0)
                {
                    if (willUseAssets != null && willUseAssets.Contains(itor.Current.Key))
                    {
                        willUseAssets.Remove(itor.Current.Key);
                        continue;
                    }

                    if (unloaded == null)
                    {
                        unloaded = new JWObjList<string>();
                    }

                    unloaded.Add(itor.Current.Key);

                    // unload
                    itor.Current.Value.Unload();

                    // recycle ResObj instance 回池
                    ResObj.Recycle(itor.Current.Value);
                }
            }

            for (int i = 0; unloaded != null && i < unloaded.Count; i++)
            {
                _resources.Remove(unloaded[i]);
            }
        }
    }
}
