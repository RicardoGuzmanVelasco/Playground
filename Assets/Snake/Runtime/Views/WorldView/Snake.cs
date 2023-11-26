using System;
using System.Linq;
using UnityEngine;

namespace Snake.Runtime.Views.WorldView
{
    internal class Snake : MonoBehaviour
    {
        [SerializeField] GameObject snakeBodyPrefab;
        [SerializeField] GameObject snakeHeadPrefab;
        
        SnakeGame Game => FindObjectOfType<SharedModel>().Model;

        void Start() => SpawnSnake();

        void SpawnSnake()
        {
            foreach(var snakePart in Game.Snake)
                SpawnSnakePart(snakePart);
        }

        void SpawnSnakePart(Coordinate snakePart)
        {
            var snakePartPrefab = Game.Head.Equals(snakePart) ? snakeHeadPrefab : snakeBodyPrefab;
            var part = Instantiate(snakePartPrefab, snakePart.Offset(), Quaternion.identity, transform);
            part.name = "SnakePart";
        }
    }
}