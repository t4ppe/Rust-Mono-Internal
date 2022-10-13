using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace CS_ClassLibraryTester
{
	// Token: 0x0200004F RID: 79
	[DefaultEvent("CheckedChanged")]
	public class MephToggleSwitch : Control
	{
		// Token: 0x0600020D RID: 525 RVA: 0x0000314F File Offset: 0x0000134F
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			this.State = MouseState.Over;
			base.Invalidate();
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00003168 File Offset: 0x00001368
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			this.State = MouseState.Down;
			base.Invalidate();
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00003181 File Offset: 0x00001381
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.State = MouseState.None;
			base.Invalidate();
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000319A File Offset: 0x0000139A
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			this.State = MouseState.Over;
			base.Invalidate();
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000031B3 File Offset: 0x000013B3
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			base.Invalidate();
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000212 RID: 530 RVA: 0x000157F4 File Offset: 0x000139F4
		// (set) Token: 0x06000213 RID: 531 RVA: 0x000031C5 File Offset: 0x000013C5
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

		// Token: 0x06000214 RID: 532 RVA: 0x000031D6 File Offset: 0x000013D6
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			base.Height = 24;
			base.Width = 50;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0001580C File Offset: 0x00013A0C
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

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000216 RID: 534 RVA: 0x00015850 File Offset: 0x00013A50
		// (remove) Token: 0x06000217 RID: 535 RVA: 0x0001588
		public event MephToggleSwitch.CheckedChangedEventHandler CheckedChanged;

		// Token: 0x06000218 RID: 536 RVA: 0x000158C0 File Offset: 0x00013AC0
		public MephToggleSwitch()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);
			this.BackColor = Color.Transparent;
			this.ForeColor = Color.Black;
			base.Size = new Size(50, 24);
			this.DoubleBuffered = true;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0001591C File Offset: 0x00013B1C
		protected override void OnPaint(PaintEventArgs e)
		{
			Bitmap bitmap = new Bitmap(base.Width, base.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			Rectangle rectangle = new Rectangle(0, 0, base.Width - 1, base.Height - 1);
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.CompositingQuality = CompositingQuality.HighQuality;
			graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
			graphics.Clear(Color.Transparent);
			LinearGradientBrush brush = new LinearGradientBrush(rectangle, Color.FromArgb(40, 40, 40), Color.FromArgb(45, 45, 45), 90f);
			graphics.FillPath(brush, Draw.RoundRect(rectangle, 4));
			graphics.DrawPath(new Pen(Color.FromArgb(15, 15, 15)), Draw.RoundRect(rectangle, 4));
			graphics.DrawPath(new Pen(Color.FromArgb(50, 50, 50)), Draw.RoundRect(new Rectangle(1, 1, base.Width - 3, base.Height - 3), 4));
			bool @checked = this.Checked;
			if (@checked)
			{
				graphics.FillPath(new SolidBrush(Color.FromArgb(80, Color.Green)), Draw.RoundRect(new Rectangle(4, 2, 25, base.Height - 5), 4));
				graphics.FillPath(new SolidBrush(Color.FromArgb(35, 35, 35)), Draw.RoundRect(new Rectangle(2, 2, 25, base.Height - 5), 4));
				graphics.DrawPath(new Pen(new SolidBrush(Color.FromArgb(20, 20, 20))), Draw.RoundRect(new Rectangle(2, 2, 25, base.Height - 5), 4));
				switch (this.State)
				{
				case MouseState.None:
					graphics.DrawString("On", new Font("Tahoma", 8f, FontStyle.Regular), Brushes.Silver, new Point(16, base.Height - 12), new StringFormat
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Center
					});
					break;
				case MouseState.Over:
					graphics.DrawString("On", new Font("Tahoma", 8f, FontStyle.Regular), Brushes.White, new Point(16, base.Height - 12), new StringFormat
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Center
					});
					break;
				case MouseState.Down:
					graphics.DrawString("On", new Font("Tahoma", 8f, FontStyle.Regular), Brushes.Silver, new Point(16, base.Height - 12), new StringFormat
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Center
					});
					break;
				}
			}
			else
			{
				graphics.FillPath(new SolidBrush(Color.FromArgb(60, Color.Red)), Draw.RoundRect(new Rectangle(base.Width / 2 - 7, 2, base.Width - 25, base.Height - 5), 4));
				graphics.FillPath(new SolidBrush(Color.FromArgb(35, 35, 35)), Draw.RoundRect(new Rectangle(base.Width / 2 - 5, 2, base.Width - 23, base.Height - 5), 4));
				graphics.DrawPath(new Pen(new SolidBrush(Color.FromArgb(20, 20, 20))), Draw.RoundRect(new Rectangle(base.Width / 2 - 5, 2, base.Width - 23, base.Height - 5), 4));
				switch (this.State)
				{
				case MouseState.None:
					graphics.DrawString("Off", new Font("Tahoma", 8f, FontStyle.Regular), Brushes.Silver, new Point(34, base.Height - 11), new StringFormat
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Center
					});
					break;
				case MouseState.Over:
					graphics.DrawString("Off", new Font("Tahoma", 8f, FontStyle.Regular), Brushes.White, new Point(34, base.Height - 11), new StringFormat
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Center
					});
					break;
				case MouseState.Down:
					graphics.DrawString("Off", new Font("Tahoma", 8f, FontStyle.Regular), Brushes.Silver, new Point(34, base.Height - 11), new StringFormat
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Center
					});
					break;
				}
			}
			e.Graphics.DrawImage((Bitmap)bitmap.Clone(), 0, 0);
			graphics.Dispose();
			bitmap.Dispose();
		}

		// Token: 0x04000274 RID: 628
		private MouseState State = MouseState.None;

		// Token: 0x04000275 RID: 629
		private bool _Checked;

		// Token: 0x02000050 RID: 80
		// (Invoke) Token: 0x0600021B RID: 539
		public delegate void CheckedChangedEventHandler(object sender);
	}
}
