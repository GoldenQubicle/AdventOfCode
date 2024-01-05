using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Common;

namespace AoC2015
{
    public class Day20 : Solution
    {
        public int Target { get; }

        public Day20(string file) : base(file)
        {
            Target = int.Parse(Input[0]) / 10;
        }

        public Day20(List<string> input) : base(input)
        {
            Target = int.Parse(input.First()) /10;
        }
        
        public override async Task<string> SolvePart1( )
        {
            //awy yeah brute force trial and error optimization ftw!
            var houseNumber = 352800;
            var divisors = new List<int>();
            while (true)
            {
                for(var i = 1 ; i <= houseNumber  ; i += 1)
                {
                    if(houseNumber % i == 0)
                    {
                        divisors.Add(i);
                    }
                }

                if (divisors.Sum() >= Target) break;

                divisors.Clear();
                houseNumber+=7;
            }
            
            return houseNumber.ToString();
        }

        public override async Task<string> SolvePart2()
        {
            //compute presents delivered
            var houses = new Dictionary<int, int>();
            for (var h = 1; h < 1_000_000; h++)
            {
                for (var i = 1; i <= 50; i++)
                {
                    var number = i * h;
                    if (houses.ContainsKey(number))
                        houses[number] += 11 * h;
                    else 
                        houses.Add(number, 11 * h);
                }
            }
            
            return houses.First(kvp => kvp.Value >= Target * 10).Key.ToString(); 
        }
    }
}