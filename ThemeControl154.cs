using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

// Token: 0x02000031 RID: 49
internal abstract class ThemeControl154 : Control
{
	// Token: 0x06000128 RID: 296 RVA: 0x00011118 File Offset: 0x0000F318
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

	// Token: 0x06000129 RID: 297 RVA: 0x00011198 File Offset: 0x0000F398
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

	// Token: 0x0600012A RID: 298 RVA: 0x00011220 File Offset: 0x0000F420
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

	// Token: 0x0600012B RID: 299 RVA: 0x0001125C File Offset: 0x0000F45C
	private void DoAnimation(bool i)
	{
		this.OnAnimation();
		if (i)
		{
			base.Invalidate();
		}
	}

	// Token: 0x0600012C RID: 300 RVA: 0x00011280 File Offset: 0x0000F480
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

	// Token: 0x0600012D RID: 301 RVA: 0x00002917 File Offset: 0x00000B17
	protected override void OnHandleDestroyed(EventArgs e)
	{
		ThemeShare.RemoveAnimationCallback(new ThemeShare.AnimationDelegate(this.DoAnimation));
		base.OnHandleDestroyed(e);
	}

	// Token: 0x0600012E RID: 302 RVA: 0x000112E8 File Offset: 0x0000F4E8
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

	// Token: 0x0600012F RID: 303 RVA: 0x00011318 File Offset: 0x0000F518
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

	// Token: 0x06000130 RID: 304 RVA: 0x00002934 File Offset: 0x00000B34
	protected override void OnMouseEnter(EventArgs e)
	{
		this.InPosition = true;
		this.SetState(MouseState.Over);
		base.OnMouseEnter(e);
	}

	// Token: 0x06000131 RID: 305 RVA: 0x00011360 File Offset: 0x0000F560
	protected override void OnMouseUp(MouseEventArgs e)
	{
		bool inPosition = this.InPosition;
		if (inPosition)
		{
			this.SetState(MouseState.Over);
		}
		base.OnMouseUp(e);
	}

	// Token: 0x06000132 RID: 306 RVA: 0x00011388 File Offset: 0x0000F588
	protected override void OnMouseDown(MouseEventArgs e)
	{
		bool flag = e.Button == MouseButtons.Left;
		if (flag)
		{
			this.SetState(MouseState.Down);
		}
		base.OnMouseDown(e);
	}

	// Token: 0x06000133 RID: 307 RVA: 0x0000294E File Offset: 0x00000B4E
	protected override void OnMouseLeave(EventArgs e)
	{
		this.InPosition = false;
		this.SetState(MouseState.None);
		base.OnMouseLeave(e);
	}

	// Token: 0x06000134 RID: 308 RVA: 0x000113B8 File Offset: 0x0000F5B8
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

	// Token: 0x06000135 RID: 309 RVA: 0x00002968 File Offset: 0x00000B68
	private void SetState(MouseState current)
	{
		this.State = current;
		base.Invalidate();
	}

	// Token: 0x17000035 RID: 53
	// (get) Token: 0x06000136 RID: 310 RVA: 0x0000FF08 File Offset: 0x0000E108
	// (set) Token: 0x06000137 RID: 311 RVA: 0x0000241F File Offset: 0x0000061F
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

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x06000138 RID: 312 RVA: 0x0000FF20 File Offset: 0x0000E120
	// (set) Token: 0x06000139 RID: 313 RVA: 0x0000241F File Offset: 0x0000061F
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

	// Token: 0x17000037 RID: 55
	// (get) Token: 0x0600013A RID: 314 RVA: 0x0000FF34 File Offset: 0x0000E134
	// (set) Token: 0x0600013B RID: 315 RVA: 0x0000241F File Offset: 0x0000061F
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

	// Token: 0x17000038 RID: 56
	// (get) Token: 0x0600013C RID: 316 RVA: 0x0000FED8 File Offset: 0x0000E0D8
	// (set) Token: 0x0600013D RID: 317 RVA: 0x000023FB File Offset: 0x000005FB
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

