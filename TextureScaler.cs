using System;
using UnityEngine;

// Token: 0x0200002E RID: 46
public class TextureScaler
{
	// Token: 0x06000092 RID: 146 RVA: 0x0000F08C File Offset: 0x0000D28C
	public static Texture2D Scaled(Texture2D src, int width, int height, FilterMode mode = FilterMode.Trilinear)
	{
		Rect source = new Rect(0f, 0f, (float)width, (float)height);
		TextureScaler._gpu_scale(src, width, height, mode);
		Texture2D texture2D = new Texture2D(width, height, TextureFormat.ARGB32, true);
		texture2D.Resize(width, height);
		texture2D.ReadPixels(source, 0, 0, true);
		return texture2D;
	}

	// Token: 0x06000093 RID: 147 RVA: 0x0000F0DC File Offset: 0x0000D2DC
	public static void Scale(Texture2D tex, int width, int height, FilterMode mode = FilterMode.Trilinear)
	{
		Rect source = new Rect(0f, 0f, (float)width, (float)height);
		TextureScaler._gpu_scale(tex, width, height, mode);
		tex.Resize(width, height);
		tex.ReadPixels(source, 0, 0, true);
		tex.Apply(true);
	}

	// Token: 0x06000094 RID: 148 RVA: 0x0000F128 File Offset: 0x0000D328
	private static void _gpu_scale(Texture2D src, int width, int height, FilterMode fmode)
	{
		src.filterMode = fmode;
		src.Apply(true);
		RenderTexture renderTarget = new RenderTexture(width, height, 32);
		Graphics.SetRenderTarget(renderTarget);
		GL.LoadPixelMatrix(0f, 1f, 1f, 0f);
		GL.Clear(true, true, new Color(0f, 0f, 0f, 0f));
		Graphics.DrawTexture(new Rect(0f, 0f, 1f, 1f), src);
	}
}
