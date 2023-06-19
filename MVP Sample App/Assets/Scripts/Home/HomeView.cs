using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Home {
	public class HomeView : HomePresenterView {

		[SerializeField]
		private RawImage m_PokemonImange = null;

		[SerializeField]
		private TMP_Text m_PokemonName = null;

		[SerializeField]
		private TMP_InputField m_PokemonIdInputField = null;

		[SerializeField]
		private Button m_SearchButton = null;

		private IHomeEventDelegate m_EventDelegate;

		public override void Init() {
			// Presenter����View�̏������������Ăяo�����
			m_SearchButton.onClick.AddListener(OnSearchButton);
		}

		public override void SetEventDelegate(IHomeEventDelegate eventDelegate) {
			// Presenter����EventDelegate�̓o�^�������Ăяo�����

			// Presenter�ɏ������˗����邽�߂̃C���^�[�t�F�[�X��ϐ��Ɋi�[
			m_EventDelegate = eventDelegate;
		}

		private void OnSearchButton() {
			// �����{�^��������
			string id = m_PokemonIdInputField.text;
			m_EventDelegate.GetPoketMonsterData(id);
		}

		public void SetPokemonImage(Texture2D texture) {
			m_PokemonImange.texture = texture;
		}

		public void SetPokemonName(string name) {
			m_PokemonName.text = name;
		}
	}
}
