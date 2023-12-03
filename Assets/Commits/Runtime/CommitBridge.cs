using System;
using System.Linq;
using CommitionalConvents;
using UnityEngine;

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

        public static Color Dye(this Commit commit)
        {
            if(commit.IsSingle)
                return commit.CommitType.id.Dye();

            var major = commit.Sizes.First();
            var minor = commit.Sizes.Skip(1).First();
            return Color.LerpUnclamped(major.type.id.Dye(), minor.type.id.Dye(), minor.size / major.size);
        }
        
        public static Color Dye(this string commitTypeId)
            => commitTypeId switch
            {
                "feat" => new Color(.17f, .58f, .91f),
                "fix" => new Color(.81f, .27f, .25f),
                "refactor" => new Color(.84f, .79f, .25f),
                "test" => new Color(.23f, .78f, .28f),
                "build" => new Color(.81f, .57f, .25f),
                "chore" => new Color(.8f, .35f, .71f),
                "ci" => new Color(.5f, .25f, .8f),
                "docs" => new Color(.25f, .81f, .68f),
                "perf" => new Color(.54f, .43f, .34f),
                "style" => new Color(.25f, .26f, .81f),
                _ => throw new ArgumentOutOfRangeException(nameof(commitTypeId), commitTypeId, null)
            };
    }
}