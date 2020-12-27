using CommandLine;
using Common;
using System.IO;

namespace App
{
    [Verb("scaffold", HelpText = "Create class file for given day & year, with optional unit test")]
    public class ScaffoldOptions
    {
        [Option(shortName: 'y', Required = true, HelpText = "The year, from 2015 to 2020")]
        public int Year { get; set; }

        [Option(shortName: 'd', Required = true, HelpText = "The day, from 1 to 25")]
        public int Day { get; set; }

        [Option(shortName: 'u', HelpText = "Creates a unit test file")]
        public bool HasUnitTest { get; set; }

        [Option(shortName: 'e', HelpText = "Sets the expected value for example part 1")]
        public int? ExpectedValuePart1 { get; set; }

        public static string Run(ScaffoldOptions s)
        {
            if ( s.Year < 2015 || s.Year > 2020 )
                return $"Error: year must be between 2015 and 2020.";

            if ( s.Day < 1 || s.Day > 25 )
                return $"Error: day must be between 1 and 25.";

            var root = Directory.GetCurrentDirectory( ).Split("\\App")[0];
            var dir = $"{root}\\AoC{s.Year}";
            var path = $"{dir}\\Day{s.Day}.cs";
            var testdir = $"{root}\\AoC{s.Year}Tests";
            var testpath = $"{testdir}\\Day{s.Day}Test.cs";

            if ( !Directory.Exists(dir) )
                return $"Error: project for year {s.Year} could not be found at {dir}.";

            if ( File.Exists(path) )
                return $"Error: file for day {s.Day} year {s.Year} already exists at {path}.";

            if ( s.HasUnitTest )
            {
                if ( !Directory.Exists(testdir) )
                    return $"Error: test project for year {s.Year} could not be found at {testdir}.";

                if ( File.Exists(testpath) )
                    return $"Error: test for day {s.Day} year {s.Year} already exists at {testpath}.";
            }

            var template = new SolutionTemplate(s.Year, s.Day);

            File.WriteAllText(path, template.CreateSolution( ));

            if ( s.HasUnitTest )
                File.WriteAllText(testpath, template.CreateUnitTest(s.ExpectedValuePart1));

            return $"Succesfully created class for year {s.Year} day {s.Day} with {( s.HasUnitTest ? "additional" : "no" )} unit test.";
        }
    }
}
