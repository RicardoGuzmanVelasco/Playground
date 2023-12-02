using FluentAssertions;
using static CommitionalConvents.Commit.Type;

namespace CommitionalConvents.Tests;

public class Tests
{
    [Test]
    public void TimeSpent()
    {
        Wip.Begin().TotalTimeSpent.Should().Be(0);

        Wip.Begin().Spend(1f, Feat).TotalTimeSpent.Should().Be(1);
        Wip.Begin().Spend(1f, Chore).TimeSpentOn(Chore).Should().Be(1);
        Wip.Begin().Spend(1f, Ci).TimeSpentOn(Fix).Should().Be(0);

        Wip.Begin().Spend(1f, Chore, Feat).TotalTimeSpent.Should().Be(2);

        Wip.Begin().Spend(887f, Style).Normalize().TimeSpentOn(Style).Should().Be(1);

        Wip.Begin().Spend(340f, Build, Docs, Perf).Normalize().TotalTimeSpent.Should().Be(1);
        Wip.Begin().Spend(340f, Build, Docs, Perf).Normalize().TimeSpentOn(Build).Should().Be(1 / 3f);
        Wip.Begin().Spend(340f, Build, Docs, Perf).Normalize().TimeSpentOn(Docs).Should().Be(1 / 3f);
        Wip.Begin().Spend(340f, Build, Docs, Perf).Normalize().TimeSpentOn(Perf).Should().Be(1 / 3f);
    }

    [Test]
    public void CommitWip()
    {
        Wip.Begin().Commit().IsNone.Should().BeTrue();
        Wip.Begin().Spend(1.45f, Chore).Commit().IsNone.Should().BeFalse();

        Wip.Begin().Spend(3245, Ci).Commit().Match
        (
            Some: c => c.IsSingle.Should().BeTrue(),
            None: Assert.Fail
        );
        
        Wip.Begin().Spend(1.77f, Ci).Spend(.45f, Chore).Commit().Match
        (
            Some: c => c.IsSingle.Should().BeFalse(),
            None: Assert.Fail
        );
    }
}