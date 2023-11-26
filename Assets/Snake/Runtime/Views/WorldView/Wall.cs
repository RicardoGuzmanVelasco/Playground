using UnityEngine;
using UnityEngine.Assertions;

internal class Wall : MonoBehaviour
{
    [SerializeField] GameObject wallPrefab;
    
    public void BuildFor(int mapSize)
    {
        Assert.IsNotNull(wallPrefab);
        
        for(var x = mapSize.OneEdge(); x <= mapSize.OtherEdge(); x++)
            for(var y = mapSize.OneEdge(); y <= mapSize.OtherEdge(); y++)
                    SpawnBrick(x, y);
    }

    void SpawnBrick(int x, int y)
    {
        var wall = Instantiate(wallPrefab, transform);
        wall.transform.localPosition = new Vector3(x, y);
    }
}