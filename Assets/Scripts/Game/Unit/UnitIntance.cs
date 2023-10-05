using System;
using Test.Network.Example.ClassRequest.Get;
using UnityEngine;

namespace Test.Network.Example.Game.Unit
{
    public class UnitIntance : MonoBehaviour
    {
        [HideInInspector] public ClassUnit.Unit Unit;
        [HideInInspector] public UnitHealth Health;
        [HideInInspector] public UnitSkills Skills;
        [HideInInspector] public UnitSpeed Speed;
        [HideInInspector] public UnitVisualEffect VisualEffect;
        [HideInInspector] public UnitImpact Impact;
        [HideInInspector] public bool UseRestApi;

        public void CreateUnitComponents(ClassUnit.Unit Unit, bool UseRestApi = false)
        {
            gameObject.name = Unit.UnitName.ToString();
            
            this.Unit = Unit;
            this.UseRestApi = UseRestApi;
            
            InitUnitComponents();
        }

        private void InitUnitComponents()
        {
            Health = gameObject.AddComponent<UnitHealth>();
            Skills = gameObject.AddComponent<UnitSkills>();
            Speed = gameObject.AddComponent<UnitSpeed>();
            VisualEffect = gameObject.AddComponent<UnitVisualEffect>();
            Impact = gameObject.AddComponent<UnitImpact>();
        }
    }
}