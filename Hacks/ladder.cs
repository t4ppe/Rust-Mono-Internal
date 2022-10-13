using System;
using UnityEngine;

namespace Hacks
{
	// Token: 0x0200006D RID: 109
	public class ladder
	{
		// Token: 0x06000461 RID: 1121 RVA: 0x0000461C File Offset: 0x0000281C
		public static void SpawnLadder()
		{
			GameManager.client.CreatePrefab("assets/prefabs/building/ladder.wall.wood/ladder.wooden.wall.prefab", LocalPlayer.Entity.lookingAtPoint, Quaternion.Euler(LocalPlayer.Entity.transform.rotation.x, 90f, 0f), true);
		}
	}
}
