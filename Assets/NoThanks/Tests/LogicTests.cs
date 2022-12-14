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

            player.Points.Should().BeNegative();
        }
        
        [Test]
        public void NoCounters_ScoreNonNegative()
        {
            var player = new Player("player1");
            
            player.Points.Should().BeGreaterOrEqualTo(0);
        }

        [Test]
        public void JustOneCard_ScoreIsThatCard()
        {
            var player = new Player("player1");
            
            player.TakeCard(new PlayingCard(new Card(26)));
            
            player.Points.Should().Be(26);
        }

        [Test]
        public void NonConsecutiveCards_AddsSumToScore()
        {
            var player = new Player("player1");
            
            player.TakeCard(new PlayingCard(new Card(26)));
            player.TakeCard(new PlayingCard(new Card(29)));
            
            player.Points.Should().Be(26 + 29);
        }

        [Test]
        public void ConsecutiveCards_AddsOnlyTheLowest()
        {
            var player = new Player("player1");
            
            player.TakeCard(new PlayingCard(new Card(26)));
            player.TakeCard(new PlayingCard(new Card(27)));
            
            player.Points.Should().Be(26);
        }

        [Test]
        public void Mixed_AddsOnlyTheLowestConsecutives()
        {
            var player = new Player("player1");
            
            player.TakeCard(new PlayingCard(new Card(14)));
            player.TakeCard(new PlayingCard(new Card(15)));
            player.TakeCard(new PlayingCard(new Card(16)));
            player.TakeCard(new PlayingCard(new Card(18)));
            player.TakeCard(new PlayingCard(new Card(19)));
            
            player.Points.Should().Be(14+18);
        }

        [Test]
        public void When_aGroup_IsCreated_Afterwards()
        {
            var player = new Player("player1");
            
            player.TakeCard(new PlayingCard(new Card(14)));
            player.TakeCard(new PlayingCard(new Card(16)));
            player.TakeCard(new PlayingCard(new Card(15)));
            
            player.Points.Should().Be(14);
        }
    }
}