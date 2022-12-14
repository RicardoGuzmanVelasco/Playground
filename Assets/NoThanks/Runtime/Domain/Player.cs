using System.Collections.Generic;
using System.Linq;

namespace NoThanks.Runtime.Domain
{
    public class Player : Score
    {
        readonly string id;

        readonly CardGroups cards = new();
        readonly List<Counter> counters = new();

        public Player(string id)
        {
            this.id = id;
        }

        public override int Points => cards.Points + counters.Sum(c => c.Points);


        public bool CanDecide()
        {
            return counters.Count > 0;
        }

        public void SupplyCounter()
        {
            counters.Add(new Counter());
        }

        public void SupplyCounters(int howMany)
        {
            for(var i = 0; i < howMany; i++)
                counters.Add(new Counter());
        }

        public void SubstractCounter()
        {
            counters.RemoveAt(0);
        }

        public void TakeCard(PlayingCard card)
        {
            cards.Add(card.Card);
            for(var i = 0; i < card.Counters; i++)
                counters.Add(new Counter());
        }

        public override string ToString()
        {
            return id;
        }
    }
}