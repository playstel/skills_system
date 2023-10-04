using System.Collections.Generic;
using System.Linq;
using LazySoccer.Network.Example.ClassRequest.Get;
using LazySoccer.Network.Example.CollectionEnum;
using UnityEngine;

namespace LazySoccer.Network.Example.CollectionScriptableObject
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