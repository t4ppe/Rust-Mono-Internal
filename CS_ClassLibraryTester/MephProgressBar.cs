using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace CS_ClassLibraryTester
{
	// Token: 0x02000052 RID: 82
	public class MephProgressBar : Control
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0001623C File Offset: 0x0001443C
		// (set) Token: 0x06000237 RID: 567 RVA: 0x00003312 File Offset: 0x00001512
		public int Maximum
		{
			get
			{
				return this._Maximum;
			}
			set
			{
				this._Maximum = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00016254 File Offset: 0x00014454
		// (set) Token: 0x06000239 RID: 569 RVA: 0x00003323 File Offset: 0x00001523
		public int Value
		{
			get
			{
				int value = this._Value;
				int result;
				if (value != 0)
				{
					result = this._Value;
				}
				else
				{
					result = 0;
				}
				return result;
			}
			set
			{
				this._Value = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0001627C File Offset: 0x0001447C
		// (set) Token: 0x0600023B RID: 571 RVA: 0x00003334 File Offset: 0x00001534
		public bool ShowPercentage
		{
			get
			{
				return this._ShowPercentage;
			}
			set
			{
				this._ShowPercentage = value;
				base.Invalidate();
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00003345 File Offset: 0x00001545
		protected override void CreateHandle()
		{
			base.CreateHandle();
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00016294 File Offset: 0x00014494
		public void Animate()
		{
			for (;;)
			{
				bool flag = this.OFS <= base.Width;
				if (flag)
				{
					this.OFS++;
				}
				else
				{
					this.OFS = 0;
				}
				base.Invalidate();
				Thread.Sleep(this.Speed);
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000162EC File Offset: 0x000144EC
		public MephProgressBar()
		{
			this.DoubleBuffered = true;
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
			this.BackColor = Color.Transparent;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00016348 File Offset: 0x00014548
		protected override void OnPaint(PaintEventArgs e)
		{
			Bitmap bitmap = new Bitmap(base.Width, base.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			int num = Convert.ToInt32(this._Value / this._Maximum * base.Width);
			graphics.Clear(this.BackColor);
			SolidBrush brush = new SolidBrush(Color.White);
			Color[] colors = new Color[]
			{
				Color.FromArgb(20, 20, 20),
				Color.FromArgb(40, 40, 40),
				Color.FromArgb(45, 45, 45),
				Color.FromArgb(46, 46, 46),
				Color.FromArgb(47, 47, 47),
				Color.FromArgb(48, 48, 48),
				Color.FromArgb(49, 49, 49),
				Color.FromArgb(50, 50, 50)
			};
			graphics.FillPath(new SolidBrush(Color.FromArgb(50, 50, 50)), Draw.RoundRect(new Rectangle(0, 0, base.Width - 1, base.Height - 1), 3));
			Draw.InnerGlowRounded(graphics, base.ClientRectangle, 3, colors);
			bool flag = num != 0;
			if (flag)
			{
				graphics.FillPath(new LinearGradientBrush(new Rectangle(1, 1, num, base.Height - 3), Color.FromArgb(30, 30, 30), Color.FromArgb(35, 35, 35), 90f), Draw.RoundRect(new Rectangle(1, 1, num, base.Height - 3), 2));
				graphics.DrawPath(new Pen(Color.FromArgb(45, 45, 45)), Draw.RoundRect(new Rectangle(1, 1, num, base.Height - 3), 2));
				brush = new SolidBrush(Color.White);
			}
			bool showPercentage = this._ShowPercentage;
			if (showPercentage)
			{
				graphics.DrawString(Convert.ToString(this.Value + "%"), new Font("Tahoma", 9f, FontStyle.Bold), brush, new Rectangle(0, 1, base.Width - 1, base.Height - 1), new StringFormat
				{
					Alignment = StringAlignment.Center,
					LineAlignment = StringAlignment.Center
				});
			}
			e.Graphics.DrawImage((Bitmap)bitmap.Clone(), 0, 0);
			graphics.Dispose();
			bitmap.Dispose();
		}

		// Token: 0x0400027D RID: 637
		private int OFS = 0;

		// Token: 0x0400027E RID: 638
		private int Speed = 50;

		// Token: 0x0400027F RID: 639
		private int _Maximum = 100;

		// Token: 0x04000280 RID: 640
		private int _Value = 0;

		// Token: 0x04000281 RID: 641
		private bool _ShowPercentage = false;
	}
}
