using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JW.Editor.Res
{
    public static class LuacBuilder 
    {
        [MenuItem("街机/资源/LuaC 脚本")]
        public static void LuacLuaScripts()
        {
            JW.Common.Log.LogD("------------>LuaC 全部lua 脚本<--------------------");
            string toolPath = Application.dataPath + "/../../Tools/LuaC/build.bat";
            EditorCmdHelper.ProcessCommand(toolPath,"");
            AssetDatabase.Refresh();
        }
    }
}
