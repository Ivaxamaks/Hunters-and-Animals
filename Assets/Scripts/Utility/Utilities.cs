using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public static class Utilities
    {
        public static T GetRandomElement<T>(List<T> array)
        {
            var arrayCount = array.Count;
            var randomIndex = Random.Range(0, arrayCount);
            return array[randomIndex];
        }

        public static Vector3 ConvertVector2(Vector2 vector)
        {
            return new Vector3(vector.x, 0, vector.y);
        }
        
        public static float RandomMinMaxVector2(Vector2 vector)
        {
            return vector.x < vector.y ? Random.Range(vector.x, vector.y) : Random.Range(vector.y, vector.x);
        }
        
        public static Vector2 RandomBetweenVectors2(Vector2 vector, Vector2 vector2)
        {
            var x = Random.Range(vector.x, vector2.x);
            var y = Random.Range(vector.y, vector2.y);
            return new Vector2(x, y);
        }
    }
}
