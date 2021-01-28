using System.Linq;
using Terraria;
using Terraria.ModLoader;


namespace UnclutteredProjectiles {
	partial class UPProjectile : GlobalProjectile {
		public static void RemoveDustsNearHiddenProjectiles( int dustStartIdx, int dustAmount ) {
			foreach( int projWho in UPProjectile.HiddenProjectiles.ToArray() ) {
				Projectile proj = Main.projectile[projWho];
				if( proj == null || !proj.active ) {
					UPProjectile.HiddenProjectiles.Remove( projWho );
					continue;
				}

				UPMod.RemoveDustsNearPosition( proj.position, dustStartIdx, dustAmount );
			}
		}



		////////////////

		private void UpdateHideState( Projectile projectile ) {
			if( this.HidingState == 0 ) {
				bool isNearMe = UPMod.IsNearMeForProjectileDimming( projectile.position );
				bool isNearBoss = UPNpc.IsNearBossForProjectileDimming( projectile.position );

				if( isNearMe || isNearBoss ) {
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

			if( this.HidePercent >= 1f ) {
				UPProjectile.HiddenProjectiles.Add( projectile.whoAmI );
			} else {
				UPProjectile.HiddenProjectiles.Remove( projectile.whoAmI );
			}
		}

		private void UpdateHideAmount() {
			if( this.HidingState == 1 ) {
				if( this.HidePercent < 1f ) {
					this.HidePercent += 1f / 5f;
				}
				if( this.HidePercent >= 1f ) {
					this.HidePercent = 1f;
					this.HidingState = 0;
				}
			} else if( this.HidingState == -1 ) {
				if( this.HidePercent > 0f ) {
					this.HidePercent -= 1f / 5f;
				}
				if( this.HidePercent <= 0f ) {
					this.HidePercent = 0f;
					this.HidingState = 0;
				}
			}
		}
	}
}
