using HamstarHelpers.Components.Config;
using HamstarHelpers.Helpers.ProjectileHelpers;
using System;
using System.Collections.Generic;
using Terraria.ID;


namespace UnclutteredProjectiles.Config {
	public class UPConfigData : ConfigurationDataBase {
		public readonly static string ConfigFileName = "Uncluttered Projectiles Config.json";



		////////////////

		public string VersionSinceUpdate = "";

		public bool DebugModeInfo = false;

		public bool AreFriendlyProjectilesLikelySpam = true;
		public bool AreHostileProjectilesLikelySpam = false;
		public bool AreFriendlyAndHostileProjectilesLikelySpam = false;
		public bool AreUnfriendlyAndUnhostileProjectilesLikelySpam = true;

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

		///

		public ISet<string> NotSpamProjectiles = new HashSet<string>();



		////////////////

		public void SetDefaults() {
			this.NotSpamProjectiles.Clear();
			this.NotSpamProjectiles.Add( ProjectileIdentityHelpers.GetProperUniqueId(ProjectileID.CrystalVileShardHead) );
			this.NotSpamProjectiles.Add( ProjectileIdentityHelpers.GetProperUniqueId(ProjectileID.CrystalVileShardShaft) );
			this.NotSpamProjectiles.Add( ProjectileIdentityHelpers.GetProperUniqueId(ProjectileID.VilethornBase) );
			this.NotSpamProjectiles.Add( ProjectileIdentityHelpers.GetProperUniqueId(ProjectileID.VilethornTip) );
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
			if( versSince < new Version(1,1,0) ) {
				if( this.ProjectileDimPercent == 0.9f ) {
					this.ProjectileDimPercent = newConfig.ProjectileDimPercent;
				}
			}
			if( versSince < new Version(1,1,1,4) ) {
				this.SetDefaults();
			}

			this.VersionSinceUpdate = mymod.Version.ToString();

			return true;
		}
	}
}
