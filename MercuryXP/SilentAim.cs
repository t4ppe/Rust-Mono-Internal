using System;
using ProtoBuf;
using UnityEngine;

namespace MercuryXP
{
	// Token: 0x02000043 RID: 67
	public class SilentAim : MonoBehaviour
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x000138EC File Offset: 0x00011AEC
		
		// Token: 0x060001E9 RID: 489 RVA: 0x00013918 File Offset: 0x00011B18
		

		// Token: 0x04000251 RID: 593
		public static bool Enabled;

		// Token: 0x04000252 RID: 594
		public static bool Head;

		// Token: 0x02000044 RID: 68
		public enum BoneUID : uint
		{
			// Token: 0x04000254 RID: 596
			Head = 698017942u,
			// Token: 0x04000255 RID: 597
			Chest = 3399023664u,
			// Token: 0x04000256 RID: 598
			Body = 1036806628u,
			// Token: 0x04000257 RID: 599
			Stomach = 3399023662u,
			// Token: 0x04000258 RID: 600
			Arm = 1789163859u,
			// Token: 0x04000259 RID: 601
			Hip = 108214850u,
			// Token: 0x0400025A RID: 602
			Legs = 3354754288u,
			// Token: 0x0400025B RID: 603
			Foot = 3354606619u
		}

		// Token: 0x02000045 RID: 69
		public enum HitPartUID : uint
		{
			// Token: 0x0400025D RID: 605
			Head = 698017942u,
			// Token: 0x0400025E RID: 606
			Chest = 1890214305u
		}
	}
}
