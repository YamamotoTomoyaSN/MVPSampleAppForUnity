using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Home {
	public class HomeView : HomePresenterView {

		[SerializeField] private RawImage m_PokemonImange = null; // �|�P�����摜�\���I�u�W�F�N�g

		[SerializeField] private TMP_Text m_PokemonName = null; // �|�P�������\���I�u�W�F�N�g

		[SerializeField] private TMP_InputField m_PokemonIdInputField = null; // �|�P����ID���̓I�u�W�F�N�g

		[SerializeField] private Button m_SearchButton = null; // �����{�^���I�u�W�F�N�g

		private IHomeEventDelegate m_EventDelegate;

		/**
		 * Presenter����View�̏������������Ăяo�����
		 */
		public override void Init() {
			m_SearchButton.onClick.AddListener(OnSearchButton);
		}

		/**
		 * Presenter����EventDelegate�̓o�^�������Ăяo�����
		 */
		public override void SetEventDelegate(IHomeEventDelegate eventDelegate) {
			// Presenter�ɏ������˗����邽�߂̃C���^�[�t�F�[�X��ϐ��Ɋi�[
			m_EventDelegate = eventDelegate;
		}

		/**
		 * �����{�^���C�x���g�n���h���[
		 */
		private void OnSearchButton() {
			string idText = m_PokemonIdInputField.text;
			// Presenter�Ƀ|�P�������̎擾�˗�
			m_EventDelegate.GetPoketMonsterData(idText);
		}

		/**
		 * �|�P�����摜�\��
		 */
		public void SetPokemonImage(Texture2D texture) {
			m_PokemonImange.texture = texture;
		}

		/**
		 * �|�P�������\��
		 */
		public void SetPokemonName(string name) {
			m_PokemonName.text = name;
		}
	}
}
