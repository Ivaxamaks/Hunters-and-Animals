using System.Collections.Generic;
using UnityEngine;

namespace MapGeneration
{
    [CreateAssetMenu(fileName = "MapGenerationSettings", menuName = "MapGenerationSettings")]
    public class MapGenerationSettings : ScriptableObject
    {
        public List<GameObject>  ObstaclePrefabs;
        public List<GameObject>  GroundPrefabs;
        public Vector2 Size = new Vector2(100, 100);
        public Vector2 MinObstacleSize = Vector2.one;
        public Vector2 MaxObstacleSize = new Vector2(5, 5);
        public Vector2 MinMaxObstacleCount = new Vector2(10, 20);
    }
}
