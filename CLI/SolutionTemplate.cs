namespace CLI
{
    public sealed class SolutionTemplate
    {
        public int Year { get; init; }
        public string Day { get; init; }
        public bool HasUnitTest { get; init; }
        public string ExpectedValuePart1 { get; init; }

        public string CreateSolution( ) =>
           $@"using System;
             using System.Collections.Generic;
             using System.Linq;
             using System.Text;
             using System.Threading.Tasks;
             using Common;

             namespace AoC{Year}
             {{
                 public class Day{Day} : Solution
                 {{
                     public Day{Day}(string file) : base(file) {{ }}
                     
                     public Day{Day}(List<string> input) : base(input) {{ }}

                     public override string SolvePart1( ) => null;

                     public override string SolvePart2( ) => null;
                 }}
             }}".Replace("             ", "");

        public string CreateUnitTest(string part1Expected) =>
            $@"using AoC{Year};
             using NUnit.Framework;
             using System.Collections.Generic;
             using System.Linq;
             
             namespace AoC{Year}Tests
             {{
                 public class Day{Day}Test
                 {{
                     Day{Day} day{Day};
             
                     [SetUp]
                     public void Setup( )
                     {{
                         day{Day} = new Day{Day}(""day{Day}test1"");
                     }}
             
                     [Test]
                     public void Part1( )
                     {{
                         var actual = day{Day}.SolvePart1( );
                         Assert.AreEqual(""{( string.IsNullOrEmpty(part1Expected) ? string.Empty : part1Expected )}"", actual);
                     }}
             
                     [Test]
                     public void Part2( )
                     {{
                         var actual = day{Day}.SolvePart2( );
                         Assert.AreEqual("""", actual);
                     }}
                 }}
             }}".Replace("             ", "");
    }
}
