using System.Collections.Generic;

namespace CommitionalConvents
{
    public record Wip
    {
        Dictionary<CommitType, float> timeSpent = new();

        public float TotalTimeSpent { get; private set; }

        Wip() { }
        public static Wip Begin() => new();

        public Wip Spend(CommitType type, float time)
            => this with
            {
                timeSpent = new(timeSpent)
                {
                    [type] = timeSpent.GetValueOrDefault(type) + time
                },
                TotalTimeSpent = TotalTimeSpent + time
            };

        public float TimeSpentOn(CommitType type)
            => timeSpent.GetValueOrDefault(type);
    }
}