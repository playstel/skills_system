using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Example.Network;
using LazySoccer.Network.Example.ClassRequest.Get;
using LazySoccer.Network.Example.CollectionEnum;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace LazySoccer.Network.Example.Network.WebRequestUnit
{
    public enum UnitRequest
    {
        GetUnitSkills, GetUnitCollection
    }
    
    public class WebRequestUnit : WebRequest<UnitRequest>
    {
        public WebRequestUrlUnit dbURL;

        public override string FullURL(string URL, UnitRequest type, string URLParam = "")
        {
            return URL + dbURL.dictionatyURL[type] + URLParam;
        }
        
        public async UniTask<List<ClassUnit.UnitSkill>> GET_UnitSkills(EnumUnit.UnitName unitName)
        {
            var param = $"/{unitName}";
            
            var webResponse = await GetRequest(UnitRequest.GetUnitSkills, URLParam: param);

            Debug.Log("GET_UninSkills | response: " + webResponse.downloadHandler.text);

            if (webResponse.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("GET_UninSkills | error: " + webResponse.result);
                return null;
            }
            
            return JsonConvert.DeserializeObject<List<ClassUnit.UnitSkill>>(webResponse.downloadHandler.text);
        }

        
        public async UniTask<List<ClassUnit.Unit>> GET_UnitCollection()
        {
            var webResponse = await GetRequest(UnitRequest.GetUnitCollection);

            Debug.Log("GET_UninCollection | response: " + webResponse.downloadHandler.text);
            
            if (webResponse.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("GET_UninSkills | error: " + webResponse.result);
                return null;
            }
            
            return JsonConvert.DeserializeObject<List<ClassUnit.Unit>>(webResponse.downloadHandler.text);
        }
    }
}