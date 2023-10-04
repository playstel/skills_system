using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using LazySoccer.Network.Example.ClassRequest.Get;
using LazySoccer.Network.Example.CollectionScriptableObject;
using LazySoccer.Network.Example.Game.Unit;
using LazySoccer.Network.Example.Network.WebRequestUnit;
using LazySoccer.Network.Example.Service;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LazySoccer.Network.Example.Game.UnitList
{
    public class UnitList : MonoBehaviour
    {
        [SerializeField] private bool UseRestApi;
        [SerializeField] private List<ClassUnit.Unit> _unitCollection = new();

        private async void Start()
        {
            _unitCollection = await LoadUnitCollection();

            CreateUnitInstances();
        }

        private async UniTask<List<ClassUnit.Unit>> LoadUnitCollection()
        {
            if (UseRestApi)
            {
                return await ServiceLocator.GetService<WebRequestUnit>().GET_UnitCollection();
            }

            return ServiceLocator.GetService<UnitLocalSource>().LocalUnitCollection.GetUnitCollection();
        }

        public List<ClassUnit.Unit> GetUnitColletion()
        {
            return _unitCollection;
        }

        [Button]
        public void CreateUnitInstances()
        {
            foreach (var unitData in _unitCollection)
            {
                var unitInstance = new GameObject();

                unitInstance.transform.SetParent(transform);
                unitInstance.AddComponent<UnitIntance>().CreateUnitComponents(unitData, UseRestApi);
            }
        }
    }
}