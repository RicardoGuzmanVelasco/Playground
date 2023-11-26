using System;
using System.Linq;
using UnityEngine;
using static UnityEngine.Quaternion;

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
            => FindObjectsOfType<SnakePart>().Select(x => x.gameObject).ToList().ForEach(Destroy);

        void SpawnSnake() => Game.Snake.ToList().ForEach(SpawnSnakePart);

        void SpawnSnakePart(Coordinate snakePart)
            => Instantiate(SnakePartPrefab(snakePart), snakePart.Offset(SnakeGame.MapSize), identity, transform);

        SnakePart SnakePartPrefab(Coordinate snakePart)
            => Game.Head.Equals(snakePart) ? snakeHeadPrefab : snakeBodyPrefab;
    }
}