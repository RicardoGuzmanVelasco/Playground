using System;
using LanguageExt;
using static LanguageExt.Option<CommitionalConvents.Commit>;

namespace CommitionalConvents
{
    public record Staging
    {
        readonly Wip wip;
        
        public float TotalTimeToComplete => wip.TotalTimeSpent;
        public float Eta { get; private set; }
        public float ProportionDone => Eta / wip.TotalTimeSpent;
        
        public bool Done => Eta <= 0;
        public Option<Commit> Result => Done ? wip.Commit() : None;

        Staging(Wip wip)
        {
            this.wip = wip;
            Eta = wip.TotalTimeSpent;
        }
        public static Staging DoWith(Wip wip) => new(wip);

        public Staging Inject(float time)
            => this with { Eta = Math.Max(0, Eta - time) };
    }
}