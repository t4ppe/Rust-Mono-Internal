using System;
using System.Runtime.InteropServices;

namespace CS_ClassLibraryTester
{
	// Token: 0x0200005E RID: 94
	internal class PrecisionTimer : IDisposable
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0001B148 File Offset: 0x00019348
		public bool Enabled
		{
			get
			{
				return this._Enabled;
			}
		}

		// Token: 0x06000389 RID: 905
		[DllImport("kernel32.dll")]
		private static extern bool CreateTimerQueueTimer(ref IntPtr handle, IntPtr queue, PrecisionTimer.TimerDelegate callback, IntPtr state, uint dueTime, uint period, uint flags);

		// Token: 0x0600038A RID: 906
		[DllImport("kernel32.dll")]
		private static extern bool DeleteTimerQueueTimer(IntPtr queue, IntPtr handle, IntPtr callback);

		// Token: 0x0600038B RID: 907 RVA: 0x0001B160 File Offset: 0x00019360
		public void Create(uint dueTime, uint period, PrecisionTimer.TimerDelegate callback)
		{
			bool enabled = this._Enabled;
			if (!enabled)
			{
				this.TimerCallback = callback;
				bool flag = PrecisionTimer.CreateTimerQueueTimer(ref this.Handle, IntPtr.Zero, this.TimerCallback, IntPtr.Zero, dueTime, period, 0u);
				bool flag2 = !flag;
				if (flag2)
				{
					this.ThrowNewException("CreateTimerQueueTimer");
				}
				this._Enabled = flag;
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0001B1BC File Offset: 0x000193BC
		public void Delete()
		{
			bool flag = !this._Enabled;
			if (!flag)
			{
				bool flag2 = PrecisionTimer.DeleteTimerQueueTimer(IntPtr.Zero, this.Handle, IntPtr.Zero);
				bool flag3 = !flag2 && Marshal.GetLastWin32Error() != 997;
				if (flag3)
				{
					this.ThrowNewException("DeleteTimerQueueTimer");
				}
				this._Enabled = !flag2;
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00003F7C File Offset: 0x0000217C
		private void ThrowNewException(string name)
		{
			throw new Exception(string.Format("{0} failed. Win32Error: {1}", name, Marshal.GetLastWin32Error()));
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00003F99 File Offset: 0x00002199
		public void Dispose()
		{
			this.Delete();
		}

		// Token: 0x040002ED RID: 749
		private bool _Enabled;

		// Token: 0x040002EE RID: 750
		private IntPtr Handle;

		// Token: 0x040002EF RID: 751
		private PrecisionTimer.TimerDelegate TimerCallback;

		// Token: 0x0200005F RID: 95
		// (Invoke) Token: 0x06000391 RID: 913
		public delegate void TimerDelegate(IntPtr r1, bool r2);
	}
}
