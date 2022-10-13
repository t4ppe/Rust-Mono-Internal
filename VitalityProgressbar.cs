using System;
using System.Drawing;
using System.Drawing.Drawing2D;

// Token: 0x0200003E RID: 62
internal class VitalityProgressbar : ThemeControl154
{
	// Token: 0x1700004A RID: 74
	// (get) Token: 0x060001D3 RID: 467 RVA: 0x00012EF4 File Offset: 0x000110F4
	// (set) Token: 0x060001D4 RID: 468 RVA: 0x00012F0C File Offset: 0x0001110C
	public int Minimum
	{
		get
		{
			return this._Minimum;
		}
		set
		{
			bool flag = value < 0;
			if (flag)
			{
				throw new Exception("Property value is not valid.");
			}
			this._Minimum = value;
			bool flag2 = value > this._Value;
			if (flag2)
			{
				this._Value = value;
			}
			bool flag3 = value > this._Maximum;
			if (flag3)
			{
				this._Maximum = value;
			}
			base.Invalidate();
		}
	}

	// Token: 0x1700004B RID: 75
	// (get) Token: 0x060001D5 RID: 469 RVA: 0x00012F64 File Offset: 0x00011164
	// (set) Token: 0x060001D6 RID: 470 RVA: 0x00012F7C File Offset: 0x0001117C
	public int Maximum
	{
		get
		{
			return this._Maximum;
		}
		set
		{
			bool flag = value < 0;
			if (flag)
			{
				throw new Exception("Property value is not valid.");
			}
			this._Maximum = value;
			bool flag2 = value < this._Value;
			if (flag2)
			{
				this._Value = value;
			}
			bool flag3 = value < this._Minimum;
			if (flag3)
			{
				this._Minimum = value;
			}
			base.Invalidate();
		}
	}

	// Token: 0x1700004C RID: 76
	// (get) Token: 0x060001D7 RID: 471 RVA: 0x00012FD4 File Offset: 0x000111D4
	// (set) Token: 0x060001D8 RID: 472 RVA: 0x00012FEC File Offset: 0x000111EC
	public int Value
	{
		get
		{
			return this._Value;
		}
		set
		{
			bool flag = value > this._Maximum || value < this._Minimum;
			if (flag)
			{
				throw new Exception("Property value is not valid.");
			}
			this._Value = value;
			base.Invalidate();
		}
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x00002F5E File Offset: 0x0000115E
	private void Increment(int amount)
	{
		this.Value += amount;
	}

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x060001DA RID: 474 RVA: 0x00013030 File Offset: 0x00011230
	// (set) Token: 0x060001DB RID: 475 RVA: 0x00002F70 File Offset: 0x00001170
	public bool Animated
	{
		get
		{
			return base.IsAnimated;
		}
		set
		{
			base.IsAnimated = value;
			base.Invalidate();
		}
	}

	// Token: 0x060001DC RID: 476 RVA: 0x00013048 File Offset: 0x00011248
	protected override void OnAnimation()
	{
		bool flag = this.HBPos == 0;
		if (flag)
		{
			this.HBPos = 7;
		}
		else
		{
			this.HBPos++;
		}
	}

	// Token: 0x060001DD RID: 477 RVA: 0x00002F82 File Offset: 0x00001182
	public VitalityProgressbar()
	{
		this.Animated = true;
		base.SetColor("BG", Color.FromArgb(240, 240, 240));
	}

	// Token: 0x060001DE RID: 478 RVA: 0x00002FBC File Offset: 0x000011BC
	protected override void ColorHook()
	{
		this.BG = base.GetColor("BG");
	}

	// Token: 0x060001DF RID: 479 RVA: 0x00013080 File Offset: 0x00011280
	protected override void PaintHook()
	{
		this.G.Clear(this.BG);
		base.DrawBorders(Pens.LightGray, 1);
		base.DrawCorners(Color.Transparent);
		LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(new Point(2, 2), new Size(base.Width - 2, base.Height - 5)), Color.White, Color.FromArgb(240, 240, 240), 90f);
		this.G.FillRectangle(brush, new Rectangle(new Point(2, 2), new Size(base.Width / this.Maximum * this.Value - 5, base.Height - 5)));
		this.G.RenderingOrigin = new Point(this.HBPos, 0);
		HatchBrush brush2 = new HatchBrush(HatchStyle.BackwardDiagonal, Color.LightGray, Color.Transparent);
		this.G.FillRectangle(brush2, new Rectangle(new Point(1, 2), new Size(base.Width / this.Maximum * this.Value - 3, base.Height - 3)));
		this.G.DrawLine(Pens.LightGray, new Point(base.Width / this.Maximum * this.Value - 2, 1), new Point(base.Width / this.Maximum * this.Value - 2, base.Height - 3));
	}

	// Token: 0x0400024C RID: 588
	private Color BG;

	// Token: 0x0400024D RID: 589
	private int HBPos;

	// Token: 0x0400024E RID: 590
	private int _Minimum;

	// Token: 0x0400024F RID: 591
	private int _Maximum = 100;

	// Token: 0x04000250 RID: 592
	private int _Value;
}
