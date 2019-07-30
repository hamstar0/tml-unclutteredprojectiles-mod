using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Terraria.ID;
using Terraria.ModLoader.Config;


namespace UnclutteredProjectiles.Config {
	public class UPConfigData : ModConfig {
		public override ConfigScope Mode => ConfigScope.ServerSide;


		////

		public bool DebugModeInfo = false;


		[DefaultValue( true )]
		public bool AreFriendlyProjectilesLikelySpam = true;

		public bool AreHostileProjectilesLikelySpam = false;

		public bool AreFriendlyAndHostileProjectilesLikelySpam = false;

		[DefaultValue( true )]
		public bool AreUnfriendlyAndUnhostileProjectilesLikelySpam = true;


		[DefaultValue( true )]
		public bool UnclutterDuringBosses = true;

		[DefaultValue( true )]
		public bool UnclutterDuringInvasions = true;

		[DefaultValue( true )]
		public bool UnclutterDuringEclipses = true;


		public bool UnclutterDuringLunarApocalypse = false;

		////

		[DefaultValue( 2000 )]
		public int DustRemoveRatePerTenthOfASecond = 2000;

		[DefaultValue( 160 )]
		public int DustRemoveDistance = 160;   //10 blocks

		////

		[DefaultValue( 192 )]
		public int ProjectileDimNearBossDistance = 192;   //12 blocks (squared)

		[DefaultValue( 1536 )]
		public int ProjectileDimNearCurrentPlayerDistance = 1536; //96 blocks (squared)

		[DefaultValue( 0.8f )]
		public float ProjectileDimPercent = 0.8f;

		///

		public ISet<ProjectileDefinition> NotSpamProjectiles = new HashSet<ProjectileDefinition>();



		////////////////

		[OnDeserialized]
		internal void OnDeserializedMethod( StreamingContext context ) {
			if( this.NotSpamProjectiles != null ) {
				return;
			}

			this.NotSpamProjectiles = new HashSet<ProjectileDefinition>();
			this.NotSpamProjectiles.Add( new ProjectileDefinition( ProjectileID.CrystalVileShardHead ) );
			this.NotSpamProjectiles.Add( new ProjectileDefinition( ProjectileID.CrystalVileShardShaft ) );
			this.NotSpamProjectiles.Add( new ProjectileDefinition( ProjectileID.VilethornBase ) );
			this.NotSpamProjectiles.Add( new ProjectileDefinition( ProjectileID.VilethornTip ) );
		}
	}
}
