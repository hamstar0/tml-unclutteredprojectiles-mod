using HamstarHelpers.Components.Config;
using HamstarHelpers.Helpers.TmlHelpers.ModHelpers;
using Terraria.ModLoader;
using UnclutteredProjectiles.Config;


namespace UnclutteredProjectiles {
	partial class UPMod : Mod {
		public static UPMod Instance { get; private set; }



		////////////////

		public JsonConfig<UPConfigData> ConfigJson { get; private set; }
		public UPConfigData Config => this.ConfigJson.Data;



		////////////////

		public UPMod() {
			UPMod.Instance = this;

			this.ConfigJson = new JsonConfig<UPConfigData>(
				UPConfigData.ConfigFileName,
				ConfigurationDataBase.RelativePath,
				new UPConfigData()
			);
		}

		public override void Load() {
			this.LoadConfig();
		}

		public override void Unload() {
			UPMod.Instance = null;
		}

		////

		private void LoadConfig() {
			var mymod = UPMod.Instance;

			if( !this.ConfigJson.LoadFile() ) {
				this.Config.SetDefaults();
				this.ConfigJson.SaveFile();
				ErrorLogger.Log( "Uncluttered Projectiles config " + mymod.Version.ToString() + " created." );
			}

			if( this.Config.UpdateToLatestVersion() ) {
				ErrorLogger.Log( "Uncluttered Projectiles updated to " + mymod.Version.ToString() );
				this.ConfigJson.SaveFile();
			}
		}


		////////////////

		public override object Call( params object[] args ) {
			return ModBoilerplateHelpers.HandleModCall( typeof(UPAPI), args );
		}
	}
}
