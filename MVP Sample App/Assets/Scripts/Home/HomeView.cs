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

		/// <summary>
		/// Presenter����View�̏������������Ăяo�����
		/// </summary>
		public override void Init() {
			m_SearchButton.onClick.AddListener(OnSearchButton);
		}

		/// <summary>
		/// Presenter����EventDelegate�̓o�^�������Ăяo�����
		/// </summary>
		/// <param name="eventDelegate">Presenter�ɏ������˗����邽�߂̃C���^�[�t�F�[�X</param>
		public override void SetEventDelegate(IHomeEventDelegate eventDelegate) {
			m_EventDelegate = eventDelegate;
		}

		/// <summary>
		/// �����{�^���̃C�x���g�n���h���[
		/// </summary>
		private void OnSearchButton() {
			string idText = m_PokemonIdInputField.text;
			// Presenter�Ƀ|�P�������̎擾�˗�
			m_EventDelegate.GetPoketMonsterData(idText);
		}

		/// <summary>
		/// �|�P�����摜�\��
		/// </summary>
		/// <param name="texture">�|�P�����摜</param>
		public void SetPokemonImage(Texture2D texture) {
			m_PokemonImange.texture = texture;
		}

		/// <summary>
		/// �|�P�������\��
		/// </summary>
		/// <param name="name">�|�P������</param>
		public void SetPokemonName(string name) {
			m_PokemonName.text = name;
		}
	}
}
