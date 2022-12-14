namespace NoThanks.Runtime.Domain
{
    public class Card : Score
    {
        public Card(int points)
        {
            Points = points;
        }
        
        public override string ToString()
        {
            return Points.ToString();
        }
    }
}