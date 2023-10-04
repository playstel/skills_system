using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Example.Network
{
    public class WebRequest<T> : MonoBehaviour
    {
        public string swaggerURL;
        public string bearerToken;

        public async UniTask<UnityWebRequest> GetRequest(T type, string URLParam = "")
        {
            Debug.Log("Warning! The response addresses a fake server address, so the web request will generate a destination host error");

            UnityWebRequest webRequest = UnityWebRequest.Get(FullURL(swaggerURL, type, URLParam));

            webRequest.SetRequestHeader("Authorization", $"Bearer {bearerToken}");
            
            Debug.Log("GET: " + FullURL(swaggerURL, type, URLParam));

            var webRequestResult = await webRequest.SendWebRequest();

            if (webRequestResult == null)
            {
                Debug.LogError("Web request error");
                return null;
            }
            
            return webRequest;
        }
        
        public virtual string FullURL(string URL, T type, string URLParam = "") => URL + URLParam;
    }
}