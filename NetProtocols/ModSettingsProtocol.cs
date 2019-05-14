using HamstarHelpers.Components.Network;
using UnclutteredProjectiles.Config;


namespace UnclutteredProjectiles.NetProtocols {
	class ModSettingsProtocol : PacketProtocolRequestToServer {
		public UPConfigData ModSettings;



		////////////////

		private ModSettingsProtocol() { }


		////////////////

		protected override void InitializeServerSendData( int who ) {
			this.ModSettings = UPMod.Instance.Config;
		}

		////////////////
		
		protected override void ReceiveReply() {
			UPMod.Instance.ConfigJson.SetData( this.ModSettings );
		}
	}
}
