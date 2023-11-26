using UnityEngine;
using UnityEngine.Assertions;

internal class WorldViewWall : MonoBehaviour
{
    [SerializeField] GameObject wallPrefab;
    
    public void BuildFor(int mapSize)
    {
        Assert.IsNotNull(wallPrefab);
        
        for(var x = OneEdge(mapSize); x <= OtherEdge(mapSize); x++)
            for(var y = OneEdge(mapSize); y <= OtherEdge(mapSize); y++)
                    SpawnBrick(x, y);
    }

    void SpawnBrick(int x, int y)
    {
        var wall = Instantiate(wallPrefab, transform);
        wall.transform.localPosition = new Vector3(x, y);
    }

    static int OtherEdge(int mapSize) => mapSize / 2 - 1;
    static int OneEdge(int mapSize) => -mapSize / 2;

    static bool IsEdge(int mapSize, int x, int y)
    {
        return x == -mapSize / 2 || x == mapSize / 2 - 1 || y == -mapSize / 2 || y == mapSize / 2 - 1;
    }
}