using UnityEngine;

namespace Home {
	// HomeView����HomePresenter�Ɉ˗����鏈���̃C���^�[�t�F�[�X
	public interface IHomeEventDelegate {
		void GetPokemonData(string idText); // �|�P�������擾�˗��p���\�b�h
	}

	// HomeView�Ōp�����钊�ۃN���X
	public abstract class HomePresenterView : MonoBehaviour {
		// HomeView�̏������p�̒��ۃ��\�b�h
		public abstract void Init();
		// HomeView�ɃC���^�[�t�F�[�X�����p�����钊�ۃ��\�b�h
		public abstract void SetEventDelegate(IHomeEventDelegate eventDelegate);
	}

	public class HomePresenter : MonoBehaviour, IHomeEventDelegate {

		[SerializeField] private HomeView m_View = null;  // HomeVIew�̎Q��

		[SerializeField] private HomeModel m_Model = null;  // HomeModel�̎Q��

		void Start() {
			// HomeView�̏�����
			m_View.Init();
			// HomeView�ɃC���^�[�t�F�[�X��n��
			m_View.SetEventDelegate(this);
		}

		/// <summary>
		/// �|�P�������擾
		/// </summary>
		/// <param name="idText">�|�P����ID�̃e�L�X�g</param>
		public void GetPokemonData(string idText) {
			if (!int.TryParse(idText, out int id)) {
				// ���͂������e��int(ID)�ɕϊ��ł��Ȃ������ꍇ
				return;
			}
			Debug.Log("Search pokemon id:" + id);
			// Model�Ƀ|�P�������̎擾�˗�������
			StartCoroutine(m_Model.GetPokemonData(id.ToString(), OnCompletedGetPokemonData));
		}

		/// <summary>
		/// �|�P�������擾�������̃C�x���g�n���h���[
		/// </summary>
		/// <param name="texture">�|�P�����摜</param>
		/// <param name="name">�|�P������</param>
		private void OnCompletedGetPokemonData(Texture2D texture, string name) {
			if (texture == null || name == null) {
				// �|�P�������擾���s���A�������Ȃ�
				Debug.Log("Error: Get pokemon data");
				return;
			}
			Debug.Log("Success: Get pokemon data:" + name);
			// View�Ƀ|�P�����摜�̕\���˗�
			m_View.SetPokemonImage(texture);
			// View�Ƀ|�P�������̕\���˗�
			m_View.SetPokemonName(name);
		}
	}
}