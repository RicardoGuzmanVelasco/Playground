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

            var dealer = new Dealer(table);

            var stack = await dealer.ShuffleStack();
            await dealer.SupplyCounters(player1, player2, player3);
        }
    }
}