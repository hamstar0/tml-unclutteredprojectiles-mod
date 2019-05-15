using HamstarHelpers.Components.Config;
using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using UnclutteredProjectiles.Config;


namespace UnclutteredProjectiles {
	partial class UPMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-unclutteredprojectiles-mod";

		public static string ConfigFileRelativePath {
			get { return ConfigurationDataBase.RelativePath + Path.DirectorySeparatorChar + UPConfigData.ConfigFileName; }
		}
		public static void ReloadConfigFromFile() {
			if( Main.netMode != 0 ) {
				throw new Exception( "Cannot reload configs outside of single player." );
			}
			if( !UPMod.Instance.ConfigJson.LoadFile() ) {
				UPMod.Instance.ConfigJson.SaveFile();
			}
		}

		public static void ResetConfigFromDefaults() {
			if( Main.netMode != 0 ) {
				throw new Exception( "Cannot reset to default configs outside of single player." );
			}

			var configData = new UPConfigData();
			configData.SetDefaults();

			UPMod.Instance.ConfigJson.SetData( configData );
			UPMod.Instance.ConfigJson.SaveFile();
		}
	}
}
