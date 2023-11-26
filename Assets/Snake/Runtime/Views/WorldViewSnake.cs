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
        void Start() => SpawnFruit();
        void Update() => MoveFruit();
        
        void SpawnFruit()
        {
            var fruit = Instantiate(fruitPrefab, FruitWorldPosition(), Quaternion.identity, transform);
            fruit.name = "Fruit";
        }

        void MoveFruit()
        {
            var fruit = GameObject.Find("Fruit");
            Assert.IsNotNull(fruit);
            
            fruit.transform.localPosition = FruitWorldPosition();
        }

        static void SpawnWorld()
        {
            FindObjectOfType<Wall>().BuildFor(SnakeGame.MapSize);
            FindObjectOfType<Floor>().BuildFor(SnakeGame.MapSize);
        }
        
        Vector2 FruitWorldPosition()
        {
            var coord = new Vector2(Game.Fruit.X, Game.Fruit.Y);
            var worldPosition = coord - Vector2.one * (1 + SnakeGame.MapSize.MaxEdge());
            return worldPosition;
        }
    }
}