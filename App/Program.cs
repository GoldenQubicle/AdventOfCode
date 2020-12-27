using Common;
using System;
using System.Threading.Tasks;

public class Program
{
    public static async Task<int> Main(string[ ] args)
    {
        /*
         * arguments coming in are expected to be day year, with optional expected value for part1 unit test
         * example usage; dotnet run --project .\App.csproj 7 2015 1568746
         * 
         * TODO find a proper CLI framework       
         */
        if ( args.Length >= 2 && 
            int.TryParse(args[0], out var day) &&
            int.TryParse(args[1], out var year) )
        {
            Console.WriteLine($"Trying to scaffold for year {year} day {day}");
            int pt1 = 0;
            if ( args.Length == 3 && int.TryParse(args[2], out pt1) )
            {
                Console.WriteLine($"Unit test part 1 has expected value of {pt1}");
            }

            var template = new SolutionTemplate(year, day).WithUnitTest(pt1);
        }

        return await Task.FromResult(0);
    }
}