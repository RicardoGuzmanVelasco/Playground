using Snake.Runtime.Views.WorldView;
using UnityEngine;

namespace Snake.Runtime.Views
{
    public class WorldViewSnake : MonoBehaviour
    {
        SnakeGame Game => FindObjectOfType<SharedModel>().Model;

        void Awake()
        {
            FindObjectOfType<Wall>().BuildFor(SnakeGame.MapSize);
            FindObjectOfType<Floor>().BuildFor(SnakeGame.MapSize);
        }

        void OnEnable()
        {
            Debug.LogError("Esta vista no está hecha");
        }
    }
}