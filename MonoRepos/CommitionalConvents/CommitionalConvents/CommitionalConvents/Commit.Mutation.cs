using System.Linq;
using LanguageExt;
using static LanguageExt.Option<CommitionalConvents.Commit.Type>;

namespace CommitionalConvents
{
    public partial record Commit
    {
        Commit.Type Mutate()
            => MutationOf
            (
                sizesByType.OrderByDescending(x => x.Value).First().Key,
                sizesByType.OrderByDescending(x => x.Value).Skip(1).First().Key
            );

        public static Commit.Type MutationOf(Commit.Type first, Commit.Type second)
            => ExistingMutation(first, second).IfNone(first);

        static Option<Type> ExistingMutation(Type first, Type second)
            => (first.id + second.id) switch
            {
                "feat" + "refactor" => new Commit.Type("featactor"),
                "refactor" + "feat" => new Commit.Type("refeat"),
                "chore" + "fix" => new Commit.Type("chorix"),
                "refactor" + "test" => new Commit.Type("retest"),
                "test" + "refactor" => new Commit.Type("testactor"),
                _ => None
            };
    }
}