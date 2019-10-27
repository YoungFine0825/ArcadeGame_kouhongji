﻿// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2015/04/26 19:25

using UnityEngine;

#pragma warning disable 1591
namespace DG.DemiLib
{
    /// <summary>
    /// Stores a color palette, which can be passed to default DeGUI layouts when calling <code>DeGUI.BeginGUI</code>,
    /// and changed at any time by calling <code>DeGUI.ChangePalette</code>.
    /// You can inherit from this class to create custom color palettes with more hColor options.
    /// </summary>
    [System.Serializable]
    public class DeColorPalette
    {
        public DeColorGlobal global = new DeColorGlobal();
        public DeColorBG bg = new DeColorBG();
        public DeColorContent content = new DeColorContent();

        #region Public Methods

        /// <summary>
        /// Converts a HEX color to a Unity Color and returns it
        /// </summary>
        /// <param name="hex">The HEX color, either with or without the initial # (accepts both regular and short format)</param>
        public static Color HexToColor(string hex)
        {
            if (hex[0] == '#') hex = hex.Substring(1);
            int len = hex.Length;
            bool isShortFormat = len < 6;
            if (isShortFormat) {
                float r = (HexToInt(hex[0]) + HexToInt(hex[0]) * 16f) / 255f;
                float g = (HexToInt(hex[1]) + HexToInt(hex[1]) * 16f) / 255f;
                float b = (HexToInt(hex[2]) + HexToInt(hex[2]) * 16f) / 255f;
                float a = len == 4 ? (HexToInt(hex[3]) + HexToInt(hex[3]) * 16f) / 255f : 1;
                return new Color(r, g, b, a);
            } else {
                float r = (HexToInt(hex[1]) + HexToInt(hex[0]) * 16f) / 255f;
                float g = (HexToInt(hex[3]) + HexToInt(hex[2]) * 16f) / 255f;
                float b = (HexToInt(hex[5]) + HexToInt(hex[4]) * 16f) / 255f;
                float a = len == 8 ? (HexToInt(hex[7]) + HexToInt(hex[6]) * 16f) / 255f : 1;
                return new Color(r, g, b, a);
            }
        }

        #endregion

        #region Methods

        static int HexToInt(char hexVal)
        {
            return int.Parse(hexVal.ToString(), System.Globalization.NumberStyles.HexNumber);
        }

        #endregion
    }

    /// <summary>
    /// Global colors
    /// </summary>
    [System.Serializable]
    public class DeColorGlobal
    {
        public Color black = Color.black;
        public Color white = Color.white;
        public Color blue = new Color(0f, 0.4f, 0.91f);
        public Color green = new Color(0.11f, 0.84f, 0.02f);
        public Color orange = new Color(0.98f, 0.44f, 0f);
        public Color purple = new Color(0.67f, 0.17f, 0.87f);
        public Color red = new Color(0.93f, 0.04f, 0.04f);
        public Color yellow = new Color(0.93f, 0.77f, 0.04f);
    }

    /// <summary>
    /// Background colors
    /// </summary>
    [System.Serializable]
    public class DeColorBG
    {
        public DeSkinColor def = Color.white;
        public DeSkinColor critical = new DeSkinColor(new Color(0.9411765f, 0.2388736f, 0.006920422f, 1f), new Color(1f, 0.2482758f, 0f, 1f));
        public DeSkinColor divider = new DeSkinColor(0.6f, 0.3f);
        public DeSkinColor toggleOn = new DeSkinColor(new Color(0.3158468f, 0.875f, 0.1351103f, 1f), new Color(0.2183823f, 0.7279412f, 0.09099264f, 1f));
        public DeSkinColor toggleOff = new DeSkinColor(0.75f, 0.4f);
    }

    /// <summary>
    /// Content colors
    /// </summary>
    [System.Serializable]
    public class DeColorContent
    {
        public DeSkinColor def = new DeSkinColor(Color.black, new Color(0.7f, 0.7f, 0.7f, 1));
        public DeSkinColor critical = new DeSkinColor(new Color(1f, 0.9148073f, 0.5588235f, 1f), new Color(1f, 0.3881846f, 0.3014706f, 1f));
        public DeSkinColor toggleOn = new DeSkinColor(new Color(1f, 0.9686275f, 0.6980392f, 1f), new Color(0.8025267f, 1f, 0.4705882f, 1f));
        public DeSkinColor toggleOff = new DeSkinColor(new Color(0.4117647f, 0.3360727f, 0.3360727f, 1f), new Color(0.6470588f, 0.5185986f, 0.5185986f, 1f));
    }
}