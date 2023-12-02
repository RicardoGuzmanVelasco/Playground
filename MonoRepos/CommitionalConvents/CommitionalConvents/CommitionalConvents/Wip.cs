using System.Collections.Generic;
using System.Linq;

namespace CommitionalConvents
{
    public record Wip
    {
        Dictionary<CommitType, float> timeSpent = new();

        public float TotalTimeSpent { get; private set; }

        Wip() { }
        public static Wip Begin() => new();
        
        public float TimeSpentOn(CommitType type)
            => timeSpent.GetValueOrDefault(type);
        
        public Wip Spend(float time, CommitType type)
            => this with
            {
                timeSpent = new(timeSpent)
                {
                    [type] = timeSpent.GetValueOrDefault(type) + time
                },
                TotalTimeSpent = TotalTimeSpent + time
            };

        public Wip Spend(float time, params CommitType[] types)
            => types.Aggregate(this, (current, type) => current.Spend(time, type));
    }
}