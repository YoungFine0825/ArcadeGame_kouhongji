/********************************************************************
	created:	2018-11-23
	author:		jordenwu
	
	purpose:	网络资产信息 记录
*********************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JW.Framework.NetAsset
{
    /// <summary>
    /// 网络资源类型
    /// </summary>
    public enum NetAssetType
    {
        //图片
        Image=1,
        //声音
        Audio=2,
        //视频
        Video=3,
        //预制件包
        Bundle=4,
        //非图片 234
        NotImage=5,
    }

    /// <summary>
    /// 网络资产信息记录
    /// </summary>
    public class NetAssetInfo : IComparable
    {
        public string Key;
        //最后更改时间
        public DateTime LastModifyTime;
        //
        public NetAssetType AssetType=NetAssetType.NotImage;
        public int ImageWidth = 0;
        public int ImageHeight = 0;

        public int CompareTo(object obj)
        {
            NetAssetInfo cInfo = obj as NetAssetInfo;

            if (LastModifyTime > cInfo.LastModifyTime)
            {
                return 1;
            }
            else if (LastModifyTime == cInfo.LastModifyTime)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 缓存文件路径
        /// </summary>
        /// <returns></returns>
        public string GetCachedFilePath()
        {
            return JW.Res.FileUtil.CombinePath(NetAssetService.S_CachedNetAssetDirectory, Key + ".bytes");
        }


    };


    
}
