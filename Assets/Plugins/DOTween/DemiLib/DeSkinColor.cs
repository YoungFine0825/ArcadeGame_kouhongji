﻿// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2015/04/24 18:40

using DG.DemiLib.Core;
using UnityEngine;

#pragma warning disable 1591
namespace DG.DemiLib
{
    /// <summary>
    /// Contains both free and pro skins color variations,
    /// and automatically returns the correct one when converted to Color
    /// </summary>
    [System.Serializable]
    public struct DeSkinColor
    {
        public Color free, pro;

        public DeSkinColor(Color free, Color pro)
        {
            this.free = free;
            this.pro = pro;
        }

        public DeSkinColor(float freeGradation, float proGradation)
        {
            this.free = new Color(freeGradation, freeGradation, freeGradation, 1);
            this.pro = new Color(proGradation, proGradation, proGradation, 1);
        }

        public DeSkinColor(Color color) : this()
        {
            free = color;
            pro = color;
        }

        public DeSkinColor(float gradation) : this()
        {
            free = new Color(gradation, gradation, gradation, 1);
            pro = free;
        }

        public static implicit operator Color(DeSkinColor v)
        {
            return GUIUtils.isProSkin ? v.pro : v.free;
        }

        public static implicit operator DeSkinColor(Color v)
        {
            return new DeSkinColor(v);
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", free, pro);
        }
    }
}