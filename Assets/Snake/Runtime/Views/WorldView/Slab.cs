using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Snake.Runtime.Views.WorldView
{
    internal class Slab : MonoBehaviour
    {
        [SerializeField] Sprite[] slabSprites;

        void Start()
        {
            Assert.IsNotNull(slabSprites);
            Assert.IsTrue(slabSprites.Any());
        
            var randomSlab = slabSprites[UnityEngine.Random.Range(0, slabSprites.Length)];
            GetComponent<SpriteRenderer>().sprite = randomSlab;
        }
    }
}