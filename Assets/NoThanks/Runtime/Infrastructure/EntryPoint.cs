using System;
using NoThanks.Runtime.Application;
using NoThanks.Runtime.Domain;
using UnityEngine;

namespace NoThanks.Runtime.Infrastructure
{
    public class EntryPoint : MonoBehaviour
    {
        async void Start()
        {
            var player1 = new Player("Posadas");
            var player2 = new Player("Rocío");
            var player3 = new Player("Yo");

            var table = new Table();

            var supply = new SupplyCounters(table);

            await supply.Execute(player1, player2, player3);
        }
    }
}