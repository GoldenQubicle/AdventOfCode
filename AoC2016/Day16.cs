using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace AoC2016
{
    public class Day16 : Solution
    {
        public int Length { get; set; } = 272;
        public string InitialState { get; }

        public Day16(string file) : base(file, "\n") =>  InitialState = Input[0]; 
        
        public Day16(List<string> input) : base(input) =>  InitialState = Input[0];

        public override async Task<string> SolvePart1()
        {
            var data = InitialState;
            while (data.Length < Length)
                data = Dragonize(data);

            return CheckSum(data[..Length]);
        }

        public override async Task<string> SolvePart2()
        {
            Length = 35651584;
            return SolvePart1().Result;
        }

        public string Dragonize(string input) => @$"{input}{0}{
            new string(input.Reverse().Select(c => c switch
            {
                '1' => '0',
                '0' => '1',
                _ => c
            }).ToArray())}";

        public string CheckSum(string input)
        {
            var checksum = GetActualCheckSum(input);

            while(checksum.Length % 2 == 0 )
                checksum = GetActualCheckSum(checksum);

            return checksum;
        }

        private static string GetActualCheckSum(string input)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < input.Length; i += 2)
            {
                sb.Append(input[i] == input[i + 1] ? "1" : "0");
            }
            return sb.ToString();
        }
    }
}