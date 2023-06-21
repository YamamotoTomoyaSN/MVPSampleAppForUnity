using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace Home {
	public class HomeView : HomePresenterView {

		[SerializeField] private RectTransform m_PokemonImageArea = null; // �|�P�����摜�G���A�I�u�W�F�N�g

		[SerializeField] private RawImage m_PokemonImage = null; // �|�P�����摜�\���I�u�W�F�N�g

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
			m_EventDelegate.GetPokemonData(idText);
		}

		/// <summary>
		/// �|�P�����摜�\��
		/// </summary>
		/// <param name="texture">�|�P�����摜</param>
		public void SetPokemonImage(Texture2D texture) {
			// �|�P�����摜�̃T�C�Y����ʃT�C�Y�ɍ����������`�ɍX�V
			UpdatePokemonImageSquare();

			m_PokemonImage.texture = texture;
		}

		/// <summary>
		/// �|�P�������\��
		/// </summary>
		/// <param name="name">�|�P������</param>
		public void SetPokemonName(string name) {
			m_PokemonName.text = name;
		}

		/// <summary>
		/// �|�P�����摜�𐳕��`�ɂ���
		/// </summary>
		private void UpdatePokemonImageSquare() {
			// �|�P�����摜�G���A�̉����A�c�����擾
			float areaWidth = m_PokemonImageArea.sizeDelta.x;
			float areaHeight = m_PokemonImageArea.sizeDelta.y;

			// �|�P�����摜�T�C�Y�����ꂼ��G���A�T�C�Y�̉����A�c���̏��������ɍ��킹��
			float imageWidth = areaWidth > areaHeight ? areaHeight : areaWidth;
			float imageHeight = areaWidth > areaHeight ? areaHeight : areaWidth;

			// �|�P�����摜�T�C�Y�̕ύX
			m_PokemonImage.rectTransform.sizeDelta = new Vector2(imageWidth, imageHeight);
		}
	}
}
