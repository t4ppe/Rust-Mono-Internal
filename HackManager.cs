using BlackZero;
using Hacks;
using Network;
using ProtoBuf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

// Token: 0x0200000D RID: 13
public class HackManager : MonoBehaviour
{
    // Token: 0x06000049 RID: 73 RVA: 0x0000A278 File Offset: 0x00008478
    static HackManager()
    {
        HackManager.MarkersExplosive = new List<Vector3>();
        HackManager.rect_esp = new Rect(50f, 100f, 235f, 120f);
        HackManager.color_0 = new Color(0.28f, 0.8f, 1f, 1f);
        HackManager.color_1 = new Color(0.13f, 0.9f, 0.55f, 1f);
        HackManager.EspRGBAPlayers = new Color(1f, 0f, 0f, 1f);
        HackManager.Whitelist = new List<string>();
        HackManager.enableHackKey = KeyCode.Mouse2;
        HackManager.enableHack = true;
        HackManager.showMenuKey = KeyCode.Delete;
        HackManager.showMenu = true;
        HackManager.Aim_Heli = false;
        HackManager.ESP_Active = false;
        HackManager.ESP_OnlinePlayers = true;
        HackManager.ESP_Scientist = false;
        HackManager.ESP_Node = false;
        HackManager.ESP_Animal = false;
        HackManager.ESP_Dropped = false;
        HackManager.ESP_Heli = false;
        HackManager.ESP_Airdrop = false;
        HackManager.ESP_Crate = false;
        HackManager.ESP_Stash = false;
        HackManager.ESP_Far = true;
        HackManager.ESP_FarRange = 600;
        HackManager.Color_ESPFar = new Color(1f, 0f, 0f, 1f);
        HackManager.ESP_Sleepers = false;
        HackManager.ESP_Bones = false;
        HackManager.Color_ESPBones = new Color(0f, 1f, 0f, 1f);
        HackManager.ESP_Boxes = false;
        HackManager.ESP_Boxes3D = false;
        HackManager.ESP_ActiveRange = 150;
        HackManager.ESP_ShowInventory = false;
        HackManager.Misc_FastHeal = false;
        HackManager.Misc_FastLoot = false;
        HackManager.Misc_FastGather = false;
        HackManager.ESP_ShowTCAuth = false;
        HackManager.shouldDrawCupboards = true;
        HackManager.ESP_TC = false;
        HackManager.ESP_Corpse = false;
        HackManager.ESP_Turrets = false;
        HackManager.ESP_Boxs = false;
        HackManager.ESP_Entitys = false;
        HackManager.ESP_Explosions = false;
        HackManager.Misc_InstantRevive = false;
        HackManager.Misc_Autoloot = false;
        HackManager.Misc_OreHotSpot = false;
        HackManager.Misc_TreeMarker = true;
        HackManager.Misc_MeleeAim = false;
        HackManager.Misc_NoClip = false;
        HackManager.Color_ESPTC = Color.yellow;
        HackManager.toggleAimbot = false;
        HackManager.toggleAimbotHeli = false;
        HackManager.aimbotKeyy = KeyCode.C;
        HackManager.AimAtHead = false;
        HackManager.maxFOV = 120f;
        HackManager.VelocityPrediction = true;
        HackManager.BulletDropPrediction = true;
        HackManager.shouldEnableAimbot = false;
        HackManager.toggleVisibleCheck = false;
        HackManager.aimbotKey = KeyCode.X;
        HackManager.aimbotFov = 80f;
        HackManager.Aim_Fov = 6;
        HackManager.Aim_DrawFov = false;
        HackManager.Color_AimDrawFov = new Color(111f, 23f, 52f);
        HackManager.Aim_Xhair = false;
        HackManager.Radar_Active = false;
        HackManager.Color_RadarFriends = new Color(0f, 0f, 0f, 0f);
        HackManager.Radar_Enemies = true;
        HackManager.Color_RadarEnemies = new Color(1f, 0f, 0f, 1f);
        HackManager.Radar_X = 0;
        HackManager.Radar_Y = 0;
        HackManager.Radar_Size = 150;
        HackManager.Radar_Range = 500;
        HackManager.Misc_Spider = false;
        HackManager.Misc_Speed = false;
        HackManager.Misc_Freeze = false;
        HackManager.Misc_FreezeValue = 12;
        HackManager.Misc_LowGravity = false;
        HackManager.Misc_NoSink = true;
        HackManager.Misc_MultiJump = false;
        HackManager.Misc_AdminPriv = false;
        HackManager.Aim_NoRecoil = true;
        HackManager.Aim_NoSway = true;
        HackManager.Aim_NoSpread = true;
        HackManager.Aim_ForceAuto = true;
        HackManager.ShowMaxDistanceExplosive = 2000f;
        HackManager.FovRadius = 60;
        HackManager.Aim_Active = true;
        HackManager.AimMode = false;
        HackManager.Aim_Smooth = true;
        HackManager.Aim_VisCheck = false;
        HackManager.Aim_BoltFast = true;
        HackManager.Aim_Range = 1000;
        HackManager.Aim_Position = false;
        HackManager.Aim_NoDrop = true;
        HackManager.PlayerBones = new Dictionary<string, HackManager.BonePositions>();
        HackManager.PlayerBoxes = new Dictionary<string, HackManager.BoxPositions>();
    }
    // Token: 0x0600002A RID: 42 RVA: 0x0000556C File Offset: 0x0000376C
    public Vector3 ClampAngles(Vector3 angles)
    {
        bool flag = angles.x > 89f;
        if (flag)
        {
            angles.x -= 360f;
        }
        else
        {
            bool flag2 = angles.x < -89f;
            if (flag2)
            {
                angles.x += 360f;
            }
        }

        bool flag3 = angles.y > 180f;
        if (flag3)
        {
            angles.y -= 360f;
        }
        else
        {
            bool flag4 = angles.y < -180f;
            if (flag4)
            {
                angles.y += 360f;
            }
        }

        angles.z = 0f;
        return angles;
    }

    // Token: 0x0600002B RID: 43 RVA: 0x00005620 File Offset: 0x00003820
    void CalcPositons(global::BasePlayer p)
    {
        Bounds bounds = default(Bounds);
        bool flag = p.IsDucked();
        if (flag)
        {
            bounds.center = p.transform.position + new Vector3(0f, 0.55f, 0f);
            bounds.extents = new Vector3(0.5f, 0.55f, 0.5f);
        }
        else
        {
            bounds.center = p.transform.position + new Vector3(0f, 0.9f, 0f);
            bounds.extents = new Vector3(0.5f, 0.9f, 0.5f);
        }

        Vector3 center = bounds.center;
        Vector3 extents = bounds.extents;
        v3FrontTopLeft = new Vector3(center.x - extents.x, center.y + extents.y, center.z - extents.z);
        v3FrontTopRight = new Vector3(center.x + extents.x, center.y + extents.y, center.z - extents.z);
        v3FrontBottomLeft = new Vector3(center.x - extents.x, center.y - extents.y, center.z - extents.z);
        v3FrontBottomRight = new Vector3(center.x + extents.x, center.y - extents.y, center.z - extents.z);
        v3BackTopLeft = new Vector3(center.x - extents.x, center.y + extents.y, center.z + extents.z);
        v3BackTopRight = new Vector3(center.x + extents.x, center.y + extents.y, center.z + extents.z);
        v3BackBottomLeft = new Vector3(center.x - extents.x, center.y - extents.y, center.z + extents.z);
        v3BackBottomRight = new Vector3(center.x + extents.x, center.y - extents.y, center.z + extents.z);
        v3FrontTopLeft = base.transform.TransformPoint(v3FrontTopLeft);
        v3FrontTopRight = base.transform.TransformPoint(v3FrontTopRight);
        v3FrontBottomLeft = base.transform.TransformPoint(v3FrontBottomLeft);
        v3FrontBottomRight = base.transform.TransformPoint(v3FrontBottomRight);
        v3BackTopLeft = base.transform.TransformPoint(v3BackTopLeft);
        v3BackTopRight = base.transform.TransformPoint(v3BackTopRight);
        v3BackBottomLeft = base.transform.TransformPoint(v3BackBottomLeft);
        v3BackBottomRight = base.transform.TransformPoint(v3BackBottomRight);
        v2FrontTopLeft = MainCamera.mainCamera.WorldToScreenPoint(v3FrontTopLeft);
        v2FrontTopLeft.y = (float)Screen.height - v2FrontTopLeft.y;
        v2FrontTopRight = MainCamera.mainCamera.WorldToScreenPoint(v3FrontTopRight);
        v2FrontTopRight.y = (float)Screen.height - v2FrontTopRight.y;
        v2FrontBottomLeft = MainCamera.mainCamera.WorldToScreenPoint(v3FrontBottomLeft);
        v2FrontBottomLeft.y = (float)Screen.height - v2FrontBottomLeft.y;
        v2FrontBottomRight = MainCamera.mainCamera.WorldToScreenPoint(v3FrontBottomRight);
        v2FrontBottomRight.y = (float)Screen.height - v2FrontBottomRight.y;
        v2BackTopLeft = MainCamera.mainCamera.WorldToScreenPoint(v3BackTopLeft);
        v2BackTopLeft.y = (float)Screen.height - v2BackTopLeft.y;
        v2BackTopRight = MainCamera.mainCamera.WorldToScreenPoint(v3BackTopRight);
        v2BackTopRight.y = (float)Screen.height - v2BackTopRight.y;
        v2BackBottomLeft = MainCamera.mainCamera.WorldToScreenPoint(v3BackBottomLeft);
        v2BackBottomLeft.y = (float)Screen.height - v2BackBottomLeft.y;
        v2BackBottomRight = MainCamera.mainCamera.WorldToScreenPoint(v3BackBottomRight);
        v2BackBottomRight.y = (float)Screen.height - v2BackBottomRight.y;
    }

    // Token: 0x0600002C RID: 44 RVA: 0x00005AF0 File Offset: 0x00003CF0
    bool VisibleOnScreen(Vector3 point)
    {
        Vector3 rhs = point - MainCamera.mainCamera.transform.position;
        return Vector3.Dot(MainCamera.mainCamera.transform.TransformDirection(Vector3.forward), rhs) > 0f;
    }

    // Token: 0x0600002D RID: 45 RVA: 0x00005B44 File Offset: 0x00003D44
    void DrawBox()
    {
        bool flag = VisibleOnScreen(v3FrontTopLeft) && VisibleOnScreen(v3FrontTopRight) && VisibleOnScreen(v3FrontBottomLeft) && VisibleOnScreen(v3FrontBottomRight) && VisibleOnScreen(v3BackTopLeft) && VisibleOnScreen(v3BackTopRight) && VisibleOnScreen(v3BackBottomLeft) && VisibleOnScreen(v3BackBottomRight);
        if (flag)
        {
            Drawing.DrawLine(v2FrontTopLeft, v2FrontTopRight, Color.green, 1.5f, true);
            Drawing.DrawLine(v2FrontTopRight, v2FrontBottomRight, Color.green, 1.5f, true);
            Drawing.DrawLine(v2FrontBottomRight, v2FrontBottomLeft, Color.green, 1.5f, true);
            Drawing.DrawLine(v2FrontBottomLeft, v2FrontTopLeft, Color.green, 1.5f, true);
            Drawing.DrawLine(v2BackTopLeft, v2BackTopRight, Color.green, 1.5f, true);
            Drawing.DrawLine(v2BackTopRight, v2BackBottomRight, Color.green, 1.5f, true);
            Drawing.DrawLine(v2BackBottomRight, v2BackBottomLeft, Color.green, 1.5f, true);
            Drawing.DrawLine(v2BackBottomLeft, v2BackTopLeft, Color.green, 1.5f, true);
            Drawing.DrawLine(v2FrontTopLeft, v2BackTopLeft, Color.green, 1.5f, true);
            Drawing.DrawLine(v2FrontTopRight, v2BackTopRight, Color.green, 1.5f, true);
            Drawing.DrawLine(v2FrontBottomRight, v2BackBottomRight, Color.green, 1.5f, true);
            Drawing.DrawLine(v2FrontBottomLeft, v2BackBottomLeft, Color.green, 1.5f, true);
        }
    }

    // Token: 0x0600002E RID: 46 RVA: 0x00005D28 File Offset: 0x00003F28
    float NormalizeAngle(float angle)
    {
        while (angle > 360f)
            angle -= 360f;


        while (angle < 0f)
            angle += 360f;


        return angle;
    }

    // Token: 0x0600002F RID: 47 RVA: 0x00005D70 File Offset: 0x00003F70
    Vector3 NormalizeAngles(Vector3 angles)
    {
        angles.x = NormalizeAngle(angles.x);
        angles.y = NormalizeAngle(angles.y);
        angles.z = NormalizeAngle(angles.z);
        return angles;
    }

    // Token: 0x06000030 RID: 48 RVA: 0x00005DBC File Offset: 0x00003FBC
    Vector3 EulerAngles(Quaternion q1)
    {
        float num = q1.w * q1.w;
        float num2 = q1.x * q1.x;
        float num3 = q1.y * q1.y;
        float num4 = q1.z * q1.z;
        float num5 = num2 + num3 + num4 + num;
        float num6 = q1.x * q1.w - q1.y * q1.z;
        Vector3 a = default(Vector3);
        bool flag = num6 <= 0.4995f * num5;
        Vector3 result;
        if (flag)
        {
            bool flag2 = num6 >= -0.4995f * num5;
            if (flag2)
            {
                Quaternion quaternion = new Quaternion(q1.w, q1.z, q1.x, q1.y);
                a.y = Mathf.Atan2(2f * quaternion.x * quaternion.w + 2f * quaternion.y * quaternion.z, 1f - 2f * (quaternion.z * quaternion.z + quaternion.w * quaternion.w));
                a.x = Mathf.Asin(2f * (quaternion.x * quaternion.z - quaternion.w * quaternion.y));
                a.z = Mathf.Atan2(2f * quaternion.x * quaternion.y + 2f * quaternion.z * quaternion.w, 1f - 2f * (quaternion.y * quaternion.y + quaternion.z * quaternion.z));
                result = NormalizeAngles(a * 57.2958f);
            }
            else
            {
                a.y = -2f * Mathf.Atan2(q1.y, q1.x);
                a.x = -1.57079637f;
                a.z = 0f;
                result = NormalizeAngles(a * 57.2958f);
            }
        }
        else
        {
            a.y = 2f * Mathf.Atan2(q1.y, q1.x);
            a.x = 1.57079637f;
            a.z = 0f;
            result = NormalizeAngles(a * 57.2958f);
        }

        return result;
    }

    // Token: 0x06000031 RID: 49 RVA: 0x0000602C File Offset: 0x0000422C
    Vector3 RotatePoint(Vector3 center, Vector3 origin, float angle)
    {
        float f = angle * 0.0174532924f;
        float num = 0f - Mathf.Sin(f);
        float num2 = Mathf.Cos(f);
        origin.x -= center.x;
        origin.z -= center.z;
        float num3 = origin.x * num2 - origin.z * num;
        float num4 = origin.x * num + origin.z * num2;
        num3 += center.x;
        num4 += center.z;
        return new Vector3(num3, origin.y, num4);
    }

    // Token: 0x06000032 RID: 50 RVA: 0x000060C8 File Offset: 0x000042C8
    void DrawBoundingBox(global::BasePlayer p, Color dwColor, bool ThreeD)
    {
        bool flag = HackManager.PlayerBoxes.ContainsKey(p.userID.ToString());
        if (flag)
        {
            Vector3 frontTopright = HackManager.PlayerBoxes[p.userID.ToString()].frontTopright;
            Vector3 frontTopleft = HackManager.PlayerBoxes[p.userID.ToString()].frontTopleft;
            Vector3 frontBottomright = HackManager.PlayerBoxes[p.userID.ToString()].frontBottomright;
            Vector3 frontBottomleft = HackManager.PlayerBoxes[p.userID.ToString()].frontBottomleft;
            Vector3 backTopright = HackManager.PlayerBoxes[p.userID.ToString()].backTopright;
            Vector3 backTopleft = HackManager.PlayerBoxes[p.userID.ToString()].backTopleft;
            Vector3 backBottomright = HackManager.PlayerBoxes[p.userID.ToString()].backBottomright;
            Vector3 backBottomleft = HackManager.PlayerBoxes[p.userID.ToString()].backBottomleft;
            if (ThreeD)
            {
                Drawing.DrawLine(frontTopleft, frontTopright, dwColor, 1.85f, true);
                Drawing.DrawLine(frontTopright, frontBottomright, dwColor, 1.85f, true);
                Drawing.DrawLine(frontBottomright, frontBottomleft, dwColor, 1.85f, true);
                Drawing.DrawLine(frontBottomleft, frontTopleft, dwColor, 1.85f, true);
                Drawing.DrawLine(backTopleft, backTopright, dwColor, 1.85f, true);
                Drawing.DrawLine(backTopright, backBottomright, dwColor, 1.85f, true);
                Drawing.DrawLine(backBottomright, backBottomleft, dwColor, 1.85f, true);
                Drawing.DrawLine(backBottomleft, backTopleft, dwColor, 1.85f, true);
                Drawing.DrawLine(frontTopleft, backTopleft, dwColor, 1.85f, true);
                Drawing.DrawLine(frontTopright, backTopright, dwColor, 1.85f, true);
                Drawing.DrawLine(frontBottomright, backBottomright, dwColor, 1.85f, true);
                Drawing.DrawLine(frontBottomleft, backBottomleft, dwColor, 1.85f, true);
            }
            else
            {
                Vector2[] array = new Vector2[]
                {
                    frontBottomleft,
                    backTopright,
                    backBottomleft,
                    frontTopright,
                    frontBottomright,
                    backBottomright,
                    backTopleft,
                    frontTopleft
                };
                float x = frontBottomleft.x;
                float y = frontBottomleft.y;
                float x2 = frontBottomleft.x;
                float y2 = frontBottomleft.y;
                for (int i = 1; i < 8; i++)
                {
                    bool flag2 = x > array[i].x;
                    if (flag2)
                    {
                        x = array[i].x;
                    }

                    bool flag3 = y < array[i].y;
                    if (flag3)
                    {
                        y = array[i].y;
                    }

                    bool flag4 = x2 < array[i].x;
                    if (flag4)
                    {
                        x2 = array[i].x;
                    }

                    bool flag5 = y2 > array[i].y;
                    if (flag5)
                    {
                        y2 = array[i].y;
                    }
                }

                Drawing.DrawLine(new Vector2(x, y2), new Vector2(x, y), dwColor, 1.85f, true);
                Drawing.DrawLine(new Vector2(x, y), new Vector2(x2, y), dwColor, 1.85f, true);
                Drawing.DrawLine(new Vector2(x2, y), new Vector2(x2, y2), dwColor, 1.85f, true);
                Drawing.DrawLine(new Vector2(x2, y2), new Vector2(x, y2), dwColor, 1.85f, true);
            }
        }
    }

