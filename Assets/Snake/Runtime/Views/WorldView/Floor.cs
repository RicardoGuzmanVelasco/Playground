using UnityEngine;

namespace Snake.Runtime.Views.WorldView
{
    internal class Floor : MonoBehaviour
    {
        [SerializeField] GameObject floorPrefab;
    
        public void BuildFor(int mapSize)
        {
            for(var x = mapSize.MinEdge(); x <= mapSize.MaxEdge(); x++)
            for(var y = mapSize.MinEdge(); y <= mapSize.MaxEdge(); y++)
                SpawnFloor(x, y);
        }

        void SpawnFloor(int x, int y)
        {
            var floor = Instantiate(floorPrefab, transform);
            floor.transform.localPosition = new(x, y);
        }
    }
}