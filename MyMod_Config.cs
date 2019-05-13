using Terraria.ModLoader;


namespace UnclutteredProjectiles {
	partial class UPMod : Mod {
		public static bool IsDebugModeInfo() {
			return true;
		}


		public static int GetDustRemoveDistanceSquared() {
			return 65536;   //16 blocks (squared)
		}


		public static int GetProjectileDimNearBossDistanceSquared() {
			return 36864;   //12 blocks (squared)
		}


		public static int GetProjectileDimNearCurrentPlayerDistanceSquared() {
			return 36864;	//12 blocks (squared)
		}


		public static float GetProjectileDimPercent() {
			return 0.9f;
		}
	}
}
