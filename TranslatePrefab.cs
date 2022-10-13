using System;

// Token: 0x0200002F RID: 47
public class TranslatePrefab
{
	// Token: 0x06000096 RID: 150 RVA: 0x0000F1B4 File Offset: 0x0000D3B4
	public static string Translate(string prefabName)
	{
		string result = prefabName;
		bool flag = prefabName == "stag";
		if (flag)
		{
			result = "Stag";
		}
		bool flag2 = prefabName == "wolf";
		if (flag2)
		{
			result = "Wolf";
		}
		bool flag3 = prefabName == "chicken";
		if (flag3)
		{
			result = "Chicken";
		}
		bool flag4 = prefabName == "boar";
		if (flag4)
		{
			result = "Boar";
		}
		bool flag5 = prefabName == "bear";
		if (flag5)
		{
			result = "Bear";
		}
		bool flag6 = prefabName == "horse";
		if (flag6)
		{
			result = "Horse";
		}
		bool flag7 = prefabName == "stone-ore";
		if (flag7)
		{
			result = "Stone Ore";
		}
		bool flag8 = prefabName == "sulfur-ore";
		if (flag8)
		{
			result = "Sulfur Ore";
		}
		bool flag9 = prefabName == "metal-ore";
		if (flag9)
		{
			result = "Metal Ore";
		}
		bool flag10 = prefabName == "box.wooden.large";
		if (flag10)
		{
			result = "Large Wooden Box";
		}
		bool flag11 = prefabName == "furnace";
		if (flag11)
		{
			result = "Furnace";
		}
		bool flag12 = prefabName == "furnace.large";
		if (flag12)
		{
			result = "Large Furnace";
		}
		bool flag13 = prefabName == "woodbox_deployed";
		if (flag13)
		{
			result = "Small Wooden Box";
		}
		bool flag14 = prefabName == "supply_drop";
		if (flag14)
		{
			result = "Airdrop";
		}
		bool flag15 = prefabName == "smallstash";
		if (flag15)
		{
			result = "Stash";
		}
		bool flag16 = prefabName.Contains("hemp");
		if (flag16)
		{
			result = "Hemp";
		}
		return result;
	}
}
