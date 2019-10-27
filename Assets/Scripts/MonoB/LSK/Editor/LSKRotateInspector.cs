/* ============================================================================== 
* 功能描述：LSKRotateInspector 
* 创 建 者：XH 
* 创建日期：2018/11/6 9:57:31 
* ==============================================================================*/

using UnityEngine;
using UnityEditor;

namespace XH.Pin
{


    //[CustomEditor(typeof(LSKRotateComponent))]
    public class LSKRotateInspector : Editor
    {
        private SerializedObject _serObj;
        //SerializedProperty _propSpeed;
        //SerializedProperty _propMinSpeed;
        //SerializedProperty _propMaxSpeed;
        //SerializedProperty _propMinDuration;
        //SerializedProperty _propMaxDuration;
        SerializedProperty _shakeDuration;
        SerializedProperty _shakeStrength;
        SerializedProperty _shakeVibrato;
        SerializedProperty _shakeRandomnees;
        SerializedProperty _shakeSnap;
        SerializedProperty _shakefadeOut;

        private LSKRotateComponent _lskCmp;
        public void OnEnable()
        {
            _lskCmp = target as LSKRotateComponent;
            _serObj = new SerializedObject(target);

            //_propSpeed = _serObj.FindProperty("_speed");
            //_propMinSpeed = _serObj.FindProperty("_minSpeed");
            //_propMaxSpeed = _serObj.FindProperty("_maxSpeed");
            //_propMinDuration = _serObj.FindProperty("_minDuration");
            //_propMaxDuration = _serObj.FindProperty("_maxDuration");
            _shakeDuration = _serObj.FindProperty("_shakeDuration"); 
            _shakeStrength = _serObj.FindProperty("_shakeStrength"); 
            _shakeVibrato = _serObj.FindProperty("_shakeVibrato"); 
            _shakeRandomnees = _serObj.FindProperty("_shakeRandomnees"); 
            _shakeSnap = _serObj.FindProperty("_shakeSnap"); 
            _shakefadeOut = _serObj.FindProperty("_shakefadeOut"); 
        }
        public override void OnInspectorGUI()
        {
            //_serObj.Update();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("震动配置");
            EditorGUILayout.Separator();
            EditorGUILayout.Slider(_shakeDuration, 0.1f, 5f, new GUIContent("Duration"));
            EditorGUILayout.Slider(_shakeStrength, 0f, 100f, new GUIContent("Strength"));
            EditorGUILayout.IntSlider(_shakeVibrato, -100, 300, new GUIContent("Vibrato"));
            EditorGUILayout.Slider(_shakeRandomnees, -400, 400, new GUIContent("Randomnees"));
            _shakeSnap.boolValue = EditorGUILayout.Toggle("Snap", _shakeSnap.boolValue);
            _shakefadeOut.boolValue = EditorGUILayout.Toggle("Fade Out", _shakefadeOut.boolValue);
            //EditorGUILayout.Slider(_propSpeed, -1000f, 1000f, new GUIContent("初速度"));
            //EditorGUILayout.Slider(_propMinSpeed, -5000f, 0f, new GUIContent("最小速度"));
            //EditorGUILayout.Slider(_propMaxSpeed, 0f, 5000f, new GUIContent("最大速度"));
            //EditorGUILayout.Slider(_propMinDuration, 0.1f, 5f, new GUIContent("最小幅度"));
            //EditorGUILayout.Slider(_propMaxDuration, 0.5f, 10f, new GUIContent("最大幅度"));
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            if (GUILayout.Button("震动"))
            {
                //_lskCmp.ReCaculateSpeed();
            }
            //EditorGUILayout.Space();
            //EditorGUILayout.Space();
            //if (GUILayout.Button("作弊"))
            //{
            //    //_lskCmp.Trick();
            //}

            EditorGUILayout.EndVertical();

            _serObj.ApplyModifiedProperties();
        }


    }
}