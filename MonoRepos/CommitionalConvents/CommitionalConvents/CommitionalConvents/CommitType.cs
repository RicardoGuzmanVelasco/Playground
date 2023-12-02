using System;

namespace CommitionalConvents
{
    public static class Commit
    {
        public readonly struct Type
        {
            readonly string type;

            Type(string type) => this.type = type;

            public static Type Feat => new("feat");
            public static Type Fix => new("fix");
            public static Type Refactor => new("refactor");
            public static Type Test => new("test");

            public static Type Ci => new("ci");
            public static Type Build => new("build");
            public static Type Docs => new("docs");

            public static Type Chore => new("chore");
            public static Type Perf => new("perf");
            public static Type Style => new("style");
        }
    }
}