using CommandLine;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CLI.Verbs;

[Verb("getinput", HelpText = "Get input for the specified year and day.")]
public class GetInputOptions : BaseOptions
{

	[Option(shortName: 's', Required = true, HelpText = "The session token for adventofcode.com")]
	public string SessionId { get; set; }

	public override (bool isValid, string message) Validate()
	{
		var result = base.Validate();

		if (!result.isValid) return result;

		var dataDir = $"{RootPath}\\AoC{Year}\\data";
		var dataPath = $"{dataDir}\\Day{DayString}.txt";

		if (!Directory.Exists(dataDir))
			result = (false, $"Error: data folder for year {Year} could not be found at {dataDir}.");

		if (File.Exists(dataPath))
			result = (false, $"Error: input file for day {Day} year {Year} already exists at {dataPath}.");

		return result;
	}

	public static async Task<string> Run(GetInputOptions options)
	{
		var (isValid, message) = options.Validate();

		if (isValid)
		{
			var baseAddress = new Uri("https://adventofcode.com");
			var cookieContainer = new CookieContainer();
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

			//added user agent because: reddit.com/r/adventofcode/comments/z9dhtd/please_include_your_contact_info_in_the_useragent/
			httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("(+github.com/GoldenQubicle/AdventOfCode)"));

			var aocDir = $"{RootPath}\\AoC{options.Year}";
			var inputFile = $"{aocDir}\\data\\day{options.DayString}.txt";
			var response = await httpClient.GetAsync($"{options.Year}/day/{options.Day}/input");

			try
			{
				response = response.EnsureSuccessStatusCode();
				var content = await response.Content.ReadAsStringAsync();
				await File.WriteAllTextAsync(inputFile, content);
				
				message = $"Success: retrieved input file for year {options.Year} day {options.Day}";
			}
			catch (HttpRequestException e)
			{
				isValid = false;
				message = $"Error: {e.Message} Probably session related, make sure token is still valid.";
			}
		}

		Console.ForegroundColor = isValid ? ConsoleColor.Green : ConsoleColor.Red;
		return message;
	}
}