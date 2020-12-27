using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var current = Directory.GetCurrentDirectory( ).Replace("App\\bin\\Debug\\net5.0", "");
            var dir = $"{current}\\AoC{Year}";

            if ( !Directory.Exists(dir) )
                throw new InvalidOperationException($"Project for year {year} does not exist yet");

            var path = $"{dir}\\Day{Day}.cs";

            if ( File.Exists(path) )
                throw new InvalidOperationException($"Solution for day {Day} already exists");

            File.WriteAllText(path, CreateSolution( ));
        }

        private string Year { get; }
        private string Day { get; }

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

                     public override string SolvePart1( ) => string.Empty;

                     public override string SolvePart2( ) => string.Empty;
                 }}
             }}".Replace("             ", "");

    }
}
