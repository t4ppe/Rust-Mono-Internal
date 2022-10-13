using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace CS_ClassLibraryTester
{
	// Token: 0x0200005A RID: 90
	internal abstract class ThemeControl154 : Control
	{
		// Token: 0x06000308 RID: 776 RVA: 0x00019D94 File Offset: 0x00017F94
		public ThemeControl154()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.Opaque | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this._ImageSize = Size.Empty;
			this.Font = new Font("Verdana", 8f);
			this.MeasureBitmap = new Bitmap(1, 1);
			this.MeasureGraphics = Graphics.FromImage(this.MeasureBitmap);
			this.DrawRadialPath = new GraphicsPath();
			this.InvalidateCustimization();
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00019E14 File Offset: 0x00018014
		protected sealed override void OnHandleCreated(EventArgs e)
		{
			this.InvalidateCustimization();
			this.ColorHook();
			bool flag = this._LockWidth != 0;
			if (flag)
			{
				base.Width = this._LockWidth;
			}
			bool flag2 = this._LockHeight != 0;
			if (flag2)
			{
				base.Height = this._LockHeight;
			}
			this.Transparent = this._Transparent;
			bool flag3 = this._Transparent && this._BackColor;
			if (flag3)
			{
				this.BackColor = Color.Transparent;
			}
			base.OnHandleCreated(e);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00019E9C File Offset: 0x0001809C
		protected sealed override void OnParentChanged(EventArgs e)
		{
			bool flag = base.Parent != null;
			if (flag)
			{
				this.OnCreation();
				this.DoneCreation = true;
				this.InvalidateTimer();
			}
			base.OnParentChanged(e);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00019ED8 File Offset: 0x000180D8
		private void DoAnimation(bool i)
		{
			this.OnAnimation();
			if (i)
			{
				base.Invalidate();
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00019EFC File Offset: 0x000180FC
		protected sealed override void OnPaint(PaintEventArgs e)
		{
			bool flag = base.Width == 0 || base.Height == 0;
			if (!flag)
			{
				bool transparent = this._Transparent;
				if (transparent)
				{
					this.PaintHook();
					e.Graphics.DrawImage(this.B, 0, 0);
				}
				else
				{
					this.G = e.Graphics;
					this.PaintHook();
				}
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00003A12 File Offset: 0x00001C12
		protected override void OnHandleDestroyed(EventArgs e)
		{
			ThemeShare.RemoveAnimationCallback(new ThemeShare.AnimationDelegate(this.DoAnimation));
			base.OnHandleDestroyed(e);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00019F64 File Offset: 0x00018164
		protected sealed override void OnSizeChanged(EventArgs e)
		{
			bool transparent = this._Transparent;
			if (transparent)
			{
				this.InvalidateBitmap();
			}
			base.Invalidate();
			base.OnSizeChanged(e);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00019F94 File Offset: 0x00018194
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			bool flag = this._LockWidth != 0;
			if (flag)
			{
				width = this._LockWidth;
			}
			bool flag2 = this._LockHeight != 0;
			if (flag2)
			{
				height = this._LockHeight;
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00003A2F File Offset: 0x00001C2F
		protected override void OnMouseEnter(EventArgs e)
		{
			this.InPosition = true;
			this.SetState(MouseState.Over);
			base.OnMouseEnter(e);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00019FDC File Offset: 0x000181DC
		protected override void OnMouseUp(MouseEventArgs e)
		{
			bool inPosition = this.InPosition;
			if (inPosition)
			{
				this.SetState(MouseState.Over);
			}
			base.OnMouseUp(e);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0001A004 File Offset: 0x00018204
		protected override void OnMouseDown(MouseEventArgs e)
		{
			bool flag = e.Button == MouseButtons.Left;
			if (flag)
			{
				this.SetState(MouseState.Down);
			}
			base.OnMouseDown(e);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00003A49 File Offset: 0x00001C49
		protected override void OnMouseLeave(EventArgs e)
		{
			this.InPosition = false;
			this.SetState(MouseState.None);
			base.OnMouseLeave(e);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0001A034 File Offset: 0x00018234
		protected override void OnEnabledChanged(EventArgs e)
		{
			bool enabled = base.Enabled;
			if (enabled)
			{
				this.SetState(MouseState.None);
			}
			else
			{
				this.SetState(MouseState.Block);
			}
			base.OnEnabledChanged(e);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00003A63 File Offset: 0x00001C63
		private void SetState(MouseState current)
		{
			this.State = current;
			base.Invalidate();
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000FF08 File Offset: 0x0000E108
		// (set) Token: 0x06000317 RID: 791 RVA: 0x0000241F File Offset: 0x0000061F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Color ForeColor
		{
			get
			{
				return Color.Empty;
			}
			set
			{
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000FF20 File Offset: 0x0000E120
		// (set) Token: 0x06000319 RID: 793 RVA: 0x0000241F File Offset: 0x0000061F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Image BackgroundImage
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000FF34 File Offset: 0x0000E134
		// (set) Token: 0x0600031B RID: 795 RVA: 0x0000241F File Offset: 0x0000061F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return ImageLayout.None;
			}
			set
			{
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000FED8 File Offset: 0x0000E0D8
		// (set) Token: 0x0600031D RID: 797 RVA: 0x000023FB File Offset: 0x000005FB
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000FEF0 File Offset: 0x0000E0F0
		// (set) Token: 0x0600031F RID: 799 RVA: 0x0000240D File Offset: 0x0000060D
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000FDAC File Offset: 0x0000DFAC
		// (set) Token: 0x06000321 RID: 801 RVA: 0x0001A068 File Offset: 0x00018268
		[Category("Misc")]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				bool flag = !base.IsHandleCreated && value == Color.Transparent;
				if (flag)
				{
					this._BackColor = true;
				}
				else
				{
					base.BackColor = value;
					bool flag2 = base.Parent != null;
					if (flag2)
					{
						this.ColorHook();
					}
				}
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0001A0B8 File Offset: 0x000182B8
		// (set) Token: 0x06000323 RID: 803 RVA: 0x00003A74 File Offset: 0x00001C74
		public bool NoRounding
		{
			get
			{
				return this._NoRounding;
			}
			set
			{
				this._NoRounding = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0001A0D0 File Offset: 0x000182D0
		// (set) Token: 0x06000325 RID: 805 RVA: 0x0001A0E8 File Offset: 0x000182E8
		public Image Image
		{
			get
			{
				return this._Image;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					this._ImageSize = Size.Empty;
				}
				else
				{
					this._ImageSize = value.Size;
				}
				this._Image = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0001A12C File Offset: 0x0001832C
		// (set) Token: 0x06000327 RID: 807 RVA: 0x0001A144 File Offset: 0x00018344
		public bool Transparent
		{
			get
			{
				return this._Transparent;
			}
			set
			{
				this._Transparent = value;
				bool flag = !base.IsHandleCreated;
				if (!flag)
				{
					bool flag2 = !value && this.BackColor.A != byte.MaxValue;
					if (flag2)
					{
						throw new Exception("Unable to change value to false while a transparent BackColor is in use.");
					}
					base.SetStyle(ControlStyles.Opaque, !value);
					base.SetStyle(ControlStyles.SupportsTransparentBackColor, value);
					if (value)
					{
						this.InvalidateBitmap();
					}
					else
					{
						this.B = null;
					}
					base.Invalidate();
				}
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0001A1CC File Offset: 0x000183CC
		// (set) Token: 0x06000329 RID: 809 RVA: 0x0001A234 File Offset: 0x00018434
		public Bloom[] Colors
		{
			get
			{
				List<Bloom> list = new List<Bloom>();
				Dictionary<string, Color>.Enumerator enumerator = this.Items.GetEnumerator();
				while (enumerator.MoveNext())
				{
					List<Bloom> list2 = list;
					KeyValuePair<string, Color> keyValuePair = enumerator.Current;
					string key = keyValuePair.Key;
					keyValuePair = enumerator.Current;
					list2.Add(new Bloom(key, keyValuePair.Value));
				}
				return list.ToArray();
			}
			set
			{
				for (int i = 0; i < value.Length; i++)
				{
					Bloom bloom = value[i];
					bool flag = this.Items.ContainsKey(bloom.Name);
					if (flag)
					{
						this.Items[bloom.Name] = bloom.Value;
					}
				}
				this.InvalidateCustimization();
				this.ColorHook();
				base.Invalidate();
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0001A2A4 File Offset: 0x000184A4
		// (set) Token: 0x0600032B RID: 811 RVA: 0x0001A2BC File Offset: 0x000184BC
		public string Customization
		{
			get
			{
				return this._Customization;
			}
			set
			{
				bool flag = value == this._Customization;
				if (!flag)
				{
					Bloom[] colors = this.Colors;
					try
					{
						byte[] value2 = Convert.FromBase64String(value);
						for (int i = 0; i <= colors.Length - 1; i++)
						{
							colors[i].Value = Color.FromArgb(BitConverter.ToInt32(value2, i * 4));
						}
					}
					catch
					{
						return;
					}
					this._Customization = value;
					this.Colors = colors;
					this.ColorHook();
					base.Invalidate();
				}
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0001A358 File Offset: 0x00018558
		protected Size ImageSize
		{
			get
			{
				return this._ImageSize;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0001A370 File Offset: 0x00018570
		// (set) Token: 0x0600032E RID: 814 RVA: 0x0001A388 File Offset: 0x00018588
		protected int LockWidth
		{
			get
			{
				return this._LockWidth;
			}
			set
			{
				this._LockWidth = value;
				bool flag = this.LockWidth != 0 && base.IsHandleCreated;
				if (flag)
				{
					base.Width = this.LockWidth;
				}
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0001A3C0 File Offset: 0x000185C0
		// (set) Token: 0x06000330 RID: 816 RVA: 0x0001A3D8 File Offset: 0x000185D8
		protected int LockHeight
		{
			get
			{
				return this._LockHeight;
			}
			set
			{
				this._LockHeight = value;
				bool flag = this.LockHeight != 0 && base.IsHandleCreated;
				if (flag)
				{
					base.Height = this.LockHeight;
				}
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0001A410 File Offset: 0x00018610
		// (set) Token: 0x06000332 RID: 818 RVA: 0x00003A85 File Offset: 0x00001C85
		protected bool IsAnimated
		{
			get
			{
				return this._IsAnimated;
			}
			set
			{
				this._IsAnimated = value;
				this.InvalidateTimer();
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0001A428 File Offset: 0x00018628
		protected Pen GetPen(string name)
		{
			return new Pen(this.Items[name]);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0001A44C File Offset: 0x0001864C
		protected Pen GetPen(string name, float width)
		{
			return new Pen(this.Items[name], width);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0001A470 File Offset: 0x00018670
		protected SolidBrush GetBrush(string name)
		{
			return new SolidBrush(this.Items[name]);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0001A494 File Offset: 0x00018694
		protected Color GetColor(string name)
		{
			return this.Items[name];
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0001A4B4 File Offset: 0x000186B4
		protected void SetColor(string name, Color value)
		{
			bool flag = this.Items.ContainsKey(name);
			if (flag)
			{
				this.Items[name] = value;
			}
			else
			{
				this.Items.Add(name, value);
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00003A96 File Offset: 0x00001C96
		protected void SetColor(string name, byte r, byte g, byte b)
		{
			this.SetColor(name, Color.FromArgb((int)r, (int)g, (int)b));
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00003AAA File Offset: 0x00001CAA
		protected void SetColor(string name, byte a, byte r, byte g, byte b)
		{
			this.SetColor(name, Color.FromArgb((int)a, (int)r, (int)g, (int)b));
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00003AC0 File Offset: 0x00001CC0
		protected void SetColor(string name, byte a, Color value)
		{
			this.SetColor(name, Color.FromArgb((int)a, value));
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0001A4F0 File Offset: 0x000186F0
		private void InvalidateBitmap()
		{
			bool flag = base.Width == 0 || base.Height == 0;
			if (!flag)
			{
				this.B = new Bitmap(base.Width, base.Height, PixelFormat.Format32bppPArgb);
				this.G = Graphics.FromImage(this.B);
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0001A548 File Offset: 0x00018748
		private void InvalidateCustimization()
		{
			MemoryStream memoryStream = new MemoryStream(this.Items.Count * 4);
			foreach (Bloom bloom in this.Colors)
			{
				memoryStream.Write(BitConverter.GetBytes(bloom.Value.ToArgb()), 0, 4);
			}
			memoryStream.Close();
			this._Customization = Convert.ToBase64String(memoryStream.ToArray());
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0001A5C0 File Offset: 0x000187C0
		private void InvalidateTimer()
		{
			bool flag = base.DesignMode || !this.DoneCreation;
			if (!flag)
			{
				bool isAnimated = this._IsAnimated;
				if (isAnimated)
				{
					ThemeShare.AddAnimationCallback(new ThemeShare.AnimationDelegate(this.DoAnimation));
				}
				else
				{
					ThemeShare.RemoveAnimationCallback(new ThemeShare.AnimationDelegate(this.DoAnimation));
				}
			}
		}

		// Token: 0x0600033E RID: 830
		protected abstract void ColorHook();

		// Token: 0x0600033F RID: 831
		protected abstract void PaintHook();

		// Token: 0x06000340 RID: 832 RVA: 0x0000241F File Offset: 0x0000061F
		protected virtual void OnCreation()
		{
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000241F File Offset: 0x0000061F
		protected virtual void OnAnimation()
		{
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0001A61C File Offset: 0x0001881C
		protected Rectangle Offset(Rectangle r, int amount)
		{
			this.OffsetReturnRectangle = new Rectangle(r.X + amount, r.Y + amount, r.Width - amount * 2, r.Height - amount * 2);
			return this.OffsetReturnRectangle;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0001A668 File Offset: 0x00018868
		protected Size Offset(Size s, int amount)
		{
			this.OffsetReturnSize = new Size(s.Width + amount, s.Height + amount);
			return this.OffsetReturnSize;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0001A6A0 File Offset: 0x000188A0
		protected Point Offset(Point p, int amount)
		{
			this.OffsetReturnPoint = new Point(p.X + amount, p.Y + amount);
			return this.OffsetReturnPoint;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0001A6D8 File Offset: 0x000188D8
		protected Point Center(Rectangle p, Rectangle c)
		{
			this.CenterReturn = new Point(p.Width / 2 - c.Width / 2 + p.X + c.X, p.Height / 2 - c.Height / 2 + p.Y + c.Y);
			return this.CenterReturn;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0001A744 File Offset: 0x00018944
		protected Point Center(Rectangle p, Size c)
		{
			this.CenterReturn = new Point(p.Width / 2 - c.Width / 2 + p.X, p.Height / 2 - c.Height / 2 + p.Y);
			return this.CenterReturn;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0001A7A0 File Offset: 0x000189A0
		protected Point Center(Rectangle child)
		{
			return this.Center(base.Width, base.Height, child.Width, child.Height);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0001A7D4 File Offset: 0x000189D4
		protected Point Center(Size child)
		{
			return this.Center(base.Width, base.Height, child.Width, child.Height);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0001A808 File Offset: 0x00018A08
		protected Point Center(int childWidth, int childHeight)
		{
			return this.Center(base.Width, base.Height, childWidth, childHeight);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0001A830 File Offset: 0x00018A30
		protected Point Center(Size p, Size c)
		{
			return this.Center(p.Width, p.Height, c.Width, c.Height);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0001A864 File Offset: 0x00018A64
		protected Point Center(int pWidth, int pHeight, int cWidth, int cHeight)
		{
			this.CenterReturn = new Point(pWidth / 2 - cWidth / 2, pHeight / 2 - cHeight / 2);
			return this.CenterReturn;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0001A898 File Offset: 0x00018A98
		protected Size Measure()
		{
			return this.MeasureGraphics.MeasureString(this.Text, this.Font, base.Width).ToSize();
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0001A8D0 File Offset: 0x00018AD0
		protected Size Measure(string text)
		{
			return this.MeasureGraphics.MeasureString(text, this.Font, base.Width).ToSize();
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0001A904 File Offset: 0x00018B04
		protected void DrawPixel(Color c1, int x, int y)
		{
			bool transparent = this._Transparent;
			if (transparent)
			{
				this.B.SetPixel(x, y, c1);
			}
			else
			{
				this.DrawPixelBrush = new SolidBrush(c1);
				this.G.FillRectangle(this.DrawPixelBrush, x, y, 1, 1);
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00003AD2 File Offset: 0x00001CD2
		protected void DrawCorners(Color c1, int offset)
		{
			this.DrawCorners(c1, 0, 0, base.Width, base.Height, offset);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00003AEC File Offset: 0x00001CEC
		protected void DrawCorners(Color c1, Rectangle r1, int offset)
		{
			this.DrawCorners(c1, r1.X, r1.Y, r1.Width, r1.Height, offset);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00003B14 File Offset: 0x00001D14
		protected void DrawCorners(Color c1, int x, int y, int width, int height, int offset)
		{
			this.DrawCorners(c1, x + offset, y + offset, width - offset * 2, height - offset * 2);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00003B35 File Offset: 0x00001D35
		protected void DrawCorners(Color c1)
		{
			this.DrawCorners(c1, 0, 0, base.Width, base.Height);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00003B4E File Offset: 0x00001D4E
		protected void DrawCorners(Color c1, Rectangle r1)
		{
			this.DrawCorners(c1, r1.X, r1.Y, r1.Width, r1.Height);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0001A954 File Offset: 0x00018B54
		protected void DrawCorners(Color c1, int x, int y, int width, int height)
		{
			bool noRounding = this._NoRounding;
			if (!noRounding)
			{
				bool transparent = this._Transparent;
				if (transparent)
				{
					this.B.SetPixel(x, y, c1);
					this.B.SetPixel(x + (width - 1), y, c1);
					this.B.SetPixel(x, y + (height - 1), c1);
					this.B.SetPixel(x + (width - 1), y + (height - 1), c1);
				}
				else
				{
					this.DrawCornersBrush = new SolidBrush(c1);
					this.G.FillRectangle(this.DrawCornersBrush, x, y, 1, 1);
					this.G.FillRectangle(this.DrawCornersBrush, x + (width - 1), y, 1, 1);
					this.G.FillRectangle(this.DrawCornersBrush, x, y + (height - 1), 1, 1);
					this.G.FillRectangle(this.DrawCornersBrush, x + (width - 1), y + (height - 1), 1, 1);
				}
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00003B75 File Offset: 0x00001D75
		protected void DrawBorders(Pen p1, int offset)
		{
			this.DrawBorders(p1, 0, 0, base.Width, base.Height, offset);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00003B8F File Offset: 0x00001D8F
		protected void DrawBorders(Pen p1, Rectangle r, int offset)
		{
			this.DrawBorders(p1, r.X, r.Y, r.Width, r.Height, offset);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00003BB7 File Offset: 0x00001DB7
		protected void DrawBorders(Pen p1, int x, int y, int width, int height, int offset)
		{
			this.DrawBorders(p1, x + offset, y + offset, width - offset * 2, height - offset * 2);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00003BD8 File Offset: 0x00001DD8
		protected void DrawBorders(Pen p1)
		{
			this.DrawBorders(p1, 0, 0, base.Width, base.Height);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00003BF1 File Offset: 0x00001DF1
		protected void DrawBorders(Pen p1, Rectangle r)
		{
			this.DrawBorders(p1, r.X, r.Y, r.Width, r.Height);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00003C18 File Offset: 0x00001E18
		protected void DrawBorders(Pen p1, int x, int y, int width, int height)
		{
			this.G.DrawRectangle(p1, x, y, width - 1, height - 1);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00003C32 File Offset: 0x00001E32
		protected void DrawText(Brush b1, HorizontalAlignment a, int x, int y)
		{
			this.DrawText(b1, this.Text, a, x, y);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0001AA4C File Offset: 0x00018C4C
		protected void DrawText(Brush b1, string text, HorizontalAlignment a, int x, int y)
		{
			bool flag = text.Length == 0;
			if (!flag)
			{
				this.DrawTextSize = this.Measure(text);
				this.DrawTextPoint = this.Center(this.DrawTextSize);
				switch (a)
				{
				case HorizontalAlignment.Left:
					this.G.DrawString(text, this.Font, b1, (float)x, (float)(this.DrawTextPoint.Y + y));
					break;
				case HorizontalAlignment.Right:
					this.G.DrawString(text, this.Font, b1, (float)(base.Width - this.DrawTextSize.Width - x), (float)(this.DrawTextPoint.Y + y));
					break;
				case HorizontalAlignment.Center:
					this.G.DrawString(text, this.Font, b1, (float)(this.DrawTextPoint.X + x), (float)(this.DrawTextPoint.Y + y));
					break;
				}
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0001AB3C File Offset: 0x00018D3C
		protected void DrawText(Brush b1, Point p1)
		{
			bool flag = this.Text.Length == 0;
			if (!flag)
			{
				this.G.DrawString(this.Text, this.Font, b1, p1);
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0001AB80 File Offset: 0x00018D80
		protected void DrawText(Brush b1, int x, int y)
		{
			bool flag = this.Text.Length == 0;
			if (!flag)
			{
				this.G.DrawString(this.Text, this.Font, b1, (float)x, (float)y);
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00003C47 File Offset: 0x00001E47
		protected void DrawImage(HorizontalAlignment a, int x, int y)
		{
			this.DrawImage(this._Image, a, x, y);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0001ABC0 File Offset: 0x00018DC0
		protected void DrawImage(Image image, HorizontalAlignment a, int x, int y)
		{
			bool flag = image == null;
			if (!flag)
			{
				this.DrawImagePoint = this.Center(image.Size);
				switch (a)
				{
				case HorizontalAlignment.Left:
					this.G.DrawImage(image, x, this.DrawImagePoint.Y + y, image.Width, image.Height);
					break;
				case HorizontalAlignment.Right:
					this.G.DrawImage(image, base.Width - image.Width - x, this.DrawImagePoint.Y + y, image.Width, image.Height);
					break;
				case HorizontalAlignment.Center:
					this.G.DrawImage(image, this.DrawImagePoint.X + x, this.DrawImagePoint.Y + y, image.Width, image.Height);
					break;
				}
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00003C5A File Offset: 0x00001E5A
		protected void DrawImage(Point p1)
		{
			this.DrawImage(this._Image, p1.X, p1.Y);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00003C78 File Offset: 0x00001E78
		protected void DrawImage(int x, int y)
		{
			this.DrawImage(this._Image, x, y);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00003C8A File Offset: 0x00001E8A
		protected void DrawImage(Image image, Point p1)
		{
			this.DrawImage(image, p1.X, p1.Y);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0001ACA0 File Offset: 0x00018EA0
		protected void DrawImage(Image image, int x, int y)
		{
			bool flag = image == null;
			if (!flag)
			{
				this.G.DrawImage(image, x, y, image.Width, image.Height);
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00003CA3 File Offset: 0x00001EA3
		protected void DrawGradient(ColorBlend blend, int x, int y, int width, int height)
		{
			this.DrawGradientRectangle = new Rectangle(x, y, width, height);
			this.DrawGradient(blend, this.DrawGradientRectangle);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00003CC5 File Offset: 0x00001EC5
		protected void DrawGradient(ColorBlend blend, int x, int y, int width, int height, float angle)
		{
			this.DrawGradientRectangle = new Rectangle(x, y, width, height);
			this.DrawGradient(blend, this.DrawGradientRectangle, angle);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00003CE9 File Offset: 0x00001EE9
		protected void DrawGradient(ColorBlend blend, Rectangle r)
		{
			this.DrawGradientBrush = new LinearGradientBrush(r, Color.Empty, Color.Empty, 90f);
			this.DrawGradientBrush.InterpolationColors = blend;
			this.G.FillRectangle(this.DrawGradientBrush, r);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00003D27 File Offset: 0x00001F27
		protected void DrawGradient(ColorBlend blend, Rectangle r, float angle)
		{
			this.DrawGradientBrush = new LinearGradientBrush(r, Color.Empty, Color.Empty, angle);
			this.DrawGradientBrush.InterpolationColors = blend;
			this.G.FillRectangle(this.DrawGradientBrush, r);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00003D61 File Offset: 0x00001F61
		protected void DrawGradient(Color c1, Color c2, int x, int y, int width, int height)
		{
			this.DrawGradientRectangle = new Rectangle(x, y, width, height);
			this.DrawGradient(c1, c2, this.DrawGradientRectangle);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00003D85 File Offset: 0x00001F85
		protected void DrawGradient(Color c1, Color c2, int x, int y, int width, int height, float angle)
		{
			this.DrawGradientRectangle = new Rectangle(x, y, width, height);
			this.DrawGradient(c1, c2, this.DrawGradientRectangle, angle);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00003DAB File Offset: 0x00001FAB
		protected void DrawGradient(Color c1, Color c2, Rectangle r)
		{
			this.DrawGradientBrush = new LinearGradientBrush(r, c1, c2, 90f);
			this.G.FillRectangle(this.DrawGradientBrush, r);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00003DD4 File Offset: 0x00001FD4
		protected void DrawGradient(Color c1, Color c2, Rectangle r, float angle)
		{
			this.DrawGradientBrush = new LinearGradientBrush(r, c1, c2, angle);
			this.G.FillRectangle(this.DrawGradientBrush, r);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00003DFA File Offset: 0x00001FFA
		public void DrawRadial(ColorBlend blend, int x, int y, int width, int height)
		{
			this.DrawRadialRectangle = new Rectangle(x, y, width, height);
			this.DrawRadial(blend, this.DrawRadialRectangle, width / 2, height / 2);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00003E24 File Offset: 0x00002024
		public void DrawRadial(ColorBlend blend, int x, int y, int width, int height, Point center)
		{
			this.DrawRadialRectangle = new Rectangle(x, y, width, height);
			this.DrawRadial(blend, this.DrawRadialRectangle, center.X, center.Y);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00003E54 File Offset: 0x00002054
		public void DrawRadial(ColorBlend blend, int x, int y, int width, int height, int cx, int cy)
		{
			this.DrawRadialRectangle = new Rectangle(x, y, width, height);
			this.DrawRadial(blend, this.DrawRadialRectangle, cx, cy);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00003E7A File Offset: 0x0000207A
		public void DrawRadial(ColorBlend blend, Rectangle r)
		{
			this.DrawRadial(blend, r, r.Width / 2, r.Height / 2);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00003E98 File Offset: 0x00002098
		public void DrawRadial(ColorBlend blend, Rectangle r, Point center)
		{
			this.DrawRadial(blend, r, center.X, center.Y);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0001ACD4 File Offset: 0x00018ED4
		public void DrawRadial(ColorBlend blend, Rectangle r, int cx, int cy)
		{
			this.DrawRadialPath.Reset();
			this.DrawRadialPath.AddEllipse(r.X, r.Y, r.Width - 1, r.Height - 1);
			this.DrawRadialBrush1 = new PathGradientBrush(this.DrawRadialPath);
			this.DrawRadialBrush1.CenterPoint = new Point(r.X + cx, r.Y + cy);
			this.DrawRadialBrush1.InterpolationColors = blend;
			bool flag = this.G.SmoothingMode == SmoothingMode.AntiAlias;
			if (flag)
			{
				this.G.FillEllipse(this.DrawRadialBrush1, r.X + 1, r.Y + 1, r.Width - 3, r.Height - 3);
			}
			else
			{
				this.G.FillEllipse(this.DrawRadialBrush1, r);
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00003EB2 File Offset: 0x000020B2
		protected void DrawRadial(Color c1, Color c2, int x, int y, int width, int height)
		{
			this.DrawRadialRectangle = new Rectangle(x, y, width, height);
			this.DrawRadial(c1, c2, this.DrawRadialRectangle);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00003ED6 File Offset: 0x000020D6
		protected void DrawRadial(Color c1, Color c2, int x, int y, int width, int height, float angle)
		{
			this.DrawRadialRectangle = new Rectangle(x, y, width, height);
			this.DrawRadial(c1, c2, this.DrawRadialRectangle, angle);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00003EFC File Offset: 0x000020FC
		protected void DrawRadial(Color c1, Color c2, Rectangle r)
		{
			this.DrawRadialBrush2 = new LinearGradientBrush(r, c1, c2, 90f);
			this.G.FillEllipse(this.DrawRadialBrush2, r);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00003F25 File Offset: 0x00002125
		protected void DrawRadial(Color c1, Color c2, Rectangle r, float angle)
		{
			this.DrawRadialBrush2 = new LinearGradientBrush(r, c1, c2, angle);
			this.G.FillEllipse(this.DrawRadialBrush2, r);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0001ADC4 File Offset: 0x00018FC4
		public GraphicsPath CreateRound(int x, int y, int width, int height, int slope)
		{
			this.CreateRoundRectangle = new Rectangle(x, y, width, height);
			return this.CreateRound(this.CreateRoundRectangle, slope);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0001ADF4 File Offset: 0x00018FF4
		public GraphicsPath CreateRound(Rectangle r, int slope)
		{
			this.CreateRoundPath = new GraphicsPath(FillMode.Winding);
			this.CreateRoundPath.AddArc(r.X, r.Y, slope, slope, 180f, 90f);
			this.CreateRoundPath.AddArc(r.Right - slope, r.Y, slope, slope, 270f, 90f);
			this.CreateRoundPath.AddArc(r.Right - slope, r.Bottom - slope, slope, slope, 0f, 90f);
			this.CreateRoundPath.AddArc(r.X, r.Bottom - slope, slope, slope, 90f, 90f);
			this.CreateRoundPath.CloseFigure();
			return this.CreateRoundPath;
		}

		// Token: 0x040002C3 RID: 707
		protected Graphics G;

		// Token: 0x040002C4 RID: 708
		protected Bitmap B;

		// Token: 0x040002C5 RID: 709
		private bool DoneCreation;

		// Token: 0x040002C6 RID: 710
		private bool InPosition;

		// Token: 0x040002C7 RID: 711
		protected MouseState State;

		// Token: 0x040002C8 RID: 712
		private bool _BackColor;

		// Token: 0x040002C9 RID: 713
		private bool _NoRounding;

		// Token: 0x040002CA RID: 714
		private Image _Image;

		// Token: 0x040002CB RID: 715
		private bool _Transparent;

		// Token: 0x040002CC RID: 716
		private Dictionary<string, Color> Items = new Dictionary<string, Color>();

		// Token: 0x040002CD RID: 717
		private string _Customization;

		// Token: 0x040002CE RID: 718
		private Size _ImageSize;

		// Token: 0x040002CF RID: 719
		private int _LockWidth;

		// Token: 0x040002D0 RID: 720
		private int _LockHeight;

		// Token: 0x040002D1 RID: 721
		private bool _IsAnimated;

		// Token: 0x040002D2 RID: 722
		private Rectangle OffsetReturnRectangle;

		// Token: 0x040002D3 RID: 723
		private Size OffsetReturnSize;

		// Token: 0x040002D4 RID: 724
		private Point OffsetReturnPoint;

		// Token: 0x040002D5 RID: 725
		private Point CenterReturn;

		// Token: 0x040002D6 RID: 726
		private Bitmap MeasureBitmap;

		// Token: 0x040002D7 RID: 727
		private Graphics MeasureGraphics;

		// Token: 0x040002D8 RID: 728
		private SolidBrush DrawPixelBrush;

		// Token: 0x040002D9 RID: 729
		private SolidBrush DrawCornersBrush;

		// Token: 0x040002DA RID: 730
		private Point DrawTextPoint;

		// Token: 0x040002DB RID: 731
		private Size DrawTextSize;

		// Token: 0x040002DC RID: 732
		private Point DrawImagePoint;

		// Token: 0x040002DD RID: 733
		private LinearGradientBrush DrawGradientBrush;

		// Token: 0x040002DE RID: 734
		private Rectangle DrawGradientRectangle;

		// Token: 0x040002DF RID: 735
		private GraphicsPath DrawRadialPath;

		// Token: 0x040002E0 RID: 736
		private PathGradientBrush DrawRadialBrush1;

		// Token: 0x040002E1 RID: 737
		private LinearGradientBrush DrawRadialBrush2;

		// Token: 0x040002E2 RID: 738
		private Rectangle DrawRadialRectangle;

		// Token: 0x040002E3 RID: 739
		private GraphicsPath CreateRoundPath;

		// Token: 0x040002E4 RID: 740
		private Rectangle CreateRoundRectangle;
	}
}
