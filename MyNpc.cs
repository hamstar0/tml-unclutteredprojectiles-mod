using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;


namespace UnclutteredProjectiles {
	class UPNpc : GlobalNPC {
		private static ISet<int> BossWhos = new HashSet<int>();



		////////////////

		public static bool IsAnyBossActive() {
			foreach( int npcWho in UPNpc.BossWhos.ToArray() ) {
				NPC npc = Main.npc[npcWho];
				if( npc == null || !npc.active ) {
					UPNpc.BossWhos.Remove( npcWho );
				}
			}

			return UPNpc.BossWhos.Count > 0;
		}

		public static bool IsNearBoss( Vector2 position ) {
			var mymod = UPMod.Instance;

			foreach( int npcWho in UPNpc.BossWhos.ToArray() ) {
				NPC npc = Main.npc[npcWho];
				if( npc == null || !npc.active ) {
					UPNpc.BossWhos.Remove( npcWho );
					continue;
				}

				int mydist = (int)Vector2.DistanceSquared( position, npc.position );

				if( mydist < mymod.Config.GetProjectileDimNearBossDistanceSquared ) {
					return true;
				}
			}
			return false;
		}



		////////////////
		
		public override bool PreAI( NPC npc ) {
			if( npc.boss ) {
				UPNpc.BossWhos.Add( npc.whoAmI );
			}

			return base.PreAI( npc );
		}
	}
}
