using System;
using CommitionalConvents;
using UnityEngine;

namespace Commits.Runtime
{
    public class SharedModel : MonoBehaviour
    {
        const int HardcodedConstantToReplace = 1;
        
        public Wip Wip { get; private set; } = Wip.Begin();
        
        public Staging Staging { get; private set; }
        public bool IsStaging => Staging != null;
        
        public bool CanCommit => Wip.TotalTimeSpent >= HardcodedConstantToReplace;
        public float ProportionToCommit => Wip.TotalTimeSpent / HardcodedConstantToReplace;
        public float MinTimeToCommit => HardcodedConstantToReplace;
        
        void Update() => InjectStaging(Time.deltaTime);

        public void Inject(float time, Commit.Type type) => Wip = Wip.Spend(time, type);
        public void InjectStaging(float time)
        {
            if(!IsStaging)
                return;
            
            Staging = Staging.Inject(time);
            if(Staging.Done)
                Staging = null;
        }

        public void Commit()
        {
            Staging = Staging.DoWith(Wip);
            Wip = Wip.Begin();
        }

    }
}