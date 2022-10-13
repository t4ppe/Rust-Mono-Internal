using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000008 RID: 8
public class Expl : MonoBehaviour
{
	// Token: 0x06000018 RID: 24 RVA: 0x00004EA4 File Offset: 0x000030A4
	public void OnDDraw()
	{
		foreach (Vector3 vector in new List<Vector3>(Expl.MarkersExplosive))
		{
			float num = Vector3.Distance(Expl.LocalPlayer.Position, vector);
			bool flag = num <= Expl.ShowMaxDistanceExplosive;
			if (flag)
			{
				Renders.DrawString(new Vector2(vector.x, vector.y), string.Format("<size=30>✴</size>\n[{0}]", (int)num), Color.red, true, 12, true, 0);
			}
			bool flag2 = num < 5f;
			if (flag2)
			{
				Expl.MarkersExplosive.Remove(vector);
			}
		}
	}

	// Token: 0x04000023 RID: 35
	public static float ShowMaxDistanceExplosive = 2000f;

	// Token: 0x04000024 RID: 36
	public Vector3 Position;

	// Token: 0x04000025 RID: 37
	public static List<Expl> ListPlayers = new List<Expl>();

	// Token: 0x04000026 RID: 38
	public static List<Vector3> MarkersExplosive = new List<Vector3>();

	// Token: 0x04000027 RID: 39
	public static Expl LocalPlayer = null;
}
