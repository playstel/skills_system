using System.Collections.Generic;
using System.Linq;
using Test.Network.Example.ClassRequest.Get;
using Test.Network.Example.CollectionEnum;
using UnityEngine;

namespace Test.Network.Example.CollectionScriptableObject
{
    [CreateAssetMenu(menuName = "Unit/Skills")]
    public class LocalUnitSkills : ScriptableObject
    {
        [SerializeField] private List<ClassUnit.UnitSkill> listUnitSkills;

        public ClassUnit.UnitSkill GetUnitSkill(EnumUnit.SkillName skillName)
        {
            return listUnitSkills.Where(s => s.SkillName == skillName).FirstOrDefault();
        }
    }
}