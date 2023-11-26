using System;
using Snake.Runtime.Views.WorldView;
using UnityEngine;
using UnityEngine.Assertions;

namespace Snake.Runtime.Views
{
    public class WorldViewSnake : MonoBehaviour
    {
        [SerializeField] GameObject fruitPrefab;
        
        SnakeGame Game => FindObjectOfType<SharedModel>().Model;

        void Awake() => SpawnWorld();

        void Start()
        {
            var coord = new Vector2(Game.Fruit.X, Game.Fruit.Y);
            var worldPosition = coord - Vector2.one * ( 1 + SnakeGame.MapSize.MaxEdge());
            SpawnFruitAt(worldPosition);
        }

        void SpawnFruitAt(Vector2 position)
        {
            Assert.IsNotNull(fruitPrefab);
            Instantiate(fruitPrefab, position, Quaternion.identity, transform);
        }

        static void SpawnWorld()
        {
            FindObjectOfType<Wall>().BuildFor(SnakeGame.MapSize);
            FindObjectOfType<Floor>().BuildFor(SnakeGame.MapSize);
        }
    }
}