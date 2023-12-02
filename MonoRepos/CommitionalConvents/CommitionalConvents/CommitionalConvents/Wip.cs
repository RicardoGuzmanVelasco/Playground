using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using static CommitionalConvents.Commit;

namespace CommitionalConvents
{
    public record Wip
    {
        Dictionary<Commit.Type, float> timeSpent = new();

        public float TotalTimeSpent => timeSpent.Values.Sum();

        Wip() { }
        public static Wip Begin() => new();

        public float TimeSpentOn(Commit.Type type)
            => timeSpent.GetValueOrDefault(type);

        public Wip Spend(float time, params Commit.Type[] types)
            => types.Aggregate(this, (current, type) => current.Spend(time, type));

        public Wip Spend(float time, Commit.Type type)
            => this with
            {
                timeSpent = new(timeSpent) { [type] = timeSpent.GetValueOrDefault(type) + time }
            };

        public Wip Normalize()
            => this with
            {
                timeSpent = timeSpent.ToDictionary(kv => kv.Key, kv => kv.Value / TotalTimeSpent)
            };

        public Option<Commit> Commit()
            => timeSpent.Any()
                ? timeSpent.Aggregate(Empty, (current, p)
                    => current.And(p.Value, p.Key))
                : Option<Commit>.None;
    }
}