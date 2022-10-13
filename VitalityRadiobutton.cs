using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

// Token: 0x0200003C RID: 60
[DefaultEvent("CheckedChanged")]
internal class VitalityRadiobutton : ThemeControl154
{
	// Token: 0x17000049 RID: 73
	// (get) Token: 0x060001C6 RID: 454 RVA: 0x00012C04 File Offset: 0x00010E04
	// (set) Token: 0x060001C7 RID: 455 RVA: 0x00012C1C File Offset: 0x00010E1C
	public bool Checked
	{
		get
		{
			return this._Checked;
		}
		set
		{
			this._Checked = value;
			this.InvalidateControls();
			bool flag = this.CheckedChanged != null;
			if (flag)
			{
				this.CheckedChanged(this);
			}
			base.Invalidate();
		}
	}

	// Token: 0x14000002 RID: 2
	// (add) Token: 0x060001C8 RID: 456 RVA: 0x00012C5C File Offset: 0x00010E5C
	// (remove) Token: 0x060001C9 RID: 457 RVA: 0x00012C94 File Offset: 0x00010E94
	
	public event VitalityRadiobutton.CheckedChangedEventHandler CheckedChanged;

	// Token: 0x060001CA RID: 458 RVA: 0x00012CCC File Offset: 0x00010ECC
	private void InvalidateControls()
	{
		bool flag = !base.IsHandleCreated || !this._Checked;
		if (!flag)
		{
			foreach (object obj in base.Parent.Controls)
			{
				Control control = (Control)obj;
				bool flag2 = control != this && control is VitalityRadiobutton;
				if (flag2)
				{
					((VitalityRadiobutton)control).Checked = false;
				}
			}
		}
	}

	// Token: 0x060001CB RID: 459 RVA: 0x00012D68 File Offset: 0x00010F68
	protected override void OnMouseDown(MouseEventArgs e)
	{
		bool flag = !this._Checked;
		if (flag)
		{
			this.Checked = true;
		}
		base.OnMouseDown(e);
	}

	// Token: 0x060001CC RID: 460 RVA: 0x00002F0B File Offset: 0x0000110B
	public VitalityRadiobutton()
	{
		base.LockHeight = 22;
		base.Width = 140;
		base.SetColor("BG", Color.FromArgb(240, 240, 240));
	}

	// Token: 0x060001CD RID: 461 RVA: 0x00002F4A File Offset: 0x0000114A
	protected override void ColorHook()
	{
		this.BG = base.GetColor("BG");
	}

	// Token: 0x060001CE RID: 462 RVA: 0x00012D94 File Offset: 0x00010F94
	protected override void PaintHook()
	{
		this.G.Clear(this.BG);
		this.G.SmoothingMode = SmoothingMode.HighQuality;
		bool @checked = this._Checked;
		if (@checked)
		{
			this.G.FillEllipse(Brushes.Gray, new Rectangle(new Point(7, 7), new Size(8, 8)));
		}
		bool flag = this.State == MouseState.Over;
		if (flag)
		{
			this.G.FillEllipse(Brushes.White, new Rectangle(new Point(4, 4), new Size(14, 14)));
			bool checked2 = this._Checked;
			if (checked2)
			{
				this.G.FillEllipse(Brushes.Gray, new Rectangle(new Point(7, 7), new Size(8, 8)));
			}
		}
		this.G.DrawEllipse(Pens.White, new Rectangle(new Point(3, 3), new Size(16, 16)));
		this.G.DrawEllipse(Pens.LightGray, new Rectangle(new Point(2, 2), new Size(18, 18)));
		this.G.DrawEllipse(Pens.LightGray, new Rectangle(new Point(4, 4), new Size(14, 14)));
		this.G.DrawString(this.Text, new Font("Segoe UI", 9f), Brushes.Gray, 23f, 3f);
	}

	// Token: 0x04000249 RID: 585
	private Color BG;

	// Token: 0x0400024A RID: 586
	private bool _Checked;

	// Token: 0x0200003D RID: 61
	// (Invoke) Token: 0x060001D0 RID: 464
	public delegate void CheckedChangedEventHandler(object sender);
}
