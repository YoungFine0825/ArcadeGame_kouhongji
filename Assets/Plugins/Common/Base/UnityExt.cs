/********************************************************************
	created:	17:5:2018   
	filename: 	UnityExt
	author:		jordenwu
	
	purpose:	Unity组件类 添加Null保护 减少crash 率 同时扩展部分方法参数方便lua调用
*********************************************************************/
using UnityEngine;
using System;
using System.Collections;
using Object = UnityEngine.Object;

namespace JW.Common
{
    /// <summary>
    /// Behaviour类
    /// </summary>
    public static class ExtBehaviour
    {
        public static void ExtSetEnabled(this Behaviour behaviour, bool value)
        {
            if (behaviour == null)
            {
                return;
            }

            behaviour.enabled = value;
        }
    }

    /// <summary>
    /// Component类
    /// </summary>
    public static class ExtComponent
    {
        public static T ExtGetComponent<T>(this Component cpt) where T : Component
        {
            return cpt == null ? null : cpt.GetComponent<T>();
        }

        public static T ExtGetComponentInChildren<T>(this Component cpt) where T : Component
        {
            return cpt == null ? null : cpt.GetComponentInChildren<T>();
        }

        public static T ExtGetComponentInParent<T>(this Component cpt) where T : Component
        {
            return cpt == null ? null : cpt.GetComponentInParent<T>();
        }

        public static Component ExtGetComponentInParent(this Component cpt, string typeName)
        {
            if (cpt == null || string.IsNullOrEmpty(typeName))
            {
                return null;
            }

            Type t = Type.GetType(typeName);
            return t == null ? null : cpt.GetComponentInParent(t);
        }

        public static T[] ExtGetComponents<T>(this Component cpt) where T : Component
        {
            return cpt == null ? null : cpt.GetComponents<T>();
        }

        public static T[] ExtGetComponentsInChildren<T>(this Component cpt, bool includeInactive = false) where T : Component
        {
            return cpt == null ? null : cpt.GetComponentsInChildren<T>(includeInactive);
        }

        public static Component[] ExtGetComponentsInChildren(this Component cpt, Type t)
        {
            return cpt == null ? null : cpt.GetComponentsInChildren(t);
        }

        public static Component[] ExtGetComponentsInChildren(this Component cpt, string typeName)
        {
            if (cpt == null || string.IsNullOrEmpty(typeName))
            {
                return null;
            }

            Type t = Type.GetType(typeName);
            return t == null ? null : cpt.GetComponentsInChildren(t);
        }

        public static T[] ExtGetComponentsInParent<T>(this Component cpt, bool includeInactive = false) where T : Component
        {
            return cpt == null ? null : cpt.GetComponentsInParent<T>(includeInactive);
        }
    }

    /// <summary>
    /// GameObject
    /// </summary>
    public static class ExtGameObject
    {
        public static void ExtSetActive(this GameObject go, bool value)
        {
            if (go == null)
            {
                return;
            }

            if (value == go.activeSelf)
            {
                return;
            }

            go.SetActive(value);
        }

        public static void ExtSetLayer(this GameObject go, int layer)
        {
            if (go == null)
            {
                return;
            }

            go.layer = layer;
        }

        public static void ExtSetLayerRecursively(this GameObject go, int layer)
        {
            if (go == null)
            {
                return;
            }

            go.ExtSetLayer(layer);

            Transform tf = go.transform;
            for (int i = 0, imax = tf.childCount; i < imax; ++i)
            {
                Transform child = tf.GetChild(i);
                ExtSetLayerRecursively(child.gameObject, layer);
            }
        }

        public static T ExtAddComponent<T>(this GameObject go, bool addIfNotExist = false) where T : Component
        {
            if (go == null)
            {
                return null;
            }

            if (addIfNotExist)
            {
                T cpt = go.ExtGetComponent<T>();
                if (cpt != null)
                {
                    return cpt;
                }
            }

            return go.AddComponent<T>();
        }

