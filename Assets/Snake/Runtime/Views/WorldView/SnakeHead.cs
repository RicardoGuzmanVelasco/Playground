using System;
using UnityEngine;

namespace Snake.Runtime.Views.WorldView
{
    internal class SnakeHead : SnakePart
    {
        void Start()
        {
            var lookingAt = FindObjectOfType<SharedModel>().Model.direction;
            var vector = new Vector2(lookingAt.X, lookingAt.Y);
            
            transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.left, vector));
        }
    }
}