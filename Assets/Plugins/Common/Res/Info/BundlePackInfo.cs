/********************************************************************
	created:	18:5:2018   
	filename: 	BundlePackInfo
	author:		jordenwu
	
	purpose:	AssetBundle信息定义
*********************************************************************/
using System.Collections.Generic;
using UnityEngine;
using JW.Common;

namespace JW.Res
{
    /// <summary>
    /// bundle生命周期
    /// </summary>
    [System.Serializable]
    public enum EBundleLife
    {
        Cache,     // 缓存，在UnloadUnusedBundles时卸载
        Immediate, // 用时加载，用完释放，默认选项
        Resident,  // 常驻，更新资源后加载，Logout卸载
    }

    /// <summary>
    /// bundle 包标记 标记AssetBundle 的打包方式 加载方式等
    /// </summary>
    [System.Serializable]
    public enum EBundleFlag
    {
        None = 0,
        UnCompress = 1,                 //非压缩的Bundle 
        Resident = 1 << 1,              //常驻的Bundle，只有在没有依赖父Bundle的时候才有效
        LZMA = 1 << 2,                  //LZMA压缩方式
        LZ4 = 1 << 3,                   // ChunkBasedCompression压缩方式
        UnityScene = 1 << 4,            // 是否是Unity场景
        NoBundle=1<<5,                  // 表示资源预制件 可以打包但是同样存在Resource目录
        PreLoad=1<<6,                   //预加载
        Optional = 1 << 7,              // 跳过
    }

    /// <summary>
    /// Bundle包信息定义
    /// </summary>
    [System.Serializable]
    public class BundlePackInfo : ResPackInfo
    {
        //标记
        [SerializeField]
        public int Flags;
        //内存方式
        [SerializeField]
        public EBundleLife Life;
        //状态
        [SerializeField]
        public string State;
        //依赖包Name 逗号分隔
        [SerializeField]
        public string DependencyNames;

        //依赖AssetBundle包信息列表
        [SerializeField]
        public List<BundlePackInfo> Dependencies=null;

        // 是否存储在用户磁盘 通过check location 设置了内部列表位置
        public bool Outside
        {
            get
            {
                return (Count > 0 && Resources[0].Outside);
            }
        }

        /// <summary>
        /// 资源预制件 assetbundle 
        /// </summary>
        /// <returns></returns>
        public override byte GetPackType()
        {
            return (byte)ResPackType.ResPackTypeBundle;
        }

        /// <summary>
        /// 读二进制配置初始化
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        public override void Read(byte[] data, ref int offset)
        {
            base.Read(data, ref offset);
			Path = Path.ToLower ();
            // Flags
            Flags = MemoryOperator.ReadInt(data, ref offset);

            // Life
            Life = (EBundleLife)MemoryOperator.ReadShort(data, ref offset);
            
            // Dependency
            int hasDependency = MemoryOperator.ReadByte(data, ref offset);
            if (hasDependency == 1)
            {
                DependencyNames = MemoryOperator.ReadString(data, ref offset);
            }            
        }
        
        /// <summary>
        /// 处理依赖关系
        /// </summary>
        /// <param name="loaded"></param>
        public override void ProcessDependency(List<ResPackInfo> loaded)
        {
            if (string.IsNullOrEmpty(DependencyNames) || loaded == null || loaded.Count == 0)
            {
                return;
            }

            string[] tags = DependencyNames.Split(',');
            for (int j = 0; j < tags.Length; j++)
            {
                string tag = tags[j];

                for (int i = 0; i < loaded.Count; i++)
                {
                    BundlePackInfo info = loaded[i] as BundlePackInfo;
                    if (info == null)
                    {
                        continue;
                    }

                    if (info.Path.Equals(tag,System.StringComparison.OrdinalIgnoreCase))
                    {
                        if (Dependencies == null)
                        {
                            Dependencies = new List<BundlePackInfo>(1);
                        }
                        Dependencies.Add(info);
                    }
                }
            }
            if (Dependencies == null)
            {
                JW.Common.Log.LogE("Specify DependencyNames {0}, but no dependency instance. bundle:{1}", DependencyNames, Path);
            }
        }

        /// <summary>
        /// 检查标识位
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public bool HasFlag(EBundleFlag flag)
        {
            int flagInt = (int)flag;
            return (Flags & flagInt) > 0;
        }

        //表示资源包的资源 出档时候存在于Resource 目录
        public bool IsNoBundle()
        {
            return HasFlag(EBundleFlag.NoBundle); 
        }

        /// <summary>
        /// 位置检查
        /// </summary>
        /// <returns></returns>
        protected override bool CheckLocation()
        {
            if (!base.CheckLocation())
            {
                bool outside = FileUtil.IsExistInIFSExtraFolder(Path);
                //内部资源都标记在外面
                for (int i = 0; i < Count; i++)
                {
                    ResInfo r = Resources[i];
                    r.Outside = outside;
                }
            }
            return true;
        }

#if UNITY_EDITOR || UNITY_STANDALONE

        /// <summary>
        /// 写二进制配置
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        public override void Write(byte[] data, ref int offset)
        {
            base.Write(data, ref offset);
            // Flags
            MemoryOperator.WriteInt(Flags, data, ref offset);

            // Life
            MemoryOperator.WriteShort((short)Life, data, ref offset);
            
            // Dependency
            if (string.IsNullOrEmpty(DependencyNames))
            {
                MemoryOperator.WriteByte(0, data, ref offset);
            }
            else
            {
                MemoryOperator.WriteByte(1, data, ref offset);
                MemoryOperator.WriteString(DependencyNames, data, ref offset);
            }
        }
#endif
    }

}
