using UnityEngine;

namespace Hacks
{
    // Token: 0x02000062 RID: 98
    internal class AimHeli : MonoBehaviour
    {
        // Token: 0x0600044E RID: 1102 RVA: 0x0001FA78 File Offset: 0x0001DC78
        public static void AimHelicopter()
        {
            Vector3 posTargetPlayer = AimHeli.GetPosTargetPlayer();
            bool flag = posTargetPlayer != Vector3.zero;
            if (flag)
            {
                Vector3 vector = posTargetPlayer;
                Quaternion quaternion = Quaternion.LookRotation(vector - MainCamera.mainCamera.transform.position, Vector3.right);
                float num = quaternion.eulerAngles.x;
                num = ((MainCamera.mainCamera.transform.position.y < vector.y) ? (-360f + num) : num);
                num = Mathf.Clamp(num, -89f, 89f);
                LocalPlayer.Entity.input.SetViewVars(new Vector3(num, quaternion.eulerAngles.y, 0f));
            }
        }

        // Token: 0x0600044F RID: 1103 RVA: 0x0001FB38 File Offset: 0x0001DD38
        public static float BulletDrop(Vector3 v1, Vector3 v2, float BulletSpeed)
        {
            float num = Vector3.Distance(v1, v2);
            bool flag = (double)num >= 0.001;
            float result;
            if (flag)
            {
                float num2 = 9.81f;
                float num3 = num / BulletSpeed;
                result = (float)(0.5 * (double)num2 * (double)num3 * (double)num3);
            }
            else
            {
                result = 0f;
            }

            return result;
        }

        // Token: 0x06000450 RID: 1104 RVA: 0x0001FB90 File Offset: 0x0001DD90
        static Vector3 GetPosTargetPlayer()
        {
            Vector3 result = Vector3.zero;
            Vector2 a = new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2));
            float num = 999f;
            foreach (BaseHelicopter baseHelicopter in HackManager.heli)
            {
                Vector3 vector = new Vector3(baseHelicopter.transform.position.x, baseHelicopter.transform.position.y, baseHelicopter.transform.position.z);
                bool flag = vector != Vector3.zero;
                if (flag)
                {
                    Vector3 vector2 = MainCamera.mainCamera.WorldToScreenPoint(vector);
                    Vector2 b = new Vector2(vector2.x, (float)Screen.height - vector2.y);
                    float num2 = Mathf.Abs(Vector2.Distance(a, b));
                    bool flag2 = num2 <= HackManager.maxFOV && num2 <= num;
                    if (flag2)
                    {
                        result = vector;
                        var activeItem = LocalPlayer.Entity.Belt.GetActiveItem();
                        bool flag3 = activeItem != null && (activeItem.info.shortname.Contains("bow") || activeItem.info.shortname.Contains("smg.") || activeItem.info.shortname.Contains("pistol.") || activeItem.info.shortname.Contains("lmg.") || activeItem.info.shortname.Contains("rifle")) && (HackManager.BulletDropPrediction || HackManager.VelocityPrediction);
                        if (flag3)
                        {
                            float bulletSpeed = 375f;
                            var shortname = activeItem.info.shortname;
                        }

                        num = num2;
                    }
                }
            }

            return result;
        }

        // Token: 0x04000353 RID: 851
        public static BasePlayer localplayer;
    }
}