	// Token: 0x17000039 RID: 57
	// (get) Token: 0x0600013E RID: 318 RVA: 0x0000FEF0 File Offset: 0x0000E0F0
	// (set) Token: 0x0600013F RID: 319 RVA: 0x0000240D File Offset: 0x0000060D
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

	// Token: 0x1700003A RID: 58
	// (get) Token: 0x06000140 RID: 320 RVA: 0x0000FDAC File Offset: 0x0000DFAC
	// (set) Token: 0x06000141 RID: 321 RVA: 0x000113EC File Offset: 0x0000F5EC
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

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x06000142 RID: 322 RVA: 0x0001143C File Offset: 0x0000F63C
	// (set) Token: 0x06000143 RID: 323 RVA: 0x00002979 File Offset: 0x00000B79
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

	// Token: 0x1700003C RID: 60
	// (get) Token: 0x06000144 RID: 324 RVA: 0x00011454 File Offset: 0x0000F654
	// (set) Token: 0x06000145 RID: 325 RVA: 0x0001146C File Offset: 0x0000F66C
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

	// Token: 0x1700003D RID: 61
	// (get) Token: 0x06000146 RID: 326 RVA: 0x000114B0 File Offset: 0x0000F6B0
	// (set) Token: 0x06000147 RID: 327 RVA: 0x000114C8 File Offset: 0x0000F6C8
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

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x06000148 RID: 328 RVA: 0x00011550 File Offset: 0x0000F750
	// (set) Token: 0x06000149 RID: 329 RVA: 0x000115B8 File Offset: 0x0000F7B8
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

	// Token: 0x1700003F RID: 63
	// (get) Token: 0x0600014A RID: 330 RVA: 0x00011628 File Offset: 0x0000F828
	// (set) Token: 0x0600014B RID: 331 RVA: 0x00011640 File Offset: 0x0000F840
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

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x0600014C RID: 332 RVA: 0x000116DC File Offset: 0x0000F8DC
	protected Size ImageSize
	{
		get
		{
			return this._ImageSize;
		}
	}

	// Token: 0x17000041 RID: 65
	// (get) Token: 0x0600014D RID: 333 RVA: 0x000116F4 File Offset: 0x0000F8F4
	// (set) Token: 0x0600014E RID: 334 RVA: 0x0001170C File Offset: 0x0000F90C
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

	// Token: 0x17000042 RID: 66
	// (get) Token: 0x0600014F RID: 335 RVA: 0x00011744 File Offset: 0x0000F944
	// (set) Token: 0x06000150 RID: 336 RVA: 0x0001175C File Offset: 0x0000F95C
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

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x06000151 RID: 337 RVA: 0x00011794 File Offset: 0x0000F994
	// (set) Token: 0x06000152 RID: 338 RVA: 0x0000298A File Offset: 0x00000B8A
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

	// Token: 0x06000153 RID: 339 RVA: 0x000117AC File Offset: 0x0000F9AC
	protected Pen GetPen(string name)
	{
		return new Pen(this.Items[name]);
	}

	// Token: 0x06000154 RID: 340 RVA: 0x000117D0 File Offset: 0x0000F9D0
	protected Pen GetPen(string name, float width)
	{
		return new Pen(this.Items[name], width);
	}

	// Token: 0x06000155 RID: 341 RVA: 0x000117F4 File Offset: 0x0000F9F4
	protected SolidBrush GetBrush(string name)
	{
		return new SolidBrush(this.Items[name]);
	}

	// Token: 0x06000156 RID: 342 RVA: 0x00011818 File Offset: 0x0000FA18
	protected Color GetColor(string name)
	{
		return this.Items[name];
	}

	// Token: 0x06000157 RID: 343 RVA: 0x00011838 File Offset: 0x0000FA38
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

	// Token: 0x06000158 RID: 344 RVA: 0x0000299B File Offset: 0x00000B9B
	protected void SetColor(string name, byte r, byte g, byte b)
	{
		this.SetColor(name, Color.FromArgb((int)r, (int)g, (int)b));
	}

