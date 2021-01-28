using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.World;


namespace UnclutteredProjectiles {
	partial class UPMod : Mod {
		public static bool IsNearMeForProjectileDimming( Vector2 position ) {
			var mymod = UPMod.Instance;
			int mydistSqr = (int)Vector2.DistanceSquared( position, Main.LocalPlayer.position );
			int projDimDist = mymod.Config.ProjectileDimNearCurrentPlayerDistance;
			int projDimDistSqr = projDimDist * projDimDist;

			return mydistSqr < projDimDistSqr;
		}


		////////////////

		public static bool AreSpamProjectileLikelyToExist() {
			var config = UPMod.Instance.Config;
			bool unclutBoss = config.UnclutterDuringBosses;
			bool unclutEclip = config.UnclutterDuringEclipses;
			bool unclutInvas = config.UnclutterDuringInvasions;
			bool unclutLunar = config.UnclutterDuringLunarApocalypse;
			bool isBossActive = UPNpc.IsAnyBossActive();

			if( unclutBoss && !isBossActive ) {	// No boss active?
				if( !WorldHelpers.IsAboveWorldSurface( Main.LocalPlayer.position ) ) {	// Not above world surface?
					return false;
				}
			}

			return ( unclutBoss && isBossActive )
				|| ( unclutEclip && Main.eclipse )
				|| ( unclutInvas && Main.invasionType != 0 )
				|| ( unclutInvas && Main.pumpkinMoon )
				|| ( unclutInvas && Main.snowMoon )
				|| ( unclutLunar && NPC.LunarApocalypseIsUp );
		}


		////////////////

		public static void RemoveDustsNearPosition( Vector2 position, int dustIdxStart, int dustAmount ) {
			var mymod = UPMod.Instance;
			int dustRemoveDistSqr = mymod.Config.DustRemoveDistance * mymod.Config.DustRemoveDistance;
			int dustsCleaned = 0;

			int max = dustIdxStart + dustAmount;
			if( max >= Main.dust.Length ) {
				max = Main.dust.Length - 1;
			}

			for( int i = dustIdxStart; i < max; i++ ) {
				var dust = Main.dust[i];
				if( dust == null || !dust.active ) { continue; }
				
				if( Vector2.DistanceSquared(dust.position, position ) < dustRemoveDistSqr ) {  // 8 blocks
					Main.dust[i] = new Dust();
					dustsCleaned++;
				}
			}

			if( mymod.Config.DebugModeInfo && dustsCleaned > 0 ) {
				Main.NewText( "dusts cleared: " + dustsCleaned );
			}
		}
	}
}
