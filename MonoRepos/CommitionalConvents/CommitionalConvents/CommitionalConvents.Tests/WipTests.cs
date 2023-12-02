using FluentAssertions;
using static CommitionalConvents.CommitType;

namespace CommitionalConvents.Tests;

public class WipTests
{
    [Fact]
    public void TimeSpent()
    {
        Wip.Begin().TotalTimeSpent.Should().Be(0);
        
        Wip.Begin().Spend(Feat(), 1f).TotalTimeSpent.Should().Be(1);
        Wip.Begin().Spend(Feat(), 1f).TimeSpentOn(Feat()).Should().Be(1);
        Wip.Begin().Spend(Feat(), 1f).TimeSpentOn(Fix()).Should().Be(0);
    }
}