using System;
using Snake.Runtime.Views.WorldView;
using UnityEngine;
using UnityEngine.Assertions;

namespace Snake.Runtime.Views
{
    public class WorldViewSnake : MonoBehaviour
    {
        SnakeGame Game => FindObjectOfType<SharedModel>().Model;

        void Awake() => SpawnWorld();

        static void SpawnWorld()
        {
            FindObjectOfType<Wall>().BuildFor(SnakeGame.MapSize);
            FindObjectOfType<Floor>().BuildFor(SnakeGame.MapSize);
        }
    }
}