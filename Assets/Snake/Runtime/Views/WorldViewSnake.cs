using Snake.Runtime.Views.WorldView;
using UnityEngine;

namespace Snake.Runtime.Views
{
    public class WorldViewSnake : MonoBehaviour
    {
        void Awake() => SpawnWorld();

        static void SpawnWorld()
        {
            FindObjectOfType<Wall>().BuildFor(SnakeGame.MapSize);
            FindObjectOfType<Floor>().BuildFor(SnakeGame.MapSize);
        }
    }
}