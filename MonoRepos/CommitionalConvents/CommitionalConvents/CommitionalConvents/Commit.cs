using System.Collections.Generic;

namespace CommitionalConvents
{
    public partial class Commit
    {
        readonly Dictionary<Commit.Type, float> distribution = new();
        
        public static Commit Single(Commit.Type type)
            => new() { distribution = { [type] = 1 } };
    }
}