using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Extensions;

namespace Common
{
    public abstract class Solution
    {
        public List<string> Input { get; }
        protected Solution(string file) => Input = ParseInput(file, "\r\n");
        protected Solution(string file, string split) => Input = ParseInput(file, split);
        protected Solution(List<string> input) => Input = input;
        protected Solution() { }

        private List<string> ParseInput(string file, string split)
        {
            // admittedly bit hacky solution 
            // however the solution MUST be able to find the correct data folder,
            // whether it was called from a test, the cli or whatever. 
            var aocYear = GetType().FullName.Split('.')[0];

            //TODO somehow the files for 20201 use a different line ending..?
            //not sure what is going on but needed different split characters in order to parse the input for 2021
            split = aocYear.GetInteger() == 2021 ? "\n" : "\r\n";

            var path = GetType().Assembly.Location.Replace($"{aocYear}.dll", "");

            return File.ReadAllText($"{path}\\data\\{file}.txt")
                   .Split(split)
                   .Select(s => s.Trim())
                   .Where(s => !string.IsNullOrEmpty(s))
                   .ToList();
        }

        public abstract string SolvePart1();
        public abstract string SolvePart2();
    }
}
