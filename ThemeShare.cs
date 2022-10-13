using System;
using System.Collections.Generic;

// Token: 0x02000032 RID: 50
internal static class ThemeShare
{
	// Token: 0x06000199 RID: 409 RVA: 0x00012248 File Offset: 0x00010448
	private static void HandleCallbacks(IntPtr state, bool reserve)
	{
		ThemeShare.Invalidate = (ThemeShare.Frames >= 50);
		bool invalidate = ThemeShare.Invalidate;
		if (invalidate)
		{
			ThemeShare.Frames = 0;
		}
		List<ThemeShare.AnimationDelegate> callbacks = ThemeShare.Callbacks;
		lock (callbacks)
		{
			for (int i = 0; i <= ThemeShare.Callbacks.Count - 1; i++)
			{
				ThemeShare.Callbacks[i](ThemeShare.Invalidate);
			}
		}
		ThemeShare.Frames += 50;
	}

	// Token: 0x0600019A RID: 410 RVA: 0x000122EC File Offset: 0x000104EC
	private static void InvalidateThemeTimer()
	{
		bool flag = ThemeShare.Callbacks.Count == 0;
		if (flag)
		{
			ThemeShare.ThemeTimer.Delete();
		}
		else
		{
			ThemeShare.ThemeTimer.Create(0u, 50u, new PrecisionTimer.TimerDelegate(ThemeShare.HandleCallbacks));
		}
	}

	// Token: 0x0600019B RID: 411 RVA: 0x00012338 File Offset: 0x00010538
	public static void AddAnimationCallback(ThemeShare.AnimationDelegate callback)
	{
		List<ThemeShare.AnimationDelegate> callbacks = ThemeShare.Callbacks;
		lock (callbacks)
		{
			bool flag2 = ThemeShare.Callbacks.Contains(callback);
			if (!flag2)
			{
				ThemeShare.Callbacks.Add(callback);
				ThemeShare.InvalidateThemeTimer();
			}
		}
	}

	// Token: 0x0600019C RID: 412 RVA: 0x0001239C File Offset: 0x0001059C
	public static void RemoveAnimationCallback(ThemeShare.AnimationDelegate callback)
	{
		List<ThemeShare.AnimationDelegate> callbacks = ThemeShare.Callbacks;
		lock (callbacks)
		{
			bool flag2 = !ThemeShare.Callbacks.Contains(callback);
			if (!flag2)
			{
				ThemeShare.Callbacks.Remove(callback);
				ThemeShare.InvalidateThemeTimer();
			}
		}
	}

	// Token: 0x04000230 RID: 560
	private static int Frames;

	// Token: 0x04000231 RID: 561
	private static bool Invalidate;

	// Token: 0x04000232 RID: 562
	public static PrecisionTimer ThemeTimer = new PrecisionTimer();

	// Token: 0x04000233 RID: 563
	private const int FPS = 50;

	// Token: 0x04000234 RID: 564
	private const int Rate = 50;

	// Token: 0x04000235 RID: 565
	private static List<ThemeShare.AnimationDelegate> Callbacks = new List<ThemeShare.AnimationDelegate>();

	// Token: 0x02000033 RID: 51
	// (Invoke) Token: 0x0600019F RID: 415
	public delegate void AnimationDelegate(bool invalidate);
}
