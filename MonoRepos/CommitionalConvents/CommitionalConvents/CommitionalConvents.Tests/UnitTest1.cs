using FluentAssertions;

namespace CommitionalConvents.Tests;

public class UnitTest1
{
    [Fact]
    public void EqualityByCommitType()
    {
        Commit.Feat().SameTypeThan(Commit.Feat()).Should().BeTrue();
        Commit.Feat().SameTypeThan(Commit.Fix()).Should().BeFalse();
    }
}