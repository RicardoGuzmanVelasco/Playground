using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

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