        public static T ExtGetComponent<T>(this GameObject go) where T : Component
        {
            return go == null ? null : go.GetComponent<T>();
        }

        public static T ExtGetComponentInChildren<T>(this GameObject go) where T : Component
        {
            return go == null ? null : go.GetComponentInChildren<T>();
        }

        public static T ExtGetComponentInParent<T>(this GameObject go) where T : Component
        {
            return go == null ? null : go.GetComponentInParent<T>();
        }

        public static T[] ExtGetComponents<T>(this GameObject go) where T : Component
        {
            return go == null ? null : go.GetComponents<T>();
        }

        public static T[] ExtGetComponentsInChildren<T>(this GameObject go, bool includeInactive = false) where T : Component
        {
            return go == null ? null : go.GetComponentsInChildren<T>(includeInactive);
        }

        public static T[] ExtGetComponentsInParent<T>(this GameObject go, bool includeInactive = false) where T : Component
        {
            return go == null ? null : go.GetComponentsInParent<T>(includeInactive);
        }

        public static GameObject ExtAddChild(this GameObject parent)
        {
            if (parent == null)
            {
                return null;
            }

            GameObject go = new GameObject();
            go.transform.ExtSetParent(parent.transform);
            go.ExtSetLayer(parent.layer);
            return go;
        }

        public static GameObject ExtAddChild(this GameObject parent, string name)
        {
            if (parent == null)
            {
                return null;
            }

            GameObject go = new GameObject(name);
            go.transform.ExtSetParent(parent.transform);
            go.ExtSetLayer(parent.layer);
            return go;
        }

        public static GameObject ExtAddChild(this GameObject parent, Object prefab)
        {
            if (parent == null || prefab == null)
            {
                return null;
            }

            GameObject go = (GameObject)prefab.ExtInstantiate();
            if (go == null)
            {
                return null;
            }

            go.transform.ExtSetParent(parent.transform);
            go.ExtSetLayer(parent.layer);
            return go;
        }

        public static void ExtSetUIActive(this GameObject go, bool active)
        {
            if (go == null || go.ExtGetUIActive() == active)
            {
                return;
            }

            int delta = active ? -10000 : 10000;
            go.transform.ExtSetLocalPositionY(go.transform.localPosition.y + delta);
            go.ExtSetActive(true);
        }

        public static bool ExtGetUIActive(this GameObject go)
        {
            if (go == null)
            {
                return false;
            }

            return go.transform.localPosition.y <= 5000;
        }
    }

    /// <summary>
    /// MonoBehaviour
    /// </summary>
    public static class ExtMonoBehaviour
    {
        public static Coroutine ExtStartCoroutine(this MonoBehaviour monoBehaviour, IEnumerator routine)
        {
            return monoBehaviour.StartCoroutine(routine);
        }

        public static Coroutine ExtStartCoroutine(this MonoBehaviour monoBehaviour, string methodName)
        {
            return monoBehaviour.StartCoroutine(methodName);
        }

        public static void ExtStopAllCoroutines(this MonoBehaviour monoBehaviour)
        {
            monoBehaviour.StopAllCoroutines();
        }

        public static void ExtStopCoroutine(this MonoBehaviour monoBehaviour, Coroutine routine)
        {
            monoBehaviour.StopCoroutine(routine);
        }

        public static void ExtStopCoroutine(this MonoBehaviour monoBehaviour, IEnumerator routine)
        {
            monoBehaviour.StopCoroutine(routine);
        }

        public static void ExtStopCoroutine(this MonoBehaviour monoBehaviour, string methodName)
        {
            monoBehaviour.StopCoroutine(methodName);
        }
    }

    /// <summary>
    /// Unity Object类
    /// </summary>
    public static class ExtObject
    {
        public static bool IsNull(this UnityEngine.Object o)
        {
            return o == null;
        }

