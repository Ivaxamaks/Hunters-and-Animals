using UnityEngine;
using Utility;

namespace MapGeneration
{
    public class Tile
    {
        public Vector2    Position { get; }
        public GameObject Obstacle { get; private set; }

        public Tile(float x, float y, GameObject surface)
        {
            Position = new Vector2(x, y);
            var position = Utilities.ConvertVector2(Position);
            Object.Instantiate(surface, position, Quaternion.identity);
        }

        public void SetObstacle(GameObject obstacle)
        {
            var position = Utilities.ConvertVector2(Position);
            Obstacle = Object.Instantiate(obstacle, position, Quaternion.identity);
        }
    }
}
