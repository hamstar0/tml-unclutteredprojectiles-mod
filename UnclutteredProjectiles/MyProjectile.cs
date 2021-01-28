using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;


namespace UnclutteredProjectiles {
	partial class UPProjectile : GlobalProjectile {
		private static ISet<int> HiddenProjectiles = new HashSet<int>();



		////////////////

		public static bool IsSpamProjectile( Projectile projectile ) {
			var config = UPMod.Instance.Config;
			int projType = projectile.type;
			
			if( !UPMod.AreSpamProjectileLikelyToExist() ) {
				return false;
			}

			if( projType >= 0 ) {
				if( Main.projPet.Length > projType  && Main.projPet[ projType ] ) {
					return false;
				}
				if( ProjectileID.Sets.LightPet.Length > projType && ProjectileID.Sets.LightPet[projType] ) {
					return false;
				}
			}

			var projDef = new ProjectileDefinition( projType );

			if( config.NotSpamProjectiles.Contains(projDef) ) {
				return false;
			}
			
			return ( config.AreFriendlyProjectilesLikelySpam && projectile.friendly )
				|| ( config.AreHostileProjectilesLikelySpam && projectile.hostile )
				|| ( config.AreFriendlyAndHostileProjectilesLikelySpam && projectile.friendly && projectile.hostile )
				|| ( config.AreUnfriendlyAndUnhostileProjectilesLikelySpam && !projectile.friendly && !projectile.hostile );
		}



		////////////////

		public int Timer = 9999;
		public int HidingState = 0;
		public float HidePercent = 0f;


		////////////////

		public override bool CloneNewInstances => true;
		public override bool InstancePerEntity => true;



		////////////////

		public override bool PreDraw( Projectile projectile, SpriteBatch spriteBatch, Color lightColor ) {
			if( this.HidePercent > 0f ) {
				float percent = this.HidePercent * UPMod.Instance.Config.ProjectileDimPercent;

				projectile.alpha = (int)( percent * 255f );
			}
			return base.PreDraw( projectile, spriteBatch, lightColor );
		}

		////////////////

		public override bool PreAI( Projectile projectile ) {
			if( Main.netMode == 2 ) {
				return base.PreAI( projectile );
			}
			if( !UPProjectile.IsSpamProjectile(projectile) ) {
				return base.PreAI( projectile );
			}

			if( ++this.Timer > 5 ) {
				this.Timer = 0;

				this.UpdateHideState( projectile );
			}

			this.UpdateHideAmount();

			return base.PreAI( projectile );
		}
	}
}
