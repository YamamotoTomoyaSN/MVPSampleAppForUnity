using UnityEngine;

namespace Home {

	public interface IHomeEventDelegate {
		void GetPoketMonsterData(string idText);
	}

	public abstract class HomePresenterView : MonoBehaviour {
		public abstract void Init();
		public abstract void SetEventDelegate(IHomeEventDelegate eventDelegate);
	}

	public class HomePresenter : MonoBehaviour, IHomeEventDelegate {

		[SerializeField] private HomeView m_View = null;

		[SerializeField] private HomeModel m_Model = null;

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
		public void GetPoketMonsterData(string idText) {
			if (!int.TryParse(idText, out int id)) {
				// ���͂������e��int(ID)�ɕϊ��ł��Ȃ������ꍇ
				return;
			}
			Debug.Log("Search pokemon id:" + id);
			// Model�Ƀ|�P�������̎擾�˗�������
			StartCoroutine(m_Model.GetPoketMonsterDataAPI(id.ToString(), OnCompletedGetPoketMonsterData));
		}

		/// <summary>
		/// �|�P�������擾�������̃C�x���g�n���h���[
		/// </summary>
		/// <param name="texture">�|�P�����摜</param>
		/// <param name="name">�|�P������</param>
		private void OnCompletedGetPoketMonsterData(Texture2D texture, string name) {
			if (texture == null || name == null) {
				// �|�P�������擾���s���A�������Ȃ�
				return;
			}

			Debug.Log("aaaaa texture width:" + texture.width + " height:" + texture.height);

			// View�Ƀ|�P�����摜�̕\���˗�
			m_View.SetPokemonImage(texture);

			// View�Ƀ|�P�������̕\���˗�
			m_View.SetPokemonName(name);
		}
	}
}