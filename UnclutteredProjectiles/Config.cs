using System;
using System.Collections.Generic;
using System.ComponentModel;
using Terraria.ID;
using Terraria.ModLoader.Config;
using HamstarHelpers.Classes.UI.ModConfig;


namespace UnclutteredProjectiles.Config {
	class MyFloatInputElement : FloatInputElement { }




	public class UPConfigData : ModConfig {
		public override ConfigScope Mode => ConfigScope.ClientSide;


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

		[Range( 0, 6000 )]
		[DefaultValue( 2000 )]
		public int DustRemoveRatePerSixthOfASecond = 2000;

		[Range( 0, 10000 )]
		[DefaultValue( 160 )]
		public int DustRemoveDistance = 160;   //10 blocks

		////

		[Label( "Projectile dimming tile distance^2 near boss" )]
		[Range( 0, 50 * 50 )]
		[DefaultValue( 192 )]
		public int ProjectileDimNearBossDistance = 192;   //12 blocks (squared)

		[Label( "Projectile dimming tile distance^2 near me" )]
		[Range( 0, 192 * 192 )]
		[DefaultValue( 1536 )]
		public int ProjectileDimNearCurrentPlayerDistance = 1536; //96 blocks (squared)

		[Range( 0f, 1f )]
		[DefaultValue( 0.8f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float ProjectileDimPercent = 0.8f;

		////

		public HashSet<ProjectileDefinition> NotSpamProjectiles = new HashSet<ProjectileDefinition> {
			new ProjectileDefinition( ProjectileID.CrystalVileShardHead ),
			new ProjectileDefinition( ProjectileID.CrystalVileShardShaft ),
			new ProjectileDefinition( ProjectileID.VilethornBase ),
			new ProjectileDefinition( ProjectileID.VilethornTip )
		};



		////////////////

		public override ModConfig Clone() {
			var clone = (UPConfigData)base.Clone();

			clone.NotSpamProjectiles = new HashSet<ProjectileDefinition>( this.NotSpamProjectiles );

			return clone;
		}
	}
}
