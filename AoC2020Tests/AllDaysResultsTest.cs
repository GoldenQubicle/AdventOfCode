using Common;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2020Tests;

class AllDaysResultsTest
{
	//so this is superduper fugly, however, what's more ridiculous is that NUnit FIRST grabs a test case BEFORE calling SetUp
	//meaning initializing the testcase source in SetUp will result in a big fat NRE. 
	private static Dictionary<string, (string pt1, string pt2)> expectedDayResults =>
		File.ReadAllLines($"{Directory.GetCurrentDirectory( )}/data/AllResults.txt")
			.Select((line, i) => (line.Split("part"), i))
			.Aggregate(new Dictionary<string, (string pt1, string pt2)>( ), (dic, line) =>
			{
				var day = line.Item1[0].Replace(" ", "").Replace("Day", "");
				if ( line.i % 2 == 0 )
					dic.Add(day, (line.Item1[1][4..], string.Empty));
				else
					dic[day] = (dic[day].pt1, line.Item1[1][4..]);
				return dic;
			});

	[TestCaseSource(nameof(GetExpectedPerDay))]
	public void ActualResultPerDay(KeyValuePair<string, (string pt1, string pt2)> testCase)
	{
		var solution = Solution.Initialize("2020", testCase.Key);
		var actualPt1 = solution.SolvePart1( ).Result;
		var actualPt2 = solution.SolvePart2( ).Result;

		Assert.AreEqual(testCase.Value.pt1, actualPt1);
		Assert.AreEqual(testCase.Value.pt2, actualPt2);
	}

	private static IEnumerable<KeyValuePair<string, (string, string)>> GetExpectedPerDay( )
	{
		foreach ( var kvp in expectedDayResults )
		{
			yield return kvp;
		}
	}
}