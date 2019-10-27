using DG.DemiEditor;
using DG.DOTweenEditor.Core;
using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace DG.DOTweenEditor
{
	[CustomEditor(typeof(DOTweenPath))]
	public class DOTweenPathInspector : ABSAnimationInspector
	{
		[Serializable]
		private sealed class HeaderItem
		{
			public static readonly DOTweenPathInspector.HeaderItem item = new DOTweenPathInspector.HeaderItem();

			public static ReorderableList.HeaderCallbackDelegate headerHander;

			public void OnInspectorGUI(Rect rect)
			{
				EditorGUI.LabelField(rect, "Path Waypoints");
			}
		}

		private readonly Color _wpColor = Color.white;

		private readonly Color _arrowsColor = new Color(1f, 1f, 1f, 0.85f);

		private readonly Color _wpColorEnd = Color.red;

		private DOTweenPath _src;

		private readonly List<WpHandle> _wpsByDepth = new List<WpHandle>();

		private int _minHandleControlId;

		private int _maxHandleControlId;

		private int _selectedWpIndex = -1;

		private int _lastSelectedWpIndex = -1;

		private int _lastCreatedWpIndex = -1;

		private bool _changed;

		private Vector3 _lastSceneViewCamPosition;

		private Quaternion _lastSceneViewCamRotation;

		private bool _isDragging;

		private bool _reselectAfterDrag;

		private bool _sceneCamStored;

		private bool _refreshAfterEnable;

		private Camera _fooSceneCam;

		private Transform _fooSceneCamTrans;

		private ReorderableList _wpsList;

		public bool updater;

		private bool _showAddManager
		{
			get
			{
				return this._src.inspectorMode == DOTweenInspectorMode.Default || this._src.inspectorMode == DOTweenInspectorMode.Developer;
			}
		}

		private bool _showTweenSettings
		{
			get
			{
				return this._src.inspectorMode == DOTweenInspectorMode.Default || this._src.inspectorMode == DOTweenInspectorMode.Developer;
			}
		}

		private Camera _sceneCam
		{
			get
			{
				if (this._fooSceneCam == null)
				{
					SceneView currentDrawingSceneView = SceneView.currentDrawingSceneView;
					if (currentDrawingSceneView == null)
					{
						return null;
					}
					this._fooSceneCam = currentDrawingSceneView.camera;
				}
				return this._fooSceneCam;
			}
		}

		private Transform _sceneCamTrans
		{
			get
			{
				if (this._fooSceneCamTrans == null)
				{
					if (this._sceneCam == null)
					{
						return null;
					}
					this._fooSceneCamTrans = this._sceneCam.transform;
				}
				return this._fooSceneCamTrans;
			}
		}

		private void OnEnable()
		{
			this._src = (base.target as DOTweenPath);
			this.StoreSceneCamData();
			if (this._src.path == null)
			{
				this.ResetPath(RepaintMode.None);
			}
			this.onStartProperty = base.serializedObject.FindProperty("onStart");
			this.onPlayProperty = base.serializedObject.FindProperty("onPlay");
			this.onUpdateProperty = base.serializedObject.FindProperty("onUpdate");
			this.onStepCompleteProperty = base.serializedObject.FindProperty("onStepComplete");
			this.onCompleteProperty = base.serializedObject.FindProperty("onComplete");
			this.onRewindProperty = base.serializedObject.FindProperty("onRewind");
			this.onTweenCreatedProperty = base.serializedObject.FindProperty("onTweenCreated");
			this._refreshAfterEnable = true;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			EditorGUIUtils.SetGUIStyles(null);
			GUILayout.Space(3f);
			EditorGUIUtils.InspectorLogo();
			if (Application.isPlaying)
			{
				GUILayout.Space(8f);
				GUILayout.Label("Path Editor disabled while in play mode", EditorGUIUtils.wordWrapLabelStyle, new GUILayoutOption[0]);
				GUILayout.Space(10f);
				return;
			}
			if (this._refreshAfterEnable)
			{
				this._refreshAfterEnable = false;
				if (this._src.path == null)
				{
					this.ResetPath(RepaintMode.None);
				}
				else
				{
					this.RefreshPath(RepaintMode.Scene, true);
				}
				this._wpsList = new ReorderableList(this._src.wps, typeof(Vector3), true, true, true, true);
				ReorderableList arg_CD_0 = this._wpsList;
				ReorderableList.HeaderCallbackDelegate arg_CD_1;
				if ((arg_CD_1 = DOTweenPathInspector.HeaderItem.headerHander) == null)
				{
                    HeaderItem.headerHander = new ReorderableList.HeaderCallbackDelegate(HeaderItem.item.OnInspectorGUI);
                    arg_CD_1 = HeaderItem.headerHander;
				}
				arg_CD_0.drawHeaderCallback = arg_CD_1;
				this._wpsList.onReorderCallback = delegate(ReorderableList list)
				{
					this.RefreshPath(RepaintMode.Scene, true);
				};
				this._wpsList.drawElementCallback = delegate(Rect rect, int index, bool isActive, bool isFocused)
				{
					Rect position = new Rect(rect.xMin, rect.yMin, 23f, rect.height);
					Rect position2 = new Rect(position.xMax, position.yMin, rect.width - 23f, position.height);
					GUI.Label(position, (index + 1).ToString());
					this._src.wps[index] = EditorGUI.Vector3Field(position2, "", this._src.wps[index]);
				};
			}
			bool flag = false;
			Undo.RecordObject(this._src, "DOTween Path");
			if (this._src.inspectorMode != DOTweenInspectorMode.Default)
			{
				GUILayout.Label("Inspector Mode: <b>" + this._src.inspectorMode + "</b>", ABSAnimationInspector.styles.custom.warningLabel, new GUILayoutOption[0]);
				GUILayout.Space(2f);
			}
			if (!(this._src.GetComponent<DOTweenVisualManager>() != null) && this._showAddManager)
			{
				if (GUILayout.Button(new GUIContent("Add Manager", "Adds a manager component which allows you to choose additional options for this gameObject"), new GUILayoutOption[0]))
				{
					this._src.gameObject.AddComponent<DOTweenVisualManager>();
				}
				GUILayout.Space(4f);
			}
			AnimationInspectorGUI.StickyTitle("Scene View Commands");
			DeGUILayout.BeginVBox(DeGUI.styles.box.stickyTop);
			GUILayout.Label("➲ SHIFT + " + (EditorUtils.isOSXEditor ? "CMD" : "CTRL") + ": add a waypoint\n➲ SHIFT + ALT: remove a waypoint", new GUILayoutOption[0]);
			DeGUILayout.EndVBox();
			AnimationInspectorGUI.StickyTitle("Info");
			DeGUILayout.BeginVBox(DeGUI.styles.box.stickyTop);
			GUILayout.Label("Path Length: " + ((this._src.path == null) ? "-" : this._src.path.length.ToString()), new GUILayoutOption[0]);
			DeGUILayout.EndVBox();
			if (this._showTweenSettings)
			{
				AnimationInspectorGUI.StickyTitle("Tween Options");
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				this._src.autoPlay = DeGUILayout.ToggleButton(this._src.autoPlay, new GUIContent("AutoPlay", "If selected, the tween will play automatically"), DeGUI.styles.button.tool, new GUILayoutOption[0]);
				this._src.autoKill = DeGUILayout.ToggleButton(this._src.autoKill, new GUIContent("AutoKill", "If selected, the tween will be killed when it completes, and won't be reusable"), DeGUI.styles.button.tool, new GUILayoutOption[0]);
				GUILayout.EndHorizontal();
				DeGUILayout.BeginVBox(DeGUI.styles.box.stickyTop);
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				this._src.duration = EditorGUILayout.FloatField("Duration", this._src.duration, new GUILayoutOption[0]);
				if (this._src.duration < 0f)
				{
					this._src.duration = 0f;
				}
				this._src.isSpeedBased = DeGUILayout.ToggleButton(this._src.isSpeedBased, new GUIContent("SpeedBased", "If selected, the duration will count as units/degree x second"), DeGUI.styles.button.tool, new GUILayoutOption[]
				{
					GUILayout.Width(75f)
				});
				GUILayout.EndHorizontal();
				this._src.delay = EditorGUILayout.FloatField("Delay", this._src.delay, new GUILayoutOption[0]);
				if (this._src.delay < 0f)
				{
					this._src.delay = 0f;
				}
				this._src.easeType = EditorGUIUtils.FilteredEasePopup(this._src.easeType);
				if (this._src.easeType == Ease.INTERNAL_Custom)
				{
					this._src.easeCurve = EditorGUILayout.CurveField("   Ease Curve", this._src.easeCurve, new GUILayoutOption[0]);
				}
				this._src.loops = EditorGUILayout.IntField(new GUIContent("Loops", "Set to -1 for infinite loops"), this._src.loops, new GUILayoutOption[0]);
				if (this._src.loops < -1)
				{
					this._src.loops = -1;
				}
				if (this._src.loops > 1 || this._src.loops == -1)
				{
					this._src.loopType = (LoopType)EditorGUILayout.EnumPopup("   Loop Type", this._src.loopType, new GUILayoutOption[0]);
				}
				this._src.id = EditorGUILayout.TextField("ID", this._src.id, new GUILayoutOption[0]);
				this._src.updateType = (UpdateType)EditorGUILayout.EnumPopup("Update Type", this._src.updateType, new GUILayoutOption[0]);
				if (this._src.inspectorMode == DOTweenInspectorMode.Developer)
				{
					GUILayout.BeginHorizontal(new GUILayoutOption[0]);
					Rigidbody component = this._src.GetComponent<Rigidbody>();
					this._src.tweenRigidbody = EditorGUILayout.Toggle("Tween Rigidbody", component != null && this._src.tweenRigidbody, new GUILayoutOption[0]);
					if (component == null)
					{
						GUILayout.Label("No rigidbody found", ABSAnimationInspector.styles.custom.warningLabel, new GUILayoutOption[0]);
					}
					GUILayout.EndHorizontal();
					if (this._src.tweenRigidbody)
					{
						EditorGUILayout.HelpBox("Tweening a rigidbody works correctly only when it's kinematic", MessageType.Warning);
					}
				}
				DeGUILayout.EndVBox();
				AnimationInspectorGUI.StickyTitle("Path Tween Options");
				DeGUILayout.BeginVBox(DeGUI.styles.box.stickyTop);
				PathType pathType = this._src.pathType;
				this._src.pathType = (PathType)EditorGUILayout.EnumPopup("Path Type", this._src.pathType, new GUILayoutOption[0]);
				if (pathType != this._src.pathType)
				{
					flag = true;
				}
				if (this._src.pathType != PathType.Linear)
				{
					this._src.pathResolution = EditorGUILayout.IntSlider("   Path resolution", this._src.pathResolution, 2, 20, new GUILayoutOption[0]);
				}
				bool isClosedPath = this._src.isClosedPath;
				this._src.isClosedPath = EditorGUILayout.Toggle("Close Path", this._src.isClosedPath, new GUILayoutOption[0]);
				if (isClosedPath != this._src.isClosedPath)
				{
					flag = true;
				}
				this._src.isLocal = EditorGUILayout.Toggle(new GUIContent("Local Movement", "If checked, the path will tween the localPosition (instead than the position) of its target"), this._src.isLocal, new GUILayoutOption[0]);
				this._src.pathMode = (PathMode)EditorGUILayout.EnumPopup("Path Mode", this._src.pathMode, new GUILayoutOption[0]);
				this._src.lockRotation = (AxisConstraint)EditorGUILayout.EnumPopup("Lock Rotation", this._src.lockRotation, new GUILayoutOption[0]);
				this._src.orientType = (OrientType)EditorGUILayout.EnumPopup("Orientation", this._src.orientType, new GUILayoutOption[0]);
				if (this._src.orientType != OrientType.None)
				{
					switch (this._src.orientType)
					{
					case OrientType.ToPath:
						this._src.lookAhead = EditorGUILayout.Slider("   LookAhead", this._src.lookAhead, 0f, 1f, new GUILayoutOption[0]);
						break;
					case OrientType.LookAtTransform:
						this._src.lookAtTransform = (EditorGUILayout.ObjectField("   LookAt Target", this._src.lookAtTransform, typeof(Transform), true, new GUILayoutOption[0]) as Transform);
						break;
					case OrientType.LookAtPosition:
						this._src.lookAtPosition = EditorGUILayout.Vector3Field("   LookAt Position", this._src.lookAtPosition, new GUILayoutOption[0]);
						break;
					}
				}
				DeGUILayout.EndVBox();
			}
			AnimationInspectorGUI.StickyTitle("Path Editor Options");
			DeGUILayout.BeginVBox(DeGUI.styles.box.stickyTop);
			this._src.relative = EditorGUILayout.Toggle(new GUIContent("Relative", "If toggled, the whole path moves with the target"), this._src.relative, new GUILayoutOption[0]);
			this._src.pathColor = EditorGUILayout.ColorField("Color", this._src.pathColor, new GUILayoutOption[0]);
			this._src.showIndexes = EditorGUILayout.Toggle("Show Indexes", this._src.showIndexes, new GUILayoutOption[0]);
			this._src.showWpLength = EditorGUILayout.Toggle("Show WPs Lengths", this._src.showWpLength, new GUILayoutOption[0]);
			this._src.livePreview = EditorGUILayout.Toggle("Live Preview", this._src.livePreview, new GUILayoutOption[0]);
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("Handles Type/Mode", new GUILayoutOption[]
			{
				GUILayout.Width(EditorGUIUtility.labelWidth - 11f)
			});
			this._src.handlesType = (HandlesType)EditorGUILayout.EnumPopup(this._src.handlesType, new GUILayoutOption[0]);
			this._src.handlesDrawMode = (HandlesDrawMode)EditorGUILayout.EnumPopup(this._src.handlesDrawMode, new GUILayoutOption[0]);
			GUILayout.EndHorizontal();
			if (this._src.handlesDrawMode == HandlesDrawMode.Perspective)
			{
				this._src.perspectiveHandleSize = EditorGUILayout.FloatField("   Handle Size", this._src.perspectiveHandleSize, new GUILayoutOption[0]);
			}
			DeGUILayout.EndVBox();
			if (this._showTweenSettings)
			{
				AnimationInspectorGUI.AnimationEvents(this, this._src);
			}
			this.DrawExtras();
			GUILayout.Space(10f);
			DeGUILayout.BeginToolbar(new GUILayoutOption[0]);
			this._src.wpsDropdown = DeGUILayout.ToolbarFoldoutButton(this._src.wpsDropdown, "Waypoints", false, false);
			GUILayout.FlexibleSpace();
			if (GUILayout.Button(new GUIContent("Copy to clipboard", "Copies the current waypoints to clipboard, as an array ready to be pasted in your code"), DeGUI.styles.button.tool, new GUILayoutOption[0]))
			{
				this.CopyWaypointsToClipboard();
			}
			DeGUILayout.EndToolbar();
			if (this._src.wpsDropdown)
			{
				DeGUILayout.BeginVBox(DeGUI.styles.box.stickyTop);
				bool arg_ABE_0 = GUI.changed;
				this._wpsList.DoLayoutList();
				if (!arg_ABE_0 && GUI.changed)
				{
					flag = true;
				}
				DeGUILayout.EndVBox();
			}
			else
			{
				GUILayout.Space(5f);
			}
			if (flag)
			{
				this.RefreshPath(RepaintMode.Scene, false);
				return;
			}
			if (GUI.changed)
			{
				EditorUtility.SetDirty(this._src);
				this.DORepaint(RepaintMode.Scene, false);
			}
		}

		private void OnSceneGUI()
		{
			if (Application.isPlaying)
			{
				return;
			}
			this.StoreSceneCamData();
			if (!this._src.gameObject.activeInHierarchy || !this._sceneCamStored)
			{
				return;
			}
			if (this._wpsByDepth.Count != this._src.wps.Count)
			{
				this.FillWpIndexByDepth();
			}
			EditorGUIUtils.SetGUIStyles(null);
			Event current = Event.current;
			Undo.RecordObject(this._src, "DOTween Path");
			if (current.type == EventType.MouseDown)
			{
				if (current.shift)
				{
					if (EditorGUI.actionKey)
					{
						Vector3 vector = (this._lastCreatedWpIndex != -1) ? this._src.wps[this._lastCreatedWpIndex] : ((this._selectedWpIndex != -1) ? this._src.wps[this._selectedWpIndex] : ((this._lastSelectedWpIndex != -1) ? this._src.wps[this._lastSelectedWpIndex] : this._src.transform.position));
						Matrix4x4 worldToCameraMatrix = this._sceneCam.worldToCameraMatrix;
						float z = -(worldToCameraMatrix.m20 * vector.x + worldToCameraMatrix.m21 * vector.y + worldToCameraMatrix.m22 * vector.z + worldToCameraMatrix.m23);
						Vector3 item = this._sceneCam.ViewportToWorldPoint(new Vector3(current.mousePosition.x / this._sceneCam.pixelRect.width, 1f - current.mousePosition.y / this._sceneCam.pixelRect.height, z));
						if (this._selectedWpIndex != -1 && this._selectedWpIndex < this._src.wps.Count - 1)
						{
							this._src.wps.Insert(this._selectedWpIndex + 1, item);
							this._lastCreatedWpIndex = this._selectedWpIndex + 1;
							this._selectedWpIndex = this._lastCreatedWpIndex;
						}
						else
						{
							this._src.wps.Add(item);
							this._lastCreatedWpIndex = this._src.wps.Count - 1;
							this._selectedWpIndex = this._lastCreatedWpIndex;
						}
						this.RefreshPath(RepaintMode.Scene, true);
						return;
					}
					if (current.alt && this._src.wps.Count > 1)
					{
						this.FindSelectedWaypointIndex();
						if (this._selectedWpIndex != -1)
						{
							this._src.wps.RemoveAt(this._selectedWpIndex);
							this.ResetIndexes();
							this.RefreshPath(RepaintMode.Scene, true);
							return;
						}
					}
				}
				this.FindSelectedWaypointIndex();
			}
			if (this._src.wps.Count < 1)
			{
				return;
			}
			if (current.type == EventType.MouseDrag)
			{
				this._isDragging = true;
				if (this._src.livePreview)
				{
					bool flag = this.CheckTargetMove();
					if (this._selectedWpIndex != -1)
					{
						flag = true;
					}
					if (flag)
					{
						this.RefreshPath(RepaintMode.Scene, false);
					}
				}
			}
			else if (this._isDragging && current.rawType == EventType.MouseUp)
			{
				if (this._isDragging && this._selectedWpIndex != -1)
				{
					this._reselectAfterDrag = true;
				}
				this._isDragging = false;
				if (this._selectedWpIndex != -1 || this.CheckTargetMove())
				{
					EditorUtility.SetDirty(this._src);
					this.RefreshPath(RepaintMode.Scene, true);
				}
			}
			else if (this.CheckTargetMove())
			{
				this.RefreshPath(RepaintMode.Scene, false);
			}
			if (this._changed && !this._isDragging)
			{
				this.FillWpIndexByDepth();
				this._changed = false;
			}
			int count = this._src.wps.Count;
			for (int i = 0; i < count; i++)
			{
				WpHandle wpHandle = this._wpsByDepth[i];
				bool flag2 = wpHandle.wpIndex == this._selectedWpIndex;
				Vector3 vector2 = this._src.wps[wpHandle.wpIndex];
				float num = (this._src.handlesDrawMode == HandlesDrawMode.Orthographic) ? (HandleUtility.GetHandleSize(vector2) * 0.2f) : this._src.perspectiveHandleSize;
				bool expr_40C = wpHandle.wpIndex >= 0 && wpHandle.wpIndex < (this._src.isClosedPath ? count : (count - 1));
				Vector3 vector3 = expr_40C ? ((wpHandle.wpIndex >= count - 1) ? this._src.transform.position : this._src.wps[wpHandle.wpIndex + 1]) : Vector3.zero;
				bool flag3 = expr_40C && Vector3.Distance(this._sceneCamTrans.position, vector2) < Vector3.Distance(this._sceneCamTrans.position, vector2 + Vector3.ClampMagnitude(vector3 - vector2, num * 1.75f));
				if (flag2)
				{
					Handles.color = Color.yellow;
				}
				else if (wpHandle.wpIndex == count - 1 && !this._src.isClosedPath)
				{
					Handles.color = this._wpColorEnd;
				}
				else
				{
					Handles.color = this._wpColor;
				}
				if (expr_40C & flag3)
				{
					this.DrawArrowFor(wpHandle.wpIndex, num, vector3);
				}
				int controlID = GUIUtility.GetControlID(FocusType.Passive);
				if (i == 0)
				{
					this._minHandleControlId = controlID;
				}
				if (this._src.handlesType == HandlesType.Free)
				{
					vector2 = Handles.FreeMoveHandle(vector2, Quaternion.identity, num, Vector3.one, new Handles.DrawCapFunction(Handles.SphereCap));
				}
				else
				{
					vector2 = Handles.PositionHandle(vector2, Quaternion.identity);
				}
				this._src.wps[wpHandle.wpIndex] = vector2;
				int controlID2 = GUIUtility.GetControlID(FocusType.Passive);
				wpHandle.controlId = ((i == 0) ? (controlID2 - 1) : (controlID + 1));
				this._maxHandleControlId = controlID2;
				if (expr_40C && !flag3)
				{
					this.DrawArrowFor(wpHandle.wpIndex, num, vector3);
				}
				Vector3 position = this._sceneCamTrans.InverseTransformPoint(vector2) + new Vector3(num * 0.75f, 0.1f, 0f);
				position = this._sceneCamTrans.TransformPoint(position);
				if (this._src.showIndexes || this._src.showWpLength)
				{
					string text = (this._src.showIndexes && this._src.showWpLength) ? string.Concat(new object[]
					{
						wpHandle.wpIndex + 1,
						"(",
						this._src.path.wpLengths[wpHandle.wpIndex + 1].ToString("N2"),
						")"
					}) : (this._src.showIndexes ? (wpHandle.wpIndex + 1).ToString() : this._src.path.wpLengths[wpHandle.wpIndex + 1].ToString("N2"));
					Handles.Label(position, text, flag2 ? EditorGUIUtils.handleSelectedLabelStyle : EditorGUIUtils.handlelabelStyle);
				}
			}
			Handles.color = this._src.pathColor;
			if (this._src.pathType == PathType.Linear)
			{
				Handles.DrawPolyLine(this._src.path.wps);
			}
			else if (this._src.path.nonLinearDrawWps != null)
			{
				Handles.DrawPolyLine(this._src.path.nonLinearDrawWps);
			}
			if (this._reselectAfterDrag && current.type == EventType.Repaint)
			{
				this._reselectAfterDrag = false;
			}
			if (!this._changed)
			{
				this._changed = this.Changed();
			}
			if (this._changed)
			{
				EditorUtility.SetDirty(this._src);
			}
		}

		private void DORepaint(RepaintMode repaintMode, bool refreshWpIndexByDepth)
		{
			switch (repaintMode)
			{
			case RepaintMode.Scene:
				SceneView.RepaintAll();
				break;
			case RepaintMode.Inspector:
				EditorUtility.SetDirty(this._src);
				break;
			case RepaintMode.SceneAndInspector:
				EditorUtility.SetDirty(this._src);
				SceneView.RepaintAll();
				break;
			}
			if (refreshWpIndexByDepth)
			{
				this.FillWpIndexByDepth();
			}
		}

		private bool Changed()
		{
			if (GUI.changed)
			{
				return true;
			}
			if (this._lastSelectedWpIndex != this._selectedWpIndex)
			{
				this._lastSelectedWpIndex = this._selectedWpIndex;
				return true;
			}
			if (this.CheckTargetMove())
			{
				return true;
			}
			if (this._sceneCamTrans.position != this._lastSceneViewCamPosition || this._sceneCamTrans.rotation != this._lastSceneViewCamRotation)
			{
				this._lastSceneViewCamPosition = this._sceneCamTrans.position;
				this._lastSceneViewCamRotation = this._sceneCamTrans.rotation;
				return true;
			}
			return false;
		}

		private void DrawArrowFor(int wpIndex, float handleSize, Vector3 arrowPointsAt)
		{
			Color arg_5A_0 = Handles.color;
			Handles.color = this._arrowsColor;
			Vector3 vector = this._src.wps[wpIndex];
			Vector3 vector2 = arrowPointsAt - vector;
			if (vector2.magnitude >= handleSize * 1.75f)
			{
				Handles.ConeCap(wpIndex, vector + Vector3.ClampMagnitude(vector2, handleSize), Quaternion.LookRotation(vector2), handleSize * 0.65f);
			}
			Handles.color = arg_5A_0;
		}

		private void DrawExtras()
		{
			AnimationInspectorGUI.StickyTitle("Extras");
			DeGUILayout.BeginVBox(DeGUI.styles.box.sticky);
			if (GUILayout.Button("Reset Path", new GUILayoutOption[0]))
			{
				this.ResetPath(RepaintMode.SceneAndInspector);
			}
			DeGUILayout.EndVBox();
			GUILayout.Space(2f);
			GUILayout.BeginHorizontal(DeGUI.styles.box.stickyTop, new GUILayoutOption[0]);
			if (GUILayout.Button("Drop To Floor", new GUILayoutOption[0]))
			{
				this.DropToFloor(this._src.dropToFloorOffset);
			}
			GUILayout.Space(7f);
			GUILayout.Label("Offset Y", new GUILayoutOption[]
			{
				GUILayout.Width(49f)
			});
			this._src.dropToFloorOffset = EditorGUILayout.FloatField(this._src.dropToFloorOffset, new GUILayoutOption[]
			{
				GUILayout.Width(40f)
			});
			GUILayout.EndHorizontal();
		}

		private void StoreSceneCamData()
		{
			if (this._sceneCam == null)
			{
				this._sceneCamStored = false;
				return;
			}
			if (this._sceneCamStored)
			{
				return;
			}
			if (this._sceneCam == null)
			{
				return;
			}
			this._sceneCamStored = true;
			this._lastSceneViewCamPosition = this._sceneCamTrans.position;
			this._lastSceneViewCamRotation = this._sceneCamTrans.rotation;
		}

		private void FillWpIndexByDepth()
		{
			if (!this._sceneCamStored)
			{
				return;
			}
			int count = this._src.wps.Count;
			if (count == 0)
			{
				return;
			}
			this._wpsByDepth.Clear();
			for (int i = 0; i < count; i++)
			{
				this._wpsByDepth.Add(new WpHandle(i));
			}
			this._wpsByDepth.Sort(delegate(WpHandle x, WpHandle y)
			{
				float num = Vector3.Distance(this._sceneCamTrans.position, this._src.wps[x.wpIndex]);
				float num2 = Vector3.Distance(this._sceneCamTrans.position, this._src.wps[y.wpIndex]);
				if (num > num2)
				{
					return -1;
				}
				if (num < num2)
				{
					return 1;
				}
				return 0;
			});
		}

		private void FindSelectedWaypointIndex()
		{
			this._lastSelectedWpIndex = this._selectedWpIndex;
			this._selectedWpIndex = -1;
			int count = this._src.wps.Count;
			if (count == 0)
			{
				return;
			}
			int nearestControl = HandleUtility.nearestControl;
			if (nearestControl == 0 || nearestControl < this._minHandleControlId || nearestControl > this._maxHandleControlId)
			{
				return;
			}
			int num = -1;
			for (int i = 0; i < count; i++)
			{
				int controlId = this._wpsByDepth[i].controlId;
				if (controlId != -1 && controlId != 0)
				{
					int wpIndex = this._wpsByDepth[i].wpIndex;
					if (controlId > nearestControl)
					{
						this._selectedWpIndex = this._wpsByDepth[(num == -1) ? i : num].wpIndex;
						this._lastCreatedWpIndex = -1;
						return;
					}
					if (controlId == nearestControl)
					{
						this._selectedWpIndex = wpIndex;
						this._lastCreatedWpIndex = -1;
						return;
					}
					num = i;
				}
			}
			if (this._selectedWpIndex == -1)
			{
				this._selectedWpIndex = this._wpsByDepth[num].wpIndex;
				this._lastCreatedWpIndex = -1;
			}
		}

		private void ResetPath(RepaintMode repaintMode)
		{
			this._src.wps.Clear();
			this._src.lastSrcPosition = this._src.transform.position;
			this._src.path = new Path(this._src.pathType, this._src.wps.ToArray(), 10, new Color?(this._src.pathColor));
			this._wpsByDepth.Clear();
			this.ResetIndexes();
			this.DORepaint(repaintMode, false);
		}

		private void ResetIndexes()
		{
			this._selectedWpIndex = (this._lastSelectedWpIndex = (this._lastCreatedWpIndex = -1));
		}

		private bool CheckTargetMove()
		{
			if (this._src.lastSrcPosition != this._src.transform.position)
			{
				if (this._src.relative)
				{
					Vector3 b = this._src.transform.position - this._src.lastSrcPosition;
					int count = this._src.wps.Count;
					for (int i = 0; i < count; i++)
					{
						this._src.wps[i] = this._src.wps[i] + b;
					}
				}
				this._src.lastSrcPosition = this._src.transform.position;
				return true;
			}
			return false;
		}

		private void RefreshPath(RepaintMode repaintMode, bool refreshWpIndexByDepth)
		{
			if (this._src.wps.Count < 1)
			{
				return;
			}
			this._src.path.AssignDecoder(this._src.pathType);
			this._src.path.AssignWaypoints(this._src.GetFullWps(), false);
			this._src.path.FinalizePath(this._src.isClosedPath, AxisConstraint.None, this._src.transform.position);
			if (this._src.pathType != PathType.Linear)
			{
				Path.RefreshNonLinearDrawWps(this._src.path);
			}
			this.DORepaint(repaintMode, refreshWpIndexByDepth);
		}

		private void DropToFloor(float offsetY)
		{
			bool flag = false;
			for (int i = 0; i < this._src.wps.Count; i++)
			{
				Vector3 origin = this._src.wps[i];
				origin.y += 0.01f;
				RaycastHit raycastHit;
				if (Physics.Raycast(origin, Vector3.down, out raycastHit, float.PositiveInfinity))
				{
					flag = true;
					Vector3 point = raycastHit.point;
					point.y += offsetY;
					this._src.wps[i] = point;
					this.RefreshPath(RepaintMode.SceneAndInspector, true);
				}
			}
			if (!flag)
			{
				EditorUtility.DisplayDialog("Drop To Floor", "No colliders to drop on.", "Ok");
				return;
			}
			EditorUtility.SetDirty(this._src);
		}

		private void CopyWaypointsToClipboard()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Vector3[] waypoints = new[] { ");
			for (int i = 0; i < this._src.wps.Count; i++)
			{
				Vector3 vector = this._src.wps[i];
				if (i > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(string.Format("new Vector3({0}f,{1}f,{2}f)", vector.x, vector.y, vector.z));
			}
			stringBuilder.Append(" };");
			EditorGUIUtility.systemCopyBuffer = stringBuilder.ToString();
		}
	}
}
