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
				// 入力した内容がID(int)に変換できなかった場合
				return;
			}
			Debug.Log("Search pokemon id:" + id);
			// Modelにポケモン情報の取得依頼をする
			StartCoroutine(m_Model.GetPoketMonsterDataAPI(id.ToString(), OnCompletedGetPoketMonsterData));
		}

		private void OnCompletedGetPoketMonsterData(Texture2D texture, string name) {
			// Viewにポケモンテクスチャの表示依頼
			m_View.SetPokemonImage(texture);

			// Viewにポケモン名の表示依頼
			m_View.SetPokemonName(name);
		}
	}
}