using AoC2015;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

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

        private static List<(Day22.Player boss, Day22.Player player, List<string> scenario, string winner)> _testCases = new()
        {
            (new Day22.Player
            {
                Name = "TheBoss",
                Damage = 8,
                HitPoints = 13,
            },
            new Day22.Player
            {
                Name = "me",
                HitPoints = 10,
                Mana = 250
            }, 
            new List<string>
            {
                nameof(Day22.Poison),
                nameof(Day22.MagicMissile)
            }, "me"),
            (new Day22.Player
            {
                Name = "TheBoss",
                Damage = 8,
                HitPoints = 14,
            },
            new Day22.Player
            {
                Name = "me",
                HitPoints = 10,
                Mana = 250
            },
            new List<string>
            {
                nameof(Day22.Recharge),
                nameof(Day22.Shield),
                nameof(Day22.Drain),
                nameof(Day22.Poison),
                nameof(Day22.MagicMissile),
            }, "me"),
            (new Day22.Player(), new Day22.Player{Mana = 10}, new List<string>(), "TheBoss")
        };

        [TestCaseSource(nameof(_testCases))]
        public void Part1((Day22.Player boss, Day22.Player player, List<string> scenario, string winner) testCase)
        {
            day22.Scenario = testCase.scenario;
            day22.TheBoss = testCase.boss;
            day22.ThePlayer = testCase.player;
            var actual = day22.SolvePart1();
            Assert.AreEqual(testCase.winner, actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day22.SolvePart2();
            Assert.AreEqual("", actual);
        }
    }
}