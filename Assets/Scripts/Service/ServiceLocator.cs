using System.Collections.Generic;
using UnityEngine;

namespace LazySoccer.Network.Example.Service
{
    public class ServiceLocator : MonoBehaviour
    {
        private static ServiceLocator _singleton;
        private Dictionary<System.Type, Object> cache = new ();
        
        private static ServiceLocator singleton
        {
            get
            {
                return _singleton;
            }
        }

        private void Awake()
        {
            _singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        
        public static T GetService<T>() where T : Object
        {
            Object cached;
        
            System.Type key = typeof(T);

            if (singleton == null || singleton.cache == null)
            {
                Debug.LogError("Could not find singleton cache");
                return null;
            }
        
            singleton.cache.TryGetValue(key, out cached);
        
            if (!cached)
            {
                cached = GameObject.FindObjectOfType<T>();
            
                if (!cached)
                {
                    Debug.LogError("Could not find the Service: " + key.Name);

                    return null;
                }
                
                if (singleton.cache.ContainsKey(key))
                {
                    singleton.cache.Remove(key);
                }
                
                singleton.cache.Add(key, cached);
            }
            
            return cached as T;
        }
        
        private void OnDestroy()
        {
            if (_singleton != null)
                _singleton = null;
        }
    }
}