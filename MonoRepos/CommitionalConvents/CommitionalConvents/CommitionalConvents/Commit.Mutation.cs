using System;

namespace CommitionalConvents
{
    public partial record Commit
    {
        public static Commit.Type MutationOf(Commit.Type first, Commit.Type second)
        {
            return default;
        }
    }
}