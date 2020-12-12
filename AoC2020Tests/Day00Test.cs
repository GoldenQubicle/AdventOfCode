using AoC2020;
using NUnit.Framework;

namespace AoC2020Tests
{
    class Day00Test
    {
        [Ignore("superceded")]
        [Test]
        public void AbstractSolutionShouldParseCorrectly( )
        {
            var day00 = new Day00("test");
            Assert.AreEqual(2233, day00.Input.Count);
        }
    }
}
