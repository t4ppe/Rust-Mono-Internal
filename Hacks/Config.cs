using System;
using UnityEngine;

namespace Hacks
{
	// Token: 0x02000063 RID: 99
	public class Config
	{
		// Token: 0x04000354 RID: 852
		public static bool shouldNoRecoil = false;

		// Token: 0x04000355 RID: 853
		public static bool shouldNoSway = false;

		// Token: 0x04000356 RID: 854
		public static bool shouldEnableAimbot = false;

		// Token: 0x04000357 RID: 855
		public static float maxFOV = 120f;

		// Token: 0x04000358 RID: 856
		public static bool AimAtHead = false;

		// Token: 0x04000359 RID: 857
		public static KeyCode AimKey = KeyCode.X;

		// Token: 0x0400035A RID: 858
		public static KeyCode GetKey = KeyCode.Mouse4;

		// Token: 0x0400035B RID: 859
		public static bool BulletDropPrediction = false;

		// Token: 0x0400035C RID: 860
		public static bool VelocityPrediction = false;

		// Token: 0x0400035D RID: 861
		public static bool ForceAutomatic = false;

		// Token: 0x0400035E RID: 862
		public static bool shouldDrawCupboards = false;

		// Token: 0x0400035F RID: 863
		public static bool shouldDrawDoors = false;

		// Token: 0x04000360 RID: 864
		public static bool shouldDrawCorpses = false;

		// Token: 0x04000361 RID: 865
		public static bool shouldDrawHeli = false;

		// Token: 0x04000362 RID: 866
		public static bool shouldDrawWorldItems = false;

		// Token: 0x04000363 RID: 867
		public static bool autoDayTime = false;

		// Token: 0x04000364 RID: 868
		public static bool shouldDrawHealthBar = false;

		// Token: 0x04000365 RID: 869
		public static bool shouldDrawEquipedItem = false;

		// Token: 0x04000366 RID: 870
		public static bool shouldDrawSleepers = false;

		// Token: 0x04000367 RID: 871
		public static bool shouldDrawPlayers = false;

		// Token: 0x04000368 RID: 872
		public static bool shouldDrawStorage = false;

		// Token: 0x04000369 RID: 873
		public static bool shouldDrawCrosshair = false;

		// Token: 0x0400036A RID: 874
		public static bool shouldDrawBaseCar = false;

		// Token: 0x0400036B RID: 875
		public static bool shouldDrawResources = false;

		// Token: 0x0400036C RID: 876
		public static bool shouldDrawCollectible = false;

		// Token: 0x0400036D RID: 877
		public static bool shouldDrawAnimals = false;

		// Token: 0x0400036E RID: 878
		public static bool shouldAlwaysBeDaytime = false;

		// Token: 0x0400036F RID: 879
		public static int drawDistanceGeneral = 1000;

		// Token: 0x04000370 RID: 880
		public static int drawDistanceLoot = 300;

		// Token: 0x04000371 RID: 881
		public static bool shouldDrawWoodenBoxes = false;

		// Token: 0x04000372 RID: 882
		public static bool shouldDrawSupplyDrops = false;

		// Token: 0x04000373 RID: 883
		public static bool shouldDrawBarrels = false;

		// Token: 0x04000374 RID: 884
		public static bool shouldDrawTrash = false;

		// Token: 0x04000375 RID: 885
		public static bool shouldDrawFurnaces = false;

		// Token: 0x04000376 RID: 886
		public static bool shouldDrawFridges = false;

		// Token: 0x04000377 RID: 887
		public static bool shouldDrawCrates = false;

		// Token: 0x04000378 RID: 888
		public static bool shouldDrawStashes = false;

		// Token: 0x04000379 RID: 889
		public static bool shouldDrawRepairBenches = false;

		// Token: 0x0400037A RID: 890
		public static bool shouldDrawRecyclers = false;

		// Token: 0x0400037B RID: 891
		public static bool shouldDrawFoodboxes = false;

		// Token: 0x0400037C RID: 892
		public static bool shouldDrawCampfires = false;

		// Token: 0x0400037D RID: 893
		public static bool shouldDrawFuelStorages = false;

		// Token: 0x0400037E RID: 894
		public static bool shouldDrawWaterCatchers = false;

		// Token: 0x0400037F RID: 895
		public static bool shouldDrawLightSources = false;

		// Token: 0x04000380 RID: 896
		public static bool shouldDrawRefineries = false;

