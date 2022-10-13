using System.Reflection;
using UnityEngine;

namespace Hacks
{
    // Token: 0x0200006A RID: 106
    internal class Silentxd : MonoBehaviour
    {
        // Token: 0x0600045E RID: 1118 RVA: 0x000206B0 File Offset: 0x0001E8B0
        void Update()
        {
            if (HackManager.Silentxd && Input.GetKeyDown(KeyCode.F))
            {
                float num = 999f;
                foreach (BasePlayer basePlayer in BasePlayer.VisiblePlayerList)
                {
                    Vector3 vector = MainCamera.mainCamera.WorldToScreenPoint(basePlayer.transform.position + new Vector3(0f, 1.7f, 0f));
                    float num2 = Mathf.Abs(Vector2.Distance(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(vector.x, (float)Screen.height - vector.y)));
                    if (num2 <= HackManager.aimbotFov && num2 < num)
                    {
                        num = num2;
                        Silentxd.targetPlayer = basePlayer;
                    }
                }

                if (Silentxd.targetPlayer != null)
                {
                    foreach (Projectile projectile in UnityEngine.Object.FindObjectsOfType<Projectile>())
                    {
                        projectile.thickness = 1E+11f;
                        projectile.ricochetChance = 100f;
                        projectile.InitializeVelocity(Vector3.down);
                        projectile.maxDistance = 2000f;
                        projectile.breakProbability = 0f;
                        projectile.stickProbability = 0f;
                        typeof(Projectile).GetField("currentPosition", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(projectile, Silentxd.targetPlayer.FindBone("head").position);
                        typeof(Projectile).GetField("currentVelocity", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(projectile, Vector3.down * 3000f);
                        projectile.transform.position = Vector3.Lerp(projectile.transform.position, Silentxd.targetPlayer.FindBone("head").position, Vector3.Distance(projectile.transform.position, Silentxd.targetPlayer.FindBone("head").position * 100f * Time.deltaTime));
                    }
                }
            }
        }

        // Token: 0x04000414 RID: 1044
        public static bool Enabled;

        // Token: 0x04000415 RID: 1045
        public static BasePlayer targetPlayer;
    }
}