	// Token: 0x06000159 RID: 345 RVA: 0x000029AF File Offset: 0x00000BAF
	protected void SetColor(string name, byte a, byte r, byte g, byte b)
	{
		this.SetColor(name, Color.FromArgb((int)a, (int)r, (int)g, (int)b));
	}

	// Token: 0x0600015A RID: 346 RVA: 0x000029C5 File Offset: 0x00000BC5
	protected void SetColor(string name, byte a, Color value)
	{
		this.SetColor(name, Color.FromArgb((int)a, value));
	}

	// Token: 0x0600015B RID: 347 RVA: 0x00011874 File Offset: 0x0000FA74
	private void InvalidateBitmap()
	{
		bool flag = base.Width == 0 || base.Height == 0;
		if (!flag)
		{
			this.B = new Bitmap(base.Width, base.Height, PixelFormat.Format32bppPArgb);
			this.G = Graphics.FromImage(this.B);
		}
	}

	// Token: 0x0600015C RID: 348 RVA: 0x000118CC File Offset: 0x0000FACC
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

	// Token: 0x0600015D RID: 349 RVA: 0x00011944 File Offset: 0x0000FB44
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

	// Token: 0x0600015E RID: 350
	protected abstract void ColorHook();

	// Token: 0x0600015F RID: 351
	protected abstract void PaintHook();

	// Token: 0x06000160 RID: 352 RVA: 0x0000241F File Offset: 0x0000061F
	protected virtual void OnCreation()
	{
	}

	// Token: 0x06000161 RID: 353 RVA: 0x0000241F File Offset: 0x0000061F
	protected virtual void OnAnimation()
	{
	}

	// Token: 0x06000162 RID: 354 RVA: 0x000119A0 File Offset: 0x0000FBA0
	protected Rectangle Offset(Rectangle r, int amount)
	{
		this.OffsetReturnRectangle = new Rectangle(r.X + amount, r.Y + amount, r.Width - amount * 2, r.Height - amount * 2);
		return this.OffsetReturnRectangle;
	}

	// Token: 0x06000163 RID: 355 RVA: 0x000119EC File Offset: 0x0000FBEC
	protected Size Offset(Size s, int amount)
	{
		this.OffsetReturnSize = new Size(s.Width + amount, s.Height + amount);
		return this.OffsetReturnSize;
	}

	// Token: 0x06000164 RID: 356 RVA: 0x00011A24 File Offset: 0x0000FC24
	protected Point Offset(Point p, int amount)
	{
		this.OffsetReturnPoint = new Point(p.X + amount, p.Y + amount);
		return this.OffsetReturnPoint;
	}

	// Token: 0x06000165 RID: 357 RVA: 0x00011A5C File Offset: 0x0000FC5C
	protected Point Center(Rectangle p, Rectangle c)
	{
		this.CenterReturn = new Point(p.Width / 2 - c.Width / 2 + p.X + c.X, p.Height / 2 - c.Height / 2 + p.Y + c.Y);
		return this.CenterReturn;
	}

	// Token: 0x06000166 RID: 358 RVA: 0x00011AC8 File Offset: 0x0000FCC8
	protected Point Center(Rectangle p, Size c)
	{
		this.CenterReturn = new Point(p.Width / 2 - c.Width / 2 + p.X, p.Height / 2 - c.Height / 2 + p.Y);
		return this.CenterReturn;
	}

	// Token: 0x06000167 RID: 359 RVA: 0x00011B24 File Offset: 0x0000FD24
	protected Point Center(Rectangle child)
	{
		return this.Center(base.Width, base.Height, child.Width, child.Height);
	}

	// Token: 0x06000168 RID: 360 RVA: 0x00011B58 File Offset: 0x0000FD58
	protected Point Center(Size child)
	{
		return this.Center(base.Width, base.Height, child.Width, child.Height);
	}

	// Token: 0x06000169 RID: 361 RVA: 0x00011B8C File Offset: 0x0000FD8C
	protected Point Center(int childWidth, int childHeight)
	{
		return this.Center(base.Width, base.Height, childWidth, childHeight);
	}

