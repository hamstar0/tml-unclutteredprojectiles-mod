using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;


namespace UnclutteredProjectiles {
	partial class UPMod : Mod {
		public static bool IsSpamLikely() {
			var config = UPMod.Instance.Config;
			bool unclutBoss = config.UnclutterDuringBosses;
			bool unclutEclip = config.UnclutterDuringEclipses;
			bool unclutInvas = config.UnclutterDuringInvasions;
			bool unclutLunar = config.UnclutterDuringLunarApocalypse;

			return ( unclutBoss && UPNpc.IsAnyBossActive() )
				|| ( unclutEclip && Main.eclipse )
				|| ( unclutInvas && Main.invasionType != 0 )
				|| ( unclutInvas && Main.pumpkinMoon )
				|| ( unclutInvas && Main.snowMoon )
				|| ( unclutLunar && NPC.LunarApocalypseIsUp );
		}

		public static void RemoveDustsNearPosition( Vector2 position, int dustIdxStart, int dustAmount ) {
			var mymod = UPMod.Instance;
			int dustsCleaned = 0;

			int max = dustIdxStart + dustAmount;
			if( max >= Main.dust.Length ) {
				max = Main.dust.Length - 1;
			}

			for( int i = dustIdxStart; i < max; i++ ) {
				var dust = Main.dust[i];
				if( dust == null || !dust.active ) { continue; }
				
				if( Vector2.DistanceSquared(dust.position, position ) < mymod.Config.DustRemoveDistanceSquared ) {  // 8 blocks
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
