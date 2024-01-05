using AoC2015;
using NUnit.Framework;
using System.Collections.Generic;

namespace AoC2015Tests
{
    public class Day22Test
    {
        Day22 day22;

        [SetUp]
        public void Setup( )
        {
            day22 = new Day22(new List<string>());
        }

        private static Day22.Player TheBoss => new()
        {
            Name = "TheBoss",
            Damage = 8,
            HitPoints = 13,
        };

        private static Day22.Player ThePlayer => new()
        {
            Name = "me",
            HitPoints = 10,
            Mana = 250
        };

        private static List<(Day22.Player boss, Day22.Player player, List<string> scenario, string winner)> _testCases = new()
        {
            (TheBoss, ThePlayer,
            new List<string>
            {
                nameof(Day22.Poison),
                nameof(Day22.MagicMissile)
            }, "me"),
            (TheBoss, ThePlayer,
            new List<string>
            {
                nameof(Day22.Recharge),
                nameof(Day22.Shield),
                nameof(Day22.Drain),
                nameof(Day22.Poison),
                nameof(Day22.MagicMissile),
            }, "me"),
            (TheBoss, new Day22.Player { Mana = 10 }, new List<string> { nameof(Day22.MagicMissile) }, "TheBoss")
        };

        [TestCaseSource(nameof(_testCases))]
        public void PlayGameTest((Day22.Player boss, Day22.Player player, List<string> scenario, string winner) testCase)
        {
            day22.TheBoss = testCase.boss;
            day22.ThePlayer = testCase.player;
            var actual = day22.PlayGame(scenario: testCase.scenario);
            Assert.AreEqual(testCase.winner, actual.winner);
        }

        [Test]
        public void Part1()
        {
            var actual = day22.SolvePart1().Result;
            Assert.AreEqual("900", actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day22.SolvePart2().Result;
            Assert.AreEqual("1216", actual);
        }
    }
}