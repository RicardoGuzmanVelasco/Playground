using System;
using System.Collections.Generic;
using System.Linq;

namespace CommitionalConvents
{
    public partial record Commit
    {
        Dictionary<Commit.Type, float> sizesByType = new();
        
        public float TotalSize => sizesByType.Values.Sum();
        public bool IsSingle => sizesByType.Count == 1;
        public IEnumerable<(Commit.Type type, float size)> Sizes
            => sizesByType
                .Select(kv => (kv.Key, kv.Value))
                .OrderByDescending(kv => kv.Item2);

        public Commit.Type CommitType
            => IsSingle ? sizesByType.Keys.Single() : Mutate();
        
        public IEnumerable<Commit.Type> AllTypes
            => sizesByType.Keys;

        public static Commit Stage(float size, Type type)
            => Empty.And(size, type);
        public static Commit Empty => new();

        public Commit And(float size, Commit.Type type)
            => this with { sizesByType = new(sizesByType) { [type] = size } };

        public float SizeOf(Type type)
            => sizesByType.TryGetValue(type, out var weight) ? weight : 0;
    }
}