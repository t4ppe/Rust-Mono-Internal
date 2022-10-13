using System;
using UnityEngine;

namespace Hacks
{
	// Token: 0x02000061 RID: 97
	internal class Aimbot
	{
		// Token: 0x0600044B RID: 1099 RVA: 0x0001F840 File Offset: 0x0001DA40
		public static void Aim()
		{
			Vector3 posTargetPlayer = Aimbot.GetPosTargetPlayer();
			bool flag = posTargetPlayer == Vector3.zero;
			if (!flag)
			{
				Quaternion quaternion = Quaternion.LookRotation(posTargetPlayer - MainCamera.mainCamera.transform.position, Vector3.right);
				float num = quaternion.eulerAngles.x;
				num = ((MainCamera.mainCamera.transform.position.y < posTargetPlayer.y) ? (-360f + num) : num);
				num = Mathf.Clamp(num, -89f, 89f);
				LocalPlayer.Entity.input.SetViewVars(new Vector3(num, quaternion.eulerAngles.y, 0f));
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001F8F4 File Offset: 0x0001DAF4
		private static Vector3 GetPosTargetPlayer()
		{
			Vector3 result = Vector3.zero;
			Vector2 a = new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2));
			float num = 999f;
			foreach (BasePlayer basePlayer in BasePlayer.VisiblePlayerList)
			{
				bool flag = basePlayer == null || basePlayer.IsLocalPlayer() || basePlayer.IsSleeping() || basePlayer.health <= 0f;
				if (!flag)
				{
					Vector3 positionBone = PlayerESP.GetPositionBone(basePlayer.GetModel(), "headCenter");
					bool flag2 = positionBone == Vector3.zero;
					if (!flag2)
					{
						bool flag3 = true;
						bool toggleVisibleCheck = HackManager.toggleVisibleCheck;
						if (toggleVisibleCheck)
						{
							flag3 = PlayerESP.IsVisible(positionBone);
						}
						bool flag4 = !flag3;
						if (!flag4)
						{
							Vector3 screenPos = PlayerESP.GetScreenPos(positionBone);
							bool flag5 = screenPos.z <= 0f;
							if (!flag5)
							{
								Vector2 b = new Vector2(screenPos.x, (float)Screen.height - screenPos.y);
								float num2 = Mathf.Abs(Vector2.Distance(a, b));
								bool flag6 = num2 > HackManager.aimbotFov || num2 > num;
								if (!flag6)
								{
									result = Aimbot.helirotortop;
									result = positionBone;
									num = num2;
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x04000352 RID: 850
		private static Vector3 helirotortop;
	}
}
