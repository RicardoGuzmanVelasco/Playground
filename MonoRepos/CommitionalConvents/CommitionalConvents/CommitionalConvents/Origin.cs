using System.Collections.Generic;
using static System.Array;

namespace CommitionalConvents
{
    public record Origin
    {
        Issue[] issues;
        Commit[] commits;
        
        Origin(Issue[] issues, Commit[] commits)
        {
            this.issues = issues;
            this.commits = commits;
        }
        public static Origin Fresh => new(Empty<Issue>(), Empty<Commit>());
        public float TechDebtProportion => IssuesToCommitsRatio;

        public Origin Push(Issue issue)
            => this with { issues = new List<Issue>(issues) { issue }.ToArray() };
        public Origin Push(Commit commit)
            => this with { commits = new List<Commit>(commits) { commit }.ToArray() };
        
        
        /// Ya me preocuparé de la fórmula.
        float IssuesToCommitsRatio => issues.Length / (float) commits.Length;
    }
}