using System;
using LazySoccer.Network.Example.ClassRequest.Get;
using UnityEngine;

namespace LazySoccer.Network.Example.Game.Unit
{
    public class UnitImpact : MonoBehaviour
    {
        private UnitIntance _unit;

        private void Start()
        {
            _unit = GetComponent<UnitIntance>();
        }

        public void SetImpact(ClassUnit.SkillImpact unitImpact)
        {
            _unit.Health.HandleHealthChange(unitImpact.ImpactHealthPoints);
            _unit.Speed.HandleSpeedChange(unitImpact.ImpactSpeedPoints);
            _unit.VisualEffect.HandleEffectStart(unitImpact.ImpactFX);
        }
    }
}