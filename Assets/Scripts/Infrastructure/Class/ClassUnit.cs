using System;
using System.Collections.Generic;
using Test.Network.Example.CollectionEnum;
using UnityEngine;

namespace Test.Network.Example.ClassRequest.Get
{
    public class ClassUnit
    {
        [Serializable]
        public class Unit
        {
            public EnumUnit.UnitName UnitName;
            public List<EnumUnit.SkillName> UnitSkills;
        }
        
        [Serializable]
        public class UnitSkill
        {
            public EnumUnit.SkillName SkillName; 
            public EnumUnit.SkillType SkillType; // what king of action using in skill: it can be a melle attack, or selfheal, or etc.
            public SkillInfo SkillInfo;
            public SkillUses SkillUses;
            public SkillArea SkillArea;
            public SkillImpact SkillImpact;
        }
        
        [Serializable]
        public class SkillUses
        {
            public double SkillUseCost; // 10 mana points
            public int SkillCooldownMsec; // example: cooldown with value 15000 will proceed 15 seconds
        }

        [Serializable]
        public class SkillImpact
        {
            public int ImpactHealthPoints; // example: for healing = 10; for damage = -10
            public int ImpactSpeedPoints; // example: for fast running = 10; to slow down enemies = -10
            public float ImpactDurationMsec; // how long impact will stay on target
            public EnumUnit.ImpactFX ImpactFX; // fire = 2
        }
        
        [Serializable]
        public class SkillArea
        {
            public float SkillMaxThrowDistance; // 10 meters of throw
            public float SkillThrowPower; // 10 meters of throw
            public float SkillZoneRadius; // 5 meters of damage radius
        }

        [Serializable]
        public class SkillInfo
        {
            public string SkillNameText; 
            public string SkillDescriptionText; 
            public string SkillImageByteCode; 
        }
    }
}