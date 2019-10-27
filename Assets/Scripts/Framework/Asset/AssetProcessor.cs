/********************************************************************
	created:	2018-05-28   
	filename: 	AssetProcessor
	author:		jordenwu
	
	purpose:	资产 加载 回收 销毁 处理器
*********************************************************************/
using UnityEngine;
using JW.Res;
using JW.Common;
using System;
using JW.Framework.UGUI;
using JW.Framework.MVC;
using JW.PLink;

namespace JW.Framework.Asset
{
    /// <summary>
    /// 资产处理器
    /// </summary>
    public static class AssetProcessor
    {
        public static BaseAsset CreateAssetClass(int type)
        {
            switch (type)
            {
                case AssetType.Model:
                    return new ModelAsset();
                case AssetType.Instantiate:
                    return new BaseAsset();
                case AssetType.UI:
                    return new UIAsset();
                case AssetType.Audio:
                    return new AudioAsset();
                case AssetType.Sprite:
                    return new SpriteAsset();
                case AssetType.Primitive:
                    return new BaseAsset();
            }
            JW.Common.Log.LogE("AssetProcessor.CreateAssetClass : invalid type - {0}", type);
            return null;
        }

        /// <summary>
        /// 资产创建预处理
        /// </summary>
        /// <param name="ba"></param>
        /// <returns></returns>
        public static bool ProcessCreate(BaseAsset ba)
        {
            if (ba == null || ba.Resource == null || ba.Resource.Content == null)
            {
                JW.Common.Log.LogE("AssetProcessor.ProcessCreate : invalid parameter");
                return false;
            }

            switch (ba.BaseData.Type)
            {
                case AssetType.UIForm:
                    {
                        UIFormAsset ast = ba as UIFormAsset;
                        if (ast == null)
                        {
                            JW.Common.Log.LogE("AssetProcessor.ProcessCreate : type is not object - {0}", ba.BaseData.Name);
                            return false;
                        }

                        if (!InstantiateAsset(ast))
                        {
                            return false;
                        }
                        //逻辑处理
                        ast.OnFormAssetCreate();
                    }
                    break;

                case AssetType.Model:
                    {
                        ModelAsset ast = ba as ModelAsset;
                        if (ast == null)
                        {
                            JW.Common.Log.LogE("AssetProcessor.ProcessCreate : type is not model - {0}", ba.BaseData.Name);
                            return false;
                        }

                        if (!InstantiateAsset(ast))
                        {
                            return false;
                        }

                        ast.Render = ast.RootGo.ExtGetComponentsInChildren<Renderer>(true);
                        ast.AnimationCpt = ast.RootGo.ExtGetComponentInChildren<Animation>();
                        ast.AnimatorCtrl = ast.RootGo.ExtGetComponentInChildren<Animator>();
                    }
                    break;
                case AssetType.UI:
                    {
                        UIAsset ast = ba as UIAsset;
                        if (ast == null)
                        {
                            JW.Common.Log.LogE("AssetProcessor.ProcessCreate : type is not uiasset - {0}", ba.BaseData.Name);
                            return false;
                        }

                        if (!InstantiateAsset(ba))
                        {
                            return false;
                        }
                        ast.FormCom = ast.RootGo.ExtGetComponent<UIForm>();
                        ast.PLinkCom = ast.RootGo.ExtGetComponent<PrefabLink>();
                    }
                    break;
                //基础实例
                case AssetType.Instantiate:
                    {
                        if (!InstantiateAsset(ba))
                        {
                            return false;
                        }
                    }
                    break;
                case AssetType.Audio:
                    {
                        AudioAsset ast = ba as AudioAsset;
                        if (ast == null)
                        {
                            JW.Common.Log.LogE("AssetProcessor.ProcessCreate : type is not audioclip - {0}", ba.BaseData.Name);
                            return false;
                        }

                        ast.Clip = ba.Resource.Content as AudioClip;
                        if (ast.Clip == null)
                        {
                            JW.Common.Log.LogE("AssetProcessor.ProcessCreate : resource is not audioclip - {0}", ba.BaseData.Name);
                            return false;
                        }
                    }
                    break;
                case AssetType.Sprite:
                    {
                        SpriteAsset ast = ba as SpriteAsset;
                        if (ast == null)
                        {
                            JW.Common.Log.LogE("AssetProcessor.ProcessCreate : type is not SpriteAsset - {0}", ba.BaseData.Name);
                            return false;
                        }

#if UNITY_EDITOR || !USE_PACK_RES
                        Texture2D tt = ba.Resource.Content as Texture2D;
                        ast.SpriteObj = Sprite.Create(tt, new Rect(0, 0, tt.width, tt.height), new Vector2(0.5f, 0.5f));
#else
                        
                        Sprite ss = ba.Resource.Content as Sprite;
                        if (ss == null)
                        {
                            JW.Common.Log.LogD("SpriteAsset Load From AB Is Texture2D Type");
                            Texture2D tt1 = ba.Resource.Content as Texture2D;
                            ast.SpriteObj = Sprite.Create(tt1, new Rect(0, 0, tt1.width, tt1.height), new Vector2(0.5f, 0.5f));
                        }
                        else
                        {
                            ast.SpriteObj = ss;
                        }
#endif
                        if (ast.SpriteObj == null)
                        {
                            JW.Common.Log.LogE("AssetProcessor.ProcessCreate : resource is not Sprite - {0}", ba.BaseData.Name);
                            return false;
                        }
                    }
                    break;
            }

            return true;
        }

