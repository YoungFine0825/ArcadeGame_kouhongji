using UnityEngine;
using UnityEditor;
using JW.Res;
using JW.Common;

public class ABMonitorWindow : EditorWindow
{
    Vector2 mScroll = Vector2.zero;
    GUIStyle _labelStyle = new GUIStyle();

    void OnEnable()
    {
        _labelStyle.normal.textColor = Color.white;
        _labelStyle.alignment = TextAnchor.MiddleLeft;
    }

    void OnGUI()
    {
        if (!BundleService.IsValidate())
        {
            return;
        }

        JWObjDictionary<string, BundleRef> bundleDict = BundleService.GetInstance().BundleDict;

        GUILayout.Space(3f);
        GUILayout.BeginVertical();


        // list title
        GUILayout.BeginHorizontal("Table Title", GUILayout.MinHeight(20f));
        GUILayout.Label("Index", _labelStyle, GUILayout.Width(60f));
        GUILayout.Label("RefCnt", _labelStyle, GUILayout.Width(60f));
        GUILayout.Label("Tag", _labelStyle, GUILayout.Width(120f));
        GUILayout.Label("Name", _labelStyle, GUILayout.Width(60f));
        GUILayout.EndHorizontal();

        ResPackConfig config = ResService.GetInstance().PackConfig;
        if (config == null)
        {
            return;
        }

        // list
        mScroll = GUILayout.BeginScrollView(mScroll);

        int index = 0;
        foreach (var kv in bundleDict)
        {
            BundleRef bundle = kv.Value;
            BundlePackInfo pi = config.GetPackInfo(bundle.Path) as BundlePackInfo;
            if (pi == null)
            {
                continue;
            }

            index++;

            GUILayout.BeginHorizontal("Table Row", GUILayout.MinHeight(20f));

            // index
            GUILayout.Label(index.ToString(), _labelStyle, GUILayout.Width(60f));

            // ref count
            GUILayout.Label(bundle.RefCnt.ToString(), _labelStyle, GUILayout.Width(60f));

            // tag
            //GUILayout.Label(pi.Tag, _labelStyle, GUILayout.Width(120f));

            // path
            GUILayout.Label(pi.Path, _labelStyle);

            GUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();
        GUILayout.EndVertical();
        GUILayout.Space(3f);
    }
}

