using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NoThanks.Runtime.Domain
{
    public class Deck : IEnumerable<Card>
    {
        readonly List<Card> cards;
        
        public Deck(IReadOnlyList<Card> cards)
        {
            this.cards = cards.ToList();
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