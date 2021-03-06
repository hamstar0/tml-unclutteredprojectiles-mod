﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;


namespace UnclutteredProjectiles {
	class UPNpc : GlobalNPC {
		private static ISet<int> BossWhos = new HashSet<int>();



		////////////////

		public static bool IsNearBossForProjectileDimming( Vector2 position ) {
			var mymod = UPMod.Instance;
			int projDimDist = mymod.Config.ProjectileDimNearBossDistance;
			int projDimDistSqr = projDimDist * projDimDist;

			foreach( int npcWho in UPNpc.BossWhos.ToArray() ) {
				NPC npc = Main.npc[npcWho];
				if( npc?.active != true ) {
					UPNpc.BossWhos.Remove( npcWho );
					continue;
				}

				int mydist = (int)Vector2.DistanceSquared( position, npc.position );

				if( mydist < projDimDistSqr ) {
					return true;
				}
			}

			return false;
		}

		public static bool IsAnyBossActive() {
			foreach( int npcWho in UPNpc.BossWhos.ToArray() ) {
				NPC npc = Main.npc[npcWho];
				if( npc?.active != true ) {
					UPNpc.BossWhos.Remove( npcWho );
				}
			}

			return UPNpc.BossWhos.Count > 0;
		}



		////////////////
		
		public override bool PreAI( NPC npc ) {
			if( Main.netMode != 2 ) {
				if( npc.boss ) {
					UPNpc.BossWhos.Add( npc.whoAmI );
				}
			}

			return base.PreAI( npc );
		}
	}
}
