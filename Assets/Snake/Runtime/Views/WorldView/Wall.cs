﻿using UnityEngine;
using UnityEngine.Assertions;

namespace Snake.Runtime.Views.WorldView
{
    internal class Wall : MonoBehaviour
    {
        [SerializeField] GameObject wallPrefab;
    
        public void BuildFor(int mapSize)
        {
            var mapBounds = mapSize + 2;
            Assert.IsNotNull(wallPrefab);
        
            for(var x = mapBounds.MinEdge(); x <= mapBounds.MaxEdge(); x++)
            for(var y = mapBounds.MinEdge(); y <= mapBounds.MaxEdge(); y++)
                if(mapBounds.IsEdge(x, y))
                    SpawnBrick(x, y);
        }

        void SpawnBrick(int x, int y)
        {
            var wall = Instantiate(wallPrefab, transform);
            wall.transform.localPosition = new Vector3(x, y);
        }
    }
}