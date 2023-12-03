using System.Linq;

namespace CommitionalConvents
{
    public partial record Issue
    {
        public record Type
        {
            public readonly string id;
            public readonly Commit.Type counter;
            
            Type(string id, Commit.Type counter)
            {
                this.id = id;
                this.counter = counter;
            }

            public static Type ToDo => new("todo", Commit.Type.Feat);
            public static Type Bug => new("bug", Commit.Type.Fix);
            public static Type Debt => new("debt", Commit.Type.Refactor);
            public static Type Qa => new("qa", Commit.Type.Test);
            
            public static Type DevOps => new("devops", Commit.Type.Ci);
            public static Type Plugin => new("plugin", Commit.Type.Build);
            public static Type Doubt => new("doubt", Commit.Type.Docs);
            
            public static Type Stuff => new("stuff", Commit.Type.Chore);
            public static Type Lag => new("lag", Commit.Type.Perf);
            public static Type Guide => new("guide", Commit.Type.Style);
        }
    }
}