using System.Collections.Generic;
using System.Linq;
using Common;

namespace AoC2021
{
    public class Day10 : Solution
    {
        private readonly Dictionary<char, int> scoresPart1 = new()
        {
            { ')', 3 },
            { ']', 57 },
            { '}', 1197 },
            { '>', 25137 },
        };

        private readonly Dictionary<char, int> scoresPart2 = new()
        {
            { '(', 1 },
            { '[', 2 },
            { '{', 3 },
            { '<', 4 },
        };

        public Day10(string file) : base(file) { }

        public override string SolvePart1() => Input.Select(IsCorrupted).Sum(r => r.Item2).ToString();

        public override string SolvePart2()
        {
            var completionScores = Input.Where(l => !IsCorrupted(l).Item1).Aggregate(new List<long>(),
                (scores, s) =>
                {
                    var stack = new Stack<char>();
                    foreach (var c in s)
                    {
                        if (c == '(' || c == '[' || c == '{' || c == '<')
                        {
                            stack.Push(c);
                            continue;
                        }

                        if (c == ')' && stack.Peek() == '(' ||
                            c == ']' && stack.Peek() == '[' ||
                            c == '}' && stack.Peek() == '{' ||
                            c == '>' && stack.Peek() == '<')
                            stack.Pop();
                    }

                    var score = 0L;
                    while (stack.Any())
                    {
                        var c = stack.Pop();
                        score *= 5;
                        score += scoresPart2[c];
                    }
                    scores.Add(score);
                    return scores;
                }).OrderBy(l => l).ToList();

            return completionScores[(completionScores.Count() - 1) / 2].ToString();
        }

        private (bool, int) IsCorrupted(string s)
        {
            var stack = new Stack<char>();
            foreach (var c in s)
            {
                if (c == '(' || c == '[' || c == '{' || c == '<')
                {
                    stack.Push(c);
                    continue;
                }

                if (c == ')' && stack.Peek() != '(' ||
                    c == ']' && stack.Peek() != '[' ||
                    c == '}' && stack.Peek() != '{' ||
                    c == '>' && stack.Peek() != '<')
                    return (true, scoresPart1[c]);

                stack.Pop();
            }
            return (false, 0);
        }
    }
}