using System;
using System.Collections.Generic;
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
        for(var y = SnakeGame.MapSize - 1; y >= 0; y--)
        {
            for(var x = 0; x < SnakeGame.MapSize; x++)
                result.Append(Print(x, y, GameOrLastTickWhenIsGameOver()));

            result.AppendLine();
        }

        TextualView.text = result.ToString();
    }

    SnakeGame GameOrLastTickWhenIsGameOver() => Game.GameOver ? Game.Undo().Undo() : Game;

    static string Print(int x, int y, SnakeGame snakeGame)
        => (x, y).Equals(snakeGame.Fruit)
            ? "*"
            : PrintNoFruit(x, y, snakeGame);

    static string PrintNoFruit(int x, int y, SnakeGame game)
        => game.ExistsSnakeAt((x, y))
            ? PrintSnake(x, y, game.Snake)
            : "·";

    static string PrintSnake(int x, int y, IEnumerable<Coordinate> snake)
        => (x, y).Equals(snake.First())
            ? "O"
            : "o";
}