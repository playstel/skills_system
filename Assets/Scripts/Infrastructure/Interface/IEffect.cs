using LazySoccer.Network.Example.CollectionEnum;

namespace LazySoccer.Network.Example.Collection.CollectionInterface
{
    public interface IEffect
    {
        void HandleEffectStart(EnumUnit.ImpactFX? value);
        void HandleEffectFinish(EnumUnit.ImpactFX? value);
        void DestroyAllEffects();
    }
}