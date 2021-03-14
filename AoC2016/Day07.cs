using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Common;

namespace AoC2016
{
    public class Day07 : Solution
    {
        public Day07(string file) : base(file, "\n") { }

        public Day07(List<string> input) : base(input) { }

        public override string SolvePart1( ) => Input.Count(HasTlsSupport).ToString();

        public override string SolvePart2( ) => Input.Count(HasSslSupport).ToString();

        public bool HasTlsSupport(string ip)
        {
            var inBrackets = false;
            var isValid = false;

            foreach(var (c, i) in ip.Select((c, i) => (c, i)))
            {
                switch(c)
                {
                    case '[':
                        inBrackets = true;
                        continue;
                    case ']':
                        inBrackets = false;
                        continue;
                }

                if(i > ip.Length - 4) continue;

                if(c != ip[i + 3] || ip[i + 1] != ip[i + 2] || c == ip[i + 1]) continue;

                if(inBrackets) return false;
                isValid = true;
            }

            return isValid;
        }

        public bool HasSslSupport(string ip)
        {
            var inBrackets = false;
            var hasAba = false;
            var hasBab = false;

            foreach(var (c, i) in ip.Select((c, i) => (c, i)))
            {
                switch(c)
                {
                    case '[':
                        inBrackets = true;
                        continue;
                    case ']':
                        inBrackets = false;
                        continue;
                }

                if(inBrackets) continue;

                if(i > ip.Length - 3) continue;

                if(c != ip[i + 2] || c == ip[i + 1] || ip[i + 1] == '[' || ip[i + 1] == ']') continue;

                hasAba = true;
                var lookFor = new string(new[ ] { ip[i + 1], c, ip[i + 1] });

                foreach(Match match in Regex.Matches(ip, lookFor))
                {
                    if(match.Success)
                    {
                        var idx = match.Index;
                        while(idx > 0)
                        {
                            idx--;
                            if(ip[idx] == ']') break;
                            if(ip[idx] == '[') hasBab = true;
                            if(hasBab) break;
                        }
                    }
                    if(hasBab) break;
                }
            }
            return hasAba && hasBab;
        }
    }
}