using System;
using System.Collections.Generic;

namespace ResponseData {
	public class PokemonResponseData {

		[Serializable]
		public struct GetPokemonAPIResponseData {

			public Sprites sprites;
			public Species species;

			[Serializable]
			public struct Sprites {
				public string front_default;
			}

			[Serializable]
			public struct Species {
				public string url;
			}
		}

		[Serializable]
		public struct GetPokemonNameAPIResponseData {

			public List<Names> names;

			[Serializable]
			public struct Names {
				public Language language;
				public string name;
			}

			[Serializable]
			public struct Language {
				public string name;
			}
		}
	}
}
