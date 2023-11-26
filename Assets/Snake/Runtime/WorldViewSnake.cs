using System;
using Snake;
using UnityEngine;

public class WorldViewSnake : MonoBehaviour
{
    SnakeGame Game => FindObjectOfType<SharedModel>().Model;

    void Awake()
    {
        FindObjectOfType<WorldViewWall>().BuildFor(SnakeGame.MapSize);
    }

    void OnEnable()
    {
        Debug.LogError("Esta vista no está hecha");
    }
}