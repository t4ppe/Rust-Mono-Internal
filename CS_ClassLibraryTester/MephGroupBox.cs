using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CS_ClassLibraryTester
{
	// Token: 0x0200004D RID: 77
	public class MephGroupBox : ContainerControl
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00015530 File Offset: 0x00013730
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0000313E File Offset: 0x0000133E
		public MephGroupBox.HeaderLine Header_Line
		{
			get
			{
				return this._HeaderLine;
			}
			set
			{
				this._HeaderLine = value;
				base.Invalidate();
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00015548 File Offset: 0x00013748
		public MephGroupBox()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
			this.BackColor = Color.Transparent;
			this.ForeColor = Color.FromArgb(205, 205, 205);
			base.Size = new Size(174, 115);
			this._HeaderLine = MephGroupBox.HeaderLine.Enabled;
			this.DoubleBuffered = true;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000155B4 File Offset: 0x000137B4
		protected override void OnPaint(PaintEventArgs e)
		{
			Bitmap bitmap = new Bitmap(base.Width, base.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			Rectangle rectangle = new Rectangle(0, 0, base.Width - 1, base.Height - 1);
			base.OnPaint(e);
			graphics.Clear(this.BackColor);
			Font font = new Font("Verdana", 8f, FontStyle.Regular);
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			Color[] colors = new Color[]
			{
				Color.FromArgb(20, 20, 20),
				Color.FromArgb(45, 45, 45),
				Color.FromArgb(40, 40, 40),
				Color.FromArgb(45, 45, 45),
				Color.FromArgb(46, 46, 46),
				Color.FromArgb(47, 47, 47),
				Color.FromArgb(48, 48, 48),
				Color.FromArgb(49, 49, 49),
				Color.FromArgb(50, 50, 50)
			};
			graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, 50, 50)), rectangle);
			Draw.InnerGlow(graphics, rectangle, colors);
			MephGroupBox.HeaderLine headerLine = this._HeaderLine;
			if (headerLine != MephGroupBox.HeaderLine.Enabled)
			{
				if (headerLine != MephGroupBox.HeaderLine.Disabled)
				{
				}
			}
			else
			{
				graphics.DrawLine(new Pen(new SolidBrush(Color.FromArgb(45, 45, 45))), 16, 29, base.Width - 17, 29);
				graphics.DrawLine(new Pen(new SolidBrush(Color.FromArgb(20, 20, 20))), 15, 30, base.Width - 16, 30);
				graphics.DrawLine(new Pen(new SolidBrush(Color.FromArgb(45, 45, 45))), 16, 31, base.Width - 17, 31);
			}
			graphics.DrawString(this.Text, font, Brushes.Silver, new Rectangle(0, 3, base.Width - 1, 27), new StringFormat
			{
				Alignment = StringAlignment.Center,
				LineAlignment = StringAlignment.Center
			});
			e.Graphics.DrawImage((Bitmap)bitmap.Clone(), 0, 0);
			graphics.Dispose();
			bitmap.Dispose();
		}

		// Token: 0x04000270 RID: 624
		private MephGroupBox.HeaderLine _HeaderLine;

		// Token: 0x0200004E RID: 78
		public enum HeaderLine
		{
			// Token: 0x04000272 RID: 626
			Enabled,
			// Token: 0x04000273 RID: 627
			Disabled
		}
	}
}
