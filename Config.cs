using HamstarHelpers.Components.Config;
using System;


namespace UnclutteredProjectiles.Config {
	public class UPConfigData : ConfigurationDataBase {
		public readonly static string ConfigFileName = "Uncluttered Projectiles Config.json";



		////////////////

		public string VersionSinceUpdate = "";

		public bool DebugModeInfo = false;

		public bool AreFriendlyProjectilesLikelySpam = true;
		public bool AreHostileProjectilesLikelySpam = false;
		
		public bool UnclutterDuringBosses = true;
		public bool UnclutterDuringInvasions = true;
		public bool UnclutterDuringEclipses = true;

		public bool UnclutterDuringLunarApocalypse = false;

		////

		public int DustRemoveRatePerTenthOfASecond = 2000;
		public int DustRemoveDistance = 160;   //10 blocks

		////

		public int ProjectileDimNearBossDistance = 192;   //12 blocks (squared)
		public int ProjectileDimNearCurrentPlayerDistance = 1536; //96 blocks (squared)
		public float ProjectileDimPercent = 0.8f;



		////////////////

		public void SetDefaults() { }


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
			if( versSince < new Version(1,1,0) ) {
				if( this.ProjectileDimPercent == 0.9f ) {
					this.ProjectileDimPercent = newConfig.ProjectileDimPercent;
				}
			}

			this.VersionSinceUpdate = mymod.Version.ToString();

			return true;
		}
	}
}
