using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace Home {
	public class HomeView : HomePresenterView {

		[SerializeField] private RectTransform m_PokemonImageArea = null; // ポケモン画像エリアオブジェクト

		[SerializeField] private RawImage m_PokemonImage = null; // ポケモン画像表示オブジェクト

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
			m_EventDelegate.GetPokemonData(idText);
		}

		/// <summary>
		/// ポケモン画像表示
		/// </summary>
		/// <param name="texture">ポケモン画像</param>
		public void SetPokemonImage(Texture2D texture) {
			// ポケモン画像のサイズを画面サイズに合った正方形に更新
			UpdatePokemonImageSquare();

			m_PokemonImage.texture = texture;
		}

		/// <summary>
		/// ポケモン名表示
		/// </summary>
		/// <param name="name">ポケモン名</param>
		public void SetPokemonName(string name) {
			m_PokemonName.text = name;
		}

		/// <summary>
		/// ポケモン画像を正方形にする
		/// </summary>
		private void UpdatePokemonImageSquare() {
			// ポケモン画像エリアの横幅、縦幅を取得
			float areaWidth = m_PokemonImageArea.sizeDelta.x;
			float areaHeight = m_PokemonImageArea.sizeDelta.y;

			// ポケモン画像サイズをそれぞれエリアサイズの横幅、縦幅の小さい方に合わせる
			float imageWidth = areaWidth > areaHeight ? areaHeight : areaWidth;
			float imageHeight = areaWidth > areaHeight ? areaHeight : areaWidth;

			// ポケモン画像サイズの変更
			m_PokemonImage.rectTransform.sizeDelta = new Vector2(imageWidth, imageHeight);
		}
	}
}
