﻿using HamstarHelpers.Components.Config;
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

		public int DustRemoveRate = 2000;
		public int DustRemoveDistanceSquared = 25600;   //10 blocks (squared)

		////

		public int ProjectileDimNearBossDistanceSquared = 36864;   //12 blocks (squared)
		public int ProjectileDimNearCurrentPlayerDistanceSquared = 2359296; //96 blocks (squared)
		public float ProjectileDimPercent = 0.9f;



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
