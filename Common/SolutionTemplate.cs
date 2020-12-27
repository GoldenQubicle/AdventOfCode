using System;
using System.Collections.Generic;
using System.IO;

namespace Common
{
    public sealed class SolutionTemplate
    {
        public SolutionTemplate(int year, int day)
        {
            if ( year < 2015 || year > 2021 )
                throw new ArgumentOutOfRangeException("Only year 2015 to 2020 are available");
            if ( day < 0 || day > 25 )
                throw new ArgumentOutOfRangeException("Only day 1 to 25 are part of the calendar");

            Year = year.ToString( ); ;
            Day = day < 10 ? day.ToString( ).PadLeft(2, '0') : day.ToString( );
            Root = Directory.GetCurrentDirectory( ).Split("\\App")[0];
            
            var dir = $"{Root}\\AoC{Year}";
            if ( !Directory.Exists(dir) )
                throw new InvalidOperationException($"Project for year {Year} does not exist yet");

            var path = $"{dir}\\Day{Day}.cs";
            if ( File.Exists(path) )
                throw new InvalidOperationException($"Solution for day {Day} already exists");

            File.WriteAllText(path, CreateSolution( ));
        }

        public SolutionTemplate WithUnitTest(int part1Expected)
        {
            var dir = $"{Root}\\AoC{Year}Tests";

            if ( !Directory.Exists(dir) )
                throw new InvalidOperationException($"Test Project for year {Year} does not exist yet");

            var path = $"{dir}\\Day{Day}Test.cs";

            if ( File.Exists(path) )
                throw new InvalidOperationException($"Tests for day {Day} already exists");

            File.WriteAllText(path, CreateUnitTest(part1Expected != 0 ? part1Expected.ToString( ) : string.Empty));

            return this;
        }

        private string Year { get; }
        private string Day { get; set; }
        private string Root { get; }

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
