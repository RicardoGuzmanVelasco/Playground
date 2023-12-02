using System;
using System.Collections.Generic;
using System.Linq;

namespace CommitionalConvents
{
    public partial record Commit
    {
        Dictionary<Commit.Type, float> distribution = new();

        public bool IsSingle => distribution.Count == 1;

        public Commit.Type CommitType
            => IsSingle
                ? distribution.Keys.Single()
                : Mutate();
        
        public static Commit Empty => new();

        public Commit And(float weight, Commit.Type type)
            => this with { distribution = new(distribution) { [type] = weight } };

        public float this[Type type]
            => distribution.TryGetValue(type, out var weight) ? weight : 0;

        Commit.Type Mutate()
            => MutationOf
            (
                distribution.OrderByDescending(x => x.Value).First().Key,
                distribution.OrderByDescending(x => x.Value).Skip(1).First().Key
            );
    }
}