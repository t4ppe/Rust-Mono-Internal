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
	// Token: 0x02000059 RID: 89
	internal abstract class ThemeContainer154 : ContainerControl
	{
		// Token: 0x06000278 RID: 632 RVA: 0x00018278 File Offset: 0x00016478
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

		// Token: 0x06000279 RID: 633 RVA: 0x00018320 File Offset: 0x00016520
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

		// Token: 0x0600027A RID: 634 RVA: 0x000183D0 File Offset: 0x000165D0
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

		// Token: 0x0600027B RID: 635 RVA: 0x000184A0 File Offset: 0x000166A0
		private void DoAnimation(bool i)
		{
			this.OnAnimation();
			if (i)
			{
				base.Invalidate();
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x000184C4 File Offset: 0x000166C4
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

		// Token: 0x0600027D RID: 637 RVA: 0x000034C9 File Offset: 0x000016C9
		protected override void OnHandleDestroyed(EventArgs e)
		{
			ThemeShare.RemoveAnimationCallback(new ThemeShare.AnimationDelegate(this.DoAnimation));
			base.OnHandleDestroyed(e);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00018538 File Offset: 0x00016738
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

		// Token: 0x0600027F RID: 639 RVA: 0x000185D0 File Offset: 0x000167D0
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

		// Token: 0x06000280 RID: 640 RVA: 0x0001862C File Offset: 0x0001682C
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

		// Token: 0x06000281 RID: 641 RVA: 0x000034E6 File Offset: 0x000016E6
		private void SetState(MouseState current)
		{
			this.State = current;
			base.Invalidate();
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00018674 File Offset: 0x00016874
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

		// Token: 0x06000283 RID: 643 RVA: 0x000186CC File Offset: 0x000168CC
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

		// Token: 0x06000284 RID: 644 RVA: 0x000034F7 File Offset: 0x000016F7
		protected override void OnMouseEnter(EventArgs e)
		{
			this.SetState(MouseState.Over);
			base.OnMouseEnter(e);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000350A File Offset: 0x0000170A
		protected override void OnMouseUp(MouseEventArgs e)
		{
			this.SetState(MouseState.Over);
			base.OnMouseUp(e);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00018700 File Offset: 0x00016900
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

		// Token: 0x06000287 RID: 647 RVA: 0x00018768 File Offset: 0x00016968
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

		// Token: 0x06000288 RID: 648 RVA: 0x0001884C File Offset: 0x00016A4C
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

		// Token: 0x06000289 RID: 649 RVA: 0x000188E8 File Offset: 0x00016AE8
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

		// Token: 0x0600028A RID: 650 RVA: 0x00018A14 File Offset: 0x00016C14
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

		// Token: 0x0600028B RID: 651 RVA: 0x00018ACC File Offset: 0x00016CCC
		private void InitializeMessages()
		{
			this.Messages[0] = Message.Create(base.Parent.Handle, 161, new IntPtr(2), IntPtr.Zero);
			for (int i = 1; i <= 8; i++)
			{
				this.Messages[i] = Message.Create(base.Parent.Handle, 161, new IntPtr(i + 9), IntPtr.Zero);
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000FC2C File Offset: 0x0000DE2C
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

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000FD6C File Offset: 0x0000DF6C
		// (set) Token: 0x0600028E RID: 654 RVA: 0x00018B4C File Offset: 0x00016D4C
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

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000FDAC File Offset: 0x0000DFAC
		// (set) Token: 0x06000290 RID: 656 RVA: 0x00018B74 File Offset: 0x00016D74
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

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000FE48 File Offset: 0x0000E048
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0000FE60 File Offset: 0x0000E060
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

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000FE90 File Offset: 0x0000E090
		// (set) Token: 0x06000294 RID: 660 RVA: 0x0000FEA8 File Offset: 0x0000E0A8
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

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000FED8 File Offset: 0x0000E0D8
		// (set) Token: 0x06000296 RID: 662 RVA: 0x000023FB File Offset: 0x000005FB
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

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000FEF0 File Offset: 0x0000E0F0
		// (set) Token: 0x06000298 RID: 664 RVA: 0x0000240D File Offset: 0x0000060D
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

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000FF08 File Offset: 0x0000E108
		// (set) Token: 0x0600029A RID: 666 RVA: 0x0000241F File Offset: 0x0000061F
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

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000FF20 File Offset: 0x0000E120
		// (set) Token: 0x0600029C RID: 668 RVA: 0x0000241F File Offset: 0x0000061F
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

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000FF34 File Offset: 0x0000E134
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000241F File Offset: 0x0000061F
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

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00018BF8 File Offset: 0x00016DF8
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000351D File Offset: 0x0000171D
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

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00018C10 File Offset: 0x00016E10
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x00003527 File Offset: 0x00001727
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

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00018C28 File Offset: 0x00016E28
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x00003531 File Offset: 0x00001731
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

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00018C40 File Offset: 0x00016E40
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x00018C80 File Offset: 0x00016E80
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

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00018CD8 File Offset: 0x00016ED8
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x00018D18 File Offset: 0x00016F18
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

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00018D70 File Offset: 0x00016F70
		// (set) Token: 0x060002AA RID: 682 RVA: 0x00018DB0 File Offset: 0x00016FB0
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

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00018DEC File Offset: 0x00016FEC
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000353B File Offset: 0x0000173B
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

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00018E04 File Offset: 0x00017004
		// (set) Token: 0x060002AE RID: 686 RVA: 0x00018E1C File Offset: 0x0001701C
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

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00018E5C File Offset: 0x0001705C
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x00018EC4 File Offset: 0x000170C4
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

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00018F34 File Offset: 0x00017134
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x00018F4C File Offset: 0x0001714C
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

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00018FE8 File Offset: 0x000171E8
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x00019000 File Offset: 0x00017200
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

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00019084 File Offset: 0x00017284
		protected Size ImageSize
		{
			get
			{
				return this._ImageSize;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0001909C File Offset: 0x0001729C
		protected bool IsParentForm
		{
			get
			{
				return this._IsParentForm;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00010404 File Offset: 0x0000E604
		protected bool IsParentMdi
		{
			get
			{
				bool flag = base.Parent == null;
				return !flag && base.Parent.Parent != null;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x000190B4 File Offset: 0x000172B4
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x000190CC File Offset: 0x000172CC
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

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002BA RID: 698 RVA: 0x00019104 File Offset: 0x00017304
		// (set) Token: 0x060002BB RID: 699 RVA: 0x0001911C File Offset: 0x0001731C
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

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00019154 File Offset: 0x00017354
		// (set) Token: 0x060002BD RID: 701 RVA: 0x0001916C File Offset: 0x0001736C
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

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002BE RID: 702 RVA: 0x000191B0 File Offset: 0x000173B0
		// (set) Token: 0x060002BF RID: 703 RVA: 0x000191C8 File Offset: 0x000173C8
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

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0001921C File Offset: 0x0001741C
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000354C File Offset: 0x0000174C
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

		// Token: 0x060002C2 RID: 706 RVA: 0x00019234 File Offset: 0x00017434
		protected Pen GetPen(string name)
		{
			return new Pen(this.Items[name]);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00019258 File Offset: 0x00017458
		protected Pen GetPen(string name, float width)
		{
			return new Pen(this.Items[name], width);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0001927C File Offset: 0x0001747C
		protected SolidBrush GetBrush(string name)
		{
			return new SolidBrush(this.Items[name]);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x000192A0 File Offset: 0x000174A0
		protected Color GetColor(string name)
		{
			return this.Items[name];
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000192C0 File Offset: 0x000174C0
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

		// Token: 0x060002C7 RID: 711 RVA: 0x0000355D File Offset: 0x0000175D
		protected void SetColor(string name, byte r, byte g, byte b)
		{
			this.SetColor(name, Color.FromArgb((int)r, (int)g, (int)b));
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00003571 File Offset: 0x00001771
		protected void SetColor(string name, byte a, byte r, byte g, byte b)
		{
			this.SetColor(name, Color.FromArgb((int)a, (int)r, (int)g, (int)b));
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00003587 File Offset: 0x00001787
		protected void SetColor(string name, byte a, Color value)
		{
			this.SetColor(name, Color.FromArgb((int)a, value));
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000192FC File Offset: 0x000174FC
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

		// Token: 0x060002CB RID: 715 RVA: 0x0001937C File Offset: 0x0001757C
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

		// Token: 0x060002CC RID: 716 RVA: 0x000193F4 File Offset: 0x000175F4
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

		// Token: 0x060002CD RID: 717
		protected abstract void ColorHook();

		// Token: 0x060002CE RID: 718
		protected abstract void PaintHook();

		// Token: 0x060002CF RID: 719 RVA: 0x0000241F File Offset: 0x0000061F
		protected virtual void OnCreation()
		{
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000241F File Offset: 0x0000061F
		protected virtual void OnAnimation()
		{
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00019450 File Offset: 0x00017650
		protected Rectangle Offset(Rectangle r, int amount)
		{
			this.OffsetReturnRectangle = new Rectangle(r.X + amount, r.Y + amount, r.Width - amount * 2, r.Height - amount * 2);
			return this.OffsetReturnRectangle;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0001949C File Offset: 0x0001769C
		protected Size Offset(Size s, int amount)
		{
			this.OffsetReturnSize = new Size(s.Width + amount, s.Height + amount);
			return this.OffsetReturnSize;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x000194D4 File Offset: 0x000176D4
		protected Point Offset(Point p, int amount)
		{
			this.OffsetReturnPoint = new Point(p.X + amount, p.Y + amount);
			return this.OffsetReturnPoint;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0001950C File Offset: 0x0001770C
		protected Point Center(Rectangle p, Rectangle c)
		{
			this.CenterReturn = new Point(p.Width / 2 - c.Width / 2 + p.X + c.X, p.Height / 2 - c.Height / 2 + p.Y + c.Y);
			return this.CenterReturn;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00019578 File Offset: 0x00017778
		protected Point Center(Rectangle p, Size c)
		{
			this.CenterReturn = new Point(p.Width / 2 - c.Width / 2 + p.X, p.Height / 2 - c.Height / 2 + p.Y);
			return this.CenterReturn;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x000195D4 File Offset: 0x000177D4
		protected Point Center(Rectangle child)
		{
			return this.Center(base.Width, base.Height, child.Width, child.Height);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00019608 File Offset: 0x00017808
		protected Point Center(Size child)
		{
			return this.Center(base.Width, base.Height, child.Width, child.Height);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0001963C File Offset: 0x0001783C
		protected Point Center(int childWidth, int childHeight)
		{
			return this.Center(base.Width, base.Height, childWidth, childHeight);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00019664 File Offset: 0x00017864
		protected Point Center(Size p, Size c)
		{
			return this.Center(p.Width, p.Height, c.Width, c.Height);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00019698 File Offset: 0x00017898
		protected Point Center(int pWidth, int pHeight, int cWidth, int cHeight)
		{
			this.CenterReturn = new Point(pWidth / 2 - cWidth / 2, pHeight / 2 - cHeight / 2);
			return this.CenterReturn;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000196CC File Offset: 0x000178CC
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

		// Token: 0x060002DC RID: 732 RVA: 0x00019734 File Offset: 0x00017934
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

		// Token: 0x060002DD RID: 733 RVA: 0x00019794 File Offset: 0x00017994
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

		// Token: 0x060002DE RID: 734 RVA: 0x00003599 File Offset: 0x00001799
		protected void DrawCorners(Color c1, int offset)
		{
			this.DrawCorners(c1, 0, 0, base.Width, base.Height, offset);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000035B3 File Offset: 0x000017B3
		protected void DrawCorners(Color c1, Rectangle r1, int offset)
		{
			this.DrawCorners(c1, r1.X, r1.Y, r1.Width, r1.Height, offset);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x000035DB File Offset: 0x000017DB
		protected void DrawCorners(Color c1, int x, int y, int width, int height, int offset)
		{
			this.DrawCorners(c1, x + offset, y + offset, width - offset * 2, height - offset * 2);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000035FC File Offset: 0x000017FC
		protected void DrawCorners(Color c1)
		{
			this.DrawCorners(c1, 0, 0, base.Width, base.Height);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00003615 File Offset: 0x00001815
		protected void DrawCorners(Color c1, Rectangle r1)
		{
			this.DrawCorners(c1, r1.X, r1.Y, r1.Width, r1.Height);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x000197E4 File Offset: 0x000179E4
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

		// Token: 0x060002E4 RID: 740 RVA: 0x0000363C File Offset: 0x0000183C
		protected void DrawBorders(Pen p1, int offset)
		{
			this.DrawBorders(p1, 0, 0, base.Width, base.Height, offset);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00003656 File Offset: 0x00001856
		protected void DrawBorders(Pen p1, Rectangle r, int offset)
		{
			this.DrawBorders(p1, r.X, r.Y, r.Width, r.Height, offset);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000367E File Offset: 0x0000187E
		protected void DrawBorders(Pen p1, int x, int y, int width, int height, int offset)
		{
			this.DrawBorders(p1, x + offset, y + offset, width - offset * 2, height - offset * 2);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000369F File Offset: 0x0000189F
		protected void DrawBorders(Pen p1)
		{
			this.DrawBorders(p1, 0, 0, base.Width, base.Height);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000036B8 File Offset: 0x000018B8
		protected void DrawBorders(Pen p1, Rectangle r)
		{
			this.DrawBorders(p1, r.X, r.Y, r.Width, r.Height);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000036DF File Offset: 0x000018DF
		protected void DrawBorders(Pen p1, int x, int y, int width, int height)
		{
			this.G.DrawRectangle(p1, x, y, width - 1, height - 1);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x000036F9 File Offset: 0x000018F9
		protected void DrawText(Brush b1, HorizontalAlignment a, int x, int y)
		{
			this.DrawText(b1, this.Text, a, x, y);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x000198DC File Offset: 0x00017ADC
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

		// Token: 0x060002EC RID: 748 RVA: 0x000199F0 File Offset: 0x00017BF0
		protected void DrawText(Brush b1, Point p1)
		{
			bool flag = this.Text.Length == 0;
			if (!flag)
			{
				this.G.DrawString(this.Text, this.Font, b1, p1);
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00019A34 File Offset: 0x00017C34
		protected void DrawText(Brush b1, int x, int y)
		{
			bool flag = this.Text.Length == 0;
			if (!flag)
			{
				this.G.DrawString(this.Text, this.Font, b1, (float)x, (float)y);
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000370E File Offset: 0x0000190E
		protected void DrawImage(HorizontalAlignment a, int x, int y)
		{
			this.DrawImage(this._Image, a, x, y);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00019A74 File Offset: 0x00017C74
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

		// Token: 0x060002F0 RID: 752 RVA: 0x00003721 File Offset: 0x00001921
		protected void DrawImage(Point p1)
		{
			this.DrawImage(this._Image, p1.X, p1.Y);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000373F File Offset: 0x0000193F
		protected void DrawImage(int x, int y)
		{
			this.DrawImage(this._Image, x, y);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00003751 File Offset: 0x00001951
		protected void DrawImage(Image image, Point p1)
		{
			this.DrawImage(image, p1.X, p1.Y);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00019B70 File Offset: 0x00017D70
		protected void DrawImage(Image image, int x, int y)
		{
			bool flag = image == null;
			if (!flag)
			{
				this.G.DrawImage(image, x, y, image.Width, image.Height);
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000376A File Offset: 0x0000196A
		protected void DrawGradient(ColorBlend blend, int x, int y, int width, int height)
		{
			this.DrawGradientRectangle = new Rectangle(x, y, width, height);
			this.DrawGradient(blend, this.DrawGradientRectangle);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000378C File Offset: 0x0000198C
		protected void DrawGradient(ColorBlend blend, int x, int y, int width, int height, float angle)
		{
			this.DrawGradientRectangle = new Rectangle(x, y, width, height);
			this.DrawGradient(blend, this.DrawGradientRectangle, angle);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x000037B0 File Offset: 0x000019B0
		protected void DrawGradient(ColorBlend blend, Rectangle r)
		{
			this.DrawGradientBrush = new LinearGradientBrush(r, Color.Empty, Color.Empty, 90f);
			this.DrawGradientBrush.InterpolationColors = blend;
			this.G.FillRectangle(this.DrawGradientBrush, r);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x000037EE File Offset: 0x000019EE
		protected void DrawGradient(ColorBlend blend, Rectangle r, float angle)
		{
			this.DrawGradientBrush = new LinearGradientBrush(r, Color.Empty, Color.Empty, angle);
			this.DrawGradientBrush.InterpolationColors = blend;
			this.G.FillRectangle(this.DrawGradientBrush, r);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00003828 File Offset: 0x00001A28
		protected void DrawGradient(Color c1, Color c2, int x, int y, int width, int height)
		{
			this.DrawGradientRectangle = new Rectangle(x, y, width, height);
			this.DrawGradient(c1, c2, this.DrawGradientRectangle);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000384C File Offset: 0x00001A4C
		protected void DrawGradient(Color c1, Color c2, int x, int y, int width, int height, float angle)
		{
			this.DrawGradientRectangle = new Rectangle(x, y, width, height);
			this.DrawGradient(c1, c2, this.DrawGradientRectangle, angle);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00003872 File Offset: 0x00001A72
		protected void DrawGradient(Color c1, Color c2, Rectangle r)
		{
			this.DrawGradientBrush = new LinearGradientBrush(r, c1, c2, 90f);
			this.G.FillRectangle(this.DrawGradientBrush, r);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000389B File Offset: 0x00001A9B
		protected void DrawGradient(Color c1, Color c2, Rectangle r, float angle)
		{
			this.DrawGradientBrush = new LinearGradientBrush(r, c1, c2, angle);
			this.G.FillRectangle(this.DrawGradientBrush, r);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x000038C1 File Offset: 0x00001AC1
		public void DrawRadial(ColorBlend blend, int x, int y, int width, int height)
		{
			this.DrawRadialRectangle = new Rectangle(x, y, width, height);
			this.DrawRadial(blend, this.DrawRadialRectangle, width / 2, height / 2);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x000038EB File Offset: 0x00001AEB
		public void DrawRadial(ColorBlend blend, int x, int y, int width, int height, Point center)
		{
			this.DrawRadialRectangle = new Rectangle(x, y, width, height);
			this.DrawRadial(blend, this.DrawRadialRectangle, center.X, center.Y);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000391B File Offset: 0x00001B1B
		public void DrawRadial(ColorBlend blend, int x, int y, int width, int height, int cx, int cy)
		{
			this.DrawRadialRectangle = new Rectangle(x, y, width, height);
			this.DrawRadial(blend, this.DrawRadialRectangle, cx, cy);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00003941 File Offset: 0x00001B41
		public void DrawRadial(ColorBlend blend, Rectangle r)
		{
			this.DrawRadial(blend, r, r.Width / 2, r.Height / 2);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000395F File Offset: 0x00001B5F
		public void DrawRadial(ColorBlend blend, Rectangle r, Point center)
		{
			this.DrawRadial(blend, r, center.X, center.Y);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00019BA4 File Offset: 0x00017DA4
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

		// Token: 0x06000302 RID: 770 RVA: 0x00003979 File Offset: 0x00001B79
		protected void DrawRadial(Color c1, Color c2, int x, int y, int width, int height)
		{
			this.DrawRadialRectangle = new Rectangle(x, y, width, height);
			this.DrawRadial(c1, c2, this.DrawGradientRectangle);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000399D File Offset: 0x00001B9D
		protected void DrawRadial(Color c1, Color c2, int x, int y, int width, int height, float angle)
		{
			this.DrawRadialRectangle = new Rectangle(x, y, width, height);
			this.DrawRadial(c1, c2, this.DrawGradientRectangle, angle);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000039C3 File Offset: 0x00001BC3
		protected void DrawRadial(Color c1, Color c2, Rectangle r)
		{
			this.DrawRadialBrush2 = new LinearGradientBrush(r, c1, c2, 90f);
			this.G.FillRectangle(this.DrawGradientBrush, r);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000039EC File Offset: 0x00001BEC
		protected void DrawRadial(Color c1, Color c2, Rectangle r, float angle)
		{
			this.DrawRadialBrush2 = new LinearGradientBrush(r, c1, c2, angle);
			this.G.FillEllipse(this.DrawGradientBrush, r);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00019C94 File Offset: 0x00017E94
		public GraphicsPath CreateRound(int x, int y, int width, int height, int slope)
		{
			this.CreateRoundRectangle = new Rectangle(x, y, width, height);
			return this.CreateRound(this.CreateRoundRectangle, slope);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00019CC4 File Offset: 0x00017EC4
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

		// Token: 0x0400028E RID: 654
		protected Graphics G;

		// Token: 0x0400028F RID: 655
		protected Bitmap B;

		// Token: 0x04000290 RID: 656
		private bool DoneCreation;

		// Token: 0x04000291 RID: 657
		private bool HasShown;

		// Token: 0x04000292 RID: 658
		private Rectangle Frame;

		// Token: 0x04000293 RID: 659
		protected MouseState State;

		// Token: 0x04000294 RID: 660
		private bool WM_LMBUTTONDOWN;

		// Token: 0x04000295 RID: 661
		private Point GetIndexPoint;

		// Token: 0x04000296 RID: 662
		private bool B1;

		// Token: 0x04000297 RID: 663
		private bool B2;

		// Token: 0x04000298 RID: 664
		private bool B3;

		// Token: 0x04000299 RID: 665
		private bool B4;

		// Token: 0x0400029A RID: 666
		private int Current;

		// Token: 0x0400029B RID: 667
		private int Previous;

		// Token: 0x0400029C RID: 668
		private Message[] Messages = new Message[9];

		// Token: 0x0400029D RID: 669
		private bool _BackColor;

		// Token: 0x0400029E RID: 670
		private bool _SmartBounds = true;

		// Token: 0x0400029F RID: 671
		private bool _Movable = true;

		// Token: 0x040002A0 RID: 672
		private bool _Sizable = true;

		// Token: 0x040002A1 RID: 673
		private Color _TransparencyKey;

		// Token: 0x040002A2 RID: 674
		private FormBorderStyle _BorderStyle;

		// Token: 0x040002A3 RID: 675
		private FormStartPosition _StartPosition;

		// Token: 0x040002A4 RID: 676
		private bool _NoRounding;

		// Token: 0x040002A5 RID: 677
		private Image _Image;

		// Token: 0x040002A6 RID: 678
		private Dictionary<string, Color> Items = new Dictionary<string, Color>();

		// Token: 0x040002A7 RID: 679
		private string _Customization;

		// Token: 0x040002A8 RID: 680
		private bool _Transparent;

		// Token: 0x040002A9 RID: 681
		private Size _ImageSize;

		// Token: 0x040002AA RID: 682
		private bool _IsParentForm;

		// Token: 0x040002AB RID: 683
		private int _LockWidth;

		// Token: 0x040002AC RID: 684
		private int _LockHeight;

		// Token: 0x040002AD RID: 685
		private int _Header = 24;

		// Token: 0x040002AE RID: 686
		private bool _ControlMode;

		// Token: 0x040002AF RID: 687
		private bool _IsAnimated;

		// Token: 0x040002B0 RID: 688
		private Rectangle OffsetReturnRectangle;

		// Token: 0x040002B1 RID: 689
		private Size OffsetReturnSize;

		// Token: 0x040002B2 RID: 690
		private Point OffsetReturnPoint;

		// Token: 0x040002B3 RID: 691
		private Point CenterReturn;

		// Token: 0x040002B4 RID: 692
		private Bitmap MeasureBitmap;

		// Token: 0x040002B5 RID: 693
		private Graphics MeasureGraphics;

		// Token: 0x040002B6 RID: 694
		private SolidBrush DrawPixelBrush;

		// Token: 0x040002B7 RID: 695
		private SolidBrush DrawCornersBrush;

		// Token: 0x040002B8 RID: 696
		private Point DrawTextPoint;

		// Token: 0x040002B9 RID: 697
		private Size DrawTextSize;

		// Token: 0x040002BA RID: 698
		private Point DrawImagePoint;

		// Token: 0x040002BB RID: 699
		private LinearGradientBrush DrawGradientBrush;

		// Token: 0x040002BC RID: 700
		private Rectangle DrawGradientRectangle;

		// Token: 0x040002BD RID: 701
		private GraphicsPath DrawRadialPath;

		// Token: 0x040002BE RID: 702
		private PathGradientBrush DrawRadialBrush1;

		// Token: 0x040002BF RID: 703
		private LinearGradientBrush DrawRadialBrush2;

		// Token: 0x040002C0 RID: 704
		private Rectangle DrawRadialRectangle;

		// Token: 0x040002C1 RID: 705
		private GraphicsPath CreateRoundPath;

		// Token: 0x040002C2 RID: 706
		private Rectangle CreateRoundRectangle;
	}
}
