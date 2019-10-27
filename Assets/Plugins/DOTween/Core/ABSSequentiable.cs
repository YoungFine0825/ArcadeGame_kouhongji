// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2014/07/16 11:43
// 
// License Copyright (c) Daniele Giardini.
// This work is subject to the terms at http://dotween.demigiant.com/license.php

#pragma warning disable 1591
namespace DG.Tweening.Core
{
    public abstract class ABSSequentiable
    {
        public TweenType tweenType;
        public float sequencedPosition; // position in Sequence
        public float sequencedEndPosition; // end position in Sequence

        /// <summary>Called the first time the tween is set in a playing state, after any eventual delay</summary>
        public TweenCallback onStart; // Used also by SequenceCallback as main callback
    }
}