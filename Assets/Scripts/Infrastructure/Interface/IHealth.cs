namespace Test.Network.Example.Collection.CollectionInterface
{
    public interface IHealth 
    {
        void HandleHealthChange(int? value);

        void HandleDeath();
    }
}