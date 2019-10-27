using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DG.Tweening
{
	[AddComponentMenu("DOTween/DOTween Path")]
	public class DOTweenPath : ABSAnimationComponent
	{
		public float delay;

		public float duration = 1f;

		public Ease easeType = Ease.OutQuad;

		public AnimationCurve easeCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		public int loops = 1;

		public string id = "";

		public LoopType loopType;

		public OrientType orientType;

		public Transform lookAtTransform;

		public Vector3 lookAtPosition;

		public float lookAhead = 0.01f;

		public bool autoPlay = true;

		public bool autoKill = true;

		public bool relative;

		public bool isLocal;

		public bool isClosedPath;

		public int pathResolution = 10;

		public PathMode pathMode = PathMode.Full3D;

		public AxisConstraint lockRotation;

		public bool assignForwardAndUp;

		public Vector3 forwardDirection = Vector3.forward;

		public Vector3 upDirection = Vector3.up;

		public bool tweenRigidbody;

		public List<Vector3> wps = new List<Vector3>();

		public List<Vector3> fullWps = new List<Vector3>();

		public Path path;

		public DOTweenInspectorMode inspectorMode;

		public PathType pathType;

		public HandlesType handlesType;

		public bool livePreview = true;

		public HandlesDrawMode handlesDrawMode;

		public float perspectiveHandleSize = 0.5f;

		public bool showIndexes = true;

		public bool showWpLength;

		public Color pathColor = new Color(1f, 1f, 1f, 0.5f);

		public Vector3 lastSrcPosition;

		public bool wpsDropdown;

		public float dropToFloorOffset;

		private void Awake()
		{
			if (this.path == null || this.wps.Count < 1 || this.inspectorMode == DOTweenInspectorMode.OnlyPath)
			{
				return;
			}
			this.path.AssignDecoder(this.path.type);
			if (DOTween.isUnityEditor)
			{
				DOTween.GizmosDelegates.Add(new TweenCallback(this.path.Draw));
				this.path.gizmoColor = this.pathColor;
			}
			if (this.isLocal)
			{
				Transform transform = base.transform;
				if (transform.parent != null)
				{
					transform = transform.parent;
					Vector3 position = transform.position;
					int num = this.path.wps.Length;
					for (int i = 0; i < num; i++)
					{
						this.path.wps[i] = this.path.wps[i] - position;
					}
					num = this.path.controlPoints.Length;
					for (int j = 0; j < num; j++)
					{
						ControlPoint controlPoint = this.path.controlPoints[j];
						controlPoint.a -= position;
						controlPoint.b -= position;
						this.path.controlPoints[j] = controlPoint;
					}
				}
			}
			if (this.relative)
			{
				this.ReEvaluateRelativeTween();
			}
			if (this.pathMode == PathMode.Full3D && base.GetComponent<SpriteRenderer>() != null)
			{
				this.pathMode = PathMode.TopDown2D;
			}
			Rigidbody component = base.GetComponent<Rigidbody>();
			TweenerCore<Vector3, Path, PathOptions> tweenerCore;
			if (this.tweenRigidbody && component != null)
			{
				tweenerCore = (this.isLocal ? component.DOLocalPath(this.path, this.duration, this.pathMode).SetOptions(this.isClosedPath, AxisConstraint.None, this.lockRotation) : component.DOPath(this.path, this.duration, this.pathMode).SetOptions(this.isClosedPath, AxisConstraint.None, this.lockRotation));
			}
			else
			{
				tweenerCore = (this.isLocal ? base.transform.DOLocalPath(this.path, this.duration, this.pathMode).SetOptions(this.isClosedPath, AxisConstraint.None, this.lockRotation) : base.transform.DOPath(this.path, this.duration, this.pathMode).SetOptions(this.isClosedPath, AxisConstraint.None, this.lockRotation));
			}
			switch (this.orientType)
			{
			case OrientType.ToPath:
				if (this.assignForwardAndUp)
				{
					tweenerCore.SetLookAt(this.lookAhead, new Vector3?(this.forwardDirection), new Vector3?(this.upDirection));
				}
				else
				{
					tweenerCore.SetLookAt(this.lookAhead, null, null);
				}
				break;
			case OrientType.LookAtTransform:
				if (this.lookAtTransform != null)
				{
					if (this.assignForwardAndUp)
					{
						tweenerCore.SetLookAt(this.lookAtTransform, new Vector3?(this.forwardDirection), new Vector3?(this.upDirection));
					}
					else
					{
						tweenerCore.SetLookAt(this.lookAtTransform, null, null);
					}
				}
				break;
			case OrientType.LookAtPosition:
				if (this.assignForwardAndUp)
				{
					tweenerCore.SetLookAt(this.lookAtPosition, new Vector3?(this.forwardDirection), new Vector3?(this.upDirection));
				}
				else
				{
					tweenerCore.SetLookAt(this.lookAtPosition, null, null);
				}
				break;
			}
			tweenerCore.SetDelay(this.delay).SetLoops(this.loops, this.loopType).SetAutoKill(this.autoKill).SetUpdate(this.updateType).OnKill(delegate
			{
				this.tween = null;
			});
			if (this.isSpeedBased)
			{
				tweenerCore.SetSpeedBased<TweenerCore<Vector3, Path, PathOptions>>();
			}
			if (this.easeType == Ease.INTERNAL_Custom)
			{
				tweenerCore.SetEase(this.easeCurve);
			}
			else
			{
				tweenerCore.SetEase(this.easeType);
			}
			if (!string.IsNullOrEmpty(this.id))
			{
				tweenerCore.SetId(this.id);
			}
			if (this.hasOnStart)
			{
				if (this.onStart != null)
				{
					tweenerCore.OnStart(new TweenCallback(this.onStart.Invoke));
				}
			}
			else
			{
				this.onStart = null;
			}
			if (this.hasOnPlay)
			{
				if (this.onPlay != null)
				{
					tweenerCore.OnPlay(new TweenCallback(this.onPlay.Invoke));
				}
			}
			else
			{
				this.onPlay = null;
			}
			if (this.hasOnUpdate)
			{
				if (this.onUpdate != null)
				{
					tweenerCore.OnUpdate(new TweenCallback(this.onUpdate.Invoke));
				}
			}
			else
			{
				this.onUpdate = null;
			}
			if (this.hasOnStepComplete)
			{
				if (this.onStepComplete != null)
				{
					tweenerCore.OnStepComplete(new TweenCallback(this.onStepComplete.Invoke));
				}
			}
			else
			{
				this.onStepComplete = null;
			}
			if (this.hasOnComplete)
			{
				if (this.onComplete != null)
				{
					tweenerCore.OnComplete(new TweenCallback(this.onComplete.Invoke));
				}
			}
			else
			{
				this.onComplete = null;
			}
			if (this.hasOnRewind)
			{
				if (this.onRewind != null)
				{
					tweenerCore.OnRewind(new TweenCallback(this.onRewind.Invoke));
				}
			}
			else
			{
				this.onRewind = null;
			}
			if (this.autoPlay)
			{
				tweenerCore.Play<TweenerCore<Vector3, Path, PathOptions>>();
			}
			else
			{
				tweenerCore.Pause<TweenerCore<Vector3, Path, PathOptions>>();
			}
			this.tween = tweenerCore;
			if (this.hasOnTweenCreated && this.onTweenCreated != null)
			{
				this.onTweenCreated.Invoke();
			}
		}

		private void Reset()
		{
			this.path = new Path(this.pathType, this.wps.ToArray(), 10, new Color?(this.pathColor));
		}

		private void OnDestroy()
		{
			if (this.tween != null && this.tween.active)
			{
				this.tween.Kill(false);
			}
			this.tween = null;
		}

		public override void DOPlay()
		{
			this.tween.Play<Tween>();
		}

		public override void DOPlayBackwards()
		{
			this.tween.PlayBackwards();
		}

		public override void DOPlayForward()
		{
			this.tween.PlayForward();
		}

		public override void DOPause()
		{
			this.tween.Pause<Tween>();
		}

		public override void DOTogglePause()
		{
			this.tween.TogglePause();
		}

		public override void DORewind()
		{
			this.tween.Rewind(true);
		}

		public override void DORestart(bool fromHere = false)
		{
			if (this.tween == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(this.tween);
				}
				return;
			}
			if (fromHere && this.relative && !this.isLocal)
			{
				this.ReEvaluateRelativeTween();
			}
			this.tween.Restart(true, -1f);
		}

		public override void DOComplete()
		{
			this.tween.Complete();
		}

		public override void DOKill()
		{
			this.tween.Kill(false);
		}

		public Tween GetTween()
		{
			if (this.tween == null || !this.tween.active)
			{
				if (Debugger.logPriority > 1)
				{
					if (this.tween == null)
					{
						Debugger.LogNullTween(this.tween);
					}
					else
					{
						Debugger.LogInvalidTween(this.tween);
					}
				}
				return null;
			}
			return this.tween;
		}

		public Vector3[] GetDrawPoints()
		{
			if (this.path.wps == null || this.path.nonLinearDrawWps == null)
			{
				Debugger.LogWarning("Draw points not ready yet. Returning NULL");
				return null;
			}
			if (this.pathType == PathType.Linear)
			{
				return this.path.wps;
			}
			return this.path.nonLinearDrawWps;
		}

        public Vector3[] GetFullWps()
		{
			int count = this.wps.Count;
			int num = count + 1;
			if (this.isClosedPath)
			{
				num++;
			}
			Vector3[] array = new Vector3[num];
			array[0] = base.transform.position;
			for (int i = 0; i < count; i++)
			{
				array[i + 1] = this.wps[i];
			}
			if (this.isClosedPath)
			{
				array[num - 1] = array[0];
			}
			return array;
		}

		private void ReEvaluateRelativeTween()
		{
			Vector3 position = base.transform.position;
			if (position == this.lastSrcPosition)
			{
				return;
			}
			Vector3 b = position - this.lastSrcPosition;
			int num = this.path.wps.Length;
			for (int i = 0; i < num; i++)
			{
				this.path.wps[i] = this.path.wps[i] + b;
			}
			num = this.path.controlPoints.Length;
			for (int j = 0; j < num; j++)
			{
				ControlPoint controlPoint = this.path.controlPoints[j];
				controlPoint.a += b;
				controlPoint.b += b;
				this.path.controlPoints[j] = controlPoint;
			}
			this.lastSrcPosition = position;
		}
	}
}
