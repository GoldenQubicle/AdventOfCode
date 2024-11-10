namespace AoC2020Tests
{
    class Day18Test
    {
        Day18 day18;

        [SetUp]
        public void Setup( )
        {

        }

        [TestCase("day18test1", 71)]
        [TestCase("day18test2", 51)]
        [TestCase("day18test3", 26)]
        [TestCase("day18test4", 437)]
        [TestCase("day18test5", 12240)]
        [TestCase("day18test6", 13632)]
        public void Part1(string file, int expected )
        {
            day18 = new Day18(file);
            var actual = day18.SolvePart1( ).Result;
            Assert.AreEqual(expected.ToString( ), actual);
        }

        [TestCase("day18test1", 231)]
        [TestCase("day18test2", 51)]
        [TestCase("day18test3", 46)]
        [TestCase("day18test4", 1445)]
        [TestCase("day18test5", 669060)]
        [TestCase("day18test6", 23340)]
        public void Part2(string file, int expected)
        {
            day18 = new Day18(file);
            var actual = day18.SolvePart2( ).Result;
            Assert.AreEqual(expected.ToString( ), actual);
        }

        [Test]
        public void SimpleSum( )
        {
            day18 = new Day18("day18test1");
            var actual = day18.SimpleSum(day18.Input.First( ));
            Assert.AreEqual(71, actual);
        }

        [Test]
        public void PrecedentSum( )
        {
            day18 = new Day18("day18test1");
            var actual = day18.PrecedentSum(day18.Input.First( ));
            Assert.AreEqual(231, actual);
        }
    }
}
