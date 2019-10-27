using DG.Tweening.Core;
using DG.Tweening.Plugins;
using System;
using UnityEngine;

namespace DG.Tweening
{
	public static class ShortcutExtensionsPro
	{
		static ShortcutExtensionsPro()
		{
			new SpiralPlugin();
		}

		public static Tweener DOSpiral(this Transform target, float duration, Vector3? axis = null, SpiralMode mode = SpiralMode.Expand, float speed = 1f, float frequency = 10f, float depth = 0f, bool snapping = false)
		{
			if (Mathf.Approximately(speed, 0f))
			{
				speed = 1f;
			}
			if (axis.HasValue)
			{
				Vector3? vector = axis;
				Vector3 zero = Vector3.zero;
				if (!vector.HasValue || (vector.HasValue && !(vector.GetValueOrDefault() == zero)))
				{
					goto IL_66;
				}
			}
			axis = new Vector3?(Vector3.forward);
			IL_66:
			TweenerCore<Vector3, Vector3, SpiralOptions> expr_9B = DOTween.To<Vector3, Vector3, SpiralOptions>(SpiralPlugin.Get(), () => target.localPosition, delegate(Vector3 x)
			{
				target.localPosition = x;
			}, axis.Value, duration).SetTarget(target);
			expr_9B.plugOptions.mode = mode;
			expr_9B.plugOptions.speed = speed;
			expr_9B.plugOptions.frequency = frequency;
			expr_9B.plugOptions.depth = depth;
			expr_9B.plugOptions.snapping = snapping;
			return expr_9B;
		}

		public static Tweener DOSpiral(this Rigidbody target, float duration, Vector3? axis = null, SpiralMode mode = SpiralMode.Expand, float speed = 1f, float frequency = 10f, float depth = 0f, bool snapping = false)
		{
			if (Mathf.Approximately(speed, 0f))
			{
				speed = 1f;
			}
			if (axis.HasValue)
			{
				Vector3? vector = axis;
				Vector3 zero = Vector3.zero;
				if (!vector.HasValue || (vector.HasValue && !(vector.GetValueOrDefault() == zero)))
				{
					goto IL_66;
				}
			}
			axis = new Vector3?(Vector3.forward);
			IL_66:
			TweenerCore<Vector3, Vector3, SpiralOptions> expr_A0 = DOTween.To<Vector3, Vector3, SpiralOptions>(SpiralPlugin.Get(), () => target.position, new DOSetter<Vector3>(target.MovePosition), axis.Value, duration).SetTarget(target);
			expr_A0.plugOptions.mode = mode;
			expr_A0.plugOptions.speed = speed;
			expr_A0.plugOptions.frequency = frequency;
			expr_A0.plugOptions.depth = depth;
			expr_A0.plugOptions.snapping = snapping;
			return expr_A0;
		}
	}
}
