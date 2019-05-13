using Terraria.ModLoader;


namespace UnclutteredProjectiles {
	partial class UPMod : Mod {
		public static bool IsDebugModeInfo() {
			return true;
		}


		////

		public static bool AreFriendlyProjectilesLikelySpam() {
			return true;
		}

		public static bool AreHostileProjectilesLikelySpam() {
			return false;
		}

		////

		public static bool UnclutterDuringBosses() {
			return true;
		}

		public static bool UnclutterDuringInvasions() {
			return true;
		}

		public static bool UnclutterDuringEclipses() {
			return true;
		}

		public static bool UnclutterDuringLunarApocalypse() {
			return false;
		}


		////
		
		public static int GetDustRemoveRate() {
			return 2000;
		}

		////

		public static int GetDustRemoveDistanceSquared() {
			return 25600;   //10 blocks (squared)
		}

		public static int GetProjectileDimNearBossDistanceSquared() {
			return 36864;   //12 blocks (squared)
		}

		public static int GetProjectileDimNearCurrentPlayerDistanceSquared() {
			return 589824;	//48 blocks (squared)
		}

		////

		public static float GetProjectileDimPercent() {
			return 0.9f;
		}
	}
}
