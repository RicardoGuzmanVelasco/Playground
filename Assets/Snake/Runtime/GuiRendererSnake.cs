using System;
using Snake;
using UnityEngine;

public class GuiRendererSnake : MonoBehaviour
{
    SnakeGame Game => FindObjectOfType<SharedModel>().Model;

    void OnEnable()
    {
        Debug.LogError("Esta vista no está hecha");
    }
}