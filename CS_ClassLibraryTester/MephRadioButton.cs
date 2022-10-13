using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace CS_ClassLibraryTester
{
	// Token: 0x02000053 RID: 83
	[DefaultEvent("CheckedChanged")]
	public class MephRadioButton : Control
	{
		// Token: 0x06000240 RID: 576 RVA: 0x0000334F File Offset: 0x0000154F
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			this.State = MouseState.Over;
			base.Invalidate();
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00003368 File Offset: 0x00001568
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			this.State = MouseState.Down;
			base.Invalidate();
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00003381 File Offset: 0x00001581
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.State = MouseState.None;
			base.Invalidate();
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000339A File Offset: 0x0000159A
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			this.State = MouseState.Over;
			base.Invalidate();
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000033B3 File Offset: 0x000015B3
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			base.Height = 24;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000031B3 File Offset: 0x000013B3
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			base.Invalidate();
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000246 RID: 582 RVA: 0x000165B4 File Offset: 0x000147B4
		// (set) Token: 0x06000247 RID: 583 RVA: 0x000165CC File Offset: 0x000147CC
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

		// Token: 0x06000248 RID: 584 RVA: 0x0001660C File Offset: 0x0001480C
		protected override void OnClick(EventArgs e)
		{
			bool flag = !this._Checked;
			if (flag)
			{
				this.Checked = true;
			}
			base.OnClick(e);
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000249 RID: 585 RVA: 0x00016638 File Offset: 0x00014838
		// (remove) Token: 0x0600024A RID: 586 RVA: 0x00016670 File Offset: 0x00014870
		
		public event MephRadioButton.CheckedChangedEventHandler CheckedChanged;

		// Token: 0x0600024B RID: 587 RVA: 0x000033C7 File Offset: 0x000015C7
		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			this.InvalidateControls();
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000166A8 File Offset: 0x000148A8
		private void InvalidateControls()
		{
			bool flag = !base.IsHandleCreated || !this._Checked;
			if (!flag)
			{
				foreach (object obj in base.Parent.Controls)
				{
					Control control = (Control)obj;
					bool flag2 = control != this && control is MephRadioButton;
					if (flag2)
					{
						((MephRadioButton)control).Checked = false;
					}
				}
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00016744 File Offset: 0x00014944
		// (set) Token: 0x0600024E RID: 590 RVA: 0x000033D8 File Offset: 0x000015D8
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

		// Token: 0x0600024F RID: 591 RVA: 0x0001675C File Offset: 0x0001495C
		public MephRadioButton()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
			this.BackColor = Color.Transparent;
			this.ForeColor = Color.Black;
			base.Size = new Size(150, 24);
			this._accentColor = Color.Maroon;
			this.DoubleBuffered = true;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x000167C4 File Offset: 0x000149C4
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

		// Token: 0x04000282 RID: 642
		private Rectangle R1;

		// Token: 0x04000283 RID: 643
		private LinearGradientBrush G1;

		// Token: 0x04000284 RID: 644
		private MouseState State = MouseState.None;

		// Token: 0x04000285 RID: 645
		private bool _Checked;

		// Token: 0x04000287 RID: 647
		private Color _accentColor;

		// Token: 0x02000054 RID: 84
		// (Invoke) Token: 0x06000252 RID: 594
		public delegate void CheckedChangedEventHandler(object sender);
	}
}
