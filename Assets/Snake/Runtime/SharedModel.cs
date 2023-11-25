using System;
using System.Threading.Tasks;
using Snake;
using UnityEngine;

public class SharedModel : MonoBehaviour
{
    [SerializeField] float stepFrequencyToBeginWith = 0.5f;
    [SerializeField] bool speedUpWhenEating = true;
    
    public SnakeGame Model { get; private set; } = SnakeGame.NewGameFrom((2, 2));
    float CurrentStepFrequency => stepFrequencyToBeginWith / (speedUpWhenEating ? 1 : Model.Snake.Count);

    async void Start()
    {
        while(!Model.GameOver)
            await OneTick();
    }

    async Task OneTick()
    {
        await Task.Delay(CurrentTimeToTick());
        Model = Model.Tick();
    }

    TimeSpan CurrentTimeToTick()
        => TimeSpan.FromSeconds(CurrentStepFrequency) * Time.timeScale;

    void Update() => ListenDirectionInput();

    void ListenDirectionInput()
        => Model = Model.LookTowards(PressedDirection() ?? Model.direction);

    static Coordinate? PressedDirection()
    {
        if(Input.GetKey(KeyCode.UpArrow))
            return Vector2Int.up.ToTuple();
        
        if(Input.GetKey(KeyCode.DownArrow))
            return Vector2Int.down.ToTuple();
        
        if(Input.GetKey(KeyCode.LeftArrow))
            return Vector2Int.left.ToTuple();
        
        if(Input.GetKey(KeyCode.RightArrow))
            return Vector2Int.right.ToTuple();
        
        return null;
    }
}