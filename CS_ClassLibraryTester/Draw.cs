using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CS_ClassLibraryTester
{
	// Token: 0x0200004A RID: 74
	internal static class Draw
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x0001414C File Offset: 0x0001234C
		public static GraphicsPath RoundRect(Rectangle Rectangle, int Curve)
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

		// Token: 0x060001F4 RID: 500 RVA: 0x00014264 File Offset: 0x00012464
		public static GraphicsPath RoundRect(int X, int Y, int Width, int Height, int Curve)
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

		// Token: 0x060001F5 RID: 501 RVA: 0x00014388 File Offset: 0x00012588
		public static void InnerGlow(Graphics G, Rectangle Rectangle, Color[] Colors)
		{
			int num = 1;
			int num2 = 0;
			foreach (Color color in Colors)
			{
				G.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb((int)color.R, (int)color.B, (int)color.G))), Rectangle.X + num2, Rectangle.Y + num2, Rectangle.Width - num, Rectangle.Height - num);
				num += 2;
				num2++;
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00014410 File Offset: 0x00012610
		public static void InnerGlowRounded(Graphics G, Rectangle Rectangle, int Degree, Color[] Colors)
		{
			int num = 1;
			int num2 = 0;
			foreach (Color color in Colors)
			{
				G.DrawPath(new Pen(new SolidBrush(Color.FromArgb((int)color.R, (int)color.B, (int)color.G))), Draw.RoundRect(Rectangle.X + num2, Rectangle.Y + num2, Rectangle.Width - num, Rectangle.Height - num, Degree));
				num += 2;
				num2++;
			}
		}
	}
}
