/********************************************************************
	created:	2018-11-9
	author:		jordenwu
	
	purpose:    质量管理
*********************************************************************/
using UnityEngine;
using JW.Common;
using JW.Framework.Asset;
using UnityEngine.Rendering;

namespace JW.Framework.Quality
{
    public class QualityService : Singleton<QualityService>
    {
        private BaseAsset _curRefectionCubMapAsset;


        public override bool Initialize()
        {
           
            return true;
        }

        public override void Uninitialize()
        {

            if (_curRefectionCubMapAsset != null)
            {
                AssetService.GetInstance().Unload(_curRefectionCubMapAsset);
                _curRefectionCubMapAsset = null;
            }
        }


        public void LoadGlobalReflectionCubeMap(float intensity, string resPath)
        {
            if (string.IsNullOrEmpty(resPath))
            {
                JW.Common.Log.LogE("LoadGlobalReflectionCubeMap Error CubeMap");
            }

            RenderSettings.reflectionIntensity = intensity;
            RenderSettings.defaultReflectionMode = DefaultReflectionMode.Custom;

            BaseAsset ba = AssetService.GetInstance().LoadPrimitiveAsset(resPath,4);
            if (ba == null)
            {
                JW.Common.Log.LogE("LoadGlobalReflectionCubeMap Error CubeMap");
            }
            else
            {
                if (_curRefectionCubMapAsset != null)
                {
                    AssetService.GetInstance().Unload(_curRefectionCubMapAsset);
                    _curRefectionCubMapAsset = null;
                }

                RenderSettings.customReflection = ba.Resource.Content as Cubemap;
                _curRefectionCubMapAsset = ba;
            }
           
        }

        public void UnloadGlobalReflectionCubeMap()
        {
            RenderSettings.customReflection = null;
            if (_curRefectionCubMapAsset!=null)
            {
                AssetService.GetInstance().Unload(_curRefectionCubMapAsset);
                _curRefectionCubMapAsset = null;
            }
        }


    }
}
