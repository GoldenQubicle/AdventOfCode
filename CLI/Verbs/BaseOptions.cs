using CommandLine;
using System.Reflection;

namespace CLI.Verbs
{
    public abstract class BaseOptions
    {
        [Option(shortName: 'y', Required = true, HelpText = "The year, from 2015 to 2021")]
        public int Year { get; set; }

        [Option(shortName: 'd', Required = true, HelpText = "The day, from 1 to 25")]
        public int Day { get; set; }

        public string DayString { get => Day < 10 ? Day.ToString("00") : Day.ToString( ); }

        public bool IsFSharp { get; private set; }

        public virtual (bool isValid, string message) Validate( )
        {
            if ( Year < 2015 || Year > 2021 )
                return (false, $"Error: year must be between 2015 and 2021.");

            if ( Day < 1 || Day > 25 )
                return (false, $"Error: day must be between 1 and 25.");

            IsFSharp = Year == 2017;

            return (true, string.Empty);
        }

        public static string RootPath => Assembly.GetExecutingAssembly( ).Location.Split("\\CLI")[0];
    }
}
