using UnityEngine;

namespace Snake.Runtime.Views.WorldView
{
    internal class Fruit : MonoBehaviour
    {
        [SerializeField] GameObject fruitPrefab;
        
        void Start() => SpawnFruit();
        void Update() => MoveFruit();

        void SpawnFruit()
        {
            var fruit = Instantiate(fruitPrefab, FruitWorldPosition(), Quaternion.identity, transform);
            fruit.name = "Fruit";
        }

        static void MoveFruit()
            => GameObject.Find("Fruit").transform.localPosition = FruitWorldPosition();

        static Vector2 FruitWorldPosition()
        {
            var fruitCoords = FindObjectOfType<SharedModel>().Model.Fruit;
            var worldPosition = new Vector2(fruitCoords.X, fruitCoords.Y) - SnakeGame.MapSize.Offset();
            return worldPosition;
        }
    }
}