	// Token: 0x0600016A RID: 362 RVA: 0x00011BB4 File Offset: 0x0000FDB4
	protected Point Center(Size p, Size c)
	{
		return this.Center(p.Width, p.Height, c.Width, c.Height);
	}

	// Token: 0x0600016B RID: 363 RVA: 0x00011BE8 File Offset: 0x0000FDE8
	protected Point Center(int pWidth, int pHeight, int cWidth, int cHeight)
	{
		this.CenterReturn = new Point(pWidth / 2 - cWidth / 2, pHeight / 2 - cHeight / 2);
		return this.CenterReturn;
	}

	// Token: 0x0600016C RID: 364 RVA: 0x00011C1C File Offset: 0x0000FE1C
	protected Size Measure()
	{
		return this.MeasureGraphics.MeasureString(this.Text, this.Font, base.Width).ToSize();
	}

	// Token: 0x0600016D RID: 365 RVA: 0x00011C54 File Offset: 0x0000FE54
	protected Size Measure(string text)
	{
		return this.MeasureGraphics.MeasureString(text, this.Font, base.Width).ToSize();
	}

	// Token: 0x0600016E RID: 366 RVA: 0x00011C88 File Offset: 0x0000FE88
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

	// Token: 0x0600016F RID: 367 RVA: 0x000029D7 File Offset: 0x00000BD7
	protected void DrawCorners(Color c1, int offset)
	{
		this.DrawCorners(c1, 0, 0, base.Width, base.Height, offset);
	}

	// Token: 0x06000170 RID: 368 RVA: 0x000029F1 File Offset: 0x00000BF1
	protected void DrawCorners(Color c1, Rectangle r1, int offset)
	{
		this.DrawCorners(c1, r1.X, r1.Y, r1.Width, r1.Height, offset);
	}

	// Token: 0x06000171 RID: 369 RVA: 0x00002A19 File Offset: 0x00000C19
	protected void DrawCorners(Color c1, int x, int y, int width, int height, int offset)
	{
		this.DrawCorners(c1, x + offset, y + offset, width - offset * 2, height - offset * 2);
	}

	// Token: 0x06000172 RID: 370 RVA: 0x00002A3A File Offset: 0x00000C3A
	protected void DrawCorners(Color c1)
	{
		this.DrawCorners(c1, 0, 0, base.Width, base.Height);
	}

	// Token: 0x06000173 RID: 371 RVA: 0x00002A53 File Offset: 0x00000C53
	protected void DrawCorners(Color c1, Rectangle r1)
	{
		this.DrawCorners(c1, r1.X, r1.Y, r1.Width, r1.Height);
	}

	// Token: 0x06000174 RID: 372 RVA: 0x00011CD8 File Offset: 0x0000FED8
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

	// Token: 0x06000175 RID: 373 RVA: 0x00002A7A File Offset: 0x00000C7A
	protected void DrawBorders(Pen p1, int offset)
	{
		this.DrawBorders(p1, 0, 0, base.Width, base.Height, offset);
	}

	// Token: 0x06000176 RID: 374 RVA: 0x00002A94 File Offset: 0x00000C94
	protected void DrawBorders(Pen p1, Rectangle r, int offset)
	{
		this.DrawBorders(p1, r.X, r.Y, r.Width, r.Height, offset);
	}

	// Token: 0x06000177 RID: 375 RVA: 0x00002ABC File Offset: 0x00000CBC
	protected void DrawBorders(Pen p1, int x, int y, int width, int height, int offset)
	{
		this.DrawBorders(p1, x + offset, y + offset, width - offset * 2, height - offset * 2);
	}

	// Token: 0x06000178 RID: 376 RVA: 0x00002ADD File Offset: 0x00000CDD
	protected void DrawBorders(Pen p1)
	{
		this.DrawBorders(p1, 0, 0, base.Width, base.Height);
	}

	// Token: 0x06000179 RID: 377 RVA: 0x00002AF6 File Offset: 0x00000CF6
	protected void DrawBorders(Pen p1, Rectangle r)
	{
		this.DrawBorders(p1, r.X, r.Y, r.Width, r.Height);
	}

