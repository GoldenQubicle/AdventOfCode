using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;
using Common.Extensions;

namespace AoC2015
{
    public class Day14 : Solution
    {
        private List<(string name, int speed, int flyTime, int restTime)> reindeers;
        public int TravelSeconds { get; set; } = 2503;
        public Day14(string file) : base(file, "\n")
        {
            reindeers = Input.Select(line =>
            {
                var matches = Regex.Matches(line, @"(?<name>[A-Z]\w+)*(?<digit>\d{1,})*")
                                .Where(match => !string.IsNullOrEmpty(match.Value)).ToArray( );

                return (name: matches[0].Value,
                        speed:    int.Parse(matches[1].Value),
                        flyTime:  int.Parse(matches[2].Value),
                        restTime: int.Parse(matches[3].Value));
            }).ToList();
        }

        public Day14(List<string> input) : base(input) { }

        public override string SolvePart1( )
        {
            var afterTravelSeconds = reindeers.Select(r =>
            {
                var cycleTime = r.flyTime + r.restTime;
                var cycles = MathF.Floor(TravelSeconds / cycleTime);
                var cycleDistance = r.flyTime * r.speed;
                var totalDistance = cycles * cycleDistance;
                var remainingTime = TravelSeconds - ( cycles * cycleTime );
                var remainginDistance = remainingTime < r.flyTime ? remainingTime * r.speed : cycleDistance;
                var restingTime = remainingTime < r.flyTime ? 0 : remainingTime - r.flyTime;
                return (r.name, distance: totalDistance + remainginDistance, isResting: restingTime > 0);
            });

            return afterTravelSeconds.Max(r => r.distance ).ToString();
        }

        public override string SolvePart2( )
        {
            var scores = reindeers.ToDictionary(r => r.name, _ => 0);

            for ( int i = 1 ; i <= TravelSeconds ; i++ )
            {
                var pack = reindeers.Select(r =>
                {
                    var cycleTime = r.flyTime + r.restTime;
                    var currentCyle = MathF.Floor(i / cycleTime);
                    var elapsed = i - ( currentCyle * cycleTime );
                    var soFar = currentCyle * ( r.flyTime * r.speed );
                    var traveled = elapsed < r.flyTime ? soFar + ( elapsed * r.speed ) : soFar + (r.flyTime * r.speed);
                    return (r.name, distance: traveled);
                });

                pack.Where(r => r.distance == pack.Max(r => r.distance))
                    .ForEach(r => scores[r.name]++);
            }

            return scores.Values.Max().ToString();
        }
    }
}