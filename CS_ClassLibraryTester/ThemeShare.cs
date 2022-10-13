using System;
using System.Collections.Generic;

namespace CS_ClassLibraryTester
{
	// Token: 0x0200005B RID: 91
	internal static class ThemeShare
	{
		// Token: 0x06000379 RID: 889 RVA: 0x0001AEC4 File Offset: 0x000190C4
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
			ThemeShare.Frames += 10;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0001AF68 File Offset: 0x00019168
		private static void InvalidateThemeTimer()
		{
			bool flag = ThemeShare.Callbacks.Count == 0;
			if (flag)
			{
				ThemeShare.ThemeTimer.Delete();
			}
			else
			{
				ThemeShare.ThemeTimer.Create(0u, 10u, new PrecisionTimer.TimerDelegate(ThemeShare.HandleCallbacks));
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0001AFB4 File Offset: 0x000191B4
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

		// Token: 0x0600037C RID: 892 RVA: 0x0001B018 File Offset: 0x00019218
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

		// Token: 0x040002E5 RID: 741
		private static int Frames;

		// Token: 0x040002E6 RID: 742
		private static bool Invalidate;

		// Token: 0x040002E7 RID: 743
		public static PrecisionTimer ThemeTimer = new PrecisionTimer();

		// Token: 0x040002E8 RID: 744
		private const int FPS = 50;

		// Token: 0x040002E9 RID: 745
		private const int Rate = 10;

		// Token: 0x040002EA RID: 746
		private static List<ThemeShare.AnimationDelegate> Callbacks = new List<ThemeShare.AnimationDelegate>();

		// Token: 0x0200005C RID: 92
		// (Invoke) Token: 0x0600037F RID: 895
		public delegate void AnimationDelegate(bool invalidate);
	}
}