	// Token: 0x0600017A RID: 378 RVA: 0x00002B1D File Offset: 0x00000D1D
	protected void DrawBorders(Pen p1, int x, int y, int width, int height)
	{
		this.G.DrawRectangle(p1, x, y, width - 1, height - 1);
	}

	// Token: 0x0600017B RID: 379 RVA: 0x00002B37 File Offset: 0x00000D37
	protected void DrawText(Brush b1, HorizontalAlignment a, int x, int y)
	{
		this.DrawText(b1, this.Text, a, x, y);
	}

	// Token: 0x0600017C RID: 380 RVA: 0x00011DD0 File Offset: 0x0000FFD0
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

	// Token: 0x0600017D RID: 381 RVA: 0x00011EC0 File Offset: 0x000100C0
	protected void DrawText(Brush b1, Point p1)
	{
		bool flag = this.Text.Length == 0;
		if (!flag)
		{
			this.G.DrawString(this.Text, this.Font, b1, p1);
		}
	}

	// Token: 0x0600017E RID: 382 RVA: 0x00011F04 File Offset: 0x00010104
	protected void DrawText(Brush b1, int x, int y)
	{
		bool flag = this.Text.Length == 0;
		if (!flag)
		{
			this.G.DrawString(this.Text, this.Font, b1, (float)x, (float)y);
		}
	}

	// Token: 0x0600017F RID: 383 RVA: 0x00002B4C File Offset: 0x00000D4C
	protected void DrawImage(HorizontalAlignment a, int x, int y)
	{
		this.DrawImage(this._Image, a, x, y);
	}

	// Token: 0x06000180 RID: 384 RVA: 0x00011F44 File Offset: 0x00010144
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

	// Token: 0x06000181 RID: 385 RVA: 0x00002B5F File Offset: 0x00000D5F
	protected void DrawImage(Point p1)
	{
		this.DrawImage(this._Image, p1.X, p1.Y);
	}

	// Token: 0x06000182 RID: 386 RVA: 0x00002B7D File Offset: 0x00000D7D
	protected void DrawImage(int x, int y)
	{
		this.DrawImage(this._Image, x, y);
	}

	// Token: 0x06000183 RID: 387 RVA: 0x00002B8F File Offset: 0x00000D8F
	protected void DrawImage(Image image, Point p1)
	{
		this.DrawImage(image, p1.X, p1.Y);
	}

	// Token: 0x06000184 RID: 388 RVA: 0x00012024 File Offset: 0x00010224
	protected void DrawImage(Image image, int x, int y)
	{
		bool flag = image == null;
		if (!flag)
		{
			this.G.DrawImage(image, x, y, image.Width, image.Height);
		}
	}

	// Token: 0x06000185 RID: 389 RVA: 0x00002BA8 File Offset: 0x00000DA8
	protected void DrawGradient(ColorBlend blend, int x, int y, int width, int height)
	{
		this.DrawGradientRectangle = new Rectangle(x, y, width, height);
		this.DrawGradient(blend, this.DrawGradientRectangle);
	}

	// Token: 0x06000186 RID: 390 RVA: 0x00002BCA File Offset: 0x00000DCA
	protected void DrawGradient(ColorBlend blend, int x, int y, int width, int height, float angle)
	{
		this.DrawGradientRectangle = new Rectangle(x, y, width, height);
		this.DrawGradient(blend, this.DrawGradientRectangle, angle);
	}

	// Token: 0x06000187 RID: 391 RVA: 0x00002BEE File Offset: 0x00000DEE
	protected void DrawGradient(ColorBlend blend, Rectangle r)
	{
		this.DrawGradientBrush = new LinearGradientBrush(r, Color.Empty, Color.Empty, 90f);
		this.DrawGradientBrush.InterpolationColors = blend;
		this.G.FillRectangle(this.DrawGradientBrush, r);
	}

	// Token: 0x06000188 RID: 392 RVA: 0x00002C2C File Offset: 0x00000E2C
	protected void DrawGradient(ColorBlend blend, Rectangle r, float angle)
	{
		this.DrawGradientBrush = new LinearGradientBrush(r, Color.Empty, Color.Empty, angle);
		this.DrawGradientBrush.InterpolationColors = blend;
		this.G.FillRectangle(this.DrawGradientBrush, r);
	}

