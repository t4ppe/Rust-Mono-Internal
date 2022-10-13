using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace CS_ClassLibraryTester
{
	// Token: 0x02000057 RID: 87
	[DefaultEvent("CheckedChanged")]
	public class MephCheckBox : Control
	{
		// Token: 0x06000265 RID: 613 RVA: 0x00003443 File Offset: 0x00001643
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			this.State = MouseState.Over;
			base.Invalidate();
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000345C File Offset: 0x0000165C
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			this.State = MouseState.Down;
			base.Invalidate();
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00003475 File Offset: 0x00001675
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.State = MouseState.None;
			base.Invalidate();
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000348E File Offset: 0x0000168E
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			this.State = MouseState.Over;
			base.Invalidate();
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000031B3 File Offset: 0x000013B3
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			base.Invalidate();
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600026A RID: 618 RVA: 0x00017F40 File Offset: 0x00016140
		// (set) Token: 0x0600026B RID: 619 RVA: 0x000034A7 File Offset: 0x000016A7
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
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00017F58 File Offset: 0x00016158
		// (set) Token: 0x0600026D RID: 621 RVA: 0x000034B8 File Offset: 0x000016B8
		public Color AccentColor
		{
			get
			{
				return this._accentColor;
			}
			set
			{
				this._accentColor = value;
				base.Invalidate();
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000033B3 File Offset: 0x000015B3
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			base.Height = 24;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00017F70 File Offset: 0x00016170
		protected override void OnClick(EventArgs e)
		{
			this._Checked = !this._Checked;
			bool flag = this.CheckedChanged != null;
			if (flag)
			{
				this.CheckedChanged(this);
			}
			base.OnClick(e);
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000270 RID: 624 RVA: 0x00017FB4 File Offset: 0x000161B4
		// (remove) Token: 0x06000271 RID: 625 RVA: 0x00017FEC File Offset: 0x000161EC
		
		public event MephCheckBox.CheckedChangedEventHandler CheckedChanged;

		// Token: 0x06000272 RID: 626 RVA: 0x00018024 File Offset: 0x00016224
		public MephCheckBox()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);
			this.BackColor = Color.Transparent;
			this.ForeColor = Color.Black;
			base.Size = new Size(250, 24);
			this._accentColor = Color.Maroon;
			this.DoubleBuffered = true;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0001808C File Offset: 0x0001628C
		protected override void OnPaint(PaintEventArgs e)
		{
			Bitmap bitmap = new Bitmap(base.Width, base.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			Rectangle rect = new Rectangle(0, 0, base.Height - 1, base.Height - 1);
			Rectangle rect2 = new Rectangle(5, 5, base.Height - 11, base.Height - 11);
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.CompositingQuality = CompositingQuality.HighQuality;
			graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
			graphics.Clear(this.BackColor);
			LinearGradientBrush brush = new LinearGradientBrush(rect, Color.FromArgb(50, 50, 50), Color.FromArgb(40, 40, 40), 90f);
			graphics.FillRectangle(brush, rect);
			graphics.DrawRectangle(new Pen(Color.FromArgb(20, 20, 20)), rect);
			graphics.DrawRectangle(new Pen(Color.FromArgb(55, 55, 55)), new Rectangle(1, 1, base.Height - 3, base.Height - 3));
			bool @checked = this.Checked;
			if (@checked)
			{
				LinearGradientBrush brush2 = new LinearGradientBrush(rect2, this._accentColor, Color.FromArgb((int)(this._accentColor.R + 5), (int)(this._accentColor.G + 5), (int)(this._accentColor.B + 5)), 90f);
				graphics.FillRectangle(brush2, rect2);
				graphics.DrawRectangle(new Pen(Color.FromArgb(25, 25, 25)), rect2);
			}
			Font font = new Font("Tahoma", 10f, FontStyle.Bold);
			Brush brush3 = new SolidBrush(Color.FromArgb(200, 200, 200));
			graphics.DrawString(this.Text, font, brush3, new Point(28, 12), new StringFormat
			{
				Alignment = StringAlignment.Near,
				LineAlignment = StringAlignment.Center
			});
			e.Graphics.DrawImage((Bitmap)bitmap.Clone(), 0, 0);
			graphics.Dispose();
			bitmap.Dispose();
		}

		// Token: 0x0400028A RID: 650
		private MouseState State = MouseState.None;

		// Token: 0x0400028B RID: 651
		private bool _Checked;

		// Token: 0x0400028C RID: 652
		private Color _accentColor;

		// Token: 0x02000058 RID: 88
		// (Invoke) Token: 0x06000275 RID: 629
		public delegate void CheckedChangedEventHandler(object sender);
	}
}
