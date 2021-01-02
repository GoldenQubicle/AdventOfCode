using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Common;

namespace AoC2015
{
    public class Day06 : Solution
    {
        private readonly List<(string Command, (int x, int y) LeftUp, (int x, int y) RightDown)> instructions;
        private List<List<int>> Lights = new( );

        public Day06(string file) : base(file, "\n")
        {
            var expression = new Regex("(?<instruction>.on|.off|toggle).(?<leftup>\\d{1,},\\d{1,})(.through.)(?<rightdown>\\d{1,},\\d{1,})");

            instructions = Input
                .Select(line => expression.Match(line))
                .Select(match => (
                    Command: match.Groups["instruction"].Value.Trim( ),
                    LeftUp: (int.Parse(match.Groups["leftup"].Value.Split(",")[0]),
                                int.Parse(match.Groups["leftup"].Value.Split(",")[1])),
                    RightDown: (int.Parse(match.Groups["rightdown"].Value.Split(",")[0]),
                                int.Parse(match.Groups["rightdown"].Value.Split(",")[1]))
                )).ToList( );
        }

        public override string SolvePart1( )
        {
            ResetLights( );
            instructions.ForEach(n =>
            {
                for ( int x = n.LeftUp.x ; x <= n.RightDown.x ; x++ )
                {
                    for ( int y = n.LeftUp.y ; y <= n.RightDown.y ; y++ )
                    {
                        Lights[x][y] = n.Command switch
                        {
                            "on" => 1,
                            "off" => 0,
                            "toggle" => Lights[x][y] == 0 ? 1 : 0
                        };
                    }
                }
            });
            return Lights.Sum(row => row.Count(l => l == 1)).ToString( );
        }

        public override string SolvePart2( )
        {
            ResetLights( );
            instructions.ForEach(n =>
            {
                for ( int x = n.LeftUp.x ; x <= n.RightDown.x ; x++ )
                {
                    for ( int y = n.LeftUp.y ; y <= n.RightDown.y ; y++ )
                    {
                        Lights[x][y] = n.Command switch
                        {
                            "on" => Lights[x][y] + 1,
                            "off" => Lights[x][y] == 0 ? 0 : Lights[x][y] - 1,
                            "toggle" => Lights[x][y] + 2
                        };
                    }
                }
            });
            return Lights.Sum(row => row.Sum( )).ToString( );
        }

        private void ResetLights( )
        {
            Lights = new( );
            Enumerable.Range(0, 1000).ForEach(n =>
            {
                Lights.Add(new List<int>( ));
                Enumerable.Range(0, 1000).ForEach(nn => Lights[n].Add(0));
            });
        }
    }
}