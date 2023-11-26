using System;
using System.Linq;
using UnityEngine;

namespace Snake.Runtime.Views.WorldView
{
    internal class Snake : MonoBehaviour
    {
        [SerializeField] SnakeBody snakeBodyPrefab;
        [SerializeField] SnakeHead snakeHeadPrefab;

        SnakeGame Game => FindObjectOfType<SharedModel>().Model;

        void Start() => SpawnSnake();
        void Update() => MoveSnake();

        void MoveSnake()
        {
            if(Game.GameOver)
                return;
            
            CleanLastSnake();
            SpawnSnake();
        }

        static void CleanLastSnake()
        {
            FindObjectsOfType<SnakePart>().Select(x => x.gameObject).ToList().ForEach(Destroy);
        }

        void SpawnSnake()
        {
            foreach(var snakePart in Game.Snake)
                SpawnSnakePart(snakePart);
        }

        void SpawnSnakePart(Coordinate snakePart)
        {
            var snakePartPrefab = SnakePartPrefab(snakePart);
            Instantiate(snakePartPrefab, snakePart.Offset(SnakeGame.MapSize), Quaternion.identity, transform);
        }

        SnakePart SnakePartPrefab(Coordinate snakePart)
            => Game.Head.Equals(snakePart) ? snakeHeadPrefab : snakeBodyPrefab;
    }
}