using Test.Network.Example.Collection.CollectionInterface;
using Test.Network.Example.CollectionEnum;
using UnityEngine;

namespace Test.Network.Example.Game.Unit
{
    public class UnitVisualEffect : MonoBehaviour, IEffect
    {
        public void HandleEffectStart(EnumUnit.ImpactFX? value)
        {
            throw new System.NotImplementedException();
        }

        public void HandleEffectFinish(EnumUnit.ImpactFX? value)
        {
            throw new System.NotImplementedException();
        }

        public void DestroyAllEffects()
        {
            throw new System.NotImplementedException();
        }
    }
}