using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace XH.Pin
{
    //[CustomEditor(typeof(LSKMoveComponent))]
    public class LSKMoveInspector : Editor
    {
        //private LSKMoveComponent _moveCtrl;
        //private SerializedObject _serObj;
        //private SerializedProperty _speedProp;
        //private SerializedProperty _gravityProp;
        //private SerializedProperty _high_pos_min;
        //private SerializedProperty _high_pos_max;
        //private SerializedProperty _min_angle;
        //private SerializedProperty _max_angle;
        //private float _strength=0.15f;
        //private float _duration = 0.15f;
        //private bool _isForward = false;
        private void OnEnable()
        {
            //_moveCtrl = target as LSKMoveComponent;
            //_serObj = new SerializedObject(target);
            //_speedProp = _serObj.FindProperty("_speed");
            //_gravityProp = _serObj.FindProperty("_gravity");

            //_high_pos_min = _serObj.FindProperty("_high_pos_min");
            //_high_pos_max = _serObj.FindProperty("_high_pos_max");
            //_min_angle = _serObj.FindProperty("_min_angle");
            //_max_angle = _serObj.FindProperty("_max_angle");

        }

        public override void OnInspectorGUI()
        {
            //_serObj.Update();
            //EditorGUILayout.Space();
            //EditorGUILayout.BeginVertical("box");
            //GUILayout.Label("移动组件基本配置");
            //_high_pos_min.vector3Value = EditorGUILayout.Vector3Field("最小初速度[Min]", _high_pos_min.vector3Value);
            //_high_pos_max.vector3Value = EditorGUILayout.Vector3Field("最大初速度[Max]", _high_pos_max.vector3Value);
            //_min_angle.vector3Value = EditorGUILayout.Vector3Field("旋转角度[Min]", _min_angle.vector3Value);
            //_max_angle.vector3Value = EditorGUILayout.Vector3Field("旋转角度[Max]", _max_angle.vector3Value);
            //_isForward = EditorGUILayout.Toggle("Foward", _isForward);
            //EditorGUILayout.Slider(_gravityProp, -500, -1);
            //EditorGUILayout.EndVertical();
            //EditorGUILayout.Space();
            //if(GUILayout.Button("抛物线运动"))
            //{
            //    _moveCtrl.DoParabolaMove(_isForward);
            //}
            //if (GUILayout.Button("旋转"))
            //{
            //    _moveCtrl.DORotate();
            //}
            //if (GUILayout.Button("重置"))
            //{
            //    _moveCtrl.ResetParabola();
            //}
            //_serObj.ApplyModifiedProperties();
        }

    }
}
