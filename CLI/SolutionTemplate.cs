using System;
using System.Collections.Generic;
using System.Linq;

namespace CLI
{
    public sealed class SolutionTemplate
    {
        public int Year { get; init; }
        public string Day { get; init; }
        public bool HasUnitTest { get; init; }
        public string ExpectedValuePart1 { get; init; }
        public bool IsFSharp { get; init; }
        public List<(string input, string outcome)> TestCases { get; init; }

        public string CreateSolution() => IsFSharp ? FSharpSolution() : CSharpSolution();

        public string CreateUnitTest() => IsFSharp ? FSharpUnitTest() : CSharpUnitTest();

        private string FSharpSolution() => 
            $@"namespace AoC{Year}

             open System
             open System.IO

             module Day{Day} = 

                let SolvePart1 : string = """"

                let SolvePart2 : string = """" ".Replace("             ", "");

        private string CSharpSolution() => 
            $@"using System;
             using System.Collections.Generic;
             using System.Linq;
             using System.Text;
             using System.Text.RegularExpressions;             
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

        private string FSharpUnitTest() => 
            $@"module Day{Day}Test

            open AoC{Year}
            open NUnit.Framework

            {(TestCases.Count == 0 ? 
            @$"[<Test>]
            let Part1 () =
                let actual = Day{Day}.SolvePart1
                Assert.AreEqual(""{(string.IsNullOrEmpty(ExpectedValuePart1) ? string.Empty : ExpectedValuePart1)}"", actual)"
            : 
            @$"{TestCases.Aggregate(string.Empty,
                (s, c) => s + @$"[<TestCase(""{c.input}"",""{c.outcome}"")>] {Environment.NewLine}").TrimEnd()}
            let Part1 (input: string, expected : string) =
                let actual = Day{Day}.SolvePart1
                Assert.AreEqual(expected, actual)")}
            
            [<Test>]
            let Part2 () = 
                let actual = Day{Day}.SolvePart2
                Assert.AreEqual("""", actual)".Replace("            ", "");

        private string CSharpUnitTest() =>
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
                     
                     {(TestCases.Count == 0 ?
                @$"[Test]
                     public void Part1( )
                     {{
                         var actual = day{Day}.SolvePart1( );
                         Assert.AreEqual(""{(string.IsNullOrEmpty(ExpectedValuePart1) ? string.Empty : ExpectedValuePart1)}"", actual);
                     }}"
                :
                $@"{TestCases.Aggregate(string.Empty,
                    (s, c) => s + @$"[TestCase(""{c.input}"",""{c.outcome}"")] {Environment.NewLine}        ").TrimEnd()}
                     public void Part1(string input, string expected )
                     {{
                         day{Day} = new Day{Day}(new List<string> {{ input }} );
                         var actual = day{Day}.SolvePart1( );
                         Assert.AreEqual(expected, actual);
                     }}")}
             
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
