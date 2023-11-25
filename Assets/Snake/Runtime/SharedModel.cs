using System;
using System.Threading.Tasks;
using Snake;
using UnityEngine;

public class SharedModel : MonoBehaviour
{
    [SerializeField] float stepFrequencyToBeginWith = 0.5f;
    float CurrentStepFrequency => stepFrequencyToBeginWith / (Model.Snake.Count);
    
    public SnakeGame Model { get; private set; } = SnakeGame.NewGame;

    async void Start()
    {
        while(!Model.GameOver)
        {
            await Task.Delay(TimeSpan.FromSeconds(CurrentStepFrequency) * Time.timeScale);
            Model = Model.Tick();
        }
    }

    void Update()
    {
        ListenDirectionInput();
    }

    void ListenDirectionInput()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
            Model = Model.LookTowards(Vector2Int.up.ToTuple());
        else if(Input.GetKeyDown(KeyCode.DownArrow))
            Model = Model.LookTowards(Vector2Int.down.ToTuple());
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
            Model = Model.LookTowards(Vector2Int.left.ToTuple());
        else if(Input.GetKeyDown(KeyCode.RightArrow))
            Model = Model.LookTowards(Vector2Int.right.ToTuple());
    }
}

public static class Bridge
{
    public static (int x, int y) ToTuple(this Vector2Int vector) => (vector.x, vector.y);
}