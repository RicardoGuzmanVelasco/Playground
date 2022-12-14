using System;
using FluentAssertions;
using NoThanks.Runtime.Domain;
using NUnit.Framework;

namespace NoThanks.Tests
{
    public class LogicTests
    {
        [Test]
        public void JustCounters_ScoreIsNegative()
        {
            var player = new Player("player1");
            
            player.SupplyCounter();

            player.Score().Should().BeNegative();
        }
        
        [Test]
        public void NoCounters_ScoreNonNegative()
        {
            var player = new Player("player1");
            
            player.Score().Should().BeGreaterOrEqualTo(0);
        }
    }
}