        public static void ExtDestroy(this Object obj)
        {
            if (obj == null)
            {
                return;
            }
            if (Application.isPlaying)
            {
                Object.Destroy(obj);
            }
            else
            {
                Object.DestroyImmediate(obj);
            }
        }

        public static void ExtDestroy(this Object obj, float delayTime)
        {
            if (obj == null)
            {
                return;
            }

            Object.Destroy(obj, delayTime);
        }

        public static void ExtDestroyImmediate(this Object obj)
        {
            if (obj == null)
            {
                return;
            }

            Object.DestroyImmediate(obj);
        }

        public static Object ExtInstantiate(this Object original)
        {
            return original == null ? null : Object.Instantiate(original);
        }

        public static Object ExtInstantiate(this Object original, Vector3 position, Quaternion rotation)
        {
            return original == null ? null : Object.Instantiate(original, position, rotation);
        }

        public static void ExtDontDestroyOnLoad(this Object obj)
        {
            if (Application.isPlaying)
            {
                if (null != obj)
                {
                    Object.DontDestroyOnLoad(obj);
                }
            }
        }
    }

    /// <summary>
    /// Resource
    /// </summary>
    public static class ExtResources
    {
        public static Object Load(string path)
        {
            return Resources.Load(path);
        }

        public static ResourceRequest LoadAsync(string path)
        {
            return Resources.LoadAsync(path);
        }

        public static T Load<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }

        public static void UnloadAsset(Object assetToUnload)
        {
            Resources.UnloadAsset(assetToUnload);
        }

