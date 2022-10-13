using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CS_ClassLibraryTester
{
	// Token: 0x0200004C RID: 76
	public class MephButton : Control
	{
		// Token: 0x06000203 RID: 515 RVA: 0x000030DA File Offset: 0x000012DA
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			this.State = MouseState.Down;
			base.Invalidate();
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000030F3 File Offset: 0x000012F3
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			this.State = MouseState.Over;
			base.Invalidate();
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000310C File Offset: 0x0000130C
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			this.State = MouseState.Over;
			base.Invalidate();
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00003125 File Offset: 0x00001325
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.State = MouseState.None;
			base.Invalidate();
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000152D4 File Offset: 0x000134D4
		public MephButton()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
			this.BackColor = Color.Transparent;
			this.ForeColor = Color.FromArgb(205, 205, 205);
			this.DoubleBuffered = true;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0001532C File Offset: 0x0001352C
		protected override void OnPaint(PaintEventArgs e)
		{
			Bitmap bitmap = new Bitmap(base.Width, base.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			Rectangle rectangle = new Rectangle(0, 0, base.Width - 1, base.Height - 1);
			base.OnPaint(e);
			graphics.Clear(this.BackColor);
			Font font = new Font("Verdana", 8f, FontStyle.Regular);
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.FillPath(new SolidBrush(Color.FromArgb(40, 40, 40)), Draw.RoundRect(rectangle, 3));
			graphics.DrawPath(new Pen(new SolidBrush(Color.FromArgb(15, 15, 15))), Draw.RoundRect(rectangle, 3));
			graphics.DrawPath(new Pen(new SolidBrush(Color.FromArgb(55, 55, 55))), Draw.RoundRect(new Rectangle(1, 1, base.Width - 3, base.Height - 3), 3));
			switch (this.State)
			{
			case MouseState.None:
				graphics.DrawString(this.Text, font, Brushes.Silver, new Rectangle(0, 0, base.Width - 1, base.Height - 1), new StringFormat
				{
					Alignment = StringAlignment.Center,
					LineAlignment = StringAlignment.Center
				});
				break;
			case MouseState.Over:
				graphics.DrawString(this.Text, font, Brushes.White, new Rectangle(0, 0, base.Width - 1, base.Height - 1), new StringFormat
				{
					Alignment = StringAlignment.Center,
					LineAlignment = StringAlignment.Center
				});
				break;
			case MouseState.Down:
				graphics.DrawString(this.Text, font, Brushes.Gray, new Rectangle(0, 0, base.Width - 1, base.Height - 1), new StringFormat
				{
					Alignment = StringAlignment.Center,
					LineAlignment = StringAlignment.Center
				});
				break;
			}
			e.Graphics.DrawImage((Bitmap)bitmap.Clone(), 0, 0);
			graphics.Dispose();
			bitmap.Dispose();
		}

		// Token: 0x0400026F RID: 623
		private MouseState State = MouseState.None;
	}
}
