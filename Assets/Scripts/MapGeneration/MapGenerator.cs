using UnityEngine;
using UnityEngine.AI;
using Utility;

namespace MapGeneration
{
    [RequireComponent(typeof(NavMeshSurface))]
    public class MapGenerator : MonoBehaviour
    {
        public Map Generate(MapGenerationSettings settings)
        {
            var map = GenerateMap(settings); 
            GenerateRandomObstacles(map,settings);
            var navMeshSurface = GetComponent<NavMeshSurface>();
            navMeshSurface.BuildNavMesh();
            return map;
        }

        private Map GenerateMap(MapGenerationSettings settings)
        {
            var randomPrefab = Utilities.GetRandomElement(settings.GroundPrefabs);
            var map = new Map(settings.Size, randomPrefab);
            return map;
        }

        private void GenerateRandomObstacles(Map map, MapGenerationSettings settings)
        {
            var obstacleCount = Utilities.RandomMinMaxVector2(settings.MinMaxObstacleCount);
            for (var i = 0; i < obstacleCount; i++)
            {
                GenerateObstacle(map, settings);
            }
        }

        private void GenerateObstacle(Map map, MapGenerationSettings settings)
        {
            var size = Utilities.RandomBetweenVectors2(settings.MinObstacleSize, settings.MaxObstacleSize);
            var point = Utilities.RandomBetweenVectors2(Vector2.zero, settings.Size);
            var bounds = new Bounds(Utilities.ConvertVector2(point) + Vector3.up, Utilities.ConvertVector2(size));
            var tiles = map.GetTilesInRange(bounds);
            foreach (var tile in tiles)
            {
                if (tile.Obstacle != null) continue;
                var randomPrefab = Utilities.GetRandomElement(settings.ObstaclePrefabs);
                tile.SetObstacle(randomPrefab);
            }
        }
    }
}
