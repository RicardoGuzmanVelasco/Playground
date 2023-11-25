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

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
            Model = Model.LookTowards((0, 1));
        else if(Input.GetKeyDown(KeyCode.DownArrow))
            Model = Model.LookTowards((0, -1));
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
            Model = Model.LookTowards((1, 0));
        else if(Input.GetKeyDown(KeyCode.RightArrow))
            Model = Model.LookTowards((-1, 0));
    }
}
