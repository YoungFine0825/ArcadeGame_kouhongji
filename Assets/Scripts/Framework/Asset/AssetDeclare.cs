/********************************************************************
	created:	2018-05-28   
	filename: 	AssetDeclare
	author:		jordenwu
	
	purpose:    资产相关声明
*********************************************************************/
using JW.Common;
using JW.Res;
using UnityEngine;
using JW.Framework.MVC;
using JW.Framework.UGUI;
using JW.PLink;

namespace JW.Framework.Asset
{
    /// <summary>
    /// 加载优先级
    /// </summary>
    public static class LoadPriority
    {
        //阻塞 最高
        public const int Wait = 0;

        //推荐UI 模型 最高级异步
        public const int ImmediateShow = 1;

        //异步中 推荐 特效
        public const int SpareShow = 10000;

        //异步低 推荐当前不需要的
        public const int Silent = 20000;

        //异步的形式加载预处理的资源 最低
        public const int Preprocess = 30000;
    }

    /// <summary>
    /// 资产加载结果
    /// </summary>
    public static class AssetLoadResult
    {
        public const int Success = 0;

        public const int BundleFail = 1;

        public const int ResourceFail = 2;
    }

    /// <summary>
    /// 资产加载回调
    /// </summary>
    public interface IAssetLoadCallback
    {
        /// <summary>
        /// 资产加载完成
        /// </summary>
        /// <param name="assetName">资产名</param>
        /// <param name="result">加载结果，取值为AssetLoadResult</param>
        /// <param name="resource">资源</param>
        void OnLoadAssetCompleted(string assetName, int result, ResObj resource);
    }

    /// <summary>
    /// 资产类型定义
    /// </summary>
    public static class AssetType
    {
        // UI预制件
        public const int UI = 0;
        
        // 特效
        public const int UIParticle = 1;

        // 3D模型
        public const int Model = 2;

        // 纹理 散图
        public const int Texture = 3;

        // 实例化资源 
        public const int Instantiate = 4;

        //不实例化资源 
        public const int Primitive = 5;

        //音效
        public const int Audio = 6;

        //2D精灵
        public const int Sprite=7;

        //基础资源总数
        public const int BaseAssetTypeCount = 8;

        //UIFormClass对应的窗口实例预制
        public const int UIForm = 50;

        // Unity场景
        public const int UnityScene = 99;

        // 外部资源
        public const int External = 100;

    }

    /// <summary>
    /// Asset生命周期
    /// </summary>
    public static class LifeType
    {
        // 常驻
        public const int Resident = 0;

        // 跟随UI状态缓存
        public const int UIState = 2;

        //不用时立即销毁 
        public const int Immediate = 3;

        //手动管理 
        public const int Manual = 4;
 
    }

    /// <summary>
    /// 资产数据信息
    /// </summary>
    public struct AssetData
    {
        public int Priority;
        public IAssetLoadCallback Callback;

        public int Type;
        public string Name;
        public string Filename;

        public IObjectFactory<BaseAsset> Factory;

        public int Life;
        public string StateName;

        public int Count;
    }

    /// <summary>
    /// 基础资产定义
    /// </summary>
    public class BaseAsset
    {
        //内部使用的数据结构，外部不要修改也不要访问
        public AssetData BaseData;  
        //对应的资源对象
        public ResObj Resource;

        public GameObject RootGo;
        public Transform RootTf;

        public PrefabLink RootPLink;

        public Vector3 OrignalPosition;
        public Quaternion OrignalRotate;
        public Vector3 OrignalScale;

        public GameObject Go
        {
            get
            {
                return RootGo;
            }
        }

        public Transform Tf
        {
            get
            {
                return RootTf;
            }
        }
    }

    /// <summary>
    /// UI窗口资产
    /// </summary>
    public abstract class UIFormAsset : BaseAsset
    {
        public abstract void OnFormAssetCreate();
        public abstract void OnFormAssetDestroy();
        public abstract string GetPath();
    }

    /// <summary>
    ///  UI资产 
    /// </summary>
    public  class UIAsset : BaseAsset
    {
        public UIForm FormCom;
        public PrefabLink PLinkCom;
    }

    /// <summary>
    /// 模型资产
    /// </summary>
    public class ModelAsset : BaseAsset
    {
        public Renderer[] Render;
        public Animation AnimationCpt;
        public Animator AnimatorCtrl;
    }

    public class SpriteAsset : BaseAsset
    {
        public Sprite SpriteObj;
    }

    /// 声音资源
    /// </summary>
    public class AudioAsset : BaseAsset
    {
        public AudioClip Clip;
    }

}
