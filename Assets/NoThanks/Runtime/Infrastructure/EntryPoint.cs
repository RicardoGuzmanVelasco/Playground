using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoThanks.Runtime.Application;
using NoThanks.Runtime.Domain;
using TMPro;
using UnityEngine;

namespace NoThanks.Runtime.Infrastructure
{
    public class EntryPoint : MonoBehaviour
    {
        async void Start()
        {
            var players = new Player[]
            {
                new Player("Posadas"),
                new Player("Rocío"),
                new Player("Yo")
            };

            var table = new Table();
            var dealer = new Dealer(table);

            var deck = await Setup(dealer, players);

            var currentPlayer = 0;
            while(!deck.IsGone())
            {
                await PlayRound(currentPlayer, deck);
                currentPlayer = (currentPlayer + 1) % players.Length;
            }

            async Task PlayRound(int currentPlayer, Deck deck)
            {
                await dealer.FlipOverTopCard();
                var takeCard = await table.ListenIfTakeCard(players[currentPlayer]);
                UnityEngine.Object.FindObjectOfType<TMP_Text>().text = $"{players[currentPlayer]} elige quedarse la carta: {takeCard}";
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        static async Task<Deck> Setup(Dealer dealer, params Player[] players)
        {
            var stack = await dealer.ShuffleStack();
            await dealer.SupplyCounters(players);
            return stack;
        }
    }
}