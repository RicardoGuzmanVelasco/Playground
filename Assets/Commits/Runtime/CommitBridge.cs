using System;
using CommitionalConvents;

namespace Commits.Runtime
{
    public static class CommitBridge
    {
        public static Commit.Type ToCommitType(this string commitType)
            => commitType switch
            {
                "feat" => Commit.Type.Feat,
                "fix" => Commit.Type.Fix,
                "refactor" => Commit.Type.Refactor,
                "test" => Commit.Type.Test,
                "ci" => Commit.Type.Ci,
                "build" => Commit.Type.Build,
                "docs" => Commit.Type.Docs,
                "chore" => Commit.Type.Chore,
                "perf" => Commit.Type.Perf,
                "style" => Commit.Type.Style,
                _ => throw new ArgumentOutOfRangeException(nameof(commitType), commitType, null)
            };
    }
}