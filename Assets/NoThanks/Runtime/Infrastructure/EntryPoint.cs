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
                await PlayRound();
            }

            async Task PlayRound()
            {
                var card = await dealer.FlipOverTopCard();

                bool takeCard = false;
                do
                {
                    takeCard = await table.ListenIfTakeCard(players[currentPlayer]);

                    if(takeCard)
                        await dealer.GiveCardTo(players[currentPlayer], card);
                    else
                        await dealer.AskPlayerForCounter(players[currentPlayer], card);
                    currentPlayer = (currentPlayer + 1) % players.Length;
                } while(!takeCard);
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