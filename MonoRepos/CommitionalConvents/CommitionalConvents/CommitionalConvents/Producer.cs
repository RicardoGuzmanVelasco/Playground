using LanguageExt;

namespace CommitionalConvents
{
    public record Producer
    {
        public static Producer Basic => new();

        public bool Ignores(Issue issue, Commit commit)
            => !issue.CounterBy(commit);

        public (Option<Issue> issue, Option<Commit> commit)
            Review(Issue issue, Commit commit)
            => (issue.Diminish(commit), commit.Diminish(issue.Size, issue.IssueType.counter));
    }
}