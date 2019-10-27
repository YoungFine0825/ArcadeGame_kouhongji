//编辑器协同
#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public static class EditorCoroutine
{
    private static readonly List<IEnumerator> Datas = new List<IEnumerator>();

    public static void StartCoroutine(IEnumerator enumerator)
    {
        Datas.Add(enumerator);

        if (Datas.Count == 1)
        {
            EditorApplication.update += OnUpdate;
        }
    }

    public static void StopCoroutine(IEnumerator enumerator)
    {
        Datas.RemoveAll(m => m == enumerator);

        if (Datas.Count == 0)
        {
            EditorApplication.update -= OnUpdate;
        }
    }

    public static void StopAllCoroutine()
    {
        Datas.Clear();
        EditorApplication.update -= OnUpdate;
    }

    private static void OnUpdate()
    {
        Datas.RemoveAll(m => !m.MoveNext());

        if (Datas.Count == 0)
        {
            EditorApplication.update -= OnUpdate;
        }
    }
}


#endif