        public static AsyncOperation UnloadUnusedAssets()
        {
            return Resources.UnloadUnusedAssets();
        }
    }

    /// <summary>
    /// Time 扩展
    /// </summary>
    public static class ExtTime
    {
        public static void SetTimeScale(float timeScale)
        {
            Time.timeScale = timeScale;
        }

        public static void MultiTimeScale(float value)
        {
            float ts = Time.timeScale * value;
            SetTimeScale(ts);
        }

        public static void DivisionTimeScale(float value)
        {
            float ts = Time.timeScale / value;
            SetTimeScale(ts);
        }
    }

    /// <summary>
    /// Transform类
    /// </summary>
    public static class ExtTransform
    {
        public static void ExtSetForward(this Transform tf, Vector3 value)
        {
            if (tf == null)
            {
                return;
            }

            tf.forward = value;
        }

        public static void ExtSetRight(this Transform tf, Vector3 value)
        {
            if (tf == null)
            {
                return;
            }

            tf.right = value;
        }

        public static void ExtSetUp(this Transform tf, Vector3 value)
        {
            if (tf == null)
            {
                return;
            }

            tf.up = value;
        }

        public static void ExtSetParent(this Transform tf, Transform parent)
        {
            if (tf == null)
            {
                return;
            }

            tf.SetParent(parent, false);
        }

        public static void ExtSetParent(this Transform tf, Transform parent, bool worldPosStays)
        {
            if (tf == null)
            {
                return;
            }

            tf.SetParent(parent, worldPosStays);
        }

        public static void ExtSetEulerAngles(this Transform tf, Vector3 value)
        {
            if (tf == null)
            {
                return;
            }

            tf.eulerAngles = value;
        }

        public static void ExtSetEulerAnglesX(this Transform tf, float x)
        {
            if (tf == null)
            {
                return;
            }

            Vector3 v = tf.eulerAngles;
            v.x = x;
            tf.ExtSetEulerAngles(v);
        }

        public static void ExtSetEulerAnglesY(this Transform tf, float y)
        {
            if (tf == null)
            {
                return;
            }

            Vector3 v = tf.eulerAngles;
            v.y = y;
            tf.ExtSetEulerAngles(v);
        }

        public static void ExtSetEulerAnglesZ(this Transform tf, float z)
        {
            if (tf == null)
            {
                return;
            }

            Vector3 v = tf.eulerAngles;
            v.z = z;
            tf.ExtSetEulerAngles(v);
        }

        public static void ExtSetPosition(this Transform tf, Vector3 value)
        {
            if (tf == null)
            {
                return;
            }

            tf.position = value;
        }


        public static void ExtSetPositionX(this Transform tf, float x)
        {
            if (tf == null)
            {
                return;
            }

            Vector3 v = tf.position;
            v.x = x;
            tf.ExtSetPosition(v);
        }

        public static void ExtSetPositionY(this Transform tf, float y)
        {
            if (tf == null)
            {
                return;
            }

            Vector3 v = tf.position;
            v.y = y;
            tf.ExtSetPosition(v);
        }

        public static void ExtSetPositionZ(this Transform tf, float z)
        {
            if (tf == null)
            {
                return;
            }

            Vector3 v = tf.position;
            v.z = z;
            tf.ExtSetPosition(v);
        }

        public static void ExtSetRotation(this Transform tf, Quaternion value)
        {
            if (tf == null)
            {
                return;
            }

            tf.rotation = value;
        }

        public static void ExtSetLocalEulerAngles(this Transform tf, Vector3 value)
        {
            if (tf == null)
            {
                return;
            }

            tf.localEulerAngles = value;
        }

        public static void ExtSetLocalEulerAnglesX(this Transform tf, float x)
        {
            if (tf == null)
            {
                return;
            }

            Vector3 v = tf.localEulerAngles;
            v.x = x;
            tf.ExtSetLocalEulerAngles(v);
        }

        public static void ExtSetLocalEulerAnglesY(this Transform tf, float y)
        {
            if (tf == null)
            {
                return;
            }

            Vector3 v = tf.localEulerAngles;
            v.y = y;
            tf.ExtSetLocalEulerAngles(v);
        }

        public static void ExtSetLocalEulerAnglesOffY(this Transform tf, float offY)
        {
            if (tf == null)
            {
                return;
            }

            Vector3 v = tf.localEulerAngles;
            v.y = offY + v.y;

            tf.ExtSetLocalEulerAngles(v);
        }

        public static void ExtSetLocalEulerAnglesZ(this Transform tf, float z)
        {
            if (tf == null)
            {
                return;
            }

            Vector3 v = tf.localEulerAngles;
            v.z = z;
            tf.ExtSetLocalEulerAngles(v);
        }

        public static void ExtSetLocalEulerAnglesXYZ(this Transform tf, float x, float y, float z)
        {
            if (tf == null)
            {
                return;
            }

            Vector3 v = tf.localEulerAngles;
            v.x = x;
            v.y = y;
            v.z = z;
            tf.ExtSetLocalEulerAngles(v);
        }


        public static void ExtSetLocalPosition(this Transform tf, Vector3 value)
        {
            if (tf == null)
            {
                return;
            }

            tf.localPosition = value;
        }

        public static void ExtSetLocalPositionXYZ(this Transform tf, float x, float y, float z)
        {
            if (tf == null)
            {
                return;
            }

            Vector3 v = tf.localPosition;
            v.x = x;
            v.y = y;
            v.z = z;
            tf.localPosition = v;
        }

        public static void ExtSetLocalPositionX(this Transform tf, float x)
        {
            if (tf == null)
            {
                return;
            }

            Vector3 v = tf.localPosition;
            v.x = x;
            tf.ExtSetLocalPosition(v);
        }

        public static void ExtSetLocalPositionOffX(this Transform tf, float offset, float min, float max)
        {
            if (tf == null)
            {
                return;
            }

            Vector3 v = tf.localPosition;
            v.x = v.x + offset;

            tf.ExtSetLocalPosition(v);
        }

        public static void ExtSetLocalPositionOffXClamp(this Transform tf, float offset,float min,float max)
        {
            if (tf == null)
            {
                return;
            }

            Vector3 v = tf.localPosition;
            v.x =Mathf.Clamp(v.x+offset,min,max);
            tf.ExtSetLocalPosition(v);
        }

        public static void ExtSetLocalPositionY(this Transform tf, float y)
        {
            if (tf == null)
            {
                return;
            }

            Vector3 v = tf.localPosition;
            v.y = y;
            tf.ExtSetLocalPosition(v);
        }

        public static void ExtSetLocalPositionZ(this Transform tf, float z)
        {
            if (tf == null)
            {
                return;
            }

            Vector3 v = tf.localPosition;
            v.z = z;
            tf.ExtSetLocalPosition(v);
        }

        public static void ExtSetLocalPositionOffZ(this Transform tf, float offset)
        {
            if (tf == null)
            {
                return;
            }

            Vector3 v = tf.localPosition;
            v.z = v.z + offset;
            tf.ExtSetLocalPosition(v);
        }

        public static void ExtSetLocalPositionOffZClamp(this Transform tf, float offset,float min,float max)
        {
            if (tf == null)
            {
                return;
            }

            Vector3 v = tf.localPosition;
            v.z =Mathf.Clamp(v.z + offset,min,max);
            tf.ExtSetLocalPosition(v);
        }


        public static void ExtSetLocalPositionXZ(this Transform tf, float x,float z)
        {
            if (tf == null)
            {
                return;
            }

            Vector3 v = tf.localPosition;
            v.z = z;
            v.x = x;
            tf.ExtSetLocalPosition(v);
        }


        public static void ExtSetLocalRotation(this Transform tf, Quaternion value)
        {
            if (tf == null)
            {
                return;
            }

            tf.localRotation = value;
        }

        public static void ExtSetLocalScale(this Transform tf, Vector3 value)
        {
            if (tf == null)
            {
                return;
            }

            tf.localScale = value;
        }

        public static void ExtSetLocalScaleXYZ(this Transform tf, float x, float y, float z)
        {
            if (tf == null)
            {
                return;
            }

            tf.localScale = new Vector3(x, y, z);
        }


        public static void ExtSetLocalScaleX(this Transform tf, float x)
        {
            if (tf == null)
            {
                return;
            }

            Vector3 v = tf.localScale;
            v.x = x;
            tf.ExtSetLocalScale(v);
        }

        public static void ExtSetLocalScaleY(this Transform tf, float y)
        {
            if (tf == null)
            {
                return;
            }

            Vector3 v = tf.localScale;
            v.y = y;
            tf.ExtSetLocalScale(v);
        }

        public static void ExtSetLocalScaleZ(this Transform tf, float z)
        {
            if (tf == null)
            {
                return;
            }

            Vector3 v = tf.localScale;
            v.z = z;
            tf.ExtSetLocalScale(v);
        }

        public static void ExtLookAt(this Transform tf, Vector3 worldPosition)
        {
            if (null == tf)
            {
                return;
            }

            Vector3 diff = worldPosition - tf.position;
            Quaternion quaternion = Quaternion.FromToRotation(Vector3.forward, diff);
            tf.ExtSetRotation(quaternion);
        }

        public static void ExtLookAt(this Transform tf, Transform target)
        {
            if (tf == null || target == null)
            {
                return;
            }

            tf.ExtLookAt(target.position);
        }

        public static Transform ExtFindChild(this Transform tf, string name)
        {
            if (null == tf)
            {
                return null;
            }

            return tf.Find(name);
        }

        public static void ExtSetActive(this Transform tf, bool active)
        {
            if (null == tf)
            {
                return;
            }
            GameObject go = tf.gameObject;
            go.ExtSetActive(active);
        }

        private static GameObject sNullRoot = null;
        private static Transform sNullTf = null;

        public static void ExtSetNullParent(this Transform tf)
        {
            if (null == tf)
            {
                return;
            }
            if (sNullRoot == null)
            {
                sNullRoot = new GameObject("NullParent");
                sNullTf = sNullRoot.transform;
            }
            //
            tf.parent = sNullTf;
            tf.parent = null;
        }



        public static Vector3 SetY(this Vector3 vec3, float y)
        {
            vec3 = new Vector3(vec3.x, y, vec3.z);
            return vec3;
        }
    }

    public static class ExtRectTransform
    {
        public static void ExtSetActive(this RectTransform tf, bool active)
        {
            if (null == tf)
            {
                return;
            }
            GameObject go = tf.gameObject;
            go.ExtSetActive(active);
        }

        public static void ExtSetLocalScaleXYZ(this RectTransform tf, float x, float y, float z)
        {
            if (tf == null)
            {
                return;
            }

            tf.localScale = new Vector3(x, y, z);
        }


        public static void ExtSetLocalScaleX(this RectTransform tf, float x)
        {
            if (tf == null)
            {
                return;
            }

            Vector3 v = tf.localScale;
            v.x = x;
            tf.ExtSetLocalScale(v);
        }

        public static void ExtSetLocalScaleY(this RectTransform tf, float y)
        {
            if (tf == null)
            {
                return;
            }

            Vector3 v = tf.localScale;
            v.y = y;
            tf.ExtSetLocalScale(v);
        }

        public static void ExtSetLocalScaleZ(this RectTransform tf, float z)
        {
            if (tf == null)
            {
                return;
            }

            Vector3 v = tf.localScale;
            v.z = z;
            tf.ExtSetLocalScale(v);
        }

        public static void ExtSetLocalScale(this RectTransform tf, Vector3 value)
        {
            if (tf == null)
            {
                return;
            }

            tf.localScale = value;
        }
    }


    public static class ExtFloat
    {
        public static bool IsNearlyZero(this float v)
        {
            return Mathf.Abs(v) <= float.Epsilon;
        }
    }

    public static class ExtVector2
    {
        public static bool IsNearlyZero(this Vector2 v)
        {
            return Mathf.Abs(v.x) <= float.Epsilon && Mathf.Abs(v.y) <= float.Epsilon;
        }
    }

    public static class ExtVector3
    {
        public static bool IsNearlyZero(this Vector3 v)
        {
            return Mathf.Abs(v.x) <= float.Epsilon && Mathf.Abs(v.y) <= float.Epsilon && Mathf.Abs(v.z) <= float.Epsilon;
        }
        public static bool IsNearlyEqual(this Vector3 vec3, Vector3 other)
        {
            return Mathf.Abs(vec3.x - other.x) < float.Epsilon &&
                    Mathf.Abs(vec3.y - other.y) < float.Epsilon &&
                    Mathf.Abs(vec3.z - other.z) < float.Epsilon;
        }
    }

    public static class ExtVector4
    {
        public static bool IsNearlyZero(this Vector4 v)
        {
            return Mathf.Abs(v.x) <= float.Epsilon && Mathf.Abs(v.y) <= float.Epsilon && Mathf.Abs(v.z) <= float.Epsilon && Mathf.Abs(v.w) <= float.Epsilon;
        }
    }

    public static class ExtQuaternion
    {
        public static bool IsNearlyEqual(this Quaternion quat, Quaternion other)
        {
            return Mathf.Abs(quat.x - other.x) < float.Epsilon &&
                    Mathf.Abs(quat.y - other.y) < float.Epsilon &&
                    Mathf.Abs(quat.z - other.z) < float.Epsilon &&
                    Mathf.Abs(quat.w - other.w) < float.Epsilon;
        }
    }

    public static class ExtConfigurableJoint
    {
        public static void SetTargetRotationEuler(this ConfigurableJoint joint, float x,float y,float z)
        {
            joint.targetRotation = Quaternion.Euler(x, y, z);
        }

        public static void SetTargetPosition(this ConfigurableJoint joint, float x, float y, float z)
        {
            joint.targetPosition =new Vector3(x, y, z);
        }

    }
}