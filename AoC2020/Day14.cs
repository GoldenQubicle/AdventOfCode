using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;

namespace AoC2020
{
    public class Day14 : Solution
    {
        private Dictionary<long, long> memory;
        private List<string[ ]> program;
        public Day14(string file) : base(file) => program = Input.Select(i => i.Split("=")).ToList( );

        public override async Task<string> SolvePart1( )
        {
            memory = new( );
            var currentMask = "0";
            program.ForEach(line =>
            {
                if ( line[0].Trim( ).Equals("mask") )
                {
                    currentMask = line[1].Trim( );
                }
                else
                {
                    var value = ApplyBitMask(long.Parse(line[1].Trim( )), currentMask);
                    var key = int.Parse(line[0].Trim( ).Where(char.IsDigit).ToArray( ));

                    if ( !memory.ContainsKey(key) )
                        memory.Add(key, value);
                    else
                        memory[key] = value;
                }
            });

            return memory.Sum(kvp => kvp.Value).ToString( );
        }
        public override async Task<string> SolvePart2( )
        {
            memory = new( );

            var mask = "0";
            program.ForEach(line =>
            {
                if ( line[0].Trim( ).Equals("mask") )
                {
                    mask = line[1].Trim( );
                }
                else
                {
                    var adress = To36Bit(long.Parse(line[0].Trim( ).Where(char.IsDigit).ToArray( )));
                    var masked = ApplyBitMaskToAdress(adress, mask);
                    var adresses = GenerateFloatingAdresses(masked).Select(From36Bit).ToList( );
                    var value = long.Parse(line[1].Trim( ));

                    adresses.ForEach(a =>
                    {
                        if ( !memory.ContainsKey(a) )
                            memory.Add(a, value);
                        else
                            memory[a] = value;
                    });
                }
            });

            return memory.Sum(kvp => kvp.Value).ToString( );
        }

        public List<string> GenerateFloatingAdresses(string adress)
        {
            if ( adress.All(c => !c.Equals('X')) ) return new List<string> { adress };

            var index = adress.IndexOf('X');
            var a0 = adress.Remove(index, 1).Insert(index, "0");
            var a1 = adress.Remove(index, 1).Insert(index, "1");

            return GenerateFloatingAdresses(a0).Concat(GenerateFloatingAdresses(a1)).ToList( );
        }

        public string ApplyBitMaskToAdress(string adress, string mask) => new string(adress
                .Select((c, i) => mask[i].Equals('0') ? c : mask[i].Equals('1') ? '1' : mask[i])
                .ToArray( ));

        public long ApplyBitMask(long value, string mask)
        {
            var bits = To36Bit(value).ToCharArray( );
            for ( int i = 0 ; i < mask.Length ; i++ )
            {
                if ( mask[i] != 'X' )
                    bits[i] = mask[i];
            }
            return From36Bit(new string(bits));
        }

        public string To36Bit(long value) => Convert.ToString(value, 2).PadLeft(36, '0');

        public long From36Bit(string value) => Convert.ToInt64(value, 2);
    }
}
