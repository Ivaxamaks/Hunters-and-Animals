using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Plugins.ObjectPool
{
    public class ObjectPool
    {
        private List<GameObject> _pooledObjects = new List<GameObject>();
    
        private GameObject _poolObject;
        private float _amount;

        public ObjectPool(GameObject poolObject, int amount = 15)
        {
            _poolObject = poolObject;
            _amount = amount;

            PoolCreator();
        }
        
        private void PoolCreator()
        {
            for (var i = 0; i < _amount; i++)
            {
                CreateObject(_poolObject);
            }
        }

        public GameObject Get()
        {
            for (var i = 0; i < _pooledObjects.Count; i++)
            {
                var currentObject = _pooledObjects[i];
                if (currentObject.activeInHierarchy)
                {
                    continue;
                }
                
                currentObject.SetActive(true);
                return _pooledObjects[i];
            }

            var newObject = CreateObject(_pooledObjects.First());
            newObject.SetActive(true);

            return newObject;
        }

        private GameObject CreateObject(GameObject gameObject)
        {
            var newObject = Object.Instantiate(gameObject);

            newObject.name = gameObject.name;
            newObject.SetActive(false);
            _pooledObjects.Add(newObject);

            return newObject;
        }
        
        public void AllToPool()
        {
            foreach (var pooledObject in _pooledObjects)
            {
                pooledObject.SetActive(false);
            }
        }
        
        public void ClearPool()
        {
            foreach (var pooledObject in _pooledObjects)
            {
                Object.Destroy(pooledObject);
            }
        
            _pooledObjects.Clear();
        }
    }
}