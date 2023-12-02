using LanguageExt;

namespace CommitionalConvents
{
    public partial record Commit
    {
        public static Commit.Type MutationOf(Commit.Type first, Commit.Type second)
            => ExistingMutation(first, second)
                .Match(Some: x => x, None: first);

        static Option<Type> ExistingMutation(Type first, Type second)
            => (first.id + second.id) switch
            {
                "feat" + "refactor" => new Commit.Type("featactor"),
                "refactor" + "feat" => new Commit.Type("refeat"),
                "chore" + "fix" => new Commit.Type("chorix"),
                _ => Option<Type>.None
            };
    }
}