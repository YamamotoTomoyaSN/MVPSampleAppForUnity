using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Home {
	public class HomeView : HomePresenterView {

		[SerializeField] private RawImage m_PokemonImange = null; // ポケモン画像表示オブジェクト

		[SerializeField] private TMP_Text m_PokemonName = null; // ポケモン名表示オブジェクト

		[SerializeField] private TMP_InputField m_PokemonIdInputField = null; // ポケモンID入力オブジェクト

		[SerializeField] private Button m_SearchButton = null; // 検索ボタンオブジェクト

		private IHomeEventDelegate m_EventDelegate;

		/**
		 * Presenter側でViewの初期化処理が呼び出される
		 */
		public override void Init() {
			m_SearchButton.onClick.AddListener(OnSearchButton);
		}

		/**
		 * Presenter側でEventDelegateの登録処理が呼び出される
		 */
		public override void SetEventDelegate(IHomeEventDelegate eventDelegate) {
			// Presenterに処理を依頼するためのインターフェースを変数に格納
			m_EventDelegate = eventDelegate;
		}

		/**
		 * 検索ボタンイベントハンドラー
		 */
		private void OnSearchButton() {
			string idText = m_PokemonIdInputField.text;
			// Presenterにポケモン情報の取得依頼
			m_EventDelegate.GetPoketMonsterData(idText);
		}

		/**
		 * ポケモン画像表示
		 */
		public void SetPokemonImage(Texture2D texture) {
			m_PokemonImange.texture = texture;
		}

		/**
		 * ポケモン名表示
		 */
		public void SetPokemonName(string name) {
			m_PokemonName.text = name;
		}
	}
}
