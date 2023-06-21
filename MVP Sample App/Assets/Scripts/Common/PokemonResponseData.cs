using System;
using System.Collections.Generic;

namespace ResponseData {
	public class PokemonResponseData {

		/**
		 * �|�P�������擾API�̃��X�|���X����f�[�^���擾����p�̍\����
		 */
		[Serializable]
		public struct GetPokemonAPIResponseData {

			public Sprites sprites;
			public Species species;

			[Serializable]
			public struct Sprites {
				public string front_default; // �|�P�����̃~�j�摜�擾�p��URL(���̃A�v���ł͕s�g�p)
			}

			[Serializable]
			public struct Species {
				public string url; // �|�P�������擾�p��URL
			}
		}

		/**
		 * �|�P�������擾API�̃��X�|���X����f�[�^���擾����p�̍\����
		 */
		[Serializable]
		public struct GetPokemonNameAPIResponseData {

			public List<Names> names;

			[Serializable]
			public struct Names {
				public Language language; // ���O�̌���
				public string name; // �|�P������(����ɂ���ĕς��)
			}

			[Serializable]
			public struct Language {
				public string name; // ����̖��O(��Fja)
			}
		}
	}
}
