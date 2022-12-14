using System.Collections.Generic;
using System.Linq;

namespace NoThanks.Runtime.Domain
{
    public class CardGroups : Score
    {
        readonly Dictionary<int, List<Card>> consecutiveGroups;

        public CardGroups()
        {
            consecutiveGroups = new Dictionary<int, List<Card>>();
        }

        public override int Points => consecutiveGroups.Sum(k => k.Key);

        public void Add(Card card)
        {
            var dismantledGroups = consecutiveGroups.SelectMany(pair => pair.Value).ToList();
            dismantledGroups.Add(card);
            var orderedCards = new HashSet<Card>(dismantledGroups.OrderBy(c => c.Points));

            consecutiveGroups.Clear();

            foreach(var c in orderedCards)
                if(!ToExistingGroup(c))
                    consecutiveGroups.Add(c.Points, new List<Card> { c });
                else
                    consecutiveGroups[GroupOf(c)].Add(c);
        }

        int GroupOf(Card card)
        {
            return consecutiveGroups.First(group => group.Key == card.Points - group.Value.Count).Key;
        }

        bool ToExistingGroup(Card card)
        {
            if(consecutiveGroups.Count == 0)
                return false;

            return consecutiveGroups.Any(group => group.Key == card.Points - group.Value.Count);
        }
    }
}