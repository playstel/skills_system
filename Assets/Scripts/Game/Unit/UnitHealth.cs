using Test.Network.Example.Collection.CollectionInterface;
using UnityEngine;

namespace Test.Network.Example.Game.Unit
{
    public class UnitHealth : MonoBehaviour, IHealth
    {
        public void HandleHealthChange(int? value)
        {
            throw new System.NotImplementedException();
        }

        public void HandleDeath()
        {
            throw new System.NotImplementedException();
        }
    }
}