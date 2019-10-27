using DG.Tweening.Core;
using System;
using UnityEngine;

namespace DG.Tweening
{
	[AddComponentMenu("")]
	public class DOTweenVisualManager : MonoBehaviour
	{
		public VisualManagerPreset preset;

		public OnEnableBehaviour onEnableBehaviour;

		public OnDisableBehaviour onDisableBehaviour;

		private bool _requiresRestartFromSpawnPoint;

		private ABSAnimationComponent _animComponent;

		private void Awake()
		{
			this._animComponent = base.GetComponent<ABSAnimationComponent>();
		}

		private void Update()
		{
			if (!this._requiresRestartFromSpawnPoint || this._animComponent == null)
			{
				return;
			}
			this._requiresRestartFromSpawnPoint = false;
			this._animComponent.DORestart(true);
		}

		private void OnEnable()
		{
			switch (this.onEnableBehaviour)
			{
			case OnEnableBehaviour.Play:
				if (this._animComponent != null)
				{
					this._animComponent.DOPlay();
					return;
				}
				break;
			case OnEnableBehaviour.Restart:
				if (this._animComponent != null)
				{
					this._animComponent.DORestart(false);
					return;
				}
				break;
			case OnEnableBehaviour.RestartFromSpawnPoint:
				this._requiresRestartFromSpawnPoint = true;
				break;
			default:
				return;
			}
		}

		private void OnDisable()
		{
			this._requiresRestartFromSpawnPoint = false;
			switch (this.onDisableBehaviour)
			{
			case OnDisableBehaviour.Pause:
				if (this._animComponent != null)
				{
					this._animComponent.DOPause();
					return;
				}
				break;
			case OnDisableBehaviour.Rewind:
				if (this._animComponent != null)
				{
					this._animComponent.DORewind();
					return;
				}
				break;
			case OnDisableBehaviour.Kill:
				if (this._animComponent != null)
				{
					this._animComponent.DOKill();
					return;
				}
				break;
			case OnDisableBehaviour.KillAndComplete:
				if (this._animComponent != null)
				{
					this._animComponent.DOComplete();
					this._animComponent.DOKill();
					return;
				}
				break;
			case OnDisableBehaviour.DestroyGameObject:
				if (this._animComponent != null)
				{
					this._animComponent.DOKill();
				}
				UnityEngine.Object.Destroy(base.gameObject);
				break;
			default:
				return;
			}
		}
	}
}
