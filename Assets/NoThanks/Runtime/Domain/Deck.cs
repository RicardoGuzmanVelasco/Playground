using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NoThanks.Runtime.Domain
{
    public class Deck : IEnumerable<Card>
    {
        readonly List<Card> cards;
        
        Deck(IEnumerable<Card> cards)
        {
            this.cards = cards.ToList();
        }

        public static Deck Complete()
        {
            var result = new List<Card>();
            for(var i = 3; i <= 35; i++)
                result.Add(new Card(i));
            return new Deck(result);
        }

        public Card FlipOver()
        {
            var card = cards[0];
            cards.RemoveAt(0);
            return card;
        }
        
        public bool IsGone()
        {
            return cards.Count == 0;
        }

        public void Shuffle(Random random)
        {
            for(var i = 0; i < cards.Count; i++)
            {
                var j = random.Next(i, cards.Count);
                (cards[i], cards[j]) = (cards[j], cards[i]);
            }
        }

        public void DiscardSurplus()
        {
            for(var i = 0; i < 9; i++)
                cards.RemoveAt(0);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return cards.GetEnumerator();
        }
    }
}