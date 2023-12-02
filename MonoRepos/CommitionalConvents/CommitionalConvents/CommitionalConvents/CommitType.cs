using System;

namespace CommitionalConvents
{
    public readonly struct CommitType
    {
        readonly string type;

        CommitType(string type) => this.type = type;

        public static CommitType Feat => new("feat");
        public static CommitType Fix => new("fix");
        public static CommitType Refactor => new("refactor");
        public static CommitType Test => new("test");

        public static CommitType Ci => new("ci");
        public static CommitType Build => new("build");
        public static CommitType Docs => new("docs");

        public static CommitType Chore => new("chore");
        public static CommitType Perf => new("perf");
        public static CommitType Style => new("style");
    }
}