using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020.Solutions
{
    public class Day18 : Solution<long>
    {
        public Day18(string file) : base(file) { }

        public override long SolvePart1( ) => Solve(isPart1: true);

        public override long SolvePart2( ) => Solve(isPart1: false);

        private long Solve(bool isPart1)
        {
            var result = new List<long>( );

            foreach ( var line in Input )
            {
                var newLine = new StringBuilder( );
                var openIndex = new List<int>( );

                foreach ( var c in line )
                {
                    switch ( c )
                    {
                        case '(':
                            newLine.Append(c);
                            openIndex.Add(newLine.Length);
                            break;
                        case ')':
                            var toSolve = newLine.ToString( )[openIndex.Last( )..];
                            var solved = isPart1 ? SimpleSum(toSolve) : PrecedentSum(toSolve);
                            newLine.Remove(openIndex.Last( ) - 1, newLine.Length - ( openIndex.Last( ) - 1 ));
                            newLine.Append(solved.ToString( ));
                            openIndex.RemoveAt(openIndex.Count( ) - 1);
                            break;
                        default:
                            newLine.Append(c);
                            break;
                    }
                }
                result.Add(isPart1 ? SimpleSum(newLine.ToString( )) : PrecedentSum(newLine.ToString( )));
            }
            return result.Sum( );
        }

        public long PrecedentSum(string line)
        {
            var toParse = line.Split(' ');
            var newLine = new StringBuilder( );
            var lastAdd = string.Empty;

            for ( int i = 0 ; i < toParse.Length ; i++ )
            {
                switch ( toParse[i] )
                {
                    case "+":
                        var addTo = !string.IsNullOrEmpty(lastAdd) ? lastAdd : toParse[i - 1];
                        var solved = SimpleSum($"{addTo} + {toParse[i + 1]}");
                        lastAdd = solved.ToString( );
                        newLine.Remove(newLine.Length - addTo.Length - 1, addTo.Length + 1);
                        newLine.Append(solved.ToString( ));
                        newLine.Append(" ");
                        i += 1;
                        break;
                    default:
                        newLine.Append(toParse[i]);
                        newLine.Append(" ");
                        lastAdd = string.Empty;
                        break;
                }
            }
            return SimpleSum(newLine.ToString( ));
        }

        public long SimpleSum(string line)
        {
            var toParse = line.Split(' ');
            var result = long.Parse(toParse[0]);

            for ( int i = 1 ; i < toParse.Length ; i += 2 )
            {
                switch ( toParse[i] )
                {
                    case "+":
                        result += long.Parse(toParse[i + 1].ToString( ));
                        break;
                    case "*":
                        result *= long.Parse(toParse[i + 1].ToString( ));
                        break;
                }
            }
            return result;
        }
    }
}
