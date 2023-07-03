using UnityEngine;

namespace Home {
	// HomeViewからHomePresenterに依頼する処理のインターフェース
	public interface IHomeEventDelegate {
		void GetPokemonData(string idText); // ポケモン情報取得依頼用メソッド
	}

	// HomeViewで継承する抽象クラス
	public abstract class HomePresenterView : MonoBehaviour {
		// HomeViewの初期化用の抽象メソッド
		public abstract void Init();
		// HomeViewにインターフェースを引継ぎする抽象メソッド
		public abstract void SetEventDelegate(IHomeEventDelegate eventDelegate);
	}

	public class HomePresenter : MonoBehaviour, IHomeEventDelegate {

		[SerializeField] private HomeView m_View = null;  // HomeVIewの参照

		[SerializeField] private HomeModel m_Model = null;  // HomeModelの参照

		void Start() {
			// HomeViewの初期化
			m_View.Init();
			// HomeViewにインターフェースを渡す
			m_View.SetEventDelegate(this);
		}

		/// <summary>
		/// ポケモン情報取得
		/// </summary>
		/// <param name="idText">ポケモンIDのテキスト</param>
		public void GetPokemonData(string idText) {
			if (!int.TryParse(idText, out int id)) {
				// 入力した内容がint(ID)に変換できなかった場合
				return;
			}
			Debug.Log("Search pokemon id:" + id);
			// Modelにポケモン情報の取得依頼をする
			StartCoroutine(m_Model.GetPokemonData(id.ToString(), OnCompletedGetPokemonData));
		}

		/// <summary>
		/// ポケモン情報取得完了時のイベントハンドラー
		/// </summary>
		/// <param name="texture">ポケモン画像</param>
		/// <param name="name">ポケモン名</param>
		private void OnCompletedGetPokemonData(Texture2D texture, string name) {
			if (texture == null || name == null) {
				// ポケモン情報取得失敗時、何もしない
				Debug.Log("Error: Get pokemon data");
				return;
			}
			Debug.Log("Success: Get pokemon data:" + name);
			// Viewにポケモン画像の表示依頼
			m_View.SetPokemonImage(texture);
			// Viewにポケモン名の表示依頼
			m_View.SetPokemonName(name);
		}
	}
}