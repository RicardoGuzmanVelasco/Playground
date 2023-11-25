using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Snake;
using UnityEngine;

public class SharedModel : MonoBehaviour
{
    [SerializeField] float speed = 0.75f;
    
    public SnakeGame Model { get; private set; } = SnakeGame.NewGame;

    async void Start()
    {
        while(!Model.GameOver)
        {
            await Task.Delay(TimeSpan.FromSeconds(speed) * Time.timeScale);
            Model = Model.Tick();
        }
    }
    
    
}
