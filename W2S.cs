using System;
using UnityEngine;

// Token: 0x02000042 RID: 66
public static class W2S
{
	// Token: 0x060001E7 RID: 487 RVA: 0x000138C4 File Offset: 0x00011AC4
	public static bool WithinViewport(Vector2 pos)
	{
		return MainCamera.mainCamera.pixelRect.Contains(pos);
	}
}
