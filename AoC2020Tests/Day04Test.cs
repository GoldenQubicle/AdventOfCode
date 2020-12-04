using AoC2020.Solutions;
using NUnit.Framework;
using System.IO;

namespace AoC2020Tests
{
    class Day04Test
    {
        Day04 day4;

        [SetUp]
        public void Setup( )
        {
            day4 = new Day04("day04test1");
        }

        [Test]
        public void Part1()
        {
            var expected = 2;
            var actual = day4.SolvePart1( );
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void InvalidPassports( )
        {
            day4 = new Day04("day04test2");
            var expected = 0;
            var actual = day4.SolvePart2( );
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ValidPassports( )
        {
            day4 = new Day04("day04test3");
            var expected = 4;
            var actual = day4.SolvePart2( );
            Assert.AreEqual(expected, actual);
        }


        [TestCase("1915", false)]
        [TestCase("1920", true)]
        [TestCase("1990", true)]
        [TestCase("2002", true)]
        [TestCase("2003", false)]        
        public void BirthYearShouldBeValidated(string toCheck, bool expected )
        {
            var actual = Day04.checkBirthYear(toCheck);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("2005", false)]
        [TestCase("2010", true)]
        [TestCase("2020", true)]
        [TestCase("2021", false)]
        public void IssueYearShouldBeValidated(string toCheck, bool expected)
        {
            var actual = Day04.checkIssueYear(toCheck);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("2019", false)]
        [TestCase("2020", true)]
        [TestCase("2030", true)]
        [TestCase("2031", false)]
        public void ExperitationYearShouldBeValidated(string toCheck, bool expected)
        {
            var actual = Day04.checkExperirationYear(toCheck);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("60in", true)]
        [TestCase("190cm", true)]
        [TestCase("190in", false)]
        [TestCase("190", false)]
        public void HeightShouldBeValidated(string toCheck, bool expected)
        {
            var actual = Day04.checkHeight(toCheck);
            Assert.AreEqual(expected, actual);
        }
        [TestCase("#123abc", true )]
        [TestCase("#123abz", false )]
        [TestCase("123abc", false )]
        public void HairColorShouldBeValidated(string toCheck, bool expected)
        {
            var actual = Day04.checkHairColor(toCheck);
            Assert.AreEqual(expected, actual);
        }
        [TestCase("brn", true)]
        [TestCase("wat", false)]
        public void EyeColorShouldBeValidated(string toCheck, bool expected)
        {
            var actual = Day04.checkEyeColor(toCheck);
            Assert.AreEqual(expected, actual);
        }
        [TestCase("000000001", true)]
        [TestCase("0123456789", false)]
        public void PassportidShouldBeValidated(string toCheck, bool expected)
        {
            var actual = Day04.checkPassportId(toCheck);
            Assert.AreEqual(expected, actual);
        }
    }
}
