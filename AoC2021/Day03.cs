using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Common.Extensions;

namespace AoC2021
{
    public class Day03 : Solution
    {
        private const char One = '1';
        private const char Zero = '0';
        public Day03(string file) : base(file) { }

        public override string SolvePart1()
        {
            var gamma = new StringBuilder();
            var epsilon = new StringBuilder();

            for (var idx = 0; idx < Input[0].Length; idx++)
            {
                var (most, least) = GetMostAndLeastCommonBit(Input.Select(i => i[idx]));
                gamma.Append(most);
                epsilon.Append(least);
            }

            return (gamma.ToDecimal() * epsilon.ToDecimal()).ToString();
        }

        public override string SolvePart2()
        {
            var oxygen = GetRating(Input, One);
            var carbon = GetRating(Input, Zero);
            return (oxygen * carbon).ToString();
        }

        private static long GetRating(List<string> bits, char criteria)
        {
            var idx = 0;

            while (bits.Count > 1)
            {
                var (most, least) = GetMostAndLeastCommonBit(bits.Select(i => i[idx]));
                bits = bits.Where(i => i[idx] == (criteria == One ? most : least)).ToList();
                idx++;
            }

            return Convert.ToInt64(bits.First(), 2);
        }

        private static (char most, char least) GetMostAndLeastCommonBit(IEnumerable<char> bits)
        {
            var zero = bits.Count(c => c == Zero);
            var one = bits.Count(c => c == One);
            var most = zero > one ? Zero : One;
            var least = most == Zero ? One : Zero;
            return (most, least);
        }
    }
}