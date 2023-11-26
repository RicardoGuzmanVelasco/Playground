using UnityEngine;

namespace Snake.Runtime.Views.WorldView
{
    internal static class MapEdges
    {
        public static bool IsEdge(this int mapSize, int x, int y)
            => x == -mapSize / 2 ||
               x == mapSize / 2 - 1 ||
               y == -mapSize / 2 ||
               y == mapSize / 2 - 1;

        public static int MinEdge(this int mapSize) => -mapSize / 2;
        public static int MaxEdge(this int mapSize) => mapSize / 2 - 1;

        public static Vector2 Offset(this int mapSize) => Vector2.one * (1 + mapSize.MaxEdge());
    }
}