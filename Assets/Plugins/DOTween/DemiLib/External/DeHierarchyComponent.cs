﻿// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2017/02/04 13:50
// License Copyright (c) Daniele Giardini

using System;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 1591
namespace DG.DemiLib.External
{
    /// <summary>
    /// Used by DeHierarchy
    /// </summary>
    public class DeHierarchyComponent : MonoBehaviour
    {
        public enum HColor
        {
            None,
            Blue,
            Green,
            Orange,
            Purple,
            Red,
            Yellow,
            BrightGrey,
            DarkGrey,
            Black,
            White
        }

        public enum IcoType
        {
            Dot,
            Star,
            Cog,
            Comment,
            UI,
            Play,
            Heart,
            Skull,
            Camera,
        }

        #region Serialized

        public List<CustomizedItem> customizedItems = new List<CustomizedItem>();
        
        #endregion

        /// <summary>
        /// Returns a list of all items whose gameObject is NULL, or NULL if there's no missing gameObjects.
        /// </summary>
        public List<int> MissingItemsIndexes()
        {
            List<int> result = null;
            for (int i = customizedItems.Count - 1; i > -1; --i) {
                if (customizedItems[i].gameObject == null) {
                    if (result == null) result = new List<int>();
                    result.Add(i);
                }
            }
            return result;
        }

        /// <summary>
        /// If the item exists sets it, otherwise first creates it and then sets it
        /// </summary>
        public void StoreItemColor(GameObject go, HColor hColor)
        {
            foreach (CustomizedItem customizedItem in customizedItems) {
                if (customizedItem.gameObject != go) continue;
                // Item exists, replace it
                customizedItem.hColor = hColor;
                return;
            }
            // Item doesn't exist, add it
            customizedItems.Add(new CustomizedItem(go, hColor));
        }

        /// <summary>
        /// If the item exists sets it, otherwise first creates it and then sets it
        /// </summary>
        public void StoreItemIcon(GameObject go, IcoType icoType)
        {
            foreach (CustomizedItem customizedItem in customizedItems) {
                if (customizedItem.gameObject != go) continue;
                // Item exists, replace it
                customizedItem.icoType = icoType;
                return;
            }
            // Item doesn't exist, add it
            customizedItems.Add(new CustomizedItem(go, icoType));
        }

        /// <summary>
        /// Returns TRUE if the item existed and was removed.
        /// </summary>
        public bool RemoveItemData(GameObject go)
        {
            int index = -1;
            for (int i = 0; i < customizedItems.Count; ++i) {
                if (customizedItems[i].gameObject == go) {
                    index = i;
                    break;
                }
            }
            if (index != -1) {
                customizedItems.RemoveAt(index);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the customizedItem for the given gameObject, or NULL if none was found
        /// </summary>
        public CustomizedItem GetItem(GameObject go)
        {
            foreach (CustomizedItem customizedItem in customizedItems) {
                if (customizedItem.gameObject == go) return customizedItem;
            }
            return null;
        }

        // █████████████████████████████████████████████████████████████████████████████████████████████████████████████████████
        // ███ INTERNAL CLASSES ████████████████████████████████████████████████████████████████████████████████████████████████
        // █████████████████████████████████████████████████████████████████████████████████████████████████████████████████████

        [Serializable]
        public class CustomizedItem
        {
            public GameObject gameObject;
            public HColor hColor = HColor.BrightGrey;
            public IcoType icoType = IcoType.Dot;

            public CustomizedItem(GameObject gameObject, HColor hColor)
            {
                this.gameObject = gameObject;
                this.hColor = hColor;
            }
            public CustomizedItem(GameObject gameObject, IcoType icoType)
            {
                this.gameObject = gameObject;
                this.icoType = icoType;
            }

            public Color GetColor()
            {
                switch (hColor) {
                case HColor.Blue: return new Color(0.2145329f, 0.4501492f, 0.9117647f, 1f);
                case HColor.Green: return new Color(0.05060553f, 0.8602941f, 0.2237113f, 1f);
                case HColor.Orange: return new Color(0.9558824f, 0.4471125f, 0.05622837f, 1f);
                case HColor.Purple: return new Color(0.907186f, 0.05406574f, 0.9191176f, 1f);
                case HColor.Red: return new Color(0.9191176f, 0.1617312f, 0.07434041f, 1f);
                case HColor.Yellow: return new Color(1f, 0.853854f, 0.03676468f, 1f);
                case HColor.BrightGrey: return new Color(0.6470588f, 0.6470588f, 0.6470588f, 1f);
                case HColor.DarkGrey: return new Color(0.3308824f, 0.3308824f, 0.3308824f, 1f);
                case HColor.Black: return Color.black;
                case HColor.White: return Color.white;
                default: return Color.white;
                }
            }
        }
    }
}