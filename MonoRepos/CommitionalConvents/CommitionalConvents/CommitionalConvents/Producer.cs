using System;
using System.Linq;
using LanguageExt;
using static LanguageExt.Option<CommitionalConvents.Issue>;

namespace CommitionalConvents
{
    public record Producer
    {
        public static Producer Basic => new();
        
        public (Option<Issue> issue, Option<Commit> commit)
            Review(Issue issue, Commit commit)
        {
            if(!issue.CounterBy(commit))
                return (issue, commit);
            
            return (issue.Diminish(commit), DiminishCommit(commit));

            throw new NotImplementedException();
        }

        static Commit DiminishCommit(Commit commit)
        {
            return commit;
        }
    }
}