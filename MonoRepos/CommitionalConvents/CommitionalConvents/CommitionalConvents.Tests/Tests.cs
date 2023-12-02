using FluentAssertions;
using static CommitionalConvents.Commit;
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

    [Test]
    public void Commit_KeepSizes()
    {
        Wip.Begin().Spend(.4f, Ci).Spend(.2f, Fix).Commit().Match
        (
            None: Assert.Fail,
            Some: c => c.TotalSize.Should().Be(.6f)
        );
        Wip.Begin().Spend(.2f, Ci).Spend(.1f, Docs).Commit().Match
        (
            None: Assert.Fail,
            Some: c => c.SizeOf(Style).Should().Be(0)
        );
        
        Wip.Begin().Spend(.2f, Ci).Spend(.1f, Docs).Commit().Match
        (
            None: Assert.Fail,
            Some: c => c.SizeOf(Docs).Should().Be(.1f)
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
    
    [Test]
    public void Commit_OfMultiple_MutatesTheType()
    {
        Wip.Begin().Spend(.2f, Ci).Spend(.1f, Docs).Commit().Match
        (
            None: Assert.Fail,
            Some: c => c.CommitType.Should().Be(MutationOf(Ci, Docs))
        );
        
        Wip.Begin().Spend(.1f, Ci).Spend(.2f, Docs).Commit().Match
        (
            None: Assert.Fail,
            Some: c => c.CommitType.Should().Be(MutationOf(Docs, Ci))
        );
    }

    [Test]
    public void Staging_StartsAtWipSize()
    {
        Staging.DoWith(Wip.Begin().Spend(2.11f, Ci)).Eta.Should().Be(2.11f);
    }

    [Test]
    public void Staging_ReducesEta_OverTime()
    {
        Staging.DoWith(Wip.Begin().Spend(983.7f, Ci))
            .Inject(1f)
            .Eta.Should().Be(982.7f);
    }
    
    [Test]
    public void Staging_ReducesEta_UntilZero()
    {
        Staging.DoWith(Wip.Begin().Spend(1f, Ci))
            .Inject(1f)
            .Eta.Should().Be(0);

        Staging.DoWith(Wip.Begin().Spend(1f, Ci))
            .Inject(234f)
            .Eta.Should().Be(0);
    }

    [Test]
    public void Staging_Done_WhenEtaIsNotPositive()
    {
        Staging.DoWith(Wip.Begin().Spend(1f, Ci))
            .Inject(1f)
            .Done.Should().BeTrue();
        
        Staging.DoWith(Wip.Begin().Spend(1f, Ci))
            .Inject(0.9999f)
            .Done.Should().BeFalse();
        
        Staging.DoWith(Wip.Begin().Spend(1f, Ci))
            .Inject(234f)
            .Done.Should().BeTrue();
    }
}