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
			m_View.Init();

			m_View.SetEventDelegate(this);
		}

		public void GetPoketMonsterData(string idText) {
			if (!int.TryParse(idText, out int id)) {
				// ���͂������e��ID(int)�ɕϊ��ł��Ȃ������ꍇ
				return;
			}
			Debug.Log("Search pokemon id:" + id);
			// Model�Ƀ|�P�������̎擾�˗�������
			StartCoroutine(m_Model.GetPoketMonsterDataAPI(id.ToString(), OnCompletedGetPoketMonsterData));
		}

		private void OnCompletedGetPoketMonsterData(Texture2D texture, string name) {
			// View�Ƀ|�P�����e�N�X�`���̕\���˗�
			m_View.SetPokemonImage(texture);

			// View�Ƀ|�P�������̕\���˗�
			m_View.SetPokemonName(name);
		}
	}
}