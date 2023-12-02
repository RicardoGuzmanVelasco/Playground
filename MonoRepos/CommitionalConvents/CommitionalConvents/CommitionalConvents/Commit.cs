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

        public Commit.Type CommitType
            => IsSingle ? sizesByType.Keys.Single() : Mutate();

        public static Commit Empty => new();

        public Commit And(float weight, Commit.Type type)
            => this with { sizesByType = new(sizesByType) { [type] = weight } };

        public float SizeOf(Type type)
            => sizesByType.TryGetValue(type, out var weight) ? weight : 0;
    }
}