    // Token: 0x06000033 RID: 51 RVA: 0x000064FC File Offset: 0x000046FC
    void DrawBones(global::BasePlayer p, Color color)
    {
        bool flag = HackManager.PlayerBones.ContainsKey(p.userID.ToString());
        if (flag)
        {
            Vector3 head = HackManager.PlayerBones[p.userID.ToString()].head;
            Vector3 spine = HackManager.PlayerBones[p.userID.ToString()].spine;
            Vector3 l_shoulder = HackManager.PlayerBones[p.userID.ToString()].l_shoulder;
            Vector3 r_shoulder = HackManager.PlayerBones[p.userID.ToString()].r_shoulder;
            Vector3 l_elbow = HackManager.PlayerBones[p.userID.ToString()].l_elbow;
            Vector3 r_elbow = HackManager.PlayerBones[p.userID.ToString()].r_elbow;
            Vector3 l_hand = HackManager.PlayerBones[p.userID.ToString()].l_hand;
            Vector3 r_hand = HackManager.PlayerBones[p.userID.ToString()].r_hand;
            Vector3 pelvis = HackManager.PlayerBones[p.userID.ToString()].pelvis;
            Vector3 l_hip = HackManager.PlayerBones[p.userID.ToString()].l_hip;
            Vector3 r_hip = HackManager.PlayerBones[p.userID.ToString()].r_hip;
            Vector3 l_knee = HackManager.PlayerBones[p.userID.ToString()].l_knee;
            Vector3 r_knee = HackManager.PlayerBones[p.userID.ToString()].r_knee;
            Vector3 l_foot = HackManager.PlayerBones[p.userID.ToString()].l_foot;
            Vector3 r_foot = HackManager.PlayerBones[p.userID.ToString()].r_foot;
            Drawing.DrawLine(head, spine, color, 1.2f, true);
            Drawing.DrawLine(spine, l_shoulder, color, 1.2f, true);
            Drawing.DrawLine(l_shoulder, l_elbow, color, 1.2f, true);
            Drawing.DrawLine(l_elbow, l_hand, color, 1.2f, true);
            Drawing.DrawLine(spine, r_shoulder, color, 1.2f, true);
            Drawing.DrawLine(r_shoulder, r_elbow, color, 1.2f, true);
            Drawing.DrawLine(r_elbow, r_hand, color, 1.2f, true);
            Drawing.DrawLine(spine, pelvis, color, 1.2f, true);
            Drawing.DrawLine(pelvis, l_hip, color, 1.2f, true);
            Drawing.DrawLine(l_hip, l_knee, color, 1.2f, true);
            Drawing.DrawLine(l_knee, l_foot, color, 1.2f, true);
            Drawing.DrawLine(pelvis, r_hip, color, 1.2f, true);
            Drawing.DrawLine(r_hip, r_knee, color, 1.2f, true);
            Drawing.DrawLine(r_knee, r_foot, color, 1.2f, true);
        }
    }

    // Token: 0x06000034 RID: 52 RVA: 0x0000683C File Offset: 0x00004A3C
    internal static object GetInstanceField(Type type, object instance, string fieldName)
    {
        BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        var field = type.GetField(fieldName, bindingAttr);
        return field.GetValue(instance);
    }

