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

             open Common
             open System.Collections.Generic
             open System.Linq 

             type Day{Day} = 
                 inherit Solution

                 new (file:string) = {{ inherit Solution(file) }}
                 new (input : List<string>) = {{ inherit Solution(input) }}

                 override this.SolvePart1() = """"

                 override this.SolvePart2() = """" ".Replace("             ", "");

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
            open Common.Extensions

            {(TestCases.Count == 0 ? 
            @$"[<Test>]
            let Part1 () =
                let day = new Day{Day}(""day{Day}test1"")
                let actual = day.SolvePart1()
                Assert.AreEqual(""{(string.IsNullOrEmpty(ExpectedValuePart1) ? string.Empty : ExpectedValuePart1)}"", actual)"
            : 
            @$"{TestCases.Aggregate(string.Empty,
                (s, c) => s + @$"[<TestCase(""{c.input}"",""{c.outcome}"")>] {Environment.NewLine}").TrimEnd()}
            let Part1 (input: string, expected : string) =
                let day = new Day{Day}(input.ToList())
                let actual = day.SolvePart1()
                Assert.AreEqual(expected, actual)")}
            
            [<Test>]
            let Part2 () = 
                let day = new Day{Day}(""day{Day}"")
                let actual = day.SolvePart2()
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
                         Assert.That(actual, Is.EqualTo(""{(string.IsNullOrEmpty(ExpectedValuePart1) ? string.Empty : ExpectedValuePart1)}""));
                     }}"
                :
                $@"{TestCases.Aggregate(string.Empty,
                    (s, c) => s + @$"[TestCase(""{c.input}"",""{c.outcome}"")] {Environment.NewLine}        ").TrimEnd()}
                     public void Part1(string input, string expected )
                     {{
                         day{Day} = new Day{Day}(new List<string> {{ input }} );
                         var actual = day{Day}.SolvePart1( );
                         Assert.That(actual, Is.EqualTo(expected));
                     }}")}
             
                     [Test]
                     public void Part2( )
                     {{
                         var expected = string.Empty;
                         var actual = day{Day}.SolvePart2( );
                         Assert.That(actual, Is.EqualTo(expected));
                     }}
                 }}
             }}".Replace("             ", "");
    }
}
