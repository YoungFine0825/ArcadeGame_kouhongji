// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2014/09/04 10:47
// 
// License Copyright (c) Daniele Giardini.
// This work is subject to the terms at http://dotween.demigiant.com/license.php

using UnityEngine;

namespace DG.Tweening.Plugins.Core.PathCore
{
    public abstract class ABSPathDecoder
    {
        // Finalizes the path, assigning eventual control points and storing all required data
        public abstract void FinalizePath(Path p, Vector3[] wps, bool isClosedPath);

        // Gets a point on the path at the given percentage (0 to 1)
        public abstract Vector3 GetPoint(float perc, Vector3[] wps, Path p, ControlPoint[] controlPoints);

//        // If path is linear subdivisions is ignored
//        // and waypointsLength are stored here instead than when calling SetWaypointsLengths
//        public abstract void SetTimeToLengthTables(Path p, int subdivisions);
//
//        // If path is linear waypointsLengths were already stored when calling StoreTimeToLenTables
//        public abstract void SetWaypointsLengths(Path p, int subdivisions);
    }
}