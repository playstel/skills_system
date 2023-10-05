using Test.Network.Example.CollectionEnum;

namespace Test.Network.Example.Collection.CollectionInterface
{
    public interface IEffect
    {
        void HandleEffectStart(EnumUnit.ImpactFX? value);
        void HandleEffectFinish(EnumUnit.ImpactFX? value);
        void DestroyAllEffects();
    }
}