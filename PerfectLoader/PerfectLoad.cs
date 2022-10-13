using System;
using UnityEngine;

namespace PerfectLoader
{
	// Token: 0x02000048 RID: 72
	public class PerfectLoad
	{
		// Token: 0x060001F1 RID: 497 RVA: 0x00014128 File Offset: 0x00012328
		public static void Load()
		{
			GameObject gameObject = new GameObject();
			gameObject.AddComponent<HackManager>();
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
		}
	}
}