		// Token: 0x04000381 RID: 897
		public static bool shouldDrawQuarries = false;

		// Token: 0x04000382 RID: 898
		public static bool shouldDrawTurrets = false;

		// Token: 0x04000383 RID: 899
		public static bool shouldDrawVendingMachines = false;

		// Token: 0x04000384 RID: 900
		public static bool shouldDrawOthers = false;

		// Token: 0x04000385 RID: 901
		public static bool FastGathering = false;

		// Token: 0x04000386 RID: 902
		public static bool SpeedHack = false;

		// Token: 0x04000387 RID: 903
		public static float SpeedHackValue = 6.8f;

		// Token: 0x04000388 RID: 904
		public static bool ClimbHack = false;

		// Token: 0x04000389 RID: 905
		public static bool DisableGrass = false;

		// Token: 0x0400038A RID: 906
		public static bool NoFallDamage = false;

		// Token: 0x0400038B RID: 907
		public static bool shouldDrawSulfur = false;

		// Token: 0x0400038C RID: 908
		public static bool shouldDrawMetal = false;

		// Token: 0x0400038D RID: 909
		public static bool shouldDrawStone = false;

		// Token: 0x0400038E RID: 910
		public static bool shouldDrawDead = false;

		// Token: 0x0400038F RID: 911
		public static bool shouldDrawPine = false;

		// Token: 0x04000390 RID: 912
		public static bool shouldDrawDrift = false;

		// Token: 0x04000391 RID: 913
		public static bool shouldDrawCactus = false;

		// Token: 0x04000392 RID: 914
		public static bool shouldDrawField = false;

		// Token: 0x04000393 RID: 915
		public static bool shouldDrawDrugoe = false;

		// Token: 0x04000394 RID: 916
		public static bool shouldDrawPalm = false;

		// Token: 0x04000395 RID: 917
		public static bool shouldDrawCbopSulfur = false;

		// Token: 0x04000396 RID: 918
		public static bool shouldDrawCbopMetal = false;

		// Token: 0x04000397 RID: 919
		public static bool shouldDrawCbopStone = false;

		// Token: 0x04000398 RID: 920
		public static bool shouldDrawCbopMushroom = false;

		// Token: 0x04000399 RID: 921
		public static bool shouldDrawCbopHemp = false;

		// Token: 0x0400039A RID: 922
		public static bool shouldDrawCbopWood = false;

		// Token: 0x0400039B RID: 923
		public static bool shouldDrawCbopPumpkin = false;

		// Token: 0x0400039C RID: 924
		public static bool bool_0 = false;

		// Token: 0x0400039D RID: 925
		public static bool shouldDrawCbopDrugoe = false;

		// Token: 0x0400039E RID: 926
		public static Color sleepingColor = new Color(0f, 0f, 1f);

		// Token: 0x0400039F RID: 927
		public static Color awakeColor = new Color(224f, 255f, 82f);

		// Token: 0x040003A0 RID: 928
		public static Color friendlyColor = new Color(0.35f, 0.965f, 0.945f);

		// Token: 0x040003A1 RID: 929
		public static Color storageColor = new Color(0.588f, 0f, 0.196f);

		// Token: 0x040003A2 RID: 930
		public static Color deadColor = new Color(0f, 0.588f, 0.588f);

		// Token: 0x040003A3 RID: 931
		public static Color animalColor = new Color(0.219f, 0.7529f, 0.1137f);

		// Token: 0x040003A4 RID: 932
		public static Color resourceColor = new Color(1f, 1f, 0f);

		// Token: 0x040003A5 RID: 933
		public static Color collectibleColor = new Color(0.515625f, 0.46875f, 0.91015625f);

		// Token: 0x040003A6 RID: 934
		public static Color heliColor = new Color(0.4546f, 0.92549f, 0.6274f);

		// Token: 0x040003A7 RID: 935
		public static Color corpseColor = new Color(0.2392f, 0.576f, 0.447f);

		// Token: 0x040003A8 RID: 936
		public static Color cupboardColor = new Color(0.4431f, 0.23137f, 0.1568f);

		// Token: 0x040003A9 RID: 937
		public static Color doorsColor = new Color(0.2431f, 0.32156f, 0.34117f);

		// Token: 0x040003AA RID: 938
		public static Color worldItemColor = new Color(0.77647f, 0.858823f, 0.83529f);

		// Token: 0x040003AB RID: 939
		public static Color CollectibleColor = new Color(0.2392f, 0.576f, 0.447f);

