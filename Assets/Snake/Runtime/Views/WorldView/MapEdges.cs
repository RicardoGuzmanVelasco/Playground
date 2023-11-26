internal static class MapEdges
{
    public static bool IsEdge(this int mapSize, int x, int y)
        => x == -mapSize / 2 ||
           x == mapSize / 2 - 1 ||
           y == -mapSize / 2 ||
           y == mapSize / 2 - 1;

    public static int OneEdge(this int mapSize) => -mapSize / 2;
    public static int OtherEdge(this int mapSize) => mapSize / 2 - 1;
}