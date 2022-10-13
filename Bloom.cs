using System;
using System.Drawing;

// Token: 0x02000035 RID: 53
internal struct Bloom
{
	// Token: 0x17000044 RID: 68
	// (get) Token: 0x060001A2 RID: 418 RVA: 0x00012400 File Offset: 0x00010600
	public string Name
	{
		get
		{
			return this._Name;
		}
	}

	// Token: 0x17000045 RID: 69
	// (get) Token: 0x060001A3 RID: 419 RVA: 0x00012418 File Offset: 0x00010618
	// (set) Token: 0x060001A4 RID: 420 RVA: 0x00002E66 File Offset: 0x00001066
	public Color Value
	{
		get
		{
			return this._Value;
		}
		set
		{
			this._Value = value;
		}
	}

	// Token: 0x17000046 RID: 70
	// (get) Token: 0x060001A5 RID: 421 RVA: 0x00012430 File Offset: 0x00010630
	// (set) Token: 0x060001A6 RID: 422 RVA: 0x00012498 File Offset: 0x00010698
	public string ValueHex
	{
		get
		{
			return "#" + this._Value.R.ToString("X2", null) + this._Value.G.ToString("X2", null) + this._Value.B.ToString("X2", null);
		}
		set
		{
			try
			{
				this._Value = ColorTranslator.FromHtml(value);
			}
			catch
			{
			}
		}
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x00002E70 File Offset: 0x00001070
	public Bloom(string name, Color value)
	{
		this._Name = name;
		this._Value = value;
	}

	// Token: 0x0400023B RID: 571
	public string _Name;

	// Token: 0x0400023C RID: 572
	private Color _Value;
}