    // Token: 0x06000035 RID: 53 RVA: 0x00006864 File Offset: 0x00004A64
    void OnGUI()
    {
        try
        {
            bool flag = HackManager.showMenu;
            if (flag)
            {
            }

            bool flag2 = HackManager.toggleAimbot && UnityEngine.Input.GetKey(HackManager.aimbotKey);
            if (flag2)
            {
                Aimbot.Aim();
            }

            bool key = UnityEngine.Input.GetKey(HackManager.aimbotKeyy);
            if (key)
            {
                AimHeli.AimHelicopter();
            }

            bool flag3 = HackManager.localplayer == null;
            if (flag3)
            {
                foreach (global::BasePlayer basePlayer in global::BasePlayer.VisiblePlayerList)
                {
                    bool flag4 = basePlayer.IsLocalPlayer();
                    if (flag4)
                    {
                        HackManager.localplayer = basePlayer;
                    }
                }
            }

            bool flag5 = !Renders.initialized;
            if (flag5)
            {
                Renders.Initialize();
            }

            bool radar_Active = HackManager.Radar_Active;
            if (radar_Active)
            {
                bool flag6 = HackManager.Radar_Size % 2 != 0;
                if (flag6)
                {
                    HackManager.Radar_Size--;
                }

                Renders.DrawRadarBackground(new Rect((float)HackManager.Radar_X, (float)HackManager.Radar_Y, (float)HackManager.Radar_Size, (float)HackManager.Radar_Size));
                Renders.BoxRect(new Rect((float)(HackManager.Radar_X + HackManager.Radar_Size / 2 - 3), (float)(HackManager.Radar_Y + HackManager.Radar_Size / 2 - 3), 6f, 6f), Color.magenta);
                foreach (global::BasePlayer basePlayer2 in global::BasePlayer.VisiblePlayerList)
                {
                    bool flag7 = basePlayer2 != null && basePlayer2.health > 0f && !basePlayer2.IsSleeping() && !basePlayer2.IsLocalPlayer();
                    if (flag7)
                    {
                        bool flag8 = HackManager.Radar_Enemies && !HackManager.Whitelist.Contains(basePlayer2.userID.ToString());
                        if (flag8)
                        {
                            Vector3 position = HackManager.localplayer.transform.position;
                            Vector3 position2 = basePlayer2.transform.position;
                            float num = Vector3.Distance(position, position2);
                            float y = position.x - position2.x;
                            float x = position.z - position2.z;
                            float num2 = Mathf.Atan2(y, x) * 57.29578f - 270f - HackManager.localplayer.transform.eulerAngles.y;
                            float num3 = num * Mathf.Cos(num2 * 0.0174532924f);
                            float num4 = num * Mathf.Sin(num2 * 0.0174532924f);
                            num3 = num3 * ((float)HackManager.Radar_Size / (float)HackManager.Radar_Range) / 2f;
                            num4 = num4 * ((float)HackManager.Radar_Size / (float)HackManager.Radar_Range) / 2f;
                            bool flag9 = num <= 500f;
                            if (flag9)
                            {
                                Renders.BoxRect(new Rect((float)(HackManager.Radar_X + HackManager.Radar_Size / 2) + num3 - 3f, (float)(HackManager.Radar_Y + HackManager.Radar_Size / 2) + num4 - 3f, 6f, 6f), HackManager.Color_RadarEnemies);
                            }

                            bool flag10 = num <= 500f && basePlayer2.userID < 999999999UL && LocalPlayer.Entity != null;
                            if (flag10)
                            {
                                Renders.BoxRect(new Rect((float)(HackManager.Radar_X + HackManager.Radar_Size / 2) + num3 - 3f, (float)(HackManager.Radar_Y + HackManager.Radar_Size / 2) + num4 - 3f, 6f, 6f), Color.magenta);
                            }
                        }

                        bool flag11 = HackManager.Radar_Friends && HackManager.Whitelist.Contains(basePlayer2.userID.ToString());
                        if (flag11)
                        {
                            Vector3 position3 = HackManager.localplayer.transform.position;
                            Vector3 position4 = basePlayer2.transform.position;
                            float num5 = Vector3.Distance(position3, position4);
                            float y2 = position3.x - position4.x;
                            float x2 = position3.z - position4.z;
                            float num6 = Mathf.Atan2(y2, x2) * 57.29578f - 270f - HackManager.localplayer.transform.eulerAngles.y;
                            float num7 = num5 * Mathf.Cos(num6 * 0.0174532924f);
                            float num8 = num5 * Mathf.Sin(num6 * 0.0174532924f);
                            num7 = num7 * ((float)HackManager.Radar_Size / (float)HackManager.Radar_Range) / 2f;
                            num8 = num8 * ((float)HackManager.Radar_Size / (float)HackManager.Radar_Range) / 2f;
                            bool flag12 = num5 <= (float)HackManager.Radar_Range;
                            if (flag12)
                            {
                                Renders.BoxRect(new Rect((float)(HackManager.Radar_X + HackManager.Radar_Size / 2) + num7 - 3f, (float)(HackManager.Radar_Y + HackManager.Radar_Size / 2) + num8 - 3f, 6f, 6f), HackManager.Color_RadarFriends);
                            }
                        }
                    }
                }

                foreach (BaseHelicopter baseHelicopter in HackManager.heli)
                {
                    Vector3 position5 = HackManager.localplayer.transform.position;
                    Vector3 position6 = baseHelicopter.transform.position;
                    float num9 = Vector3.Distance(position5, position6);
                    float y3 = position5.x - position6.x;
                    float x3 = position5.z - position6.z;
                    float num10 = Mathf.Atan2(y3, x3) * 57.29578f - 270f - HackManager.localplayer.transform.eulerAngles.y;
                    float num11 = num9 * Mathf.Cos(num10 * 0.0174532924f);
                    float num12 = num9 * Mathf.Sin(num10 * 0.0174532924f);
                    num11 = num11 * ((float)HackManager.Radar_Size / (float)HackManager.Radar_Range) / 2f;
                    num12 = num12 * ((float)HackManager.Radar_Size / (float)HackManager.Radar_Range) / 2f;
                    bool flag13 = num9 <= 5000f;
                    if (flag13)
                    {
                        Renders.BoxRect(new Rect((float)(HackManager.Radar_X + HackManager.Radar_Size / 2) + num11 - 3f, (float)(HackManager.Radar_Y + HackManager.Radar_Size / 2) + num12 - 3f, 6f, 6f), Color.white);
                    }
                }
            }

            bool flag14 = HackManager.ESP_Active && HackManager.localplayer != null;
            if (flag14)
            {
                bool esp_Node = HackManager.ESP_Node;
                if (esp_Node)
                {
                    foreach (OreResourceEntity oreResourceEntity in HackManager.ore)
                    {
                        Vector3 vector = MainCamera.mainCamera.WorldToScreenPoint(oreResourceEntity.transform.position);
                        bool flag15 = vector.z > 0f;
                        if (flag15)
                        {
                            int num13 = (int)Vector3.Distance(LocalPlayer.Entity.transform.position, oreResourceEntity.transform.position);
                            bool flag16 = num13 <= HackManager.ESP_ActiveRange;
                            if (flag16)
                            {
                                vector.x += 3f;
                                vector.y = (float)Screen.height - (vector.y + 1f);
                                Renders.DrawString(new Vector2(vector.x, vector.y), string.Format("{0} [{1}]", oreResourceEntity.ShortPrefabName.Replace(".prefab", "").Replace("_deployed", "").Replace(".entity", ""), num13), Color.yellow, true, 12, true, 0);
                            }
                        }
                    }
                }

                bool esp_Animal = HackManager.ESP_Animal;
                if (esp_Animal)
                {
                    foreach (BaseNpc baseNpc in HackManager.animal)
                    {
                        Vector3 vector2 = MainCamera.mainCamera.WorldToScreenPoint(baseNpc.transform.position);
                        bool flag17 = vector2.z > 0f;
                        if (flag17)
                        {
                            int num14 = (int)Vector3.Distance(LocalPlayer.Entity.transform.position, baseNpc.transform.position);
                            bool flag18 = num14 <= HackManager.ESP_ActiveRange;
                            if (flag18)
                            {
                                vector2.x += 3f;
                                vector2.y = (float)Screen.height - (vector2.y + 1f);
                                Renders.DrawString(new Vector2(vector2.x, vector2.y), string.Format("{0} [{1}]", baseNpc.ShortPrefabName.Replace(".prefab", "").Replace("_deployed", "").Replace(".entity", ""), num14), Color.red, true, 12, true, 0);
                            }
                        }
                    }
                }

                bool esp_Dropped = HackManager.ESP_Dropped;
                if (esp_Dropped)
                {
                    foreach (global::WorldItem worldItem in HackManager.worlditem)
                    {
                        Vector3 vector3 = MainCamera.mainCamera.WorldToScreenPoint(worldItem.transform.position);
                        bool flag19 = vector3.z > 0f;
                        if (flag19)
                        {
                            int num15 = (int)Vector3.Distance(LocalPlayer.Entity.transform.position, worldItem.transform.position);
                            bool flag20 = num15 <= HackManager.ESP_ActiveRange;
                            if (flag20)
                            {
                                vector3.x += 3f;
                                vector3.y = (float)Screen.height - (vector3.y + 1f);
                                Renders.DrawString(new Vector2(vector3.x, vector3.y), string.Format("{0} [{1}]", worldItem.name.Replace(".prefab", "").Replace("_deployed", "").Replace(".entity", ""), num15), Color.cyan, true, 12, true, 0);
                            }
                        }
                    }
                }

                bool esp_Heli = HackManager.ESP_Heli;
                if (esp_Heli)
                {
                    foreach (BaseHelicopter baseHelicopter2 in HackManager.heli)
                    {
                        int num16 = (int)Vector3.Distance(LocalPlayer.Entity.transform.position, baseHelicopter2.transform.position);
                        Vector3 vector4 = MainCamera.mainCamera.WorldToScreenPoint(baseHelicopter2.transform.position);
                        bool flag21 = vector4.z > 0f;
                        if (flag21)
                        {
                            vector4.x += 3f;
                            vector4.y = (float)Screen.height - (vector4.y + 1f);
                            Renders.DrawString(new Vector2(vector4.x, vector4.y), string.Format("{0} [{1}] [{2}]", "<size=16>Верт</size>", (int)baseHelicopter2.health, num16), Color.red, true, 12, true, 0);
                        }
                    }
                }

                bool esp_Airdrop = HackManager.ESP_Airdrop;
                if (esp_Airdrop)
                {
                    foreach (StorageContainer storageContainer in HackManager.airdrop)
                    {
                        Vector3 vector5 = MainCamera.mainCamera.WorldToScreenPoint(storageContainer.transform.position);
                        bool flag22 = vector5.z > 0f;
                        if (flag22)
                        {
                            int num17 = (int)Vector3.Distance(LocalPlayer.Entity.transform.position, storageContainer.transform.position);
                            bool flag23 = storageContainer.ShortPrefabName.Contains("supply_drop");
                            bool flag24 = storageContainer.ShortPrefabName.Contains("codelockedhackablecrate");
                            bool flag25 = flag23 && num17 <= 3000;
                            if (flag25)
                            {
                                vector5.x += 3f;
                                vector5.y = (float)Screen.height - (vector5.y + 1f);
                                Renders.DrawString(new Vector2(vector5.x, vector5.y), string.Format("{0} [{1}]", storageContainer.ShortPrefabName.Replace(".prefab", "").Replace(".deployed", "").Replace(".entity", ""), num17), Color.magenta, true, 12, true, 0);
                            }

                            bool flag26 = flag24 && num17 <= 3000;
                            if (flag26)
                            {
                                vector5.x += 3f;
                                vector5.y = (float)Screen.height - (vector5.y + 1f);
                                Renders.DrawString(new Vector2(vector5.x, vector5.y), string.Format("{0} [{1}]", storageContainer.ShortPrefabName.Replace(".prefab", "").Replace(".deployed", "").Replace(".entity", ""), num17), Color.yellow, true, 12, true, 0);
                            }
                        }
                    }
                }

                bool esp_Crate = HackManager.ESP_Crate;
                if (esp_Crate)
                {
                    foreach (StorageContainer storageContainer2 in HackManager.lootbox)
                    {
                        Vector3 vector6 = MainCamera.mainCamera.WorldToScreenPoint(storageContainer2.transform.position);
                        bool flag27 = vector6.z > 0f && storageContainer2.ShortPrefabName.Contains("crate");
                        if (flag27)
                        {
                            int num18 = (int)Vector3.Distance(LocalPlayer.Entity.transform.position, storageContainer2.transform.position);
                            bool flag28 = num18 <= HackManager.ESP_ActiveRange;
                            if (flag28)
                            {
                                vector6.x += 3f;
                                vector6.y = (float)Screen.height - (vector6.y + 1f);
                                Renders.DrawString(new Vector2(vector6.x, vector6.y), string.Format("{0} [{1}]", storageContainer2.ShortPrefabName, num18), Color.magenta, true, 12, true, 0);
                            }
                        }
                    }
                }

                bool esp_Stash = HackManager.ESP_Stash;
                if (esp_Stash)
                {
                    foreach (StorageContainer storageContainer3 in HackManager.stash)
                    {
                        Vector3 vector7 = MainCamera.mainCamera.WorldToScreenPoint(storageContainer3.transform.position);
                        bool flag29 = vector7.z > 0f && storageContainer3.ShortPrefabName.Contains("small_stash");
                        if (flag29)
                        {
                            int num19 = (int)Vector3.Distance(LocalPlayer.Entity.transform.position, storageContainer3.transform.position);
                            bool flag30 = num19 <= 500;
                            if (flag30)
                            {
                                vector7.x += 3f;
                                vector7.y = (float)Screen.height - (vector7.y + 1f);
                                Renders.DrawString(new Vector2(vector7.x, vector7.y), string.Format("{0} [{1}]", storageContainer3.ShortPrefabName.Replace(".prefab", "").Replace("_deployed", "").Replace(".entity", ""), num19), Color.white, true, 12, true, 0);
                            }
                        }
                    }
                }

                bool esp_TC = HackManager.ESP_TC;
                if (esp_TC)
                {
                    foreach (BuildingPrivlidge buildingPrivlidge in HackManager.cupboard)
                    {
                        Vector3 vector8 = MainCamera.mainCamera.WorldToScreenPoint(buildingPrivlidge.transform.position);
                        bool flag31 = vector8.z > 0f;
                        if (flag31)
                        {
                            int num20 = (int)Vector3.Distance(LocalPlayer.Entity.transform.position, buildingPrivlidge.transform.position);
                            bool flag32 = num20 <= HackManager.ESP_ActiveRange;
                            if (flag32)
                            {
                                vector8.x += 3f;
                                vector8.y = (float)Screen.height - (vector8.y + 1f);
                                Renders.DrawString(new Vector2(vector8.x, vector8.y), string.Format("{0} [{1}]", buildingPrivlidge.ShortPrefabName.Replace(".deployed", ""), num20), HackManager.Color_ESPTC, true, 12, true, 0);
                            }

                            bool esp_ShowTCAuth = HackManager.ESP_ShowTCAuth;
                            if (esp_ShowTCAuth)
                            {
                                var component = buildingPrivlidge.GetComponent<BuildingPrivlidge>();
                                List<PlayerNameID> list = new List<PlayerNameID>();
                                list = component.authorizedPlayers;
                                for (int i = 0; i < list.Count; i++)
                                {
                                    Renders.DrawString(new Vector2(vector8.x, vector8.y - (float)(i + 1) * 15f), list[i].username, Color.blue, true, 12, true, 0);
                                }
                            }
                        }
                    }
                }

                bool esp_Corpse = HackManager.ESP_Corpse;
                if (esp_Corpse)
                {
                    foreach (global::LootableCorpse lootableCorpse in HackManager.corpse)
                    {
                        Vector3 vector9 = MainCamera.mainCamera.WorldToScreenPoint(lootableCorpse.transform.position);
                        bool flag33 = vector9.z > 0f;
                        if (flag33)
                        {
                            int num21 = (int)Vector3.Distance(LocalPlayer.Entity.transform.position, lootableCorpse.transform.position);
                            bool flag34 = num21 <= HackManager.ESP_ActiveRange;
                            if (flag34)
                            {
                                vector9.x += 3f;
                                vector9.y = (float)Screen.height - (vector9.y + 1f);
                                Renders.DrawString(new Vector2(vector9.x, vector9.y), string.Format("{0} [{1}]", lootableCorpse.playerName, num21), Color.green, true, 12, true, 0);
                            }
                        }
                    }
                }

                bool esp_Turrets = HackManager.ESP_Turrets;
                if (esp_Turrets)
                {
                    foreach (StorageContainer storageContainer4 in HackManager.traps)
                    {
                        Vector3 vector10 = MainCamera.mainCamera.WorldToScreenPoint(storageContainer4.transform.position);
                        bool flag35 = vector10.z > 0f;
                        if (flag35)
                        {
                            bool flag36 = storageContainer4.ShortPrefabName.Contains("turret");
                            bool flag37 = storageContainer4.ShortPrefabName.Contains("guntrap");
                            bool flag38 = flag36;
                            if (flag38)
                            {
                                int num22 = (int)Vector3.Distance(LocalPlayer.Entity.transform.position, storageContainer4.transform.position);
                                bool flag39 = num22 <= 100;
                                if (flag39)
                                {
                                    vector10.x += 3f;
                                    vector10.y = (float)Screen.height - (vector10.y + 1f);
                                    Renders.DrawString(new Vector2(vector10.x, vector10.y), string.Format("{0} [{1}]", storageContainer4.ShortPrefabName.Replace(".prefab", "").Replace(".deployed", "").Replace(".entity", "").Replace("_deployed", ""), num22), Color.magenta, true, 12, true, 0);
                                }
                            }

                            bool flag40 = flag37;
                            if (flag40)
                            {
                                int num23 = (int)Vector3.Distance(LocalPlayer.Entity.transform.position, storageContainer4.transform.position);
                                bool flag41 = num23 <= 100;
                                if (flag41)
                                {
                                    vector10.x += 3f;
                                    vector10.y = (float)Screen.height - (vector10.y + 1f);
                                    Renders.DrawString(new Vector2(vector10.x, vector10.y), string.Format("{0} [{1}]", storageContainer4.ShortPrefabName.Replace(".prefab", "").Replace("_deployed", "").Replace(".deployed", "").Replace(".entity", ""), num23), Color.yellow, true, 12, true, 0);
                                }
                            }
                        }
                    }
                }

                bool esp_Boxs = HackManager.ESP_Boxs;
                if (esp_Boxs)
                {
                    foreach (StorageContainer storageContainer5 in HackManager.boxs)
                    {
                        Vector3 vector11 = MainCamera.mainCamera.WorldToScreenPoint(storageContainer5.transform.position);
                        bool flag42 = vector11.z > 0f;
                        if (flag42)
                        {
                            bool flag43 = storageContainer5.ShortPrefabName.Contains("box.wooden");
                            bool flag44 = storageContainer5.ShortPrefabName.Contains("woodbox");
                            bool flag45 = flag43;
                            if (flag45)
                            {
                                int num24 = (int)Vector3.Distance(LocalPlayer.Entity.transform.position, storageContainer5.transform.position);
                                bool flag46 = num24 <= 100;
                                if (flag46)
                                {
                                    vector11.x += 3f;
                                    vector11.y = (float)Screen.height - (vector11.y + 1f);
                                    Renders.DrawString(new Vector2(vector11.x, vector11.y), string.Format("{0} [{1}]", storageContainer5.ShortPrefabName.Replace(".prefab", "").Replace("_deployed", "").Replace(".entity", ""), num24), Color.yellow, true, 12, true, 0);
                                }
                            }

                            bool flag47 = flag44;
                            if (flag47)
                            {
                                int num25 = (int)Vector3.Distance(LocalPlayer.Entity.transform.position, storageContainer5.transform.position);
                                bool flag48 = num25 <= 100;
                                if (flag48)
                                {
                                    vector11.x += 3f;
                                    vector11.y = (float)Screen.height - (vector11.y + 1f);
                                    Renders.DrawString(new Vector2(vector11.x, vector11.y), string.Format("{0} [{1}]", storageContainer5.ShortPrefabName.Replace(".prefab", "").Replace("_deployed", "").Replace(".entity", ""), num25), Color.yellow, true, 12, true, 0);
                                }
                            }
                        }
                    }
                }

                bool esp_Explosions = HackManager.ESP_Explosions;
                if (esp_Explosions)
                {
                    foreach (TimedExplosive timedExplosive in HackManager.explos)
                    {
                        Vector3 vector12 = MainCamera.mainCamera.WorldToScreenPoint(timedExplosive.transform.position);
                        bool flag49 = vector12.z > 0f;
                        if (flag49)
                        {
                            int num26 = (int)Vector3.Distance(LocalPlayer.Entity.transform.position, timedExplosive.transform.position);
                            bool flag50 = num26 <= 4000;
                            if (flag50)
                            {
                                vector12.x += 3f;
                                vector12.y = (float)Screen.height - (vector12.y + 1f);
                                Renders.DrawString(new Vector2(vector12.x, vector12.y), string.Format("{0} [{1}]", "*", num26), Color.blue, true, 12, true, 0);
                            }
                        }
                    }

                    new WaitForSeconds(20f);
                }

                bool esp_ShowInventory = HackManager.ESP_ShowInventory;
                if (esp_ShowInventory)
                {
                    Dictionary<global::BasePlayer, int> dictionary = new Dictionary<global::BasePlayer, int>();
                    Vector2 b = new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f);
                    foreach (global::BasePlayer basePlayer3 in global::BasePlayer.VisiblePlayerList)
                    {
                        bool flag51 = basePlayer3 != null;
                        if (flag51)
                        {
                            int value = (int)Vector2.Distance(MainCamera.mainCamera.WorldToScreenPoint(basePlayer3.transform.position), b);
                            Vector3 rhs = basePlayer3.transform.position - MainCamera.mainCamera.transform.position;
                            bool flag52 = !basePlayer3.IsLocalPlayer() && basePlayer3.health > 0f && Vector3.Dot(MainCamera.mainCamera.transform.TransformDirection(Vector3.forward), rhs) > 0f;
                            if (flag52)
                            {
                                dictionary.Add(basePlayer3, value);
                            }
                        }
                    }

                    bool flag53 = dictionary.Count > 0;
                    if (flag53)
                    {
                        dictionary = dictionary.OrderBy(delegate (KeyValuePair<global::BasePlayer, int> pair)
                        {
                            KeyValuePair<global::BasePlayer, int> keyValuePair = pair;
                            return keyValuePair.Value;
                        }).ToDictionary((KeyValuePair<global::BasePlayer, int> pair) => pair.Key, (KeyValuePair<global::BasePlayer, int> pair) => pair.Value);
                        var basePlayer4 = dictionary.Keys.First<global::BasePlayer>();
                        bool flag54 = basePlayer4 != null;
                        if (flag54)
                        {
                            global::Item[] array = basePlayer4.inventory.AllItems();
                            Rect rect = new Rect((float)Screen.width - 250f, 60f, 200f, 35f + (float)(array.Length * 16));
                            Renders.DrawRadarBackground(rect);
                            Renders.DrawString(new Vector2((float)Screen.width - 240f, 70f), basePlayer4.displayName, Color.red, false, 14, true, 0);
                            for (int j = 0; j < array.Length; j++)
                            {
                                bool flag55 = array[j] != null;
                                if (flag55)
                                {
                                    Renders.DrawString(new Vector2((float)Screen.width - 240f, 70f + (float)((j + 1) * 16)), array[j].amount.ToString() + "x " + array[j].info.displayName.english, Color.white, false, 14, true, 0);
                                }
                            }
                        }
                    }
                }

                foreach (global::BasePlayer basePlayer5 in global::BasePlayer.VisiblePlayerList)
                {
                    bool flag56 = basePlayer5 != null && basePlayer5.health > 0f && !basePlayer5.IsLocalPlayer() && basePlayer5.userID > 1000000000UL;
                    if (flag56)
                    {
                        bool misc_InstantRevive = HackManager.Misc_InstantRevive;
                        if (misc_InstantRevive)
                        {
                            var method = HackManager.localplayer.GetType().GetMethod("PointSeePoint", BindingFlags.Instance | BindingFlags.NonPublic);
                            bool flag57 = (bool)method.Invoke(HackManager.localplayer, new object[]
                            {
                                HackManager.localplayer.GetModel().eyeBone.position,
                                basePlayer5.transform.position,
                                0f,
                                false
                            });
                            bool flag58 = basePlayer5 != null && basePlayer5.HasPlayerFlag(global::BasePlayer.PlayerFlags.Wounded) && Vector3.Distance(HackManager.localplayer.transform.position, basePlayer5.transform.position) <= 3f && HackManager.Whitelist.Contains(basePlayer5.userID.ToString()) && flag57;
                            if (flag58)
                            {
                                basePlayer5.ServerRPC("RPC_Assist");
                            }
                        }

                        Vector3 position7 = basePlayer5.transform.position;
                        Vector3 vector13 = MainCamera.mainCamera.WorldToScreenPoint(position7);
                        bool flag59 = vector13.z > 0f;
                        if (flag59)
                        {
                            int num27 = (int)Vector3.Distance(LocalPlayer.Entity.transform.position, position7);
                            int num28 = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, position7);
                            Vector2 vector14 = vector13;
                            vector14.y = (float)Screen.height - vector14.y;
                            bool flag60 = !basePlayer5.IsSleeping() && HackManager.ESP_OnlinePlayers;
                            if (flag60)
                            {
                                bool esp_Far = HackManager.ESP_Far;
                                if (esp_Far)
                                {
                                    bool flag61 = num28 <= HackManager.ESP_FarRange;
                                    if (flag61)
                                    {
                                        Renders.DrawString(new Vector2(vector13.x, (float)Screen.height - vector13.y), string.Format("{0} [{1}]", basePlayer5.displayName, num27), Color.white, true, 12, true, 0);
                                        Renders.DrawHealth(vector14, basePlayer5.Health(), true);
                                        bool flag62 = basePlayer5.GetHeldEntity() != null;
                                        if (flag62)
                                        {
                                            Renders.DrawWeapon(new Vector2(vector13.x, (float)Screen.height - vector13.y), basePlayer5.GetHeldEntity().ShortPrefabName.Replace(".prefab", "").Replace(".entity", ""), Color.white, true, 12, true);
                                        }
                                        else
                                        {
                                            Renders.DrawWeapon(new Vector2(vector13.x, (float)Screen.height - vector13.y), "", Color.red, true, 12, true);
                                        }

                                        bool esp_Boxes = HackManager.ESP_Boxes;
                                        if (esp_Boxes)
                                        {
                                            bool flag63 = PlayerESP.IsVisible(position7);
                                            if (flag63)
                                            {
                                                bool esp_Boxes3D = HackManager.ESP_Boxes3D;
                                                if (esp_Boxes3D)
                                                {
                                                    DrawBoundingBox(basePlayer5, Color.yellow, true);
                                                }
                                                else
                                                {
                                                    DrawBoundingBox(basePlayer5, Color.yellow, false);
                                                }
                                            }

                                            bool esp_Boxes3D2 = HackManager.ESP_Boxes3D;
                                            if (esp_Boxes3D2)
                                            {
                                                DrawBoundingBox(basePlayer5, Color.white, true);
                                            }
                                            else
                                            {
                                                DrawBoundingBox(basePlayer5, Color.white, false);
                                            }
                                        }

                                        bool esp_Bones = HackManager.ESP_Bones;
                                        if (esp_Bones)
                                        {
                                            bool flag64 = PlayerESP.IsVisible(position7);
                                            if (flag64)
                                            {
                                                DrawBones(basePlayer5, Color.yellow);
                                            }

                                            DrawBones(basePlayer5, Color.white);
                                        }
                                    }
                                    else
                                    {
                                        Renders.DrawString(new Vector2(vector13.x, (float)Screen.height - vector13.y), string.Format("{0} [{1}]", basePlayer5.displayName, num27), HackManager.Color_ESPFar, true, 11, true, 0);
                                    }
                                }
                                else
                                {
                                    Renders.DrawString(new Vector2(vector13.x, (float)Screen.height - vector13.y), string.Format("{0} [{1}]", basePlayer5.displayName, num27), Color.yellow, true, 12, true, 0);
                                    Renders.DrawHealth(vector14, basePlayer5.Health(), true);
                                    bool flag65 = basePlayer5.GetHeldEntity() != null;
                                    if (flag65)
                                    {
                                        Renders.DrawWeapon(new Vector2(vector13.x, (float)Screen.height - vector13.y), basePlayer5.GetHeldEntity().ShortPrefabName.Replace(".prefab", "").Replace(".entity", ""), Color.white, true, 12, true);
                                    }
                                    else
                                    {
                                        Renders.DrawWeapon(new Vector2(vector13.x, (float)Screen.height - vector13.y), "", Color.white, true, 12, true);
                                    }

                                    bool esp_Boxes2 = HackManager.ESP_Boxes;
                                    if (esp_Boxes2)
                                    {
                                        bool flag66 = PlayerESP.IsVisible(position7);
                                        if (flag66)
                                        {
                                            bool esp_Boxes3D3 = HackManager.ESP_Boxes3D;
                                            if (esp_Boxes3D3)
                                            {
                                                DrawBoundingBox(basePlayer5, Color.yellow, true);
                                            }
                                            else
                                            {
                                                DrawBoundingBox(basePlayer5, Color.yellow, false);
                                            }
                                        }

                                        bool esp_Boxes3D4 = HackManager.ESP_Boxes3D;
                                        if (esp_Boxes3D4)
                                        {
                                            DrawBoundingBox(basePlayer5, Color.white, true);
                                        }
                                        else
                                        {
                                            DrawBoundingBox(basePlayer5, Color.white, false);
                                        }
                                    }

                                    bool esp_Bones2 = HackManager.ESP_Bones;
                                    if (esp_Bones2)
                                    {
                                        bool flag67 = PlayerESP.IsVisible(position7);
                                        if (flag67)
                                        {
                                            DrawBones(basePlayer5, Color.red);
                                        }

                                        DrawBones(basePlayer5, Color.red);
                                    }
                                }
                            }
                            else
                            {
                                bool flag68 = basePlayer5.IsSleeping() && HackManager.ESP_Sleepers;
                                if (flag68)
                                {
                                    Renders.DrawString(new Vector2(vector13.x, (float)Screen.height - vector13.y), string.Format("{0} [{1}]  [{2}]", basePlayer5.displayName, basePlayer5.userID, num27), HackManager.Color_ESPSleepers, true, 11, true, 0);
                                }
                                else
                                {
                                    bool flag69 = basePlayer5.IsDead();
                                    if (flag69)
                                    {
                                        Renders.DrawString(new Vector2(vector13.x, (float)Screen.height - vector13.y), string.Format("{0}   [{1}]   [{2}]", basePlayer5.displayName, basePlayer5.userID, num27), HackManager.Color_ESPSleepers, true, 11, true, 0);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        bool flag70 = basePlayer5 != null && basePlayer5.health > 0f && !basePlayer5.IsLocalPlayer() && basePlayer5.userID < 1000000000UL && HackManager.ESP_Scientist;
                        if (flag70)
                        {
                            Vector3 position8 = basePlayer5.transform.position;
                            Vector3 vector15 = MainCamera.mainCamera.WorldToScreenPoint(position8);
                            bool flag71 = vector15.z > 0f;
                            if (flag71)
                            {
                                int num29 = (int)Vector3.Distance(LocalPlayer.Entity.transform.position, position8);
                                int num30 = (int)Vector3.Distance(MainCamera.mainCamera.transform.position, position8);
                                Vector2 vector16 = vector15;
                                vector16.y = (float)Screen.height - vector16.y;
                                Vector3 position9 = basePlayer5.transform.position;
                                Vector3 vector17 = new Vector3(basePlayer5.transform.position.x, basePlayer5.transform.position.y, basePlayer5.transform.position.z + 1f);
                                Renders.DrawString(new Vector2(vector15.x, (float)Screen.height - vector15.y), string.Format("{0} [{1}]", "Учёный", num29), Color.red, true, 12, true, 0);
                                Renders.DrawHealth(vector16, basePlayer5.Health(), true);
                                bool esp_Boxes3 = HackManager.ESP_Boxes;
                                if (esp_Boxes3)
                                {
                                    bool esp_Boxes3D5 = HackManager.ESP_Boxes3D;
                                    if (esp_Boxes3D5)
                                    {
                                        DrawBoundingBox(basePlayer5, Color.yellow, true);
                                    }
                                    else
                                    {
                                        DrawBoundingBox(basePlayer5, Color.yellow, true);
                                    }
                                }

                                bool esp_Bones3 = HackManager.ESP_Bones;
                                if (esp_Bones3)
                                {
                                    DrawBones(basePlayer5, Color.yellow);
                                }
                            }
                        }
                    }
                }

                bool aim_DrawFov = HackManager.Aim_DrawFov;
                if (aim_DrawFov)
                {
                    HackManager.FovRadius = (int)((float)Screen.width * ((float)HackManager.Aim_Fov / ConVar.Graphics.fov)) / 2;
                    Drawing.DrawCircle(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), HackManager.FovRadius, HackManager.Color_AimDrawFov, 1f, true, 30);
                }

                bool aim_Xhair = HackManager.Aim_Xhair;
                if (aim_Xhair)
                {
                    Drawing.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2 - 9)), new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2 + 9)), HackManager.Color_AimXhair, 1f, true);
                    Drawing.DrawLine(new Vector2((float)(Screen.width / 2 - 9), (float)(Screen.height / 2)), new Vector2((float)(Screen.width / 2 + 9), (float)(Screen.height / 2)), HackManager.Color_AimXhair, 1f, true);
                }
            }
        }
        catch
        {
        }
    }

    // Token: 0x06000036 RID: 54 RVA: 0x00002176 File Offset: 0x00000376
    IEnumerator aim()
    {
        Vector3 targetLastPosition = Vector3.zero;
        Vector3 targetVelocity = Vector3.zero;
        global::BasePlayer aimTarget = null;
        bool aimHeli = false;
        float lasttime = 0f;
        Vector2 zero = Vector2.zero;
        Vector3 target_AimPos = Vector3.zero;
        new Dictionary<global::BasePlayer, int>();
        for (; ; )
        {
            bool flag = HackManager.localplayer == null || !HackManager.localplayer || !HackManager.Aim_Active || !UnityEngine.Input.GetKey(HackManager.aimbotKey);
            if (flag)
            {
                aimHeli = false;
                aimTarget = null;
                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                HackManager.localplayer.GetType().GetMethod("PointSeePoint", BindingFlags.Instance | BindingFlags.NonPublic);
                Vector2 centerScreen = new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f);
                bool flag2 = HackManager.Aim_Heli && validAimWeapon();
                if (flag2)
                {
                    foreach (BaseHelicopter player in HackManager.heli)
                    {
                        bool flag3 = player != null;
                        if (flag3)
                        {
                            Vector3 helirotortop2 = new Vector3(player.model.transform.position.x, player.model.transform.position.y, player.model.transform.position.z);
                            Vector3 onScreen = player.transform.position - MainCamera.mainCamera.transform.position;
                            int distanceFromCenter = (int)Vector2.Distance(MainCamera.mainCamera.WorldToScreenPoint(helirotortop2), centerScreen);
                            aimHeli = (distanceFromCenter <= HackManager.FovRadius && Vector3.Dot(MainCamera.mainCamera.transform.TransformDirection(Vector3.forward), onScreen) > 0f);
                            helirotortop2 = default(Vector3);
                            onScreen = default(Vector3);
                        }
                        else
                        {
                            aimHeli = false;
                        }
                    }

                    HashSet<BaseHelicopter>.Enumerator enumerator = default(HashSet<BaseHelicopter>.Enumerator);
                    bool flag4 = aimTarget != null || aimHeli;
                    if (flag4)
                    {
                        foreach (BaseHelicopter player2 in HackManager.heli)
                        {
                            Vector3 zero2 = Vector3.zero;
                            Vector3 helirotortop3 = new Vector3(player2.model.transform.position.x, player2.model.transform.position.y, player2.model.transform.position.z);
                            Vector3 inverse = base.transform.InverseTransformDirection(helirotortop3 - targetLastPosition);
                            float currenttime = UnityEngine.Time.time;
                            bool flag5 = lasttime != 0f && currenttime != lasttime;
                            if (flag5)
                            {
                                Vector3 b = inverse / (currenttime - lasttime);
                                targetVelocity = Vector3.Lerp(targetVelocity, b, 0.1f);
                            }

                            lasttime = currenttime;
                            targetLastPosition = helirotortop3;
                            target_AimPos = helirotortop3;

                            zero2 = default(Vector3);
                            helirotortop3 = default(Vector3);
                            inverse = default(Vector3);
                        }

                        HashSet<BaseHelicopter>.Enumerator enumerator2 = default(HashSet<BaseHelicopter>.Enumerator);
                        float traveltime = Vector3.Distance(LocalPlayer.Entity.transform.position, target_AimPos) / GetProjectileSpeed();
                        target_AimPos.x += targetVelocity.x * traveltime;
                        target_AimPos.y += targetVelocity.y * traveltime;
                        target_AimPos.z += targetVelocity.z * traveltime;
                        bool aim_NoDrop = HackManager.Aim_NoDrop;
                        if (aim_NoDrop)
                        {
                            target_AimPos.y += (float)(4.90500020980835 * (double)traveltime * (double)traveltime);
                        }

                        Vector3 relative = MainCamera.mainCamera.transform.position - target_AimPos;
                        double pitch2 = Math.Asin((double)(relative.y / relative.magnitude));
                        double yaw2 = 0.0 - Math.Atan2((double)relative.x, (double)(0f - relative.z));
                        yaw2 *= 57.295780181884766;
                        pitch2 *= 57.295780181884766;
                        Vector3 viewangles2 = new Vector3((float)pitch2, (float)yaw2, 0f);
                        viewangles2 = ClampAngles(viewangles2);
                        LocalPlayer.Entity.input.SetViewVars(viewangles2);
                        relative = default(Vector3);
                        enumerator2 = default(HashSet<BaseHelicopter>.Enumerator);
                        relative = default(Vector3);
                        viewangles2 = default(Vector3);
                    }

                    enumerator = default(HashSet<BaseHelicopter>.Enumerator);
                }

                yield return new WaitForSeconds(0.075f);
                centerScreen = default(Vector2);
            }
        }

        yield break;
    }

    // Token: 0x06000037 RID: 55 RVA: 0x00008E8C File Offset: 0x0000708C
    public bool validAimWeapon()
    {
        bool flag = !(HackManager.localplayer.GetHeldEntity() == null);
        bool result;
        if (flag)
        {
            var activeItem = LocalPlayer.Entity.Belt.GetActiveItem();
            bool flag2 = false;
            bool flag3 = activeItem != null && (activeItem.info.shortname.Contains("bow") || activeItem.info.shortname.Contains("smg.") || activeItem.info.shortname.Contains("pistol.") || activeItem.info.shortname.Contains("lmg.") || activeItem.info.shortname.Contains("rifle"));
            if (flag3)
            {
                var shortname = activeItem.info.shortname;
            }

            result = flag2;
        }
        else
        {
            result = false;
        }

        return result;
    }

    // Token: 0x06000038 RID: 56 RVA: 0x000091EC File Offset: 0x000073EC
    public float GetProjectileSpeed()
    {
        bool flag = !(HackManager.localplayer.GetHeldEntity() == null);
        float result;
        if (flag)
        {
            float num = 300f;
            var activeItem = LocalPlayer.Entity.Belt.GetActiveItem();
            bool flag2 = activeItem != null && (activeItem.info.shortname.Contains("bow") || activeItem.info.shortname.Contains("smg.") || activeItem.info.shortname.Contains("pistol.") || activeItem.info.shortname.Contains("lmg.") || activeItem.info.shortname.Contains("rifle"));
            if (flag2)
            {
                var shortname = activeItem.info.shortname;
            }

            result = num;
        }
        else
        {
            result = 375f;
        }

        return result;
    }

    // Token: 0x06000039 RID: 57 RVA: 0x000095C8 File Offset: 0x000077C8
    public static void UpdateLookAt()
    {
        RaycastHit raycastHit = default(RaycastHit);
        bool flag = UnityEngine.Physics.Raycast(LocalPlayer.Entity.eyes.HeadRay(), out raycastHit);
        if (flag)
        {
            HackManager.lookPoint = raycastHit.point;
            HackManager.lookingAt = raycastHit.transform.gameObject;
        }
    }

    // Token: 0x0600003A RID: 58 RVA: 0x00009618 File Offset: 0x00007818
    void Update()
    {
        deltaTime += (UnityEngine.Time.deltaTime - deltaTime) * 0.1f;
        bool keyDown = UnityEngine.Input.GetKeyDown(HackManager.enableHackKey);
        if (keyDown)
        {
            HackManager.enableHack = !HackManager.enableHack;
        }

        bool keyDown2 = UnityEngine.Input.GetKeyDown(HackManager.showMenuKey);
        if (keyDown2)
        {
            HackManager.showMenu = !HackManager.showMenu;
        }

        bool keyDown3 = UnityEngine.Input.GetKeyDown(KeyCode.Z);
        if (keyDown3)
        {
            HackManager.UpdateLookAt();
            bool flag = HackManager.lookingAt != null;
            if (flag)
            {
                Vector3 a = HackManager.lookingAt.transform.position - LocalPlayer.Entity.transform.position;
                a.Normalize();
                HackManager.lookingAt.transform.position -= a / 2f;
            }
        }

        bool keyDown4 = UnityEngine.Input.GetKeyDown(KeyCode.Mouse2);
        if (keyDown4)
        {
            HackManager.UpdateLookAt();
            var gameObject = HackManager.lookingAt;
            bool flag2 = gameObject != null && !gameObject.name.Contains("player");
            if (flag2)
            {
                gameObject.SetActive(false);
                HackManager.ViewerCaches.Add(gameObject);
            }
        }

        bool keyDown5 = UnityEngine.Input.GetKeyDown(KeyCode.Keypad6);
        if (keyDown5)
        {
            foreach (GameObject gameObject2 in HackManager.ViewerCaches)
                gameObject2.SetActive(true);


            HackManager.ViewerCaches.Clear();
        }

        bool misc_OreHotSpot = HackManager.Misc_OreHotSpot;
        if (misc_OreHotSpot)
        {
            OreHotSpot();
        }

        bool misc_TreeMarker = HackManager.Misc_TreeMarker;
        if (misc_TreeMarker)
        {
            TreeMarker();
        }

        bool misc_MeleeAim = HackManager.Misc_MeleeAim;
        if (misc_MeleeAim)
        {
            MeleeSilent();
        }

        bool keyDown6 = UnityEngine.Input.GetKeyDown(HackManager.showMenuKey);
        if (keyDown6)
        {
            new Form1().Show();
        }

        bool key = UnityEngine.Input.GetKey(HackManager.AIMMM);
        if (key)
        {
            silextxd();
        }
    }

    // Token: 0x0600003B RID: 59 RVA: 0x00002185 File Offset: 0x00000385
    IEnumerator silextxd()
    {
        for (; ; )
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.X) && HackManager.Enable)
            {
                if (UnityEngine.Time.time < attackCooldown)
                {
                    Debug.Log("Time.time < attackCooldown");
                    yield return null;
                }

                if (LocalPlayer.Entity != null)
                {
                    float num = float.MaxValue;
                    var heldGun = LocalPlayer.Entity.GetHeldEntity() as global::BaseProjectile;
                    if (global::BasePlayer.VisiblePlayerList != null)
                    {
                        foreach (global::BasePlayer basePlayer in global::BasePlayer.VisiblePlayerList)
                        {
                            if (!basePlayer.IsLocalPlayer() && !basePlayer.IsDead())
                            {
                                Vector3 vector = MainCamera.mainCamera.WorldToScreenPoint(basePlayer.FindBone("head").position);
                                if (vector.z > 0f)
                                {
                                    Vector2 b = new Vector2(vector.x, (float)Screen.height - vector.y);
                                    float num2 = Mathf.Abs(Vector2.Distance(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), b));
                                    if (num2 <= num)
                                    {
                                        num = num2;
                                        HackManager.target = basePlayer;
                                    }

                                    b = default(Vector2);
                                }

                                vector = default(Vector3);
                            }
                        }
                    }

                    if (HackManager.target != null && HackManager.target.IsValid() && HackManager.target.IsAlive())
                    {
                        if (heldGun != null && heldGun.IsValid())
                        {
                            if (heldGun.primaryMagazine.contents <= 0)
                            {
                                Debug.Log("Out of ammo!");
                                yield return null;
                            }
                            else
                            {
                                var component = heldGun.GetComponent<ViewModel>();
                                if (Vector3.Distance(LocalPlayer.Entity.transform.position, HackManager.target.FindBone("head").transform.position) < 40f)
                                {
                                    using (PlayerProjectileAttack projectileAttack1 = Facepunch.Pool.Get<PlayerProjectileAttack>())
                                    {
                                        Debug.Log("Close Silentaim");
                                        if (component != null)
                                        {
                                            component.Play("attack");
                                        }

                                        if (heldGun.worldModelAnimator != null)
                                        {
                                            heldGun.worldModelAnimator.SetTrigger("fire");
                                        }

                                        heldGun.primaryMagazine.contents--;
                                        heldGun.CancelInvoke("UpdateAmmoDisplay");
                                        var entity = LocalPlayer.Entity;

                                        projectileAttack1.playerAttack = Facepunch.Pool.Get<PlayerAttack>();
                                        projectileAttack1.hitDistance = Vector3.Distance(LocalPlayer.Entity.transform.position, HackManager.target.FindBone("head").transform.position);
                                        projectileAttack1.hitVelocity = Vector3.forward;

                                        projectileAttack1.playerAttack.attack = new Attack
                                        {
                                            hitID = HackManager.target.net.ID,
                                            hitBone = 698017942u,
                                            hitPartID = 698017942u,
                                            hitMaterialID = 0u,
                                            hitItem = 0u,
                                            hitPositionLocal = LocalPlayer.Entity.transform.position,
                                            hitNormalLocal = LocalPlayer.Entity.transform.position,
                                            hitPositionWorld = LocalPlayer.Entity.transform.position,
                                            hitNormalWorld = LocalPlayer.Entity.transform.position,
                                            pointStart = LocalPlayer.Entity.transform.position,
                                            pointEnd = HackManager.target.FindBone("head").transform.position
                                        };
                                        var projectileShoot = new ProjectileShoot();
                                        projectileShoot.ammoType = heldGun.primaryMagazine.ammoType.itemid;
                                        projectileShoot.projectiles = new List<ProjectileShoot.Projectile>();
                                        var projectile = new ProjectileShoot.Projectile();

                                        projectile.seed = LocalPlayer.Entity.NewProjectileSeed();
                                        projectile.startPos = LocalPlayer.Entity.transform.position;
                                        projectile.startVel = Vector3.forward;
                                        if (projectile != null && projectileShoot.projectiles != null)
                                        {
                                            projectileShoot.projectiles.Add(projectile);
                                        }

                                        float distToTarget = Vector3.Distance(LocalPlayer.Entity.transform.position, HackManager.target.transform.position);
                                        float num4 = distToTarget / 300f;
                                        heldGun.ServerRPC<ProjectileShoot>("CLProject", projectileShoot);
                                        yield return new WaitForSeconds(1.5f);
                                        LocalPlayer.Entity.ServerRPC<PlayerProjectileAttack>("OnProjectileAttack", projectileAttack1);
                                        if (heldGun.GetItem().info.displayName.english.Contains("Bow") || heldGun.GetItem().info.displayName.english.Contains("Crossbow"))
                                        {
                                            if (distToTarget < 250f)
                                            {
                                                yield return new WaitForSeconds(1.5f);
                                            }
                                            else
                                            {
                                                yield return new WaitForSeconds(1.5f);
                                            }

                                            LocalPlayer.Entity.ServerRPC<PlayerProjectileAttack>("OnProjectileAttack", projectileAttack1);
                                        }
                                        else
                                        {
                                            yield return new WaitForSeconds(1.5f);
                                            LocalPlayer.Entity.ServerRPC<PlayerProjectileAttack>("OnProjectileAttack", projectileAttack1);
                                        }
                                    }

                                    PlayerProjectileAttack projectileAttack = null;
                                }
                            }

                            attackCooldown = UnityEngine.Time.time + heldGun.repeatDelay;
                        }
                        else
                        {
                            Debug.Log("HeldGun = null");
                        }
                    }

                    heldGun = null;
                    heldGun = null;
                }
            }

            yield return null;
        }

        yield break;
        yield break;
    }

    // Token: 0x0600003C RID: 60 RVA: 0x00009834 File Offset: 0x00007A34
    void Start()
    {
        Drawing.Initialize();
        base.StartCoroutine(silextxd());
        base.gameObject.AddComponent<Silentxd>();
        base.StartCoroutine(CalculatePositions());
        base.StartCoroutine(MiscFuncs());
        base.StartCoroutine(aim());
        base.StartCoroutine(FastHeal());
        base.StartCoroutine(Autoloot());
        base.StartCoroutine(Removals());
        HackManager.ore = new HashSet<OreResourceEntity>();
        HackManager.animal = new HashSet<BaseNpc>();
        HackManager.worlditem = new HashSet<global::WorldItem>();
        HackManager.heli = new HashSet<BaseHelicopter>();
        HackManager.airdrop = new HashSet<StorageContainer>();
        HackManager.lootbox = new HashSet<StorageContainer>();
        HackManager.stash = new HashSet<StorageContainer>();
        HackManager.cupboard = new HashSet<BuildingPrivlidge>();
        HackManager.corpse = new HashSet<global::LootableCorpse>();
        HackManager.traps = new HashSet<StorageContainer>();
        HackManager.boxs = new HashSet<StorageContainer>();
        HackManager.medtool = new HashSet<MedicalTool>();
        HackManager.collect = new HashSet<CollectibleEntity>();
        HackManager.orehotspot = new HashSet<OreHotSpot>();
        HackManager.explos = new HashSet<TimedExplosive>();
        HackManager.loot = new HashSet<ItemIcon>();
        HackManager.treemarker = new HashSet<TreeEntity>();
        base.StartCoroutine(HackManager.GetEntities());
        byte[] orig = new byte[]
        {
            85,
            72,
            139,
            236,
            86,
            87,
            65,
            86,
            72,
            131,
            236,
            8
        };
        timedaction = new DumbHook(typeof(ItemIcon), "SetTimedAction", typeof(HackManager), "SetTimedAction", typeof(HackManager), "SetTimedActionTrampoline", orig);
        timedaction.Hook();
    }

    // Token: 0x0600003D RID: 61 RVA: 0x00002194 File Offset: 0x00000394
    public static IEnumerator GetEntities()
    {
        for (; ; )
        {
            bool flag = LocalPlayer.Entity != null && LocalPlayer.Entity.IsValid() && global::BaseNetworkable.clientEntities != null;
            if (flag)
            {
                HackManager.ore.Clear();
                HackManager.animal.Clear();
                HackManager.worlditem.Clear();
                HackManager.heli.Clear();
                HackManager.airdrop.Clear();
                HackManager.lootbox.Clear();
                HackManager.stash.Clear();
                HackManager.cupboard.Clear();
                HackManager.corpse.Clear();
                HackManager.traps.Clear();
                HackManager.boxs.Clear();
                HackManager.medtool.Clear();
                HackManager.orehotspot.Clear();
                HackManager.explos.Clear();
                HackManager.collect.Clear();
                HackManager.treemarker.Clear();
                foreach (global::BaseNetworkable clientEntity in global::BaseNetworkable.clientEntities)
                {
                    bool flag2 = clientEntity is OreResourceEntity && clientEntity != null && HackManager.ore != null && (clientEntity as OreResourceEntity).IsValid();
                    if (flag2)
                    {
                        HackManager.ore.Add(clientEntity as OreResourceEntity);
                    }

                    bool flag3 = clientEntity is BaseNpc && clientEntity != null && HackManager.animal != null && (clientEntity as BaseNpc).IsValid();
                    if (flag3)
                    {
                        HackManager.animal.Add(clientEntity as BaseNpc);
                    }

                    bool flag4 = clientEntity is global::WorldItem && clientEntity != null && HackManager.worlditem != null && (clientEntity as global::WorldItem).IsValid();
                    if (flag4)
                    {
                        HackManager.worlditem.Add(clientEntity as global::WorldItem);
                    }

                    bool flag5 = clientEntity is BaseHelicopter && clientEntity != null && HackManager.heli != null && (clientEntity as BaseHelicopter).IsValid();
                    if (flag5)
                    {
                        HackManager.heli.Add(clientEntity as BaseHelicopter);
                    }

                    bool flag6 = clientEntity is StorageContainer && clientEntity != null && HackManager.airdrop != null && (clientEntity as StorageContainer).IsValid();
                    if (flag6)
                    {
                        HackManager.airdrop.Add(clientEntity as StorageContainer);
                    }

                    bool flag7 = clientEntity is StorageContainer && clientEntity != null && HackManager.lootbox != null && (clientEntity as StorageContainer).IsValid();
                    if (flag7)
                    {
                        HackManager.lootbox.Add(clientEntity as StorageContainer);
                    }

                    bool flag8 = clientEntity is StorageContainer && clientEntity != null && HackManager.stash != null && (clientEntity as StorageContainer).IsValid();
                    if (flag8)
                    {
                        HackManager.stash.Add(clientEntity as StorageContainer);
                    }

                    bool flag9 = clientEntity is BuildingPrivlidge && clientEntity != null && HackManager.cupboard != null && (clientEntity as BuildingPrivlidge).IsValid();
                    if (flag9)
                    {
                        HackManager.cupboard.Add(clientEntity as BuildingPrivlidge);
                    }

                    bool flag10 = clientEntity is global::LootableCorpse && clientEntity != null && HackManager.corpse != null && (clientEntity as global::LootableCorpse).IsValid();
                    if (flag10)
                    {
                        HackManager.corpse.Add(clientEntity as global::LootableCorpse);
                    }

                    bool flag11 = clientEntity is StorageContainer && clientEntity != null && HackManager.traps != null && (clientEntity as StorageContainer).IsValid();
                    if (flag11)
                    {
                        HackManager.traps.Add(clientEntity as StorageContainer);
                    }

                    bool flag12 = clientEntity is StorageContainer && clientEntity != null && HackManager.boxs != null && (clientEntity as StorageContainer).IsValid();
                    if (flag12)
                    {
                        HackManager.boxs.Add(clientEntity as StorageContainer);
                    }

                    bool flag13 = clientEntity is MedicalTool && clientEntity != null && HackManager.medtool != null && (clientEntity as MedicalTool).IsValid();
                    if (flag13)
                    {
                        HackManager.medtool.Add(clientEntity as MedicalTool);
                    }

                    bool flag14 = clientEntity is OreHotSpot && clientEntity != null && HackManager.orehotspot != null && (clientEntity as OreHotSpot).IsValid();
                    if (flag14)
                    {
                        HackManager.orehotspot.Add(clientEntity as OreHotSpot);
                    }

                    bool flag15 = clientEntity is TimedExplosive && clientEntity != null && HackManager.explos != null && (clientEntity as TimedExplosive).IsValid();
                    if (flag15)
                    {
                        HackManager.explos.Add(clientEntity as TimedExplosive);
                    }

                    bool flag16 = clientEntity is CollectibleEntity && clientEntity != null && HackManager.collect != null && (clientEntity as CollectibleEntity).IsValid();
                    if (flag16)
                    {
                        HackManager.collect.Add(clientEntity as CollectibleEntity);
                    }

                    bool flag17 = clientEntity is TreeEntity && clientEntity != null && HackManager.treemarker != null && (clientEntity as TreeEntity).IsValid();
                    if (flag17)
                    {
                        HackManager.treemarker.Add(clientEntity as TreeEntity);
                    }
                }

                IEnumerator<global::BaseNetworkable> enumerator = null;
            }

            yield return new WaitForSeconds(0.5f);
        }

        yield break;
    }

    // Token: 0x0600003E RID: 62 RVA: 0x0000219C File Offset: 0x0000039C
    public void SetTimedAction(float time, Action action)
    {
        SetTimedActionTrampoline(time, action);
    }

    // Token: 0x0600003F RID: 63 RVA: 0x000099C0 File Offset: 0x00007BC0
    public float SetTimedActionTrampoline(float time, Action action)
    {
        int num = 12;
        int num2 = 9;
        int num3 = 104;
        int num4 = num3 * num - 15;
        int num5 = num4 + num;
        int num6 = num2 + num3;
        num = num2 + 12;
        num2 = num3 - 4;
        num4 = num + num2;
        num5 = num + num3 + num4;
        return 0f;
    }

    // Token: 0x06000040 RID: 64 RVA: 0x00009A08 File Offset: 0x00007C08
    public static void OnConsoleCommandfromServer(Message packet)
    {
        var text = packet.read.String();
        bool flag = text.Length >= 6;
        if (flag)
        {
            text.StartsWith("noclip");
        }
        else
        {
            packet.read.Position = 1L;
        }
    }

    // Token: 0x06000041 RID: 65 RVA: 0x000021A8 File Offset: 0x000003A8
    IEnumerator FastHeal()
    {
        for (; ; )
        {
            try
            {
                bool flag = HackManager.Misc_FastHeal && HackManager.localplayer.health <= 75f;
                if (flag)
                {
                    foreach (global::BasePlayer visiblePlayer in global::BasePlayer.VisiblePlayerList)
                    {
                        foreach (MedicalTool tool in HackManager.medtool)
                        {
                            var entity = LocalPlayer.Entity.GetHeldEntity();
                            int lock_distance5 = (int)Vector3.Distance(LocalPlayer.Entity.transform.position, entity.transform.position);
                            int lock_distance6 = (int)Vector3.Distance(LocalPlayer.Entity.transform.position, visiblePlayer.transform.position);
                            int lock_distance7 = (int)Vector3.Distance(LocalPlayer.Entity.transform.position, tool.transform.position);
                            bool flag2 = entity is MedicalTool && visiblePlayer.health <= 75f && (float)lock_distance5 <= 2f && (float)lock_distance6 <= 2f && (float)lock_distance7 <= 2f;
                            if (flag2)
                            {
                                entity.ServerRPC("UseSelf");
                            }

                            entity = null;
                        }

                        HashSet<MedicalTool>.Enumerator enumerator2 = default(HashSet<MedicalTool>.Enumerator);
                        enumerator2 = default(HashSet<MedicalTool>.Enumerator);
                    }

                    IEnumerator<global::BasePlayer> enumerator3 = null;
                }
            }
            catch
            {
            }

            yield return new WaitForSeconds(0.5f);
        }

        yield break;
    }

    // Token: 0x06000042 RID: 66 RVA: 0x000021B7 File Offset: 0x000003B7
    IEnumerator Autoloot()
    {
        for (; ; )
        {
            try
            {
                bool flag3 = HackManager.Misc_Autoloot && LocalPlayer.Entity != null;
                if (flag3)
                {
                    foreach (CollectibleEntity ores in HackManager.collect)
                    {
                        int flag2 = (int)Vector3.Distance(LocalPlayer.Entity.transform.position, ores.transform.position);
                        bool flag4 = ores != null && (float)flag2 <= 8f;
                        if (flag4)
                        {
                            ores.ServerRPC("Pickup");
                        }
                    }

                    HashSet<CollectibleEntity>.Enumerator enumerator = default(HashSet<CollectibleEntity>.Enumerator);
                    enumerator = default(HashSet<CollectibleEntity>.Enumerator);
                }
            }
            catch
            {
            }

            yield return new WaitForSeconds(1f);
        }

        yield break;
    }

    // Token: 0x06000043 RID: 67 RVA: 0x00009A54 File Offset: 0x00007C54
    void OreHotSpot()
    {
        bool flag = HackManager.Misc_OreHotSpot && LocalPlayer.Entity && LocalPlayer.Entity.IsValid() && UnityEngine.Time.time >= nextAttack;
        if (flag)
        {
            OreHotSpot oreHotSpot = null;
            bool flag2 = global::BasePlayer.VisiblePlayerList != null;
            if (flag2)
            {
                foreach (OreHotSpot oreHotSpot2 in HackManager.orehotspot)
                    oreHotSpot = oreHotSpot2;

            }

            bool flag3 = oreHotSpot != null && oreHotSpot.IsValid();
            if (flag3)
            {
                Vector3 position = oreHotSpot.transform.position;
                var heldEntity = LocalPlayer.Entity.GetHeldEntity();
                bool flag4 = heldEntity != null && heldEntity.IsValid();
                if (flag4)
                {
                    float num = Vector3.Distance(LocalPlayer.Entity.transform.position, oreHotSpot.transform.position);
                    var baseMelee = heldEntity as BaseMelee;
                    bool flag5 = baseMelee != null && baseMelee.IsValid() && num < 5f;
                    if (flag5)
                    {
                        using (PlayerAttack playerAttack = Facepunch.Pool.Get<PlayerAttack>())
                        {
                            playerAttack.attack = new Attack
                            {
                                hitID = oreHotSpot.net.ID,
                                hitBone = 0u,
                                hitPartID = 0u,
                                hitMaterialID = 0u,
                                hitItem = 0u,
                                hitPositionWorld = oreHotSpot.transform.position,
                                hitPositionLocal = new Vector3(-0.1f, -1f, 0f),
                                hitNormalLocal = new Vector3(0f, -1f, 0f),
                                hitNormalWorld = (MainCamera.mainCamera.transform.position - oreHotSpot.transform.position).normalized,
                                pointStart = MainCamera.mainCamera.transform.position,
                                pointEnd = oreHotSpot.transform.position
                            };
                            baseMelee.ServerRPC<PlayerAttack>("PlayerAttack", playerAttack);
                        }

                        nextAttack = UnityEngine.Time.time + baseMelee.repeatDelay;
                    }
                }
            }
        }
    }

    // Token: 0x06000044 RID: 68 RVA: 0x00009CB4 File Offset: 0x00007EB4
    void MeleeSilent()
    {
        bool flag = HackManager.Misc_MeleeAim && LocalPlayer.Entity && LocalPlayer.Entity.IsValid() && UnityEngine.Time.time >= nextAttack;
        if (flag)
        {
            var baseProjectile = LocalPlayer.Entity.GetHeldEntity() as global::BaseProjectile;
            global::BasePlayer basePlayer = null;
            float num = 99999f;
            bool flag2 = global::BasePlayer.VisiblePlayerList != null;
            if (flag2)
            {
                foreach (global::BasePlayer basePlayer2 in global::BasePlayer.VisiblePlayerList)
                {
                    bool flag3 = !basePlayer2.IsDestroyed;
                    if (flag3)
                    {
                        Vector3 vector = MainCamera.mainCamera.WorldToScreenPoint(basePlayer2.transform.position);
                        bool flag4 = vector.z > 0f;
                        if (flag4)
                        {
                            Vector2 vector2 = new Vector2(vector.x, (float)Screen.height - vector.y);
                            float num2 = Mathf.Abs(Vector2.Distance(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(vector2.x, (float)Screen.height - vector2.y)));
                            bool flag5 = num2 <= num;
                            if (flag5)
                            {
                                num = num2;
                                basePlayer = basePlayer2;
                            }
                        }
                    }
                }
            }

            bool flag6 = basePlayer != null && basePlayer.IsValid() && LocalPlayer.Entity != null;
            if (flag6)
            {
                Vector3 position = basePlayer.transform.position;
                var heldEntity = LocalPlayer.Entity.GetHeldEntity();
                bool flag7 = heldEntity != null && heldEntity.IsValid();
                if (flag7)
                {
                    float num3 = Vector3.Distance(LocalPlayer.Entity.transform.position, basePlayer.transform.position);
                    var baseMelee = heldEntity as BaseMelee;
                    bool flag8 = Vector3.Distance(LocalPlayer.Entity.transform.position, basePlayer.FindBone("head").transform.position) < 7f;
                    if (flag8)
                    {
                        using (PlayerAttack playerAttack = Facepunch.Pool.Get<PlayerAttack>())
                        {
                            playerAttack.attack = new Attack
                            {
                                hitID = basePlayer.net.ID,
                                hitBone = 698017942u,
                                hitPartID = 0u,
                                hitMaterialID = 0u,
                                hitItem = 0u,
                                hitPositionWorld = LocalPlayer.Entity.transform.position,
                                hitPositionLocal = new Vector3(-0.1f, -1f, 0f),
                                hitNormalLocal = new Vector3(0f, -1f, 0f),
                                hitNormalWorld = (MainCamera.mainCamera.transform.position - basePlayer.transform.position).normalized,
                                pointStart = MainCamera.mainCamera.transform.position,
                                pointEnd = basePlayer.FindBone("head").transform.position
                            };
                            baseMelee.ServerRPC<PlayerAttack>("PlayerAttack", playerAttack);
                        }

                        nextAttack = UnityEngine.Time.time + baseMelee.repeatDelay;
                    }
                }
            }
        }
    }

    // Token: 0x06000045 RID: 69 RVA: 0x0000A014 File Offset: 0x00008214
    void TreeMarker()
    {
        bool flag = HackManager.Misc_TreeMarker && LocalPlayer.Entity && LocalPlayer.Entity.IsValid() && UnityEngine.Time.time >= nextAttack;
        if (flag)
        {
            TreeEntity treeEntity = null;
            bool flag2 = global::BasePlayer.VisiblePlayerList != null;
            if (flag2)
            {
                foreach (TreeEntity treeEntity2 in HackManager.treemarker)
                    treeEntity = treeEntity2;

            }

            bool flag3 = treeEntity != null && treeEntity.IsValid();
            if (flag3)
            {
                Vector3 position = treeEntity.transform.position;
                var heldEntity = LocalPlayer.Entity.GetHeldEntity();
                bool flag4 = heldEntity != null && heldEntity.IsValid();
                if (flag4)
                {
                    float num = Vector3.Distance(LocalPlayer.Entity.transform.position, treeEntity.transform.position);
                    var baseMelee = heldEntity as BaseMelee;
                    bool flag5 = baseMelee != null && baseMelee.IsValid() && num < 5f;
                    if (flag5)
                    {
                        using (PlayerAttack playerAttack = Facepunch.Pool.Get<PlayerAttack>())
                        {
                            playerAttack.attack = new Attack
                            {
                                hitID = treeEntity.net.ID,
                                hitBone = 954334883u,
                                hitPartID = 0u,
                                hitMaterialID = 0u,
                                hitItem = 0u,
                                hitPositionWorld = treeEntity.transform.position,
                                hitPositionLocal = new Vector3(-0.1f, -1f, 0f),
                                hitNormalLocal = new Vector3(0f, -1f, 0f),
                                hitNormalWorld = (MainCamera.mainCamera.transform.position - treeEntity.transform.position).normalized,
                                pointStart = MainCamera.mainCamera.transform.position,
                                pointEnd = treeEntity.transform.position
                            };
                            baseMelee.ServerRPC<PlayerAttack>("PlayerAttack", playerAttack);
                        }

                        nextAttack = UnityEngine.Time.time + baseMelee.repeatDelay;
                    }
                }
            }
        }
    }

    // Token: 0x06000046 RID: 70 RVA: 0x000021C6 File Offset: 0x000003C6
    IEnumerator MiscFuncs()
    {
        global::BasePlayer.PlayerFlags oldFlags = (global::BasePlayer.PlayerFlags)0;
        bool flickerHooked = false;
        float oldGravity = 0f;
        float oldSwimGravity = 0f;
        bool multiJump_Hooked = true;
        bool speed_Hooked = true;
        for (; ; )
        {
            try
            {
                bool misc_MultiJump = HackManager.Misc_MultiJump;
                if (misc_MultiJump)
                {
                    bool flag = !multiJump_Hooked;
                    if (flag)
                    {
                        multiJump_Hooked = false;
                    }
                }
                else
                {
                    bool flag2 = multiJump_Hooked;
                    if (flag2)
                    {
                        multiJump_Hooked = true;
                    }
                }

                bool misc_Speed = HackManager.Misc_Speed;
                if (misc_Speed)
                {
                    bool flag3 = !speed_Hooked;
                    if (flag3)
                    {
                        speed_Hooked = false;
                    }
                }
                else
                {
                    bool flag4 = speed_Hooked;
                    if (flag4)
                    {
                        speed_Hooked = true;
                    }
                }

                bool flag5 = HackManager.localplayer != null;
                if (flag5)
                {
                    bool misc_AdminPriv = HackManager.Misc_AdminPriv;
                    if (misc_AdminPriv)
                    {
                        bool flag6 = HackManager.localplayer.playerFlags != (global::BasePlayer.PlayerFlags.IsAdmin | global::BasePlayer.PlayerFlags.Connected);
                        if (flag6)
                        {
                            oldFlags = HackManager.localplayer.playerFlags;
                            HackManager.localplayer.playerFlags = (global::BasePlayer.PlayerFlags.IsAdmin | global::BasePlayer.PlayerFlags.Connected);
                        }
                    }
                    else
                    {
                        bool flag7 = HackManager.localplayer.playerFlags == (global::BasePlayer.PlayerFlags.IsAdmin | global::BasePlayer.PlayerFlags.Connected) && oldFlags > (global::BasePlayer.PlayerFlags)0;
                        if (flag7)
                        {
                            HackManager.localplayer.playerFlags = oldFlags;
                        }
                    }

                    bool misc_Spider = HackManager.Misc_Spider;
                    if (misc_Spider)
                    {
                        var getGroundAngle = typeof(PlayerWalkMovement).GetField("groundAngleNew", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField);
                        getGroundAngle.SetValue(HackManager.localplayer.movement, 0f);
                        getGroundAngle = null;
                    }

                    bool misc_Freeze = HackManager.Misc_Freeze;
                    if (misc_Freeze)
                    {
                        bool flag8 = !flickerHooked;
                        if (flag8)
                        {
                            flickerHooked = true;
                        }

                        TOD_Sky.Instance.Cycle.Hour = (float)HackManager.Misc_FreezeValue;
                    }
                    else
                    {
                        bool flag9 = flickerHooked;
                        if (flag9)
                        {
                            flickerHooked = false;
                        }
                    }

                    bool misc_LowGravity = HackManager.Misc_LowGravity;
                    if (misc_LowGravity)
                    {
                        bool flag10 = oldGravity == 0f;
                        if (flag10)
                        {
                            oldGravity = HackManager.localplayer.movement.GetComponent<PlayerWalkMovement>().gravityMultiplier;
                        }

                        HackManager.localplayer.movement.GetComponent<PlayerWalkMovement>().gravityMultiplier = oldGravity / 2f;
                    }
                    else
                    {
                        bool flag11 = oldGravity != 0f;
                        if (flag11)
                        {
                            HackManager.localplayer.movement.GetComponent<PlayerWalkMovement>().gravityMultiplier = oldGravity;
                        }
                    }

                    bool misc_NoSink = HackManager.Misc_NoSink;
                    if (misc_NoSink)
                    {
                        bool flag12 = oldSwimGravity == 0f;
                        if (flag12)
                        {
                            oldSwimGravity = HackManager.localplayer.movement.GetComponent<PlayerWalkMovement>().gravityMultiplierSwimming;
                        }

                        HackManager.localplayer.movement.GetComponent<PlayerWalkMovement>().gravityMultiplierSwimming = 0f;
                    }
                    else
                    {
                        bool flag13 = oldSwimGravity != 0f;
                        if (flag13)
                        {
                            HackManager.localplayer.movement.GetComponent<PlayerWalkMovement>().gravityMultiplier = oldSwimGravity;
                        }
                    }

                    bool misc_InstantRevive = HackManager.Misc_InstantRevive;
                    if (misc_InstantRevive)
                    {
                        global::BasePlayer targetPlayer = null;
                        bool flag14 = HackManager.localplayer.lookingAt.GetComponent<global::BasePlayer>() != null;
                        if (flag14)
                        {
                            targetPlayer = HackManager.localplayer.lookingAt.GetComponent<global::BasePlayer>();
                        }

                        bool flag15 = targetPlayer != null && targetPlayer.HasPlayerFlag(global::BasePlayer.PlayerFlags.Wounded) && Vector3.Distance(HackManager.localplayer.transform.position, targetPlayer.transform.position) <= 2f && UnityEngine.Input.GetKey(KeyCode.E);
                        if (flag15)
                        {
                            targetPlayer.ServerRPC("RPC_Assist");
                        }

                        targetPlayer = null;
                    }
                }
            }
            catch (Exception)
            {
            }

            yield return new WaitForSeconds(0.01f);
        }

        yield break;
    }

    // Token: 0x06000047 RID: 71 RVA: 0x000021D5 File Offset: 0x000003D5
    IEnumerator CalculatePositions()
    {
        for (; ; )
        {
            HackManager.PlayerBones.Clear();
            HackManager.PlayerBoxes.Clear();
            foreach (global::BasePlayer visiblePlayer in global::BasePlayer.VisiblePlayerList)
            {
                HackManager.BonePositions bp = default(HackManager.BonePositions);
                Vector3 head3 = visiblePlayer.FindBone("head").position;
                Vector3 spine3 = visiblePlayer.FindBone("spine4").position;
                Vector3 position = visiblePlayer.FindBone("l_clavicle").position;
                Vector3 l_upper = visiblePlayer.FindBone("l_upperarm").position;
                Vector3 l_fore = visiblePlayer.FindBone("l_forearm").position;
                Vector3 l_hand = visiblePlayer.FindBone("l_hand").position;
                Vector3 position2 = visiblePlayer.FindBone("r_clavicle").position;
                Vector3 r_upper = visiblePlayer.FindBone("r_upperarm").position;
                Vector3 r_fore = visiblePlayer.FindBone("r_forearm").position;
                Vector3 r_hand = visiblePlayer.FindBone("r_hand").position;
                Vector3 pelvis = visiblePlayer.FindBone("pelvis").position;
                Vector3 l_hip = visiblePlayer.FindBone("l_hip").position;
                Vector3 l_knee = visiblePlayer.FindBone("l_knee").position;
                Vector3 position3 = visiblePlayer.FindBone("l_ankle_scale").position;
                Vector3 l_foot = visiblePlayer.FindBone("l_foot").position;
                Vector3 r_hip = visiblePlayer.FindBone("r_hip").position;
                Vector3 r_knee = visiblePlayer.FindBone("r_knee").position;
                Vector3 position4 = visiblePlayer.FindBone("r_ankle_scale").position;
                Vector3 r_foot = visiblePlayer.FindBone("r_foot").position;
                bool flag = VisibleOnScreen(head3) && VisibleOnScreen(spine3) && VisibleOnScreen(l_upper) && VisibleOnScreen(r_upper) && VisibleOnScreen(l_fore) && VisibleOnScreen(r_fore) && VisibleOnScreen(l_hand) && VisibleOnScreen(r_hand) && VisibleOnScreen(pelvis) && VisibleOnScreen(l_hip) && VisibleOnScreen(r_hip) && VisibleOnScreen(l_knee) && VisibleOnScreen(r_knee) && VisibleOnScreen(l_foot) && VisibleOnScreen(r_foot);
                if (flag)
                {
                    Vector3 head4 = MainCamera.mainCamera.WorldToScreenPoint(head3);
                    head4.y = (float)Screen.height - head4.y;
                    Vector3 spine4 = MainCamera.mainCamera.WorldToScreenPoint(spine3);
                    spine4.y = (float)Screen.height - spine4.y;
                    Vector3 l_upper2 = MainCamera.mainCamera.WorldToScreenPoint(l_upper);
                    l_upper2.y = (float)Screen.height - l_upper2.y;
                    Vector3 r_upper2 = MainCamera.mainCamera.WorldToScreenPoint(r_upper);
                    r_upper2.y = (float)Screen.height - r_upper2.y;
                    Vector3 l_fore2 = MainCamera.mainCamera.WorldToScreenPoint(l_fore);
                    l_fore2.y = (float)Screen.height - l_fore2.y;
                    Vector3 r_fore2 = MainCamera.mainCamera.WorldToScreenPoint(r_fore);
                    r_fore2.y = (float)Screen.height - r_fore2.y;
                    Vector3 l_hand2 = MainCamera.mainCamera.WorldToScreenPoint(l_hand);
                    l_hand2.y = (float)Screen.height - l_hand2.y;
                    Vector3 r_hand2 = MainCamera.mainCamera.WorldToScreenPoint(r_hand);
                    r_hand2.y = (float)Screen.height - r_hand2.y;
                    Vector3 l_hip2 = MainCamera.mainCamera.WorldToScreenPoint(l_hip);
                    l_hip2.y = (float)Screen.height - l_hip2.y;
                    Vector3 r_hip2 = MainCamera.mainCamera.WorldToScreenPoint(r_hip);
                    r_hip2.y = (float)Screen.height - r_hip2.y;
                    Vector3 l_knee2 = MainCamera.mainCamera.WorldToScreenPoint(l_knee);
                    l_knee2.y = (float)Screen.height - l_knee2.y;
                    Vector3 r_knee2 = MainCamera.mainCamera.WorldToScreenPoint(r_knee);
                    r_knee2.y = (float)Screen.height - r_knee2.y;
                    Vector3 l_foot2 = MainCamera.mainCamera.WorldToScreenPoint(l_foot);
                    l_foot2.y = (float)Screen.height - l_foot2.y;
                    Vector3 r_foot2 = MainCamera.mainCamera.WorldToScreenPoint(r_foot);
                    r_foot2.y = (float)Screen.height - r_foot2.y;
                    Vector3 pelvis2 = MainCamera.mainCamera.WorldToScreenPoint(pelvis);
                    pelvis2.y = (float)Screen.height - pelvis2.y;
                    bp.head = head4;
                    bp.spine = spine4;
                    bp.l_shoulder = l_upper2;
                    bp.r_shoulder = r_upper2;
                    bp.l_elbow = l_fore2;
                    bp.r_elbow = r_fore2;
                    bp.l_hand = l_hand2;
                    bp.r_hand = r_hand2;
                    bp.pelvis = pelvis2;
                    bp.l_hip = l_hip2;
                    bp.r_hip = r_hip2;
                    bp.l_knee = l_knee2;
                    bp.r_knee = r_knee2;
                    bp.l_foot = l_foot2;
                    bp.r_foot = r_foot2;
                    HackManager.PlayerBones.Add(visiblePlayer.userID.ToString(), bp);
                    head4 = default(Vector3);
                    spine4 = default(Vector3);
                    l_upper2 = default(Vector3);
                    r_upper2 = default(Vector3);
                    l_fore2 = default(Vector3);
                    r_fore2 = default(Vector3);
                    l_hand2 = default(Vector3);
                    r_hand2 = default(Vector3);
                    l_hip2 = default(Vector3);
                    r_hip2 = default(Vector3);
                    l_knee2 = default(Vector3);
                    r_knee2 = default(Vector3);
                    l_foot2 = default(Vector3);
                    r_foot2 = default(Vector3);
                    pelvis2 = default(Vector3);
                    head4 = default(Vector3);
                    spine4 = default(Vector3);
                    l_upper2 = default(Vector3);
                    r_upper2 = default(Vector3);
                    l_fore2 = default(Vector3);
                    r_fore2 = default(Vector3);
                    l_hand2 = default(Vector3);
                    r_hand2 = default(Vector3);
                    l_hip2 = default(Vector3);
                    r_hip2 = default(Vector3);
                    l_knee2 = default(Vector3);
                    r_knee2 = default(Vector3);
                    l_foot2 = default(Vector3);
                    r_foot2 = default(Vector3);
                    pelvis2 = default(Vector3);
                }

                HackManager.BoxPositions bxp = default(HackManager.BoxPositions);
                Bounds BoundsBox = default(Bounds);
                bool flag2 = visiblePlayer.IsDucked();
                if (flag2)
                {
                    BoundsBox.center = visiblePlayer.transform.position + new Vector3(0f, 0.55f, 0f);
                    BoundsBox.extents = new Vector3(0.4f, 0.65f, 0.4f);
                }
                else
                {
                    BoundsBox.center = visiblePlayer.transform.position + new Vector3(0f, 0.85f, 0f);
                    BoundsBox.extents = new Vector3(0.4f, 0.9f, 0.4f);
                }

                float angles = EulerAngles(visiblePlayer.GetModel().headBone.rotation).y;
                Vector3 v3Center = BoundsBox.center;
                Vector3 v3Extents = BoundsBox.extents;
                Vector3 v3FrontTopLeft = RotatePoint(v3Center, new Vector3(v3Center.x - v3Extents.x, v3Center.y + v3Extents.y, v3Center.z - v3Extents.z), angles);
                Vector3 v3FrontTopRight = RotatePoint(v3Center, new Vector3(v3Center.x + v3Extents.x, v3Center.y + v3Extents.y, v3Center.z - v3Extents.z), angles);
                Vector3 v3FrontBottomLeft = RotatePoint(v3Center, new Vector3(v3Center.x - v3Extents.x, v3Center.y - v3Extents.y, v3Center.z - v3Extents.z), angles);
                Vector3 v3FrontBottomRight = RotatePoint(v3Center, new Vector3(v3Center.x + v3Extents.x, v3Center.y - v3Extents.y, v3Center.z - v3Extents.z), angles);
                Vector3 v3BackTopLeft = RotatePoint(v3Center, new Vector3(v3Center.x - v3Extents.x, v3Center.y + v3Extents.y, v3Center.z + v3Extents.z), angles);
                Vector3 v3BackTopRight = RotatePoint(v3Center, new Vector3(v3Center.x + v3Extents.x, v3Center.y + v3Extents.y, v3Center.z + v3Extents.z), angles);
                Vector3 v3BackBottomLeft = RotatePoint(v3Center, new Vector3(v3Center.x - v3Extents.x, v3Center.y - v3Extents.y, v3Center.z + v3Extents.z), angles);
                Vector3 v3BackBottomRight = RotatePoint(v3Center, new Vector3(v3Center.x + v3Extents.x, v3Center.y - v3Extents.y, v3Center.z + v3Extents.z), angles);
                Vector3 v2FrontTopLeft = MainCamera.mainCamera.WorldToScreenPoint(v3FrontTopLeft);
                v2FrontTopLeft.y = (float)Screen.height - v2FrontTopLeft.y;
                Vector3 v2FrontTopRight = MainCamera.mainCamera.WorldToScreenPoint(v3FrontTopRight);
                v2FrontTopRight.y = (float)Screen.height - v2FrontTopRight.y;
                Vector3 v2FrontBottomLeft = MainCamera.mainCamera.WorldToScreenPoint(v3FrontBottomLeft);
                v2FrontBottomLeft.y = (float)Screen.height - v2FrontBottomLeft.y;
                Vector3 v2FrontBottomRight = MainCamera.mainCamera.WorldToScreenPoint(v3FrontBottomRight);
                v2FrontBottomRight.y = (float)Screen.height - v2FrontBottomRight.y;
                Vector3 v2BackTopLeft = MainCamera.mainCamera.WorldToScreenPoint(v3BackTopLeft);
                v2BackTopLeft.y = (float)Screen.height - v2BackTopLeft.y;
                Vector3 v2BackTopRight = MainCamera.mainCamera.WorldToScreenPoint(v3BackTopRight);
                v2BackTopRight.y = (float)Screen.height - v2BackTopRight.y;
                Vector3 v2BackBottomLeft = MainCamera.mainCamera.WorldToScreenPoint(v3BackBottomLeft);
                v2BackBottomLeft.y = (float)Screen.height - v2BackBottomLeft.y;
                Vector3 v2BackBottomRight = MainCamera.mainCamera.WorldToScreenPoint(v3BackBottomRight);
                v2BackBottomRight.y = (float)Screen.height - v2BackBottomRight.y;
                bool flag3 = VisibleOnScreen(v3FrontTopLeft) && VisibleOnScreen(v3FrontTopRight) && VisibleOnScreen(v3FrontBottomLeft) && VisibleOnScreen(v3FrontBottomRight) && VisibleOnScreen(v3BackTopLeft) && VisibleOnScreen(v3BackTopRight) && VisibleOnScreen(v3BackBottomLeft) && VisibleOnScreen(v3BackBottomRight);
                if (flag3)
                {
                    bxp.backBottomleft = v2BackBottomLeft;
                    bxp.backBottomright = v2BackBottomRight;
                    bxp.backTopleft = v2BackTopLeft;
                    bxp.backTopright = v2BackTopRight;
                    bxp.frontBottomleft = v2FrontBottomLeft;
                    bxp.frontBottomright = v2FrontBottomRight;
                    bxp.frontTopleft = v2FrontTopLeft;
                    bxp.frontTopright = v2FrontTopRight;
                    HackManager.PlayerBoxes.Add(visiblePlayer.userID.ToString(), bxp);
                }

                bp = default(HackManager.BonePositions);
                bxp = default(HackManager.BoxPositions);
                v2FrontTopLeft = default(Vector3);
                v2FrontTopRight = default(Vector3);
                v2FrontBottomLeft = default(Vector3);
                v2FrontBottomRight = default(Vector3);
                v2BackTopLeft = default(Vector3);
                v2BackTopRight = default(Vector3);
                v2BackBottomLeft = default(Vector3);
                v2BackBottomRight = default(Vector3);
                BoundsBox = default(Bounds);
                bp = default(HackManager.BonePositions);
                head3 = default(Vector3);
                spine3 = default(Vector3);
                position = default(Vector3);
                l_upper = default(Vector3);
                l_fore = default(Vector3);
                l_hand = default(Vector3);
                position2 = default(Vector3);
                r_upper = default(Vector3);
                r_fore = default(Vector3);
                r_hand = default(Vector3);
                pelvis = default(Vector3);
                l_hip = default(Vector3);
                l_knee = default(Vector3);
                position3 = default(Vector3);
                l_foot = default(Vector3);
                r_hip = default(Vector3);
                r_knee = default(Vector3);
                position4 = default(Vector3);
                r_foot = default(Vector3);
                bxp = default(HackManager.BoxPositions);
                BoundsBox = default(Bounds);
                v3Center = default(Vector3);
                v3Extents = default(Vector3);
                v3FrontTopLeft = default(Vector3);
                v3FrontTopRight = default(Vector3);
                v3FrontBottomLeft = default(Vector3);
                v3FrontBottomRight = default(Vector3);
                v3BackTopLeft = default(Vector3);
                v3BackTopRight = default(Vector3);
                v3BackBottomLeft = default(Vector3);
                v3BackBottomRight = default(Vector3);
                v2FrontTopLeft = default(Vector3);
                v2FrontTopRight = default(Vector3);
                v2FrontBottomLeft = default(Vector3);
                v2FrontBottomRight = default(Vector3);
                v2BackTopLeft = default(Vector3);
                v2BackTopRight = default(Vector3);
                v2BackBottomLeft = default(Vector3);
                v2BackBottomRight = default(Vector3);
            }

            IEnumerator<global::BasePlayer> enumerator = null;
            yield return new WaitForSeconds(0.005f);
        }

        yield break;
    }

    // Token: 0x06000048 RID: 72 RVA: 0x000021E4 File Offset: 0x000003E4
    IEnumerator Removals()
    {
        Dictionary<int, HackManager.RemovalInfo> removalDict = new Dictionary<int, HackManager.RemovalInfo>();
        for (; ; )
        {
            bool flag = HackManager.localplayer != null && HackManager.localplayer.GetHeldEntity() != null;
            if (flag)
            {
                var bp = LocalPlayer.Entity.GetHeldEntity().GetComponent<global::BaseProjectile>();
                bool flag2 = bp != null;
                if (flag2)
                {
                    int itemID = HackManager.localplayer.GetHeldEntity().GetOwnerItemDefinition().itemid;
                    bool flag3 = itemID == 1588298435 && HackManager.Aim_BoltFast;
                    if (flag3)
                    {
                        bool flag4 = bp.projectileVelocityScale == 1f;
                        if (flag4)
                        {
                            bp.projectileVelocityScale = 5f;
                        }
                    }
                    else
                    {
                        bool flag5 = bp.projectileVelocityScale != 1f;
                        if (flag5)
                        {
                            bp.projectileVelocityScale = 1f;
                        }
                    }

                    bool aim_NoRecoil = HackManager.Aim_NoRecoil;
                    if (aim_NoRecoil)
                    {
                        bool flag6 = bp.recoil.recoilPitchMax != 0f;
                        if (flag6)
                        {
                            bool flag7 = !removalDict.ContainsKey(itemID);
                            if (flag7)
                            {
                                removalDict.Add(itemID, new HackManager.RemovalInfo
                                {
                                    recoilPitchMax = bp.recoil.recoilPitchMax,
                                    recoilPitchMin = bp.recoil.recoilPitchMin,
                                    recoilYawMax = bp.recoil.recoilYawMax,
                                    recoilYawMin = bp.recoil.recoilYawMin,
                                    movementPenalty = bp.recoil.movementPenalty
                                });
                            }
                            else
                            {
                                bool flag8 = bp.recoil.recoilPitchMax != 0f;
                                if (flag8)
                                {
                                    HackManager.RemovalInfo updateinfo4 = removalDict[itemID];
                                    updateinfo4.recoilPitchMax = bp.recoil.recoilPitchMax;
                                    updateinfo4.recoilPitchMin = bp.recoil.recoilPitchMin;
                                    updateinfo4.recoilYawMax = bp.recoil.recoilYawMax;
                                    updateinfo4.recoilYawMin = bp.recoil.recoilYawMin;
                                    updateinfo4.movementPenalty = bp.recoil.movementPenalty;
                                    removalDict[itemID] = updateinfo4;
                                }
                            }

                            bp.recoil.recoilPitchMax = 0f;
                            bp.recoil.recoilPitchMin = 0f;
                            bp.recoil.recoilYawMax = 0f;
                            bp.recoil.recoilYawMin = 0f;
                            bp.recoil.movementPenalty = 0f;
                        }
                    }
                    else
                    {
                        bool flag9 = bp.recoil.recoilPitchMax == 0f;
                        if (flag9)
                        {
                            bp.recoil.recoilPitchMax = removalDict[itemID].recoilPitchMax;
                            bp.recoil.recoilPitchMin = removalDict[itemID].recoilPitchMin;
                            bp.recoil.recoilYawMax = removalDict[itemID].recoilYawMax;
                            bp.recoil.recoilYawMin = removalDict[itemID].recoilYawMin;
                            bp.recoil.movementPenalty = removalDict[itemID].movementPenalty;
                        }
                    }

                    bool aim_NoSway = HackManager.Aim_NoSway;
                    if (aim_NoSway)
                    {
                        bool flag10 = bp.aimSway != 0f;
                        if (flag10)
                        {
                            bool flag11 = !removalDict.ContainsKey(itemID);
                            if (flag11)
                            {
                                removalDict.Add(itemID, new HackManager.RemovalInfo
                                {
                                    aimSway = bp.aimSway,
                                    aimSwaySpeed = bp.aimSwaySpeed
                                });
                            }
                            else
                            {
                                bool flag12 = bp.aimSway != 0f;
                                if (flag12)
                                {
                                    HackManager.RemovalInfo updateinfo5 = removalDict[itemID];
                                    updateinfo5.aimSway = bp.aimSway;
                                    updateinfo5.aimSwaySpeed = bp.aimSwaySpeed;
                                    removalDict[itemID] = updateinfo5;
                                }
                            }

                            bp.aimSway = 0f;
                            bp.aimSwaySpeed = 0f;
                        }
                    }
                    else
                    {
                        bool flag13 = bp.aimSway == 0f;
                        if (flag13)
                        {
                            bp.aimSway = removalDict[itemID].aimSway;
                            bp.aimSwaySpeed = removalDict[itemID].aimSwaySpeed;
                        }
                    }

                    bool aim_ForceAuto = HackManager.Aim_ForceAuto;
                    if (aim_ForceAuto)
                    {
                        bool flag14 = !bp.automatic;
                        if (flag14)
                        {
                            bool flag15 = !removalDict.ContainsKey(itemID);
                            if (flag15)
                            {
                                removalDict.Add(itemID, new HackManager.RemovalInfo
                                {
                                    automatic = bp.automatic
                                });
                            }
                            else
                            {
                                bool flag16 = !bp.automatic;
                                if (flag16)
                                {
                                    HackManager.RemovalInfo updateinfo6 = removalDict[itemID];
                                    updateinfo6.automatic = bp.automatic;
                                    removalDict[itemID] = updateinfo6;
                                }
                            }

                            bp.automatic = true;
                        }
                    }
                    else
                    {
                        bp.automatic = removalDict[itemID].automatic;
                    }

                    bool aim_NoSpread = HackManager.Aim_NoSpread;
                    if (aim_NoSpread)
                    {
                        bool flag17 = bp.aimCone != 0f;
                        if (flag17)
                        {
                            bool flag18 = !removalDict.ContainsKey(itemID);
                            if (flag18)
                            {
                                removalDict.Add(itemID, new HackManager.RemovalInfo
                                {
                                    aimCone = bp.aimCone,
                                    aimConeHip = bp.hipAimCone,
                                    aimConePenaltyMax = bp.aimConePenaltyMax,
                                    aimConePenaltyPerShot = bp.aimconePenaltyPerShot
                                });
                            }
                            else
                            {
                                bool flag19 = bp.aimCone != 0f;
                                if (flag19)
                                {
                                    HackManager.RemovalInfo updateinfo7 = removalDict[itemID];
                                    updateinfo7.aimCone = bp.aimCone;
                                    updateinfo7.aimConeHip = bp.hipAimCone;
                                    updateinfo7.aimConePenaltyMax = bp.aimConePenaltyMax;
                                    updateinfo7.aimConePenaltyPerShot = bp.aimconePenaltyPerShot;
                                    removalDict[itemID] = updateinfo7;
                                }
                            }

                            bp.aimCone = 0f;
                            bp.hipAimCone = 0f;
                            bp.aimConePenaltyMax = 0f;
                            bp.aimconePenaltyPerShot = 0f;
                        }
                    }
                    else
                    {
                        bool flag20 = bp.aimCone == 0f;
                        if (flag20)
                        {
                            bp.aimCone = removalDict[itemID].aimCone;
                            bp.hipAimCone = removalDict[itemID].aimConeHip;
                            bp.aimConePenaltyMax = removalDict[itemID].aimConePenaltyMax;
                            bp.aimconePenaltyPerShot = removalDict[itemID].aimConePenaltyPerShot;
                        }
                    }
                }

                bp = null;
            }

            yield return new WaitForSeconds(0.25f);
        }

        yield break;
    }

    // Token: 0x04000042 RID: 66
    public static List<GameObject> ViewerCaches = new List<GameObject>();

    // Token: 0x04000043 RID: 67
    public static GameObject lookingAt;

    // Token: 0x04000044 RID: 68
    public static Vector3 lookPoint = Vector3.zero;

    // Token: 0x04000045 RID: 69
    public static Color Color_AimXhair = new Color(0f, 0f, 1f, 1f);

    // Token: 0x04000046 RID: 70
    public static bool Form1 = true;

    // Token: 0x04000047 RID: 71
    public static KeyCode AIMMM = KeyCode.X;

    // Token: 0x04000048 RID: 72
    public static bool Enable;

    // Token: 0x04000049 RID: 73
    static global::BasePlayer target;

    // Token: 0x0400004A RID: 74
    public static bool Silentxd;

    // Token: 0x0400004B RID: 75
    public static global::BasePlayer localplayer;

    // Token: 0x0400004C RID: 76
    public static List<Vector3> MarkersExplosive;

    // Token: 0x0400004D RID: 77
    float nextAttack;

    // Token: 0x0400004E RID: 78
    static Rect rect_esp;

    // Token: 0x0400004F RID: 79
    static Color color_0;

    // Token: 0x04000050 RID: 80
    static Color color_1;

    // Token: 0x04000051 RID: 81
    public static Color EspRGBAPlayers;

    // Token: 0x04000052 RID: 82
    public static HashSet<OreResourceEntity> ore;

    // Token: 0x04000053 RID: 83
    public static HashSet<BaseNpc> animal;

    // Token: 0x04000054 RID: 84
    public static HashSet<global::WorldItem> worlditem;

    // Token: 0x04000055 RID: 85
    public static HashSet<BaseHelicopter> heli;

    // Token: 0x04000056 RID: 86
    public static HashSet<StorageContainer> airdrop;

    // Token: 0x04000057 RID: 87
    public static HashSet<StorageContainer> lootbox;

    // Token: 0x04000058 RID: 88
    public static HashSet<StorageContainer> stash;

    // Token: 0x04000059 RID: 89
    public static HashSet<BuildingPrivlidge> cupboard;

    // Token: 0x0400005A RID: 90
    public static HashSet<global::LootableCorpse> corpse;

    // Token: 0x0400005B RID: 91
    public static HashSet<StorageContainer> traps;

    // Token: 0x0400005C RID: 92
    public static HashSet<StorageContainer> boxs;

    // Token: 0x0400005D RID: 93
    public static HashSet<MedicalTool> medtool;

    // Token: 0x0400005E RID: 94
    public static HashSet<CollectibleEntity> collect;

    // Token: 0x0400005F RID: 95
    public static HashSet<OreHotSpot> orehotspot;

    // Token: 0x04000060 RID: 96
    public static HashSet<TreeEntity> treemarker;

    // Token: 0x04000061 RID: 97
    public static HashSet<TimedExplosive> explos;

    // Token: 0x04000062 RID: 98
    public static HashSet<ItemIcon> loot;

    // Token: 0x04000063 RID: 99
    public static BaseHelicopter heliObject;

    // Token: 0x04000064 RID: 100
    public static List<string> Whitelist;

    // Token: 0x04000065 RID: 101
    public static KeyCode enableHackKey;

    // Token: 0x04000066 RID: 102
    public static bool enableHack;

    // Token: 0x04000067 RID: 103
    public static KeyCode showMenuKey;

    // Token: 0x04000068 RID: 104
    public static bool showMenu;

    // Token: 0x04000069 RID: 105
    public static bool Aim_Heli;

    // Token: 0x0400006A RID: 106
    public static bool ESP_Active = true;

    // Token: 0x0400006B RID: 107
    public static bool ESP_OnlinePlayers;

    // Token: 0x0400006C RID: 108
    public static bool ESP_Scientist;

    // Token: 0x0400006D RID: 109
    public static bool ESP_Node;

    // Token: 0x0400006E RID: 110
    public static bool ESP_Animal;

    // Token: 0x0400006F RID: 111
    public static bool ESP_Dropped;

    // Token: 0x04000070 RID: 112
    public static bool ESP_Heli;

    // Token: 0x04000071 RID: 113
    public static bool ESP_Airdrop;

    // Token: 0x04000072 RID: 114
    public static bool ESP_Crate;

    // Token: 0x04000073 RID: 115
    public static bool ESP_Stash;

    // Token: 0x04000074 RID: 116
    public static bool ESP_Far;

    // Token: 0x04000075 RID: 117
    public static int ESP_FarRange;

    // Token: 0x04000076 RID: 118
    public static Color Color_ESPFar;

    // Token: 0x04000077 RID: 119
    public static bool ESP_Sleepers;

    // Token: 0x04000078 RID: 120
    public static Color Color_ESPSleepers = Color.yellow;

    // Token: 0x04000079 RID: 121
    public static bool ESP_Bones;

    // Token: 0x0400007A RID: 122
    public static Color Color_ESPBones;

    // Token: 0x0400007B RID: 123
    public static bool ESP_Boxes;

    // Token: 0x0400007C RID: 124
    public static bool ESP_Boxes3D;

    // Token: 0x0400007D RID: 125
    public static int ESP_ActiveRange;

    // Token: 0x0400007E RID: 126
    public static bool ESP_ShowInventory;

    // Token: 0x0400007F RID: 127
    public static bool Misc_FastHeal;

    // Token: 0x04000080 RID: 128
    public static bool Misc_FastLoot;

    // Token: 0x04000081 RID: 129
    public static bool Misc_SpawnLadder;

    // Token: 0x04000082 RID: 130
    public static bool Misc_Demolish;

    // Token: 0x04000083 RID: 131
    public static bool Misc_FastGather;

    // Token: 0x04000084 RID: 132
    public static bool Silentaim;

    // Token: 0x04000085 RID: 133
    public static bool ESP_ShowTCAuth;

    // Token: 0x04000086 RID: 134
    public static bool shouldDrawCupboards;

    // Token: 0x04000087 RID: 135
    public static bool ESP_TC;

    // Token: 0x04000088 RID: 136
    public static bool ESP_Corpse;

    // Token: 0x04000089 RID: 137
    public static bool ESP_Turrets;

    // Token: 0x0400008A RID: 138
    public static bool ESP_Boxs;

    // Token: 0x0400008B RID: 139
    public static bool ESP_Entitys;

    // Token: 0x0400008C RID: 140
    public static bool ESP_Explosions;

    // Token: 0x0400008D RID: 141
    public static bool Misc_InstantRevive;

    // Token: 0x0400008E RID: 142
    public static bool Misc_Autoloot;

    // Token: 0x0400008F RID: 143
    public static bool Misc_OreHotSpot;

    // Token: 0x04000090 RID: 144
    public static bool Misc_TreeMarker;

    // Token: 0x04000091 RID: 145
    public static bool Misc_MeleeAim;

    // Token: 0x04000092 RID: 146
    public static bool Misc_NoClip;

    // Token: 0x04000093 RID: 147
    public static Color Color_ESPTC;

    // Token: 0x04000094 RID: 148
    public static bool toggleAimbot;

    // Token: 0x04000095 RID: 149
    public static bool toggleAimbotHeli;

    // Token: 0x04000096 RID: 150
    public static KeyCode aimbotKeyy;

    // Token: 0x04000097 RID: 151
    public static bool AimAtHead;

    // Token: 0x04000098 RID: 152
    public static float maxFOV;

    // Token: 0x04000099 RID: 153
    public static bool VelocityPrediction;

    // Token: 0x0400009A RID: 154
    public static bool BulletDropPrediction;

    // Token: 0x0400009B RID: 155
    public static bool shouldEnableAimbot;

    // Token: 0x0400009C RID: 156
    public static bool toggleVisibleCheck;

    // Token: 0x0400009D RID: 157
    public static KeyCode aimbotKey;

    // Token: 0x0400009E RID: 158
    public static float aimbotFov;

    // Token: 0x0400009F RID: 159
    public static int Aim_Fov;

    // Token: 0x040000A0 RID: 160
    public static bool Aim_DrawFov;

    // Token: 0x040000A1 RID: 161
    public static Color Color_AimDrawFov;

    // Token: 0x040000A2 RID: 162
    public static bool Aim_Xhair;

    // Token: 0x040000A3 RID: 163
    public static bool Radar_Active;

    // Token: 0x040000A4 RID: 164
    public static bool Radar_Friends;

    // Token: 0x040000A5 RID: 165
    public static Color Color_RadarFriends;

    // Token: 0x040000A6 RID: 166
    public static bool Radar_Enemies;

    // Token: 0x040000A7 RID: 167
    public static Color Color_RadarEnemies;

    // Token: 0x040000A8 RID: 168
    public static bool Radar_Animals;

    // Token: 0x040000A9 RID: 169
    public static Color Color_RadarAnimals;

    // Token: 0x040000AA RID: 170
    public static int Radar_X;

    // Token: 0x040000AB RID: 171
    public static int Radar_Y;

    // Token: 0x040000AC RID: 172
    public static int Radar_Size;

    // Token: 0x040000AD RID: 173
    public static int Radar_Range;

    // Token: 0x040000AE RID: 174
    public static bool Misc_Spider;

    // Token: 0x040000AF RID: 175
    public static bool Misc_Speed;

    // Token: 0x040000B0 RID: 176
    public static bool Misc_Freeze;

    // Token: 0x040000B1 RID: 177
    public static int Misc_FreezeValue;

    // Token: 0x040000B2 RID: 178
    public static bool Misc_LowGravity;

    // Token: 0x040000B3 RID: 179
    public static bool Misc_NoSink;

    // Token: 0x040000B4 RID: 180
    public static bool Misc_MultiJump;

    // Token: 0x040000B5 RID: 181
    public static bool Misc_AdminPriv;

    // Token: 0x040000B6 RID: 182
    public static bool Aim_NoRecoil;

    // Token: 0x040000B7 RID: 183
    public static bool Aim_NoSway;

    // Token: 0x040000B8 RID: 184
    public static bool Aim_NoSpread;

    // Token: 0x040000B9 RID: 185
    public static bool Aim_ForceAuto;

    // Token: 0x040000BA RID: 186
    Vector3 v3FrontTopLeft;

    // Token: 0x040000BB RID: 187
    Vector3 v3FrontTopRight;

    // Token: 0x040000BC RID: 188
    Vector3 v3FrontBottomLeft;

    // Token: 0x040000BD RID: 189
    Vector3 v3FrontBottomRight;

    // Token: 0x040000BE RID: 190
    Vector3 v3BackTopLeft;

    // Token: 0x040000BF RID: 191
    Vector3 v3BackTopRight;

    // Token: 0x040000C0 RID: 192
    Vector3 v3BackBottomLeft;

    // Token: 0x040000C1 RID: 193
    Vector3 v3BackBottomRight;

    // Token: 0x040000C2 RID: 194
    Vector2 v2FrontTopLeft;

    // Token: 0x040000C3 RID: 195
    Vector2 v2FrontTopRight;

    // Token: 0x040000C4 RID: 196
    Vector2 v2FrontBottomLeft;

    // Token: 0x040000C5 RID: 197
    Vector2 v2FrontBottomRight;

    // Token: 0x040000C6 RID: 198
    Vector2 v2BackTopLeft;

    // Token: 0x040000C7 RID: 199
    Vector2 v2BackTopRight;

    // Token: 0x040000C8 RID: 200
    Vector2 v2BackBottomLeft;

    // Token: 0x040000C9 RID: 201
    Vector2 v2BackBottomRight;

    // Token: 0x040000CA RID: 202
    public static float ShowMaxDistanceExplosive;

    // Token: 0x040000CB RID: 203
    public static int FovRadius;

    // Token: 0x040000CC RID: 204
    float deltaTime;

    // Token: 0x040000CD RID: 205
    public static Dictionary<global::BasePlayer, int> rageTargets;

    // Token: 0x040000CE RID: 206
    public static bool rageHeli;

    // Token: 0x040000CF RID: 207
    public static bool Aim_Active;

    // Token: 0x040000D0 RID: 208
    public static bool AimMode;

    // Token: 0x040000D1 RID: 209
    public static bool Aim_Smooth;

    // Token: 0x040000D2 RID: 210
    public static bool Aim_VisCheck;

    // Token: 0x040000D3 RID: 211
    public static bool Aim_Whitelist;

    // Token: 0x040000D4 RID: 212
    public static bool Aim_BoltFast;

    // Token: 0x040000D5 RID: 213
    public static int Aim_Range;

    // Token: 0x040000D6 RID: 214
    public static bool Aim_Position;

    // Token: 0x040000D7 RID: 215
    public static bool Aim_NoDrop;

    // Token: 0x040000D8 RID: 216
    DumbHook timedaction;

    // Token: 0x040000D9 RID: 217
    public static Dictionary<string, HackManager.BonePositions> PlayerBones;

    // Token: 0x040000DA RID: 218
    public static Dictionary<string, HackManager.BoxPositions> PlayerBoxes;

    // Token: 0x040000DB RID: 219
    float attackCooldown;

    // Token: 0x0200000E RID: 14
    public struct Resource
    {
        // Token: 0x040000DC RID: 220
        public string name;

        // Token: 0x040000DD RID: 221
        public Vector3 position;

        // Token: 0x040000DE RID: 222
        public global::BaseEntity entity;
    }

    // Token: 0x0200000F RID: 15
    public struct Animal
    {
        // Token: 0x040000DF RID: 223
        public string name;

        // Token: 0x040000E0 RID: 224
        public Vector3 position;

        // Token: 0x040000E1 RID: 225
        public global::BaseEntity entity;
    }

    // Token: 0x02000010 RID: 16
    public struct Dropped
    {
        // Token: 0x040000E2 RID: 226
        public string name;

        // Token: 0x040000E3 RID: 227
        public Vector3 position;

        // Token: 0x040000E4 RID: 228
        public global::BaseEntity entity;
    }

    // Token: 0x02000011 RID: 17
    public struct CLock
    {
        // Token: 0x040000E5 RID: 229
        public string name;

        // Token: 0x040000E6 RID: 230
        public Vector3 position;

        // Token: 0x040000E7 RID: 231
        public global::BaseEntity entity;
    }

    // Token: 0x02000012 RID: 18
    public struct Airdrop
    {
        // Token: 0x040000E8 RID: 232
        public string name;

        // Token: 0x040000E9 RID: 233
        public Vector3 position;

        // Token: 0x040000EA RID: 234
        public global::BaseEntity entity;
    }

    // Token: 0x02000013 RID: 19
    public struct Barrel
    {
        // Token: 0x040000EB RID: 235
        public string name;

        // Token: 0x040000EC RID: 236
        public Vector3 position;

        // Token: 0x040000ED RID: 237
        public global::BaseEntity entity;
    }

    // Token: 0x02000014 RID: 20
    public struct Crate
    {
        // Token: 0x040000EE RID: 238
        public string name;

        // Token: 0x040000EF RID: 239
        public Vector3 position;

        // Token: 0x040000F0 RID: 240
        public global::BaseEntity entity;
    }

    // Token: 0x02000015 RID: 21
    public struct Stash
    {
        // Token: 0x040000F1 RID: 241
        public string name;

        // Token: 0x040000F2 RID: 242
        public Vector3 position;

        // Token: 0x040000F3 RID: 243
        public global::BaseEntity entity;
    }

    // Token: 0x02000016 RID: 22
    public struct Hemp
    {
        // Token: 0x040000F4 RID: 244
        public string name;

        // Token: 0x040000F5 RID: 245
        public Vector3 position;

        // Token: 0x040000F6 RID: 246
        public global::BaseEntity entity;
    }

    // Token: 0x02000017 RID: 23
    public struct Corpse
    {
        // Token: 0x040000F7 RID: 247
        public string name;

        // Token: 0x040000F8 RID: 248
        public Vector3 position;

        // Token: 0x040000F9 RID: 249
        public global::BaseEntity entity;
    }

    // Token: 0x02000018 RID: 24
    public struct TC
    {
        // Token: 0x040000FA RID: 250
        public string name;

        // Token: 0x040000FB RID: 251
        public Vector3 position;

        // Token: 0x040000FC RID: 252
        public global::BaseEntity entity;
    }

    // Token: 0x02000019 RID: 25
    public struct Autoturret
    {
        // Token: 0x040000FD RID: 253
        public string name;

        // Token: 0x040000FE RID: 254
        public Vector3 position;

        // Token: 0x040000FF RID: 255
        public global::BaseEntity entity;
    }

    // Token: 0x0200001A RID: 26
    public struct Flameturret
    {
        // Token: 0x04000100 RID: 256
        public string name;

        // Token: 0x04000101 RID: 257
        public Vector3 position;

        // Token: 0x04000102 RID: 258
        public global::BaseEntity entity;
    }

    // Token: 0x0200001B RID: 27
    public struct Landmine
    {
        // Token: 0x04000103 RID: 259
        public string name;

        // Token: 0x04000104 RID: 260
        public Vector3 position;

        // Token: 0x04000105 RID: 261
        public global::BaseEntity entity;
    }

    // Token: 0x0200001C RID: 28
    public struct Beartrap
    {
        // Token: 0x04000106 RID: 262
        public string name;

        // Token: 0x04000107 RID: 263
        public Vector3 position;

        // Token: 0x04000108 RID: 264
        public global::BaseEntity entity;
    }

    // Token: 0x0200001D RID: 29
    public struct Shotguntrap
    {
        // Token: 0x04000109 RID: 265
        public string name;

        // Token: 0x0400010A RID: 266
        public Vector3 position;

        // Token: 0x0400010B RID: 267
        public global::BaseEntity entity;
    }

    // Token: 0x0200001E RID: 30
    public struct Painting
    {
        // Token: 0x0400010C RID: 268
        public string name;

        // Token: 0x0400010D RID: 269
        public Vector3 position;

        // Token: 0x0400010E RID: 270
        public global::BaseEntity entity;
    }

    // Token: 0x0200001F RID: 31
    public struct ItemInventory
    {
        // Token: 0x0400010F RID: 271
        public int count;

        // Token: 0x04000110 RID: 272
        public string name;
    }

    // Token: 0x02000020 RID: 32
    public struct BonePositions
    {
        // Token: 0x04000111 RID: 273
        public Vector3 head;

        // Token: 0x04000112 RID: 274
        public Vector3 spine;

        // Token: 0x04000113 RID: 275
        public Vector3 l_shoulder;

        // Token: 0x04000114 RID: 276
        public Vector3 r_shoulder;

        // Token: 0x04000115 RID: 277
        public Vector3 l_elbow;

        // Token: 0x04000116 RID: 278
        public Vector3 r_elbow;

        // Token: 0x04000117 RID: 279
        public Vector3 l_hand;

        // Token: 0x04000118 RID: 280
        public Vector3 r_hand;

        // Token: 0x04000119 RID: 281
        public Vector3 pelvis;

        // Token: 0x0400011A RID: 282
        public Vector3 l_hip;

        // Token: 0x0400011B RID: 283
        public Vector3 r_hip;

        // Token: 0x0400011C RID: 284
        public Vector3 l_knee;

        // Token: 0x0400011D RID: 285
        public Vector3 r_knee;

        // Token: 0x0400011E RID: 286
        public Vector3 l_foot;

        // Token: 0x0400011F RID: 287
        public Vector3 r_foot;
    }

    // Token: 0x02000021 RID: 33
    public struct BoxPositions
    {
        // Token: 0x04000120 RID: 288
        public Vector3 frontTopleft;

        // Token: 0x04000121 RID: 289
        public Vector3 frontTopright;

        // Token: 0x04000122 RID: 290
        public Vector3 frontBottomleft;

        // Token: 0x04000123 RID: 291
        public Vector3 frontBottomright;

        // Token: 0x04000124 RID: 292
        public Vector3 backTopleft;

        // Token: 0x04000125 RID: 293
        public Vector3 backTopright;

        // Token: 0x04000126 RID: 294
        public Vector3 backBottomleft;

        // Token: 0x04000127 RID: 295
        public Vector3 backBottomright;
    }

    // Token: 0x02000022 RID: 34
    public struct RemovalInfo
    {
        // Token: 0x04000128 RID: 296
        public float recoilPitchMax;

        // Token: 0x04000129 RID: 297
        public float recoilPitchMin;

        // Token: 0x0400012A RID: 298
        public float recoilYawMax;

        // Token: 0x0400012B RID: 299
        public float recoilYawMin;

        // Token: 0x0400012C RID: 300
        public float movementPenalty;

        // Token: 0x0400012D RID: 301
        public float aimSway;

        // Token: 0x0400012E RID: 302
        public float aimSwaySpeed;

        // Token: 0x0400012F RID: 303
        public bool automatic;

        // Token: 0x04000130 RID: 304
        public float aimCone;

        // Token: 0x04000131 RID: 305
        public float aimConeHip;

        // Token: 0x04000132 RID: 306
        public float aimConePenaltyMax;

        // Token: 0x04000133 RID: 307
        public float aimConePenaltyPerShot;
    }
}