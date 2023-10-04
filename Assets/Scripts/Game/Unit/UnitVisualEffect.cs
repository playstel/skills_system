using LazySoccer.Network.Example.Collection.CollectionInterface;
using LazySoccer.Network.Example.CollectionEnum;
using UnityEngine;

namespace LazySoccer.Network.Example.Game.Unit
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