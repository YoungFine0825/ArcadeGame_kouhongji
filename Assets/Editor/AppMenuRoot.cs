/********************************************************************
	created:	2018-06-07   
	filename: 	JWMenuRoot
	author:		jordenwu
	
	purpose:	菜单根 禁止分散项目扩展菜单
*********************************************************************/
using UnityEngine;
using UnityEditor;
using JW.Editor.Res;
using ICSharpCode.SharpZipLib;

public static class AppMenuRoot {

    [MenuItem("街机/构建/输出Win64Exe")]
    static void BuildWin64Exe()
    {
        Debug.Log("----BuildWin64Exe Begin!-----");
        //资源打包
        FullResBuilder.BuildWinRes();
        //输出EXE
        CommandBuild.BuildWin64Exe();
        //
        Debug.Log("----BuildWin64Exe Done!-----");
    }

    [MenuItem("街机/清理PlayerPrefs")]
    static void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("街机/清理MachineID")]
    static void DeleteMachineId()
    {
        PlayerPrefs.DeleteKey("MachineID");
    }

    [MenuItem("街机/归档自动更新Zip")]
    static void ZipArcadeUpdater()
    {
        string srcDir= Application.dataPath + "/../../Tools/ArcadeUpdater";
        string outZipPath = Application.dataPath + "/../../Tools/ArcadeUpdater.zip";
        ZipHelper.CreateZip(outZipPath, srcDir);
    }
}

public class SetMachineIdForm : EditorWindow
{
    private string _machineId = string.Empty;
    private bool _isSuccessed = false;

    [MenuItem("街机/设置MachineID")]
    static void OpenSetMachineIdForm()
    {
        EditorWindow.GetWindow(typeof(SetMachineIdForm));
    }

    void OnEnable()
    {
        _isSuccessed = false;
        _machineId = PlayerPrefs.GetString("MachineID", "M00000000000");
    }

    void OnGUI()
    {
        GUILayout.BeginVertical();
        //
        GUILayout.Space(20);
        GUI.skin.label.fontSize = 20;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUI.color = Color.white;
        GUILayout.Label("机器ID必须为11位数字");
        GUILayout.Space(30);
        //
        GUILayout.BeginHorizontal();
        GUI.skin.label.alignment = TextAnchor.MiddleLeft;
        GUI.skin.label.fontSize = 10;
        GUILayout.Label("输入11位机器ID");
        _machineId = GUILayout.TextField(_machineId);
        GUILayout.EndHorizontal();
        if (GUILayout.Button("确定"))
        {
            PlayerPrefs.SetString("MachineID", _machineId);
            _isSuccessed = true;
        }
        if (_isSuccessed)
        {
            GUI.color = Color.red;
            GUILayout.Label("设置成功！");
        }
        GUILayout.EndVertical();
    }
}
