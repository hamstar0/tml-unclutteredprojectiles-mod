using Terraria;


namespace UnclutteredProjectiles {
	public static partial class UPAPI {
		public static object GetModSettings() {
			return UPMod.Instance.Config;
		}

		public static void SaveModSettingsChanges() {
			UPMod.Instance.ConfigJson.SaveFile();
		}
	}
}
