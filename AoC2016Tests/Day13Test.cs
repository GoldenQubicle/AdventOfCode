namespace AoC2016Tests
{
    public class Day13Test
    {
        Day13 day13;

        [SetUp]
        public void Setup( )
        {
            day13 = new Day13(new List<string>());
        }

        [TestCase(true, "11")]
        [TestCase(false, "86")]
        public void Part1(bool isSmallCase, string expected )
        {
            if (isSmallCase)
            {
                day13.Target = (7, 4);
                day13.FavoriteNumber = 10;
                day13.Grid = (7, 10);
            }

            var actual = day13.SolvePart1().Result;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Part2( )
        {
            var actual = day13.SolvePart2().Result;
            Assert.AreEqual("127", actual);
        }

        [Test]
        public void ConstructLayoutTest( )
        {
            day13.FavoriteNumber = 10;
            var expectedLayout = new List<List<char>>{
                new () {'.','#','.','#','#','#','#','.','#','#'},
                new () {'.','.','#','.','.','#','.','.','.','#'},
                new () {'#','.','.','.','.','#','#','.','.','.'},
                new () {'#','#','#','.','#','.','#','#','#','.'},
                new () {'.','#','#','.','.','#','.','.','#','.'},
                new () {'.','.','#','#','.','.','.','.','#','.'},
                new () {'#','.','.','.','#','#','.','#','#','#'},
            };
            var actualLayout = day13.ConstructLayout(7, 10);
            Assert.AreEqual(expectedLayout, actualLayout);
        }
    }
}