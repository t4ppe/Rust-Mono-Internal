using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CS_ClassLibraryTester
{
	// Token: 0x02000056 RID: 86
	internal class MephTabcontrol : TabControl
	{
		// Token: 0x0600025E RID: 606 RVA: 0x00017108 File Offset: 0x00015308
		public GraphicsPath RoundRect(Rectangle Rectangle, int Curve)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			int num = Curve * 2;
			graphicsPath.AddArc(new Rectangle(Rectangle.X, Rectangle.Y, num, num), -180f, 90f);
			graphicsPath.AddArc(new Rectangle(Rectangle.Width - num + Rectangle.X, Rectangle.Y, num, num), -90f, 90f);
			graphicsPath.AddArc(new Rectangle(Rectangle.Width - num + Rectangle.X, Rectangle.Height - num + Rectangle.Y, num, num), 0f, 90f);
			graphicsPath.AddArc(new Rectangle(Rectangle.X, Rectangle.Height - num + Rectangle.Y, num, num), 90f, 90f);
			graphicsPath.AddLine(new Point(Rectangle.X, Rectangle.Height - num + Rectangle.Y), new Point(Rectangle.X, Curve + Rectangle.Y));
			return graphicsPath;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00017220 File Offset: 0x00015420
		public GraphicsPath RoundRect(int X, int Y, int Width, int Height, int Curve)
		{
			Rectangle rectangle = new Rectangle(X, Y, Width, Height);
			GraphicsPath graphicsPath = new GraphicsPath();
			int num = Curve * 2;
			graphicsPath.AddArc(new Rectangle(rectangle.X, rectangle.Y, num, num), -180f, 90f);
			graphicsPath.AddArc(new Rectangle(rectangle.Width - num + rectangle.X, rectangle.Y, num, num), -90f, 90f);
			graphicsPath.AddArc(new Rectangle(rectangle.Width - num + rectangle.X, rectangle.Height - num + rectangle.Y, num, num), 0f, 90f);
			graphicsPath.AddArc(new Rectangle(rectangle.X, rectangle.Height - num + rectangle.Y, num, num), 90f, 90f);
			graphicsPath.AddLine(new Point(rectangle.X, rectangle.Height - num + rectangle.Y), new Point(rectangle.X, Curve + rectangle.Y));
			return graphicsPath;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000033FA File Offset: 0x000015FA
		public MephTabcontrol()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
			this.DoubleBuffered = true;
			base.SizeMode = TabSizeMode.Fixed;
			base.ItemSize = new Size(35, 85);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00003431 File Offset: 0x00001631
		protected override void CreateHandle()
		{
			base.CreateHandle();
			base.Alignment = TabAlignment.Left;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00017344 File Offset: 0x00015544
		public Pen ToPen(Color color)
		{
			return new Pen(color);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0001735C File Offset: 0x0001555C
		public Brush ToBrush(Color color)
		{
			return new SolidBrush(color);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00017374 File Offset: 0x00015574
		protected override void OnPaint(PaintEventArgs e)
		{
			Bitmap bitmap = new Bitmap(base.Width, base.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			Font font = new Font("Verdana", 8f, FontStyle.Regular);
			try
			{
				base.SelectedTab.BackColor = Color.FromArgb(50, 50, 50);
			}
			catch
			{
			}
			graphics.Clear(base.Parent.FindForm().BackColor);
			graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, 50, 50)), new Rectangle(0, 0, base.ItemSize.Height + 3, base.Height - 1));
			for (int i = 0; i <= base.TabCount - 1; i++)
			{
				bool flag = i == base.SelectedIndex;
				if (flag)
				{
					Rectangle rect = new Rectangle(new Point(base.GetTabRect(i).Location.X - 2, base.GetTabRect(i).Location.Y - 2), new Size(base.GetTabRect(i).Width + 3, base.GetTabRect(i).Height - 1));
					ColorBlend colorBlend = new ColorBlend();
					colorBlend.Colors = new Color[]
					{
						Color.FromArgb(50, 50, 50),
						Color.FromArgb(50, 50, 50),
						Color.FromArgb(50, 50, 50)
					};
					colorBlend.Positions = new float[]
					{
						0f,
						0.5f,
						1f
					};
					graphics.FillRectangle(new LinearGradientBrush(rect, Color.Black, Color.Black, 90f)
					{
						InterpolationColors = colorBlend
					}, rect);
					Rectangle rectangle = new Rectangle(base.GetTabRect(i).Location.X + 4, base.GetTabRect(i).Location.Y + 2, base.GetTabRect(i).Size.Width + 10, base.GetTabRect(i).Size.Height - 11);
					graphics.FillPath(new SolidBrush(Color.FromArgb(50, 50, 50)), this.RoundRect(rectangle, 4));
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
					Draw.InnerGlow(graphics, new Rectangle(0, 0, base.ItemSize.Height + 3, base.Height - 1), colors);
					Color[] colors2 = new Color[]
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
					Draw.InnerGlowRounded(graphics, rectangle, 4, colors2);
					graphics.SmoothingMode = SmoothingMode.HighQuality;
					bool flag2 = base.ImageList != null;
					if (flag2)
					{
						try
						{
							bool flag3 = base.ImageList.Images[base.TabPages[i].ImageIndex] != null;
							if (flag3)
							{
								graphics.DrawImage(base.ImageList.Images[base.TabPages[i].ImageIndex], new Point(rect.Location.X + 8, rect.Location.Y + 6));
								graphics.DrawString("      " + base.TabPages[i].Text, new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Regular), Brushes.White, new Rectangle(rect.X, rect.Y - 1, rect.Width, rect.Height), new StringFormat
								{
									LineAlignment = StringAlignment.Center,
									Alignment = StringAlignment.Center
								});
							}
							else
							{
								graphics.DrawString(base.TabPages[i].Text, font, Brushes.White, new Rectangle(rect.X, rect.Y - 1, rect.Width, rect.Height), new StringFormat
								{
									LineAlignment = StringAlignment.Center,
									Alignment = StringAlignment.Center
								});
							}
						}
						catch (Exception ex)
						{
							graphics.DrawString(base.TabPages[i].Text, font, Brushes.White, new Rectangle(rect.X, rect.Y - 1, rect.Width, rect.Height), new StringFormat
							{
								LineAlignment = StringAlignment.Center,
								Alignment = StringAlignment.Center
							});
						}
					}
					else
					{
						graphics.DrawString(base.TabPages[i].Text, font, Brushes.White, new Rectangle(rect.X, rect.Y - 1, rect.Width, rect.Height), new StringFormat
						{
							LineAlignment = StringAlignment.Center,
							Alignment = StringAlignment.Center
						});
					}
					graphics.DrawLine(new Pen(Color.FromArgb(96, 110, 121)), new Point(rect.Location.X - 1, rect.Location.Y - 1), new Point(rect.Location.X, rect.Location.Y));
					graphics.DrawLine(new Pen(Color.FromArgb(96, 110, 121)), new Point(rect.Location.X - 1, rect.Bottom - 1), new Point(rect.Location.X, rect.Bottom));
				}
				else
				{
					Rectangle rectangle2 = new Rectangle(new Point(base.GetTabRect(i).Location.X - 2, base.GetTabRect(i).Location.Y - 2), new Size(base.GetTabRect(i).Width + 3, base.GetTabRect(i).Height + 1));
					graphics.DrawLine(new Pen(Color.FromArgb(96, 110, 121)), new Point(rectangle2.Right, rectangle2.Top), new Point(rectangle2.Right, rectangle2.Bottom));
					bool flag4 = base.ImageList != null;
					if (flag4)
					{
						try
						{
							bool flag5 = base.ImageList.Images[base.TabPages[i].ImageIndex] != null;
							if (flag5)
							{
								graphics.DrawImage(base.ImageList.Images[base.TabPages[i].ImageIndex], new Point(rectangle2.Location.X + 8, rectangle2.Location.Y + 6));
								graphics.DrawString("      " + base.TabPages[i].Text, this.Font, Brushes.White, new Rectangle(rectangle2.X, rectangle2.Y - 1, rectangle2.Width, rectangle2.Height), new StringFormat
								{
									LineAlignment = StringAlignment.Near,
									Alignment = StringAlignment.Near
								});
							}
							else
							{
								graphics.DrawString(base.TabPages[i].Text, font, new SolidBrush(Color.FromArgb(210, 220, 230)), new Rectangle(rectangle2.X, rectangle2.Y - 1, rectangle2.Width, rectangle2.Height), new StringFormat
								{
									LineAlignment = StringAlignment.Center,
									Alignment = StringAlignment.Center
								});
							}
						}
						catch (Exception ex2)
						{
							graphics.DrawString(base.TabPages[i].Text, font, new SolidBrush(Color.FromArgb(210, 220, 230)), new Rectangle(rectangle2.X, rectangle2.Y - 1, rectangle2.Width, rectangle2.Height), new StringFormat
							{
								LineAlignment = StringAlignment.Center,
								Alignment = StringAlignment.Center
							});
						}
					}
					else
					{
						graphics.DrawString(base.TabPages[i].Text, font, new SolidBrush(Color.FromArgb(210, 220, 230)), new Rectangle(rectangle2.X, rectangle2.Y - 1, rectangle2.Width, rectangle2.Height), new StringFormat
						{
							LineAlignment = StringAlignment.Center,
							Alignment = StringAlignment.Center
						});
					}
				}
				graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, 50, 50)), new Rectangle(86, -1, base.Width - 86, base.Height + 1));
				Color[] colors3 = new Color[]
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
				Draw.InnerGlowRounded(graphics, new Rectangle(86, 0, base.Width - 87, base.Height - 1), 3, colors3);
			}
			graphics.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(50, 50, 50))), new Rectangle(0, 0, base.ItemSize.Height + 4, base.Height - 1));
			graphics.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(20, 20, 20))), new Rectangle(1, 0, base.ItemSize.Height + 3, base.Height - 2));
			e.Graphics.DrawImage((Bitmap)bitmap.Clone(), 0, 0);
			graphics.Dispose();
			bitmap.Dispose();
		}
	}
}
