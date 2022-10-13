using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CS_ClassLibraryTester
{
	// Token: 0x02000055 RID: 85
	public class MephComboBox : ComboBox
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000255 RID: 597 RVA: 0x000169B0 File Offset: 0x00014BB0
		// (set) Token: 0x06000256 RID: 598 RVA: 0x000169C8 File Offset: 0x00014BC8
		public int StartIndex
		{
			get
			{
				return this._StartIndex;
			}
			set
			{
				this._StartIndex = value;
				try
				{
					base.SelectedIndex = value;
				}
				catch
				{
				}
				base.Invalidate();
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00016A08 File Offset: 0x00014C08
		public override Rectangle DisplayRectangle
		{
			get
			{
				return base.DisplayRectangle;
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00016A20 File Offset: 0x00014C20
		public void ReplaceItem(object sender, DrawItemEventArgs e)
		{
			e.DrawBackground();
			try
			{
				bool flag = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
				if (flag)
				{
					e.Graphics.FillRectangle(new SolidBrush(this._highlightColor), e.Bounds);
					LinearGradientBrush brush = new LinearGradientBrush(e.Bounds, Color.FromArgb(30, Color.White), Color.FromArgb(0, Color.White), 90f);
					e.Graphics.FillRectangle(brush, new Rectangle(new Point(e.Bounds.X, e.Bounds.Y), new Size(e.Bounds.Width, e.Bounds.Height)));
					e.Graphics.DrawRectangle(new Pen(Color.FromArgb(90, Color.Black))
					{
						DashStyle = DashStyle.Solid
					}, new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1));
				}
				else
				{
					e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(40, 40, 40)), e.Bounds);
				}
				using (SolidBrush solidBrush = new SolidBrush(Color.Silver))
				{
					e.Graphics.DrawString(base.GetItemText(base.Items[e.Index]), e.Font, solidBrush, new Rectangle(e.Bounds.X + 2, e.Bounds.Y, e.Bounds.Width - 4, e.Bounds.Height));
				}
			}
			catch
			{
			}
			e.DrawFocusRectangle();
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00016C3C File Offset: 0x00014E3C
		protected void DrawTriangle(Color Clr, Point FirstPoint, Point SecondPoint, Point ThirdPoint, Point FirstPoint2, Point SecondPoint2, Point ThirdPoint2, Graphics G)
		{
			List<Point> list = new List<Point>();
			list.Add(FirstPoint);
			list.Add(SecondPoint);
			list.Add(ThirdPoint);
			G.FillPolygon(new SolidBrush(Clr), list.ToArray());
			G.DrawPolygon(new Pen(new SolidBrush(Color.FromArgb(25, 25, 25))), list.ToArray());
			List<Point> list2 = new List<Point>();
			list2.Add(FirstPoint2);
			list2.Add(SecondPoint2);
			list2.Add(ThirdPoint2);
			G.FillPolygon(new SolidBrush(Clr), list2.ToArray());
			G.DrawPolygon(new Pen(new SolidBrush(Color.FromArgb(25, 25, 25))), list2.ToArray());
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00016CF8 File Offset: 0x00014EF8
		// (set) Token: 0x0600025B RID: 603 RVA: 0x000033E9 File Offset: 0x000015E9
		public Color ItemHighlightColor
		{
			get
			{
				return this._highlightColor;
			}
			set
			{
				this._highlightColor = value;
				base.Invalidate();
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00016D10 File Offset: 0x00014F10
		public MephComboBox()
		{
			base.DrawItem += this.ReplaceItem;
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
			base.DrawMode = DrawMode.OwnerDrawFixed;
			this.BackColor = Color.Transparent;
			this.ForeColor = Color.Silver;
			this.Font = new Font("Verdana", 8f, FontStyle.Regular);
			base.DropDownStyle = ComboBoxStyle.DropDownList;
			this.DoubleBuffered = true;
			base.Size = new Size(base.Width, 21);
			base.ItemHeight = 16;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00016DC4 File Offset: 0x00014FC4
		protected override void OnPaint(PaintEventArgs e)
		{
			Bitmap bitmap = new Bitmap(base.Width, base.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.Clear(this.BackColor);
			LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Rectangle(0, 0, base.Width - 1, base.Height - 2), Color.FromArgb(40, 40, 40), Color.FromArgb(40, 40, 40), 90f);
			graphics.FillPath(linearGradientBrush, Draw.RoundRect(new Rectangle((int)linearGradientBrush.Rectangle.X, (int)linearGradientBrush.Rectangle.Y, (int)linearGradientBrush.Rectangle.Width, (int)linearGradientBrush.Rectangle.Height), 3));
			LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, base.Width - 1, base.Height - 3), Color.FromArgb(40, 40, 40), Color.FromArgb(40, 40, 40), 90f);
			graphics.DrawPath(new Pen(brush), Draw.RoundRect(new Rectangle(1, 1, base.Width - 3, base.Height - 4), 3));
			graphics.DrawPath(new Pen(Color.FromArgb(20, 20, 20)), Draw.RoundRect(new Rectangle(0, 0, base.Width - 1, base.Height - 1), 3));
			graphics.DrawPath(new Pen(Color.FromArgb(55, 55, 55)), Draw.RoundRect(new Rectangle(1, 1, base.Width - 3, base.Height - 3), 3));
			this.DrawTriangle(Color.FromArgb(60, 60, 60), new Point(base.Width - 14, 12), new Point(base.Width - 7, 12), new Point(base.Width - 11, 16), new Point(base.Width - 14, 10), new Point(base.Width - 7, 10), new Point(base.Width - 11, 5), graphics);
			graphics.DrawLine(new Pen(Color.FromArgb(45, 45, 45)), new Point(base.Width - 21, 2), new Point(base.Width - 21, base.Height - 3));
			graphics.DrawLine(new Pen(Color.FromArgb(55, 55, 55)), new Point(base.Width - 20, 1), new Point(base.Width - 20, base.Height - 3));
			graphics.DrawLine(new Pen(Color.FromArgb(45, 45, 45)), new Point(base.Width - 19, 2), new Point(base.Width - 19, base.Height - 3));
			try
			{
				graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), new Rectangle(5, 0, base.Width - 20, base.Height), new StringFormat
				{
					LineAlignment = StringAlignment.Center,
					Alignment = StringAlignment.Near
				});
			}
			catch
			{
			}
			e.Graphics.DrawImage((Bitmap)bitmap.Clone(), 0, 0);
			graphics.Dispose();
			bitmap.Dispose();
		}

		// Token: 0x04000288 RID: 648
		private int _StartIndex = 0;

		// Token: 0x04000289 RID: 649
		private Color _highlightColor = Color.FromArgb(55, 55, 55);
	}
}
