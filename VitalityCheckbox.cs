using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

// Token: 0x0200003A RID: 58
[DefaultEvent("CheckedChanged")]
internal class VitalityCheckbox : ThemeControl154
{
	// Token: 0x14000001 RID: 1
	// (add) Token: 0x060001BA RID: 442 RVA: 0x00012974 File Offset: 0x00010B74
	// (remove) Token: 0x060001BB RID: 443 RVA: 0x000129AC File Offset: 0x00010BAC
	
	public event VitalityCheckbox.CheckedChangedEventHandler CheckedChanged;

	// Token: 0x17000048 RID: 72
	// (get) Token: 0x060001BC RID: 444 RVA: 0x000129E4 File Offset: 0x00010BE4
	// (set) Token: 0x060001BD RID: 445 RVA: 0x000129FC File Offset: 0x00010BFC
	public bool Checked
	{
		get
		{
			return this._Checked;
		}
		set
		{
			this._Checked = value;
			base.Invalidate();
			bool flag = this.CheckedChanged != null;
			if (flag)
			{
				this.CheckedChanged(this);
			}
		}
	}

	// Token: 0x060001BE RID: 446 RVA: 0x00012A34 File Offset: 0x00010C34
	protected override void OnMouseDown(MouseEventArgs e)
	{
		base.OnMouseDown(e);
		bool @checked = this._Checked;
		if (@checked)
		{
			this._Checked = false;
		}
		else
		{
			this._Checked = true;
		}
	}

	// Token: 0x060001BF RID: 447 RVA: 0x00012A64 File Offset: 0x00010C64
	public VitalityCheckbox()
	{
		base.LockHeight = 22;
		base.SetColor("G1", Color.White);
		base.SetColor("G2", Color.LightGray);
		base.SetColor("BG", Color.FromArgb(240, 240, 240));
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x00002EF7 File Offset: 0x000010F7
	protected override void ColorHook()
	{
		this.BG = base.GetColor("BG");
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x00012AC4 File Offset: 0x00010CC4
	protected override void PaintHook()
	{
		this.G.Clear(this.BG);
		bool @checked = this._Checked;
		if (@checked)
		{
			this.G.DrawString("a", new Font("Marlett", 14f), Brushes.Gray, new Point(0, 1));
		}
		bool flag = this.State == MouseState.Over;
		if (flag)
		{
			this.G.FillRectangle(Brushes.White, new Rectangle(new Point(3, 3), new Size(15, 15)));
			bool checked2 = this._Checked;
			if (checked2)
			{
				this.G.DrawString("a", new Font("Marlett", 14f), Brushes.Gray, new Point(0, 1));
			}
		}
		this.G.DrawRectangle(Pens.White, 2, 2, 17, 17);
		this.G.DrawRectangle(Pens.LightGray, 3, 3, 15, 15);
		this.G.DrawRectangle(Pens.LightGray, 1, 1, 19, 19);
		this.G.DrawString(this.Text, new Font("Segoe UI", 9f), Brushes.Gray, 22f, 3f);
	}

	// Token: 0x04000246 RID: 582
	private Color BG;

	// Token: 0x04000247 RID: 583
	private bool _Checked;

	// Token: 0x0200003B RID: 59
	// (Invoke) Token: 0x060001C3 RID: 451
	public delegate void CheckedChangedEventHandler(object sender);
}
