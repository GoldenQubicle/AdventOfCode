using System.IO;

namespace CLI
{
    public sealed class SolutionTemplate
    {
        public int Year { get; init; }
        public int Day { get; init; }
        public bool HasUnitTest { get; init; }
        public string ExpectedValuePart1 { get; init; }
        private string ClassPath { get; set; }
        private string TestPath { get; set; }
        private string DayString { get; set; }

        public (bool isValid, string message) TryWrite( )
        {
            if ( Year < 2015 || Year > 2020 )
                return (false, $"Error: year must be between 2015 and 2020.");

            if ( Day < 1 || Day > 25 )
                return (false, $"Error: day must be between 1 and 25.");

            var root = Directory.GetCurrentDirectory( ).Split("\\CLI")[0];
            DayString = Day < 10 ? Day.ToString( ).PadLeft(2, '0') : Day.ToString( );
            var classDir = $"{root}\\AoC{Year}";
            ClassPath = $"{classDir}\\Day{DayString}.cs";
            var testDir = $"{root}\\AoC{Year}Tests";
            TestPath = $"{testDir}\\Day{DayString}Test.cs";

            if ( !Directory.Exists(classDir) )
                return (false, $"Error: project for year {Year} could not be found at {classDir}.");

            if ( File.Exists(ClassPath) )
                return (false, $"Error: file for day {Day} year {Year} already exists at {ClassPath}.");

            if ( HasUnitTest )
            {
                if ( !Directory.Exists(testDir) )
                    return (false, $"Error: test project for year {Year} could not be found at {testDir}.");

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
                 public class Day{DayString} : Solution
                 {{
                     public Day{DayString}(string file) : base(file) {{ }}

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
                 public class Day{DayString}Test
                 {{
                     Day{DayString} day{DayString};
             
                     [SetUp]
                     public void Setup( )
                     {{
                         day{DayString} = new Day{DayString}(""day{DayString}test1"");
                     }}
             
                     [Test]
                     public void Part1( )
                     {{
                         var actual = day{DayString}.SolvePart1( );
                         Assert.AreEqual(""{( string.IsNullOrEmpty(part1Expected) ? string.Empty : part1Expected )}"", actual);
                     }}
             
                     [Test]
                     public void Part2( )
                     {{
                         var actual = day{DayString}.SolvePart2( );
                         Assert.AreEqual("""", actual);
                     }}
                 }}
             }}".Replace("             ", "");
    }
}