        /// <summary>
        /// 处理回收
        /// </summary>
        /// <param name="ba"></param>
        public static void ProcessRestore(BaseAsset ba)
        {
            if (ba == null)
            {
                JW.Common.Log.LogE("AssetProcessor.ProcessRestore : invalid parameter");
                return;
            }
            //基础重置
            if (ba.RootTf != null)
            {
                ba.RootTf.localPosition = ba.OrignalPosition;
                ba.RootTf.localRotation = ba.OrignalRotate;
                ba.RootTf.localScale = ba.OrignalScale;
            }
            //
            switch (ba.BaseData.Type)
            {
                case AssetType.Model:
                    {
                        //ModelAsset ma = ba as ModelAsset;
                        //回收处理
                    }
                    break;
                case AssetType.UIForm:
                    {
                        //回收处理
                    }
                    break;
                case AssetType.UI:
                    {
                        //回收处理
                        UIAsset aa = ba as UIAsset;
                        aa.FormCom = null;
                        aa.RootPLink = null;
                    }
                    break;
            }
        }

        /// <summary>
        /// 处理销毁
        /// </summary>
        /// <param name="ba"></param>
        /// <returns></returns>
        public static bool ProcessDestroy(BaseAsset ba)
        {
            if (ba == null)
            {
                JW.Common.Log.LogE("AssetProcessor.ProcessDestroy : invalid parameter");
                return false;
            }

            switch (ba.BaseData.Type)
            {
                case AssetType.UIForm:
                    {
                        UIFormAsset ast = ba as UIFormAsset;
                        if (ast == null)
                        {
                            JW.Common.Log.LogE("AssetProcessor.ProcessDestroy : type is not object - {0}", ba.BaseData.Name);
                            return false;
                        }

                        ast.OnFormAssetDestroy();
                        DestroyAsset(ast);
                    }
                    break;
                case AssetType.Model:
                    {
                        ModelAsset ast = ba as ModelAsset;
                        if (ast == null)
                        {
                            JW.Common.Log.LogE("AssetProcessor.ProcessDestroy : type is not model - {0}", ba.BaseData.Name);
                            return false;
                        }

                        ast.AnimationCpt = null;
                        ast.AnimatorCtrl = null;
                        DestroyAsset(ast);
                    }
                    break;
                case AssetType.UI:
                    {
                        UIAsset ast = ba as UIAsset;
                        if (ast == null)
                        {
                            JW.Common.Log.LogE("AssetProcessor.ProcessDestroy : type is not ui - {0}", ba.BaseData.Name);
                            return false;
                        }

                        ast.FormCom = null;
                        ast.PLinkCom = null;
                        DestroyAsset(ast);
                    }
                    break;

                case AssetType.Instantiate:
                    {
                        DestroyAsset(ba);
                    }
                    break;
                case AssetType.Audio:
                    {
                        AudioAsset ast = ba as AudioAsset;
                        if (ast == null)
                        {
                            JW.Common.Log.LogE("AssetProcessor.ProcessDestroy : type is not audio - {0}", ba.BaseData.Name);
                            return false;
                        }
                        ast.Clip = null;
                    }

                    break;
                case AssetType.Sprite:
                    {
                        SpriteAsset ast = ba as SpriteAsset;
                        if (ast == null)
                        {
                            JW.Common.Log.LogE("AssetProcessor.ProcessDestroy : type is not SpriteAsset - {0}", ba.BaseData.Name);
                            return false;
                        }
                        ast.SpriteObj = null;
                    }
                    break;
            }
            return true;
        }

