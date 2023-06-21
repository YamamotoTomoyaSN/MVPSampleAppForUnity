using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using static ResponseData.PokemonResponseData;

namespace Home {
	public class HomeModel : MonoBehaviour {

		private const string GET_POKEMON_DATA_API_BASE_URL = "https://pokeapi.co/api/v2/pokemon/";
		private const string GET_POKEMON_IMAGE_API_BASE_URL = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/{0}.png";

		/// <summary>
		/// ポケモン情報を取得するAPI通信
		/// </summary>
		/// <param name="id">ポケモンID</param>
		/// <param name="callback">ポケモン画像と名前を返却</param>
		/// <returns></returns>
		public IEnumerator GetPoketMonsterDataAPI(string id, Action<Texture2D, String> callback) {
			using UnityWebRequest request = UnityWebRequest.Get(GET_POKEMON_DATA_API_BASE_URL + id);

			// ポケモン情報取得APIリクエスト
			yield return request.SendWebRequest();

			if (request.result == UnityWebRequest.Result.Success) {
				// 成功時
				GetPokemonAPIResponseData data = JsonUtility.FromJson<GetPokemonAPIResponseData>(request.downloadHandler.text); // 文字列からJsonを取得

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
				ErrorProcess(request);
				callback(null, null);
			}
		}

		/// <summary>
		/// ポケモン画像を取得
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
				ErrorProcess(request);
				callback(null);
			}
		}

		/// <summary>
		/// ポケモン名を取得
		/// </summary>
		/// <param name="url">ポケモン名取得APIのURL</param>
		/// <param name="callback">ポケモン名を返却</param>
		/// <returns></returns>
		private IEnumerator GetPokemonName(string url, Action<string> callback) {
			using UnityWebRequest request = UnityWebRequest.Get(url);

			// ポケモン名取得APIリクエスト
			yield return request.SendWebRequest();

			if (request.result == UnityWebRequest.Result.Success) {
				// 成功時
				GetPokemonNameAPIResponseData data = JsonUtility.FromJson<GetPokemonNameAPIResponseData>(request.downloadHandler.text); // 文字列からJsonを取得

				// 日本語のポケモン名を取得する
				GetPokemonNameAPIResponseData.Names name = data.names.Find((value) => value.language.name == "ja");

				// ポケモン名を返却
				callback(name.name);
			} else {
				// エラー時
				ErrorProcess(request);
				callback(null);
			}
		}

		/// <summary>
		/// エラー処理
		/// </summary>
		/// <param name="request">リクエスト情報</param>
		private void ErrorProcess(UnityWebRequest request) {
			switch (request.result) {
				case UnityWebRequest.Result.InProgress:
					// リクエスト中
					Debug.Log("InProgress");
					break;
				case UnityWebRequest.Result.ProtocolError:
					Debug.Log("ProtocolError:" + request.error);
					break;
				case UnityWebRequest.Result.DataProcessingError:
					Debug.Log("DataProcessingError:" + request.error);
					break;
				case UnityWebRequest.Result.ConnectionError:
					Debug.Log("ConnectionError:" + request.error);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
