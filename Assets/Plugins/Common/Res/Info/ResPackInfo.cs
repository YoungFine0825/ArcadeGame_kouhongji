/********************************************************************
	created:	18:5:2018   
	filename: 	ResPackInfo
	author:		jordenwu
	
	purpose:	资源包信息定义
*********************************************************************/
using System.Collections.Generic;
using UnityEngine;
using JW.Common;

namespace JW.Res
{
    /// <summary>
    /// 资源包类型定义
    /// </summary>
    [System.Serializable]
    public enum ResPackType
    {   
        //预制件文件集合
        ResPackTypeBundle = 0,
        //二进制文件集合
        ResPackTypeBinary = 1,
    }

    /// <summary>
    /// 资源包信息定义基础类
    /// </summary>
    [System.Serializable]
    public class ResPackInfo
    {
        //资源包名 或者二进制文件夹名
        [SerializeField]
        public string Path;
        //包含资源列表
        [SerializeField]
        public List<ResInfo> Resources = new List<ResInfo>();
        
        // 资源数
        public int Count
        {
            get
            {
                if (Resources == null)
                {
                    return 0;
                }
                return Resources.Count;
            }
        }

        /// <summary>
        /// 取包类型 二进制包 还是资源assetbundle 包
        /// </summary>
        /// <returns></returns>
        public virtual byte GetPackType()
        {
            return 0;
        }

        /// <summary>
        /// 读二进制 配置初始化
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        public virtual void Read(byte[] data, ref int offset)
        {
            // Path
            Path = MemoryOperator.ReadString(data, ref offset);
            // resource count
            int count = MemoryOperator.ReadShort(data, ref offset);

            // resources
            for (int i = 0; i < count; i++)
            {
                ResInfo resource = new ResInfo();
                resource.Path = MemoryOperator.ReadString(data, ref offset);
                resource.Ext = MemoryOperator.ReadString(data, ref offset);
                Resources.Add(resource);
            }
            // check location
            CheckLocation();
        }

        //处理依赖
        public virtual void ProcessDependency(List<ResPackInfo> loaded)
        {

        }
        
        /// <summary>
        /// 写二进制配置
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        public virtual void Write(byte[] data, ref int offset)
        {
            // type
            byte type = GetPackType();
            MemoryOperator.WriteByte(type, data, ref offset);
            // Path 
            if (type == 0)
            {
                //小写
                MemoryOperator.WriteString(Path.ToLower(), data, ref offset);
            }
            else
            {
                MemoryOperator.WriteString(Path, data, ref offset);
            }
            // resources
            MemoryOperator.WriteShort((short)Resources.Count, data, ref offset);
            for (int i = 0; i < Resources.Count; i++)
            {
                MemoryOperator.WriteString(Resources[i].Path, data, ref offset);
                MemoryOperator.WriteString(Resources[i].Ext, data, ref offset);
            }
        }

        /// <summary>
        /// 位置检查
        /// </summary>
        /// <returns></returns>
        protected virtual bool CheckLocation()
        {
            return false;
        }

        /// <summary>
        /// 单个资源是否在外磁盘
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool IsOutside(string path)
        {
            for (int i = 0; i < Count; i++)
            {
                ResInfo r = Resources[i];
                if (r.Path.Equals(path, System.StringComparison.OrdinalIgnoreCase))
                {
                    return r.Outside;
                }
            }
            return false;
        }

        /// <summary>
        /// 添加资源
        /// </summary>
        /// <param name="resource"></param>
        public void Add(ref ResInfo resource)
        {
            if (!Resources.Contains(resource))
            {
                Resources.Add(resource);
            }
        }

        /// <summary>
        /// 是否包含指定资源
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool Contains(string path)
        {
            for (int i = 0; i < Resources.Count; i++)
            {
                if (string.Equals(path, Resources[i].Path, System.StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 获取资源扩展名
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetResExt(string path)
        {
            for (int i = 0; i < Resources.Count; i++)
            {
                if (string.Equals(path, Resources[i].Path, System.StringComparison.OrdinalIgnoreCase))
                {
                    return Resources[i].Ext;
                }
            }
            return null;
        }


        /// <summary>
        /// 是否有重名资源
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool IsDuplicated(string path, out string duplicatedFile)
        {
            duplicatedFile = "";

            string name = FileUtil.EraseExtension(path);
            for (int i = 0; i < Resources.Count; i++)
            {
                string alreadyContainsName = Resources[i].Path;
                if (string.Equals(alreadyContainsName, name, System.StringComparison.OrdinalIgnoreCase))
                {
                    duplicatedFile = Resources[i].Path + Resources[i].Ext;
                    return true;
                }
            }

            return false;
        }
    }
}
