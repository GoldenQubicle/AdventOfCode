using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020
{
    public class Day01 : Solution
    {
        public Day01(string day) : base(day)
        {
        }

        public override int SolvePart1()
        {
            foreach (var number in Input)
            {
                var lookingFor = 2020 - int.Parse(number);
                if (Input.Contains(lookingFor.ToString()))
                {
                    return lookingFor * int.Parse(number);
                }

            }
            return 0;
        }

        public override int SolvePart2()
        {
            var input = Input.Select(int.Parse).ToList();
            
            foreach(var number in input )
            {
                var lookingFor1 = 2020 - number;
                var subset = input.Where(i => i < lookingFor1 && i != number).ToList( );
                
                foreach(var other in subset )
                {
                    var lookingFor2 = lookingFor1 - other;
                    if ( input.Contains(lookingFor2) )
                    {
                        return number * other * lookingFor2;
                    }
                }
            }


            return 0;
        }
    }
}
