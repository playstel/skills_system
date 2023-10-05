using System.Collections.Generic;
using System.Linq;
using Test.Network.Example.ClassRequest.Get;
using Test.Network.Example.CollectionEnum;
using UnityEngine;

namespace Test.Network.Example.CollectionScriptableObject
{
    [CreateAssetMenu(menuName = "Unit/Collection")]
    public class LocalUnitCollection : ScriptableObject
    {
        [SerializeField] private List<ClassUnit.Unit> listUnitType;
        
        public ClassUnit.Unit GetUnit(EnumUnit.UnitName unitName)
        {
            return listUnitType.Where(u => u.UnitName == unitName).FirstOrDefault();
        }
        
        public List<ClassUnit.Unit> GetUnitCollection()
        {
            return listUnitType;
        }
    }
}