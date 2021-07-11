using CommandLine;
using Common;
using Common.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CLI.Verbs
{
    [Verb("getinput", HelpText = "Get input for the specified year and day.")]
    public class GetInputOptions : BaseOptions
    {

        [Option(shortName: 's', Required = true, HelpText = "The session token for adventofcode.com")]
        public string SessionId { get; set; }

        public override (bool isValid, string message) Validate( )
        {
            var result = base.Validate( );

            if ( !result.isValid ) return result;

            var dataDir = $"{RootPath}\\AoC{Year}\\data";
            var dataPath = $"{dataDir}\\Day{DayString}.txt";

            if ( !Directory.Exists(dataDir) )
                result = (false, $"Error: data folder for year {Year} could not be found at {dataDir}.");

            if ( File.Exists(dataPath) )
                result = (false, $"Error: input file for day {Day} year {Year} already exists at {dataPath}.");

            return result;
        }

        public static async Task<string> Run(GetInputOptions options)
        {
            var (isValid, message) = options.Validate( );

            if ( isValid )
            {
                var baseAddress = new Uri("https://adventofcode.com");
                var cookieContainer = new CookieContainer( );
                cookieContainer.Add(baseAddress, new Cookie("session", options.SessionId));

                using var httpClientHandler = new HttpClientHandler
                {
                    CookieContainer = cookieContainer,
                    AutomaticDecompression = DecompressionMethods.All,
                };
                using var httpClient = new HttpClient(httpClientHandler)
                {
                    BaseAddress = baseAddress,
                };

                var aocDir = $"{RootPath}\\AoC{options.Year}";
                var inputFile = $"{aocDir}\\data\\day{options.DayString}.txt";
                var response = await httpClient.GetAsync($"{options.Year}/day/{options.Day}/input");
               
                try
                {
                    response = response.EnsureSuccessStatusCode( );
                    var content = await response.Content.ReadAsStringAsync( );
                    await File.WriteAllTextAsync(inputFile, content);

                    var projExtension = options.IsFSharp ? ".fsproj" : ".csproj";
                    var projPath = $"{aocDir}\\AoC{options.Year}{projExtension}";
                    var projFile = await File.ReadAllLinesAsync(projPath)
                        .ContinueWith(f => UpdateProjFile(f.Result.ToList( ), options.DayString));

                    await File.WriteAllLinesAsync($"{projPath}", projFile);

                    message = $"Succes: created input file for year {options.Year} day {options.Day}";
                }
                catch ( HttpRequestException e )
                {
                    isValid = false;
                    message = $"Error: {e.Message} Probably session related, make sure token is still valid.";
                }
            }

            Console.ForegroundColor = isValid ? ConsoleColor.Green : ConsoleColor.Red;
            return message;
        }

        //note the wonky string formatting is on purpose such that the tabs in the proj file are actually alligned
        private static List<string> UpdateProjFile(List<string> file, string dayNo) => file.InsertAt(file.Count - 2,
            $@"    <None Update=""data\day{dayNo}.txt"">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>");
    }
}
