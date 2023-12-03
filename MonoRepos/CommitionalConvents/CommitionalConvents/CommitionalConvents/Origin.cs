using System.Collections.Generic;

namespace CommitionalConvents
{
    public record Origin
    {
        IEnumerable<Issue> issues;
        IEnumerable<Commit> commits;
        
        Origin(IEnumerable<Issue> issues, IEnumerable<Commit> commits)
        {
            this.issues = issues;
            this.commits = commits;
        }
        public static Origin Fresh => new(new List<Issue>(), new List<Commit>());
        
        public Origin Push(Issue issue)
            => this with { issues = new List<Issue>(issues) { issue } };
        public Origin Push(Commit commit)
            => this with { commits = new List<Commit>(commits) { commit } };
    }
}