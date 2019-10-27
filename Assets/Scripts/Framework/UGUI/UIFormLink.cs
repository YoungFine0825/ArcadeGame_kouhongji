/********************************************************************
	created:	2018-06-14
	filename: 	UIPrefabLink
	author:		jordenwu
	
	purpose:	UI窗口链接
*********************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace JW.Framework.UGUI
{
    [RequireComponent(typeof(UnityEngine.UI.CanvasScaler))]
    public class UIFormLink : MonoBehaviour
    {
        /// <summary>
        /// 窗口预制件上UI元素定义
        /// </summary>
        [Serializable]
        public class UIElement
        {
            //组件
            public Component Component;
            //名称
            public string Name;
        }

        //Form所包含的UI控件列表
        public UIElement[] Elements;

        #region 获取元素相关
        /// <summary>
        /// 获取缓存的GameObject
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>获取到的GameObject</returns>
        public GameObject GetCacheGameObject(int index)
        {
            if (Elements != null && Elements.Length > 0)
            {
                if (Elements == null || index < 0 || index >= Elements.Length)
                {
                    return null;
                }
                return Elements[index].Component.gameObject;
            }
            return null;
        }

        /// <summary>
        /// 获取缓存的Transform
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>获取到的Transform</returns>
        public Transform GetCacheTransform(int index)
        {
            if (Elements != null && Elements.Length > 0)
            {
                if (Elements == null || index < 0 || index >= Elements.Length)
                {
                    return null;
                }
                return Elements[index].Component.transform;
            }
            return null;
        }

        /// <summary>
        /// 通过index取component
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Component GetCacheComponent(int index)
        {
            if (Elements == null
                || index < 0
                || index >= Elements.Length)
            {
                return null;
            }

            return Elements[index].Component;
        }

        /// <summary>
        /// 获取缓存的Component
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="typeName">Component的类型名</param>
        /// <returns>获取到的Component</returns>
        public Component GetCacheComponent(int index, string typeName)
        {
            if (Elements != null && Elements.Length > 0)
            {
                if (Elements == null || index < 0 || index >= Elements.Length)
                {
                    return null;
                }

                return Elements[index].Component.gameObject.GetComponent(typeName);
            }
            return null;
        }
        #endregion
    }
}
