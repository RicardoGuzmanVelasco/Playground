using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoThanks.Runtime.Domain;
using NoThanks.Runtime.Infrastructure;

namespace NoThanks.Runtime.Application
{
    public class Dealer
    {
        Deck deck;
        readonly Table view;
        
        public Dealer(Table view)
        {
            this.view = view;
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
                await view.SupplyCountersToPlayer(counters, player);
            }
        }

        public async Task<Deck> ShuffleStack()
        {
            var random = new Random();
            
            var cards = CreateCards();
            Shuffle(cards, random);
            ExcludeNineCards(cards);

            deck = new Deck(cards);
            await view.FormDeck(deck);
            return deck;
        }

        public Task FlipOverTopCard()
        {
            var card = deck.FlipOver();
            return view.Show(card);
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
    }
}