		// Token: 0x040003AC RID: 940
		public static KeyCode LadderKey = KeyCode.M;

		// Token: 0x02000064 RID: 100
		internal static class ESP
		{
			// Token: 0x040003AD RID: 941
			public static bool shouldDrawCupboards;

			// Token: 0x040003AE RID: 942
			public static bool stone1;

			// Token: 0x040003AF RID: 943
			public static bool sulfur1;

			// Token: 0x040003B0 RID: 944
			public static bool inventory;

			// Token: 0x040003B1 RID: 945
			public static bool metal1;

			// Token: 0x040003B2 RID: 946
			public static bool target;

			// Token: 0x040003B3 RID: 947
			public static bool shouldDrawDoors;

			// Token: 0x040003B4 RID: 948
			public static bool autoturrett;

			// Token: 0x040003B5 RID: 949
			public static bool VisualsWindow;

			// Token: 0x040003B6 RID: 950
			public static bool landmine;

			// Token: 0x040003B7 RID: 951
			public static bool playerbox;

			// Token: 0x040003B8 RID: 952
			public static bool beartrap;

			// Token: 0x040003B9 RID: 953
			public static bool stone;

			// Token: 0x040003BA RID: 954
			public static bool flametrap;

			// Token: 0x040003BB RID: 955
			public static bool cham;

			// Token: 0x040003BC RID: 956
			public static bool guntrap;

			// Token: 0x040003BD RID: 957
			public static bool shouldDrawCorpses;

			// Token: 0x040003BE RID: 958
			public static bool npc;

			// Token: 0x040003BF RID: 959
			public static bool nosink;

			// Token: 0x040003C0 RID: 960
			public static bool shouldDrawHeli;

			// Token: 0x040003C1 RID: 961
			public static bool Skeleton;

			// Token: 0x040003C2 RID: 962
			public static bool shouldDrawWoodenBoxes;

			// Token: 0x040003C3 RID: 963
			public static bool airdrop;

			// Token: 0x040003C4 RID: 964
			public static bool hemp;

			// Token: 0x040003C5 RID: 965
			public static bool Sleepingbag;

			// Token: 0x040003C6 RID: 966
			public static bool shouldDrawWorldItems;

			// Token: 0x040003C7 RID: 967
			public static bool autoDayTime;

			// Token: 0x040003C8 RID: 968
			public static bool shouldDrawHealthBar;

			// Token: 0x040003C9 RID: 969
			public static bool shouldDrawEquipedItem;

			// Token: 0x040003CA RID: 970
			public static bool shouldDrawSleepers;

			// Token: 0x040003CB RID: 971
			public static int drawDistanceGeneral1;

			// Token: 0x040003CC RID: 972
			public static bool shouldDrawPlayers;

			// Token: 0x040003CD RID: 973
			public static bool shouldDrawStorage;

			// Token: 0x040003CE RID: 974
			public static bool shouldDrawCrosshair;

			// Token: 0x040003CF RID: 975
			public static bool shouldDrawResources;

			// Token: 0x040003D0 RID: 976
			public static bool shouldDrawCollectible;

			// Token: 0x040003D1 RID: 977
			public static bool shouldDrawAnimals;

			// Token: 0x040003D2 RID: 978
			public static bool shouldAlwaysBeDaytime;

			// Token: 0x040003D3 RID: 979
			public static int drawDistanceGeneral;

			// Token: 0x040003D4 RID: 980
			public static int drawDistanceLoot;

			// Token: 0x02000065 RID: 101
			internal static class Filters
			{
				// Token: 0x040003D5 RID: 981
				public static bool shouldDrawSupplyDrops;

				// Token: 0x040003D6 RID: 982
				public static bool shoulddrawlargeboxes;

				// Token: 0x040003D7 RID: 983
				public static bool shouldDrawBarrels;

				// Token: 0x040003D8 RID: 984
				public static bool shouldDrawTrash;

				// Token: 0x040003D9 RID: 985
				public static bool shouldDrawFurnaces;

				// Token: 0x040003DA RID: 986
				public static bool woodbox;

				// Token: 0x040003DB RID: 987
				public static bool largebox;

				// Token: 0x040003DC RID: 988
				public static bool shouldDrawFridges;

				// Token: 0x040003DD RID: 989
				public static bool shouldDrawCrates;

				// Token: 0x040003DE RID: 990
				public static bool shouldDrawStashes;

				// Token: 0x040003DF RID: 991
				public static bool shouldDrawRepairBenches;

