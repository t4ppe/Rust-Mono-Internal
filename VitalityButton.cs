using System;
using System.Drawing;
using System.Drawing.Drawing2D;

// Token: 0x02000039 RID: 57
internal class VitalityButton : ThemeControl154
{
	// Token: 0x060001B7 RID: 439 RVA: 0x00012738 File Offset: 0x00010938
	public VitalityButton()
	{
		base.Size = new Size(120, 26);
		base.SetColor("G1", Color.White);
		base.SetColor("G2", Color.LightGray);
		base.SetColor("BG", Color.FromArgb(240, 240, 240));
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x00002EC1 File Offset: 0x000010C1
	protected override void ColorHook()
	{
		this.G1 = base.GetColor("G1");
		this.G2 = base.GetColor("G2");
		this.BG = base.GetColor("BG");
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x000127A0 File Offset: 0x000109A0
	protected override void PaintHook()
	{
		this.G.Clear(this.BG);
		bool flag = this.State == MouseState.Over;
		if (flag)
		{
			this.G.FillRectangle(Brushes.White, new Rectangle(new Point(0, 0), new Size(base.Width, base.Height)));
		}
		else
		{
			bool flag2 = this.State == MouseState.Down;
			if (flag2)
			{
				LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(new Point(0, 0), new Size(base.Width, base.Height)), Color.FromArgb(240, 240, 240), Color.White, 90f);
				this.G.FillRectangle(brush, new Rectangle(new Point(0, 0), new Size(base.Width, base.Height)));
			}
			else
			{
				bool flag3 = this.State == MouseState.None;
				if (flag3)
				{
					LinearGradientBrush brush2 = new LinearGradientBrush(new Rectangle(new Point(0, 0), new Size(base.Width, base.Height)), Color.White, Color.FromArgb(240, 240, 240), 90f);
					this.G.FillRectangle(brush2, new Rectangle(new Point(0, 0), new Size(base.Width, base.Height)));
				}
			}
		}
		base.DrawBorders(Pens.LightGray);
		base.DrawCorners(Color.Transparent);
		StringFormat stringFormat = new StringFormat();
		stringFormat.Alignment = StringAlignment.Center;
		stringFormat.LineAlignment = StringAlignment.Center;
		this.G.DrawString(this.Text, new Font("Segoe UI", 9f), Brushes.Gray, new RectangleF(2f, 2f, (float)(base.Width - 5), (float)(base.Height - 4)), stringFormat);
	}

	// Token: 0x04000243 RID: 579
	private Color G1;

	// Token: 0x04000244 RID: 580
	private Color G2;

	// Token: 0x04000245 RID: 581
	private Color BG;
}
