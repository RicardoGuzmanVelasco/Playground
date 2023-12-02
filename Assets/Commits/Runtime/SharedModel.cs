using CommitionalConvents;
using UnityEngine;

namespace Commits.Runtime
{
    public class SharedModel : MonoBehaviour
    {
        const int HardcodedConstantToReplace = 5;
        
        public Wip Wip { get; private set; } = Wip.Begin();
        
        public bool CanCommit => Wip.TotalTimeSpent >= HardcodedConstantToReplace;
        public float ProportionToCommit => Wip.TotalTimeSpent / HardcodedConstantToReplace;
        public float MinTimeToCommit => HardcodedConstantToReplace;

        public void Inject(float time, Commit.Type type) => Wip = Wip.Spend(time, type);
    }
}