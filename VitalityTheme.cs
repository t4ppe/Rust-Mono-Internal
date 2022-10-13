using System;
using System.Drawing;
using System.Drawing.Drawing2D;

// Token: 0x02000038 RID: 56
internal class VitalityTheme : ThemeContainer154
{
	// Token: 0x060001B4 RID: 436 RVA: 0x000125A4 File Offset: 0x000107A4
	public VitalityTheme()
	{
		base.TransparencyKey = Color.Fuchsia;
		base.SetColor("G1", Color.White);
		base.SetColor("G2", Color.LightGray);
		base.SetColor("BG", Color.FromArgb(240, 240, 240));
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x00002E8B File Offset: 0x0000108B
	protected override void ColorHook()
	{
		this.G1 = base.GetColor("G1");
		this.G2 = base.GetColor("G2");
		this.BG = base.GetColor("BG");
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x00012608 File Offset: 0x00010808
	protected override void PaintHook()
	{
		this.G.Clear(this.BG);
		LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(new Point(1, 1), new Size(base.Width - 2, 23)), this.G1, this.G2, 90f);
		this.G.FillRectangle(brush, new Rectangle(new Point(1, 1), new Size(base.Width - 2, 23)));
		this.G.DrawLine(Pens.LightGray, 1, 25, base.Width - 2, 25);
		this.G.DrawLine(Pens.White, 1, 26, base.Width - 2, 26);
		base.DrawCorners(base.TransparencyKey);
		base.DrawBorders(Pens.LightGray, 1);
		Rectangle targetRect = new Rectangle(3, 3, 20, 20);
		this.G.DrawIcon(base.ParentForm.Icon, targetRect);
		this.G.DrawString(base.ParentForm.Text, new Font("Segoe UI", 9f), Brushes.Gray, new Point(25, 5));
	}

	// Token: 0x04000240 RID: 576
	private Color G1;

	// Token: 0x04000241 RID: 577
	private Color G2;

	// Token: 0x04000242 RID: 578
	private Color BG;
}
