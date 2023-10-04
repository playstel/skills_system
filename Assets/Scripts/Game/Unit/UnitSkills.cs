using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using LazySoccer.Network.Example.ClassRequest.Get;
using LazySoccer.Network.Example.Collection.CollectionInterface;
using LazySoccer.Network.Example.CollectionEnum;
using LazySoccer.Network.Example.CollectionScriptableObject;
using LazySoccer.Network.Example.Game.UnitList;
using LazySoccer.Network.Example.Network.WebRequestUnit;
using LazySoccer.Network.Example.Service;
using UnityEngine;

namespace LazySoccer.Network.Example.Game.Unit
{
    public class UnitSkills : MonoBehaviour, ISkill
    {
        [SerializeField] private List<ClassUnit.UnitSkill> _unitSkillsList = new();

        private UnitIntance _unit;

        private async void Start()
        {
            _unit = GetComponent<UnitIntance>();
            
            await LoadUnitSkillsData();

            SkillTest();
        }

        private async UniTask LoadUnitSkillsData()
        {
            if (_unit.UseRestApi)
            {
                _unitSkillsList = await ServiceLocator.GetService<WebRequestUnit>().GET_UnitSkills(_unit.Unit.UnitName);
                return;
            }

            var skills = ServiceLocator.GetService<UnitLocalSource>().LocalUnitCollection.GetUnit(_unit.Unit.UnitName).UnitSkills;

            foreach (var skill in skills)
            {
                var skillData = ServiceLocator.GetService<UnitLocalSource>().LocalUnitSkills.GetUnitSkill(skill);
                _unitSkillsList.Add(skillData);
            }
        }

        private void SkillTest()
        {
            if (_unit.Unit.UnitName == EnumUnit.UnitName.Warrior)
            {
                HandleSkill(EnumUnit.SkillName.Damage);
            }
            
            if (_unit.Unit.UnitName == EnumUnit.UnitName.Wizzard)
            {
                HandleSkill(EnumUnit.SkillName.MassHeal);
            }
        }

        public void HandleSkill(EnumUnit.SkillName skillName)
        {
            var skill = _unitSkillsList.Where(s => s.SkillName == skillName).FirstOrDefault();

            if (skill == default) { Debug.LogError("Failed to find skill data: " + skillName); return; }

            ActiveSkillByType(skill);
        }

        private void ActiveSkillByType(ClassUnit.UnitSkill skill = null)
        {
            Debug.Log("---");
            Debug.Log(_unit.Unit.UnitName + " use skill " + skill.SkillInfo.SkillNameText + " with type " + skill.SkillType);

            switch (skill.SkillType)
            {
                case EnumUnit.SkillType.InstantUse:
                    _unit.Impact.SetImpact(skill.SkillImpact);
                    
                    Debug.Log("Heal: " + skill.SkillImpact.ImpactHealthPoints 
                                           + " for MSeconds: " + skill.SkillImpact.ImpactDurationMsec);
                    
                    break;
                
                case EnumUnit.SkillType.Throw:
                    
                    Debug.Log("Throw | Power: " 
                              + skill.SkillArea.SkillThrowPower + " | Radius: " 
                              + skill.SkillArea.SkillZoneRadius);
                    
                    break;
                
                case EnumUnit.SkillType.MelleHit:
                    
                    Debug.Log("Melle hit addition HP substraction: " + skill.SkillImpact.ImpactHealthPoints 
                                       + " for MSeconds: " + skill.SkillImpact.ImpactDurationMsec);
                    
                    break;
                
                case EnumUnit.SkillType.RadiusAroundUnit:
                    
                    Debug.Log("RadiusAroundUnit | Duration: " 
                              + skill.SkillImpact.ImpactDurationMsec + " | Radius: " 
                              + skill.SkillArea.SkillZoneRadius + " | HP: " 
                              + skill.SkillImpact.ImpactHealthPoints);
                    
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}