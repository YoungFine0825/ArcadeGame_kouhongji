using UnityEngine;
using UnityEditor;
using JW.Res;
using JW.Common;
using JW.Framework.Asset;

public class AssetMonitorWindow : EditorWindow
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
        if (!AssetService.IsValidate())
        {
            return;
        }

        JW.Framework.Asset.AssetManager am = AssetService.GetInstance().GetAssetManager();
        JWArrayList<BaseAsset> usingList = am.GetUsingAssetList();

        GUILayout.Space(3f);
        GUILayout.BeginVertical();


        // list title
        GUILayout.BeginHorizontal("Table Title", GUILayout.MinHeight(20f));
        GUILayout.Label("Index", _labelStyle, GUILayout.Width(60f));
        GUILayout.Label("RefCnt", _labelStyle, GUILayout.Width(60f));
        GUILayout.Label("OriPath", _labelStyle, GUILayout.Width(120f));
        GUILayout.Label("Name", _labelStyle, GUILayout.Width(60f));
        GUILayout.EndHorizontal();

        // list
        mScroll = GUILayout.BeginScrollView(mScroll);

        int index = 0;
        for(int i=0;i<usingList.Count;i++)
        {
            BaseAsset ba = usingList[i];
            if (ba == null)
            {
                continue;
            }

            index++;

            GUILayout.BeginHorizontal("Table Row", GUILayout.MinHeight(20f));

            // index
            GUILayout.Label(index.ToString(), _labelStyle, GUILayout.Width(60f));

            // ref count
            GUILayout.Label(ba.Resource.RefCnt.ToString(), _labelStyle, GUILayout.Width(60f));

            // tag
            GUILayout.Label(ba.Resource.OriginPath, _labelStyle, GUILayout.Width(120f));

            // path
            GUILayout.Label(ba.Resource.Name, _labelStyle);

            GUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();
        GUILayout.EndVertical();
        GUILayout.Space(3f);
    }
}

