﻿namespace AoC2020
{
    public class Day06 : Solution
    {
        private readonly List<string[ ]> groups;

        public Day06(string file) : base(file, "\r\n\r\n") => groups = Input.Select(s => s.Split("\r\n")).ToList( );

        public override async Task<string> SolvePart1( ) => groups.Sum(g => g.SelectMany(p => p.ToCharArray( )).Distinct( ).Count( )).ToString( );

        public override async Task<string> SolvePart2( ) => groups.Sum(CountAnswers).ToString( );

        private int CountAnswers(string[ ] group)
        {
            var answers = new Dictionary<char, int>( );
            foreach ( var person in group )
            {
                foreach ( var answer in person )
                {
                    if ( !answers.ContainsKey(answer) )
                        answers.Add(answer, 1);
                    else
                        answers[answer]++;
                }
            }
            return answers.Where(c => c.Value == group.Length).Count( );
        }
    }
}
