using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using static ResponseData.PokemonResponseData;

namespace Home {
	public class HomeModel : MonoBehaviour {

		private const string BASE_URL = "https://pokeapi.co/api/v2/pokemon/";

		public IEnumerator GetPoketMonsterDataAPI(string id, Action<Texture2D> callback) {
			using UnityWebRequest request = UnityWebRequest.Get(BASE_URL + id);
			Debug.Log("Request URL:" + request.url);

			// ポケモンAPIリクエスト
			yield return request.SendWebRequest();

			switch (request.result) {
				case UnityWebRequest.Result.Success:
					// 成功時
					PokemonAPIResponseData data = JsonUtility.FromJson<PokemonAPIResponseData>(request.downloadHandler.text); // 文字列からJsonを取得

					// 画像を取得する
					StartCoroutine(GetTexture(data.sprites.front_default, (texture) => {
						callback(texture); // 画像のテクスチャをコールバック
					}));
					break;
				case UnityWebRequest.Result.InProgress:
					Debug.Log("InProgress");
					// リクエスト中
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

		private IEnumerator GetTexture(string url, Action<Texture2D> callback) {
			Uri uri = new(url);

			using UnityWebRequest request = UnityWebRequestTexture.GetTexture(uri);

			yield return request.SendWebRequest();

			switch (request.result) {
				case UnityWebRequest.Result.Success:
					// 成功時、画像のテクスチャを返す
					var texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
					callback.Invoke(texture);
					break;
				case UnityWebRequest.Result.InProgress:
					Debug.Log("InProgress");
					// リクエスト中
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
