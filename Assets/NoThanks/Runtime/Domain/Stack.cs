using System;
using System.Collections.Generic;

namespace NoThanks.Runtime.Domain
{
    public class Stack
    {
        //tiene una colección de cards
        //tienen que ser 33 menos 9 que se excluyen antes.
        //le pides flip over y te devuelve la carta que está en la parte superior.
        //le puedes preguntar si is gone.
        
        private readonly List<Card> cards;
        
        public Stack()
        {
            throw new NotImplementedException();
        }
        
        public Card FlipOver()
        {
            throw new NotImplementedException();
        }
        
        public bool IsGone()
        {
            throw new NotImplementedException();
        }
    }
}