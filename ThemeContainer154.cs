using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

// Token: 0x02000030 RID: 48
internal abstract class ThemeContainer154 : ContainerControl
{
	// Token: 0x06000098 RID: 152 RVA: 0x0000F358 File Offset: 0x0000D558
	public ThemeContainer154()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.Opaque | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this._ImageSize = Size.Empty;
		this.Font = new Font("Verdana", 8f);
		this.MeasureBitmap = new Bitmap(1, 1);
		this.MeasureGraphics = Graphics.FromImage(this.MeasureBitmap);
		this.DrawRadialPath = new GraphicsPath();
		this.InvalidateCustimization();
	}

	// Token: 0x06000099 RID: 153 RVA: 0x0000F400 File Offset: 0x0000D600
	protected sealed override void OnHandleCreated(EventArgs e)
	{
		bool doneCreation = this.DoneCreation;
		if (doneCreation)
		{
			this.InitializeMessages();
		}
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
		bool flag3 = !this._ControlMode;
		if (flag3)
		{
			base.Dock = DockStyle.Fill;
		}
		this.Transparent = this._Transparent;
		bool flag4 = this._Transparent && this._BackColor;
		if (flag4)
		{
			this.BackColor = Color.Transparent;
		}
		base.OnHandleCreated(e);
	}

	// Token: 0x0600009A RID: 154 RVA: 0x0000F4B0 File Offset: 0x0000D6B0
	protected sealed override void OnParentChanged(EventArgs e)
	{
		base.OnParentChanged(e);
		bool flag = base.Parent == null;
		if (!flag)
		{
			this._IsParentForm = (base.Parent is Form);
			bool flag2 = !this._ControlMode;
			if (flag2)
			{
				this.InitializeMessages();
				bool isParentForm = this._IsParentForm;
				if (isParentForm)
				{
					base.ParentForm.FormBorderStyle = this._BorderStyle;
					base.ParentForm.TransparencyKey = this._TransparencyKey;
					bool flag3 = !base.DesignMode;
					if (flag3)
					{
						base.ParentForm.Shown += this.FormShown;
					}
				}
				base.Parent.BackColor = this.BackColor;
			}
			this.OnCreation();
			this.DoneCreation = true;
			this.InvalidateTimer();
		}
	}

	// Token: 0x0600009B RID: 155 RVA: 0x0000F580 File Offset: 0x0000D780
	private void DoAnimation(bool i)
	{
		this.OnAnimation();
		if (i)
		{
			base.Invalidate();
		}
	}

	// Token: 0x0600009C RID: 156 RVA: 0x0000F5A4 File Offset: 0x0000D7A4
	protected sealed override void OnPaint(PaintEventArgs e)
	{
		bool flag = base.Width == 0 || base.Height == 0;
		if (!flag)
		{
			bool flag2 = this._Transparent && this._ControlMode;
			if (flag2)
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

	// Token: 0x0600009D RID: 157 RVA: 0x000023A7 File Offset: 0x000005A7
	protected override void OnHandleDestroyed(EventArgs e)
	{
		ThemeShare.RemoveAnimationCallback(new ThemeShare.AnimationDelegate(this.DoAnimation));
		base.OnHandleDestroyed(e);
	}

	// Token: 0x0600009E RID: 158 RVA: 0x0000F618 File Offset: 0x0000D818
	private void FormShown(object sender, EventArgs e)
	{
		bool flag = this._ControlMode || this.HasShown;
		if (!flag)
		{
			bool flag2 = this._StartPosition == FormStartPosition.CenterParent || this._StartPosition == FormStartPosition.CenterScreen;
			if (flag2)
			{
				Rectangle bounds = Screen.PrimaryScreen.Bounds;
				Rectangle bounds2 = base.ParentForm.Bounds;
				base.ParentForm.Location = new Point(bounds.Width / 2 - bounds2.Width / 2, bounds.Height / 2 - bounds2.Width / 2);
			}
			this.HasShown = true;
		}
	}

	// Token: 0x0600009F RID: 159 RVA: 0x0000F6B0 File Offset: 0x0000D8B0
	protected sealed override void OnSizeChanged(EventArgs e)
	{
		bool flag = this._Movable && !this._ControlMode;
		if (flag)
		{
			this.Frame = new Rectangle(7, 7, base.Width - 14, this._Header - 7);
		}
		this.InvalidateBitmap();
		base.Invalidate();
		base.OnSizeChanged(e);
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x0000F70C File Offset: 0x0000D90C
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

	// Token: 0x060000A1 RID: 161 RVA: 0x000023C4 File Offset: 0x000005C4
	private void SetState(MouseState current)
	{
		this.State = current;
		base.Invalidate();
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x0000F754 File Offset: 0x0000D954
	protected override void OnMouseMove(MouseEventArgs e)
	{
		bool flag = !this._IsParentForm || base.ParentForm.WindowState != FormWindowState.Maximized;
		if (flag)
		{
			bool flag2 = this._Sizable && !this._ControlMode;
			if (flag2)
			{
				this.InvalidateMouse();
			}
		}
		base.OnMouseMove(e);
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x0000F7AC File Offset: 0x0000D9AC
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

	// Token: 0x060000A4 RID: 164 RVA: 0x000023D5 File Offset: 0x000005D5
	protected override void OnMouseEnter(EventArgs e)
	{
		this.SetState(MouseState.Over);
		base.OnMouseEnter(e);
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x000023E8 File Offset: 0x000005E8
	protected override void OnMouseUp(MouseEventArgs e)
	{
		this.SetState(MouseState.Over);
		base.OnMouseUp(e);
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x0000F7E0 File Offset: 0x0000D9E0
	protected override void OnMouseLeave(EventArgs e)
	{
		this.SetState(MouseState.None);
		bool flag = base.GetChildAtPoint(base.PointToClient(Control.MousePosition)) != null;
		if (flag)
		{
			bool flag2 = this._Sizable && !this._ControlMode;
			if (flag2)
			{
				this.Cursor = Cursors.Default;
				this.Previous = 0;
			}
		}
		base.OnMouseLeave(e);
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x0000F848 File Offset: 0x0000DA48
	protected override void OnMouseDown(MouseEventArgs e)
	{
		bool flag = e.Button == MouseButtons.Left;
		if (flag)
		{
			this.SetState(MouseState.Down);
		}
		bool flag2 = (!this._IsParentForm || base.ParentForm.WindowState != FormWindowState.Maximized) && !this._ControlMode;
		if (flag2)
		{
			bool flag3 = this._Movable && this.Frame.Contains(e.Location);
			if (flag3)
			{
				base.Capture = false;
				this.WM_LMBUTTONDOWN = true;
				this.DefWndProc(ref this.Messages[0]);
			}
			else
			{
				bool flag4 = this._Sizable && this.Previous != 0;
				if (flag4)
				{
					base.Capture = false;
					this.WM_LMBUTTONDOWN = true;
					this.DefWndProc(ref this.Messages[this.Previous]);
				}
			}
		}
		base.OnMouseDown(e);
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x0000F92C File Offset: 0x0000DB2C
	protected override void WndProc(ref Message m)
	{
		base.WndProc(ref m);
		bool flag = this.WM_LMBUTTONDOWN && m.Msg == 513;
		if (flag)
		{
			this.WM_LMBUTTONDOWN = false;
			this.SetState(MouseState.Over);
			bool flag2 = !this._SmartBounds;
			if (!flag2)
			{
				bool isParentMdi = this.IsParentMdi;
				if (isParentMdi)
				{
					this.CorrectBounds(new Rectangle(Point.Empty, base.Parent.Parent.Size));
				}
				else
				{
					this.CorrectBounds(Screen.FromControl(base.Parent).WorkingArea);
				}
			}
		}
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x0000F9C8 File Offset: 0x0000DBC8
	private int GetIndex()
	{
		this.GetIndexPoint = base.PointToClient(Control.MousePosition);
		this.B1 = (this.GetIndexPoint.X < 7);
		this.B2 = (this.GetIndexPoint.X > base.Width - 7);
		this.B3 = (this.GetIndexPoint.Y < 7);
		this.B4 = (this.GetIndexPoint.Y > base.Height - 7);
		bool flag = this.B1 && this.B3;
		int result;
		if (flag)
		{
			result = 4;
		}
		else
		{
			bool flag2 = this.B1 && this.B4;
			if (flag2)
			{
				result = 7;
			}
			else
			{
				bool flag3 = this.B2 && this.B3;
				if (flag3)
				{
					result = 5;
				}
				else
				{
					bool flag4 = this.B2 && this.B4;
					if (flag4)
					{
						result = 8;
					}
					else
					{
						bool b = this.B1;
						if (b)
						{
							result = 1;
						}
						else
						{
							bool b2 = this.B2;
							if (b2)
							{
								result = 2;
							}
							else
							{
								bool b3 = this.B3;
								if (b3)
								{
									result = 3;
								}
								else
								{
									bool b4 = this.B4;
									if (b4)
									{
										result = 6;
									}
									else
									{
										result = 0;
									}
								}
							}
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x060000AA RID: 170 RVA: 0x0000FAF4 File Offset: 0x0000DCF4
	private void InvalidateMouse()
	{
		this.Current = this.GetIndex();
		bool flag = this.Current == this.Previous;
		if (!flag)
		{
			this.Previous = this.Current;
			switch (this.Previous)
			{
			case 0:
				this.Cursor = Cursors.Default;
				break;
			case 1:
			case 2:
				this.Cursor = Cursors.SizeWE;
				break;
			case 3:
			case 6:
				this.Cursor = Cursors.SizeNS;
				break;
			case 4:
			case 8:
				this.Cursor = Cursors.SizeNWSE;
				break;
			case 5:
			case 7:
				this.Cursor = Cursors.SizeNESW;
				break;
			}
		}
	}

	// Token: 0x060000AB RID: 171 RVA: 0x0000FBAC File Offset: 0x0000DDAC
	private void InitializeMessages()
	{
		this.Messages[0] = Message.Create(base.Parent.Handle, 161, new IntPtr(2), IntPtr.Zero);
		for (int i = 1; i <= 8; i++)
		{
			this.Messages[i] = Message.Create(base.Parent.Handle, 161, new IntPtr(i + 9), IntPtr.Zero);
		}
	}

	// Token: 0x060000AC RID: 172 RVA: 0x0000FC2C File Offset: 0x0000DE2C
	private void CorrectBounds(Rectangle bounds)
	{
		bool flag = base.Parent.Width > bounds.Width;
		if (flag)
		{
			base.Parent.Width = bounds.Width;
		}
		bool flag2 = base.Parent.Height > bounds.Height;
		if (flag2)
		{
			base.Parent.Height = bounds.Height;
		}
		int num = base.Parent.Location.X;
		int num2 = base.Parent.Location.Y;
		bool flag3 = num < bounds.X;
		if (flag3)
		{
			num = bounds.X;
		}
		bool flag4 = num2 < bounds.Y;
		if (flag4)
		{
			num2 = bounds.Y;
		}
		int num3 = bounds.X + bounds.Width;
		int num4 = bounds.Y + bounds.Height;
		bool flag5 = num + base.Parent.Width > num3;
		if (flag5)
		{
			num = num3 - base.Parent.Width;
		}
		bool flag6 = num2 + base.Parent.Height > num4;
		if (flag6)
		{
			num2 = num4 - base.Parent.Height;
		}
		base.Parent.Location = new Point(num, num2);
	}

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x060000AD RID: 173 RVA: 0x0000FD6C File Offset: 0x0000DF6C
	// (set) Token: 0x060000AE RID: 174 RVA: 0x0000FD84 File Offset: 0x0000DF84
	public override DockStyle Dock
	{
		get
		{
			return base.Dock;
		}
		set
		{
			bool flag = !this._ControlMode;
			if (!flag)
			{
				base.Dock = value;
			}
		}
	}

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x060000AF RID: 175 RVA: 0x0000FDAC File Offset: 0x0000DFAC
	// (set) Token: 0x060000B0 RID: 176 RVA: 0x0000FDC4 File Offset: 0x0000DFC4
	[Category("Misc")]
	public override Color BackColor
	{
		get
		{
			return base.BackColor;
		}
		set
		{
			bool flag = value == base.BackColor;
			if (!flag)
			{
				bool flag2 = !base.IsHandleCreated && this._ControlMode && value == Color.Transparent;
				if (flag2)
				{
					this._BackColor = true;
				}
				else
				{
					base.BackColor = value;
					bool flag3 = base.Parent != null;
					if (flag3)
					{
						bool flag4 = !this._ControlMode;
						if (flag4)
						{
							base.Parent.BackColor = value;
						}
						this.ColorHook();
					}
				}
			}
		}
	}

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000FE48 File Offset: 0x0000E048
	// (set) Token: 0x060000B2 RID: 178 RVA: 0x0000FE60 File Offset: 0x0000E060
	public override Size MinimumSize
	{
		get
		{
			return base.MinimumSize;
		}
		set
		{
			base.MinimumSize = value;
			bool flag = base.Parent != null;
			if (flag)
			{
				base.Parent.MinimumSize = value;
			}
		}
	}

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000FE90 File Offset: 0x0000E090
	// (set) Token: 0x060000B4 RID: 180 RVA: 0x0000FEA8 File Offset: 0x0000E0A8
	public override Size MaximumSize
	{
		get
		{
			return base.MaximumSize;
		}
		set
		{
			base.MaximumSize = value;
			bool flag = base.Parent != null;
			if (flag)
			{
				base.Parent.MaximumSize = value;
			}
		}
	}

	// Token: 0x1700001D RID: 29
	// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000FED8 File Offset: 0x0000E0D8
	// (set) Token: 0x060000B6 RID: 182 RVA: 0x000023FB File Offset: 0x000005FB
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

	// Token: 0x1700001E RID: 30
	// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000FEF0 File Offset: 0x0000E0F0
	// (set) Token: 0x060000B8 RID: 184 RVA: 0x0000240D File Offset: 0x0000060D
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

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000FF08 File Offset: 0x0000E108
	// (set) Token: 0x060000BA RID: 186 RVA: 0x0000241F File Offset: 0x0000061F
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

	// Token: 0x17000020 RID: 32
	// (get) Token: 0x060000BB RID: 187 RVA: 0x0000FF20 File Offset: 0x0000E120
	// (set) Token: 0x060000BC RID: 188 RVA: 0x0000241F File Offset: 0x0000061F
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

	// Token: 0x17000021 RID: 33
	// (get) Token: 0x060000BD RID: 189 RVA: 0x0000FF34 File Offset: 0x0000E134
	// (set) Token: 0x060000BE RID: 190 RVA: 0x0000241F File Offset: 0x0000061F
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

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x060000BF RID: 191 RVA: 0x0000FF48 File Offset: 0x0000E148
	// (set) Token: 0x060000C0 RID: 192 RVA: 0x00002422 File Offset: 0x00000622
	public bool SmartBounds
	{
		get
		{
			return this._SmartBounds;
		}
		set
		{
			this._SmartBounds = value;
		}
	}

	// Token: 0x17000023 RID: 35
	// (get) Token: 0x060000C1 RID: 193 RVA: 0x0000FF60 File Offset: 0x0000E160
	// (set) Token: 0x060000C2 RID: 194 RVA: 0x0000242C File Offset: 0x0000062C
	public bool Movable
	{
		get
		{
			return this._Movable;
		}
		set
		{
			this._Movable = value;
		}
	}

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000FF78 File Offset: 0x0000E178
	// (set) Token: 0x060000C4 RID: 196 RVA: 0x00002436 File Offset: 0x00000636
	public bool Sizable
	{
		get
		{
			return this._Sizable;
		}
		set
		{
			this._Sizable = value;
		}
	}

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000FF90 File Offset: 0x0000E190
	// (set) Token: 0x060000C6 RID: 198 RVA: 0x0000FFD0 File Offset: 0x0000E1D0
	public Color TransparencyKey
	{
		get
		{
			bool flag = this._IsParentForm && !this._ControlMode;
			Color transparencyKey;
			if (flag)
			{
				transparencyKey = base.ParentForm.TransparencyKey;
			}
			else
			{
				transparencyKey = this._TransparencyKey;
			}
			return transparencyKey;
		}
		set
		{
			bool flag = value == this._TransparencyKey;
			if (!flag)
			{
				this._TransparencyKey = value;
				bool flag2 = this._IsParentForm && !this._ControlMode;
				if (flag2)
				{
					base.ParentForm.TransparencyKey = value;
					this.ColorHook();
				}
			}
		}
	}

	// Token: 0x17000026 RID: 38
	// (get) Token: 0x060000C7 RID: 199 RVA: 0x00010028 File Offset: 0x0000E228
	// (set) Token: 0x060000C8 RID: 200 RVA: 0x00010068 File Offset: 0x0000E268
	public FormBorderStyle BorderStyle
	{
		get
		{
			bool flag = this._IsParentForm && !this._ControlMode;
			FormBorderStyle result;
			if (flag)
			{
				result = base.ParentForm.FormBorderStyle;
			}
			else
			{
				result = this._BorderStyle;
			}
			return result;
		}
		set
		{
			this._BorderStyle = value;
			bool flag = this._IsParentForm && !this._ControlMode;
			if (flag)
			{
				base.ParentForm.FormBorderStyle = value;
				bool flag2 = value > FormBorderStyle.None;
				if (flag2)
				{
					this.Movable = false;
					this.Sizable = false;
				}
			}
		}
	}

	// Token: 0x17000027 RID: 39
	// (get) Token: 0x060000C9 RID: 201 RVA: 0x000100C0 File Offset: 0x0000E2C0
	// (set) Token: 0x060000CA RID: 202 RVA: 0x00010100 File Offset: 0x0000E300
	public FormStartPosition StartPosition
	{
		get
		{
			bool flag = this._IsParentForm && !this._ControlMode;
			FormStartPosition startPosition;
			if (flag)
			{
				startPosition = base.ParentForm.StartPosition;
			}
			else
			{
				startPosition = this._StartPosition;
			}
			return startPosition;
		}
		set
		{
			this._StartPosition = value;
			bool flag = this._IsParentForm && !this._ControlMode;
			if (flag)
			{
				base.ParentForm.StartPosition = value;
			}
		}
	}

	// Token: 0x17000028 RID: 40
	// (get) Token: 0x060000CB RID: 203 RVA: 0x0001013C File Offset: 0x0000E33C
	// (set) Token: 0x060000CC RID: 204 RVA: 0x00002440 File Offset: 0x00000640
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

	// Token: 0x17000029 RID: 41
	// (get) Token: 0x060000CD RID: 205 RVA: 0x00010154 File Offset: 0x0000E354
	// (set) Token: 0x060000CE RID: 206 RVA: 0x0001016C File Offset: 0x0000E36C
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

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x060000CF RID: 207 RVA: 0x000101AC File Offset: 0x0000E3AC
	// (set) Token: 0x060000D0 RID: 208 RVA: 0x00010214 File Offset: 0x0000E414
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

	// Token: 0x1700002B RID: 43
	// (get) Token: 0x060000D1 RID: 209 RVA: 0x00010284 File Offset: 0x0000E484
	// (set) Token: 0x060000D2 RID: 210 RVA: 0x0001029C File Offset: 0x0000E49C
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

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x060000D3 RID: 211 RVA: 0x00010338 File Offset: 0x0000E538
	// (set) Token: 0x060000D4 RID: 212 RVA: 0x00010350 File Offset: 0x0000E550
	public bool Transparent
	{
		get
		{
			return this._Transparent;
		}
		set
		{
			this._Transparent = value;
			bool flag = !base.IsHandleCreated && !this._ControlMode;
			if (!flag)
			{
				bool flag2 = !value && this.BackColor.A != byte.MaxValue;
				if (flag2)
				{
					throw new Exception("Unable to change value to false while a transparent BackColor is in use.");
				}
				base.SetStyle(ControlStyles.Opaque, !value);
				base.SetStyle(ControlStyles.SupportsTransparentBackColor, value);
				this.InvalidateBitmap();
				base.Invalidate();
			}
		}
	}

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x060000D5 RID: 213 RVA: 0x000103D4 File Offset: 0x0000E5D4
	protected Size ImageSize
	{
		get
		{
			return this._ImageSize;
		}
	}

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x060000D6 RID: 214 RVA: 0x000103EC File Offset: 0x0000E5EC
	protected bool IsParentForm
	{
		get
		{
			return this._IsParentForm;
		}
	}

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x060000D7 RID: 215 RVA: 0x00010404 File Offset: 0x0000E604
	protected bool IsParentMdi
	{
		get
		{
			bool flag = base.Parent == null;
			return !flag && base.Parent.Parent != null;
		}
	}

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x060000D8 RID: 216 RVA: 0x00010438 File Offset: 0x0000E638
	// (set) Token: 0x060000D9 RID: 217 RVA: 0x00010450 File Offset: 0x0000E650
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

	// Token: 0x17000031 RID: 49
	// (get) Token: 0x060000DA RID: 218 RVA: 0x00010488 File Offset: 0x0000E688
	// (set) Token: 0x060000DB RID: 219 RVA: 0x000104A0 File Offset: 0x0000E6A0
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

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x060000DC RID: 220 RVA: 0x000104D8 File Offset: 0x0000E6D8
	// (set) Token: 0x060000DD RID: 221 RVA: 0x000104F0 File Offset: 0x0000E6F0
	protected int Header
	{
		get
		{
			return this._Header;
		}
		set
		{
			this._Header = value;
			bool flag = !this._ControlMode;
			if (flag)
			{
				this.Frame = new Rectangle(7, 7, base.Width - 14, value - 7);
				base.Invalidate();
			}
		}
	}

	// Token: 0x17000033 RID: 51
	// (get) Token: 0x060000DE RID: 222 RVA: 0x00010534 File Offset: 0x0000E734
	// (set) Token: 0x060000DF RID: 223 RVA: 0x0001054C File Offset: 0x0000E74C
	protected bool ControlMode
	{
		get
		{
			return this._ControlMode;
		}
		set
		{
			this._ControlMode = value;
			this.Transparent = this._Transparent;
			bool flag = this._Transparent && this._BackColor;
			if (flag)
			{
				this.BackColor = Color.Transparent;
			}
			this.InvalidateBitmap();
			base.Invalidate();
		}
	}

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x060000E0 RID: 224 RVA: 0x000105A0 File Offset: 0x0000E7A0
	// (set) Token: 0x060000E1 RID: 225 RVA: 0x00002451 File Offset: 0x00000651
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

	// Token: 0x060000E2 RID: 226 RVA: 0x000105B8 File Offset: 0x0000E7B8
	protected Pen GetPen(string name)
	{
		return new Pen(this.Items[name]);
	}

	// Token: 0x060000E3 RID: 227 RVA: 0x000105DC File Offset: 0x0000E7DC
	protected Pen GetPen(string name, float width)
	{
		return new Pen(this.Items[name], width);
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x00010600 File Offset: 0x0000E800
	protected SolidBrush GetBrush(string name)
	{
		return new SolidBrush(this.Items[name]);
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x00010624 File Offset: 0x0000E824
	protected Color GetColor(string name)
	{
		return this.Items[name];
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x00010644 File Offset: 0x0000E844
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

	// Token: 0x060000E7 RID: 231 RVA: 0x00002462 File Offset: 0x00000662
	protected void SetColor(string name, byte r, byte g, byte b)
	{
		this.SetColor(name, Color.FromArgb((int)r, (int)g, (int)b));
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x00002476 File Offset: 0x00000676
	protected void SetColor(string name, byte a, byte r, byte g, byte b)
	{
		this.SetColor(name, Color.FromArgb((int)a, (int)r, (int)g, (int)b));
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x0000248C File Offset: 0x0000068C
	protected void SetColor(string name, byte a, Color value)
	{
		this.SetColor(name, Color.FromArgb((int)a, value));
	}

	// Token: 0x060000EA RID: 234 RVA: 0x00010680 File Offset: 0x0000E880
	private void InvalidateBitmap()
	{
		bool flag = this._Transparent && this._ControlMode;
		if (flag)
		{
			bool flag2 = base.Width == 0 || base.Height == 0;
			if (!flag2)
			{
				this.B = new Bitmap(base.Width, base.Height, PixelFormat.Format32bppPArgb);
				this.G = Graphics.FromImage(this.B);
			}
		}
		else
		{
			this.G = null;
			this.B = null;
		}
	}

	// Token: 0x060000EB RID: 235 RVA: 0x00010700 File Offset: 0x0000E900
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

	// Token: 0x060000EC RID: 236 RVA: 0x00010778 File Offset: 0x0000E978
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

	// Token: 0x060000ED RID: 237
	protected abstract void ColorHook();

	// Token: 0x060000EE RID: 238
	protected abstract void PaintHook();

	// Token: 0x060000EF RID: 239 RVA: 0x0000241F File Offset: 0x0000061F
	protected virtual void OnCreation()
	{
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x0000241F File Offset: 0x0000061F
	protected virtual void OnAnimation()
	{
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x000107D4 File Offset: 0x0000E9D4
	protected Rectangle Offset(Rectangle r, int amount)
	{
		this.OffsetReturnRectangle = new Rectangle(r.X + amount, r.Y + amount, r.Width - amount * 2, r.Height - amount * 2);
		return this.OffsetReturnRectangle;
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x00010820 File Offset: 0x0000EA20
	protected Size Offset(Size s, int amount)
	{
		this.OffsetReturnSize = new Size(s.Width + amount, s.Height + amount);
		return this.OffsetReturnSize;
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x00010858 File Offset: 0x0000EA58
	protected Point Offset(Point p, int amount)
	{
		this.OffsetReturnPoint = new Point(p.X + amount, p.Y + amount);
		return this.OffsetReturnPoint;
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x00010890 File Offset: 0x0000EA90
	protected Point Center(Rectangle p, Rectangle c)
	{
		this.CenterReturn = new Point(p.Width / 2 - c.Width / 2 + p.X + c.X, p.Height / 2 - c.Height / 2 + p.Y + c.Y);
		return this.CenterReturn;
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x000108FC File Offset: 0x0000EAFC
	protected Point Center(Rectangle p, Size c)
	{
		this.CenterReturn = new Point(p.Width / 2 - c.Width / 2 + p.X, p.Height / 2 - c.Height / 2 + p.Y);
		return this.CenterReturn;
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x00010958 File Offset: 0x0000EB58
	protected Point Center(Rectangle child)
	{
		return this.Center(base.Width, base.Height, child.Width, child.Height);
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x0001098C File Offset: 0x0000EB8C
	protected Point Center(Size child)
	{
		return this.Center(base.Width, base.Height, child.Width, child.Height);
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x000109C0 File Offset: 0x0000EBC0
	protected Point Center(int childWidth, int childHeight)
	{
		return this.Center(base.Width, base.Height, childWidth, childHeight);
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x000109E8 File Offset: 0x0000EBE8
	protected Point Center(Size p, Size c)
	{
		return this.Center(p.Width, p.Height, c.Width, c.Height);
	}

	// Token: 0x060000FA RID: 250 RVA: 0x00010A1C File Offset: 0x0000EC1C
	protected Point Center(int pWidth, int pHeight, int cWidth, int cHeight)
	{
		this.CenterReturn = new Point(pWidth / 2 - cWidth / 2, pHeight / 2 - cHeight / 2);
		return this.CenterReturn;
	}

	// Token: 0x060000FB RID: 251 RVA: 0x00010A50 File Offset: 0x0000EC50
	protected Size Measure()
	{
		Graphics measureGraphics = this.MeasureGraphics;
		Size result;
		lock (measureGraphics)
		{
			result = this.MeasureGraphics.MeasureString(this.Text, this.Font, base.Width).ToSize();
		}
		return result;
	}

	// Token: 0x060000FC RID: 252 RVA: 0x00010AB8 File Offset: 0x0000ECB8
	protected Size Measure(string text)
	{
		Graphics measureGraphics = this.MeasureGraphics;
		Size result;
		lock (measureGraphics)
		{
			result = this.MeasureGraphics.MeasureString(text, this.Font, base.Width).ToSize();
		}
		return result;
	}

	// Token: 0x060000FD RID: 253 RVA: 0x00010B18 File Offset: 0x0000ED18
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

	// Token: 0x060000FE RID: 254 RVA: 0x0000249E File Offset: 0x0000069E
	protected void DrawCorners(Color c1, int offset)
	{
		this.DrawCorners(c1, 0, 0, base.Width, base.Height, offset);
	}

	// Token: 0x060000FF RID: 255 RVA: 0x000024B8 File Offset: 0x000006B8
	protected void DrawCorners(Color c1, Rectangle r1, int offset)
	{
		this.DrawCorners(c1, r1.X, r1.Y, r1.Width, r1.Height, offset);
	}

	// Token: 0x06000100 RID: 256 RVA: 0x000024E0 File Offset: 0x000006E0
	protected void DrawCorners(Color c1, int x, int y, int width, int height, int offset)
	{
		this.DrawCorners(c1, x + offset, y + offset, width - offset * 2, height - offset * 2);
	}

	// Token: 0x06000101 RID: 257 RVA: 0x00002501 File Offset: 0x00000701
	protected void DrawCorners(Color c1)
	{
		this.DrawCorners(c1, 0, 0, base.Width, base.Height);
	}

	// Token: 0x06000102 RID: 258 RVA: 0x0000251A File Offset: 0x0000071A
	protected void DrawCorners(Color c1, Rectangle r1)
	{
		this.DrawCorners(c1, r1.X, r1.Y, r1.Width, r1.Height);
	}

	// Token: 0x06000103 RID: 259 RVA: 0x00010B68 File Offset: 0x0000ED68
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

	// Token: 0x06000104 RID: 260 RVA: 0x00002541 File Offset: 0x00000741
	protected void DrawBorders(Pen p1, int offset)
	{
		this.DrawBorders(p1, 0, 0, base.Width, base.Height, offset);
	}

	// Token: 0x06000105 RID: 261 RVA: 0x0000255B File Offset: 0x0000075B
	protected void DrawBorders(Pen p1, Rectangle r, int offset)
	{
		this.DrawBorders(p1, r.X, r.Y, r.Width, r.Height, offset);
	}

	// Token: 0x06000106 RID: 262 RVA: 0x00002583 File Offset: 0x00000783
	protected void DrawBorders(Pen p1, int x, int y, int width, int height, int offset)
	{
		this.DrawBorders(p1, x + offset, y + offset, width - offset * 2, height - offset * 2);
	}

	// Token: 0x06000107 RID: 263 RVA: 0x000025A4 File Offset: 0x000007A4
	protected void DrawBorders(Pen p1)
	{
		this.DrawBorders(p1, 0, 0, base.Width, base.Height);
	}

	// Token: 0x06000108 RID: 264 RVA: 0x000025BD File Offset: 0x000007BD
	protected void DrawBorders(Pen p1, Rectangle r)
	{
		this.DrawBorders(p1, r.X, r.Y, r.Width, r.Height);
	}

	// Token: 0x06000109 RID: 265 RVA: 0x000025E4 File Offset: 0x000007E4
	protected void DrawBorders(Pen p1, int x, int y, int width, int height)
	{
		this.G.DrawRectangle(p1, x, y, width - 1, height - 1);
	}

	// Token: 0x0600010A RID: 266 RVA: 0x000025FE File Offset: 0x000007FE
	protected void DrawText(Brush b1, HorizontalAlignment a, int x, int y)
	{
		this.DrawText(b1, this.Text, a, x, y);
	}

	// Token: 0x0600010B RID: 267 RVA: 0x00010C60 File Offset: 0x0000EE60
	protected void DrawText(Brush b1, string text, HorizontalAlignment a, int x, int y)
	{
		bool flag = text.Length == 0;
		if (!flag)
		{
			this.DrawTextSize = this.Measure(text);
			this.DrawTextPoint = new Point(base.Width / 2 - this.DrawTextSize.Width / 2, this.Header / 2 - this.DrawTextSize.Height / 2);
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

	// Token: 0x0600010C RID: 268 RVA: 0x00010D74 File Offset: 0x0000EF74
	protected void DrawText(Brush b1, Point p1)
	{
		bool flag = this.Text.Length == 0;
		if (!flag)
		{
			this.G.DrawString(this.Text, this.Font, b1, p1);
		}
	}

	// Token: 0x0600010D RID: 269 RVA: 0x00010DB8 File Offset: 0x0000EFB8
	protected void DrawText(Brush b1, int x, int y)
	{
		bool flag = this.Text.Length == 0;
		if (!flag)
		{
			this.G.DrawString(this.Text, this.Font, b1, (float)x, (float)y);
		}
	}

	// Token: 0x0600010E RID: 270 RVA: 0x00002613 File Offset: 0x00000813
	protected void DrawImage(HorizontalAlignment a, int x, int y)
	{
		this.DrawImage(this._Image, a, x, y);
	}

	// Token: 0x0600010F RID: 271 RVA: 0x00010DF8 File Offset: 0x0000EFF8
	protected void DrawImage(Image image, HorizontalAlignment a, int x, int y)
	{
		bool flag = image == null;
		if (!flag)
		{
			this.DrawImagePoint = new Point(base.Width / 2 - image.Width / 2, this.Header / 2 - image.Height / 2);
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

	// Token: 0x06000110 RID: 272 RVA: 0x00002626 File Offset: 0x00000826
	protected void DrawImage(Point p1)
	{
		this.DrawImage(this._Image, p1.X, p1.Y);
	}

	// Token: 0x06000111 RID: 273 RVA: 0x00002644 File Offset: 0x00000844
	protected void DrawImage(int x, int y)
	{
		this.DrawImage(this._Image, x, y);
	}

	// Token: 0x06000112 RID: 274 RVA: 0x00002656 File Offset: 0x00000856
	protected void DrawImage(Image image, Point p1)
	{
		this.DrawImage(image, p1.X, p1.Y);
	}

	// Token: 0x06000113 RID: 275 RVA: 0x00010EF4 File Offset: 0x0000F0F4
	protected void DrawImage(Image image, int x, int y)
	{
		bool flag = image == null;
		if (!flag)
		{
			this.G.DrawImage(image, x, y, image.Width, image.Height);
		}
	}

	// Token: 0x06000114 RID: 276 RVA: 0x0000266F File Offset: 0x0000086F
	protected void DrawGradient(ColorBlend blend, int x, int y, int width, int height)
	{
		this.DrawGradientRectangle = new Rectangle(x, y, width, height);
		this.DrawGradient(blend, this.DrawGradientRectangle);
	}

	// Token: 0x06000115 RID: 277 RVA: 0x00002691 File Offset: 0x00000891
	protected void DrawGradient(ColorBlend blend, int x, int y, int width, int height, float angle)
	{
		this.DrawGradientRectangle = new Rectangle(x, y, width, height);
		this.DrawGradient(blend, this.DrawGradientRectangle, angle);
	}

	// Token: 0x06000116 RID: 278 RVA: 0x000026B5 File Offset: 0x000008B5
	protected void DrawGradient(ColorBlend blend, Rectangle r)
	{
		this.DrawGradientBrush = new LinearGradientBrush(r, Color.Empty, Color.Empty, 90f);
		this.DrawGradientBrush.InterpolationColors = blend;
		this.G.FillRectangle(this.DrawGradientBrush, r);
	}

	// Token: 0x06000117 RID: 279 RVA: 0x000026F3 File Offset: 0x000008F3
	protected void DrawGradient(ColorBlend blend, Rectangle r, float angle)
	{
		this.DrawGradientBrush = new LinearGradientBrush(r, Color.Empty, Color.Empty, angle);
		this.DrawGradientBrush.InterpolationColors = blend;
		this.G.FillRectangle(this.DrawGradientBrush, r);
	}

	// Token: 0x06000118 RID: 280 RVA: 0x0000272D File Offset: 0x0000092D
	protected void DrawGradient(Color c1, Color c2, int x, int y, int width, int height)
	{
		this.DrawGradientRectangle = new Rectangle(x, y, width, height);
		this.DrawGradient(c1, c2, this.DrawGradientRectangle);
	}

	// Token: 0x06000119 RID: 281 RVA: 0x00002751 File Offset: 0x00000951
	protected void DrawGradient(Color c1, Color c2, int x, int y, int width, int height, float angle)
	{
		this.DrawGradientRectangle = new Rectangle(x, y, width, height);
		this.DrawGradient(c1, c2, this.DrawGradientRectangle, angle);
	}

	// Token: 0x0600011A RID: 282 RVA: 0x00002777 File Offset: 0x00000977
	protected void DrawGradient(Color c1, Color c2, Rectangle r)
	{
		this.DrawGradientBrush = new LinearGradientBrush(r, c1, c2, 90f);
		this.G.FillRectangle(this.DrawGradientBrush, r);
	}

	// Token: 0x0600011B RID: 283 RVA: 0x000027A0 File Offset: 0x000009A0
	protected void DrawGradient(Color c1, Color c2, Rectangle r, float angle)
	{
		this.DrawGradientBrush = new LinearGradientBrush(r, c1, c2, angle);
		this.G.FillRectangle(this.DrawGradientBrush, r);
	}

	// Token: 0x0600011C RID: 284 RVA: 0x000027C6 File Offset: 0x000009C6
	public void DrawRadial(ColorBlend blend, int x, int y, int width, int height)
	{
		this.DrawRadialRectangle = new Rectangle(x, y, width, height);
		this.DrawRadial(blend, this.DrawRadialRectangle, width / 2, height / 2);
	}

	// Token: 0x0600011D RID: 285 RVA: 0x000027F0 File Offset: 0x000009F0
	public void DrawRadial(ColorBlend blend, int x, int y, int width, int height, Point center)
	{
		this.DrawRadialRectangle = new Rectangle(x, y, width, height);
		this.DrawRadial(blend, this.DrawRadialRectangle, center.X, center.Y);
	}

	// Token: 0x0600011E RID: 286 RVA: 0x00002820 File Offset: 0x00000A20
	public void DrawRadial(ColorBlend blend, int x, int y, int width, int height, int cx, int cy)
	{
		this.DrawRadialRectangle = new Rectangle(x, y, width, height);
		this.DrawRadial(blend, this.DrawRadialRectangle, cx, cy);
	}

	// Token: 0x0600011F RID: 287 RVA: 0x00002846 File Offset: 0x00000A46
	public void DrawRadial(ColorBlend blend, Rectangle r)
	{
		this.DrawRadial(blend, r, r.Width / 2, r.Height / 2);
	}

	// Token: 0x06000120 RID: 288 RVA: 0x00002864 File Offset: 0x00000A64
	public void DrawRadial(ColorBlend blend, Rectangle r, Point center)
	{
		this.DrawRadial(blend, r, center.X, center.Y);
	}

	// Token: 0x06000121 RID: 289 RVA: 0x00010F28 File Offset: 0x0000F128
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

	// Token: 0x06000122 RID: 290 RVA: 0x0000287E File Offset: 0x00000A7E
	protected void DrawRadial(Color c1, Color c2, int x, int y, int width, int height)
	{
		this.DrawRadialRectangle = new Rectangle(x, y, width, height);
		this.DrawRadial(c1, c2, this.DrawGradientRectangle);
	}

	// Token: 0x06000123 RID: 291 RVA: 0x000028A2 File Offset: 0x00000AA2
	protected void DrawRadial(Color c1, Color c2, int x, int y, int width, int height, float angle)
	{
		this.DrawRadialRectangle = new Rectangle(x, y, width, height);
		this.DrawRadial(c1, c2, this.DrawGradientRectangle, angle);
	}

	// Token: 0x06000124 RID: 292 RVA: 0x000028C8 File Offset: 0x00000AC8
	protected void DrawRadial(Color c1, Color c2, Rectangle r)
	{
		this.DrawRadialBrush2 = new LinearGradientBrush(r, c1, c2, 90f);
		this.G.FillRectangle(this.DrawGradientBrush, r);
	}

	// Token: 0x06000125 RID: 293 RVA: 0x000028F1 File Offset: 0x00000AF1
	protected void DrawRadial(Color c1, Color c2, Rectangle r, float angle)
	{
		this.DrawRadialBrush2 = new LinearGradientBrush(r, c1, c2, angle);
		this.G.FillEllipse(this.DrawGradientBrush, r);
	}

	// Token: 0x06000126 RID: 294 RVA: 0x00011018 File Offset: 0x0000F218
	public GraphicsPath CreateRound(int x, int y, int width, int height, int slope)
	{
		this.CreateRoundRectangle = new Rectangle(x, y, width, height);
		return this.CreateRound(this.CreateRoundRectangle, slope);
	}

	// Token: 0x06000127 RID: 295 RVA: 0x00011048 File Offset: 0x0000F248
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

	// Token: 0x040001D9 RID: 473
	protected Graphics G;

	// Token: 0x040001DA RID: 474
	protected Bitmap B;

	// Token: 0x040001DB RID: 475
	private bool DoneCreation;

	// Token: 0x040001DC RID: 476
	private bool HasShown;

	// Token: 0x040001DD RID: 477
	private Rectangle Frame;

	// Token: 0x040001DE RID: 478
	protected MouseState State;

	// Token: 0x040001DF RID: 479
	private bool WM_LMBUTTONDOWN;

	// Token: 0x040001E0 RID: 480
	private Point GetIndexPoint;

	// Token: 0x040001E1 RID: 481
	private bool B1;

	// Token: 0x040001E2 RID: 482
	private bool B2;

	// Token: 0x040001E3 RID: 483
	private bool B3;

	// Token: 0x040001E4 RID: 484
	private bool B4;

	// Token: 0x040001E5 RID: 485
	private int Current;

	// Token: 0x040001E6 RID: 486
	private int Previous;

	// Token: 0x040001E7 RID: 487
	private Message[] Messages = new Message[9];

	// Token: 0x040001E8 RID: 488
	private bool _BackColor;

	// Token: 0x040001E9 RID: 489
	private bool _SmartBounds = true;

	// Token: 0x040001EA RID: 490
	private bool _Movable = true;

	// Token: 0x040001EB RID: 491
	private bool _Sizable = true;

	// Token: 0x040001EC RID: 492
	private Color _TransparencyKey;

	// Token: 0x040001ED RID: 493
	private FormBorderStyle _BorderStyle;

	// Token: 0x040001EE RID: 494
	private FormStartPosition _StartPosition;

	// Token: 0x040001EF RID: 495
	private bool _NoRounding;

	// Token: 0x040001F0 RID: 496
	private Image _Image;

	// Token: 0x040001F1 RID: 497
	private Dictionary<string, Color> Items = new Dictionary<string, Color>();

	// Token: 0x040001F2 RID: 498
	private string _Customization;

	// Token: 0x040001F3 RID: 499
	private bool _Transparent;

	// Token: 0x040001F4 RID: 500
	private Size _ImageSize;

	// Token: 0x040001F5 RID: 501
	private bool _IsParentForm;

	// Token: 0x040001F6 RID: 502
	private int _LockWidth;

	// Token: 0x040001F7 RID: 503
	private int _LockHeight;

	// Token: 0x040001F8 RID: 504
	private int _Header = 24;

	// Token: 0x040001F9 RID: 505
	private bool _ControlMode;

	// Token: 0x040001FA RID: 506
	private bool _IsAnimated;

	// Token: 0x040001FB RID: 507
	private Rectangle OffsetReturnRectangle;

	// Token: 0x040001FC RID: 508
	private Size OffsetReturnSize;

	// Token: 0x040001FD RID: 509
	private Point OffsetReturnPoint;

	// Token: 0x040001FE RID: 510
	private Point CenterReturn;

	// Token: 0x040001FF RID: 511
	private Bitmap MeasureBitmap;

	// Token: 0x04000200 RID: 512
	private Graphics MeasureGraphics;

	// Token: 0x04000201 RID: 513
	private SolidBrush DrawPixelBrush;

	// Token: 0x04000202 RID: 514
	private SolidBrush DrawCornersBrush;

	// Token: 0x04000203 RID: 515
	private Point DrawTextPoint;

	// Token: 0x04000204 RID: 516
	private Size DrawTextSize;

	// Token: 0x04000205 RID: 517
	private Point DrawImagePoint;

	// Token: 0x04000206 RID: 518
	private LinearGradientBrush DrawGradientBrush;

	// Token: 0x04000207 RID: 519
	private Rectangle DrawGradientRectangle;

	// Token: 0x04000208 RID: 520
	private GraphicsPath DrawRadialPath;

	// Token: 0x04000209 RID: 521
	private PathGradientBrush DrawRadialBrush1;

	// Token: 0x0400020A RID: 522
	private LinearGradientBrush DrawRadialBrush2;

	// Token: 0x0400020B RID: 523
	private Rectangle DrawRadialRectangle;

	// Token: 0x0400020C RID: 524
	private GraphicsPath CreateRoundPath;

	// Token: 0x0400020D RID: 525
	private Rectangle CreateRoundRectangle;
}
