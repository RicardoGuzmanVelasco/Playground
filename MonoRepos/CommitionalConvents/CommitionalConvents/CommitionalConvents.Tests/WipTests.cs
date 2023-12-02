using FluentAssertions;
using static CommitionalConvents.Commit.Type;

namespace CommitionalConvents.Tests;

public class WipTests
{
    [Fact]
    public void TimeSpent()
    {
        Wip.Begin().TotalTimeSpent.Should().Be(0);
        
        Wip.Begin().Spend(1f, Feat).TotalTimeSpent.Should().Be(1);
        Wip.Begin().Spend(1f, Chore).TimeSpentOn(Chore).Should().Be(1);
        Wip.Begin().Spend(1f, Ci).TimeSpentOn(Fix).Should().Be(0);
        
        Wip.Begin().Spend(1f, Chore, Feat).TotalTimeSpent.Should().Be(2);
        
        Wip.Begin().Spend(887f, Style).Normalize().TimeSpentOn(Style).Should().Be(1);
        
        Wip.Begin().Spend(340f, Build, Docs, Perf).Normalize().TotalTimeSpent.Should().Be(1);
        Wip.Begin().Spend(340f, Build, Docs, Perf).Normalize().TimeSpentOn(Build).Should().Be(1/3f);
        Wip.Begin().Spend(340f, Build, Docs, Perf).Normalize().TimeSpentOn(Docs).Should().Be(1/3f); 
        Wip.Begin().Spend(340f, Build, Docs, Perf).Normalize().TimeSpentOn(Perf).Should().Be(1/3f);
    }

    [Fact]
    public void CommitWip()
    {
        Wip.Begin().Commit().IsNone.Should().BeTrue();
        Wip.Begin().Spend(1.45f, Chore).Commit().IsNone.Should().BeFalse();
    }
}