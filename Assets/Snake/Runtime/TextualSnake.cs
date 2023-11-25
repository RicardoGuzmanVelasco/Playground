using System;
using System.Linq;
using System.Text;
using Snake;
using TMPro;
using UnityEngine;

public class TextualSnake : MonoBehaviour
{
    const int HardcodedMapSize = 10;

    TMP_Text GameOverLabel => GetComponentsInChildren<TMP_Text>(includeInactive: true)
        .Single(x => x.name == "GameOverLabel");
    TMP_Text TextualView => GetComponentsInChildren<TMP_Text>()
        .Single(x => x.name == "TextualView");
    
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
        GameOverLabel.gameObject.SetActive(true);
        TextualView.color = Color.gray;
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
        
        TextualView.text = result.ToString();
    }
}