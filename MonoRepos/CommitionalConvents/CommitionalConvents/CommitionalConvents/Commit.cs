using System.Collections.Generic;

namespace CommitionalConvents
{
    public partial record Commit
    {
        Dictionary<Commit.Type, float> distribution = new();
        
        public bool IsSingle => distribution.Count == 1;
        
        public static Commit Empty => new();
        
        public Commit And(float weight, Commit.Type type)
        => this with
        {
            distribution = new(distribution) { [type] = weight }
        };

        public float this[Type type]
            => distribution.TryGetValue(type, out var weight) ? weight : 0;
    }
}