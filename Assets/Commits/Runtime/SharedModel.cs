using System;
using CommitionalConvents;
using LanguageExt;
using UnityEngine;
using static CommitionalConvents.Staging;
using static LanguageExt.Option<CommitionalConvents.Staging>;

namespace Commits.Runtime
{
    public class SharedModel : MonoBehaviour
    {
        const int HardcodedConstantToReplace = 1;

        public Wip Wip { get; private set; } = Wip.Begin();
        public Option<Staging> Staging { get; private set; }
        public Origin Origin { get; private set; } = Origin.Fresh;
        
        public event Action<Commit> StagingCompleted;

        public bool CanCommit => Wip.TotalTimeSpent >= HardcodedConstantToReplace;
        public float ProportionToCommit => Wip.TotalTimeSpent / HardcodedConstantToReplace;
        public float MinTimeToCommit => HardcodedConstantToReplace;

        void Update() => InjectStaging(Time.deltaTime);

        public void Inject(float time, Commit.Type type) => Wip = Wip.Spend(time, type);

        public void InjectStaging(float time)
        {
            Staging = Staging.Map(s => s.Inject(time));
            Staging.IfSome(x => x.Result.IfSome(Raise));
        }

        public void Commit()
        {
            Staging = DoWith(Wip);
            Wip = Wip.Begin();
        }
        
        public void Drop(Commit commit)
        {
            Origin = Origin.Drop(commit);
        }

        void Raise(Commit c)
        {
            StagingCompleted?.Invoke(c);
            Origin = Origin.Push(c);
            Staging = None;
        }
    }
}