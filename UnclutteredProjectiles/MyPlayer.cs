using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;


namespace UnclutteredProjectiles {
	class UPPlayer : ModPlayer {
		public static bool IsNearMe( Vector2 position ) {
			var mymod = UPMod.Instance;
			int mydist = (int)Vector2.DistanceSquared( position, Main.LocalPlayer.position );
			int projDimDistSqr = mymod.Config.ProjectileDimNearCurrentPlayerDistance * mymod.Config.ProjectileDimNearCurrentPlayerDistance;

			return mydist < projDimDistSqr;
		}



		////////////////

		private int Timer = 0;

		private int DustRangeCheckIdx = 0;

		
		////////////////

		public override bool CloneNewInstances => false;



		////////////////

		public override void SyncPlayer( int toWho, int fromWho, bool newPlayer ) {
			if( Main.netMode == 2 ) {
				if( toWho == -1 && fromWho == this.player.whoAmI ) {
					this.OnConnectServer();
				}
			}
		}

		public override void OnEnterWorld( Player player ) {
			if( player.whoAmI != Main.myPlayer ) { return; }
			if( this.player.whoAmI != Main.myPlayer ) { return; }

			var mymod = (UPMod)this.mod;

			if( Main.netMode == 0 ) {
				this.OnConnectSingle();
			} else if( Main.netMode == 1 ) {
				this.OnConnectClient();
			}
		}


		////////////////

		private void OnConnectSingle() {
		}

		private void OnConnectClient() {
			//PacketProtocolRequestToServer.QuickRequest<ModSettingsProtocol>( -1 );
		}

		private void OnConnectServer() {
		}


		////////////////

		public override void PreUpdate() {
			if( Main.netMode == 2 ) { return; }
			if( this.player.whoAmI != Main.myPlayer ) { return; }
			if( this.player.dead ) { return; }

			if( ++this.Timer >= 10 ) {
				this.Timer = 0;

				int dustsGone = UPMod.Instance.Config.DustRemoveRatePerTenthOfASecond;

				UPProjectile.RemoveDustsNearProjectiles( this.DustRangeCheckIdx, dustsGone );

				this.DustRangeCheckIdx += dustsGone;
				if( this.DustRangeCheckIdx >= Main.dust.Length ) {
					this.DustRangeCheckIdx = 0;
				}
			}
		}
	}
}
