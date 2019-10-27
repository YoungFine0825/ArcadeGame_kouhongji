using System;
using JW.Common;
namespace JW.Framework.Asset
{
    public static class AssetAssistor
    {
        /// <summary>
        /// 根据UI状态判断资产是否失效
        /// </summary>
        /// <param name="data">资产记录数据</param>
        /// <param name="uiState">UI状态</param>
        /// <returns></returns>
        public static bool IsAssetDead(ref AssetData data,JWArrayList<string> uiState)
        {
            if (uiState == null)
            {
                JW.Common.Log.LogE("AssetHelper.IsAssetDead : invalid parameter");
                return false;
            }

            if (data.Life == LifeType.Resident)
            {
                return false;
            }

            if (data.Life == LifeType.Manual)
            {
                return false;
            }

            if (data.Life == LifeType.UIState &&
                !string.IsNullOrEmpty(data.StateName) &&
                uiState.IndexOf(data.StateName, StringComparer.OrdinalIgnoreCase) != -1)
            {
                return false;
            }

            return true;
        }
    }
}