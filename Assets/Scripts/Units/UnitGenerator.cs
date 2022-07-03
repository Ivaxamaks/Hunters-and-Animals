using System.Collections.Generic;
using Plugins.ObjectPool;
using UnityEngine;
using MapGeneration;
using Utility;

namespace Units
{
    public class UnitGenerator
    {
        private readonly Map        _map;
        private readonly ObjectPool _unitsPool;

        public UnitGenerator(Map map, GameObject unit)
        {
            _map = map;
            _unitsPool = new ObjectPool(unit);
        }
        
        public List<Unit> Generate(int amount)
        {
            var units = new List<Unit>();
            for (var i = 0; i < amount; i++)
            {
                var poolObject = _unitsPool.Get();
                GenerateUnitPosition(poolObject);
                var unit = poolObject.GetComponent<Unit>();
                units.Add(unit);
            }

            return units;
        }

        public void UnitsToPool()
        {
            _unitsPool.AllToPool();
        }

        private void GenerateUnitPosition(GameObject unit)
        {
            var tilePosition = _map.GetRandomWalkableTile().Position;
            var position = Utilities.ConvertVector2(tilePosition);
            unit.transform.position = position;
        }
    }
}
