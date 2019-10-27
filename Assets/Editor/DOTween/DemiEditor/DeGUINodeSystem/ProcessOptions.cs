﻿// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2017/03/23 11:04
// License Copyright (c) Daniele Giardini

using UnityEngine;

namespace DG.DemiEditor.DeGUINodeSystem
{
    public class ProcessOptions
    {
        public enum EvidenceEndNodesMode
        {
            None,
            Icon,
            Invasive
        }
        public enum MinimapResolution
        {
            Normal,
            Small,
            Big
        }

        public bool allowDeletion = true;
        public bool allowCopyPaste = true;
        public bool drawBackgroundGrid = true;
        public Texture2D gridTextureOverride;
        public bool forceDarkSkin = false; // Ignored if gridTextureOverride != NULL
        public bool evidenceSelectedNodes = true;
        public bool evidenceSelectedNodesArea = true; // Draws an outline around the whole area of all selected nodes
        public Color evidenceSelectedNodesColor = new Color(0.13f, 0.48f, 0.91f);
        public EvidenceEndNodesMode evidenceEndNodes = EvidenceEndNodesMode.Icon; // Nodes that have no forward connections
        public int evidenceEndNodesBackgroundBorder = 26; // BG border for invasive evidence for nodes that have no forward connections
        public Color evidenceEndNodesBackgroundColor = new Color(1, 0, 0, 0.5f); // Color for invasive evidence for nodes that have no forward connections
        public float connectorsThickness = 3; // Thickness of connector lines
        public bool connectorsShadow = true; // If TRUE, draws a shadow around connector lines (ignores this and always draws it when dragging)
        public bool showMinimap = true;
        public int minimapMaxSize = 150;
        public MinimapResolution minimapResolution = MinimapResolution.Normal;
        public bool minimapEvidenceEndNodes = true;
        public bool minimapClickToGoto = true;
        public bool mouseWheelScalesGUI = true; // If TRUE implements GUI scaling via mouse wheel
        public float[] guiScaleValues = new[] { 1, 0.9f, 0.8f, 0.7f, 0.6f, 0.5f, 0.4f, 0.3f, 0.2f, 0.1f }; // Ordered from max to min, 1f included

        public bool debug_showFps = false;
    }
}