	// Token: 0x06000189 RID: 393 RVA: 0x00002C66 File Offset: 0x00000E66
	protected void DrawGradient(Color c1, Color c2, int x, int y, int width, int height)
	{
		this.DrawGradientRectangle = new Rectangle(x, y, width, height);
		this.DrawGradient(c1, c2, this.DrawGradientRectangle);
	}

	// Token: 0x0600018A RID: 394 RVA: 0x00002C8A File Offset: 0x00000E8A
	protected void DrawGradient(Color c1, Color c2, int x, int y, int width, int height, float angle)
	{
		this.DrawGradientRectangle = new Rectangle(x, y, width, height);
		this.DrawGradient(c1, c2, this.DrawGradientRectangle, angle);
	}

	// Token: 0x0600018B RID: 395 RVA: 0x00002CB0 File Offset: 0x00000EB0
	protected void DrawGradient(Color c1, Color c2, Rectangle r)
	{
		this.DrawGradientBrush = new LinearGradientBrush(r, c1, c2, 90f);
		this.G.FillRectangle(this.DrawGradientBrush, r);
	}

	// Token: 0x0600018C RID: 396 RVA: 0x00002CD9 File Offset: 0x00000ED9
	protected void DrawGradient(Color c1, Color c2, Rectangle r, float angle)
	{
		this.DrawGradientBrush = new LinearGradientBrush(r, c1, c2, angle);
		this.G.FillRectangle(this.DrawGradientBrush, r);
	}

	// Token: 0x0600018D RID: 397 RVA: 0x00002CFF File Offset: 0x00000EFF
	public void DrawRadial(ColorBlend blend, int x, int y, int width, int height)
	{
		this.DrawRadialRectangle = new Rectangle(x, y, width, height);
		this.DrawRadial(blend, this.DrawRadialRectangle, width / 2, height / 2);
	}

	// Token: 0x0600018E RID: 398 RVA: 0x00002D29 File Offset: 0x00000F29
	public void DrawRadial(ColorBlend blend, int x, int y, int width, int height, Point center)
	{
		this.DrawRadialRectangle = new Rectangle(x, y, width, height);
		this.DrawRadial(blend, this.DrawRadialRectangle, center.X, center.Y);
	}

	// Token: 0x0600018F RID: 399 RVA: 0x00002D59 File Offset: 0x00000F59
	public void DrawRadial(ColorBlend blend, int x, int y, int width, int height, int cx, int cy)
	{
		this.DrawRadialRectangle = new Rectangle(x, y, width, height);
		this.DrawRadial(blend, this.DrawRadialRectangle, cx, cy);
	}

	// Token: 0x06000190 RID: 400 RVA: 0x00002D7F File Offset: 0x00000F7F
	public void DrawRadial(ColorBlend blend, Rectangle r)
	{
		this.DrawRadial(blend, r, r.Width / 2, r.Height / 2);
	}

	// Token: 0x06000191 RID: 401 RVA: 0x00002D9D File Offset: 0x00000F9D
	public void DrawRadial(ColorBlend blend, Rectangle r, Point center)
	{
		this.DrawRadial(blend, r, center.X, center.Y);
	}

	// Token: 0x06000192 RID: 402 RVA: 0x00012058 File Offset: 0x00010258
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

	// Token: 0x06000193 RID: 403 RVA: 0x00002DB7 File Offset: 0x00000FB7
	protected void DrawRadial(Color c1, Color c2, int x, int y, int width, int height)
	{
		this.DrawRadialRectangle = new Rectangle(x, y, width, height);
		this.DrawRadial(c1, c2, this.DrawRadialRectangle);
	}

	// Token: 0x06000194 RID: 404 RVA: 0x00002DDB File Offset: 0x00000FDB
	protected void DrawRadial(Color c1, Color c2, int x, int y, int width, int height, float angle)
	{
		this.DrawRadialRectangle = new Rectangle(x, y, width, height);
		this.DrawRadial(c1, c2, this.DrawRadialRectangle, angle);
	}

