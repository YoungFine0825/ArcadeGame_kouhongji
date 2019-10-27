using System;
using JW.Common;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using JW.Framework.UGUI;

[CustomEditor(typeof(UIButton), true)]
public class UIButtonInspector : Editor
{
    private string[] _curvePresetName;
    private string[] _audioPresetName;

    private static readonly string[] TriggerName = { "不触发", "按下", "松开", "点击" };
    private static readonly string[] BehaviorName = { "无", "预设", "自定义" };

    // 点击动画
    private SerializedProperty _pressCurveType;
    private SerializedProperty _pressPresetName;
    private SerializedProperty _pressCurve;

    private bool _pressExpand = true;

    // 音效
    private SerializedProperty _audioTrigger;
    private SerializedProperty _audioType;
    private SerializedProperty _audioPreset;
    private SerializedProperty _audioCustomResName;

    private bool _audioExpand = true;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // 点击曲线
        _pressExpand = EditorGUIHelper.DrawHead("点击曲线", _pressExpand);
        if (_pressExpand)
        {
            EditorGUILayout.BeginVertical("Box");
            {
                DrawPressData();
            }
            EditorGUILayout.EndVertical();
        }

        // 音效
        _audioExpand = EditorGUIHelper.DrawHead("音效", _audioExpand);
        if (_audioExpand)
        {
            EditorGUILayout.BeginVertical("Box");
            {
                DrawAudioData();
            }
            EditorGUILayout.EndVertical();
        }
        serializedObject.ApplyModifiedProperties();
    }

    protected void OnEnable()
    {
        // 预设
        GameObject go = GameObject.Find(UIButton.ConfigFilename);
        if (go == null)
        {
            Object o = Resources.Load(UIButton.ConfigFilename);
            go = (GameObject)PrefabUtility.InstantiatePrefab(o);
            go.name = UIButton.ConfigFilename;
        }

        //
        UICurveCfg cmCurve = go.GetComponent<UICurveCfg>();

        _curvePresetName = new string[cmCurve.CurveDatas.Length];
        for (int i = 0; i < cmCurve.CurveDatas.Length; i++)
        {
            _curvePresetName[i] = cmCurve.CurveDatas[i].Name;
        }

        //
        UIAudioCfg cmAudio = go.GetComponent<UIAudioCfg>();

        _audioPresetName = new string[cmAudio.AudioDatas.Length];
        for (int i = 0; i < _audioPresetName.Length; i++)
        {
            _audioPresetName[i] = cmAudio.AudioDatas[i].Name;
        }

        DestroyImmediate(go);

        // 点击动画
        _pressCurveType = serializedObject.FindProperty("PressCurveType");
        _pressPresetName = serializedObject.FindProperty("PressPresetName");
        _pressCurve = serializedObject.FindProperty("PressCurve");

        // 音效
        _audioTrigger = serializedObject.FindProperty("AudioTrigger");
        _audioType = serializedObject.FindProperty("AudioType");
        _audioPreset = serializedObject.FindProperty("AudioPresetName");
        _audioCustomResName = serializedObject.FindProperty("CustomAudioName");

    }

    private void DrawPressData()
    {
        int curveType = EditorGUILayout.Popup("类型", _pressCurveType.enumValueIndex, BehaviorName);
        if (_pressCurveType.enumValueIndex != curveType)
        {
            _pressCurveType.enumValueIndex = curveType;
        }

        switch ((UIButton.BehaviorType)curveType)
        {
            case UIButton.BehaviorType.None:
                _pressPresetName.stringValue = string.Empty;

                if (_pressCurve.animationCurveValue.length > 0)
                {
                    _pressCurve.animationCurveValue = new AnimationCurve();
                }
                break;

            case UIButton.BehaviorType.Preset:
                int index = Array.FindIndex(_curvePresetName, m => m == _pressPresetName.stringValue);
                if (index == -1)
                {
                    index = 0;
                    _pressPresetName.stringValue = _curvePresetName[0];
                }

                int result = EditorGUILayout.Popup("预设名", index, _curvePresetName);
                if (result != index)
                {
                    _pressPresetName.stringValue = _curvePresetName[result];
                }

                if (_pressCurve.animationCurveValue.length > 0)
                {
                    _pressCurve.animationCurveValue = new AnimationCurve();
                }
                break;

            case UIButton.BehaviorType.Custom:
                _pressPresetName.stringValue = string.Empty;
                EditorGUILayout.PropertyField(_pressCurve, new GUIContent("曲线"));
                break;
        }
    }

    private void DrawAudioData()
    {
        int i = EditorGUILayout.Popup("触发", _audioTrigger.enumValueIndex, TriggerName);
        if (_audioTrigger.enumValueIndex != i)
        {
            _audioTrigger.enumValueIndex = i;
        }

        if ((UIButton.BehaviorType)i == UIButton.BehaviorType.None)
        {
            return;
        }

        i = EditorGUILayout.Popup("类型", _audioType.enumValueIndex, BehaviorName);
        if (_audioType.enumValueIndex != i)
        {
            _audioType.enumValueIndex = i;
        }

        switch ((UIButton.BehaviorType)i)
        {
            case UIButton.BehaviorType.None:
                break;
            //预设
            case UIButton.BehaviorType.Preset:
                int index = Array.FindIndex(_audioPresetName, m => m == _audioPreset.stringValue);
                if (index == -1)
                {
                    index = 0;
                    _audioPreset.stringValue = _audioPresetName[0];
                }

                int result = EditorGUILayout.Popup("预设名", index, _audioPresetName);
                if (result != index)
                {
                    _audioPreset.stringValue = _audioPresetName[result];
                }
                break;

            case UIButton.BehaviorType.Custom:
                EditorGUILayout.PropertyField(_audioCustomResName, new GUIContent("资源名"), true);
                break;
        }
    }
}
