/********************************************************************
	created:	2018-06-14
	filename: 	PrefabLinkInspector
	author:		jordenwu
	
	purpose:	预制件连接 查看
*********************************************************************/
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using JW.PLink;
using System;

[CustomEditor(typeof(PrefabLink))]
public class PrefabLinkInspector : UnityEditor.Editor
{
    //界面使用
    class ElementInspectorData
    {
        public int CurrentType;
        public List<System.Type> TypeList;
        public List<string> TypeNameList;
    }

    //辅助生成元素数据
    private struct ElementData
    {
        public string IndexName;
        public string TypeName;
        public string VariantName;
    }

    List<ElementInspectorData> _elemetList = new List<ElementInspectorData>();

    void OnEnable()
    {
        InitElementData();
    }

    private static string _prefix = "t";
    private static bool _expand = false;
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        //绘制元素列表
        OnDrawElements();
        serializedObject.ApplyModifiedProperties();
        //添加自动生成
        _expand = EditorGUIHelper.DrawHead("辅助", _expand);
        if (_expand)
        {
            GUILayout.BeginVertical("box");
            {
                _prefix = EditorGUILayout.TextField("引用前缀", _prefix);
                if (GUILayout.Button("复制节点定义"))
                {
                    PrefabLink pl = target as PrefabLink;
                    CopyElementDeclare(pl);
                }
                if (GUILayout.Button("复制节点初始化"))
                {
                    PrefabLink pl = target as PrefabLink;
                    CopyElementInitialize(pl);
                }
                if (GUILayout.Button("复制节点反初始化"))
                {
                    PrefabLink pl = target as PrefabLink;
                    CopyElementUnIninitialize(pl);
                }
                // if (GUILayout.Button("复制Lua节点定义"))
                // {
                //     PrefabLink pl = target as PrefabLink;
                //     CopyElementDeclareLua(pl);
                // }
                if (GUILayout.Button("复制Lua节点初始化"))
                {
                    PrefabLink pl = target as PrefabLink;
                    CopyElementInitializeLua(pl);
                }
                // if (GUILayout.Button("复制Lua节点反初始化"))
                // {
                //     PrefabLink pl = target as PrefabLink;
                //     CopyElementUnIninitializeLua(pl);
                // }
            }
        }
    }

    /// <summary>
    /// 生成连接元素声明代码
    /// </summary>
    /// <param name="link"></param>
    private static void CopyElementDeclare(PrefabLink link)
    {
        if (link == null)
        {
            return;
        }
        ElementData[] elementData = GetElementData(link);
        if (elementData == null)
        {
            return;
        }
        // 输出
        string content = string.Empty;
        for (int i = 0; i < elementData.Length; i++)
        {
            content += "private const int " + elementData[i].IndexName + " = " + i + ";\r\n";
        }

        content += "\r\n";
        for (int i = 0; i < elementData.Length; i++)
        {
            content += "private " + elementData[i].TypeName + ' ' + elementData[i].VariantName + ";\r\n";
        }
        if (!string.IsNullOrEmpty(content))
        {
            TextEditor te = new TextEditor();
            te.text = content;
            te.OnFocus();
            te.Copy();
        }
    }

    /// <summary>
    /// 生成连接元素初始化代码
    /// </summary>
    /// <param name="link"></param>
    private static void CopyElementInitialize(PrefabLink link)
    {
        if (link == null)
        {
            return;
        }
        ElementData[] elementData = GetElementData(link);
        if (elementData == null)
        {
            return ;
        }
        // 输出
        string content = string.Empty;

        for (int i = 0; i < elementData.Length; i++)
        {
            ElementData data = elementData[i];

            content += data.VariantName + " = ";

            if (data.TypeName == "GameObject")
            {
                content += "$$.GetCacheGameObject(";
                content += data.IndexName;
                content += ");\r\n";
            }
            else
            {
                content += "$$.GetCacheComponent(";
                content += data.IndexName;
                content += ")";
                content += "as " + data.TypeName;
                content += ";\r\n";
            }
        }
        if (!string.IsNullOrEmpty(content))
        {
            TextEditor te = new TextEditor();
            te.text = content;
            te.OnFocus();
            te.Copy();
        }
    }
    
    /// <summary>
    /// 生成反初始化代码
    /// </summary>
    /// <param name="prefab"></param>
    private static void CopyElementUnIninitialize(PrefabLink link)
    {
        if (link == null)
        {
            return;
        }
        ElementData[] elementData = GetElementData(link);
        if (elementData == null)
        {
            return;
        }
        // 输出
        string content = string.Empty;
        for (int i = 0; i < elementData.Length; i++)
        {
            ElementData data = elementData[i];
            content += data.VariantName + " = null;\r\n";
        }
        if (!string.IsNullOrEmpty(content))
        {
            TextEditor te = new TextEditor();
            te.text = content;
            te.OnFocus();
            te.Copy();
        }
    }


    /// <summary>
    /// 生成连接元素声明代码
    /// </summary>
    /// <param name="link"></param>
    private static void CopyElementDeclareLua(PrefabLink link)
    {
        // if (link == null)
        // {
        //     return;
        // }
        // ElementData[] elementData = GetElementData(link);
        // if (elementData == null)
        // {
        //     return;
        // }
        // // 输出
        // string content = string.Empty;
        // for (int i = 0; i < elementData.Length; i++)
        // {
        //     content += "self." + elementData[i].IndexName + " = " + i + ";\r\n";
        // }
        // content += "\r\n";
        // for (int i = 0; i < elementData.Length; i++)
        // {
        //     content += "self."+ elementData[i].VariantName +" = false" + "\r\n"; 
        // }
        // if (!string.IsNullOrEmpty(content))
        // {
        //     TextEditor te = new TextEditor();
        //     te.text = content;
        //     te.OnFocus();
        //     te.Copy();
        // }
    }

    /// <summary>
    /// 生成连接元素初始化代码
    /// </summary>
    /// <param name="link"></param>
    private static void CopyElementInitializeLua(PrefabLink link)
    {
        if (link == null)
        {
            return;
        }
        ElementData[] elementData = GetElementData(link);
        if (elementData == null)
        {
            return ;
        }
        // 输出
        string content = string.Empty;

        for (int i = 0; i < elementData.Length; i++)
        {
            ElementData data = elementData[i];

            content += "local "+ elementData[i].VariantName+" = ";

            if (data.TypeName == "GameObject")
            {
                content += string.Format("itemLink:GetCacheGameObject({0})\r\n", i);
            }
            else
            {
                content += string.Format("itemLink:GetCacheComponent({0})\r\n", i);
            }
        }
        if (!string.IsNullOrEmpty(content))
        {
            TextEditor te = new TextEditor();
            te.text = content;
            te.OnFocus();
            te.Copy();
        }
    }
    
    /// <summary>
    /// 生成反初始化代码
    /// </summary>
    /// <param name="prefab"></param>
    private static void CopyElementUnIninitializeLua(PrefabLink link)
    {
        // if (link == null)
        // {
        //     return;
        // }
        // ElementData[] elementData = GetElementData(link);
        // if (elementData == null)
        // {
        //     return;
        // }
        // // 输出
        // string content = string.Empty;
        // for (int i = 0; i < elementData.Length; i++)
        // {
        //     content += "self."+ elementData[i].VariantName +" = false" + "\r\n"; 
        // }
        // if (!string.IsNullOrEmpty(content))
        // {
        //     TextEditor te = new TextEditor();
        //     te.text = content;
        //     te.OnFocus();
        //     te.Copy();
        // }
    }

    private static ElementData[] GetElementData(PrefabLink prefab)
    {
        if (prefab == null || prefab.Elements == null || prefab.Elements.Length == 0)
        {
            EditorUtility.DisplayDialog("错误", "没有节点", "确定");
            return null;
        }

        ElementData[] elementData = new ElementData[prefab.Elements.Length];

        for (int i = 0; i < prefab.Elements.Length; i++)
        {
            Component component = prefab.Elements[i].Component;
            GameObject go = component.gameObject;
            string goName = go.name;
            if (string.IsNullOrEmpty(goName))
            {
                EditorUtility.DisplayDialog("错误", "节点名为空", "确定");
                return null;
            }

            goName = goName.Trim();
            if (string.IsNullOrEmpty(goName))
            {
                EditorUtility.DisplayDialog("错误", "节点名为空", "确定");
                return null;
            }

            // 索引
            ElementData data;

            // 类型和变量
            data.TypeName = component.GetType().Name;
            string shortTypeName = data.TypeName;
            if (data.TypeName.IndexOf("UI") == 0)
            {
                shortTypeName = data.TypeName.Substring(2);
            }

            // index
            data.IndexName = _prefix + char.ToUpper(goName[0]) + goName.Substring(1) + shortTypeName + "Index";

            // variant name
            if (string.IsNullOrEmpty(_prefix))
            {
                data.VariantName = "_" + char.ToLower(goName[0]) + goName.Substring(1) + shortTypeName;
            }
            else
            {
                data.VariantName = _prefix + char.ToUpper(goName[0]) + goName.Substring(1) + shortTypeName;
            }

            // 名字重复
            for (int j = 0; j < i; j++)
            {
                if (elementData[j].IndexName.Equals(data.IndexName, StringComparison.OrdinalIgnoreCase) ||
                    elementData[j].VariantName.Equals(data.VariantName, StringComparison.OrdinalIgnoreCase))
                {
                    EditorUtility.DisplayDialog(
                        "错误",
                        string.Format("节点名重复,{0},{1}", data.IndexName, data.VariantName),
                        "确定");
                    return null;
                }
            }

            //
            elementData[i] = data;
        }

        return elementData;
    }

    #region GUI
    /// <summary>
    /// draw element list
    /// </summary>
    void OnDrawElements()
    {
        SerializedProperty elements = serializedObject.FindProperty("Elements");
        if (elements == null)
        {
            return;
        }
        if (EditorGUIHelper.DrawArrayHead(elements, "连接节点列表", true))
        {
            // add element
            AddElement(elements);
        }

        int deleteIndex = -1;
        if (elements.isExpanded)
        {
            EditorGUILayout.BeginVertical("box");
            {
                for (int i = 0; i < _elemetList.Count; i++)
                {
                    SerializedProperty element = elements.GetArrayElementAtIndex(i);

                    if (EditorGUIHelper.DrawArrayItemHead(i))
                    {
                        deleteIndex = i;
                    }

                    EditorGUILayout.BeginHorizontal();
                    {
                        // component type popup
                        int selected = EditorGUILayout.Popup(_elemetList[i].CurrentType, _elemetList[i].TypeNameList.ToArray());
                        if (selected != _elemetList[i].CurrentType)
                        {
                            // change current type
                            _elemetList[i].CurrentType = selected;

                            // update property
                            OnChangeElementType(element, _elemetList[i].TypeList[selected]);
                        }

                        // object
                        EditorGUI.BeginChangeCheck();
                        EditorGUILayout.PropertyField(element.FindPropertyRelative("Component"), GUIContent.none);
                        if (EditorGUI.EndChangeCheck())
                        {
                            UpdateElement(elements, i);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndVertical();
        }

        if (deleteIndex >= 0 && deleteIndex < elements.arraySize)
        {
            //elements.GetArrayElementAtIndex(deleteIndex).objectReferenceValue = null;
            elements.DeleteArrayElementAtIndex(deleteIndex);

            _elemetList.RemoveAt(deleteIndex);
        }
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    void InitElementData()
    {
        _elemetList.Clear();

        SerializedProperty property = serializedObject.FindProperty("Elements");
        for (int i = 0; i < property.arraySize; i++)
        {
            SerializedProperty element = property.GetArrayElementAtIndex(i);
            InsertElementData(element);
        }
    }

    /// <summary>
    /// 添加节点
    /// </summary>
    /// <param name="elements"></param>
    void AddElement(SerializedProperty elements)
    {
        // insert into property
        elements.InsertArrayElementAtIndex(elements.arraySize);

        // adjust default property
        SerializedProperty element = elements.GetArrayElementAtIndex(elements.arraySize - 1);
        AdjustElementProperty(element);

        // insert into element list (Editor Use.)
        InsertElementData(element);
    }

    /// <summary>
    /// 更新序列化数据
    /// </summary>
    /// <param name="elements"></param>
    /// <param name="index"></param>
    void UpdateElement(SerializedProperty elements, int index)
    {
        if (index < 0 || index >= elements.arraySize)
        {
            return;
        }

        // adjust default property
        SerializedProperty element = elements.GetArrayElementAtIndex(index);
        AdjustElementProperty(element);

        // remove old element data and insert a new one into the element list (Editor Use.)
        _elemetList.RemoveAt(index);
        InsertElementData(element, index);
    }

    /// <summary>
    /// 修改element类型
    /// </summary>
    /// <param name="element"></param>
    /// <param name="type"></param>
    void OnChangeElementType(SerializedProperty element, System.Type type)
    {
        SerializedProperty objProperty = element.FindPropertyRelative("Component");
        UnityEngine.Object obj = objProperty.objectReferenceValue;
        if (obj == null || obj is GameObject)
        {
            Debug.LogError("Changing element type which is a GameObject.");
            return;
        }

        Component component = (obj as Component).GetComponent(type);
        if (component != null)
        {
            objProperty.objectReferenceValue = component;
        }
    }

    /// <summary>
    /// 插入element数据 (非序列化数据)
    /// </summary>
    /// <param name="element"></param>
    /// <param name="index"></param>
    void InsertElementData(SerializedProperty element, int index = -1)
    {
        ElementInspectorData data = new ElementInspectorData();
        data.CurrentType = 0;
        data.TypeList = new List<System.Type>();
        data.TypeNameList = new List<string>();

        UnityEngine.Object obj = element.FindPropertyRelative("Component").objectReferenceValue;
        if (obj is Component)
        {
            Component[] components = (obj as Component).GetComponents<Component>();
            if (components == null || components.Length == 0)
            {
                Debug.LogError("Inserting element data with an element who's Component is not type of [Component].");
                return;
            }

            for (int i = 0; i < components.Length; i++)
            {
                Component c = components[i];
                if (c == null)
                {
                    Debug.LogError("the element script missing objectName:" + obj.name);
                    continue;
                }

                data.TypeList.Add(c.GetType());
                data.TypeNameList.Add(c.GetType().Name);
            }

            // current type
            for (int i = 0; data.TypeList != null && i < data.TypeList.Count; i++)
            {
                if (obj.GetType() == data.TypeList[i])
                {
                    data.CurrentType = i;
                    break;
                }
            }
        }

        index = (index == -1) ? _elemetList.Count : index;
        _elemetList.Insert(index, data);
    }

    /// <summary>
    /// 修改序列化对象，换成第一个非Transform的Component
    /// </summary>
    /// <param name="element"></param>
    void AdjustElementProperty(SerializedProperty element)
    {
        Component[] components = null;
        UnityEngine.Object obj = element.FindPropertyRelative("Component").objectReferenceValue;
        if (obj is GameObject)
        {
            components = (obj as GameObject).GetComponents<Component>();
        }
        else if (obj is Component)
        {
            components = (obj as Component).GetComponents<Component>();
        }

        Component component = DefaultComponent(components);
        if (component == null)
        {
            return;
        }

        element.FindPropertyRelative("Component").objectReferenceValue = component;
    }

    /// <summary>
    /// 选择默认类型，默认使用Transform外的第一个脚本，如果没有就选择Transform
    /// </summary>
    /// <param name="components"></param>
    /// <returns></returns>
    Component DefaultComponent(Component[] components)
    {
        if (components == null)
        {
            return null;
        }

        Component component = null;
        for (int i = 0; i < components.Length; i++)
        {
            component = components[i];
            if (component.GetType() != typeof(Transform))
            {
                break;
            }
        }

        return component;
    }
    #endregion
}

