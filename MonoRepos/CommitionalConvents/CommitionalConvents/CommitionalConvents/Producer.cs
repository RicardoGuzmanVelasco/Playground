using System;
using System.Linq;
using LanguageExt;

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
            
            return (Option<Issue>.None, commit);

            throw new NotImplementedException();
        }
    }
}