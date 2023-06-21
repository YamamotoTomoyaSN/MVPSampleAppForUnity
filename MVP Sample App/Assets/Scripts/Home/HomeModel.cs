using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using static ResponseData.PokemonResponseData;

namespace Home {
	public class HomeModel : MonoBehaviour {

		// ポケモン情報取得APIのURL
		private const string GET_POKEMON_DATA_API_BASE_URL = "https://pokeapi.co/api/v2/pokemon/{0}";

		// ポケモン画像取得APIのURL
		// (基本的にはポケモン情報取得APIのレスポンスから取得するURLだが、ハイフンの構造体が作成できないので直書きのURLとポケモンIDでURLを生成する)
		private const string GET_POKEMON_IMAGE_API_BASE_URL = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/{0}.png";

		/// <summary>
		/// ポケモン情報を取得するAPI通信
		/// </summary>
		/// <param name="id">ポケモンID</param>
		/// <param name="callback">ポケモン画像と名前を返却</param>
		/// <returns></returns>
		public IEnumerator GetPokemonData(string id, Action<Texture2D, String> callback) {
			string url = string.Format(GET_POKEMON_DATA_API_BASE_URL, id);

			using UnityWebRequest request = UnityWebRequest.Get(url);

			// ポケモン情報取得APIリクエスト
			yield return request.SendWebRequest();

			if (request.result == UnityWebRequest.Result.Success) {
				// 成功時、レスポンスの文字列からJsonを取得
				GetPokemonAPIResponseData data = JsonUtility.FromJson<GetPokemonAPIResponseData>(request.downloadHandler.text);

				// ポケモン画像を取得する
				StartCoroutine(GetPokemonTexture(id, (texture) => {
					if (texture) {
						// 成功時、ポケモン名を取得する
						StartCoroutine(GetPokemonName(data.species.url, (name) => {
							// ポケモン画像と名前をコールバック
							callback(texture, name);
						}));
					} else {
						// エラー時
						callback(null, null);
					}
				}));
			} else {
				// エラー時
				ErrorProcess(request, "GetPokemonData API");
				callback(null, null);
			}
		}

		/// <summary>
		/// ポケモン画像を取得するAPI通信
		/// </summary>
		/// <param name="id">ポケモンID</param>
		/// <param name="callback">ポケモン画像を返却</param>
		/// <returns></returns>
		private IEnumerator GetPokemonTexture(string id, Action<Texture2D> callback) {
			string url = string.Format(GET_POKEMON_IMAGE_API_BASE_URL, id);
			Uri uri = new(url);

			using UnityWebRequest request = UnityWebRequestTexture.GetTexture(uri);

			// ポケモン画像取得APIリクエスト
			yield return request.SendWebRequest();

			if (request.result == UnityWebRequest.Result.Success) {
				// 成功時、ポケモン画像のテクスチャを返却
				var texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
				callback.Invoke(texture);
			} else {
				// エラー時
				ErrorProcess(request, "GetPokemonTexture API");
				callback(null);
			}
		}

		/// <summary>
		/// ポケモン名を取得するAPI通信
		/// </summary>
		/// <param name="url">ポケモン名取得APIのURL</param>
		/// <param name="callback">ポケモン名を返却</param>
		/// <returns></returns>
		private IEnumerator GetPokemonName(string url, Action<string> callback) {
			using UnityWebRequest request = UnityWebRequest.Get(url);

			// ポケモン名取得APIリクエスト
			yield return request.SendWebRequest();

			if (request.result == UnityWebRequest.Result.Success) {
				// 成功時、レスポンスの文字列からJsonを取得
				GetPokemonNameAPIResponseData data = JsonUtility.FromJson<GetPokemonNameAPIResponseData>(request.downloadHandler.text);

				// 日本語のポケモン名を取得する
				GetPokemonNameAPIResponseData.Names name = data.names.Find((value) => value.language.name == "ja");

				// ポケモン名を返却
				callback(name.name);
			} else {
				// エラー時
				ErrorProcess(request, "GetPokemonName API");
				callback(null);
			}
		}

		/// <summary>
		/// エラー処理
		/// </summary>
		/// <param name="request">リクエスト情報</param>
		private void ErrorProcess(UnityWebRequest request, string apiName) {
			switch (request.result) {
				case UnityWebRequest.Result.InProgress:
					// リクエスト中
					Debug.Log(apiName + ": InProgress");
					break;
				case UnityWebRequest.Result.ProtocolError:
					Debug.Log(apiName + ": ProtocolError:" + request.error);
					break;
				case UnityWebRequest.Result.DataProcessingError:
					Debug.Log(apiName + ": DataProcessingError:" + request.error);
					break;
				case UnityWebRequest.Result.ConnectionError:
					Debug.Log(apiName + ": ConnectionError:" + request.error);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
