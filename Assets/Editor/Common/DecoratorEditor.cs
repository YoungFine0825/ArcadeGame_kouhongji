//A base class for creating editors that decorate Unity's built-in editor types.
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
public class DecoratorEditor : UnityEditor.Editor
{
    // empty array for invoking methods using reflection
    private static readonly object[] EmptyArray = new object[0];
    private static readonly Assembly EditorAssembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));

    private readonly System.Type _decoratedEditorType;
    private readonly Dictionary<string, MethodInfo> _decoratedMethods = new Dictionary<string, MethodInfo>();

    private UnityEditor.Editor _editorInstance;

    protected UnityEditor.Editor EditorInstance
    {
        get
        {
            if (_editorInstance == null && targets != null && targets.Length > 0)
            {
                _editorInstance = CreateEditor(targets, _decoratedEditorType);
            }

            if (_editorInstance == null)
            {
                Debug.LogError("Could not create editor !");
            }

            return _editorInstance;
        }
    }

    public DecoratorEditor(string editorTypeName)
    {
        _decoratedEditorType = EditorAssembly.GetTypes().FirstOrDefault(t => t.Name == editorTypeName);
    }

 
    protected void OnDisable()
    {
        if (_editorInstance != null)
        {
            DestroyImmediate(_editorInstance);
            _editorInstance = null;
        }
    }

    /// <summary>
    /// Delegates a method call with the given name to the decorated editor instance.
    /// </summary>
    protected void CallInspectorMethod(string methodName)
    {
        MethodInfo method;

        // Add MethodInfo to cache
        if (!_decoratedMethods.ContainsKey(methodName))
        {
            var flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public;

            method = _decoratedEditorType.GetMethod(methodName, flags);

            if (method != null)
            {
                _decoratedMethods[methodName] = method;
            }
        }
        else
        {
            method = _decoratedMethods[methodName];
        }

        if (method != null)
        {
            method.Invoke(EditorInstance, EmptyArray);
        }
    }

    public void OnSceneGUI()
    {
        CallInspectorMethod("OnSceneGUI");
    }

    protected override void OnHeaderGUI()
    {
        CallInspectorMethod("OnHeaderGUI");
    }

    public override void OnInspectorGUI()
    {
        EditorInstance.OnInspectorGUI();
    }

    public override void DrawPreview(Rect previewArea)
    {
        EditorInstance.DrawPreview(previewArea);
    }

    public override string GetInfoString()
    {
        return EditorInstance.GetInfoString();
    }

    public override GUIContent GetPreviewTitle()
    {
        return EditorInstance.GetPreviewTitle();
    }

    public override bool HasPreviewGUI()
    {
        return EditorInstance.HasPreviewGUI();
    }

    public override void OnInteractivePreviewGUI(Rect r, GUIStyle background)
    {
        EditorInstance.OnInteractivePreviewGUI(r, background);
    }

    public override void OnPreviewGUI(Rect r, GUIStyle background)
    {
        EditorInstance.OnPreviewGUI(r, background);
    }

    public override void OnPreviewSettings()
    {
        EditorInstance.OnPreviewSettings();
    }

    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        return EditorInstance.RenderStaticPreview(assetPath, subAssets, width, height);
    }

    public override bool RequiresConstantRepaint()
    {
        return EditorInstance.RequiresConstantRepaint();
    }

    public override bool UseDefaultMargins()
    {
        return EditorInstance.UseDefaultMargins();
    }
}

