using System;
using System.Linq;
using System.Text;
using DG.Tweening;
using Snake;
using TMPro;
using UnityEngine;

public class TextualSnake : MonoBehaviour
{
    TMP_Text GameOverLabel => GetComponentsInChildren<TMP_Text>(includeInactive: true)
        .Single(x => x.name == "GameOverLabel");
    TMP_Text TextualView => GetComponentsInChildren<TMP_Text>()
        .Single(x => x.name == "TextualView");
    
    SnakeGame Game => FindObjectOfType<SharedModel>().Model;

    void Update()
    {
        if(GameOverLabel.gameObject.activeInHierarchy)
            return;
        
        if(Game.GameOver)
            PrintGameOver();
        else
            PrintGame();
    }

    void PrintGameOver()
    {
        GameOverLabel.gameObject.SetActive(true);
        GameOverLabel.transform.DOPunchScale(Vector3.one * 0.1f, duration: 2f);
        TextualView.DOColor(Color.gray, 2f);
    }

    void PrintGame()
    {
        var result = new StringBuilder();
        for(var y = 0; y < SnakeGame.MapSize; y++)
        {
            for(var x = 0; x < SnakeGame.MapSize; x++)
                result.Append(Print(x, y));

            result.AppendLine();
        }
        
        TextualView.text = result.ToString();
    }

    string Print(int x, int y) => (x, y).Equals(Game.Fruit) ? "*" : PrintNoFruit(x, y);
    string PrintNoFruit(int x, int y) => Game.ExistsSnakeAt((x, y)) ? PrintSnake(x, y) : "·";
    string PrintSnake(int x, int y) => (x, y).Equals(Game.Snake.First()) ? "O" : "o";
}