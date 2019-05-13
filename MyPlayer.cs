using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;


namespace UnclutteredProjectiles {
	class UPPlayer : ModPlayer {
		public static bool IsNearMe( Vector2 position ) {
			int mydist = (int)Vector2.DistanceSquared( position, Main.LocalPlayer.position );

			return mydist < UPMod.GetProjectileDimNearCurrentPlayerDistanceSquared();
		}



		////////////////

		private int Timer = 0;

		private int DustRangeCheckIdx = 0;

		
		////////////////

		public override bool CloneNewInstances => false;


		////////////////

		public override void PreUpdate() {
			if( this.player.whoAmI != Main.myPlayer ) {
				return;
			}

			if( ++this.Timer >= 10 ) {
				this.Timer = 0;

				int dustsGone = UPMod.GetDustRemoveRate();

				UPProjectile.RemoveDustsNearProjectiles( this.DustRangeCheckIdx, dustsGone );

				this.DustRangeCheckIdx += dustsGone;
				if( this.DustRangeCheckIdx >= Main.dust.Length ) {
					this.DustRangeCheckIdx = 0;
				}
			}
		}
	}
}
