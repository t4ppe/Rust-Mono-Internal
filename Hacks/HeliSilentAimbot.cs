using System;
using System.Reflection;
using UnityEngine;

namespace Hacks
{
	// Token: 0x0200006E RID: 110
	public class HeliSilentAimbot : MonoBehaviour
	{
		// Token: 0x06000463 RID: 1123 RVA: 0x00002221 File Offset: 0x00000421
		private void Update()
		{
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00020924 File Offset: 0x0001EB24
		public static void HeliSilentAim()
		{
			BaseHelicopter baseHelicopter = null;
			float num = float.MaxValue;
			foreach (BaseHelicopter baseHelicopter2 in UnityEngine.Object.FindObjectsOfType<BaseHelicopter>())
			{
				Vector3 vector = MainCamera.mainCamera.WorldToScreenPoint(baseHelicopter.transform.position);
				float num2 = Mathf.Abs(Vector2.Distance(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(vector.x, (float)Screen.height - vector.y)));
				if (num2 <= num && num2 <= 250f)
				{
					num = num2;
					baseHelicopter = baseHelicopter2;
				}
			}
			foreach (Projectile projectile in UnityEngine.Object.FindObjectsOfType<Projectile>())
			{
				if (baseHelicopter != null && projectile.isAuthoritative)
				{
					projectile.thickness = 1f;
					projectile.breakProbability = 0f;
					projectile.InitializeVelocity(Vector3.down);
					projectile.gravityModifier = 0f;
					projectile.initialDistance = 0f;
					typeof(Projectile).GetField("currentPosition", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(projectile, baseHelicopter.mainRotor.transform.position);
					typeof(Projectile).GetField("currentVelocity", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(projectile, Vector3.down * 10f);
					projectile.transform.position = Vector3.Lerp(projectile.transform.position, baseHelicopter.mainRotor.transform.position, Vector3.Distance(projectile.transform.position, baseHelicopter.mainRotor.transform.position * 5f) * Time.deltaTime);
				}
			}
		}
	}
}
