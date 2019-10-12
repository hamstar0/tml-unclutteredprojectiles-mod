using HamstarHelpers.Helpers.TModLoader.Mods;
using Terraria.ModLoader;
using UnclutteredProjectiles.Config;


namespace UnclutteredProjectiles {
	partial class UPMod : Mod {
		public static UPMod Instance { get; private set; }



		////////////////

		public UPConfigData Config => ModContent.GetInstance<UPConfigData>();



		////////////////

		public UPMod() {
			UPMod.Instance = this;
		}

		public override void Load() {
		}

		public override void Unload() {
			UPMod.Instance = null;
		}


		////////////////

		public override object Call( params object[] args ) {
			return ModBoilerplateHelpers.HandleModCall( typeof(UPAPI), args );
		}
	}
}
