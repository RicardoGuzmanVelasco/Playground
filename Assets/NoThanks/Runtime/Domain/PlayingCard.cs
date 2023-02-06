namespace NoThanks.Runtime.Domain
{
    public class PlayingCard
    {
        public Card Card { get; }
        public int Counters { get; private set; }
        
        public PlayingCard(Card card)
        {
            this.Card = card;
        }

        public void PutOneCounterOnto()
        {
            Counters++;
        }
        
        public override string ToString()
        {
            return Card.ToString();
        }
    }
}