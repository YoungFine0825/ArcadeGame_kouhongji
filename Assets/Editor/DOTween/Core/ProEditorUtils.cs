using System;
using UnityEditor;
using UnityEngine;

namespace DG.DOTweenEditor.Core
{
	public static class ProEditorUtils
	{
		public static void AddGlobalDefine(string id)
		{
			bool flag = false;
			foreach (BuildTargetGroup buildTargetGroup in Enum.GetValues(typeof(BuildTargetGroup)))
			{
				if (buildTargetGroup != BuildTargetGroup.Unknown)
				{
					string text = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
					if (!text.Contains(id))
					{
						flag = true;
						text += ((text.Length > 0) ? (";" + id) : id);
						PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, text);
					}
				}
			}
			if (flag)
			{
				Debug.Log("DOTween : added global define " + id);
			}
		}

		public static void RemoveGlobalDefine(string id)
		{
			bool flag = false;
			foreach (BuildTargetGroup buildTargetGroup in Enum.GetValues(typeof(BuildTargetGroup)))
			{
				if (buildTargetGroup != BuildTargetGroup.Unknown)
				{
					string text = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
					if (text.Contains(id))
					{
						flag = true;
						if (text.Contains(id + ";"))
						{
							text = text.Replace(id + ";", "");
						}
						else if (text.Contains(";" + id))
						{
							text = text.Replace(";" + id, "");
						}
						else
						{
							text = text.Replace(id, "");
						}
						PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, text);
					}
				}
			}
			if (flag)
			{
				Debug.Log("DOTween : removed global define " + id);
			}
		}
	}
}
