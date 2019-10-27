using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(JW.Framework.UGUI.UICurveCfg))]
public class UICurveCfgInspector : Editor
{
    private struct DataProperty
    {
        public SerializedProperty Name;
        public SerializedProperty Curve;
        //public SerializedProperty HyperbolaCurve;
    }

    private SerializedProperty _dataProperty;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // 数据
        if (EditorGUIHelper.DrawArrayHead(_dataProperty, "数据", true))
        {
            _dataProperty.InsertArrayElementAtIndex(_dataProperty.arraySize);   // 添加
        }

        int deleteIndex = -1;
        if (_dataProperty.isExpanded)
        {
            EditorGUILayout.BeginVertical("Box");
            {
                for (int i = 0; i < _dataProperty.arraySize; i++)
                {
                    DataProperty data;
                    GetDataProperty(_dataProperty.GetArrayElementAtIndex(i), out data);

                    if (EditorGUIHelper.DrawArrayItemHead(i, GetDataHead(ref data), Color.white))
                    {
                        deleteIndex = i;
                    }

                    DrawData(ref data);
                }
            }
            EditorGUILayout.EndVertical();
        }

        if (deleteIndex >= 0 && deleteIndex < _dataProperty.arraySize)
        {
            _dataProperty.DeleteArrayElementAtIndex(deleteIndex);
        }

        serializedObject.ApplyModifiedProperties();
    }

    protected void OnEnable()
    {
        _dataProperty = serializedObject.FindProperty("CurveDatas");
    }

    protected void OnDisable()
    {
        _dataProperty = null;
    }

    private string GetDataHead(ref DataProperty data)
    {
        float duration = 0f;

        AnimationCurve curve = data.Curve.animationCurveValue;
        if (curve != null && curve.length >= 2)
        {
            duration = curve[curve.length - 1].time;
        }

        string title = string.Format("时长：{0}", duration);

        //
        //duration = 0f;
        //curve = data.HyperbolaCurve.animationCurveValue;
        //if (curve != null && curve.length >= 2)
        //{
        //    duration = curve[curve.length - 1].time;
        //}

        //title += string.Format(" 双曲线时长：{0}", duration);

        return title;
    }

    private void DrawData(ref DataProperty data)
    {
        EditorGUILayout.BeginVertical("Box");
        EditorGUI.indentLevel += 1;
        {
            EditorGUILayout.PropertyField(data.Name, new GUIContent("名称"));

            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.PropertyField(data.Curve, new GUIContent("曲线"));

                if (GUILayout.Button("复制"))
                {
                    CopyCurve(data.Curve.animationCurveValue);
                }

                if (GUILayout.Button("粘帖"))
                {
                    PasteCurve(data.Curve);
                }
            }
            EditorGUILayout.EndHorizontal();

            //EditorGUILayout.BeginHorizontal();
            //{
            //    EditorGUILayout.PropertyField(data.HyperbolaCurve, new GUIContent("双曲线"));

            //    if (GUILayout.Button("复制"))
            //    {
            //        CopyCurve(data.HyperbolaCurve.animationCurveValue);
            //    }

            //    if (GUILayout.Button("粘帖"))
            //    {
            //        PasteCurve(data.HyperbolaCurve);
            //    }
            //}
            //EditorGUILayout.EndHorizontal();
        }
        EditorGUI.indentLevel -= 1;
        EditorGUILayout.EndVertical();
    }

    private void GetDataProperty(SerializedProperty prop, out DataProperty dataProperty)
    {
        dataProperty.Name = prop.FindPropertyRelative("Name");
        dataProperty.Curve = prop.FindPropertyRelative("Curve");
        //dataProperty.HyperbolaCurve = prop.FindPropertyRelative("HyperbolaCurve");
    }

    private void CopyCurve(AnimationCurve curve)
    {
        if (curve == null || curve.length < 2)
        {
            return;
        }

        string content = string.Empty;
        for (int i = 0; i < curve.length; i++)
        {
            Keyframe kf = curve[i];

            content += string.Format("Keyframe({0:f}, {1:f}, {2:f}, {3:f})", kf.time, kf.value, kf.inTangent, kf.outTangent);
        }

        Debug.Log(content);

        TextEditor te = new TextEditor();
        te.text = content;
        te.OnFocus();
        te.Copy();
    }

    private void PasteCurve(SerializedProperty curveProperty)
    {
        TextEditor te = new TextEditor();
        te.OnFocus();
        te.Paste();
        string content = te.text;

        List<float> keyFrames = new List<float>();

        Regex keyFrameRegex = new Regex(@"Keyframe\(([-\d\., ]+)\)", RegexOptions.IgnoreCase);
        Regex valueRegex = new Regex(@"([-\d\.]+)", RegexOptions.IgnoreCase);

        MatchCollection keyFrameMc = keyFrameRegex.Matches(content);
        for (int i = 0; i < keyFrameMc.Count; i++)
        {
            MatchCollection mc = valueRegex.Matches(keyFrameMc[i].Value);
            for (int j = 0; j < mc.Count; j++)
            {
                float f;
                if (float.TryParse(mc[j].Value, out f))
                {
                    keyFrames.Add(f);
                }
            }
        }

        if (keyFrames.Count < 8 || keyFrames.Count % 4 != 0)
        {
            return;
        }

        AnimationCurve curve = new AnimationCurve();
        for (int i = 0; i < keyFrames.Count; i += 4)
        {
            curve.AddKey(new Keyframe(keyFrames[i], keyFrames[i + 1], keyFrames[i + 2], keyFrames[i + 3]));
        }

        curveProperty.animationCurveValue = curve;
    }
}
