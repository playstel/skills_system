using System.Collections.Generic;
using System.Linq;
using LazySoccer.Network.Example.ClassRequest.Get;
using LazySoccer.Network.Example.CollectionEnum;
using UnityEngine;

namespace LazySoccer.Network.Example.CollectionScriptableObject
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