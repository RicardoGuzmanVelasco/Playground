using System.Linq;
using LanguageExt;
using static LanguageExt.Option<CommitionalConvents.Issue>;

namespace CommitionalConvents
{
    public partial record Issue
    {
        public Issue.Type IssueType { get; private set; }
        public float Size { get; private set; }
        
        Issue(float size, Issue.Type type)
        {
            Size = size;
            IssueType = type;
        }

        public static Issue Emerge(Type type, float size)
            => new(size, type);
        
        public bool CounterBy(Commit commit)
            => commit.AllTypes.Any(t => IssueType.counter.Equals(t));

        public Option<Issue> Diminish(Commit commit)
            => IsDestroyedBy(commit)
                ? None
                : this with { Size = ResultSize(commit) };

        bool IsDestroyedBy(Commit commit) => ResultSize(commit) <= 0;
        float ResultSize(Commit commit)
            => Size - commit.SizeOf(IssueType.counter);
    }
}