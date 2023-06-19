using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResponseData {
	public class PokemonResponseData {

		[Serializable]
		public struct PokemonAPIResponseData {

			public Sprites sprites;

			[Serializable]
			public struct Sprites {
				public string front_default;
			}
		}
	}
}
