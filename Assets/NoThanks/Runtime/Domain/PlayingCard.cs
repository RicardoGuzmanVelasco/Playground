using System.Collections.Generic;

namespace NoThanks.Runtime.Domain
{
    public class PlayingCard
    {
        public Card card;
        public int counters;
        
        public PlayingCard(Card card)
        {
            this.card = card;
        }

        public void PutOntoOneCounter()
        {
            counters++;
        }
        
        public override string ToString()
        {
            return card.ToString();
        }
    }
}