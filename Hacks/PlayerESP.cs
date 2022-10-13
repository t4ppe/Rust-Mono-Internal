using System;
using UnityEngine;

namespace Hacks
{
	// Token: 0x02000068 RID: 104
	internal class PlayerESP
	{
		// Token: 0x06000455 RID: 1109 RVA: 0x000203B4 File Offset: 0x0001E5B4
		public static void Players()
		{
			foreach (BasePlayer basePlayer in BasePlayer.VisiblePlayerList)
			{
				bool flag = basePlayer != null && basePlayer.health > 0f && !basePlayer.IsLocalPlayer() && basePlayer.userID > 1000000000UL;
				if (!flag)
				{
					bool flag2 = basePlayer.IsSleeping();
					if (!flag2)
					{
						int distance = PlayerESP.GetDistance(basePlayer.transform.position);
						bool flag3 = !basePlayer.IsSleeping() || basePlayer.IsSleeping();
						if (!flag3)
						{
							Vector3 screenPos = PlayerESP.GetScreenPos(basePlayer.transform.position);
							bool flag4 = screenPos.z <= 0f;
							if (!flag4)
							{
								bool flag5 = !basePlayer.IsSleeping();
								if (flag5)
								{
									Vector3 positionBone = PlayerESP.GetPositionBone(basePlayer.GetModel(), "headCenter");
									bool flag6 = positionBone == Vector3.zero;
									if (!flag6)
									{
										Vector3 screenPos2 = PlayerESP.GetScreenPos(positionBone + new Vector3(0f, 0.3f, 0f));
										float num = Mathf.Abs(screenPos.y - screenPos2.y);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00020520 File Offset: 0x0001E720
		public static Vector3 GetPositionBone(Model model, string name)
		{
			Vector3 result = Vector3.zero;
			bool flag = model != null;
			if (flag)
			{
				bool flag2 = name == "headCenter";
				if (flag2)
				{
					result = new Vector3(model.headBone.position.x, model.eyeBone.position.y, model.headBone.position.z);
				}
				else
				{
					result = model.FindBone(name).position;
				}
			}
			return result;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x000045CE File Offset: 0x000027CE
		public static bool IsVisible(Vector3 position)
		{
			return LocalPlayer.Entity.IsVisible(position, MainCamera.mainCamera.transform.position, float.PositiveInfinity);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x000045EF File Offset: 0x000027EF
		public static Vector3 GetScreenPos(Vector3 position)
		{
			return MainCamera.mainCamera.WorldToScreenPoint(position);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000045FC File Offset: 0x000027FC
		private static int GetDistance(Vector3 position)
		{
			return (int)Vector3.Distance(LocalPlayer.Entity.transform.position, position);
		}
	}
}
