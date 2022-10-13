using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CS_ClassLibraryTester
{
	// Token: 0x0200004B RID: 75
	public class MephTheme : ContainerControl
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0001449C File Offset: 0x0001269C
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x00003036 File Offset: 0x00001236
		public string SubHeader
		{
			get
			{
				return this._subHeader;
			}
			set
			{
				this._subHeader = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x000144B4 File Offset: 0x000126B4
		// (set) Token: 0x060001FA RID: 506 RVA: 0x00003047 File Offset: 0x00001247
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

		// Token: 0x060001FB RID: 507 RVA: 0x000144CC File Offset: 0x000126CC
		public MephTheme()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
			this.BackColor = Color.FromArgb(28, 28, 28);
			this._subHeader = "Insert Sub Header";
			this._accentColor = Color.DarkRed;
			this.DoubleBuffered = true;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0001455C File Offset: 0x0001275C
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			Rectangle rectangle = new Rectangle(0, 0, base.Width, base.Height);
			base.OnPaint(e);
			graphics.Clear(Color.Fuchsia);
			Color[] colors = new Color[]
			{
				Color.FromArgb(10, 10, 10),
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
			Color[] colors2 = new Color[]
			{
				Color.FromArgb(5, 5, 5),
				Color.FromArgb(40, 40, 40),
				Color.FromArgb(41, 41, 41),
				Color.FromArgb(42, 42, 42),
				Color.FromArgb(43, 43, 43),
				Color.FromArgb(44, 44, 44),
				Color.FromArgb(45, 45, 45)
			};
			graphics.FillRectangle(new SolidBrush(Color.FromArgb(45, 45, 45)), new Rectangle(0, 35, base.Width, 23));
			Draw.InnerGlow(graphics, new Rectangle(0, 35, base.Width, 23), colors2);
			LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 36, 11, 21), this._accentColor, Color.FromArgb((int)((this._accentColor.R >= 10) ? (this._accentColor.R - 10) : (this._accentColor.R + 10)), (int)((this._accentColor.G >= 10) ? (this._accentColor.G - 10) : (this._accentColor.G + 10)), (int)((this._accentColor.B >= 10) ? (this._accentColor.B - 10) : (this._accentColor.B + 10))), 90f);
			graphics.FillRectangle(brush, new Rectangle(0, 36, 11, 21));
			graphics.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(5, 5, 5))), new Rectangle(0, 35, 11, 22));
			graphics.FillRectangle(brush, new Rectangle(base.Width - 12, 36, 11, 21));
			graphics.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(5, 5, 5))), new Rectangle(base.Width - 12, 35, 11, 22));
			LinearGradientBrush brush2 = new LinearGradientBrush(new Rectangle(1, 0, base.Width - 1, 17), Color.FromArgb(255, Color.FromArgb(90, 90, 90)), Color.FromArgb(255, 71, 71, 71), 90f);
			graphics.FillRectangle(brush2, new Rectangle(1, 0, base.Width - 2, 17));
			graphics.DrawLine(new Pen(new SolidBrush(Color.FromArgb(5, 5, 5))), 0, 0, base.Width, 0);
			graphics.DrawLine(new Pen(new SolidBrush(Color.FromArgb(150, 150, 150))), 1, 1, base.Width - 2, 1);
			graphics.DrawLine(new Pen(new SolidBrush(Color.FromArgb(85, 85, 85))), 1, 34, base.Width - 2, 34);
			graphics.DrawLine(new Pen(new SolidBrush(Color.FromArgb(45, 45, 45))), 1, 58, base.Width - 2, 58);
			Font font = new Font("Verdana", 10f, FontStyle.Regular);
			graphics.DrawString(this.Text, font, new SolidBrush(Color.FromArgb(225, 225, 225)), new Rectangle(0, 0, base.Width, 35), new StringFormat
			{
				Alignment = StringAlignment.Center,
				LineAlignment = StringAlignment.Center
			});
			Font font2 = new Font("Verdana", 8f, FontStyle.Regular);
			graphics.DrawString(this._subHeader, font2, new SolidBrush(Color.FromArgb(225, 225, 225)), new Rectangle(0, 35, base.Width, 23), new StringFormat
			{
				Alignment = StringAlignment.Center,
				LineAlignment = StringAlignment.Center
			});
			Font font3 = new Font("Marlett", 10f, FontStyle.Regular);
			switch (this.State)
			{
			case MouseState.None:
				graphics.DrawString("r", font3, new SolidBrush(Color.FromArgb(178, 178, 178)), new Rectangle(-4, -6, base.Width, 35), new StringFormat
				{
					Alignment = StringAlignment.Far,
					LineAlignment = StringAlignment.Center
				});
				graphics.DrawString("1", font3, new SolidBrush(Color.FromArgb(178, 178, 178)), new Rectangle(-21, -5, base.Width, 35), new StringFormat
				{
					Alignment = StringAlignment.Far,
					LineAlignment = StringAlignment.Center
				});
				graphics.DrawString("0", font3, new SolidBrush(Color.FromArgb(178, 178, 178)), new Rectangle(-38, -6, base.Width, 35), new StringFormat
				{
					Alignment = StringAlignment.Far,
					LineAlignment = StringAlignment.Center
				});
				break;
			case MouseState.Over:
			{
				bool flag = this.X > base.Width - 18 & this.X < base.Width - 10 & this.Y < 18 & this.Y > 8;
				if (flag)
				{
					graphics.DrawString("r", font3, new SolidBrush(Color.FromArgb(255, 255, 255)), new Rectangle(-4, -6, base.Width, 35), new StringFormat
					{
						Alignment = StringAlignment.Far,
						LineAlignment = StringAlignment.Center
					});
					graphics.DrawString("1", font3, new SolidBrush(Color.FromArgb(178, 178, 178)), new Rectangle(-21, -5, base.Width, 35), new StringFormat
					{
						Alignment = StringAlignment.Far,
						LineAlignment = StringAlignment.Center
					});
					graphics.DrawString("0", font3, new SolidBrush(Color.FromArgb(178, 178, 178)), new Rectangle(-38, -6, base.Width, 35), new StringFormat
					{
						Alignment = StringAlignment.Far,
						LineAlignment = StringAlignment.Center
					});
				}
				else
				{
					bool flag2 = this.X > base.Width - 36 & this.X < base.Width - 25 & this.Y < 18 & this.Y > 8;
					if (flag2)
					{
						graphics.DrawString("r", font3, new SolidBrush(Color.FromArgb(178, 178, 178)), new Rectangle(-4, -6, base.Width, 35), new StringFormat
						{
							Alignment = StringAlignment.Far,
							LineAlignment = StringAlignment.Center
						});
						graphics.DrawString("1", font3, new SolidBrush(Color.FromArgb(255, 255, 255)), new Rectangle(-21, -5, base.Width, 35), new StringFormat
						{
							Alignment = StringAlignment.Far,
							LineAlignment = StringAlignment.Center
						});
						graphics.DrawString("0", font3, new SolidBrush(Color.FromArgb(178, 178, 178)), new Rectangle(-38, -6, base.Width, 35), new StringFormat
						{
							Alignment = StringAlignment.Far,
							LineAlignment = StringAlignment.Center
						});
					}
					else
					{
						bool flag3 = this.X > base.Width - 52 & this.X < base.Width - 44 & this.Y < 18 & this.Y > 8;
						if (flag3)
						{
							graphics.DrawString("r", font3, new SolidBrush(Color.FromArgb(178, 178, 178)), new Rectangle(-4, -6, base.Width, 35), new StringFormat
							{
								Alignment = StringAlignment.Far,
								LineAlignment = StringAlignment.Center
							});
							graphics.DrawString("1", font3, new SolidBrush(Color.FromArgb(178, 178, 178)), new Rectangle(-21, -5, base.Width, 35), new StringFormat
							{
								Alignment = StringAlignment.Far,
								LineAlignment = StringAlignment.Center
							});
							graphics.DrawString("0", font3, new SolidBrush(Color.FromArgb(255, 255, 255)), new Rectangle(-38, -6, base.Width, 35), new StringFormat
							{
								Alignment = StringAlignment.Far,
								LineAlignment = StringAlignment.Center
							});
						}
						else
						{
							graphics.DrawString("r", font3, new SolidBrush(Color.FromArgb(178, 178, 178)), new Rectangle(-4, -6, base.Width, 35), new StringFormat
							{
								Alignment = StringAlignment.Far,
								LineAlignment = StringAlignment.Center
							});
							graphics.DrawString("1", font3, new SolidBrush(Color.FromArgb(178, 178, 178)), new Rectangle(-21, -5, base.Width, 35), new StringFormat
							{
								Alignment = StringAlignment.Far,
								LineAlignment = StringAlignment.Center
							});
							graphics.DrawString("0", font3, new SolidBrush(Color.FromArgb(178, 178, 178)), new Rectangle(-38, -6, base.Width, 35), new StringFormat
							{
								Alignment = StringAlignment.Far,
								LineAlignment = StringAlignment.Center
							});
						}
					}
				}
				break;
			}
			case MouseState.Down:
				graphics.DrawString("r", font3, new SolidBrush(Color.FromArgb(178, 178, 178)), new Rectangle(-4, -6, base.Width, 35), new StringFormat
				{
					Alignment = StringAlignment.Far,
					LineAlignment = StringAlignment.Center
				});
				graphics.DrawString("1", font3, new SolidBrush(Color.FromArgb(178, 178, 178)), new Rectangle(-21, -5, base.Width, 35), new StringFormat
				{
					Alignment = StringAlignment.Far,
					LineAlignment = StringAlignment.Center
				});
				graphics.DrawString("0", font3, new SolidBrush(Color.FromArgb(178, 178, 178)), new Rectangle(-38, -6, base.Width, 35), new StringFormat
				{
					Alignment = StringAlignment.Far,
					LineAlignment = StringAlignment.Center
				});
				break;
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000150F8 File Offset: 0x000132F8
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			bool flag = e.Button == MouseButtons.Left & new Rectangle(0, 0, base.Width, base.Height).Contains(e.Location) & this.X < base.Width - 53;
			if (flag)
			{
				this.Cap = true;
				this.MouseP = e.Location;
			}
			else
			{
				bool flag2 = this.X > base.Width - 18 & this.X < base.Width - 8 & this.Y < 18 & this.Y > 8;
				if (flag2)
				{
					base.FindForm().Close();
				}
				else
				{
					bool flag3 = this.X > base.Width - 36 & this.X < base.Width - 25 & this.Y < 18 & this.Y > 8;
					if (flag3)
					{
						bool flag4 = base.FindForm().WindowState == FormWindowState.Maximized;
						if (flag4)
						{
							base.FindForm().WindowState = FormWindowState.Normal;
						}
						else
						{
							base.FindForm().WindowState = FormWindowState.Maximized;
						}
					}
					else
					{
						bool flag5 = this.X > base.Width - 52 & this.X < base.Width - 44 & this.Y < 18 & this.Y > 8;
						if (flag5)
						{
							base.FindForm().WindowState = FormWindowState.Minimized;
						}
					}
				}
			}
			this.State = MouseState.Down;
			base.Invalidate();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00003058 File Offset: 0x00001258
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			this.State = MouseState.Over;
			base.Invalidate();
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00003071 File Offset: 0x00001271
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.State = MouseState.None;
			base.Invalidate();
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000308A File Offset: 0x0000128A
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			this.Cap = false;
			this.State = MouseState.Over;
			base.Invalidate();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0001528C File Offset: 0x0001348C
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			this.X = e.Location.X;
			this.Y = e.Location.Y;
			base.Invalidate();
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000030AA File Offset: 0x000012AA
		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			base.ParentForm.FormBorderStyle = FormBorderStyle.None;
			base.ParentForm.TransparencyKey = Color.Fuchsia;
			this.Dock = DockStyle.Fill;
		}

		// Token: 0x04000266 RID: 614
		private string _subHeader;

		// Token: 0x04000267 RID: 615
		private Color _accentColor;

		// Token: 0x04000268 RID: 616
		private Point MouseP = new Point(0, 0);

		// Token: 0x04000269 RID: 617
		private bool Cap = false;

		// Token: 0x0400026A RID: 618
		private MouseState State = MouseState.None;

		// Token: 0x0400026B RID: 619
		private int X;

		// Token: 0x0400026C RID: 620
		private int Y;

		// Token: 0x0400026D RID: 621
		private Rectangle MinBtn = new Rectangle(0, 0, 32, 25);

		// Token: 0x0400026E RID: 622
		private Rectangle CloseBtn = new Rectangle(33, 0, 65, 25);
	}
}
