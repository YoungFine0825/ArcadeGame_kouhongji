using DG.DemiEditor;
using System;
using UnityEngine;

namespace DG.DOTweenEditor.Core
{
	public class StylePalette : DeStylePalette
	{
		public class Custom : DeStyleSubPalette
		{
			public GUIStyle stickyToolbar;

			public GUIStyle stickyTitle;

			public GUIStyle warningLabel;

			public override void Init()
			{
				this.stickyToolbar = new GUIStyle(DeGUI.styles.toolbar.flat);
				this.stickyTitle = GUIStyleExtensions.ContentOffsetX(GUIStyleExtensions.MarginBottom(GUIStyleExtensions.Clone(new GUIStyle(GUI.skin.label), new object[]
				{
					FontStyle.Bold,
					11
				}), 0), -2f);
				this.warningLabel = GUIStyleExtensions.Background(GUIStyleExtensions.Add(new GUIStyle(GUI.skin.label), new object[]
				{
					Color.black,
					0
				}), DeStylePalette.orangeSquare, null);
			}
		}

		public readonly StylePalette.Custom custom = new StylePalette.Custom();
	}
}
