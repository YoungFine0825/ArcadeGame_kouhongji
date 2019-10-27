#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class EditorGUIHelper
{
    public const string SubGroupStyle = "ObjectFieldThumb";

    private static GUIContent addButtonContent = new GUIContent("+", "add element");

    /// <summary>
    /// 绘制标题
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="expand">是否呈现打开状态</param>
    /// <returns>是否打开</returns>
    public static bool DrawHead(string title, bool expand)
    {
        GUILayout.Space(5f);

        Color oldColor = GUI.backgroundColor;
        if (!expand)
        {
            GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f);
        }

        title = string.Format(expand ? "\u25BC <b><size=11>{0}</size></b>" : "\u25BA <b><size=11>{0}</size></b>", title);

        GUILayout.BeginHorizontal();
        {
            if (!GUILayout.Toggle(true, title, "dragtab", GUILayout.MinWidth(20f)))
            {
                expand = !expand;
            }
        }
        GUILayout.EndHorizontal();

        GUI.backgroundColor = oldColor;
        GUILayout.Space(expand ? 2f : 5f);

        return expand;
    }

    /// <summary>
    /// 绘制数组标题
    /// </summary>
    /// <param name="prop">数组属性</param>
    /// <param name="title">标题</param>
    /// <param name="showAddButton">是否显示添加</param>
    /// <returns>是否点击了添加</returns>
    public static bool DrawArrayHead(SerializedProperty prop, string title, bool showAddButton)
    {
        bool toggleAddButton = false;

        GUILayout.Space(5f);

        Color oldColor = GUI.backgroundColor;
        if (!prop.isExpanded)
        {
            GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f);
        }

        title = string.Format(prop.isExpanded ? "\u25BC <b><size=11>{0}</size></b> - 数量：{1}" : "\u25BA <b><size=11>{0}</size></b> - 数量：{1}", title, prop.arraySize);

        GUILayout.BeginHorizontal();
        {
            if (!GUILayout.Toggle(true, title, "dragtab", GUILayout.MinWidth(20f)))
            {
                prop.isExpanded = !prop.isExpanded;
            }

            if (showAddButton)
            {
                toggleAddButton = GUILayout.Button("+", "dragtab", GUILayout.Width(22f));
            }
        }
        GUILayout.EndHorizontal();

        GUI.backgroundColor = oldColor;
        GUILayout.Space(prop.isExpanded ? 2f : 5f);

        return toggleAddButton;
    }

    /// <summary>
    /// 绘制数组中数据项标题
    /// </summary>
    /// <param name="index">索引</param>
    /// <param name="title">标题，可以为null标识没有额外的标题</param>
    /// <param name="titleColor">标题颜色</param>
    /// <returns>是否删除数据项</returns>
    public static bool DrawArrayItemHead(int index, string title = null, Color titleColor = default(Color))
    {
        bool del = false;

        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("×", "dragtab", GUILayout.Width(22f)))
            {
                del = true; // 删除
            }

            EditorGUILayout.LabelField(string.Format("索引：{0:D2}", index), GUILayout.Width(50f));

            if (!string.IsNullOrEmpty(title))
            {
                GUI.color = titleColor;
                EditorGUILayout.LabelField(title);
                GUI.color = Color.white;
            }
        }
        EditorGUILayout.EndHorizontal();

        return del;
    }

    /// <summary>
    /// 绘制数组中数据项标题
    /// </summary>
    /// <param name="index">索引</param>
    /// <param name="delCall">删除回调</param>
    /// <param name="addCall">添加回调</param>
    /// <param name="title">标题</param>
    /// <param name="expand">是否呈现打开状态</param>
    /// <returns>是否打开</returns>
    public static bool DrawHead(int index, Action<int> delCall, Action<int> addCall, string title, bool expand)
    {
        GUILayout.Space(5f);

        Color oldColor = GUI.backgroundColor;
        if (!expand)
        {
            GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f);
        }

        title = string.Format(expand ? "\u25BC <b><size=11>{0}</size></b>" : "\u25BA <b><size=11>{0}</size></b>", title);

        GUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("×", "dragtab", GUILayout.Width(22f)))
            {
                if (delCall != null)
                {
                    delCall(index);
                }
            }

            if (GUILayout.Button("+", "dragtab", GUILayout.Width(22f)))
            {
                if (addCall != null)
                {
                    addCall(index);
                }
            }

            if (!GUILayout.Toggle(true, title, "dragtab", GUILayout.MinWidth(20f)))
            {
                expand = !expand;
            }
        }
        GUILayout.EndHorizontal();

        GUI.backgroundColor = oldColor;
        GUILayout.Space(expand ? 2f : 5f);

        return expand;
    }

    public static T[] AddElement<T>(T[] elements, T element, int index = -1)
    {
        List<T> elementsList = new List<T>(elements);
        if (index == -1 || index >= elements.Length)
        {
            elementsList.Add(element);
        }
        else
        {
            elementsList.Insert(index, element);
        }
        return elementsList.ToArray();
    }

    public static T[] RemoveElement<T>(T[] elements, T element)
    {
        List<T> elementsList = new List<T>(elements);
        elementsList.Remove(element);
        return elementsList.ToArray();
    }

    public static T[] CopyElement<T>(T[] source, int count)
    {
        if (source == null)
        {
            source = new T[0];
        }
        T[] arr = new T[count];
        if (source == null)
        {
            source = new T[count];
        }

        for (int i = 0, imax = Mathf.Min(count, source.Length); i < imax; i++)
        {
            arr[i] = source[i];
        }

        return arr;
    }

    public static bool ArrayField(SerializedProperty list,string desc)
    {
        if (list == null || !list.isArray)
        {
            return false;
        }
        EditorGUILayout.PropertyField(list,new GUIContent(desc));

        if (list.isExpanded)
        {
            SerializedProperty size = list.FindPropertyRelative("Array.size");
            if (size.hasMultipleDifferentValues)
            {
                EditorGUILayout.HelpBox("Not showing lists with different sizes.", MessageType.Info);
            }
            else
            {
                EditorGUI.indentLevel += 1;
                EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
                for (int i = 0, imax = list.arraySize; i < imax; i++)
                {
                    EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), true);
                }
                EditorGUI.indentLevel -= 1;

                if (list.arraySize == 0 && GUILayout.Button(addButtonContent, EditorStyles.miniButton))
                {
                    list.arraySize++;
                }
            }

            return true;
        }
        return false;
    }
}


#endif