using FluentAssertions;
using static CommitionalConvents.Commit.Type;

namespace CommitionalConvents.Tests;

public class WipTests
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

    [Test]
    public void Commit_KeepWeights()
    {
        Wip.Begin().Spend(.2f, Ci).Spend(.1f, Docs).Commit().Match
        (
            None: Assert.Fail,
            Some: c => c[Style].Should().Be(0)
        );
        
        Wip.Begin().Spend(.2f, Ci).Spend(.1f, Docs).Commit().Match
        (
            None: Assert.Fail,
            Some: c => c[Docs].Should().BeApproximately(1/3f, .001f)
        );
    }
    
    [Test]
    public void Commit_OfSingle_KeepsTheType()
    {
        Wip.Begin().Spend(.2f, Ci).Commit().Match
        (
            None: Assert.Fail,
            Some: c => c.CommitType.Should().Be(Ci)
        );
        
        Wip.Begin().Spend(.2f, Ci).Commit().Match
        (
            None: Assert.Fail,
            Some: c => c.CommitType.Should().NotBe(Style)
        );
    }
}