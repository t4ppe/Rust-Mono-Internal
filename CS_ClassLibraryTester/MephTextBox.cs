using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CS_ClassLibraryTester
{
	// Token: 0x02000051 RID: 81
	public class MephTextBox : Control
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00015DA8 File Offset: 0x00013FA8
		// (set) Token: 0x0600021F RID: 543 RVA: 0x00015DC0 File Offset: 0x00013FC0
		public TextBox txtbox
		{
			get
			{
				return this.withEventsField_txtbox;
			}
			set
			{
				bool flag = this.withEventsField_txtbox != null;
				if (flag)
				{
				}
				this.withEventsField_txtbox = value;
				bool flag2 = this.withEventsField_txtbox != null;
				if (flag2)
				{
				}
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00015DF4 File Offset: 0x00013FF4
		// (set) Token: 0x06000221 RID: 545 RVA: 0x000031F3 File Offset: 0x000013F3
		public bool UseSystemPasswordChar
		{
			get
			{
				return this._passmask;
			}
			set
			{
				this.txtbox.UseSystemPasswordChar = this.UseSystemPasswordChar;
				this._passmask = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00015E0C File Offset: 0x0001400C
		// (set) Token: 0x06000223 RID: 547 RVA: 0x00003216 File Offset: 0x00001416
		public int MaxLength
		{
			get
			{
				return this._maxchars;
			}
			set
			{
				this._maxchars = value;
				this.txtbox.MaxLength = this.MaxLength;
				base.Invalidate();
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00015E24 File Offset: 0x00014024
		// (set) Token: 0x06000225 RID: 549 RVA: 0x00003239 File Offset: 0x00001439
		public HorizontalAlignment TextAlignment
		{
			get
			{
				return this._align;
			}
			set
			{
				this._align = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00015E3C File Offset: 0x0001403C
		// (set) Token: 0x06000227 RID: 551 RVA: 0x0000324A File Offset: 0x0000144A
		public bool MultiLine
		{
			get
			{
				return this._multiline;
			}
			set
			{
				this._multiline = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00015E54 File Offset: 0x00014054
		// (set) Token: 0x06000229 RID: 553 RVA: 0x0000325B File Offset: 0x0000145B
		public bool WordWrap
		{
			get
			{
				return this._wordwrap;
			}
			set
			{
				this._wordwrap = value;
				base.Invalidate();
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000241F File Offset: 0x0000061F
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000031B3 File Offset: 0x000013B3
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			base.Invalidate();
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000326C File Offset: 0x0000146C
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			this.txtbox.BackColor = this.BackColor;
			base.Invalidate();
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00003290 File Offset: 0x00001490
		protected override void OnForeColorChanged(EventArgs e)
		{
			base.OnForeColorChanged(e);
			this.txtbox.ForeColor = this.ForeColor;
			base.Invalidate();
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000032B4 File Offset: 0x000014B4
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.txtbox.Font = this.Font;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000032D1 File Offset: 0x000014D1
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			this.txtbox.Focus();
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000032E8 File Offset: 0x000014E8
		public void TextChngTxtBox()
		{
			this.Text = this.txtbox.Text;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000032FD File Offset: 0x000014FD
		public void TextChng()
		{
			this.txtbox.Text = this.Text;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00015E6C File Offset: 0x0001406C
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			bool flag = !this.MultiLine;
			if (flag)
			{
				base.Height = 24;
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00015E9C File Offset: 0x0001409C
		public void NewTextBox()
		{
			TextBox txtbox = this.txtbox;
			txtbox.Multiline = this.MultiLine;
			txtbox.BackColor = Color.FromArgb(50, 50, 50);
			txtbox.ForeColor = this.ForeColor;
			txtbox.Text = string.Empty;
			txtbox.TextAlign = HorizontalAlignment.Center;
			txtbox.BorderStyle = BorderStyle.None;
			txtbox.Location = new Point(5, 4);
			txtbox.Font = new Font("Verdana", 8f, FontStyle.Regular);
			bool multiLine = this.MultiLine;
			if (multiLine)
			{
				bool wordWrap = this.WordWrap;
				if (wordWrap)
				{
					txtbox.WordWrap = true;
				}
				else
				{
					txtbox.WordWrap = false;
				}
			}
			else
			{
				bool wordWrap2 = this.WordWrap;
				if (wordWrap2)
				{
					txtbox.WordWrap = true;
				}
				else
				{
					txtbox.WordWrap = false;
				}
			}
			txtbox.Size = new Size(base.Width - 10, base.Height - 11);
			txtbox.UseSystemPasswordChar = this.UseSystemPasswordChar;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00015F9C File Offset: 0x0001419C
		public MephTextBox()
		{
			this.NewTextBox();
			base.Controls.Add(this.txtbox);
			this.Text = "";
			this.BackColor = Color.FromArgb(50, 50, 50);
			this.ForeColor = Color.Silver;
			base.Size = new Size(135, 24);
			this.DoubleBuffered = true;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0001603C File Offset: 0x0001423C
		protected override void OnPaint(PaintEventArgs e)
		{
			Bitmap bitmap = new Bitmap(base.Width, base.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			Rectangle rectangle = new Rectangle(0, 0, base.Width - 1, base.Height - 1);
			TextBox txtbox = this.txtbox;
			txtbox.Multiline = this.MultiLine;
			bool flag = !this.MultiLine;
			if (flag)
			{
				base.Height = this.txtbox.Height + 11;
				bool wordWrap = this.WordWrap;
				if (wordWrap)
				{
					txtbox.WordWrap = true;
				}
				else
				{
					txtbox.WordWrap = false;
				}
			}
			else
			{
				this.txtbox.Height = base.Height - 11;
				bool wordWrap2 = this.WordWrap;
				if (wordWrap2)
				{
					txtbox.WordWrap = true;
				}
				else
				{
					txtbox.WordWrap = false;
				}
			}
			txtbox.Width = base.Width - 10;
			txtbox.TextAlign = this.TextAlignment;
			txtbox.UseSystemPasswordChar = this.UseSystemPasswordChar;
			graphics.Clear(this.BackColor);
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
			graphics.FillPath(new SolidBrush(Color.FromArgb(50, 50, 50)), Draw.RoundRect(rectangle, 3));
			Draw.InnerGlowRounded(graphics, rectangle, 3, colors);
			e.Graphics.DrawImage((Bitmap)bitmap.Clone(), 0, 0);
			graphics.Dispose();
			bitmap.Dispose();
		}

		// Token: 0x04000277 RID: 631
		private TextBox withEventsField_txtbox = new TextBox();

		// Token: 0x04000278 RID: 632
		private bool _passmask = false;

		// Token: 0x04000279 RID: 633
		private int _maxchars = 32767;

		// Token: 0x0400027A RID: 634
		private HorizontalAlignment _align;

		// Token: 0x0400027B RID: 635
		private bool _multiline = false;

		// Token: 0x0400027C RID: 636
		private bool _wordwrap = false;
	}
}
