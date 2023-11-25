using System;
using System.Text;
using Snake;
using TMPro;
using UnityEngine;

public class TextualSnake : MonoBehaviour
{
    const int HardcodedMapSize = 10;
    
    SnakeGame Game => FindObjectOfType<SharedModel>().Model;

    void Update()
    {
        if(Game.GameOver)
            PrintGameOver();
        else
            PrintGame();
    }

    void PrintGameOver()
    {
        GetComponentInChildren<TMP_Text>().text = "Game Over";
    }

    void PrintGame()
    {
        var result = new StringBuilder();
        for(var y = HardcodedMapSize; y > -HardcodedMapSize; y--)
        {
            for(var x = HardcodedMapSize; x > -HardcodedMapSize; x--)
                if(Game.Fruit.Equals((Coordinate)(x, y)))
                    result.Append("*");
                else
                    result.Append(Game.ExistsSnakeAt((x, y)) ? "o" : "·");

            result.AppendLine();
        }
        
        GetComponentInChildren<TMP_Text>().text = result.ToString();
    }
}