using System;

namespace CommitionalConvents
{
    public record Commit
    {
        readonly string type;
        readonly string hash;

        Commit(string type) : this(type, Guid.NewGuid().ToString()) { }
        Commit(string type, string hash) => (this.type, this.hash) = (type, hash);

        public static Commit Feat() => new("feat");
        public static Commit Fix() => new("fix");
        public static Commit Refactor() => new("refactor");
        public static Commit Test() => new("test");
        
        public static Commit Ci() => new("ci");
        public static Commit Build() => new("build");
        public static Commit Docs() => new("docs");
        public static Commit Chore() => new("chore");
        
        public static Commit Perf() => new("perf");
        public static Commit Style() => new("style");

        public bool SameTypeThan(Commit other) => this.type == other.type;
    }
}