using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoThanks.Runtime.Domain;
using NoThanks.Runtime.Infrastructure;

namespace NoThanks.Runtime.Application
{
    public class Dealer
    {
        Deck deck;
        readonly Player[] players;

        readonly Table table;
        
        public Dealer(Table table, IEnumerable<Player> players)
        {
            this.table = table;
            this.players = players.ToArray();
        }

        public async Task SupplyCounters()
        {
            var counters = players.Length switch
            {
                >= 3 and <= 5 => 11,
                6 => 9,
                7 => 7,
                _ => throw new NotSupportedException()
            };

            foreach(var player in players)
            {
                player.SupplyCounters(counters);
                await table.SupplyCountersToPlayer(counters, player);
            }
        }

        public async Task<Deck> SetupDeck()
        {
            deck = Deck.Complete();
            deck.Shuffle(new Random());
            deck.DiscardSurplus();

            await table.SetupDeck(deck);
            return deck;
        }

        public async Task<PlayingCard> FlipOverTopCard()
        {
            var card = deck.FlipOver();
            await table.Show(card);
            return new PlayingCard(card);
        }

        public Task GiveCardTo(Player player, PlayingCard card)
        {
            player.TakeCard(card);
            return table.GiveCardToPlayer(card, player);
        }

        public Task AskPlayerForCounter(Player player, PlayingCard card)
        {
            player.SubstractCounter();
            card.PutOneCounterOnto();
            return table.PutCounterOnCard(card, player);
        }

        public async Task AddsPointsUp()
        {
            await table.NotifyGameOver();

            foreach(var player in players)
                await table.ShowPointsOf(player);

            var winner = players.OrderBy(p => p.Points).First();
            await table.NotifyWinner(winner);
        }
    }
}