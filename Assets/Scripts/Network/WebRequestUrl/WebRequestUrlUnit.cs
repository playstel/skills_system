using System;
using Example.Network;
using UnityEngine;

namespace Test.Network.Example.Network.WebRequestUnit
{
    [CreateAssetMenu(fileName = "UrlUnit", menuName = "Networking/UrlUnit", order = 1)]
    public class WebRequestUrlUnit: WebRequestUrl<UnitRequest>
    {
        public WebRequestUrlUnitDb dictionatyURL;
    }
    
    [Serializable]
    public class WebRequestUrlUnitDb : WebRequestUrlDb<UnitRequest> { }


}