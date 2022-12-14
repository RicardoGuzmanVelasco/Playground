using System.Collections.Generic;

namespace NoThanks.Runtime.Domain
{
    public class Player
    {
        List<Card> cards;
        List<Counter> counters;
        
        public int Score()
        {
            var score = 0;
            foreach (var card in cards)
            {
                score += card.Points;
            }
            foreach (var counter in counters)
            {
                score -= counter.Points;
            }
            return score;
        }
    }
}