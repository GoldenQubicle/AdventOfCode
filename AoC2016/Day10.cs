using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Common;
using Common.Extensions;

namespace AoC2016
{
    public class Day10 : Solution
    {
        private readonly Dictionary<int, Bot> bots = new();
        private readonly Dictionary<int, int> output = new();
        public (int High, int Low) LookFor { get; set; } = (61, 17);

        public Day10(string file) : base(file, "\n") { }

        public override string SolvePart1( )
        {
            ParseInput();

            while(!bots.Values.Any(b => b.Found))
            {
                bots.Values.ForEach(b => b.Execute());
            }

            return bots.Where(b => b.Value.Found).Select(b => b.Key).First().ToString();
        }

        public override string SolvePart2()
        {
            ParseInput();
            
            while(bots.Values.Any(b => b.Values.Count == 2))
                bots.Values.ForEach(b => b.Execute());

            return (output[0] * output[1] * output[2]).ToString();
        }

        private void ParseInput( )
        {
            foreach(var line in Input)
            {
                var matches = Regex.Matches(line, @"(?<input>value (?<chip>\d+).*bot (?<botIn>\d+))|(?<output>bot (?<botOut>\d+).*(?<low>(bot|output) \d+).*(?<high>(bot|output) \d+))");
                foreach(Match match in matches)
                {
                    var bot = match.Groups["input"].Success
                        ? int.Parse(match.Groups["botIn"].Value)
                        : int.Parse(match.Groups["botOut"].Value);
                    
                    if(!bots.ContainsKey(bot))
                        bots.Add(bot, new Bot(LookFor));

                    if(match.Groups["input"].Success)
                    {
                        var chip = int.Parse(match.Groups["chip"].Value);
                        bots[bot].AddChip(chip);
                    }

                    if(match.Groups["output"].Success)
                    {
                        var chipLow = int.Parse(match.Groups["low"].Value.Where(char.IsDigit).ToArray());
                        bots[bot].Low = match.Groups["low"].Value.Contains("bot")
                            ? ( ) => bots[chipLow].AddChip(bots[bot].Values.Min())
                            : ( ) => output[chipLow] = bots[bot].Values.Min();

                        var chipHigh = int.Parse(match.Groups["high"].Value.Where(char.IsDigit).ToArray());
                        bots[bot].High = match.Groups["high"].Value.Contains("bot")
                            ? ( ) => bots[chipHigh].AddChip(bots[bot].Values.Max())
                            : ( ) => output[chipHigh] = bots[bot].Values.Max();
                    }
                }
            }
        }

        public class Bot
        {
            public Action Low { get; set; }
            public Action High { get; set; }

            public bool Found { get; private set; }
            private readonly (int High, int Low) lookFor;
            public readonly List<int> Values = new();

            public Bot((int High, int Low) lookFor) => this.lookFor = lookFor;

            public void AddChip(int chip)
            {
                Values.Add(chip);
                if(Values.Contains(lookFor.Low) && Values.Contains(lookFor.High))
                    Found = true;
            }

            public void Execute( )
            {
                if(Values.Count == 2)
                {
                    Low?.Invoke();
                    High?.Invoke();
                    Values.Clear();
                }
            }
        }
    }
}