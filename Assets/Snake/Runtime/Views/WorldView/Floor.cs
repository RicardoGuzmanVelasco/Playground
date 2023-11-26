using UnityEngine;

internal class Floor : MonoBehaviour
{
    [SerializeField] GameObject floorPrefab;
    
    public void BuildFor(int mapSize)
    {
        for(var x = mapSize.OneEdge(); x <= mapSize.OtherEdge(); x++)
            for(var y = mapSize.OneEdge(); y <= mapSize.OtherEdge(); y++)
                SpawnFloor(x, y);
    }

    void SpawnFloor(int i, int i1)
    {
        throw new System.NotImplementedException();
    }
}