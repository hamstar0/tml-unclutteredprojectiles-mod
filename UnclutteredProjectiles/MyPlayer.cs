using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;


namespace UnclutteredProjectiles {
	class UPPlayer : ModPlayer {
		private int Timer = 0;

		private int DustRangeCheckIdx = 0;

		
		////////////////

		public override bool CloneNewInstances => false;



		////////////////

		public override void PreUpdate() {
			if( Main.netMode == 2 ) { return; }
			if( this.player.whoAmI != Main.myPlayer ) { return; }
			if( this.player.dead ) { return; }

			if( ++this.Timer >= 10 ) {
				this.Timer = 0;

				this.CleanupNextBatchOfHiddenDusts();
			}
		}


		////////////////

		private void CleanupNextBatchOfHiddenDusts() {
			int dustsGone = UPMod.Instance.Config.DustRemoveRatePerSixthOfASecond;

			UPProjectile.RemoveDustsNearHiddenProjectiles( this.DustRangeCheckIdx, dustsGone );

			this.DustRangeCheckIdx += dustsGone;
			if( this.DustRangeCheckIdx >= Main.dust.Length ) {
				this.DustRangeCheckIdx = 0;
			}
		}
	}
}
