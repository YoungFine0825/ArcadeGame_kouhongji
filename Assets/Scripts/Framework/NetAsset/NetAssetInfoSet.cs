/********************************************************************
	created:	2018-11-23
	author:		jordenwu
	
	purpose:	网络资产信息 集合 用于记录当前的所有资产信息
*********************************************************************/
using JW.Common;
using System;

namespace JW.Framework.NetAsset
{
    public class NetAssetInfoSet
    {
        public const int Version = 10000;

        private JWObjList<NetAssetInfo> _cachedAssetInfos = new JWObjList<NetAssetInfo>();
        private JWObjDictionary<string, NetAssetInfo> _cachedAssetInfoMap = new JWObjDictionary<string, NetAssetInfo>();

        /// <summary>
        /// 写成配置
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        public void Write(byte[] data, ref int offset)
        {
            int startOffset = offset;
            offset += 4;
            MemoryOperator.WriteShort((short)Version, data, ref offset);
            MemoryOperator.WriteShort((short)_cachedAssetInfos.Count, data, ref offset);
            //
            for (int i = 0; i < _cachedAssetInfos.Count; i++)
            {
                NetAssetInfo info = _cachedAssetInfos[i];
                //Key
                MemoryOperator.WriteString(info.Key, data, ref offset);
                MemoryOperator.WriteShort((short)info.AssetType, data, ref offset);
                if (info.AssetType == NetAssetType.Image)
                {
                    MemoryOperator.WriteShort((short)info.ImageWidth, data, ref offset);
                    MemoryOperator.WriteShort((short)info.ImageHeight, data, ref offset);
                }
                //最后修改时间
                MemoryOperator.WriteDateTime(ref info.LastModifyTime, data, ref offset);
            }
            MemoryOperator.WriteInt(offset - startOffset, data, ref startOffset);
        }

        /// <summary>
        /// 读取配置
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        public void Read(byte[] data, ref int offset)
        {
            _cachedAssetInfos.Clear();
            _cachedAssetInfoMap.Clear();

            if (data == null)
            {
                return;
            }

            int dataLength = data.Length - offset;

            if (dataLength < 6)
            {
                return;
            }

            int storedDataLength = MemoryOperator.ReadInt(data, ref offset);
            if (storedDataLength < 6 || storedDataLength > dataLength)
            {
                return;
            }
            //
            int version = MemoryOperator.ReadShort(data, ref offset);
            if (version != Version)
            {
                return;
            }
            //信息数量
            int amount = MemoryOperator.ReadShort(data, ref offset);
            for (int i = 0; i < amount; i++)
            {
                NetAssetInfo info = new NetAssetInfo();
                info.Key = MemoryOperator.ReadString(data, ref offset);
                info.AssetType =(NetAssetType)MemoryOperator.ReadShort(data, ref offset);
                if (info.AssetType == NetAssetType.Image)
                {
                    info.ImageWidth = MemoryOperator.ReadShort(data, ref offset);
                    info.ImageHeight = MemoryOperator.ReadShort(data, ref offset);
                }
                //
                info.LastModifyTime = MemoryOperator.ReadDateTime(data, ref offset);
                //
                if (!_cachedAssetInfoMap.ContainsKey(info.Key))
                {
                    _cachedAssetInfoMap.Add(info.Key, info);
                    _cachedAssetInfos.Add(info);
                }
            }
            //排序
            _cachedAssetInfos.Sort();
        }

        /// <summary>
        /// 获取信息记录
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public NetAssetInfo GetCachedNetAssetInfo(string key)
        {
            if (_cachedAssetInfoMap.ContainsKey(key))
            {
                NetAssetInfo info = null;
                _cachedAssetInfoMap.TryGetValue(key, out info);

                return info;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取缓存的资源总数
        /// </summary>
        /// <returns></returns>
        public int GetCachedNetAssetCnt()
        {
            return _cachedAssetInfos.Count;
        }

        /// <summary>
        /// 添加信息记录
        /// </summary>
        /// <param name="key"></param>
        /// <param name="info"></param>
        public void AddNetAssetInfo(string key, NetAssetInfo info)
        {
            if (_cachedAssetInfoMap.ContainsKey(key))
            {
                return;
            }

            _cachedAssetInfoMap.Add(key, info);
            _cachedAssetInfos.Add(info);
        }

        /// <summary>
        /// 移除最早的资产
        /// </summary>
        /// <returns></returns>
        public string RemoveEarliestNetAssetInfo()
        {
            if (_cachedAssetInfos.Count <= 0)
            {
                return null;
            }

            NetAssetInfo info = _cachedAssetInfos[0];

            _cachedAssetInfos.RemoveAt(0);
            _cachedAssetInfoMap.Remove(info.Key);

            return info.Key;
        }


        public void RemoveNetAssetInfoAndDelFileByDays(float days)
        {
            for(int i = _cachedAssetInfos.Count-1; i >=0; i--)
            {
                NetAssetInfo info = _cachedAssetInfos[i];
                if ((DateTime.Now - info.LastModifyTime).TotalDays >= days)
                {
                    //彻底清除
                    JW.Res.FileUtil.DeleteFile(info.GetCachedFilePath());
                    _cachedAssetInfoMap.Remove(info.Key);
                    _cachedAssetInfos.RemoveAt(i);
                }
            }
        }

        //
        public void RemoveAllNetAssetInfo()
        {
            _cachedAssetInfos.Clear();
            _cachedAssetInfoMap.Clear();
        }

        /// <summary>
        /// 按时间排序
        /// </summary>
        public void SortNetAssetInfo()
        {
            _cachedAssetInfos.Sort();
        }
    };
}
