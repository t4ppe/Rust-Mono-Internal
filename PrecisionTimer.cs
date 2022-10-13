using System;
using System.Runtime.InteropServices;

// Token: 0x02000036 RID: 54
internal class PrecisionTimer : IDisposable
{
    // Token: 0x17000047 RID: 71
    // (get) Token: 0x060001A8 RID: 424 RVA: 0x000124CC File Offset: 0x000106CC
    public bool Enabled => enabled;

    // Token: 0x060001A9 RID: 425
    [DllImport("kernel32.dll")]
    static extern bool CreateTimerQueueTimer(ref IntPtr handle, IntPtr queue, PrecisionTimer.TimerDelegate callback, IntPtr state, uint dueTime, uint period, uint flags);

    // Token: 0x060001AA RID: 426
    [DllImport("kernel32.dll")]
    static extern bool DeleteTimerQueueTimer(IntPtr queue, IntPtr handle, IntPtr callback);

    // Token: 0x060001AB RID: 427 RVA: 0x000124E4 File Offset: 0x000106E4
    public void Create(uint dueTime, uint period, PrecisionTimer.TimerDelegate callback)
    {
        bool enabled = this.enabled;
        if (!enabled)
        {
            TimerCallback = callback;
            bool flag = PrecisionTimer.CreateTimerQueueTimer(ref Handle, IntPtr.Zero, TimerCallback, IntPtr.Zero, dueTime, period, 0u);
            bool flag2 = !flag;
            if (flag2)
            {
                ThrowNewException("CreateTimerQueueTimer");
            }

            this.enabled = flag;
        }
    }

    // Token: 0x060001AC RID: 428 RVA: 0x00012540 File Offset: 0x00010740
    public void Delete()
    {
        bool flag = !enabled;
        if (!flag)
        {
            bool flag2 = PrecisionTimer.DeleteTimerQueueTimer(IntPtr.Zero, Handle, IntPtr.Zero);
            bool flag3 = !flag2 && Marshal.GetLastWin32Error() != 997;
            if (flag3)
            {
                ThrowNewException("DeleteTimerQueueTimer");
            }

            enabled = !flag2;
        }
    }

    // Token: 0x060001AD RID: 429 RVA: 0x0000241F File Offset: 0x0000061F
    void ThrowNewException(string name)
    {
    }

    // Token: 0x060001AE RID: 430 RVA: 0x00002E81 File Offset: 0x00001081
    public void Dispose() => Delete();

    // Token: 0x0400023D RID: 573
    bool enabled;

    // Token: 0x0400023E RID: 574
    IntPtr Handle;

    // Token: 0x0400023F RID: 575
    PrecisionTimer.TimerDelegate TimerCallback;

    // Token: 0x02000037 RID: 55
    // (Invoke) Token: 0x060001B1 RID: 433
    public delegate void TimerDelegate(IntPtr r1, bool r2);
}