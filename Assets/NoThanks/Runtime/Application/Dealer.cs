﻿using System;
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
        readonly Table table;
        
        public Dealer(Table table)
        {
            this.table = table;
        }

        public async Task SupplyCounters(params Player[] players)
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

        public async Task<Deck> ShuffleStack()
        {
            var random = new Random();
            
            var cards = CreateCards();
            Shuffle(cards, random);
            ExcludeNineCards(cards);

            deck = new Deck(cards);
            await table.FormDeck(deck);
            return deck;
        }

        public async Task<PlayingCard> FlipOverTopCard()
        {
            var card = deck.FlipOver();
            await table.Show(card);
            return new PlayingCard(card);
        }

        static void ExcludeNineCards(List<Card> cards)
        {
            for(var i = 0; i < 9; i++)
                cards.RemoveAt(0);
        }

        static void Shuffle(List<Card> cards, Random random)
        {
            for(var i = 0; i < cards.Count; i++)
            {
                var j = random.Next(i, cards.Count);
                (cards[i], cards[j]) = (cards[j], cards[i]);
            }
        }

        List<Card> CreateCards()
        {
            var result = new List<Card>();
            for(var i = 3; i <= 35; i++)
                result.Add(new Card(i));
            return result;
        }

        public Task GiveCardTo(Player player, PlayingCard card)
        {
            player.TakeCard(card);
            return table.GiveCardToPlayer(card, player);
        }

        public Task AskPlayerForCounter(Player player, PlayingCard card)
        {
            player.SubstractCounter();
            card.counters++;
            return table.PutCounterOnCard(card, player);
        }

        public async Task AddsPointsUp(IReadOnlyList<Player> players)
        {
            await table.NotifyGameOver();

            foreach(var player in players)
                await table.ShowPointsOf(player);

            var winner = players.OrderBy(p => p.Points).First();
            await table.NotifyWinner(winner);
        }
    }
}