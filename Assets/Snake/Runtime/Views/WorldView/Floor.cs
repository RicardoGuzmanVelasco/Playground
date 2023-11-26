using UnityEngine;

namespace Snake.Runtime.Views.WorldView
{
    internal class Floor : MonoBehaviour
    {
        [SerializeField] GameObject floorPrefab;
    
        public void BuildFor(int mapSize)
        {
            for(var x = mapSize.OneEdge(); x <= mapSize.OtherEdge(); x++)
            for(var y = mapSize.OneEdge(); y <= mapSize.OtherEdge(); y++)
                SpawnFloor(x, y);
        }

        void SpawnFloor(int x, int y)
        {
            var floor = Instantiate(floorPrefab, transform);
            floor.transform.localPosition = new(x, y);
        }
    }
}