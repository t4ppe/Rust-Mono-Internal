using System;
using System.Drawing;

namespace CS_ClassLibraryTester
{
	// Token: 0x0200005D RID: 93
	internal struct Bloom
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0001B07C File Offset: 0x0001927C
		public string Name
		{
			get
			{
				return this._Name;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0001B094 File Offset: 0x00019294
		// (set) Token: 0x06000384 RID: 900 RVA: 0x00003F61 File Offset: 0x00002161
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

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0001B0AC File Offset: 0x000192AC
		// (set) Token: 0x06000386 RID: 902 RVA: 0x0001B114 File Offset: 0x00019314
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

		// Token: 0x06000387 RID: 903 RVA: 0x00003F6B File Offset: 0x0000216B
		public Bloom(string name, Color value)
		{
			this._Name = name;
			this._Value = value;
		}

		// Token: 0x040002EB RID: 747
		public string _Name;

		// Token: 0x040002EC RID: 748
		private Color _Value;
	}
}
