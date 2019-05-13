using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;


namespace UnclutteredProjectiles {
	class UPPlayer : ModPlayer {
		private const int DustRemovals = 2000;



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

				UPProjectile.RemoveDustsNearProjectiles( this.DustRangeCheckIdx, UPPlayer.DustRemovals );

				this.DustRangeCheckIdx += UPPlayer.DustRemovals;
				if( this.DustRangeCheckIdx >= Main.dust.Length ) {
					this.DustRangeCheckIdx = 0;
				}
			}
		}
	}
}
