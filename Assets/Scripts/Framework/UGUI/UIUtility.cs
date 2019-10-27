/********************************************************************
	created:	2018-8-17
	author:		jordenwu
	
	purpose:	UI工具类
*********************************************************************/
using UnityEngine;
using UnityEngine.UI;
namespace JW.Framework.UGUI
{
    public class UIUtility
    {
        //用于隐藏GameObject的Layer
        public const int c_hideLayer = 31;

        //UI层
        public const int c_uiLayer = 5;

        //Default层
        public const int c_defaultLayer = 0;

        //UI_BottomBg层
        public const int c_UIBottomBg = 18;

        /// 遍历获取UI组件
        /// @go在非active状态下依然有效
        public static T GetComponentInChildren<T>(GameObject go) where T : Component
        {
            if (go == null)
            {
                return null;
            }

            T t = go.GetComponent<T>();

            if (t != null)
            {
                return t;
            }

            var trans = go.transform;
            var Count = trans.childCount;

            for (int i = 0; i < Count; i++)
            {
                t = GetComponentInChildren<T>(trans.GetChild(i).gameObject);

                if (t != null)
                {
                    return t;
                }
            }

            return null;
        }


        /// 遍历获取UI组件数组
        /// @go在非active状态下依然有效
        public static void GetComponentsInChildren<T>(GameObject go, T[] components, ref int count) where T : Component
        {
            T t = go.GetComponent<T>();

            if (t != null)
            {
                components[count] = t;
                count++;
            }

            for (int i = 0; i < go.transform.childCount; i++)
            {
                GetComponentsInChildren<T>(go.transform.GetChild(i).gameObject, components, ref count);
            }
        }

        /// 屏幕坐标转换为世界坐标
        /// @camera
        /// @screenPoint
        /// @z
        public static Vector3 ScreenToWorldPoint(Camera camera, Vector2 screenPoint, float z)
        {
            return ((camera == null) ? new Vector3(screenPoint.x, screenPoint.y, z) : camera.ViewportToWorldPoint(new Vector3(screenPoint.x / Screen.width, screenPoint.y / Screen.height, z)));
        }

        /// 世界坐标转换为屏幕坐标
        /// @camera
        /// @worldPoint
        public static Vector2 WorldToScreenPoint(Camera camera, Vector3 worldPoint)
        {
            return ((camera == null) ? new Vector2(worldPoint.x, worldPoint.y) : (Vector2)camera.WorldToScreenPoint(worldPoint));
        }

        /// 设置GameObject Layer
        /// @gameObject
        /// @layer
        public static void SetGameObjectLayer(GameObject gameObject, int layer)
        {
            gameObject.layer = layer;

            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                SetGameObjectLayer(gameObject.transform.GetChild(i).gameObject, layer);
            }
        }

        /// 范围内取值
        /// @value
        /// @min
        /// @max
        public static float ValueInRange(float value, float min, float max)
        {
            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }
            else
            {
                return value;
            }
        }
    };
};
