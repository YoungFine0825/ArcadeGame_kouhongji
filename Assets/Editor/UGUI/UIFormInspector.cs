using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using JW.Framework.UGUI;

[CustomEditor(typeof(UIForm))]
public class UIFormInspector : UIFormLinkInspector
{
    public new void OnEnable()
    {
        base.OnEnable();
    }

    //private bool _audioExpand = false;
    private bool _genExpand = false;
    //private bool _aniExpand = false;
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.BeginVertical("box");
        SerializedProperty sp = serializedObject.FindProperty("ReferenceResolution");
        // EditorGUILayout.Vector2Field("设计分辨率", sp.vector2Value);
        // sp = serializedObject.FindProperty("IsSingleton");
        EditorGUILayout.PropertyField(sp, new GUIContent("设计分辨率"));
        // EditorGUI.EndDisabledGroup();

        sp = serializedObject.FindProperty("IsSingleton");
        EditorGUILayout.PropertyField(sp, new GUIContent("是否单例"));

        sp = serializedObject.FindProperty("IsModal");
        EditorGUILayout.PropertyField(sp, new GUIContent("是否模态"));

        sp = serializedObject.FindProperty("ShowPriority");
        EditorGUILayout.PropertyField(sp, new GUIContent("显示优先级"));

        sp = serializedObject.FindProperty("GroupId");
        EditorGUILayout.PropertyField(sp, new GUIContent("显示组"));

        sp = serializedObject.FindProperty("IsFullScreenBG");
        EditorGUILayout.PropertyField(sp, new GUIContent("全屏背景"));

        sp = serializedObject.FindProperty("IsDisableInput");
        EditorGUILayout.PropertyField(sp, new GUIContent("禁止输入"));

        sp = serializedObject.FindProperty("IsHideUnderForms");
        EditorGUILayout.PropertyField(sp, new GUIContent("隐藏下面Form"));

        sp = serializedObject.FindProperty("IsAlwaysKeepVisible");
        EditorGUILayout.PropertyField(sp, new GUIContent("始终保持可见"));

        EditorGUILayout.EndVertical();
        /*
        _audioExpand=EditorGUIHelper.DrawHead("音效", _audioExpand);
        if (_audioExpand)
        {
            //todo 拖动填充
            GUILayout.BeginVertical("box");
            {
                sp = serializedObject.FindProperty("OpenedWwiseEvents");
                EditorGUIHelper.ArrayField(sp, "打开");
                sp = serializedObject.FindProperty("ClosedWwiseEvents");
                EditorGUIHelper.ArrayField(sp, "关闭");
            }
            EditorGUILayout.EndVertical();
        }
        */
        /*
        _aniExpand = EditorGUIHelper.DrawHead("动画", _aniExpand);
        if (_aniExpand)
        {
            //todo 拖动填充
            GUILayout.BeginVertical("box");
            {
                sp = serializedObject.FindProperty("FadeInAnimationType");
                EditorGUILayout.PropertyField(sp, new GUIContent("FadeIn动画类型"));
                sp = serializedObject.FindProperty("FadeInAnimationName");
                EditorGUILayout.PropertyField(sp, new GUIContent("FadeIn动画名称"));

                sp = serializedObject.FindProperty("FadeOutAnimationType");
                EditorGUILayout.PropertyField(sp, new GUIContent("FadeOut动画类型"));
                sp = serializedObject.FindProperty("FadeOutAnimationName");
                EditorGUILayout.PropertyField(sp, new GUIContent("FadeOut动画名称"));
            }
            EditorGUILayout.EndVertical();
        }*/

        //连接元素
        base.OnDrawElements();

        //
        serializedObject.ApplyModifiedProperties();

        //添加自动生成
        _genExpand = EditorGUIHelper.DrawHead("辅助", _genExpand);
        if (_genExpand)
        {
            GUILayout.BeginVertical("box");
            {
                EditorGUI.BeginDisabledGroup(true);
                _prefix = EditorGUILayout.TextField("引用前缀", _prefix);
                EditorGUI.EndDisabledGroup();

                if (GUILayout.Button("复制节点定义"))
                {
                    UIFormLink pl = target as UIFormLink;
                    CopyElementDeclare(pl);
                }
                if (GUILayout.Button("复制节点初始化"))
                {
                    UIFormLink pl = target as UIFormLink;
                    CopyElementInitialize(pl);
                }
                if (GUILayout.Button("复制节点反初始化"))
                {
                    UIFormLink pl = target as UIFormLink;
                    CopyElementUnIninitialize(pl);
                }

                if (GUILayout.Button("复制Lua节点定义"))
                {
                    UIFormLink pl = target as UIFormLink;
                    CopyElementDeclareLua(pl);
                }

                if (GUILayout.Button("复制Lua节点初始化"))
                {
                    UIFormLink pl = target as UIFormLink;
                    CopyElementInitializeLua(pl);
                }

                if (GUILayout.Button("复制Lua节点反初始化"))
                {
                    UIFormLink pl = target as UIFormLink;
                    CopyElementUnIninitializeLua(pl);
                }

                if (GUILayout.Button("复制Lua全部初始化代码"))
                {
                    UIFormLink pl = target as UIFormLink;
                    CopyElementWholeLua(pl);
                }

            }
            EditorGUILayout.EndVertical();
        }
    }

}