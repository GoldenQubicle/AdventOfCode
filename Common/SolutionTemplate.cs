using System.IO;

namespace Common
{
    public sealed class SolutionTemplate
    {
        public int Year { get; init; }
        public int Day { get; init; }
        public bool HasUnitTest { get; init; }
        public string ExpectedValuePart1 { get; init; }
        private string ClassPath { get; set; }
        private string TestPath { get; set; }

        public (bool isValid, string message) TryWrite( )
        {
            if ( Year < 2015 || Year > 2020 )
                return (false, $"Error: year must be between 2015 and 2020.");

            if ( Day < 1 || Day > 25 )
                return (false, $"Error: day must be between 1 and 25.");

            var root = Directory.GetCurrentDirectory( ).Split("\\App")[0];
            var day = Day < 10 ? Day.ToString( ).PadLeft(2, '0') : Day.ToString( );
            var dir = $"{root}\\AoC{Year}";
            ClassPath = $"{dir}\\Day{day}.cs";
            var testdir = $"{root}\\AoC{Year}Tests";
            TestPath = $"{testdir}\\Day{day}Test.cs";

            if ( !Directory.Exists(dir) )
                return (false, $"Error: project for year {Year} could not be found at {dir}.");

            if ( File.Exists(ClassPath) )
                return (false, $"Error: file for day {Day} year {Year} already exists at {ClassPath}.");

            if ( HasUnitTest )
            {
                if ( !Directory.Exists(testdir) )
                    return (false, $"Error: test project for year {Year} could not be found at {testdir}.");

                if ( File.Exists(TestPath) )
                    return (false, $"Error: test for day {Day} year {Year} already exists at {TestPath}.");
            }

            File.WriteAllText(ClassPath, CreateSolution( ));

            if ( HasUnitTest )
                File.WriteAllText(TestPath, CreateUnitTest(ExpectedValuePart1));

            return (true, $"Succes: created class for year {Year} day {Day} with {( HasUnitTest ? "additional" : "no" )} unit test.");
        }

        private string CreateSolution( ) =>
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

                     public override string SolvePart1( ) => null;

                     public override string SolvePart2( ) => null;
                 }}
             }}".Replace("             ", "");

        private string CreateUnitTest(string part1Expected) =>
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
