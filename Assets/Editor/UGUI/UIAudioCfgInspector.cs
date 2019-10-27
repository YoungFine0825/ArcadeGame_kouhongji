using JW.Common;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;

[CustomEditor(typeof(JW.Framework.UGUI.UIAudioCfg))]
public class UIAudioCfgInspector : Editor
{
    private SerializedProperty _dataProperty;

    private List<Rect> _resNameRect;
    private int _dropIndex = -1;
    private string _dropResName;

    public override void OnInspectorGUI()
    {
        if (OnDrop())
        {
            return;
        }

        serializedObject.Update();
        //判断拖动掉落
        if (_dropIndex >= 0 && !string.IsNullOrEmpty(_dropResName))
        {
            SerializedProperty p = _dataProperty.GetArrayElementAtIndex(_dropIndex);
            p = p.FindPropertyRelative("ResName");
            p.stringValue = _dropResName;
            _dropIndex = -1;
            _dropResName = null;
            //serializedObject.ApplyModifiedProperties();
        }

        //清理
        if (Event.current.type == EventType.Repaint)
        {
            _resNameRect.Clear();
        }

        //界面
        if (EditorGUIHelper.DrawArrayHead(_dataProperty, "音频数据", true))
        {
            _dataProperty.InsertArrayElementAtIndex(_dataProperty.arraySize);   // 添加
        }

        if (_dataProperty.isExpanded)
        {
            int deleteIndex = -1;

            EditorGUILayout.BeginVertical("Box");
            {
                for (int i = 0; i < _dataProperty.arraySize; i++)
                {
                    if (EditorGUIHelper.DrawArrayItemHead(i))
                    {
                        deleteIndex = i;
                    }

                    EditorGUILayout.BeginVertical("box");
                    {
                        DrawData(_dataProperty.GetArrayElementAtIndex(i));
                    }
                    EditorGUILayout.EndVertical();
                }
            }
            EditorGUILayout.EndVertical();

            if (deleteIndex >= 0 && deleteIndex < _dataProperty.arraySize)
            {
                _dataProperty.DeleteArrayElementAtIndex(deleteIndex);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    protected void OnEnable()
    {
        _dataProperty = serializedObject.FindProperty("AudioDatas");
        _resNameRect = new List<Rect>();
    }

    protected void OnDisable()
    {
        _dataProperty = null;
        _resNameRect = null;
    }

    private void DrawData(SerializedProperty data)
    {
        SerializedProperty p = data.FindPropertyRelative("Name");
        EditorGUILayout.PropertyField(p, new GUIContent("名称"));

        EditorGUILayout.BeginHorizontal();
        {
            p = data.FindPropertyRelative("ResName");
            EditorGUILayout.PropertyField(p, new GUIContent("资源名"));

            if (Event.current.type == EventType.Repaint)
            {
                _resNameRect.Add(GUILayoutUtility.GetLastRect());
            }

            if (GUILayout.Button("!", GUILayout.MaxWidth(20f)))
            {
                UnityEngine.Object o = AssetDatabase.LoadMainAssetAtPath("Assets/Resources/" + p.stringValue + ".ogg");
                if (o)
                {
                    EditorGUIUtility.PingObject(o);
                }
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    private bool OnDrop()
    {
        if (Event.current.type == EventType.DragUpdated)
        {
            _dropIndex = -1;
            _dropResName = null;
            //判断是否掉入
            for (int i = 0; i < _resNameRect.Count; i++)
            {
                if (_resNameRect[i].Contains(Event.current.mousePosition))
                {
                    _dropIndex = i;
                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                }
            }
            if (_dropIndex == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        if (Event.current.type == EventType.DragExited)
        {
            if (_dropIndex == -1)
            {
                return false;
            }
            //
            if (DragAndDrop.objectReferences == null || DragAndDrop.objectReferences.Length != 1 ||
               DragAndDrop.objectReferences[0].GetType() != typeof(AudioClip))
            {
                return false;
            }

            string path = AssetDatabase.GetAssetPath(DragAndDrop.objectReferences[0]);
            JW.Common.Log.LogD(path);
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }
            //if (path.IndexOf("Assets/Resources/Audio", StringComparison.Ordinal) != 0)
            //{
            //    return false;
            //}

            path = path.Replace("Assets/Resources/", "");
            path = path.Replace(".ogg", "");
            _dropResName = path;
            JW.Common.Log.LogD(_dropResName);
            return true;
        }

        return false;
    }
}