	// Token: 0x06000195 RID: 405 RVA: 0x00002E01 File Offset: 0x00001001
	protected void DrawRadial(Color c1, Color c2, Rectangle r)
	{
		this.DrawRadialBrush2 = new LinearGradientBrush(r, c1, c2, 90f);
		this.G.FillEllipse(this.DrawRadialBrush2, r);
	}

	// Token: 0x06000196 RID: 406 RVA: 0x00002E2A File Offset: 0x0000102A
	protected void DrawRadial(Color c1, Color c2, Rectangle r, float angle)
	{
		this.DrawRadialBrush2 = new LinearGradientBrush(r, c1, c2, angle);
		this.G.FillEllipse(this.DrawRadialBrush2, r);
	}

	// Token: 0x06000197 RID: 407 RVA: 0x00012148 File Offset: 0x00010348
	public GraphicsPath CreateRound(int x, int y, int width, int height, int slope)
	{
		this.CreateRoundRectangle = new Rectangle(x, y, width, height);
		return this.CreateRound(this.CreateRoundRectangle, slope);
	}

	// Token: 0x06000198 RID: 408 RVA: 0x00012178 File Offset: 0x00010378
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

	// Token: 0x0400020E RID: 526
	protected Graphics G;

	// Token: 0x0400020F RID: 527
	protected Bitmap B;

	// Token: 0x04000210 RID: 528
	private bool DoneCreation;

	// Token: 0x04000211 RID: 529
	private bool InPosition;

	// Token: 0x04000212 RID: 530
	protected MouseState State;

	// Token: 0x04000213 RID: 531
	private bool _BackColor;

	// Token: 0x04000214 RID: 532
	private bool _NoRounding;

	// Token: 0x04000215 RID: 533
	private Image _Image;

	// Token: 0x04000216 RID: 534
	private bool _Transparent;

	// Token: 0x04000217 RID: 535
	private Dictionary<string, Color> Items = new Dictionary<string, Color>();

	// Token: 0x04000218 RID: 536
	private string _Customization;

	// Token: 0x04000219 RID: 537
	private Size _ImageSize;

	// Token: 0x0400021A RID: 538
	private int _LockWidth;

	// Token: 0x0400021B RID: 539
	private int _LockHeight;

	// Token: 0x0400021C RID: 540
	private bool _IsAnimated;

	// Token: 0x0400021D RID: 541
	private Rectangle OffsetReturnRectangle;

	// Token: 0x0400021E RID: 542
	private Size OffsetReturnSize;

	// Token: 0x0400021F RID: 543
	private Point OffsetReturnPoint;

	// Token: 0x04000220 RID: 544
	private Point CenterReturn;

	// Token: 0x04000221 RID: 545
	private Bitmap MeasureBitmap;

	// Token: 0x04000222 RID: 546
	private Graphics MeasureGraphics;

	// Token: 0x04000223 RID: 547
	private SolidBrush DrawPixelBrush;

	// Token: 0x04000224 RID: 548
	private SolidBrush DrawCornersBrush;

	// Token: 0x04000225 RID: 549
	private Point DrawTextPoint;

	// Token: 0x04000226 RID: 550
	private Size DrawTextSize;

	// Token: 0x04000227 RID: 551
	private Point DrawImagePoint;

	// Token: 0x04000228 RID: 552
	private LinearGradientBrush DrawGradientBrush;

	// Token: 0x04000229 RID: 553
	private Rectangle DrawGradientRectangle;

	// Token: 0x0400022A RID: 554
	private GraphicsPath DrawRadialPath;

	// Token: 0x0400022B RID: 555
	private PathGradientBrush DrawRadialBrush1;

	// Token: 0x0400022C RID: 556
	private LinearGradientBrush DrawRadialBrush2;

	// Token: 0x0400022D RID: 557
	private Rectangle DrawRadialRectangle;

	// Token: 0x0400022E RID: 558
	private GraphicsPath CreateRoundPath;

	// Token: 0x0400022F RID: 559
	private Rectangle CreateRoundRectangle;
}
