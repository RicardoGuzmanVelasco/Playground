using UnityEngine;

namespace Snake.Runtime.Views
{
    public class GuiRendererSnake : MonoBehaviour
    {
        SnakeGame Game => FindObjectOfType<SharedModel>().Model;

        void OnEnable()
        {
            Debug.LogError("Esta vista no está hecha");
        }
    }
}