using AoC2015;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2015Tests
{
    public class Day21Test
    {
        Day21 day21;

        [SetUp]
        public void Setup( )
        {
            day21 = new Day21("day21test1");
        }
        
        [Test]
        public void Part1( )
        {
            day21 = new Day21("day21");
            var actual = day21.SolvePart1( );
            Assert.AreEqual("78", actual);
        }

        [Test]
        public void Part2( )
        {
            day21 = new Day21("day21");
            var actual = day21.SolvePart2( );
            Assert.AreEqual("148", actual);
        }

        [Test]
        public void PlayGameTest()
        {
            var player = new Day21.Player
            {
                Name = "Me",
                HitPoints = 8,
                Damage = 5,
                Armor = 5
            };

            var winner = day21.PlayGame(player);

            Assert.AreEqual("Me", winner.Name);
        }
    }
}