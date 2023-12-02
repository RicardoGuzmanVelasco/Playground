using CommitionalConvents;
using UnityEngine;

namespace Commits.Runtime
{
    public class SharedModel : MonoBehaviour
    {
        public Wip Wip { get; private set; } = Wip.Begin();
        
        public void Inject(float time, Commit.Type type) => Wip = Wip.Spend(time, type);
    }
}