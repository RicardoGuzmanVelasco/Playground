namespace CommitionalConvents
{
    public partial record Issue
    {
        public readonly struct Type
        {
            public readonly string id;
            
            internal Type(string id) => this.id = id;
            
            public static Type Idea => new("idea");
            public static Type Bug => new("bug");
            public static Type Legacy => new("legacy");
            public static Type Qa => new("qa");
            
            public static Type DevOps => new("devops");
            public static Type Lag => new("lag");
        }
    }
}