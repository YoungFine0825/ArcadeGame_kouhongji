using DG.Tweening.Plugins.Options;
using System;
using UnityEngine;

namespace DG.Tweening.Plugins
{
	public struct SpiralOptions : IPlugOptions
	{
		public float depth;

		public float frequency;

		public float speed;

		public SpiralMode mode;

		public bool snapping;

		public float unit;

		public Quaternion axisQ;

		public void Reset()
		{
			this.depth = (this.frequency = (this.speed = 0f));
			this.mode = SpiralMode.Expand;
			this.snapping = false;
		}
	}
}
