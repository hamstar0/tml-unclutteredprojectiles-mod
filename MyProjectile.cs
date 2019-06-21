using HamstarHelpers.Helpers.ProjectileHelpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace UnclutteredProjectiles {
	class UPProjectile : GlobalProjectile {
		private static ISet<int> HiddenProjectiles = new HashSet<int>();



		////////////////
		
		public static bool IsSpamProjectile( Projectile projectile ) {
			var config = UPMod.Instance.Config;
			int projType = projectile.type;
			
			if( !UPMod.IsSpamLikely() ) {
				return false;
			}

			if( projType >= 0 ) {
				if( Main.projPet.Length > projType  && Main.projPet[ projType ] ) {
					return false;
				}
				if( ProjectileID.Sets.LightPet.Length > projType && ProjectileID.Sets.LightPet[ projType ] ) {
					return false;
				}
			}

			string projUid = ProjectileIdentityHelpers.GetProperUniqueId( projType );

			if( config.NotSpamProjectiles.Contains(projUid) ) {
				return false;
			}
			
			return ( config.AreFriendlyProjectilesLikelySpam && projectile.friendly )
				|| ( config.AreHostileProjectilesLikelySpam && projectile.hostile )
				|| ( config.AreFriendlyAndHostileProjectilesLikelySpam && projectile.friendly && projectile.hostile )
				|| ( config.AreUnfriendlyAndUnhostileProjectilesLikelySpam && !projectile.friendly && !projectile.hostile );
		}

		public static void RemoveDustsNearProjectiles( int dustStartIdx, int dustAmount ) {
			foreach( int projWho in UPProjectile.HiddenProjectiles.ToArray() ) {
				Projectile proj = Main.projectile[ projWho ];
				if( proj == null || !proj.active ) {
					UPProjectile.HiddenProjectiles.Remove( projWho );
					continue;
				}

				UPMod.RemoveDustsNearPosition( proj.position, dustStartIdx, dustAmount );
			}
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

				if( this.HidingState == 0 ) {
					if( UPPlayer.IsNearMe( projectile.position ) || UPNpc.IsNearBoss( projectile.position ) ) {
						if( this.HidePercent < 1f ) {
							if( UPMod.Instance.Config.DebugModeInfo ) {
								Main.NewText( "-hide " + projectile.Name + " " + projectile.whoAmI );
							}
							this.HidingState = 1;
						}
					} else {
						if( this.HidePercent > 0f ) {
							if( UPMod.Instance.Config.DebugModeInfo ) {
								Main.NewText( "+show " + projectile.Name + " " + projectile.whoAmI );
							}
							this.HidingState = -1;
						}
					}
				}

				if( this.HidePercent == 1f ) {
					UPProjectile.HiddenProjectiles.Add( projectile.whoAmI );
				} else {
					UPProjectile.HiddenProjectiles.Remove( projectile.whoAmI );
				}
			}

			this.UpdateHideState();

			return base.PreAI( projectile );
		}


		////////////////

		private void UpdateHideState() {
			if( this.HidingState == 1 ) {
				if( this.HidePercent < 1f ) {
					this.HidePercent += 1f / 5f;
				} else {
					this.HidePercent = 1f;
					this.HidingState = 0;
				}
			} else if( this.HidingState == -1 ) {
				if( this.HidePercent > 0f ) {
					this.HidePercent -= 1f / 5f;
				} else {
					this.HidePercent = 0f;
					this.HidingState = 0;
				}
			}
		}
	}
}
