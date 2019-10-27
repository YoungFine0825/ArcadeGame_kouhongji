using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using JW.Res;
using UnityEngine.UI;

namespace JW.Editor.Res
{
    public class PrefabResPacker : IResPacker
    {
        protected string mPath;
        protected List<KeyValuePair<string, GameObject>> mRefResPaths = new List<KeyValuePair<string, GameObject>>();
        protected List<string> mTmpAssetPaths = new List<string>();
        protected EPackHandlerParam _mPackHandlerParam;

        protected Dictionary<System.Type, IRefResPacker> mCustomResPackerDict = new Dictionary<System.Type, IRefResPacker>();

        protected List<string> unrefResList;

        public bool onlyCustomPacker = false;

        public enum EProcessType
        {
            Atlas = 1,
            Font = 1 << 1,

            All = Atlas | Font,
        }
        public EProcessType ProcessType = EProcessType.All;

        public void SetHandlerParam(EPackHandlerParam packHandlerParam)
        {
            _mPackHandlerParam = packHandlerParam;
        }

        public virtual void SetPath(string path)
        {
            this.mPath = path;
        }

        public string GetPath()
        {
            return mPath;
        }

        public virtual IEnumerable<string> GetRelatedFiles()
        {
            return new string[] { mPath, mPath + ".meta" };
        }

        public virtual string[] GetRelatedFileList()
        {
            if (null == unrefResList || 0 == unrefResList.Count)
            {
                return ResPackHelper.GetDependencies(mPath);
            }
            else
            {
                List<string> tmpList = new List<string>();
                string[] files = ResPackHelper.GetDependencies(mPath);
                string path;
                for (int i = 0; i < files.Length; i++)
                {
                    path = files[i].Remove(0, 7);
                    if (path.EndsWith(".meta"))
                    {
                        path = path.Substring(0, path.Length - 5);
                    }

                    if (!unrefResList.Contains(path))
                    {
                        tmpList.Add(files[i]);
                    }
                }

                return tmpList.ToArray();
            }
        }

        public virtual void Preprocess(List<string> excludeList = null)
        {
            GameObject prefab = (GameObject)AssetDatabase.LoadMainAssetAtPath(mPath);

            if (BreakReferences(prefab))
            {
                // save
                EditorUtility.SetDirty(prefab);
                AssetDatabase.SaveAssets();
            }

            Resources.UnloadUnusedAssets();
        }

        /// <summary>
        /// 断链操作
        /// </summary>
        /// <param name="prefab"></param>
        /// <returns></returns>
        bool BreakReferences(GameObject prefab)
        {
            return false;
            /*
            if (prefab == null)
            {
                return false;
            }

            bool changed = false;

            Transform[] children = prefab.GetComponentsInChildren<Transform>(true);
            foreach (Transform tf in children)
            {
                GameObject go = tf.gameObject;
                if (go == null) continue;

                WeakReference reference = null;

                Component[] components = go.GetComponents(typeof(Component));
                for (int i = 0; i < components.Length; i++)
                {
                    Component com = components[i];
                    if (com == null)
                    {
                        Debug.LogError(string.Format("Component missing, prefab:{0}, node:{1}", prefab.name, go.name));
                        continue;
                    }

                    if (com.GetType() == typeof(Transform)) continue;

                    string path = string.Empty;
                    if (com is Image && (ProcessType & EProcessType.Atlas) > 0)
                    {
                        path = ProcessUGUIImage(com as Image);
                    }
                    else if (com is Text && (ProcessType & EProcessType.Font) > 0)
                    {
                        path = ProcessUGUIText(com as Text);
                    }

                    if (!string.IsNullOrEmpty(path))
                    {

                        if (!path.Contains("Resources"))
                        {
                            throw new System.Exception(string.Format("Breaking asset reference failed for [{0}], reference asset is not in Resources folder.", prefab.name));
                        }
                        changed = true;

                        if (reference == null)
                        {
                            reference = go.AddComponent<WeakReference>();
                        }
                        reference.AddRecord(i, path);
                        //reference.AddRecord(i, FileManager.EraseExtension(path.Substring("Resources/".Length)));
                    }
                }
            }

            return changed;
            */
        }

        /// <summary>
        /// 图集断链
        /// </summary>
        /// <param name="sprite"></param>
        /// <returns></returns>
        string ProcessUGUIImage(Image img)
        {
            return null;
            /*
            if (img == null)
            {
                return string.Empty;
            }

            Sprite ss = img.sprite;
            if (ss == null)
            {
                return string.Empty;
            }

            string path = ResPackHelper.GetRefAssetPath(ss);
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            img.sprite = null;
            return path;
            */
        }

        /// <summary>
        /// 字体断链
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        string ProcessUGUIText(Text label)
        {
            return string.Empty;
            /*
            if (label == null || label.font == null)
            {
                return string.Empty;
            }

            Font font = label.font;
            string path = ResPackHelper.GetRefAssetPath(font);
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            label.font = null;
            return path;
            */
        }

        public virtual Object GetAimAssetObj()
        {
            return AssetDatabase.LoadMainAssetAtPath(mPath);
        }

        public virtual void Clear()
        {
        }

        /// <summary>
        /// 对各个组件获得对应的资源打包器。
        /// 添加打包器之后要在ResourceMgr中的GetRefAssetUnpacker中添加相应的解包器
        /// </summary>
        /// <param name="composType"></param>
        /// <returns></returns>
        protected virtual IRefResPacker GetRefResPacker(System.Type composType)
        {
            IRefResPacker refAssetPacker = null;

            if (mCustomResPackerDict.TryGetValue(composType, out refAssetPacker))
            {
                return refAssetPacker;
            }

            if (onlyCustomPacker) return null;

            return refAssetPacker;
        }

        public virtual List<System.Type> GetReplaceRefTypeList()
        {
            return new List<System.Type> { };
        }

        public virtual void AddCustomResPacker(System.Type type, IRefResPacker refResPacker)
        {
            mCustomResPackerDict.Add(type, refResPacker);
        }
    }
}