				// Token: 0x040003E0 RID: 992
				public static bool shouldDrawRecyclers;

				// Token: 0x040003E1 RID: 993
				public static bool shouldDrawFoodboxes;

				// Token: 0x040003E2 RID: 994
				public static bool shouldDrawCampfires;

				// Token: 0x040003E3 RID: 995
				public static bool shouldDrawFuelStorages;

				// Token: 0x040003E4 RID: 996
				public static bool weaponcrate;

				// Token: 0x040003E5 RID: 997
				public static bool elitecrate;

				// Token: 0x040003E6 RID: 998
				public static bool shouldDrawWaterCatchers;

				// Token: 0x040003E7 RID: 999
				public static bool shouldDrawLightSources;

				// Token: 0x040003E8 RID: 1000
				public static bool shouldDrawRefineries;

				// Token: 0x040003E9 RID: 1001
				public static bool shouldDrawQuarries;

				// Token: 0x040003EA RID: 1002
				public static bool shouldDrawTurrets;

				// Token: 0x040003EB RID: 1003
				public static bool shouldDrawVendingMachines;

				// Token: 0x040003EC RID: 1004
				public static bool shouldDrawOthers;
			}

			// Token: 0x02000066 RID: 102
			public static class Colors
			{
				// Token: 0x040003ED RID: 1005
				public static Color sleepingColor;

				// Token: 0x040003EE RID: 1006
				public static Color awakeColor;

				// Token: 0x040003EF RID: 1007
				public static Color friendlyColor;

				// Token: 0x040003F0 RID: 1008
				public static Color storageColor;

				// Token: 0x040003F1 RID: 1009
				public static Color deadColor;

				// Token: 0x040003F2 RID: 1010
				public static Color animalColor;

				// Token: 0x040003F3 RID: 1011
				public static Color resourceColor;

				// Token: 0x040003F4 RID: 1012
				public static Color collectibleColor;

				// Token: 0x040003F5 RID: 1013
				public static Color heliColor;

				// Token: 0x040003F6 RID: 1014
				public static Color corpseColor;

				// Token: 0x040003F7 RID: 1015
				public static Color cupboardColor;

				// Token: 0x040003F8 RID: 1016
				public static Color doorsColor;

				// Token: 0x040003F9 RID: 1017
				public static Color worldItemColor;

				// Token: 0x02000067 RID: 103
				public static class Misc
				{
					// Token: 0x040003FA RID: 1018
					public static bool FastGathering;

					// Token: 0x040003FB RID: 1019
					public static bool SpeedHack;

					// Token: 0x040003FC RID: 1020
					public static bool instant;

					// Token: 0x040003FD RID: 1021
					public static bool autolock;

					// Token: 0x040003FE RID: 1022
					public static bool meleerange;

					// Token: 0x040003FF RID: 1023
					public static bool fastheal;

					// Token: 0x04000400 RID: 1024
					public static float SpeedHackValue;

					// Token: 0x04000401 RID: 1025
					public static bool ClimbHack;

					// Token: 0x04000402 RID: 1026
					public static float dayhekxd = 14f;

					// Token: 0x04000403 RID: 1027
					public static float fovrad = 9f;

					// Token: 0x04000404 RID: 1028
					public static bool MiscWindow;

					// Token: 0x04000405 RID: 1029
					public static bool admin;

					// Token: 0x04000406 RID: 1030
					public static bool autopick;

					// Token: 0x04000407 RID: 1031
					public static bool meleehack;

					// Token: 0x04000408 RID: 1032
					public static bool autopickup;

					// Token: 0x04000409 RID: 1033
					public static bool autopickup1;

					// Token: 0x0400040A RID: 1034
					public static bool autopickup2;

					// Token: 0x0400040B RID: 1035
					public static bool autopickup3;

					// Token: 0x0400040C RID: 1036
					public static bool autopickup4;

					// Token: 0x0400040D RID: 1037
					public static bool melee;

					// Token: 0x0400040E RID: 1038
					public static bool RemovalsWindow;

					// Token: 0x0400040F RID: 1039
					public static bool bows;

					// Token: 0x04000410 RID: 1040
					public static bool spawnlatter;

					// Token: 0x04000411 RID: 1041
					public static bool DisableGrass;

					// Token: 0x04000412 RID: 1042
					public static bool NoFallDamage;

					// Token: 0x04000413 RID: 1043
					public static bool noclip;
				}
			}
		}
	}
}
