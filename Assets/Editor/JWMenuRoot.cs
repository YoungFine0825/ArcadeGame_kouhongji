/********************************************************************
	created:	2018-06-07   
	filename: 	JWMenuRoot
	author:		jordenwu
	
	purpose:	菜单根 禁止分散项目扩展菜单
*********************************************************************/
using UnityEngine;
using UnityEditor;
using JW.Editor.Res;

public static class JWStudioMenuRoot {


    [MenuItem("JWStudio/AssetBundle监视器")]
    static void OpenBundleMonitorWindow()
    {
        ABMonitorWindow window = UnityEditor.EditorWindow.GetWindow<ABMonitorWindow>();
        window.titleContent = new GUIContent("资源包监视");
    }

    [MenuItem("JWStudio/AssetService监视器")]
    static void OpenAssetMonitorWindow()
    {
        AssetMonitorWindow window = UnityEditor.EditorWindow.GetWindow<AssetMonitorWindow>();
        window.titleContent = new GUIContent("资产监视");
    }


    [MenuItem("JWStudio/清理PlayerPrefs")]
    static void CleanPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

}
