using System;
using UnityEngine;

// Token: 0x0200002D RID: 45
public class Renders
{
	// Token: 0x06000088 RID: 136 RVA: 0x0000232A File Offset: 0x0000052A
	public static void Initialize()
	{
		Renders.guistyle_0.font = Font.CreateDynamicFontFromOSFont("SegoeUI", 12);
		Renders.initialized = true;
	}

	// Token: 0x06000089 RID: 137 RVA: 0x0000234A File Offset: 0x0000054A
	public static void BoxRect(Rect rect, Color color)
	{
		Renders.texture2D_0.SetPixel(0, 0, color);
		Renders.texture2D_0.Apply();
		Renders.color_0 = color;
		GUI.color = color;
		GUI.DrawTexture(rect, Renders.texture2D_0);
	}

	// Token: 0x0600008A RID: 138 RVA: 0x0000E9C4 File Offset: 0x0000CBC4
	public static void DrawRadarBackground(Rect rect)
	{
		Color color = new Color(0f, 0f, 0f, 0.5f);
		Renders.texture2D_0.SetPixel(0, 0, color);
		Renders.texture2D_0.Apply();
		GUI.color = color;
		GUI.DrawTexture(rect, Renders.texture2D_0);
	}

	// Token: 0x0600008B RID: 139 RVA: 0x0000EA1C File Offset: 0x0000CC1C
	public static void DrawESPBox(BasePlayer player, float thick, Color color)
	{
		Vector3 position = player.transform.position;
		Vector3 position2 = player.transform.position + new Vector3(0f, 1.8f, 0f);
		Vector3 vector = MainCamera.mainCamera.WorldToScreenPoint(position);
		Vector3 vector2 = MainCamera.mainCamera.WorldToScreenPoint(position2);
		float num = Mathf.Abs(vector.y - vector2.y);
		float num2 = num / 2f;
		bool flag = player.IsDucked();
		if (flag)
		{
			Vector2 vector3 = new Vector2(num2, num / 2f);
		}
		else
		{
			Vector2 vector3 = new Vector2(num2, num);
		}
		bool flag2 = vector.x > 0f && vector.x < (float)Screen.width && vector2.x > 0f && vector2.x < (float)Screen.width && vector.y > 0f && vector.y < (float)Screen.height && vector2.y > 0f && vector2.y < (float)Screen.height;
		if (flag2)
		{
			bool flag3 = vector2.x - vector.x > 10f;
			if (flag3)
			{
				vector.x += (vector2.x - vector.x) / 2f;
			}
			bool flag4 = vector.x - vector2.x > 10f;
			if (flag4)
			{
				vector.x -= (vector.x - vector2.x) / 2f;
			}
			Renders.BoxRect(new Rect(vector.x - num2 / 2f, (float)Screen.height - vector.y, num2, thick), color);
			Renders.BoxRect(new Rect(vector.x - num2 / 2f, (float)Screen.height - vector.y - num, num2, thick), color);
			Renders.BoxRect(new Rect(vector.x - num2 / 2f, (float)Screen.height - vector.y - num, thick, num), color);
			Renders.BoxRect(new Rect(vector.x + num2 / 2f, (float)Screen.height - vector.y - num, thick, num), color);
		}
	}

	// Token: 0x0600008C RID: 140 RVA: 0x0000EC60 File Offset: 0x0000CE60
	public static void DrawBox(Vector2 pos, Vector2 size, float thick, Color color, bool v)
	{
		Renders.BoxRect(new Rect(pos.x, pos.y, size.x, thick), color);
		Renders.BoxRect(new Rect(pos.x, pos.y, thick, size.y), color);
		Renders.BoxRect(new Rect(pos.x + size.x, pos.y, thick, size.y), color);
		Renders.BoxRect(new Rect(pos.x, pos.y + size.y, size.x + thick, thick), color);
	}

	// Token: 0x0600008D RID: 141 RVA: 0x0000ECFC File Offset: 0x0000CEFC
	public static void DrawHealth(Vector2 pos, float health, bool center = false)
	{
		if (center)
		{
			pos -= new Vector2(26f, 0f);
		}
		pos += new Vector2(0f, 18f);
		Renders.BoxRect(new Rect(pos.x, pos.y, 52f, 5f), Color.black);
		pos += new Vector2(1f, 1f);
		Color color = Color.green;
		bool flag = health <= 50f;
		if (flag)
		{
			color = Color.yellow;
		}
		bool flag2 = health <= 25f;
		if (flag2)
		{
			color = Color.red;
		}
		Renders.BoxRect(new Rect(pos.x, pos.y, 0.5f * health, 3f), color);
	}

	// Token: 0x0600008E RID: 142 RVA: 0x0000EDD4 File Offset: 0x0000CFD4
	public static void DrawString(Vector2 pos, string text, Color color, bool center = true, int size = 12, bool stroke = true, int v = 0)
	{
		Renders.guistyle_0.fontSize = size;
		Renders.guistyle_0.fontStyle = FontStyle.Bold;
		GUIContent content = new GUIContent(text);
		if (center)
		{
			pos.x -= Renders.guistyle_0.CalcSize(content).x / 2f;
		}
		if (stroke)
		{
			GUI.color = Color.black;
			GUI.Label(new Rect(pos.x - 1f, pos.y, 300f, 25f), content, Renders.guistyle_0);
			GUI.Label(new Rect(pos.x + 1f, pos.y, 300f, 25f), content, Renders.guistyle_0);
			GUI.Label(new Rect(pos.x, pos.y - 1f, 300f, 25f), content, Renders.guistyle_0);
			GUI.Label(new Rect(pos.x, pos.y + 1f, 300f, 25f), content, Renders.guistyle_0);
		}
		GUI.color = color;
		GUI.Label(new Rect(pos.x, pos.y, 300f, 25f), content, Renders.guistyle_0);
	}

	// Token: 0x0600008F RID: 143 RVA: 0x0000EF24 File Offset: 0x0000D124
	public static void DrawWeapon(Vector2 pos, string text, Color color, bool center = true, int size = 12, bool stroke = true)
	{
		Renders.guistyle_0.fontSize = size;
		Renders.guistyle_0.fontStyle = FontStyle.Bold;
		GUIContent content = new GUIContent(text);
		if (center)
		{
			pos.x -= Renders.guistyle_0.CalcSize(content).x / 2f;
		}
		pos += new Vector2(0f, 21f);
		if (stroke)
		{
			GUI.color = Color.black;
			GUI.Label(new Rect(pos.x - 1f, pos.y, 300f, 25f), content, Renders.guistyle_0);
			GUI.Label(new Rect(pos.x + 1f, pos.y, 300f, 25f), content, Renders.guistyle_0);
			GUI.Label(new Rect(pos.x, pos.y - 1f, 300f, 25f), content, Renders.guistyle_0);
			GUI.Label(new Rect(pos.x, pos.y + 1f, 300f, 25f), content, Renders.guistyle_0);
		}
		GUI.color = color;
		GUI.Label(new Rect(pos.x, pos.y, 300f, 25f), content, Renders.guistyle_0);
	}

	// Token: 0x040001D5 RID: 469
	public static bool initialized = false;

	// Token: 0x040001D6 RID: 470
	private static Color color_0;

	// Token: 0x040001D7 RID: 471
	private static GUIStyle guistyle_0 = new GUIStyle(GUI.skin.label);

	// Token: 0x040001D8 RID: 472
	private static Texture2D texture2D_0 = new Texture2D(1, 1);
}
