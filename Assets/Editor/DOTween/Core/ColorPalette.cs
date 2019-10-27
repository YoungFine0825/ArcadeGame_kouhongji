using DG.DemiLib;
using System;
using UnityEngine;

namespace DG.DOTweenEditor.Core
{
	[Serializable]
	public class ColorPalette : DeColorPalette
	{
		[Serializable]
		public class Custom
		{
			public DeSkinColor stickyDivider = new DeSkinColor(Color.black, new Color(0.5f, 0.5f, 0.5f));
		}

		public ColorPalette.Custom custom = new ColorPalette.Custom();
	}
}
