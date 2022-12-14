using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NoThanks.Runtime.Domain
{
    public class Deck : IEnumerable<Card>
    {
        readonly List<Card> cards;
        
        public Deck(IEnumerable<Card> cards)
        {
            this.cards = cards.ToList();
        }

        public static IEnumerable<Card> AllAvailableCards()
        {
            for(var i = 3; i <= 35; i++)
                yield return new Card(i);
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