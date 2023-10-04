using LazySoccer.Network.Example.CollectionEnum;

namespace LazySoccer.Network.Example.Collection.CollectionInterface
{
    public interface ISkill
    {
        void HandleSkill(EnumUnit.SkillName skillName);
    }
}