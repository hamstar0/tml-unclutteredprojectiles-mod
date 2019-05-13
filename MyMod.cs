using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;


namespace UnclutteredProjectiles {
	partial class UPMod : Mod {
		public static UPMod Instance { get; private set; }



		////////////////
		
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
				
				if( Vector2.DistanceSquared(dust.position, position ) < UPMod.GetDustRemoveDistanceSquared() ) {  // 8 blocks
					Main.dust[i] = new Dust();
					dustsCleaned++;
				}
			}

			if( UPMod.IsDebugModeInfo() && dustsCleaned > 0 ) {
				Main.NewText( "dusts cleared: " + dustsCleaned );
			}
		}



		////////////////

		public UPMod() {
			UPMod.Instance = this;
		}

		public override void Unload() {
			UPMod.Instance = null;
		}
	}
}