using System;
using UnityEngine;

// Token: 0x02000007 RID: 7
internal class Dynamic2D
{
	// Token: 0x06000017 RID: 23 RVA: 0x00004E3C File Offset: 0x0000303C
	public Dynamic2D(Vector3 position)
	{
		this.Position = position;
		this.Position2D = MainCamera.mainCamera.WorldToScreenPoint(this.Position);
		bool flag = this.Position2D.z > 0f;
		if (flag)
		{
			this.IsValid = true;
		}
	}

	// Token: 0x04000020 RID: 32
	public bool IsValid;

	// Token: 0x04000021 RID: 33
	public Vector3 Position = Vector3.zero;

	// Token: 0x04000022 RID: 34
	public Vector3 Position2D = Vector3.zero;
}
