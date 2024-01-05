﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CLI
{
    public sealed class SolutionTemplate
    {
        public int Year { get; init; }
        public string Day { get; init; }
        public bool HasUnitTest { get; init; }
        public string ExpectedValuePart1 { get; init; }
        public List<(string input, string outcome)> TestCases { get; init; }

        public string CreateSolution() =>  CSharpSolution();

        public string CreateUnitTest() => CSharpUnitTest();

        private string CSharpSolution() => 
            $@"using System;
             using System.Collections.Generic;
             using System.Linq;
             using System.Text;
             using System.Text.RegularExpressions;             
             using System.Threading.Tasks;
             using Common;

             namespace AoC{Year};
             
             public class Day{Day} : Solution
             {{
                 public Day{Day}(string file) : base(file) {{ }}
                 
                 public Day{Day}(List<string> input) : base(input) {{ }}

                 public override async Task<string> SolvePart1( ) 
                 {{
                 	return string.Empty;
                 }}

                 public override async Task<string> SolvePart2( )
                 {{
                 	return string.Empty;
                 }}
             }}
             ".Replace("             ", "");


        private string CSharpUnitTest() =>
            $@"using AoC{Year};
             using NUnit.Framework;
             using System.Collections.Generic;
             using System.Linq;
             
             namespace AoC{Year}Tests;
            
             public class Day{Day}Test
             {{
                 Day{Day} day{Day};
            
                 [SetUp]
                 public void Setup( )
                 {{
                     day{Day} = new Day{Day}(""day{Day}test1"");
                 }}
                 
                 {(TestCases.Count == 0 ?
            @$"[Test]
                 public void Part1( )
                 {{
                     var actual = day{Day}.SolvePart1( );
                     Assert.That(actual, Is.EqualTo(""{(string.IsNullOrEmpty(ExpectedValuePart1) ? string.Empty : ExpectedValuePart1)}""));
                 }}"
            :
            $@"{TestCases.Aggregate(string.Empty,
                (s, c) => s + @$"[TestCase(""{c.input}"",""{c.outcome}"")] {Environment.NewLine}        ").TrimEnd()}
                 public void Part1(string input, string expected )
                 {{
                     day{Day} = new Day{Day}(new List<string> {{ input }} );
                     var actual = day{Day}.SolvePart1( );
                     Assert.That(actual, Is.EqualTo(expected));
                 }}")}
            
                 [Test]
                 public void Part2( )
                 {{
                     var expected = string.Empty;
                     var actual = day{Day}.SolvePart2( );
                     Assert.That(actual, Is.EqualTo(expected));
                 }}
             }}
             ".Replace("             ", "");
    }
}