        /// <summary>
        /// clone处理
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static BaseAsset ProcessClone(BaseAsset s)
        {
            //todo 上层逻辑 池化 这里禁止clone
            return null;
            /*
            if (s == null)
            {
                JW.Common.Log.LogE("ProcessClone Error Param");
                return null;
            }
            BaseAsset ba =s.BaseData.Factory.CreateObject();
            if (ba == null)
            {
                JW.Common.Log.LogE("ProcessClone : failed to create asset - {0}", s.BaseData.Name);
                return null;
            }
            //
            ba.BaseData = s.BaseData;
            ba.Resource = s.Resource;
            //资源对象引用++
            ba.Resource.RefCnt++;
            //
            if (ba == null || ba.Resource == null || ba.Resource.Content == null)
            {
                JW.Common.Log.LogE("AssetProcessor.ProcessCreate : invalid parameter");
                return null;
            }
            //
            switch (ba.BaseData.Type)
            {
                case AssetType.UIForm:
                    {
                        //窗口禁止clone
                        return null;
                    }
                case AssetType.Model:
                    {
                        ModelAsset ast = ba as ModelAsset;
                        if (ast == null)
                        {
                            JW.Common.Log.LogE("AssetProcessor.ProcessCreate : type is not model - {0}", ba.BaseData.Name);
                            return null;
                        }
                        if (!InstantiateAsset(ast))
                        {
                            return null;
                        }
                        ast.Render = ast.RootGo.ExtGetComponentsInChildren<Renderer>(true);
                        ast.AnimationCpt = ast.RootGo.ExtGetComponentInChildren<Animation>();
                        ast.AnimatorCtrl = ast.RootGo.ExtGetComponentInChildren<Animator>();
                    }
                    break;
                //基础实例
                case AssetType.Instantiate:
                    {
                        if (!InstantiateAsset(ba))
                        {
                            return null;
                        }
                    }
                    break;
            }
            return ba;
            */
        }

        /// <summary>
        /// 关联资源
        /// </summary>
        /// <param name="ba"></param>
        public static void ProcessAssociateResource(BaseAsset ba)
        {
            if (ba == null)
            {
                JW.Common.Log.LogE("AssetProcessor.ProcessAssociateResource : invalid parameter");
                return;
            }
        }

        /// <summary>
        /// 实例化资产
        /// </summary>
        /// <param name="ba"></param>
        /// <returns></returns>
        private static bool InstantiateAsset(BaseAsset ba)
        {
            if (ba.RootGo == null)
            {
                ba.RootGo = (GameObject)ba.Resource.Content.ExtInstantiate();
            }

            if (ba.RootGo == null)
            {
                JW.Common.Log.LogE("AssetProcessor.Instantiate : failed to instantiate - {0}", ba.BaseData.Name);
                return false;
            }

            ba.RootGo.ExtSetActive(false);
            ba.RootTf = ba.RootGo.transform;
            ba.OrignalPosition = ba.RootTf.localPosition;
            ba.OrignalRotate = ba.RootTf.localRotation;
            ba.OrignalScale = ba.RootTf.localScale;
            ba.RootPLink = ba.RootGo.GetComponent<PrefabLink>();
            //
            return true;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        /// <param name="ba"></param>
        private static void DestroyAsset(BaseAsset ba)
        {
            if (ba.RootGo != null)
            {
                ba.RootGo.ExtSetActive(false);

                ba.RootGo.ExtDestroy();
            }

            ba.RootGo = null;
            ba.RootTf = null;
            ba.RootTf = null;
            ba.RootPLink = null;
        }
    }
}
