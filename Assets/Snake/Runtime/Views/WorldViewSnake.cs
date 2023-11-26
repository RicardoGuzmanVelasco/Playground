using System;
using Snake;
using UnityEngine;

public class WorldViewSnake : MonoBehaviour
{
    SnakeGame Game => FindObjectOfType<SharedModel>().Model;

    void Awake()
    {
        FindObjectOfType<Wall>().BuildFor(SnakeGame.MapSize);
    }

    void OnEnable()
    {
        Debug.LogError("Esta vista no está hecha");
    }
}