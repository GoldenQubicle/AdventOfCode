using CommandLine;
using System;
using System.IO;

namespace CLI.Verbs
{
    [Verb("scaffold", HelpText = "Create class file for given day & year, with optional unit test")]
    public class ScaffoldOptions : BaseOptions
    {
        [Option(shortName: 'u', HelpText = "Creates a unit test file")]
        public bool HasUnitTest { get; set; }

        [Option(shortName: 'e', HelpText = "Sets the expected value for example part 1")]
        public string ExpectedValuePart1 { get; set; }
        private string ClassPath { get; set; }
        private string TestPath { get; set; }

        public override (bool isValid, string message) Validate( )
        {
            var result = base.Validate( );
            
            if ( !result.isValid ) return result;

            var classDir = $"{RootPath}\\AoC{Year}";
            ClassPath = $"{classDir}\\Day{DayString}.cs";
            var testDir = $"{RootPath}\\AoC{Year}Tests";
            TestPath = $"{testDir}\\Day{DayString}Test.cs";

            if ( !Directory.Exists(classDir) )
                result = (false, $"Error: project for year {Year} could not be found at {classDir}.");

            if ( File.Exists(ClassPath) )
                result = (false, $"Error: file for day {Day} year {Year} already exists at {ClassPath}.");

            if ( HasUnitTest )
            {
                if ( !Directory.Exists(testDir) )
                    result = (false, $"Error: test project for year {Year} could not be found at {testDir}.");

                if ( File.Exists(TestPath) )
                    result = (false, $"Error: test for day {Day} year {Year} already exists at {TestPath}.");
            }
            return result;
        }

        public static string Run(ScaffoldOptions options)
        {
            var (isValid, message) = options.Validate( );

            if ( isValid )
            {
                var template = new SolutionTemplate
                {
                    Year = options.Year,
                    Day = options.DayString,
                    HasUnitTest = options.HasUnitTest,
                    ExpectedValuePart1 = options.ExpectedValuePart1
                };

                File.WriteAllText(options.ClassPath, template.CreateSolution( ));

                if ( options.HasUnitTest )
                    File.WriteAllText(options.TestPath, template.CreateUnitTest(options.ExpectedValuePart1));

                message = $"Succes: created class for year {options.Year} day {options.Day} with {( options.HasUnitTest ? "additional" : "no" )} unit test.";
            }

            Console.ForegroundColor = isValid ? ConsoleColor.Green : ConsoleColor.Red;
            return message;
        }
    }
}
