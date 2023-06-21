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

		/// <summary>
		/// Presenter側でViewの初期化処理が呼び出される
		/// </summary>
		public override void Init() {
			m_SearchButton.onClick.AddListener(OnSearchButton);
		}

		/// <summary>
		/// Presenter側でEventDelegateの登録処理が呼び出される
		/// </summary>
		/// <param name="eventDelegate">Presenterに処理を依頼するためのインターフェース</param>
		public override void SetEventDelegate(IHomeEventDelegate eventDelegate) {
			m_EventDelegate = eventDelegate;
		}

		/// <summary>
		/// 検索ボタンのイベントハンドラー
		/// </summary>
		private void OnSearchButton() {
			string idText = m_PokemonIdInputField.text;
			// Presenterにポケモン情報の取得依頼
			m_EventDelegate.GetPoketMonsterData(idText);
		}

		/// <summary>
		/// ポケモン画像表示
		/// </summary>
		/// <param name="texture">ポケモン画像</param>
		public void SetPokemonImage(Texture2D texture) {
			m_PokemonImange.texture = texture;
		}

		/// <summary>
		/// ポケモン名表示
		/// </summary>
		/// <param name="name">ポケモン名</param>
		public void SetPokemonName(string name) {
			m_PokemonName.text = name;
		}
	}
}
