namespace Snake.Runtime
{
    struct CircularIndex
    {
        int maxIndex;
        int index;
    
        public CircularIndex(int maxIndex)
        {
            this.maxIndex = maxIndex;
            index = 0;
        }
    
        public void Increment() => index = (index + 1) % maxIndex;
    
        public static implicit operator int(CircularIndex circularIndex) => circularIndex.index;
    }
}