using HamstarHelpers.Components.Config;
using System;


namespace UnclutteredProjectiles.Config {
	public class UPConfigData : ConfigurationDataBase {
		public readonly static string ConfigFileName = "Uncluttered Projectiles Config.json";



		////////////////

		public string VersionSinceUpdate = "";

		public bool DebugModeInfo = false;



		////////////////

		public void SetDefaults() {
		}


		////////////////

		public bool UpdateToLatestVersion() {
			var mymod = UPMod.Instance;
			var newConfig = new UPConfigData();
			newConfig.SetDefaults();

			var versSince = this.VersionSinceUpdate != "" ?
				new Version( this.VersionSinceUpdate ) :
				new Version();

			if( versSince >= mymod.Version ) {
				return false;
			}

			if( this.VersionSinceUpdate == "" ) {
				this.SetDefaults();
			}

			this.VersionSinceUpdate = mymod.Version.ToString();

			return true;
		}
	}
}
