using System;
using System.Collections.Generic;

namespace ResponseData {
	public class PokemonResponseData {

		/**
		 * ポケモン情報取得APIのレスポンスからデータを取得する用の構造体
		 */
		[Serializable]
		public struct GetPokemonAPIResponseData {

			public Sprites sprites;
			public Species species;

			[Serializable]
			public struct Sprites {
				public string front_default; // ポケモンのミニ画像取得用のURL(このアプリでは不使用)
			}

			[Serializable]
			public struct Species {
				public string url; // ポケモン名取得用のURL
			}
		}

		/**
		 * ポケモン名取得APIのレスポンスからデータを取得する用の構造体
		 */
		[Serializable]
		public struct GetPokemonNameAPIResponseData {

			public List<Names> names;

			[Serializable]
			public struct Names {
				public Language language; // 名前の言語
				public string name; // ポケモン名(言語によって変わる)
			}

			[Serializable]
			public struct Language {
				public string name; // 言語の名前(例：ja)
			}
		}
	}
}
