using System;
using System.Collections;
using System.Collections.Generic;

namespace NoThanks.Runtime.Domain
{
    public class Deck : IEnumerable<Card>
    {
        //tiene una colección de cards
        //tienen que ser 33 menos 9 que se excluyen antes.
        //le pides flip over y te devuelve la carta que está en la parte superior.
        //le puedes preguntar si is gone.
        
        private readonly IReadOnlyList<Card> cards;
        
        public Deck(IReadOnlyList<Card> cards)
        {
            this.cards = cards;
        }
        
        public Card FlipOver()
        {
            throw new NotImplementedException();
        }
        
        public bool IsGone()
        {
            throw new NotImplementedException();
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