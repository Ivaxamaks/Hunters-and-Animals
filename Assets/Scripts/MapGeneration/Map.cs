using System.Collections.Generic;
using UnityEngine;

namespace MapGeneration
{
    public class Map
    {
        private readonly Tile[,] _tiles;
        private readonly Vector2 _size;

        public Map(Vector2 size, GameObject surface)
        {
            _size = size;
            var tiles = new Tile[(int)size.x, (int)size.y];
            for (var x = 0; x < size.x; x++)
            {
                for (var y = 0; y < size.y; y++)
                {
                    tiles[x, y] = new Tile(x, y, surface);
                }
            }

            _tiles = tiles;
        }
        
        public IEnumerable<Tile> GetTilesInRange(Bounds bounds)
        {
            var countX = bounds.max.x > _size.x ? _size.x : bounds.max.x;
            var countY = bounds.max.z > _size.y ? _size.y : bounds.max.z;
            var minX = bounds.min.x > 0 ? bounds.min.x : 0;
            var minY = bounds.min.z > 0 ? bounds.min.z : 0;
            for (var x = (int)minX; x < countX; x++)
            {
                for (var y = (int)minY; y < countY; y++)
                {
                    yield return _tiles[x, y];
                }
            }
        }

        public Tile GetRandomWalkableTile()
        {
            var randomX = (int)Random.Range(0, _size.x);
            var randomY = (int)Random.Range(0, _size.y);
            var tile = _tiles[randomX, randomY];
            return tile.Obstacle == null ? _tiles[randomX, randomY] : GetRandomWalkableTile();
        }
    }
}
