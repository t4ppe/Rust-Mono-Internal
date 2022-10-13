using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

// Token: 0x0200003F RID: 63
internal class VitalityTabControl : TabControl
{
	// Token: 0x060001E0 RID: 480 RVA: 0x00002FD0 File Offset: 0x000011D0
	public VitalityTabControl()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
		this.DoubleBuffered = true;
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x00002FEF File Offset: 0x000011EF
	protected override void CreateHandle()
	{
		base.CreateHandle();
		base.Alignment = TabAlignment.Top;
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x000131F0 File Offset: 0x000113F0
	protected override void OnPaint(PaintEventArgs e)
	{
		Bitmap bitmap = new Bitmap(base.Width, base.Height);
		Graphics graphics = Graphics.FromImage(bitmap);
		try
		{
			base.SelectedTab.BackColor = Color.FromArgb(240, 240, 240);
		}
		catch
		{
		}
		graphics.Clear(base.Parent.BackColor);
		for (int i = 0; i <= base.TabCount - 1; i++)
		{
			bool flag = i != base.SelectedIndex;
			if (flag)
			{
				Rectangle rectangle = new Rectangle(base.GetTabRect(i).X, base.GetTabRect(i).Y + 3, base.GetTabRect(i).Width + 2, base.GetTabRect(i).Height);
				LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Point(rectangle.X, rectangle.Y), new Point(rectangle.X, rectangle.Y + rectangle.Height), Color.White, Color.LightGray);
				graphics.FillRectangle(linearGradientBrush, rectangle);
				linearGradientBrush.Dispose();
				graphics.DrawRectangle(Pens.LightGray, rectangle);
				graphics.DrawRectangle(Pens.LightGray, new Rectangle(rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 2, rectangle.Height));
				graphics.DrawString(base.TabPages[i].Text, this.Font, Brushes.Gray, rectangle, new StringFormat
				{
					LineAlignment = StringAlignment.Near,
					Alignment = StringAlignment.Center
				});
			}
		}
		graphics.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), 0, base.ItemSize.Height, base.Width, base.Height);
		graphics.DrawRectangle(Pens.LightGray, 0, base.ItemSize.Height, base.Width - 1, base.Height - base.ItemSize.Height - 1);
		graphics.DrawRectangle(Pens.LightGray, 1, base.ItemSize.Height + 1, base.Width - 3, base.Height - base.ItemSize.Height - 3);
		bool flag2 = base.SelectedIndex != -1;
		if (flag2)
		{
			Rectangle r = new Rectangle(base.GetTabRect(base.SelectedIndex).X - 2, base.GetTabRect(base.SelectedIndex).Y, base.GetTabRect(base.SelectedIndex).Width + 3, base.GetTabRect(base.SelectedIndex).Height);
			LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(new Rectangle(r.X + 2, r.Y + 2, r.Width - 2, r.Height), Color.White, Color.LightGray, 90f);
			graphics.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), new Rectangle(r.X + 2, r.Y + 2, r.Width - 2, r.Height));
			graphics.DrawLine(Pens.LightGray, new Point(r.X, r.Y + r.Height - 2), new Point(r.X, r.Y));
			graphics.DrawLine(Pens.LightGray, new Point(r.X, r.Y), new Point(r.X + r.Width, r.Y));
			graphics.DrawLine(Pens.LightGray, new Point(r.X + r.Width, r.Y), new Point(r.X + r.Width, r.Y + r.Height - 2));
			graphics.DrawLine(Pens.LightGray, new Point(r.X + 1, r.Y + r.Height - 1), new Point(r.X + 1, r.Y + 1));
			graphics.DrawLine(Pens.LightGray, new Point(r.X + 1, r.Y + 1), new Point(r.X + r.Width - 1, r.Y + 1));
			graphics.DrawLine(Pens.LightGray, new Point(r.X + r.Width - 1, r.Y + 1), new Point(r.X + r.Width - 1, r.Y + r.Height - 1));
			graphics.DrawString(base.TabPages[base.SelectedIndex].Text, this.Font, Brushes.Gray, r, new StringFormat
			{
				LineAlignment = StringAlignment.Center,
				Alignment = StringAlignment.Center
			});
		}
		graphics.DrawLine(new Pen(Color.FromArgb(240, 240, 240)), new Point(0, 1), new Point(0, 2));
		e.Graphics.DrawImage((Bitmap)bitmap.Clone(), 0, 0);
		graphics.Dispose();
		bitmap.Dispose();
	}
}
