using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Properties
{
	// Token: 0x02000047 RID: 71
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x060001ED RID: 493 RVA: 0x00003023 File Offset: 0x00001223
		internal Resources()
		{
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001EE RID: 494 RVA: 0x000140C8 File Offset: 0x000122C8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = Resources.resourceMan == null;
				if (flag)
				{
					ResourceManager resourceManager = new ResourceManager("Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00014110 File Offset: 0x00012310
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x0000302D File Offset: 0x0000122D
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x0400025F RID: 607
		private static ResourceManager resourceMan;

		// Token: 0x04000260 RID: 608
		private static CultureInfo resourceCulture;
	}
}
