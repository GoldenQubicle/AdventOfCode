using CommandLine;
using Common;
using System;

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
        public string ExpectedValuePart1 { get; set; }

        public static string Run(ScaffoldOptions s)
        {          
            var template = new SolutionTemplate
            {
                Year = s.Year,
                Day = s.Day,
                HasUnitTest = s.HasUnitTest,
                ExpectedValuePart1 = s.ExpectedValuePart1
            };

            var (isValid, message) = template.TryWrite( );

            Console.ForegroundColor = isValid ? ConsoleColor.Green : ConsoleColor.Red;

            return message;
        }
    }
}
