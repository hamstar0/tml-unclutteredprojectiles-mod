using HamstarHelpers.Components.Network;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using UnclutteredProjectiles.NetProtocols;


namespace UnclutteredProjectiles {
	class UPPlayer : ModPlayer {
		public static bool IsNearMe( Vector2 position ) {
			int mydist = (int)Vector2.DistanceSquared( position, Main.LocalPlayer.position );

			return mydist < UPMod.Instance.Config.ProjectileDimNearCurrentPlayerDistanceSquared;
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
			PacketProtocolRequestToServer.QuickRequest<ModSettingsProtocol>( -1 );
		}

		private void OnConnectServer() {
		}


		////////////////

		public override void PreUpdate() {
			if( this.player.whoAmI != Main.myPlayer ) { return; }
			if( this.player.dead ) { return; }

			if( ++this.Timer >= 10 ) {
				this.Timer = 0;

				int dustsGone = UPMod.Instance.Config.DustRemoveRate;

				UPProjectile.RemoveDustsNearProjectiles( this.DustRangeCheckIdx, dustsGone );

				this.DustRangeCheckIdx += dustsGone;
				if( this.DustRangeCheckIdx >= Main.dust.Length ) {
					this.DustRangeCheckIdx = 0;
				}
			}
		}
	}
}
