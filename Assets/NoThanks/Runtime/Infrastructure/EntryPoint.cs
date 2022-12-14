using System.Threading.Tasks;
using NoThanks.Runtime.Application;
using NoThanks.Runtime.Domain;
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
            var dealer = new Dealer(table, players);

            var deck = await Setup(dealer);

            var currentPlayer = 0;
            while(!deck.IsGone())
                await PlayRound();

            await dealer.AddsPointsUp();

            async Task PlayRound()
            {
                var card = await dealer.FlipOverTopCard();

                var takeCard = false;
                while(!takeCard)
                    takeCard = await AskNextPlayer();

                async Task<bool> AskNextPlayer()
                {
                    if(!players[currentPlayer].CanDecide())
                        takeCard = true;
                    else
                        takeCard = await table.ListenIfTakeCard(players[currentPlayer]);

                    if(takeCard)
                        await dealer.GiveCardTo(players[currentPlayer], card);
                    else
                        await dealer.AskPlayerForCounter(players[currentPlayer], card);
                    
                    if(!takeCard)
                        GoToNextPlayer();
                    return takeCard;
                }

                void GoToNextPlayer()
                {
                    currentPlayer = (currentPlayer + 1) % players.Length;
                }
            }
        }

        static async Task<Deck> Setup(Dealer dealer)
        {
            var stack = await dealer.ShuffleStack();
            await dealer.SupplyCounters();
            return stack;
        }
    }
}